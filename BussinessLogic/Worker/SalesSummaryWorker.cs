using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Worker.OtherReports;
using BussinessLogic.Worker.StockReports;
using DataAccessLayer.Helpers;
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

		public SalesSummaryWorker(ILogger<SalesSummaryWorker> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Sales Summary background service started at {Time}",EatTime.Now);

			while (!stoppingToken.IsCancellationRequested)
			{
				var now =EatTime.Now;
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
	}
}
