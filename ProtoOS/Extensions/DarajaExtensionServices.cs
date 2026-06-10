
using Daraja.Services;
using FuelFlow.Services.Daraja;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;
using Safaricom_Daraja.Stk_Push;

namespace FuelFlow.Extensions;

public static class DarajaServiceExtensions
{
	/// <summary>
	/// Registers all Daraja services.
	/// Call from Program.cs: builder.Services.AddDaraja(builder.Configuration);
	/// </summary>
	public static IServiceCollection AddDaraja(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		// Bind config
		services.Configure<DarajaConfig>(options =>
		{
			configuration.GetSection(DarajaConfig.SectionName).Bind(options);
		});
		var cfg = configuration.GetSection(DarajaConfig.SectionName).Get<DarajaConfig>()
				  ?? throw new InvalidOperationException("Daraja configuration section is missing.");

		// Named HttpClient for Daraja — shared base URL, timeout
		services.AddHttpClient("FuelFlow", client =>
		{
			client.BaseAddress = new Uri(cfg.BaseUrl);
			client.Timeout = TimeSpan.FromSeconds(30);
		});

		// Core services
		services.AddSingleton<IDarajaTokenService, DarajaTokenService>();
		services.AddScoped<IStkPushService, StkPushService>();
		services.AddScoped<IC2BService, C2BService>();
		services.AddScoped<IPullTransactionService, PullTransactionService>();
		services.AddScoped<IPullTransactionImportService, PullTransactionImportService>();


		// Callback handlers
		services.AddScoped<IStkCallbackHandler, StkCallbackHandler>();

		return services;
	}
}