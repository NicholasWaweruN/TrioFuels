using BussinessLogic.PlateDetection;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BussinessLogic.Worker.Authentication
{
	public class AuthenticationWorker : BackgroundService
	{
		private readonly ILogger<AuthenticationWorker> _logger;
		private readonly IServiceProvider _serviceProvider;

		public AuthenticationWorker(ILogger<AuthenticationWorker> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Authentication background service started at {Time}", DateTime.UtcNow);

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					using var scope = _serviceProvider.CreateScope();
					await DeactivateInactiveUsersAsync(scope);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error in AuthenticationWorker");
				}

				// Calculate next 2:00 AM run time
				var now = DateTime.UtcNow;
				var nextRun = now.Date.AddDays(1).AddHours(2); // tomorrow at 2 AM

				var delay = nextRun - now;
				_logger.LogInformation("Next deactivation check scheduled at {NextRun}", nextRun);

				await Task.Delay(delay, stoppingToken);
			}
		}

		private async Task DeactivateInactiveUsersAsync(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetRequiredService<OTOContext>();

			var cutoffDate = DateTime.UtcNow.AddDays(-30);

			var inactiveUsers = await context.Users
				.Where(user =>
					user.LastLoginDate <= cutoffDate &&
					user.IsActive &&
					user.UserType != 2)
				.ToListAsync();


			if (inactiveUsers.Count != 0)
			{
				foreach (var user in inactiveUsers)
				{
					user.IsActive = false;
					user.DateModified = DateTime.UtcNow;
					user.ModifiedBy = "System";

					_logger.LogInformation(
						"Deactivating user {UserName} (ID: {UserId})",
						user.UserName,
						user.Id
					);

					var audit = new UserTrail
					{
						ActionType = "System Users",
						Message = $"[SYSTEM ACTION] User account deactivated due to inactivity.\n" +
								  $"Username: {user.UserName}\n" +
								  $"User ID: {user.Id}\n" +
								  $"Last Login: {user.LastLoginDate:yyyy-MM-dd HH:mm:ss}\n" +
								  $"Action Performed By: System\n" +
								  $"Deactivation Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
						DateCreated = DateTime.UtcNow,
						UserCode = "System",
						UserName = user.UserName ?? user.Id,
					};

					context.UserTrails.Add(audit);
				}

				await context.SaveChangesAsync();

				_logger.LogInformation(
					"Deactivated {Count} inactive users at {Time}",
					inactiveUsers.Count,
					DateTime.UtcNow
				);
			}
			else
			{
				_logger.LogInformation("No inactive users found at {Time}", DateTime.UtcNow);
			}
		}


	}
}
