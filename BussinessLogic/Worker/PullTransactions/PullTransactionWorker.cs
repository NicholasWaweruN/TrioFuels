using FuelFlow.Services.Daraja;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace BussinessLogic.Worker.PullTransactions;

/// <summary>
/// Background worker that pulls M-Pesa transactions from Safaricom
/// every hour and upserts them into the MpesaTransactions table.
/// </summary>
public sealed class PullTransactionWorker(
	IServiceScopeFactory scopeFactory,
	ILogger<PullTransactionWorker> logger) : BackgroundService
{
	private static readonly TimeSpan Interval = TimeSpan.FromHours(1);

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
		logger.LogInformation("Pull cycle starting at {Time}", DateTime.UtcNow);

		try
		{
			// Use a new scope per run — ImportService depends on scoped OTOContext
			await using var scope = scopeFactory.CreateAsyncScope();
			var importService = scope.ServiceProvider
				.GetRequiredService<IPullTransactionImportService>();

			// Pull the last 2 hours (overlap to avoid missing transactions near the boundary)
			var to = DateTime.UtcNow;
			var from = to.AddHours(-2);

			var results = await importService.ImportAllTillsAsync(from, to, ct);

			var totalInserted = results.Values.Sum(r => r.Inserted);
			var totalUpdated = results.Values.Sum(r => r.Updated);
			var totalSkipped = results.Values.Sum(r => r.Skipped);
			var failed = results.Values.Where(r => !r.Success).ToList();

			logger.LogInformation(
				"Pull cycle complete. Inserted={Inserted} Updated={Updated} Skipped={Skipped}",
				totalInserted, totalUpdated, totalSkipped);

			if (failed.Count > 0)
			{
				foreach (var f in failed)
					logger.LogError("Pull failed for till {Till}: {Error}", f.TillNumber, f.Error);
			}
		}
		catch (OperationCanceledException)
		{
			// App is shutting down — exit cleanly
		}
		catch (Exception ex)
		{
			// Log but don't crash the worker — it will retry next hour
			logger.LogError(ex, "Unhandled exception in pull cycle");
		}
	}
}