namespace FuelFlow.Extensions;

public static class LoggingExtensions
{
	/// <summary>
	/// Configures application logging.
	/// <list type="bullet">
	///   <item>Development: Console + Debug, full verbosity.</item>
	///   <item>Production: Console only; EF Core noise suppressed to Warning.</item>
	/// </list>
	/// </summary>
	public static IServiceCollection AddApplicationLogging(
		this IServiceCollection services,
		WebApplicationBuilder builder)
	{
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();

		if (builder.Environment.IsDevelopment())
		{
			builder.Logging.AddDebug();
		}

		if (builder.Environment.IsProduction())
		{
			// Suppress the high-frequency EF Core query logs in production.
			builder.Logging.AddFilter(
				"Microsoft.EntityFrameworkCore",
				LogLevel.Warning);
		}

		return services;
	}
}