using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FuelFlow.Extensions;

public static class DatabaseExtensions
{
	public static IServiceCollection AddDatabaseServices(
		this IServiceCollection services,
		IConfiguration configuration,
		IWebHostEnvironment environment)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrWhiteSpace(connectionString))
			throw new InvalidOperationException(
				"Required configuration key 'ConnectionStrings:DefaultConnection' is missing.");

		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

		if (environment.IsDevelopment())
		{
			// Logs parameter values – never enable this in production.
			dataSourceBuilder.EnableParameterLogging();
		}

		var dataSource = dataSourceBuilder.Build();

		services.AddDbContext<OTOContext>(options =>
		{
			options.UseNpgsql(dataSource, npgsql =>
			{
				npgsql.CommandTimeout(60);

				npgsql.EnableRetryOnFailure(
					maxRetryCount: 3,
					maxRetryDelay: TimeSpan.FromSeconds(5),
					errorCodesToAdd: null);

				npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

				npgsql.MigrationsAssembly("DataAccessLayer");
			});

			// NoTracking globally; use AsTracking() on write operations explicitly.
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

			options.EnableSensitiveDataLogging(environment.IsDevelopment());
			options.EnableDetailedErrors(environment.IsDevelopment());
		});

		return services;
	}
}