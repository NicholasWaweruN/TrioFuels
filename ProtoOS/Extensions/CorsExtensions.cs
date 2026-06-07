namespace FuelFlow.Extensions;

public static class CorsExtensions
{
	/// <summary>
	/// Registers the CORS policy used by <c>app.UseCors("AllowAll")</c>.
	/// </summary>
	/// <remarks>
	/// "AllowAll" is intentionally permissive for development / internal APIs.
	/// For production, replace with an origin-restricted policy:
	/// <code>
	///     policy.WithOrigins("https://app.fuelflow.co.ke")
	///           .AllowAnyHeader()
	///           .AllowAnyMethod();
	/// </code>
	/// </remarks>
	public static IServiceCollection AddCorsServices(
		this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", policy =>
			{
				policy
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
			});
		});

		return services;
	}
}