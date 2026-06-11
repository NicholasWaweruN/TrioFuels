using Daraja.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public sealed class C2BRegistrationStartupService : IHostedService
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly ILogger<C2BRegistrationStartupService> _logger;

	public C2BRegistrationStartupService(
		IServiceScopeFactory scopeFactory,
		ILogger<C2BRegistrationStartupService> logger)
	{
		_scopeFactory = scopeFactory;
		_logger = logger;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Registering C2B URLs on startup...");

		try
		{
			// IHostedService is singleton; IC2BService is scoped.
			// We must create an explicit scope to resolve scoped services
			// from a singleton — otherwise the DI container throws a
			// captive dependency error at startup.
			await using var scope = _scopeFactory.CreateAsyncScope();
			var c2bService = scope.ServiceProvider.GetRequiredService<IC2BService>();

			await c2bService.RegisterMasterShortCodeAsync(cancellationToken);
			await c2bService.RegisterAllTillsAsync(cancellationToken);

			_logger.LogInformation("C2B URL registration complete.");
		}
		catch (Exception ex)
		{
			// Log but don't crash the host — the app can still serve other traffic.
			// Re-registration can be triggered manually via the admin endpoint.
			_logger.LogError(ex, "C2B startup registration failed. Manual re-registration may be required.");
		}
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}