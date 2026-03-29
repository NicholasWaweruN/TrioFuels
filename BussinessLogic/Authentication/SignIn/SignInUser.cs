using BusinessLogic.Authentication.AddUsers;
using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Authentication.Token;
using BusinessLogic.EmailService;
using BusinessLogic.Messaging;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Authentication;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Authentication.Entity;

namespace BussinessLogic.Authentication.SignIn
{
	/// <summary>
	/// Handles user sign-in, authentication, and password management.
	/// </summary>
	public class SignInUser : ISignInUser
	{
		// Dependencies required for user sign-in operations.
		private readonly OTOContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICommonSetups _setups;
		private readonly IMessagingService _messagingService;
		private readonly IAfricaIsTalking _isTalking;
		private readonly IEmailService _emailService;
		private readonly ILogger<RegisterUsers> _logger;
		private readonly IAuthCommonTasks _authentication;
		private readonly ITokenManagement _token;
		private readonly IHttpContextAccessor _httpContextAccessor;
	

		// Constructor to initialize dependencies.
		public SignInUser(
			OTOContext context,
			UserManager<ApplicationUser> userManager,
			ICommonSetups setups,
			IMessagingService messagingService,
			IAfricaIsTalking isTalking,
			IEmailService emailService,
			ILogger<RegisterUsers> logger,
			IAuthCommonTasks authentication,
			ITokenManagement token,
			IHttpContextAccessor httpContextAccessor
			)
		{
			_context = context;
			_userManager = userManager;
			_setups = setups;
			_messagingService = messagingService;
			_isTalking = isTalking;
			_emailService = emailService;
			_logger = logger;
			_authentication = authentication;
			_token = token;
			_httpContextAccessor = httpContextAccessor;
		
		}

		/// <summary>
		/// Signs in a user based on email or phone number.
		/// </summary>
		public async Task<ServiceResponse<object>> SignInUserAsync(EmailLoginModel signIn)
		{
			return signIn.UserName.Contains('@')
				? await SignInUserByEmailAsync(signIn)
				: await SignInUserByPhoneNumberAsync(signIn);
		}

		/// <summary>
		/// Signs in a user using their email address.
		/// </summary>
		private async Task<ServiceResponse<object>> SignInUserByEmailAsync(EmailLoginModel signIn)
		{
			var user = await _userManager.FindByEmailAsync(signIn.UserName);
			return user == null
				? HandleInvalidUserName(signIn.UserName)
				: await ValidateUserCredentialsAsync(user, signIn.Password, signIn.AppCode, signIn.PdaDeviceImei, signIn.VersionCode);
		}

		/// <summary>
		/// Signs in a user using their phone number.
		/// </summary>
		private async Task<ServiceResponse<object>> SignInUserByPhoneNumberAsync(EmailLoginModel signIn)
		{
			// Normalize phone number and validate.
			signIn.UserName = _messagingService.NormalizePhoneNumber(signIn.UserName);

			if (!_messagingService.IsValidPhoneNumber(signIn.UserName))
				return ServiceResponse<object>.Information("Invalid phone number", null);

			// Check if user exists with the provided phone number.
			var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == signIn.UserName);
			return user == null
				? HandleInvalidUserName(signIn.UserName)
				: await ValidateUserCredentialsAsync(user, signIn.Password, signIn.AppCode, signIn.PdaDeviceImei, signIn.VersionCode);
		}

		/// <summary>
		/// Validates user credentials and checks device/app constraints.
		/// </summary>
		private async Task<ServiceResponse<object>> ValidateUserCredentialsAsync(ApplicationUser user, string password, string appCode, string pdaDeviceImei, string appVersion)
		{
			// App specific checks
			if (appCode.Equals(Apps.OtogasApp))
			{
				// Enforce minimum version
				var storedVersion = await _context.Setup.Select(s => s.App_VersionCode).FirstOrDefaultAsync();
				if (string.IsNullOrWhiteSpace(storedVersion) || !string.Equals(storedVersion, appVersion, StringComparison.OrdinalIgnoreCase))
				{
					return ServiceResponse<object>.Information("You have an outdated fuel app app. Kindly request for an update", null);
				}

				//Device binding
				var isCorrectDevice = await IsCorrectDevice(pdaDeviceImei, user.UserCode);
				if (!isCorrectDevice)
					return ServiceResponse<object>.Information("The PDADevice does not belong to that dispenser", null);

				// Shift status
				var isShiftOpen = await CheckOpenShift(user.UserCode);
				if (isShiftOpen.ResponseCode != Response.Success)
					return isShiftOpen;
			}

			// Validate the user password.
			var passwordCheckResult = await CheckUserPasswordAsync(user, password);
			if (passwordCheckResult.ResponseCode != Response.Success)
				return passwordCheckResult;

			return await GenerateUserTokenAsync(user, appCode);
		}

