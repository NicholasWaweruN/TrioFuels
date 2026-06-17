using AfricasTalkingCS;
using BusinessLogic.Authentication.AddUsers;
using BusinessLogic.EmailService;
using BusinessLogic.Messaging;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Authentication;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Authentication.AddUsers
{
	public class RegisterUsers : IRegisterUsers
	{
		private readonly OTOContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICommonSetups _setups;
		private readonly IMessagingService _messagingService;
		private readonly IAfricaIsTalking _isTalking;
		private readonly IEmailService _emailService;
		private readonly ILogger<RegisterUsers> _logger;
		private readonly IAuthCommonTasks _authentication;

		public RegisterUsers(OTOContext context, UserManager<ApplicationUser> userManager, ICommonSetups setups,
			IMessagingService messagingService, IAfricaIsTalking isTalking, IEmailService emailService,
			ILogger<RegisterUsers> logger, IAuthCommonTasks authentication)
		{
			_context = context;
			_userManager = userManager;
			_setups = setups;
			_messagingService = messagingService;
			_isTalking = isTalking;
			_emailService = emailService;
			_logger = logger;
			_authentication = authentication;
		}

		#region Public Methods

		/// <summary>
		/// Registers a new user by validating the input, generating a user code, saving the user to the database, and sending OTP and confirmation emails.
		/// </summary>
		/// <param name="register">The register model containing user details.</param>
		/// <returns>A ServiceResponse indicating success or failure of the operation.</returns>
		public async Task<ServiceResponse<object>> RegisterUserAsync(RegisterModel register)
		{
			try
			{
				// ─────────────────────────────────────────────
				// 1. Normalize input ONCE (performance + consistency)
				// ─────────────────────────────────────────────
				var email = register.Email.Trim().ToLowerInvariant();
				var phone = _messagingService.NormalizePhoneNumber(register.PhoneNumber);
				var payroll = register.PayrollNumber?.Trim().ToUpperInvariant();

				register.Email = email;
				register.PhoneNumber = phone;
				register.PayrollNumber = payroll!;

				// ─────────────────────────────────────────────
				// 2. Fast indexed validations (NO OR queries)
				// ─────────────────────────────────────────────

				if (await _context.Users.AnyAsync(x => x.Email == email))
					return ServiceResponse<object>.Information("Email already exists", null);

				if (await _context.Users.AnyAsync(x => x.PhoneNumber == phone))
					return ServiceResponse<object>.Information("Phone number already exists", null);

				if (!string.IsNullOrWhiteSpace(payroll) &&
					await _context.Users.AnyAsync(x => x.PayrollNumber == payroll))
					return ServiceResponse<object>.Information("Payroll number already exists", null);

				// ─────────────────────────────────────────────
				// 3. Generate user code (external service)
				// ─────────────────────────────────────────────
				var userCode = await _setups.GetCodeGenerator("UserCode");

				if (string.IsNullOrEmpty(userCode))
					return ServiceResponse<object>.Error("Failed to generate user code", null);

				// ─────────────────────────────────────────────
				// 4. Generate secure OTP (NO hardcoded values)
				// ─────────────────────────────────────────────
				var otp = Random.Shared.Next(100000, 999999).ToString();

				// ─────────────────────────────────────────────
				// 5. Build user entity (clean + consistent)
				// ─────────────────────────────────────────────
				var user = new ApplicationUser
				{
					Id = Guid.NewGuid().ToString(),
					UserCode = userCode,
					FirstName = _setups.SentenceCase(register.FirstName),
					LastName = _setups.SentenceCase(register.LastName),
					MiddName = _setups.SentenceCase(register.MiddName),
					Email = email,
					UserName = email,
					NormalizedEmail = email.ToUpperInvariant(),
					NormalizedUserName = email.ToUpperInvariant(),
					PhoneNumber = phone,
					PayrollNumber = payroll!,
					AccessApps = string.Join(",", register.AccessApps ?? []),
					DateCreated = DateTime.UtcNow,
					DateModified = DateTime.UtcNow,
					IsActive = true,
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					UserType = 1,
					SecurityStamp = Guid.NewGuid().ToString(),
					ConcurrencyStamp = Guid.NewGuid().ToString(),
					AccessFailedCount = 0,
					
				};


				var listApps = register.AccessApps;
				var userApps = new List<UserApps>();

				foreach (var item in listApps!)
				{
					var code = "0" + item;
					userApps.Add(new UserApps
					{
						AppsCode = code,
						DateCreated = DateTime.Now,
						UserCode = userCode
					});
				}
				

				// ─────────────────────────────────────────────
				// 6. Create user (Identity handles transaction internally)
				// ─────────────────────────────────────────────
				var result = await _userManager.CreateAsync(user);

				if (!result.Succeeded)
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));

					_logger.LogError("User creation failed for {Email}, errors: {Errors}",email,errors);

					return ServiceResponse<object>.Error("User creation failed", errors);
				}

				// ─────────────────────────────────────────────
				// 7. Save OTP + Audit in ONE DB CALL
				// ─────────────────────────────────────────────
			

				var audit = new UserTrail
				{
					UserCode = userCode,
					ActionType = "UserRegistration",
					Message = $"User {email} created by {_authentication.Name()}",
					UserName = _authentication.Name(),
					DateCreated = DateTime.UtcNow
				};

				_context.AddRange(audit, userApps);
				await _context.SaveChangesAsync();

				// ─────────────────────────────────────────────
				// 8. Email (non-blocking)
				// ─────────────────────────────────────────────
				var body = BuildEmailBody(
					$"{register.FirstName} {register.LastName}",
					otp);

				await _emailService.SendEmail(email, null, "Otopay Account", body);

				return ServiceResponse<object>.Success("User created successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating user {Email}", register.Email);
				return ServiceResponse<object>.Error("Unexpected error occurred", null);
			}
		}
		 string BuildEmailBody(string userName, string otp)
		{
			var baseUrl = _setups.GetHostUrl();
			string loginUrl = baseUrl + "/login";

			return """
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <title>Welcome to Otopay!</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }
                .container {
                    width: 100%;
                    max-width: 600px;
                    margin: 20px auto;
                    background: #ffffff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
                .header {
                    text-align: center;
                    padding: 10px 0;
                }
                .header h1 {
                    color: #333;
                }
                .content {
                    padding: 20px;
                    text-align: center;
                }
                .content p {
                    font-size: 16px;
                    color: #555;
                    line-height: 1.5;
                }
                .footer {
                    text-align: center;
                    padding: 10px;
                    font-size: 14px;
                    color: #777;
                }
                .button {
                    display: inline-block;
                    padding: 10px 20px;
                    margin-top: 15px;
                    font-size: 16px;
                    color: #ffffff;
                    background: #007bff;
                    text-decoration: none;
                    border-radius: 5px;
                }
                .button:hover {
                    background: #0056b3;
                }
            </style>
        </head>
        <body>
            <div class="container">
                <div class="header">
                    <h1>Welcome to Otopay!</h1>
                </div>
                <div class="content">
                    <p>Dear <strong>{{UserName}}</strong>,</p>
                    <p>Your fuel flow account has been created successfully. You can now enjoy seamless transactions and exclusive benefits.</p>
                    <p>Go to the forgot password button on the login screen and change your password there.</p>
                    <p>Your One Time Password is <strong>{{Otp}}</strong></p>
                    <p>To get started, click the button below:</p>
                    <a href="{{LoginUrl}}" class="button">Login to Your Account</a>
                </div>
                <div class="footer">
                    <p>&copy; 2025 Otopay. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
        """
				.Replace("{{UserName}}", userName)
				.Replace("{{Otp}}", otp)
				.Replace("{{LoginUrl}}", loginUrl);
		}

		/// <summary>
		/// Confirms a user's phone number using an OTP.
		/// </summary>
		/// <param name="phoneNumber">The phone number to confirm.</param>
		/// <param name="otp">The OTP provided by the user.</param>
		/// <returns>A ServiceResponse indicating the success or failure of the operation.</returns>
		public async Task<ServiceResponse<object>> ConfirmPhoneNumberWithOtpAsync(string phoneNumber, string otp)
		{
			try
			{
				// Normalize and validate phone number
				var normalizedPhoneNumber = _messagingService.NormalizePhoneNumber(phoneNumber);
				if (!_messagingService.IsValidPhoneNumber(normalizedPhoneNumber))
				{
					return ServiceResponse<object>.Information("Invalid phone number", null);
				}

				// Find the user by phone number
				var user = await FindUserByPhoneNumberAsync(normalizedPhoneNumber);
				if (user == null)
				{
					return ServiceResponse<object>.Information("Phone number does not exist", null);
				}

				// Validate the OTP
				var otpResponse = await ValidateOtpAsync(normalizedPhoneNumber, otp);
				return otpResponse
					? ServiceResponse<object>.Success("Phone number confirmed successfully", null)
					: ServiceResponse<object>.Information("Invalid OTP", null);
			}
			catch (Exception ex)
			{
				// Log any errors during phone number confirmation
				_logger.LogError("Error confirming phone number {phoneNumber}: {error}", phoneNumber, ex.Message);
				return ServiceResponse<object>.Error("An error occurred while confirming phone number", null);
			}
		}

		/// <summary>
		/// Deactivates a user's account.
		/// </summary>
		/// <param name="userCode">The user code of the account to deactivate.</param>
		/// <returns>A ServiceResponse indicating success or failure of the operation.</returns>
		public async Task<ServiceResponse<object>> DeactivateUserAsync(string userCode)
		{
			return await ChangeUserActivationStatusAsync(userCode, false, "deactivated");
		}

		/// <summary>
		/// Activates a user's account.
		/// </summary>
		/// <param name="userCode">The user code of the account to activate.</param>
		/// <returns>A ServiceResponse indicating success or failure of the operation.</returns>
		public async Task<ServiceResponse<object>> ActivateUserAsync(string userCode)
		{
			return await ChangeUserActivationStatusAsync(userCode, true, "activated");
		}

		/// <summary>
		/// Retrieves all users from the database.
		/// </summary>
		/// <returns>A ServiceResponse containing the list of users or an error message.</returns>
		public async Task<ServiceResponse<object>> GetAllUsersAsync()
		{
			try
			{
				// Retrieve users from the database
				var users = await _context.Users.Where(u => u.UserType != 2)
					.Select(u => new
					{
						u.FirstName,
						u.MiddName,
						u.LastName,
						u.PayrollNumber,
						u.PhoneNumber,
						u.Email,
						u.UserCode,
						u.IsActive,
					})
					.ToListAsync();

				// Return success or information response based on user count
				return users.Count > 0
					? ServiceResponse<object>.Success("Users retrieved successfully", users)
					: ServiceResponse<object>.Information("No users found", null);
			}
			catch (Exception ex)
			{
				// Log any errors during user retrieval
				_logger.LogError("Error retrieving users: {error}", ex.Message);
				return ServiceResponse<object>.Error("An error occurred while retrieving users", null);
			}
		}

		/// <summary>
		/// Updates the details of an existing user.
		/// </summary>
		/// <param name="userCode">The user code of the user to update.</param>
		/// <param name="updateUser">The update model containing the new user details.</param>
		/// <returns>A ServiceResponse indicating success or failure of the operation.</returns>
		public async Task<ServiceResponse<object>> UpdateUserDetailsAsync(string userCode, UpdateUsers updateUser)
		{
			try
			{
				// Find the user by user code
				var user = await FindUserByUserCodeAsync(userCode);
				if (user == null)
				{
					return ServiceResponse<object>.Information("User not found", null);
				}

				// Track changes for audit trail
				var changes = GetUserChanges(user, updateUser);

				if (!changes.Any())
				{
					return ServiceResponse<object>.Information("No changes detected.", null);
				}

				// Apply the updates
				ApplyUserUpdates(user, updateUser);

				// Save changes
				await _context.SaveChangesAsync();

				// Audit trail entry
				var auditMessage = $@"User account updated by {_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.
				Affected User: {user.FirstName} {user.MiddName} {user.LastName} ({user.UserCode}) Changes:{string.Join(':', Environment.NewLine, changes)}";

				await SaveAuditTrailAsync(user.UserCode, auditMessage);

				return ServiceResponse<object>.Success($"{user.FirstName}'s account updated successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error updating user {userCode}: {error}", userCode, ex.Message);
				return ServiceResponse<object>.Error("An error occurred while updating user", null);
			}
		}

		/// <summary>
		/// Compares existing user with new values and returns the changed fields.
		/// </summary>
		private static List<string> GetUserChanges(ApplicationUser user, UpdateUsers updateUser)
		{
			var changes = new List<string>();

			if (!string.IsNullOrEmpty(updateUser.FirstName) && updateUser.FirstName != user.FirstName)
				changes.Add($"FirstName: '{user.FirstName}' → '{updateUser.FirstName}'");

			if (!string.IsNullOrEmpty(updateUser.MiddName) && updateUser.MiddName != user.MiddName)
				changes.Add($"MiddName: '{user.MiddName}' → '{updateUser.MiddName}'");

			if (!string.IsNullOrEmpty(updateUser.LastName) && updateUser.LastName != user.LastName)
				changes.Add($"LastName: '{user.LastName}' → '{updateUser.LastName}'");

			if (!string.IsNullOrEmpty(updateUser.PhoneNumber) && updateUser.PhoneNumber != user.PhoneNumber)
				changes.Add($"PhoneNumber: '{user.PhoneNumber}' → '{updateUser.PhoneNumber}'");

			if (!string.IsNullOrEmpty(updateUser.Email) && updateUser.Email != user.Email)
				changes.Add($"Email: '{user.Email}' → '{updateUser.Email}'");

			if (!string.IsNullOrEmpty(updateUser.PayrollNumber) && updateUser.PayrollNumber != user.PayrollNumber)
				changes.Add($"PayrollNumber: '{user.PayrollNumber}' → '{updateUser.PayrollNumber}'");

			return changes;
		}

		/// <summary>
		/// Applies updates to the user entity.
		/// </summary>
		private static void ApplyUserUpdates(ApplicationUser user, UpdateUsers updateUser)
		{
			if (!string.IsNullOrEmpty(updateUser.FirstName)) user.FirstName = updateUser.FirstName;
			if (!string.IsNullOrEmpty(updateUser.MiddName)) user.MiddName = updateUser.MiddName;
			if (!string.IsNullOrEmpty(updateUser.LastName)) user.LastName = updateUser.LastName;
			if (!string.IsNullOrEmpty(updateUser.PhoneNumber)) user.PhoneNumber = updateUser.PhoneNumber;
			if (!string.IsNullOrEmpty(updateUser.Email)) user.Email = updateUser.Email;
			if (!string.IsNullOrEmpty(updateUser.PayrollNumber)) user.PayrollNumber = updateUser.PayrollNumber;
		}

		/// <summary>
		/// Saves audit trail entry (can log to DB or external service).
		/// </summary>
		private async Task SaveAuditTrailAsync(string userCode, string message)
		{
			var audit = new UserTrail
			{
				UserCode = userCode,
				ActionType = "UpdateUser",
				Message = message,
				UserName = _authentication.Name(),
				DateCreated = DateTime.UtcNow
			};

			_context.UserTrails.Add(audit);
			await _context.SaveChangesAsync();
		}

		#endregion

		#region Private Helper Methods

		/// <summary>
		/// Validates the input for user registration.
		/// </summary>
		/// <param name="register">The register model containing user details.</param>
		/// <returns>A ServiceResponse indicating whether the input is valid.</returns>
		private async Task<ServiceResponse<bool>> ValidateUserInputAsync(RegisterModel register)
		{
			// Normalize email and phone number
			register.Email = register.Email.ToLower();
			register.PhoneNumber = _messagingService.NormalizePhoneNumber(register.PhoneNumber);

			// Check if a user with the same email, phone number, or payroll number already exists
			var existingUser = await _context.Users
				.FirstOrDefaultAsync(x => x.Email == register.Email || x.PhoneNumber == register.PhoneNumber || x.PayrollNumber == register.PayrollNumber);

			if (existingUser != null)
			{
				if (existingUser.Email == register.Email)
					return ServiceResponse<bool>.Information($"A user with the email {register.Email} already exists", false);
				if (existingUser.PhoneNumber == register.PhoneNumber)
					return ServiceResponse<bool>.Information($"A user with the phone number {register.PhoneNumber} already exists", false);
				if (existingUser.PayrollNumber == register.PayrollNumber)
					return ServiceResponse<bool>.Information($"A user with the payroll number {register.PayrollNumber} already exists", false);
			}

			return ServiceResponse<bool>.Success("User input validated", true);
		}

		/// <summary>
		/// Saves the new user to the database.
		/// </summary>
		/// <param name="register">The register model containing user details.</param>
		/// <param name="otp">The OTP to be used as the password.</param>
		/// <param name="userCode">The generated user code.</param>
		/// <returns>A ServiceResponse indicating success or failure of the save operation.</returns>
		private async Task<ServiceResponse<object>> SaveUserAsync(RegisterModel register, string otp, string userCode)
		{
			try
			{
				var user = new ApplicationUser
				{
					Email = register.Email.ToLower(),
					FirstName = _setups.SentenceCase(register.FirstName),
					LastName = _setups.SentenceCase(register.LastName),
					PhoneNumber = register.PhoneNumber,
					PayrollNumber = register.PayrollNumber.ToUpper(),
					DateCreated = DateTime.UtcNow,
					IsActive = true,
					MiddName = _setups.SentenceCase(register.MiddName),
					UserCode = userCode,
					UserName = register.Email.ToUpper(),
					AccessApps = string.Empty,
					CreatedBy = userCode,
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					LockoutEnabled = true,
					NormalizedEmail = register.Email.ToLower(),
					NormalizedUserName = register.Email.ToLower(),
					SecurityStamp = Guid.NewGuid().ToString().ToLower(),
					ConcurrencyStamp = Guid.NewGuid().ToString().ToLower(),
					Id = Guid.NewGuid().ToString(),
					AccessFailedCount = 0,
					LastLoginDate = null,
					PasswordLastUpdated = null,
					DepartmentCode = string.Empty,
					DateModified = DateTime.UtcNow,
					TwoFactorEnabled = false,
					StationCode = string.Empty,
					UserType = 1
				};

				// Use the provided OTP or default password
				string password = !string.IsNullOrEmpty(otp) ? otp : "M!ngat@30";

				// Use UserManager to create the user
				var result = await _userManager.CreateAsync(user, password);

				if (result.Succeeded)
				{
					//await _messagingService.SendSms(user.PhoneNumber, "Your Otopay Account has been created. Kindly continue to change the password");
					return ServiceResponse<object>.Success($"{user.FirstName} created successfully", null);
				}
				else
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));
					_logger.LogError("User creation failed for {newUser}, error: {errors}", user.FirstName, errors);
					return ServiceResponse<object>.Information($"{user.FirstName} creation failed errors", null);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error saving user {userName}", register.FirstName);
				return ServiceResponse<object>.Error("Something went wrong contact admin", ex.Message);
			}
		}

		/// <summary>
		/// Validates an OTP for a given phone number.
		/// </summary>
		/// <param name="phoneNumber">The phone number associated with the OTP.</param>
		/// <param name="otp">The OTP to validate.</param>
		/// <returns>A boolean indicating whether the OTP is valid.</returns>
		private async Task<bool> ValidateOtpAsync(string phoneNumber, string otp)
		{
			return await _context.Otps
				.AnyAsync(o => o.OTPCode == otp && o.PhoneNumber == phoneNumber && o.OTPStatus);
		}

		/// <summary>
		/// Finds a user by their phone number.
		/// </summary>
		/// <param name="phoneNumber">The phone number to search for.</param>
		/// <returns>The ApplicationUser associated with the phone number, or null if not found.</returns>
		private async Task<ApplicationUser?> FindUserByPhoneNumberAsync(string phoneNumber)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
		}

		/// <summary>
		/// Finds a user by their user code.
		/// </summary>
		/// <param name="userCode">The user code to search for.</param>
		/// <returns>The ApplicationUser associated with the user code, or null if not found.</returns>
		private async Task<ApplicationUser?> FindUserByUserCodeAsync(string userCode)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.UserCode == userCode);
		}

		/// <summary>
		/// Changes the activation status of a user.
		/// </summary>
		/// <param name="userCode">The user code of the user to change status.</param>
		/// <param name="isActive">The new activation status (true for active, false for inactive).</param>
		/// <param name="action">The action performed (activated or deactivated).</param>
		/// <returns>A ServiceResponse indicating success or failure of the operation.</returns>
		private async Task<ServiceResponse<object>> ChangeUserActivationStatusAsync(string userCode, bool isActive, string action)
		{
			try
			{
				// Find user by user code
				var user = await FindUserByUserCodeAsync(userCode);
				if (user == null)
				{
					return ServiceResponse<object>.Information($"No user found with code '{userCode}'. Please verify and try again.", null);
				}

				// Update activation status
				user.IsActive = isActive;
				await _context.SaveChangesAsync();

				var statusText = isActive ? "Active" : "Inactive";
				var message = $@"User '{user.FirstName} {user.LastName}' 
                        has been {action} successfully on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.
                        Current Status: {statusText} by {_authentication.Name()}";

				// Save audit trail
				var userTrail = new UserTrail
				{
					ActionType = "UserActivationStatusChange",
					Message = message,
					UserName = _authentication.Name(),
					UserCode = _authentication.Usercode(),
					DateCreated = DateTime.UtcNow,
				};
				await _context.UserTrails.AddAsync(userTrail);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success(message, null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error changing activation status for user {userCode}", userCode);
				return ServiceResponse<object>.Error(
					$"An unexpected error occurred while trying to {action} user with code '{userCode}'. Please try again later.",
					null
				);
			}
		}

		#endregion
	}


}