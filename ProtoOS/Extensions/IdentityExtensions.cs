using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;

namespace FuelFlow.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddIdentity<ApplicationUser, UserRoles>(options =>
			{
				options.User.RequireUniqueEmail = true;

				options.Password.RequiredLength = 8;
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;

				options.Lockout.DefaultLockoutTimeSpan =
					TimeSpan.FromMinutes(5);

				options.Lockout.MaxFailedAccessAttempts = 5;
			})
			.AddEntityFrameworkStores<OTOContext>()
			.AddDefaultTokenProviders();

			return services;
		}
	}
}