using BusinessLogic.EmailService;
using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Messaging;
using BussinessLogic.Sales.Sales_ForeCast;
using BussinessLogic.Sales.Wallet;
using BussinessLogic.Stock.VarianceReport;
using BussinessLogic.Worker.OtherReports;
using BussinessLogic.Worker.RecordedTotalizer_Readings;
using BussinessLogic.Worker.SalesReport;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models.IdentityGovernance;
using Microsoft.IdentityModel.Tokens;


namespace BussinessLogic.Worker;
public class EmailBackgroundService : BackgroundService
{
	private readonly ILogger<EmailBackgroundService> _logger;
	private readonly IServiceProvider _serviceProvider;
	

	public EmailBackgroundService(ILogger<EmailBackgroundService> logger, IServiceProvider serviceProvider)
	{
		_logger = logger;
		_serviceProvider = serviceProvider;
	}

	private DateTime? _lastForecastSalesRun;
	private DateTime? _lastMonthlySalesReportRun;
	private DateTime? _lastWalletBalanceReportRun;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Email background service is running.");

		while (!stoppingToken.IsCancellationRequested)
		{
			var currentTime = DateTime.UtcNow;
			using var scope = _serviceProvider.CreateScope();

			try
			{
				if (currentTime.Hour == 16 && currentTime.Minute == 0 && (_lastForecastSalesRun == null || _lastForecastSalesRun.Value.Date != currentTime.Date))
				{
					await ForecastSales(scope, currentTime);
					_lastForecastSalesRun = currentTime;
				}

				if (currentTime.Hour == 2 && currentTime.Minute <= 5 && (_lastMonthlySalesReportRun == null || _lastMonthlySalesReportRun.Value.Date != currentTime.Date))
				{
					
					await GenerateMonthlySalesReport(scope, currentTime);
					_lastMonthlySalesReportRun = currentTime;
				}

				if (currentTime.Hour == 12 && currentTime.Minute <= 59 && (_lastWalletBalanceReportRun == null || _lastWalletBalanceReportRun.Value.Date != currentTime.Date))
				{
					await WalletBalanceReport(scope);
					_lastWalletBalanceReportRun = currentTime;
				}

				await ApplyScheduledPrices(scope, currentTime);
				await SendRescheduledMessages(scope);
				await VarianceReports(scope, currentTime);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred during the background service execution.");
			}

			await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
		}
	}
	private static async Task GenerateMonthlySalesReport(IServiceScope scope, DateTime currentTime)
	{
		var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
		var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();

		var month = currentTime.Month;
		var year = currentTime.Year;
		var emails = await emailRecipients.GetRecipients("002");

		await salesReportService.GenerateMonthlySalesReportAsync(month, year, emails ?? new Mails());
	}
	private static async Task ForecastSales(IServiceScope scope, DateTime currentTime)
	{
		var salesReportService = scope.ServiceProvider.GetRequiredService<Forecast>();
		await salesReportService.GetForeCastData();
	}
	private static async Task WalletBalanceReport(IServiceScope scope)
	{
		var walletService = scope.ServiceProvider.GetRequiredService<TransactionsSummaries>();
		var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();

		var emails = await emailRecipients.GetRecipients("003");
		await walletService.ComprehensiveCustomerBalances(emails ?? new Mails());
	}
	private async Task ApplyScheduledPrices(IServiceScope scope, DateTime currentTime)
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<OTOContext>();
		var priceCache = new Dictionary<(string ProductCode, string StationCode), Price>();

		try
		{
			// Fetch schedules to apply or revert
			var schedules = await dbContext.PriceSchedules
				.Where(ps =>
					(ps.StartTime <= currentTime && ps.EndTime > currentTime && !ps.IsActive && !ps.Processed) ||
					(ps.EndTime <= currentTime && ps.IsActive))
				.ToListAsync();

			var applyList = schedules
				.Where(ps => ps.StartTime <= currentTime && ps.EndTime > currentTime && !ps.IsActive && !ps.Processed)
				.ToList();

			var revertList = schedules
				.Where(ps => ps.EndTime <= currentTime && ps.IsActive)
				.ToList();

			// Apply new prices
			var applyTasks = applyList.Select(async schedule =>
			{
				try
				{
					await UpdatePrice(schedule, dbContext, priceCache, schedule.ScheduledPrice);
					schedule.IsActive = true;
					schedule.Processed = true;
					dbContext.Update(schedule); // Explicitly mark entity as modified
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Failed to apply price schedule for Product {ProductCode} at Station {StationCode}", schedule.ProductCode, schedule.StationCode);
				}
			});

			// Revert to original prices
			var revertTasks = revertList.Select(async schedule =>
			{
				try
				{
					await UpdatePrice(schedule, dbContext, priceCache, schedule.OriginalPrice);
					schedule.IsActive = false;
					dbContext.Update(schedule); // Explicitly mark entity as modified
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Failed to revert price schedule for Product {ProductCode} at Station {StationCode}", schedule.ProductCode, schedule.StationCode);
				}
			});

			await Task.WhenAll(applyTasks.Concat(revertTasks));
			await dbContext.SaveChangesAsync();

			_logger.LogInformation("Applied {ApplyCount} and reverted {RevertCount} price schedules.",applyList.Count, revertList.Count);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Critical error applying or reverting scheduled prices.");
		}
	}
	private static async Task UpdatePrice(PriceSchedule schedule, OTOContext dbContext, Dictionary<(string, string), Price> priceCache, decimal newPrice)
	{
		if (!priceCache.TryGetValue((schedule.ProductCode, schedule.StationCode), out var price))
		{
			price = await dbContext.Prices.FirstOrDefaultAsync(p => p.ProductCode == schedule.ProductCode && p.StationCode == schedule.StationCode);

			if (price != null)
			{
				dbContext.Attach(price);
				priceCache[(schedule.ProductCode, schedule.StationCode)] = price;
				var message = $"Price for {schedule.ProductCode} at {schedule.StationCode} updated from {price.Amount} to {newPrice} ";
			}
		}

		if (price != null)
		{
			price.Amount = newPrice;
			dbContext.Update(price);
		}
	}


	// update PriceAdjustedByAmount


	private async Task SendRescheduledMessages(IServiceScope scope)
	{
		
		var dbContext = scope.ServiceProvider.GetRequiredService<OTOContext>();
		var messageService = scope.ServiceProvider.GetRequiredService<BulkSms>();
		var ValidatePhoneNumber = scope.ServiceProvider.GetRequiredService<IMessagingService>();

		try
		{
			var unsentMessages = await dbContext.RescheduledMessages
				.Where(sms => !sms.IsSent && sms.ScheduledSendingdate <= DateTime.UtcNow && sms.PhoneNumber != string.Empty)
				.ToListAsync();

			foreach (var sms in unsentMessages)
			{
				List<string> phoneNumbers = [];

				var validPhone = ValidatePhoneNumber.NormalizePhoneNumber(sms.PhoneNumber);
				
				if(string.IsNullOrEmpty(validPhone))
				{
					_logger.LogWarning(message: $"Invalid phone number for message ID {sms.Id}: {sms.PhoneNumber}");
					continue;
				}
				else
				{
					phoneNumbers.Add(validPhone);
					var result = await messageService.BulkMessages(phoneNumbers, sms.Message,sms.SenderId);
					sms.IsSent = true;
					sms.DateSent = DateTime.UtcNow;
					dbContext.Update(sms);
					await dbContext.SaveChangesAsync();
				}
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error sending rescheduled messages.");
		}
	}
	private static async Task VarianceReports(IServiceScope scope, DateTime CurrentTime)
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<OTOContext>();
		var varianceReport = scope.ServiceProvider.GetRequiredService<VarianceReport>();
		var auditReport = scope.ServiceProvider.GetRequiredService<FraudAuditReport>();
		var getEmailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
		var shiftsales = scope.ServiceProvider.GetRequiredService<ShiftsSales>();

		var varianceShifts = await dbContext.Shifts
			.Where(s => s.ShiftStatus == 2 && !s.IsEmailSent)
			.ToListAsync();

		var twelveHoursAgo = DateTime.UtcNow.AddHours(-14);

		var varianceShift = await dbContext.Shifts
			.Where(s => s.ShiftStartTime >= twelveHoursAgo && s.ShiftStatus != 1)
			.ToListAsync();


		foreach (var variance in varianceShifts)
		{
			await varianceReport.GetVarianceReport(variance.ShiftNumber);

			variance.IsEmailSent = true;
			dbContext.Update(variance);
			await dbContext.SaveChangesAsync();

			var emails = await getEmailRecipients.GetRecipients("011");
			if (emails is not null)
			{
				await auditReport.ShiftAuditReport(emails, variance.ShiftNumber);
			}
			var email = await getEmailRecipients.GetRecipients("014");
			if (email is not null)
			{
				await auditReport.UngaPromoReport(email, variance.ShiftNumber);
			}
		}

	}
	private static void PromotionalData(IServiceScope scope, DateTime currentTime)
	{
		var salesReportService = scope.ServiceProvider.GetRequiredService<PromotionReport>();
		salesReportService.GeneratePromotionReport(new DateTime(2025, 01, 08), new DateTime(2025, 01, 31), new DateTime(2025, 01, 01), new DateTime(2025, 01, 07));
	}
}
