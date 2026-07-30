using BussinessLogic.Services.Daraja;
using DataAccessLayer.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Worker.PullTransactions;

/// <summary>
/// Runs once at app startup and backfills the last 24 hours of M-Pesa
/// transactions for every configured till, catching anything C2B never
/// posted. Gated behind "Daraja:RunStartupBackfill" in config so it only
/// fires when you deliberately turn it on (e.g. right after a deploy where
/// you know there's a gap to catch up), rather than re-pulling 24h of data
/// on every restart. Safe to leave the flag on if you want — the import is
/// idempotent (upserts by TransID) — but it's wasted API calls if there's
/// nothing new to catch.
/// </summary>
public sealed class PullBackfillStartupService(
	IServiceScopeFactory scopeFactory,
	IConfiguration configuration,
	ILogger<PullBackfillStartupService> logger) : IHostedService
{
	public async Task StartAsync(CancellationToken ct)
	{
		var enabled = configuration.GetValue<bool>("Daraja:RunStartupBackfill");

		if (!enabled)
		{
			logger.LogInformation("Startup backfill skipped (Daraja:RunStartupBackfill is false or unset).");
			return;
		}

		try
		{
			await using var scope = scopeFactory.CreateAsyncScope();
			var importService = scope.ServiceProvider.GetRequiredService<IPullTransactionImportService>();

			var to = EatTime.Now;
			var from = to.AddHours(-24);

			logger.LogInformation("Startup backfill starting | window [{From} - {To}]", from, to);

			var results = await importService.BackfillAllTillsAsync(from, to, ct);

			foreach (var (tillNumber, result) in results)
			{
				if (!result.Success)
				{
					logger.LogError("Startup backfill failed for Till {Till}: {Error}", tillNumber, result.Error);
					continue;
				}

				logger.LogInformation(
					"Startup backfill complete for Till {Till}: {Inserted} added, {Updated} matched/modified, {Skipped} failures",
					tillNumber, result.Inserted, result.Updated, result.Skipped);
			}
		}
		catch (Exception ex)
		{
			// Don't crash startup over a backfill hiccup — the hourly
			// PullTransactionWorker will still run its normal rolling window.
			logger.LogError(ex, "Startup backfill task blew up.");
		}
	}

	public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}