using AfricasTalkingCS;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using BussinessLogic.Setup;

namespace BusinessLogic.EmailService
{
	public class MessagingService : IMessagingService
	{
		private readonly IConfiguration _configuration;
		private readonly OTOContext _context;
		private readonly ILogger<MessagingService> _logger;
		private readonly ICommonSetups _setups;
		public MessagingService(IConfiguration configuration, OTOContext context, ILogger<MessagingService> logger, ICommonSetups setups)
		{
			_configuration = configuration;
			_context = context;
			_logger = logger;
			_setups = setups;

		}
		public async Task<bool> SendSms(string recepient, string otp)
		{
			try
			{
				recepient = NormalizePhoneNumber(recepient);
				var username = _configuration["AfricasTalking:Username"];
				var apiKey = _configuration["AfricasTalking:ApiKey"];
				var SMSSenderId = _configuration["AfricasTalking:SMSSenderId"];
				var gateway = new AfricasTalkingGateway(username, apiKey);
				var sms = gateway.SendMessage(recepient, otp);
				await SaveOtpAsync(recepient, otp);

				foreach (var res in sms["SMSMessageData"]["Recipients"])
				{
					Console.WriteLine((string)res["number"] + ": ");
					Console.WriteLine((string)res["status"] + ": ");
					Console.WriteLine((string)res["messageId"] + ": ");
					Console.WriteLine((string)res["cost"] + ": ");
				}
				return true;
			}
			catch (AfricasTalkingGatewayException)
			{
				return false;
			}
		}

		public async Task<bool> SendSmsAsync(string recipient, string otp)
		{
			try
			{
				// Normalize phone number
				recipient = NormalizePhoneNumber(recipient);

				// Retrieve Africa's Talking API credentials from configuration
				var username = _configuration["AfricasTalking:Username"];
				var apiKey = _configuration["AfricasTalking:ApiKey"];
				var smsSenderId = _configuration["AfricasTalking:SMSSenderId"];

				// Initialize Africa's Talking gateway
				var gateway = new AfricasTalkingGateway(username, apiKey);

				// Send the SMS
				var smsResponse = gateway.SendMessage(recipient, otp, smsSenderId);

				// Save OTP for tracking
				await SaveOtpAsync(recipient, otp);

				// Process response and log message details
				foreach (var res in smsResponse["SMSMessageData"]["Recipients"])
				{
					var logEntry = new BulkMessageLog
					{
						RecipientNumber = res["number"]?.ToString() ?? "",
						Status = res["status"]?.ToString() ?? "",
						MessageId = res["messageId"]?.ToString() ?? "",
						Cost = res["cost"]?.ToString() ?? "",
						Timestamp = DateTime.UtcNow,
						DeliveryStatus = "Sent",
						Message = otp,
						Sender = smsSenderId ?? "",
						StatusCode = res["statusCode"]?.ToString() ?? "",
						BatchNumber = _setups.GenerateSaleId(),
						FailureReason = "",
					};

					// Save the log (implement SaveBulkMessageLogAsync in your repository or service)
					_context.Add(logEntry);
					await _context.SaveChangesAsync();
				}

				return true;
			}
			catch (AfricasTalkingGatewayException ex)
			{
				// Log error details (implement a logger or use an existing logging mechanism)
				_logger.LogError(ex, "Failed to send SMS to {Recipient}", recipient);
				return false;
			}
			catch (Exception ex)
			{
				// Handle unexpected errors
				_logger.LogError(ex, "An unexpected error occurred while sending SMS to {Recipient}", recipient);
				return false;
			}
		}


		//send bulk sms using africaistalking gateway
		public bool SendBulkSms(string recepients, string message)
		{
			var username = _configuration["AfricasTalking:Username"];
			var apiKey = _configuration["AfricasTalking:ApiKey"];
			var SMSSenderId = _configuration["AfricasTalking:SMSSenderId"];
			var gateway = new AfricasTalkingGateway(username, apiKey);
			try
			{
				// Send the SMS
				var response = gateway.SendMessage(recepients, message, from: SMSSenderId);
				Console.WriteLine("Response: " + response);
				return true;
			}
			catch (AfricasTalkingGatewayException ex)
			{
				Console.WriteLine("Encountered an error: " + ex.Message);
				return false;
			}
		}
		public string GetOtp()
		{
			Random random = new();
			return random.Next(100000, 999999).ToString();
		}
		public string NormalizePhoneNumber(string phoneNumber)
		{
			if (!IsValidPhoneNumber(phoneNumber))
			{
				throw new ArgumentException("Invalid phone number format.");
			}

			if (phoneNumber.StartsWith('+'))
			{
				return phoneNumber; // If it already starts with '+', return as is
			}
			string localNumber = phoneNumber.StartsWith("254") ? phoneNumber[3..] : phoneNumber.StartsWith('0') ? phoneNumber[1..] : phoneNumber;

			return "+254" + localNumber;
		}
		public bool IsValidPhoneNumber(string phoneNumber)
		{
			if (phoneNumber.Length.Equals(10) && phoneNumber.StartsWith('0'))
				return true;
			else if (phoneNumber.Length.Equals(12) && phoneNumber.StartsWith("254"))
				return true;
			else if (phoneNumber.Length.Equals(13) && phoneNumber.StartsWith("+254"))
				return true;
			else
				return false;
		}
		public async Task<ServiceResponse<bool>> SaveOtpAsync(string phoneNumber, string otp)
		{
			try
			{
				var otps = new Otps
				{
					OTPType = 1,
					UserCode = string.Empty,
					ExpiryDate = DateTime.UtcNow,
					OTPCode = otp,
					OTPStatus = true,
					PhoneNumber = phoneNumber,

				};
				await _context.AddAsync(otps);
				await _context.SaveChangesAsync();
				return ServiceResponse<bool>.Success("OTP saved successfully", true);
			}
			catch (Exception)
			{
				return ServiceResponse<bool>.Error("An error occurred while saving OTP", false);
			}
		}
		public async Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber)
		{
			try
			{
				phoneNumber = NormalizePhoneNumber(phoneNumber);
				if (!IsValidPhoneNumber(phoneNumber))
					return ServiceResponse<object>.Information("Invalid phone number format", null);

				var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
				if (user is not null)
				{
					Random random = new();
					var otp = random.Next(10000, 99999);
					var sendotp = await SendSms(phoneNumber, "Tour OTP Is :" + otp.ToString());
					if (sendotp)
					{
						var otps = new Otps
						{
							PhoneNumber = phoneNumber,
							ExpiryDate = DateTime.UtcNow,
							OTPCode = otp.ToString(),
							OTPType = 1,
							OTPStatus = true,
							UserCode = user.UserCode
						};
						await _context.Otps.AddAsync(otps);
						await _context.SaveChangesAsync();
						return ServiceResponse<object>.Success("OTP Sent Successfully", null);
					}
					else
						return ServiceResponse<object>.Error("An error occurred while sending OTP", null);
				}
				else
					return ServiceResponse<object>.Information("User not found", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while sending OTP", null);
			}
		}
		//upload excel file with one column for phoneNumbers


	}
}
