using BussinessLogic.Worker;
using BussinessLogic.Worker.Authentication;
using BussinessLogic.Worker.Roles;

namespace FuelFlow.Extensions;
//
public static class BackgroundWorkerExtensions
{
	/// <summary>
	/// Registers all <see cref="IHostedService"/> background workers.
	/// Workers run concurrently on the thread-pool once the host starts.
	/// </summary>
	public static IServiceCollection AddBackgroundWorkers(
		this IServiceCollection services)
	{
		services.AddHostedService<EmailBackgroundService>();
		services.AddHostedService<SalesSummaryWorker>();
		services.AddHostedService<AuthenticationWorker>();
		//services.AddHostedService<ApiPermissionRoleSeeder>();

		return services;
	}
}