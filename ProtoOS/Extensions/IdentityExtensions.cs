using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;

namespace FuelFlow.Extensions;

public static class IdentityExtensions
{
	public static IServiceCollection AddIdentityServices(
		this IServiceCollection services)
	{
		services
			.AddIdentity<ApplicationUser, UserRoles>(options =>
			{
				// ── User ────────────────────────────────────────────────────
				options.User.RequireUniqueEmail = true;

				// ── Password ────────────────────────────────────────────────
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;

				// ── Lockout ─────────────────────────────────────────────────
				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

				// ── Sign-in ─────────────────────────────────────────────────
				// RequireConfirmedAccount = true enforces the full account
				// confirmation flow; flip RequireConfirmedEmail to true when
				// you are ready to send confirmation emails.
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedAccount = true;
			})
			.AddEntityFrameworkStores<OTOContext>()
			.AddDefaultTokenProviders();

		return services;
	}
}