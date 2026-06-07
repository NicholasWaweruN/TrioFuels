using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FuelFlow.Extensions;

public static class JwtExtensions
{
	public static IServiceCollection AddJwtAuthentication(
		this IServiceCollection services,
		IConfiguration configuration,
		IWebHostEnvironment environment)
	{
		var tokenKey = configuration["TokenKey"];

		if (string.IsNullOrWhiteSpace(tokenKey))
			throw new InvalidOperationException(
				"Required configuration key 'TokenKey' is missing.");

		var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey));

		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				// Enforce HTTPS metadata validation outside of development.
				options.RequireHttpsMetadata = !environment.IsDevelopment();
				options.SaveToken = true;

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = signingKey,

					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,

					// Zero clock-skew: tokens expire at exactly the stated time.
					ClockSkew = TimeSpan.Zero
				};
			});

		return services;
	}
}