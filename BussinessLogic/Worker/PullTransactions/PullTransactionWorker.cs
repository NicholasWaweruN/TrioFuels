using BussinessLogic.Services.Daraja;
using DataAccessLayer.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Worker.PullTransactions;

/// <summary>
/// Background worker that pulls M-Pesa transactions from Safaricom on a
/// rolling hourly cycle and upserts them into MpesaTransactions.
///
/// This worker only covers the rolling window going forward — it does NOT
/// retroactively fix transactions already missing from before this fix
/// shipped. For that, use IPullTransactionImportService.BackfillRangeAsync
/// (or an admin endpoint) once, manually, for the affected date range.
/// </summary>
public sealed class PullTransactionWorker(
	IServiceScopeFactory scopeFactory,
	ILogger<PullTransactionWorker> logger) : BackgroundService
{
	private static readonly TimeSpan Interval = TimeSpan.FromHours(1);
	private static readonly TimeSpan LookbackWindow = TimeSpan.FromHours(1) + TimeSpan.FromMinutes(10);
	private static readonly TimeSpan StartupDelay = TimeSpan.FromSeconds(30);

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		logger.LogInformation("PullTransactionWorker started. Running every {Interval}, lookback {Lookback}.",Interval, LookbackWindow);

		await Task.Delay(StartupDelay, stoppingToken);

		while (!stoppingToken.IsCancellationRequested)
		{
			await RunPullCycleAsync(stoppingToken);
			await Task.Delay(Interval, stoppingToken);
		}

		logger.LogInformation("PullTransactionWorker stopped.");
	}

	private async Task RunPullCycleAsync(CancellationToken ct)
	{
		var to = EatTime.Now;
		var from = to.Subtract(LookbackWindow);

		logger.LogInformation("Pull cycle starting at {Now} | window [{From} - {To}]", to, from, to);

		using var scope = scopeFactory.CreateScope();
		var importService = scope.ServiceProvider.GetRequiredService<IPullTransactionImportService>();

		try
		{
			var results = await importService.ImportAllTillsAsync(from, to, ct);

			foreach (var (tillNumber, result) in results)
			{
				if (!result.Success)
				{
					logger.LogError("Pull cycle failed for Till {Till}: {Error}", tillNumber, result.Error);
					continue;
				}

				logger.LogInformation(
					"Pull cycle complete for Till {Till}: {Inserted} added, {Updated} matched/modified, {Skipped} failures",
					tillNumber, result.Inserted, result.Updated, result.Skipped);
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull cycle threw an unhandled exception.");
		}
	}
}