		/// <summary>
		/// Check password, lockout, expiry, updates counters coherently.
		/// </summary>
		private async Task<ServiceResponse<object>> CheckUserPasswordAsync(ApplicationUser user, string password)
		{
			if (!user.IsActive)
				return ServiceResponse<object>.Information("Your account is not active. Contact admin", null);

			// Hard limit
			const int maxTries = 5;
			if (user.AccessFailedCount >= maxTries)
				return ServiceResponse<object>.Information("You have reached maximum password tries. Kindly reset your password or contact Admin.", null);

			// Expiry check
			var isPassExpired = await IsPasswordExpiredAsync(user);
			if (isPassExpired)
				return ServiceResponse<object>.Information("Your password is expired. Kindly change it.", null);

			// Verify
			if (!await _userManager.CheckPasswordAsync(user, password))
			{
				user.AccessFailedCount += 1;
				_context.Users.Update(user);
				await _context.SaveChangesAsync();

				var remaining = Math.Max(0, maxTries - user.AccessFailedCount);
				return ServiceResponse<object>.Information($"Invalid password. {remaining} tries left before lockout.", null);
			}

			// Success path: reset counters and set last login
			user.AccessFailedCount = 0;
			user.LastLoginDate = DateTime.UtcNow;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			return ServiceResponse<object>.Success("Password valid", null);
		}

		/// <summary>
		/// Gets expiry days from setup (fallback 30) and evaluates expiry.
		/// </summary>
		private async Task<bool> IsPasswordExpiredAsync(ApplicationUser user)
		{
			// If never set, require change
			if (user.PasswordLastUpdated == null) return true;

			var expiryDays = await GetPasswordExpiryDaysAsync();
			var expirationDate = user.PasswordLastUpdated.Value.AddDays(expiryDays);
			return DateTime.UtcNow > expirationDate;
		}

		private async Task<int> GetPasswordExpiryDaysAsync()
		{
			// If your Setup has a PasswordExpiryDays column, use it, else fallback 30
			var expiry = await _context.Setup.Select(s => (int?)s.PasswordExpiryDays).FirstOrDefaultAsync();
			return expiry.GetValueOrDefault(30);
		}

