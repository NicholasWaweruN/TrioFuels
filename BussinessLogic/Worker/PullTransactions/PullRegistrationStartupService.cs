using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Safaricom_Daraja;

namespace BussinessLogic.Worker.PullTransactions;

/// <summary>
/// Runs once at app startup and registers Pull for every configured till.
/// Safe to run on every restart — Safaricom returns "already registered" (1001)
/// rather than an error for tills that are already active, so this is idempotent.
/// </summary>
public sealed class PullRegistrationStartupService(
	IServiceScopeFactory scopeFactory,
	ILogger<PullRegistrationStartupService> logger) : IHostedService
{
	public async Task StartAsync(CancellationToken ct)
	{
		try
		{
			await using var scope = scopeFactory.CreateAsyncScope();
			var pullService = scope.ServiceProvider.GetRequiredService<IPullTransactionService>();

			var results = await pullService.RegisterAllTillsAsync(ct);

			var failed = results.Where(r => !r.Value.Success).ToList();
			if (failed.Count > 0)
			{
				foreach (var f in failed)
					logger.LogError("Pull registration failed for Till {Till}: {Error}", f.Key, f.Value.ErrorMessage);
			}
			else
			{
				logger.LogInformation("Pull registration confirmed for all {Count} configured tills.", results.Count);
			}
		}
		catch (Exception ex)
		{
			// Don't crash startup over a registration hiccup — PullTransactionWorker's
			// hourly queries will still run, they just may keep returning 1001 until
			// this is retried (e.g. next deploy/restart) or triggered manually.
			logger.LogError(ex, "Pull registration startup task blew up.");
		}
	}

	public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}