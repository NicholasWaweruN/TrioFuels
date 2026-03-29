using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Messaging;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace BussinessLogic.Messaging
{
	public class BulkMessaging
	{
		private readonly OTOContext _context;
		private readonly IConfiguration _configuration;
		private readonly Dictionary<string, (string Username, string ApiKey, string From)> _senderCredentials;
		private readonly IAuthCommonTasks _authentications;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emailService;
		private readonly ILogger<BulkMessaging> _logger;
		private const int BatchSize = 10000;

		public BulkMessaging(
			OTOContext context,
			IConfiguration configuration,
			IOptions<AfricaIsTalkingSettings> africaIsTalkingSettings,
			IOptions<ProAfricaIsTalkingSettings> proAfricaIsTalkingSettings,
			IOptions<SafetyAfricasTalking> safetyAfricasTalking,
			IAuthCommonTasks tasks,
			ICommonSetups setups,
			IEmailService emailService,
			ILogger<BulkMessaging> logger)
		{
			_context = context;
			_configuration = configuration;
			_senderCredentials = new Dictionary<string, (string, string, string)>
			{
				{ "Progas", (proAfricaIsTalkingSettings.Value.Username!, proAfricaIsTalkingSettings.Value.ApiKey, proAfricaIsTalkingSettings.Value.SMSSenderId) },
				{ "Fuel Flow", (africaIsTalkingSettings.Value.Username!, africaIsTalkingSettings.Value.ApiKey, africaIsTalkingSettings.Value.SMSSenderId) },
				{ "ProtoSafety", (safetyAfricasTalking.Value.Username!, safetyAfricasTalking.Value.ApiKey, safetyAfricasTalking.Value.SMSSenderId) }
			};

			_authentications = tasks;
			_setups = setups;
			_emailService = emailService;
			_logger = logger;
		}

		public async Task<ServiceResponse<object>> BulkMessages(IFormFile file, string message, string sender)
		{
			if (file == null || string.IsNullOrEmpty(sender) || string.IsNullOrEmpty(message))
			{
				return ServiceResponse<object>.Information("File, sender, or message is empty!", null);
			}

			if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
			{
				return ServiceResponse<object>.Information("Invalid file format. Please upload an Excel file.", null);
			}

			if (!_senderCredentials.TryGetValue(sender, out var credentials))
			{
				return ServiceResponse<object>.Information("Invalid sender!", null);
			}

			var phoneNumberList = await ExtractPhoneNumbersFromFile(file);
			if (phoneNumberList.Count == 0)
			{
				return ServiceResponse<object>.Information("No valid phone numbers found!", null);
			}

			var batchNumber = _setups.GenerateSaleId();
			var totalBatches = (int)Math.Ceiling((double)phoneNumberList.Count / BatchSize);

			using var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("apiKey", credentials.ApiKey);

			for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
			{
				var currentBatch = phoneNumberList.Skip(batchIndex * BatchSize).Take(BatchSize).ToList();
				var recipients = string.Join(",", currentBatch);

				var payload = new FormUrlEncodedContent(
				[
					new KeyValuePair<string, string>("username", credentials.Username),
					new KeyValuePair<string, string>("to", recipients),
					new KeyValuePair<string, string>("message", message),
					new KeyValuePair<string, string>("from", credentials.From)
				]);

				try
				{
					var response = await httpClient.PostAsync("https://api.africastalking.com/version1/messaging", payload);
					var result = await response.Content.ReadAsStringAsync();

					if (response.Content.Headers.ContentType?.MediaType != "text/xml")
					{
						_logger.LogError($"Unexpected response format: {result}");
						return ServiceResponse<object>.Error("Unexpected response format from the server.", null);
					}

					var bulkMessageLogs = ParseXmlResponse(result, sender, message, batchNumber);
					if (bulkMessageLogs.Count > 0)
					{
						await SaveBulkMessageLogsAsync(bulkMessageLogs);
						SendEmailNotificationAsync(sender, batchNumber, bulkMessageLogs.Count, message);
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error sending bulk messages");
					return ServiceResponse<object>.Error("An error occurred while sending bulk messages.", null);
				}

				await Task.Delay(1000); // Throttle requests if necessary
			}

			return ServiceResponse<object>.Success($"{sender} Bulk messages sent successfully", null);
		}

		private async Task<List<string>> ExtractPhoneNumbersFromFile(IFormFile file)
		{
			using var stream = new MemoryStream();
			await file.CopyToAsync(stream);
			using var workbook = new XLWorkbook(stream);
			var worksheet = workbook.Worksheet(1);
			var row = worksheet.LastRowUsed();

			if (row == null)
			{
				return [];
			}

			var phoneNumbers = worksheet.CellsUsed()
				.Select(x => x.Value.ToString())
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Select(NormalizePhoneNumber)
				.Where(x => x != null)
				.Distinct()
				.ToList();

			return phoneNumbers!;
		}

		private string? NormalizePhoneNumber(string phoneNumber)
		{
			var regex = new Regex(@"^(?:0|254|\+254)?(\d{9})$");
			var match = regex.Match(phoneNumber);
			return match.Success ? $"+254{match.Groups[1].Value}" : null;
		}

		private List<BulkMessageLog> ParseXmlResponse(string xmlResponse, string sender, string message, string batchNumber)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlResponse);

			var recipientsNode = xmlDoc.SelectNodes("//Recipient") ?? throw new Exception("Invalid XML structure.");

			return recipientsNode
				.Cast<XmlNode>()
				.Where(recipientNode =>
					recipientNode["number"] != null &&
					recipientNode["cost"] != null &&
					recipientNode["status"] != null &&
					recipientNode["statusCode"] != null &&
					recipientNode["messageId"] != null)
				.Select(recipientNode => new BulkMessageLog
				{
					Sender = sender,
					Message = message,
					RecipientNumber = recipientNode["number"]?.InnerText ?? string.Empty,
					Status = recipientNode["status"]?.InnerText ?? string.Empty,
					StatusCode = recipientNode["statusCode"]?.InnerText ?? string.Empty,
					Cost = recipientNode["cost"]?.InnerText ?? string.Empty,
					MessageId = recipientNode["messageId"]?.InnerText ?? string.Empty,
					BatchNumber = batchNumber,
					Timestamp = DateTime.UtcNow,
					DeliveryStatus = "Sent"
				})
				.ToList();
		}

		private async Task SaveBulkMessageLogsAsync(List<BulkMessageLog> logs)
		{
			_context.BulkMessageLogs.AddRange(logs);
			await _context.SaveChangesAsync();
		}

		private void SendEmailNotificationAsync(string sender, string batchNumber, int recipientCount, string message)
		{
			var userName = _authentications.Name();
			var email = _context.Users
				.Where(x => x.UserCode == _authentications.Usercode())
				.Select(x => x.Email)
				.FirstOrDefault();

			if (email != null && email.EndsWith("@protoenergy.com"))
			{
				var xhtml = EmailBody()
					.Replace("{{batchnumber}}", batchNumber)
					.Replace("{{userName}}", userName)
					.Replace("{{recipientName}}", userName)
					.Replace("{{senderName}}", sender)
					.Replace("{{norecipients}}", recipientCount.ToString())
					.Replace("{{message}}", message)
					.Replace("{{startDate}}", DateTime.UtcNow.Date.AddDays(-1).ToString("yyyy-MM-dd"))
					.Replace("{{endDate}}", DateTime.UtcNow.Date.AddDays(1).ToString("yyyy-MM-dd"));

				_emailService.SendEmail(email, null, "Bulk SMS", xhtml);
			}
		}

		private static string EmailBody()
		{
			return """
            <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Batch Number Notification</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }
                    .email-container {
                        max-width: 600px;
                        margin: 20px auto;
                        background: #ffffff;
                        padding: 20px;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }
                    .header {
                        text-align: center;
                        font-size: 24px;
                        font-weight: bold;
                        color: #333;
                        margin-bottom: 20px;
                    }
                    .content {
                        font-size: 16px;
                        color: #555;
                    }
                    .batch-number {
                        font-size: 18px;
                        font-weight: bold;
                        color: #007BFF;
                    }
                    .message-text {
                        font-size: 8px;
                        color: fuchsia;
                        font-weight: bold;
                    }
                    .footer {
                        margin-top: 20px;
                        text-align: center;
                        font-size: 14px;
                        color: #999;
                    }
                    .download-link {
                        color: #007BFF;
                        text-decoration: none;
                        font-weight: bold;
                    }
                    .download-link:hover {
                        text-decoration: underline;
                    }
                </style>
            </head>
            <body>
                <div class="email-container">
                    <div class="header">
                        Batch Number Notification
                    </div>
                    <div class="content">
                        Dear {{recipientName}},<br><br>
                        This is to inform you that you have successfully sent the SMS to {{norecipients}} 
                        recipients under the sender name {{senderName}}.
                        <br><br>
                        The message sent is <span class="message-text">"{{message}}"</span>.
                        <br><br>
                        Please use this <span class="batch-number">{{batchnumber}}</span> batch number to download the delivery report from Otopay.
                        <br><br>
                        You can also download the report by clicking the following link:
                        <br>
                        <a href="https://fuelflow.protoenergy.com/otogas/Messaging/DownloadBulkMessages?sender=Otogas&batchNumber={{batchnumber}}&startDate={{startDate}}&endDate={{endDate}}" class="download-link">Download Messages</a>
                        <br><br>
                        If you have any questions, feel free to contact support.
                    </div>
                    <div class="footer">
                        &copy; 2025 Otopay. All rights reserved.
                    </div>
                </div>
            </body>
            </html>
            """;
		}
	}
}