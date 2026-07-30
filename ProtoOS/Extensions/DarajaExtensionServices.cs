using BussinessLogic.Services.Daraja;
using BussinessLogic.Worker.PullTransactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Safaricom_Daraja;
using Safaricom_Daraja.C2bService;
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
		services.AddHttpClient("Daraja", client =>
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
		//services.AddHostedService<C2BRegistrationStartupService>();
		services.AddHostedService<PullBackfillStartupService>(); // add this line
																		 // Registers Pull for every configured till once at startup. Idempotent —
																		 // safe to leave in even after registration has already succeeded once.
		services.AddHostedService<PullRegistrationStartupService>();

		services.AddScoped<StkPushDiagnosticService>();

		// Callback handlers
		services.AddScoped<IStkCallbackHandler, StkCallbackHandler>();

		return services;
	}
}