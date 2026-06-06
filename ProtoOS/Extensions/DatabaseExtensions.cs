using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FuelFlow.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection AddDatabaseServices(
			this IServiceCollection services,
			IConfiguration configuration,
			IWebHostEnvironment environment)
		{
			var connectionString =
				configuration.GetConnectionString("DefaultConnection");

			var dataSourceBuilder =
				new NpgsqlDataSourceBuilder(connectionString);

			dataSourceBuilder.EnableParameterLogging();

			var dataSource = dataSourceBuilder.Build();

			services.AddDbContext<OTOContext>(options =>
			{
				options.UseNpgsql(dataSource, npgsql =>
				{
					npgsql.CommandTimeout(60);
					npgsql.EnableRetryOnFailure(3);
					npgsql.MigrationsAssembly("DataAccessLayer");
				});

				options.EnableSensitiveDataLogging(environment.IsDevelopment());
				options.EnableDetailedErrors(environment.IsDevelopment());
			});

			return services;
		}
	}
}