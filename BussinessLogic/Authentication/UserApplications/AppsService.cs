using BusinessLogic.Authentication.UserApplications;
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Authentication.UserApplications
{
	/// <summary>
	/// Service class for handling user applications related operations.
	/// </summary>
	public class AppsService : IAppsService
	{
		private readonly OTOContext _context;
		private readonly ILogger<AppsService> _logger;
		private readonly IAuthCommonTasks _tasks;
		/// <summary>
		/// Initializes a new instance of the <see cref="AppsService"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="logger">The logger instance.</param>
		public AppsService(OTOContext context, ILogger<AppsService> logger, IAuthCommonTasks tasks )
		{
			_context = context;
			_logger = logger;
			_tasks = tasks;
		}

		/// <summary>
		/// Retrieves all available applications.
		/// </summary>
		/// <returns>A ServiceResponse containing the list of applications.</returns>
		public async Task<ServiceResponse<object>> GetApps()
		{
			try
			{
				var apps = await (from a in _context.ProtoApps
								  select new
								  {
									  a.AppsCode,
									  a.AppsName
								  }).ToListAsync();

				if (apps.Count > 0)
					return ServiceResponse<object>.Success("Apps Retrieved Successfully", apps.OrderBy(x => x.AppsCode));
				return ServiceResponse<object>.Information("No Apps Found", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while getting apps", null);
			}
		}

		/// <summary>
		/// Adds a user application association asynchronously.
		/// </summary>
		/// <param name="userCode">The user code.</param>
		/// <param name="appsCode">The application code.</param>
		/// <returns>A ServiceResponse indicating the outcome of the operation.</returns>
		public async Task<ServiceResponse<object>> AddUserAppsAsync(string userCode, string appsCode)
		{
			try
			{
				//check user exists 
				var user = await _context.Users.FirstOrDefaultAsync(x => x.UserCode == userCode);
				var userApps = new UserApps
				{
					UserCode = userCode,
					AppsCode = appsCode
				};
				if (user is null)
					return ServiceResponse<object>.Information("User Not Found", null);

				var message = @$"User {user.FirstName} {user.MiddName} {user.LastName} added to app {appsCode} by {_tasks.Name} ";
				await _tasks.AddUserTrail(message, "User App Registration");
				await _context.UserApps.AddAsync(userApps);
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("User Apps Added Successfully", null);
			}
			catch (Exception ex)
			{ 
				_logger.LogError("Error adding user apps for user {userCode}: {error}", userCode, ex.Message);
				return ServiceResponse<object>.Error("An error occurred while adding user apps", null);
			}
		}

		/// <summary>
		/// Removes a user application association asynchronously.
		/// </summary>
		/// <param name="userCode">The user code.</param>
		/// <param name="appsCode">The application code.</param>
		/// <returns>A ServiceResponse indicating the outcome of the operation.</returns>
		public async Task<ServiceResponse<object>> RemoveUserAppsAsync(string userCode, string appsCode)
		{
			try
			{
				var userApps = await _context.UserApps.FirstOrDefaultAsync(x => x.UserCode == userCode && x.AppsCode == appsCode);
				if (userApps is null)
					return ServiceResponse<object>.Information("User Apps Not Found", null);

				_context.UserApps.Remove(userApps);
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("User Apps Removed Successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error removing user apps for user {userCode}: {error}", userCode, ex.Message);
				return ServiceResponse<object>.Error("An error occurred while removing user apps", null);
			}
		}

		/// <summary>
		/// Retrieves the applications associated with a specific user asynchronously.
		/// </summary>
		/// <param name="userCode">The user code.</param>
		/// <returns>A ServiceResponse containing the list of user applications.</returns>
		public async Task<ServiceResponse<object>> GetUserAppsAsync(string userCode)
		{
			try
			{
				var userApps = await (from a in _context.ProtoApps
									  join ua in _context.UserApps on a.AppsCode equals ua.AppsCode
									  where ua.UserCode == userCode
									  select new
									  {
										  a.AppsCode,
										  a.AppsName
									  }).ToListAsync();
				if (userApps.Count > 0)
					return ServiceResponse<object>.Success("User Apps Retrieved Successfully", userApps.OrderBy(x => x.AppsCode));
				return ServiceResponse<object>.Information("No User Apps Found", null);
			}
			catch (Exception)
			{
				return  ServiceResponse<object>.Error("An error occurred while getting user apps", null);
			}
		}

		/// <summary>
		/// Assigns a user to an application asynchronously.
		/// </summary>
		/// <param name="userCode">The user code.</param>
		/// <param name="appsCode">The application code.</param>
		/// <returns>A ServiceResponse indicating the outcome of the operation.</returns>
		public async Task<ServiceResponse<object>> AssignUserToAppAsync(string userCode, string appsCode)
		{
			try
			{
				// Check if the user already has access to the app.
				var hasAccess = await CheckUserAccessToApp(userCode, appsCode);
				if (hasAccess)
					return ServiceResponse<object>.Information("User Already Has Access To This App", null);

				var userApps = new UserApps
				{
					UserCode = userCode,
					AppsCode = appsCode
				};
				await _context.UserApps.AddAsync(userApps);
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("User Assigned To App Successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error assigning user to app for user {userCode}: {error}", userCode, ex.Message);
				return ServiceResponse<object>.Error("An error occurred while assigning user to app", null);
			}
		}

		/// <summary>
		/// Checks if a user has access to a specific application.
		/// </summary>
		/// <param name="userCode">The user code.</param>
		/// <param name="appsCode">The application code.</param>
		/// <returns>A boolean indicating whether the user has access to the application.</returns>
		private async Task<bool> CheckUserAccessToApp(string userCode, string appsCode)
		{
			try
			{
				var userApps = await (from ua in _context.UserApps
									  where ua.UserCode == userCode && ua.AppsCode == appsCode
									  select ua).AnyAsync();
				return userApps;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error checking user access to app for user {userCode}: {error}", userCode, ex.Message);
				return false;
			}
		}
	}
}
