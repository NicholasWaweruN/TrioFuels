using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Authentication;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BussinessLogic.Authentication.CommonTasks
{
	public class AuthCommonTasks : IAuthCommonTasks
	{
		private readonly IHttpContextAccessor _httpContextAccessor; // Accessor to retrieve HTTP context and user claims
		private readonly OTOContext _context; // Database context for accessing data models

		public AuthCommonTasks(IHttpContextAccessor httpContextAccessor, OTOContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_context = context;
		}

		// Retrieves the value of a specific user claim from the current HTTP context.
		private string GetClaimValue(string claimType) =>
			_httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value ?? string.Empty;

		// Retrieves the user's code from the claims.
		public string Usercode() => GetClaimValue("UserCode");

		// Retrieves the user's username from the claims.
		public string Username() => GetClaimValue("username");

		// Retrieves the user's full name from the claims.
		public string Name() => GetClaimValue("Name");

		// Retrieves the user's email from the claims.
		public string Email() => GetClaimValue("email");

		// Retrieves the user's phone number from the claims.
		public string PhoneNumber() => GetClaimValue("phoneNumber");

		// Retrieves the user's payroll number from the claims.
		public string PayrollNumber() => GetClaimValue("payrollNumber");

		// Adds a new user trail to track user actions and activity.
		public async Task<ServiceResponse<object>> AddUserTrail(string message, string actionType)
		{
			try
			{
				// Create a new UserTrail entity to log user activity
				var userTrail = new UserTrail
				{
					ActionType = actionType,
					Message = message,
					UserCode = Usercode(), // From current user claims
					UserName = Name(), // From current user claims
					DateCreated = DateTime.UtcNow,
				};

				// Adds to DB
				_context.UserTrails.Add(userTrail);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("User trail added successfully", userTrail);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"Error adding user trail: {ex.Message}", null);
			}
		}

		public async Task ErrorTrail(ErrorTrail errorTrail)
		{
			try
			{
				var sql = $@"
					INSERT INTO ErrorTrails 
						(DateCreated, ErrorCode, ErrorMessage, Method, InnerErrorMessage, StackTrace)
					VALUES 
						('{DateTime.UtcNow}', '{errorTrail.ErrorCode ?? ""}', '{errorTrail.ErrorMessage ?? ""}', 
						 '{errorTrail.Method ?? ""}', '{errorTrail.InnerErrorMessage ?? ""}', 
						 '{errorTrail.StackTrace ?? ""}')";

				await _context.Database.ExecuteSqlRawAsync(sql);
			}
			catch (Exception)
			{

			}
		}



		// Retrieves details of the current user based on their UserCode from the claims.
		public async Task<ServiceResponse<object>> GetUserDetails()
		{
			try
			{
				var roles = await (from rt in _context.RoleToUser
								   join ur in _context.RoleAndPermisions on rt.RoleCode equals ur.RoleCode
								   join r in _context.Roles on ur.PermissionCode equals r.Id
								   where rt.UserCode == Usercode()
								   select r.ApiPermission).ToListAsync();


				// Retrieves a list of API permissions assigned to the user's roles
				var roleList = await _context.ApiPermisions
					.Where(ap => _context.Roles.Any(r => r.Id == ap.RoleId))
					.Select(ap => ap.ApiPermission)
					.ToListAsync();

				// Retrieves user details along with their roles
				var user = await _context.Users
					.Where(u => u.UserCode == Usercode()) // Filters by current user's UserCode
					.Select(u => new
					{
						Names = $"{u.FirstName} {u.MiddName} {u.LastName}", // Concatenates the user's full name
						u.PayrollNumber, // User's payroll number
						u.PhoneNumber, // User's phone number
						u.Email, // User's email address
						u.UserCode, // User's code
						Roles = roles, // List of user roles
						u.IsActive, // Indicates whether the user is active
						DateCreated = u.DateCreated.ToString("yyyy-MMM-dd"), 
						// Formats the date of user creation
					}).FirstOrDefaultAsync();

				// Returns the user details if found
				if (user != null)
				{
					return ServiceResponse<object>.Success("User details retrieved successfully", user);
				}
				// Returns an information response if user details are not found
				return ServiceResponse<object>.Information("User details not found", null);
			}
			catch (Exception ex)
			{
				// Returns an error response in case of an exception
				return ServiceResponse<object>.Error($"Error retrieving user details: {ex.Message}", null);
			}
		}

		// Retrieves detailed information of a user based on a provided user code.
		public async Task<ServiceResponse<UserDetailsDto>> GetUserDetailsAsync(string? userCode)
		{
			try
			{

				userCode = string.IsNullOrEmpty(userCode) ? Usercode() : userCode;

				// Retrieves the user entity based on the provided user code
				var user = await _context.Users.FirstOrDefaultAsync(u => u.UserCode == userCode);
				if (user != null)
				{
					// Maps the user entity to a UserDetailsDto object
					var userDetails = new UserDetailsDto
					{
						FirstName = user.FirstName,
						MiddName = user.MiddName,
						LastName = user.LastName,
						Email = user.Email ?? string.Empty,
						PhoneNumber = user.PhoneNumber ?? string.Empty,
						PayrollNumber = user.PayrollNumber,
						DateAdded = user.DateCreated,
						IsActive = user.IsActive,
					};

					// Returns a success response with the user's details
					return ServiceResponse<UserDetailsDto>.Success($"{user.FirstName} exists", userDetails);
				}
				// Returns an information response if the user is not found
				return ServiceResponse<UserDetailsDto>.Information("User not found", null);
			}
			catch (Exception ex)
			{
				// Returns an error response in case of an exception
				return ServiceResponse<UserDetailsDto>.Error($"Error retrieving user: {ex.Message}", null);
			}
		}


		public string HashPin(string pin)
		{
			using var rng = RandomNumberGenerator.Create();
			byte[] salt = new byte[16];
			rng.GetBytes(salt);

			using var pbkdf2 = new Rfc2898DeriveBytes(pin, salt, 10000, HashAlgorithmName.SHA256);
			byte[] hash = pbkdf2.GetBytes(32);

			byte[] hashBytes = new byte[salt.Length + hash.Length];
			Array.Copy(salt, 0, hashBytes, 0, salt.Length);
			Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

			return Convert.ToBase64String(hashBytes);
		}
	}
}
