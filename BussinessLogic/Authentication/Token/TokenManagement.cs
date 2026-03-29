using BusinessLogic.Authentication.Token;
using BusinessLogic.EmailService;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BussinessLogic.Authentication.Token
{
	public class TokenManagement : ITokenManagement
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly OTOContext _context;
		private readonly IMessagingService _messaging;

		public TokenManagement(IConfiguration configuration,
			UserManager<ApplicationUser> userManager,
			IHttpContextAccessor contextAccessor,
			OTOContext context,
			IMessagingService messaging,
			IMemoryCache memoryCache)
		{
			_configuration = configuration;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
			_context = context;
			_messaging = messaging;
			_memoryCache = memoryCache;
		}

		public async Task<TokenDto> CreateToken(ApplicationUser user, string appCode)
		{
			// fetch roles
			var roleList = await (from rt in _context.RoleToUser
								  join ur in _context.RoleAndPermisions on rt.RoleCode equals ur.RoleCode
								  join r in _context.Roles on ur.PermissionCode equals r.Id
								  where rt.UserCode == user.UserCode
								  select new { RoleName = r.Name })
								  .ToListAsync();

			var roles = roleList.Select(x => x.RoleName).Distinct().ToList();

			// IMPORTANT: Update SecurityStamp to revoke previous sessions
			await _userManager.UpdateSecurityStampAsync(user);

			// rebuild user with new stamp
			var foundUser = await _userManager.FindByIdAsync(user.Id);
			if (foundUser != null)
			{
				user = foundUser;
			}
			else
			{
				// Optionally handle the case where the user is not found, e.g. throw or return error
				throw new InvalidOperationException("User not found after updating security stamp.");
			}

			var claims = new List<Claim>
			{
				new("UniqueId", Guid.NewGuid().ToString()),
				new(ClaimTypes.NameIdentifier, user.Id),
				new(ClaimTypes.GivenName, user.FirstName),
				new(ClaimTypes.Surname, user.LastName),
				new("username", user.UserName ?? string.Empty),
				new("id", user.Id),
				new("Name", $"{user.FirstName} {user.MiddName} {user.LastName}"),
				new("PayrollNumber", user.PayrollNumber ?? string.Empty),
				new("PhoneNumber", user.PhoneNumber ?? string.Empty),
				new("UserCode", user.UserCode ?? string.Empty),
				new("Email", user.Email ?? string.Empty),
				new("DepartmentCode", user.DepartmentCode ?? string.Empty),
				new("stamp", user.SecurityStamp ?? string.Empty) // 🔑 SecurityStamp claim
			};

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var token = GenerateJwtToken(claims);

			return new TokenDto { Tooken = token };
		}

		private string GenerateJwtToken(IEnumerable<Claim> claims)
		{
			string? tokenKey = _configuration.GetValue<string>("TokenKey");
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey ?? string.Empty));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(720), // 12-hour session
				signingCredentials: cred
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		// Optional manual revocation
		public async Task<ServiceResponse<object>> ExpireTokenAsync(string token)
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(token);
				var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

				if (string.IsNullOrEmpty(userId))
					return ServiceResponse<object>.Information("Invalid token", null);

				var user = await _userManager.FindByIdAsync(userId);
				if (user == null)
					return ServiceResponse<object>.Information("User not found", null);

				await _userManager.UpdateSecurityStampAsync(user);
				return ServiceResponse<object>.Information("Token expired successfully", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Unexpected error. Kindly contact Admin.", null);
			}
		}

		public async Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber)
		{
			var otp = new Random().Next(10000, 99999);
			await _messaging.SendSmsAsync(phoneNumber, otp.ToString());
			return ServiceResponse<object>.Success("OTP Sent Successfully", null);
		}
	}
}
