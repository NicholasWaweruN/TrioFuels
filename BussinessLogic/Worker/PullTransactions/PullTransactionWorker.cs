using BussinessLogic.Services.Daraja;
using DataAccessLayer.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Safaricom_Daraja;

namespace BussinessLogic.Worker.PullTransactions;

/// <summary>
/// Background worker that pulls M-Pesa transactions from Safaricom
/// every hour and upserts them into the MpesaTransactions table.
/// </summary>
public sealed class PullTransactionWorker(IServiceScopeFactory scopeFactory, ILogger<PullTransactionWorker> logger) : BackgroundService
{
	private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		logger.LogInformation("PullTransactionWorker started. Running every {Interval}.", Interval);

		// Stagger startup by 30 seconds so the app finishes booting first
		await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

		while (!stoppingToken.IsCancellationRequested)
		{
			await RunPullAsync(stoppingToken);
			await Task.Delay(Interval, stoppingToken);
		}

		logger.LogInformation("PullTransactionWorker stopped.");
	}

	private async Task RunPullAsync(CancellationToken ct)
	{
		logger.LogInformation("Pull cycle starting at {Now}", EatTime.Now);

		using var scope = scopeFactory.CreateScope();
		var importService = scope.ServiceProvider.GetRequiredService<IPullTransactionImportService>();

		var to = EatTime.Now;
		var from = to.AddHours(-1);

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