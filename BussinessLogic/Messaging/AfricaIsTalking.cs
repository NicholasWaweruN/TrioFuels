using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AfricasTalkingCS;
using BusinessLogic.Messaging;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Messaging;
using DataAccessLayer.EntityModels.Messaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PhoneNumbers;

namespace BussinessLogic.Messaging
{
	public class AfricaIsTalking : IAfricaIsTalking
	{
		private readonly AfricaIsTalkingSettings _africaIsTalkingSettings;
		private readonly OTOContext _context;
		public AfricaIsTalking(IOptions<AfricaIsTalkingSettings> africaIsTalkingSettings, OTOContext context)
		{
			_africaIsTalkingSettings = africaIsTalkingSettings.Value;
			_context = context;
		}
		public async Task<bool> SendSms(string phoneNumber, string message)
		{
			try
			{

				phoneNumber = NormalizePhoneNumber(phoneNumber);
				var username = _africaIsTalkingSettings.Username;
				var apiKey = _africaIsTalkingSettings.ApiKey;
				var from = _africaIsTalkingSettings.SMSSenderId;

				var Sms = new Sms
				{
					DateCreated = DateTime.UtcNow,
					Message = message,
					PhoneNumber = phoneNumber,
					Status = "Sent",
				};

				var gateway = new AfricasTalkingGateway(username, apiKey);
				dynamic result = gateway.SendMessage(phoneNumber, message, from);

				await _context.AddAsync(Sms);
				await _context.SaveChangesAsync();

				return true;
			}

			catch (Exception)
			{
				return false;
			}

		}

		public void SendBulkSms(List<string> recipients, string message)
		{
			// Retrieve credentials from appsettings.json
			var sms = new List<MessageDetails>();
			recipients = NormalizePhoneNumbers(recipients);
			var username = _africaIsTalkingSettings.Username;
			var apiKey = _africaIsTalkingSettings.ApiKey;
			var from = _africaIsTalkingSettings.SMSSenderId;

			// Create a new instance of the AfricasTalkingGateway
			var gateway = new AfricasTalkingGateway(username, apiKey);

			// Convert the list of recipients to a comma-separated string
			string recipientString = string.Join(",", recipients);

			try
			{
				// Send the SMS
				dynamic result = gateway.SendMessage(recipientString, message, from, -1);

			}
			catch (AfricasTalkingGatewayException ex)
			{
				// Handle the error
				Console.WriteLine("Error: " + ex.Message);
			}
		}
		private string NormalizePhoneNumber(string phoneNumber)
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
		private bool IsValidPhoneNumber(string phoneNumber)
		{
			if (phoneNumber.Length.Equals(10) && phoneNumber.StartsWith('0'))
			{
				return true;
			}
			else if (phoneNumber.Length.Equals(12) && phoneNumber.StartsWith("254"))
			{
				return true;
			}
			else if (phoneNumber.Length.Equals(13) && phoneNumber.StartsWith("+254"))
			{
				return true;
			}
			else
				return false;
		}
		private List<string> NormalizePhoneNumbers(IEnumerable<string> phoneNumbers)
		{
			var normalizedNumbers = new List<string>();

			foreach (var phoneNumber in phoneNumbers)
			{
				if (IsValidPhoneNumber(phoneNumber))
				{
					normalizedNumbers.Add(NormalizePhoneNumber(phoneNumber));
				}
				else
				{
					Console.WriteLine($"Invalid phone number: {phoneNumber}");
				}
			}

			return normalizedNumbers;
		}

		//Receive call backs
		public void ReceiveCallBacks(SmsCallbackRequest callback)
		{
			var callbackDetails = new AfricasTalkingCallback
			{
				PhoneNumber = callback.PhoneNumber,
				MessageId = callback.MessageId,
				Status = callback.Status,
				NetworkCode = callback.NetworkCode,
				FailureReason = callback.FailureReason,
				Cost = callback.Cost,
				DateCreated = DateTime.UtcNow,

			};
			_context.Add(callbackDetails);
			_context.SaveChanges();
		}

		// upload contacts batch validate the length of the phone number and normalize the phone number
		public List<string> UploadContacts(List<string> phoneNumbers)
		{
			var normalizedNumbers = new List<string>();
			foreach (var phoneNumber in phoneNumbers)
			{
				if (IsValidPhoneNumber(phoneNumber))
				{
					normalizedNumbers.Add(NormalizePhoneNumber(phoneNumber));
				}
				else
				{
					Console.WriteLine($"Invalid phone number: {phoneNumber}");
				}
			}
			return normalizedNumbers;
		}

	}

}


