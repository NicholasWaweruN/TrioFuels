using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Worker.OtherReports;
using BussinessLogic.Worker.RecordedTotalizer_Readings;
using BussinessLogic.Worker.StockReports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Worker
{
	public class SalesSummaryWorker : BackgroundService
	{
		private readonly ILogger<SalesSummaryWorker> _logger;
		private readonly IServiceProvider _serviceProvider;

		// Track last run for each report
		private DateTime _lastRunDailySummary;
		private DateTime _lastRunAbove100;
		private DateTime _lastRunTotalizerRecordings;
		private readonly DateTime _lastRunStockTakeSummaries;
		public SalesSummaryWorker(ILogger<SalesSummaryWorker> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Sales Summary background service started at {Time}", DateTime.UtcNow);

			while (!stoppingToken.IsCancellationRequested)
			{
				var now = DateTime.UtcNow;
				using var scope = _serviceProvider.CreateScope();

				try
				{
					// 00:00 - Daily Summary
					if (now.Hour == 0 && now.Minute <= 3 && _lastRunDailySummary.Date != now.Date)
					{
						_logger.LogInformation("Running GenerateDailySalesSummary at {Time}", now);
						await GenerateDailySalesSummary(scope, now);
						_lastRunDailySummary = now;
					}

					// 01:00 - Above100 + InstallationCost + TotalizerReadings + Telematic
					if (now.Hour == 1 && now.Minute <= 3 && _lastRunAbove100.Date != now.Date)
					{
						_logger.LogInformation("Running Above100, InstallationCost, TotalizerReadings, TelematicReport at {Time}", now);
						await Above100(scope, now);
						await InstallationCost(scope, now);
						await DailyTotalizerReadings(scope, now);
						await TelematicReport(scope, now);
						_lastRunAbove100 = now;
					}

					// 05:00 - Totalizer Recordings
					if (now.Hour == 5 && now.Minute <= 5 && _lastRunTotalizerRecordings.Date != now.Date)
					{
						_logger.LogInformation("Running DailyTotalizerRecordings at {Time}", now);
						await DailyTotalizerRecordings(scope, now);
						_lastRunTotalizerRecordings = now;
					}

					// 08:00 - Stock Take Summaries
					if (now.Hour == 8 && _lastRunStockTakeSummaries.Date != now.Date)
					{
						//_logger.LogInformation("Running StockTakeSummariesReport at {Time}", now);
						//await StockTakeSummariesReportAsync(scope, now);
						//_lastRunStockTakeSummaries = now;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Unhandled exception in SalesSummaryWorker");
				}

				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}

		private static async Task GenerateDailySalesSummary(IServiceScope scope, DateTime currentTime)
		{
			var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReport_Summary>();
			await Task.Run(() => salesReportService.GenerateMonthlyStationReportsToStream(currentTime.Year, currentTime.Month));
		}

		private static async Task Above100(IServiceScope scope, DateTime currentTime)
		{
			try
			{
				var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
				var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
				var emails = await emailRecipients.GetRecipients("006");
				await salesReportService.Above100(emails ?? new Mails());
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteLogs(ex.Message);
			}
		}

		private static async Task InstallationCost(IServiceScope scope, DateTime currentTime)
		{
			try
			{
				var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
				var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
				var emails = await emailRecipients.GetRecipients("009");
				await salesReportService.SendInstallationCostReportAsync(emails ?? new Mails());
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteLogs(ex.Message);
			}
		}

		private static async Task DailyTotalizerReadings(IServiceScope scope, DateTime currentTime)
		{
			try
			{
				var salesReportService = scope.ServiceProvider.GetRequiredService<TotalizerDailyReport>();
				await salesReportService.GenerateStyledTotalizerExcelWithAllDaysAsync();
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteLogs(ex.Message);
			}
		}

		private static async Task DailyTotalizerRecordings(IServiceScope scope, DateTime currentTime)
		{
			var salesReportService = scope.ServiceProvider.GetRequiredService<RecordTotalizerReadingReport>();
			var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
			var emails = await emailRecipients.GetRecipients("006");
			ErrorLogger.WriteLogs("Above100 report started");
			await salesReportService.DailyTotalizerRecordings(emails ?? new Mails());
		}

		private static async Task TelematicReport(IServiceScope scope, DateTime currentTime)
		{
			var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
			var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
			var emails = await emailRecipients.GetRecipients("012");
			await salesReportService.TelematicVehiclesSalesReport(emails ?? new Mails());
		}

		private static async Task StockTakeSummariesReportAsync(IServiceScope scope, DateTime currentTime)
		{
			var salesReportService = scope.ServiceProvider.GetRequiredService<StockTakeSummaryReport>();
			var emailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
			var emails = await emailRecipients.GetRecipients("015");
			await salesReportService.StockTakeSummariesReportAsync(emails ?? new Mails(), currentTime.Year, currentTime.Month);
		}
	}

	public static class ErrorLogger
	{
		public static void WriteLogs(string error)
		{
			string dir = @"C:\ErrorLogs\api\";
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); // FIXED

			string filePath = Path.Combine(dir, DateTime.UtcNow.ToString("yyyyMMdd") + ".txt");
			using StreamWriter write = new(filePath, true);
			write.WriteLine($"{DateTime.UtcNow}: {error}");
		}
	}
}