		/// <summary>
		/// Generates a user token and retrieves access information.
		/// </summary>
		private async Task<ServiceResponse<object>> GenerateUserTokenAsync(ApplicationUser user, string appCode)
		{
		
			var daysLeft = await DaysToPasswordExpiryAsync(user);
			if (daysLeft < 0) daysLeft = 0;

			var success  = $"Successful! {daysLeft} day(s) remaining to password expiry.";


			var tokenResult = await _token.CreateToken(user, appCode);
			var userAccessApps = await GetUserAccessApps(user.UserCode);

			var mustChangePassword = await IsPasswordExpiredAsync(user);
			if (appCode == Apps.OtogasApp)
			{
				// Retrieve dispenser nozzle details if applicable.
				var dispenserNozzles = await GetDispenserNozzlesAsync(user.UserCode);
				if (dispenserNozzles.ResponseObject is not null)
				{
					var tillNumber = await CheckTillNumber(dispenserNozzles.ResponseObject.DispenserCode);
					if (tillNumber.ResponseCode != Response.Success)
						return tillNumber;

					var shiftNumber = await GetShiftNumber(user.UserCode);
					var priceList = await GetPriceList(dispenserNozzles.ResponseObject.StationCode);

					_logger.LogInformation("User {userCode} signed in", user.UserCode);

					return ServiceResponse<object>.Success($"{success}", new UserDetailsDispensers
					{
						IsTochangePassword = mustChangePassword,
						Name = string.Join(' ', user.FirstName, user.LastName),
						UserCode = user.UserCode,
						Token = tokenResult.Tooken, // renamed for consistency
						Roles = tokenResult.Roles,
						AccessApps = userAccessApps,
						NozzleDetails = new NozzleDetails
						{
							DispenserCode = dispenserNozzles.ResponseObject.DispenserCode,
							StationCode = dispenserNozzles.ResponseObject.StationCode,
							DispenserName = dispenserNozzles.ResponseObject.DispenserName,
							StationName = dispenserNozzles.ResponseObject.StationName,
							Nozzles = dispenserNozzles.ResponseObject.Nozzles,
							TillNumber = dispenserNozzles.ResponseObject.TillNumber,
							StoreNumber = dispenserNozzles.ResponseObject.StoreNumber
						}
					});
				}
			}

		

			return ServiceResponse<object>.Success($"{success}", new UserDetailsDispensers
			{
				IsTochangePassword = mustChangePassword,
				UserCode = user.UserCode,
				Name = string.Join(' ', user.FirstName, user.LastName),
				Token = tokenResult.Tooken,
				Roles = tokenResult.Roles,
				AccessApps = userAccessApps,
				NozzleDetails = new NozzleDetails
				{
					DispenserName = string.Empty,
					StationName = string.Empty,
					Nozzles = []
				}
			});
		}

		private async Task<int> DaysToPasswordExpiryAsync(ApplicationUser user)
		{
			var expiryDays = await GetPasswordExpiryDaysAsync();
			if (user.PasswordLastUpdated == null) return 0;

			var daysRemaining = (user.PasswordLastUpdated.Value.AddDays(expiryDays) - DateTime.UtcNow).Days;

			return daysRemaining;
		}


		/// <summary>
		/// Retrieves the user's accessible apps based on their user code.
		/// </summary>
		private async Task<List<AccessTypes>> GetUserAccessApps(string userCode)
		{
			return await (from apps in _context.ProtoApps
						  join userApps in _context.UserApps on apps.AppsCode equals userApps.AppsCode
						  where userApps.UserCode == userCode
						  select new AccessTypes
						  {
							  AccessApp = apps.AppsName,
							  AppsCode = apps.AppsCode
						  }).ToListAsync();
		}

		/// <summary>
		/// Retrieves dispenser nozzles assigned to the user.
		/// </summary>
		private async Task<ServiceResponse<NozzleDetails>> GetDispenserNozzlesAsync(string userCode)
		{
			// Get user dispenser code (even if inactive assignment)
			var userDispenser = await (from d in _context.DispenserAssignments
									   where d.AttedantUserCode == userCode
									   select d.DispenserCode).FirstOrDefaultAsync();

			// Pre-check: Dispenser + Till exists
			var dispenser2 = await (from d in _context.Dispensers
									where d.DispenserCode == userDispenser
									select new { d.DispenserCode, d.DispenserName, d.TillNumber })
								  .FirstOrDefaultAsync();

			if (dispenser2 == null)
				return ServiceResponse<NozzleDetails>.Information("No data found", null);

			if (string.IsNullOrWhiteSpace(dispenser2.TillNumber))
				return ServiceResponse<NozzleDetails>.Information("Till number not found", null);

			// Active assignment + station + till details
			var dispenser = await (from d in _context.DispenserAssignments
								   join ds in _context.Dispensers on d.DispenserCode equals ds.DispenserCode
								   join st in _context.Stations on ds.StationCode equals st.StationCode
								   join t in _context.Tills on ds.TillNumber equals t.TillNumber
								   where d.AttedantUserCode == userCode && d.IsActive
								   select new { d.DispenserCode, ds.DispenserName, st.StationName, ds.TillNumber, st.StationCode, t.StoreNumber })
								  .FirstOrDefaultAsync();

			if (dispenser == null)
				return ServiceResponse<NozzleDetails>.Information("No data found", new NozzleDetails());

			var nozzles = await _context.Nozzles
				.Where(n => n.DispenserCode == dispenser.DispenserCode)
				.Select(n => new Nozzles { NozzleCode = n.NozzleCode, NozzleName = n.NozzleName })
				.ToListAsync();

			return ServiceResponse<NozzleDetails>.Success("Dispenser nozzles retrieved", new NozzleDetails
			{
				DispenserName = dispenser.DispenserName,
				StationName = dispenser.StationName,
				TillNumber = dispenser.TillNumber,
				StoreNumber = dispenser.StoreNumber,
				Nozzles = nozzles,
				DispenserCode = dispenser.DispenserCode,
				StationCode = dispenser.StationCode,
			});
		}

