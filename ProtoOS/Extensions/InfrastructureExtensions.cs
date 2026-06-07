using DataAccessLayer.DTOs.Messaging;

namespace FuelFlow.Extensions;

public static class InfrastructureExtensions
{
	/// <summary>
	/// Binds strongly-typed settings objects from <c>appsettings.json</c>
	/// (or environment variables / secrets) into the DI container so they
	/// can be injected via <c>IOptions&lt;T&gt;</c>.
	/// </summary>
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<SmtpSettings>(
			configuration.GetSection("SmtpSettings"));

		services.Configure<AfricaIsTalkingSettings>(
			configuration.GetSection("AfricasTalking"));

		services.Configure<ProAfricaIsTalkingSettings>(
			configuration.GetSection("ProAfricasTalking"));

		services.Configure<SafetyAfricasTalking>(
			configuration.GetSection("SafetyAfricasTalking"));

		return services;
	}
}