		/// <summary>
		/// Handles invalid username scenarios.
		/// </summary>
		private static ServiceResponse<object> HandleInvalidUserName(string userName)
		{
			return ServiceResponse<object>.Information($"Invalid username {userName}", null);
		}

		/// <summary>
		/// Resets the current user's password (authenticated user action).
		/// </summary>
		public async Task<ServiceResponse<object>> ResetPasswordAsync(string newPassword, string confirmPassword)
		{
			try
			{
				// Validate input parameters.
				if (string.IsNullOrWhiteSpace(newPassword))
					return ServiceResponse<object>.Information("Password cannot be empty", null);

				if (string.IsNullOrWhiteSpace(confirmPassword))
					return ServiceResponse<object>.Information("Confirm password cannot be empty", null);

				if (!string.Equals(newPassword, confirmPassword))
					return ServiceResponse<object>.Information("Passwords do not match", null);

				// Retrieve the current user based on user code.
				var user = await _context.Users.FirstOrDefaultAsync(x => x.UserCode == _authentication.Usercode());
				if (user == null)
					return ServiceResponse<object>.Information("User not found", null);

				if (!user.IsActive)
					return ServiceResponse<object>.Information("Your account is not active. Contact admin", null);

				// Check password reuse against history
				if (await IsPasswordReusedAsync(user, newPassword))
					return ServiceResponse<object>.Information("You have used this password before. Please choose a different password.", null);

				// Remove and set new password (forces rehash)
				var removePasswordResult = await _userManager.RemovePasswordAsync(user);
				if (!removePasswordResult.Succeeded)
					return ServiceResponse<object>.Information("Password reset failed", null);

				var addPasswordResult = await _userManager.AddPasswordAsync(user, newPassword);
				if (!addPasswordResult.Succeeded)
					return ServiceResponse<object>.Information("Password reset failed", null);

				// Update security stamp to invalidate old tokens.
				await _userManager.UpdateSecurityStampAsync(user);

				user.AccessFailedCount = 0;
				user.PasswordLastUpdated = DateTime.UtcNow;

				_context.PasswordHistory.Add(new PasswordHistory
				{
					UserCode = user.UserCode,
					DateCreated = DateTime.UtcNow,
					PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword),
				});

				_context.Users.Update(user);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Password reset successful", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error resetting password for user {userCode}", _authentication.Usercode());
				return ServiceResponse<object>.Error("An error occurred while resetting the password", ex);
			}
		}

		/// <summary>
		/// Sends an OTP to the provided phone number.
		/// </summary>
		public async Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber)
		{
			try
			{
				phoneNumber = _messagingService.NormalizePhoneNumber(phoneNumber);
				if (!_messagingService.IsValidPhoneNumber(phoneNumber))
					return ServiceResponse<object>.Information("Invalid phone number", null);

				// Optional: throttle protection (e.g., last OTP within X minutes) — implement if desired

				var otp = _messagingService.GetOtp();

				// Save OTP as active
				var otpResponse = await _messagingService.SaveOtpAsync(phoneNumber, otp);
				if (otpResponse.ResponseCode != Response.Success)
					return ServiceResponse<object>.Information("OTP not saved", otpResponse.ResponseObject);

				// Send OTP via SMS.
				var sent = await _messagingService.SendSmsAsync(phoneNumber, $"Your OTP is {otp}");
				return sent
					? ServiceResponse<object>.Success("OTP sent successfully", otp)
					: ServiceResponse<object>.Information("OTP not sent", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error sending OTP to phone number {phoneNumber}", phoneNumber);
				return ServiceResponse<object>.Error("An error occurred while sending OTP", ex);
			}
		}

		/// <summary>
		/// Changes the user's password (authenticated user action).
		/// </summary>
		public async Task<ServiceResponse<object>> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => u.UserCode == _authentication.Usercode());
				if (user == null)
					return ServiceResponse<object>.Information("User not found", null);

				if (!user.IsActive)
					return ServiceResponse<object>.Information($"Hi {user.FirstName}, your account is inactive", "");

				if (!await _userManager.CheckPasswordAsync(user, oldPassword))
					return ServiceResponse<object>.Information("Invalid old password", null);

				if (!string.Equals(newPassword, confirmPassword))
					return ServiceResponse<object>.Information("Passwords do not match", null);

				if (await IsPasswordReusedAsync(user, newPassword))
					return ServiceResponse<object>.Information("You have used this password before. Please choose a different password.", null);

				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

				if (result.Succeeded)
				{
					user.AccessFailedCount = 0;
					user.PasswordLastUpdated = DateTime.UtcNow;

					await _userManager.UpdateSecurityStampAsync(user);

					await _context.PasswordHistory.AddAsync(new PasswordHistory
					{
						UserCode = user.UserCode,
						DateCreated = DateTime.UtcNow,
						PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword),
					});

					_context.Users.Update(user);
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("Password changed successfully", null);
				}

				return ServiceResponse<object>.Information("Password change failed", result.Errors);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error changing password for user {userCode}", _authentication.Usercode());
				return ServiceResponse<object>.Error("An error occurred while changing password", ex);
			}
		}

		/// <summary>
		/// Resets the user's password using OTP validation.
		/// </summary>
		public async Task<ServiceResponse<object>> ForgotPassword(ResetPasswordModel reset)
		{
			try
			{
				reset.PhoneNumber = _messagingService.NormalizePhoneNumber(reset.PhoneNumber);
				if (!_messagingService.IsValidPhoneNumber(reset.PhoneNumber))
					return ServiceResponse<object>.Information("Invalid phone number", null);

				// Fetch an active OTP and consume it to prevent replay
				var otpEntity = await _context.Otps
					.Where(o => o.OTPCode == reset.OTP && o.PhoneNumber == reset.PhoneNumber && o.OTPStatus == true)
					.OrderByDescending(o => o.DateCreated) // if you have timestamp
					.FirstOrDefaultAsync();

				if (otpEntity == null)
					return ServiceResponse<object>.Information("Invalid OTP", null);

				// Optional: enforce OTP expiry window here if your schema has it

				if (!string.Equals(reset.NewPassword, reset.ConfirmPassword))
					return ServiceResponse<object>.Information("Passwords do not match", null);

				var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == reset.PhoneNumber);
				if (user == null)
					return ServiceResponse<object>.Information("Phone number does not exist", null);

				if (await IsPasswordReusedAsync(user, reset.NewPassword))
					return ServiceResponse<object>.Information("You have used this password before. Please choose a different password.", null);

				// Consume OTP before applying password change to prevent replay
				otpEntity.OTPStatus = false;
				_context.Otps.Update(otpEntity);

				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, reset.NewPassword);

				if (result.Succeeded)
				{
					user.AccessFailedCount = 0;
					user.PasswordLastUpdated = DateTime.UtcNow;

					await _userManager.UpdateSecurityStampAsync(user);

					_context.PasswordHistory.Add(new PasswordHistory
					{
						UserCode = user.UserCode,
						DateCreated = DateTime.UtcNow,
						PasswordHash = _userManager.PasswordHasher.HashPassword(user, reset.NewPassword),
					});

					_context.Users.Update(user);
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("Password reset successfully", null);
				}

				// If failed, still persist OTP consumption to thwart brute-force reuse
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Information("Password reset failed", result.Errors);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error resetting password for phone number {phoneNumber}", reset.PhoneNumber);
				return ServiceResponse<object>.Error("An error occurred while resetting password", ex);
			}
		}

		/// <summary>
		/// Checks if the IMEI belongs to the correct device for the user.
		/// </summary>
		private async Task<bool> IsCorrectDevice(string imei, string userCode)
		{
			if (string.IsNullOrWhiteSpace(imei)) return false;

			// Retrieve the dispenser code assigned to the user.
			var dispenserCode = await (from d in _context.DispenserAssignments
									   where d.AttedantUserCode.Equals(userCode)
									   select d.DispenserCode).FirstOrDefaultAsync();

			if (string.IsNullOrWhiteSpace(dispenserCode)) return false;

			// Check if the IMEI belongs to the correct dispenser.
			var device = await (from p in _context.PdaDevices
								where p.DispenserCode.Equals(dispenserCode) && p.DeviceIMEI.Equals(imei)
								select p).FirstOrDefaultAsync();

			return device != null;
		}

		/// <summary>
		/// Check if this dispenser has an open shift owned by a different user.
		/// </summary>
		private async Task<ServiceResponse<object>> CheckOpenShift(string userCode)
		{
			// Fetch the dispenser assigned to the user
			var thisUserDispenser = await _context.DispenserAssignments
				.Where(d => d.AttedantUserCode == userCode)
				.Select(d => d.DispenserCode)
				.FirstOrDefaultAsync();

			if (thisUserDispenser == null)
				return ServiceResponse<object>.Information("User has no dispenser assigned", null);

			// Another user with open shift on same dispenser?
			var hasOpenShift = await _context.Shifts
				.AnyAsync(s => s.UserCode != userCode &&
							   s.ShiftStatus == ShiftStatus.Open &&
							   s.DispenserCode == thisUserDispenser);

			if (hasOpenShift)
				return ServiceResponse<object>.Information("This dispenser has an open shift", null);

			return ServiceResponse<object>.Success("User has no open shift", null);
		}

		private async Task<string> GetShiftNumber(string userCode)
		{
			var shiftNumber = await (from s in _context.Shifts
									 where s.UserCode == userCode && s.ShiftStatus == ShiftStatus.Open
									 select s.ShiftNumber).FirstOrDefaultAsync();
			return shiftNumber ?? string.Empty;
		}

		//get the price List
		public async Task<Dictionary<string, decimal>> GetPriceList(string stationCode)
		{
			var price = new Dictionary<string, decimal>();
			var userCode = _authentication.Usercode();
			var priceList = await (from p in _context.Prices
								   where p.UserCode == userCode && stationCode == p.StationCode
								   select new
								   {
									   Price = p.Amount,
									   p.StationCode,
									   p.ProductCode
								   }).ToListAsync();

			foreach (var p in priceList)
				price[p.ProductCode] = p.Price;

			return price;
		}

		//check if the dispenser is assigned a tillNumber
		public async Task<ServiceResponse<object>> CheckTillNumber(string dispenserCode)
		{
			var tillNumber = await (from d in _context.Dispensers
									where d.DispenserCode == dispenserCode
									select d.TillNumber).FirstOrDefaultAsync();

			if (string.IsNullOrWhiteSpace(tillNumber))
				return ServiceResponse<object>.Information("No till number assigned to the dispenser", null);

			return ServiceResponse<object>.Success("Till number assigned to the dispenser", tillNumber);
		}

		/// <summary>
		/// Checks new password against user's password history using secure verification.
		/// </summary>
		private async Task<bool> IsPasswordReusedAsync(ApplicationUser user, string newPassword)
		{
			// Adjust window size as your policy requires (e.g., last 20 passwords)
			const int historyWindow = 20;

			var history = await _context.PasswordHistory
				.Where(h => h.UserCode == user.UserCode)
				.OrderByDescending(h => h.DateCreated)
				.Take(historyWindow)
				.ToListAsync();

			foreach (var h in history)
			{
				var result = _userManager.PasswordHasher.VerifyHashedPassword(user, h.PasswordHash, newPassword);
				if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
					return true;
			}
			return false;
		}
	}
	public class ResetPasswordModel
	{
		[Required]
		[RegularExpression(@"^0(?:7|1)\d{8}$", ErrorMessage = "Must start with 07 or 01 and be 10 digits.")]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required]
		public string OTP { get; set; } = string.Empty;
		[Required]
		[StringLength(100, MinimumLength = 8)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
		public string NewPassword { get; set; } = string.Empty;

		[Required]
		[Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
