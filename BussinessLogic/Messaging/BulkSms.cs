using AfricasTalkingCS;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Messaging;
using DataAccessLayer.EntityModels.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace BussinessLogic.Messaging
{
	public class BulkSms
	{
		private readonly OTOContext _context;
		private readonly IConfiguration _configuration;
		private readonly AfricaIsTalkingSettings _africaIsTalkingSettings;
		private readonly SafetyAfricasTalking _safetyAfricasTalking;
		private readonly ProAfricaIsTalkingSettings _proAfricaIsTalkingSettings;
		private readonly IAuthCommonTasks _authentications;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emailService;
		public BulkSms(OTOContext context, IConfiguration configuration, IOptions<AfricaIsTalkingSettings> africaIsTalkingSettings, IOptions<ProAfricaIsTalkingSettings> proAfricaIsTalkingSettings, IAuthCommonTasks tasks,ICommonSetups setups, IEmailService emailService, IOptions<SafetyAfricasTalking> safetyAfricasTalking)
		{
			_context = context;
			_configuration = configuration;
			_africaIsTalkingSettings = africaIsTalkingSettings.Value;
			_proAfricaIsTalkingSettings = proAfricaIsTalkingSettings.Value;
			_authentications = tasks;
			_setups = setups;
			_emailService = emailService;
			_safetyAfricasTalking = safetyAfricasTalking.Value;
		}

		public async Task FetchMessagesAsync(string username, string apiKey, string lastReceivedId = "0")
		{
			var client = new HttpClient();

			// Africa's Talking API URL
			var url = $"https://api.africastalking.com/version1/messaging?username={username}&lastReceivedId={lastReceivedId}";

			// Set request headers
			client.DefaultRequestHeaders.Add("apikey", apiKey);

			try
			{
				// Send GET request
				var response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode(); // Throws if the status code is not 2xx

				// Read response as JSON
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var messageData = JsonConvert.DeserializeObject<JObject>(jsonResponse);

				// Process received messages
				if (messageData != null)
				{
					var messages = messageData["SMSMessageData"]?["Messages"];
					if (messages != null)
					{
						foreach (var message in messages)
						{
							Console.WriteLine($"Message ID: {message["id"]}");
							Console.WriteLine($"From: {message["from"]}");
							Console.WriteLine($"Message: {message["text"]}");
							Console.WriteLine($"Date: {message["date"]}");
							// Do something with the message
						}
					}
				}
				else
				{
					Console.WriteLine("No messages found.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching messages: {ex.Message}");
			}
		}

		public async Task<ServiceResponse<object>> SendBulkOTPAsync(IFormFile file, string message, string sender)
		{
			try
			{
				var username = string.Empty;
				var apiKey = string.Empty;
				var from = string.Empty;

				if (file == null)
					return ServiceResponse<object>.Information("File is empty!", null);
				if (sender == null)
					return ServiceResponse<object>.Information("Sender is empty!", null);
				if (message == null)
					return ServiceResponse<object>.Information("Message is empty!", null);

				if (sender == "Progas")
				{
					username = _proAfricaIsTalkingSettings.Username;
					apiKey = _proAfricaIsTalkingSettings.ApiKey;
					from = _proAfricaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "Otogas")
				{
					username = _africaIsTalkingSettings.Username;
					apiKey = _africaIsTalkingSettings.ApiKey;
					from = _africaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "ProtoSafety")
				{
					username = _safetyAfricasTalking.Username;
					apiKey = _safetyAfricasTalking.ApiKey;
					from = _safetyAfricasTalking.SMSSenderId;
				}
				else
				{
					return ServiceResponse<object>.Information("Invalid sender!", null);
				}

				var phoneNumberList = new List<PhoneNumbers>();

				if (file == null || file.Length <= 0)
					return ServiceResponse<object>.Information("File is empty!", null);

				if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
					return ServiceResponse<object>.Information("Invalid file format. Please upload an Excel file.", null);

				using var stream = new MemoryStream();
				await file.CopyToAsync(stream);

				using var workbook = new XLWorkbook(stream);
				var worksheet = workbook.Worksheet(1); // Get the first worksheet
				var row = worksheet.LastRowUsed();
				if (row == null)
					return ServiceResponse<object>.Information("No data found in the Excel file!", null);
				int rowCount = row.RowNumber();

				// Get the data from the Excel file to a list
				var phoneNumber = worksheet.CellsUsed().Select(x => x.Value.ToString()).ToList();

				// Process and filter valid phone numbers
				phoneNumberList = [.. phoneNumber
					.Select(static x =>
					{
						// Format based on the length of the phone number
						if (x.Length == 9)
						{
							return "+254" + x;
						}
						else if (x.Length == 10 && x.StartsWith("0"))
						{
							return string.Concat("+254", x.AsSpan(1)); // Remove the leading '0'
						}
						else if (x.Length == 12 && x.StartsWith("254"))
						{
							return "+" + x; // Ensure it starts with '+'
						}
						else if (x.Length == 13 && x.StartsWith("+254"))
						{
							return x; // Valid number
						}
						else
						{
							return null; // Invalid phone number
						}
					})
					.Where(x => x != null) // Filter out invalid (null) numbers
					.Select(x => new PhoneNumbers { PhoneNumber = x ?? string.Empty })];

				HashSet<PhoneNumbers> values = new(phoneNumberList);
				phoneNumberList = new List<PhoneNumbers>(values);

				if (phoneNumberList.Count == 0)
					return ServiceResponse<object>.Information("No valid phone numbers found!", null);

				// Split phone numbers into batches of 10,000
				const int batchSize = 10000;
				var totalBatches = (int)Math.Ceiling((double)phoneNumberList.Count / batchSize);

				var geteway = new AfricasTalkingGateway(username, apiKey);

				for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
				{
					var currentBatch = phoneNumberList.Skip(batchIndex * batchSize).Take(batchSize).ToList();
					var recipients = string.Join(',', currentBatch.Select(x => x.PhoneNumber));

					// Send SMS for the current batch
					var result = geteway.SendMessage(recipients, message, from, -1);
					await Task.Delay(1000); // Adjust delay as necessary
				}

				return ServiceResponse<object>.Success($"{sender} Bulk messages sent successfully", null);
			}
			catch (Exception ex)
			{
				// You might want to log the exception message to troubleshoot
				return ServiceResponse<object>.Error($"An error occurred while sending OTP: {ex.Message}", null);
			}
		}

		public class PhoneNumbers
		{
			public string PhoneNumber { get; set; } = string.Empty;
		}
		//get credit balance sms in africastalking

		public class SmsBalanceResponse
		{
			public string ErrorMessage { get; set; } = string.Empty;
			[Precision(18, 2)] public decimal Balance { get; set; }
		};

		public static async Task<SmsBalanceResponse> GetSmsBalance(string username, string apiKey)
		{
			using HttpClient client = new();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("apiKey", apiKey);

			string url = $"https://api.africastalking.com/version1/user?username={username}";

			try
			{
				HttpResponseMessage response = await client.GetAsync(url);

				// Check if the response status code is successful
				response.EnsureSuccessStatusCode();

				// Read the response content as string
				string responseBody = await response.Content.ReadAsStringAsync();

				// Parse the JSON response to extract the balance
				var parsedResponse = JsonConvert.DeserializeObject<RootObject>(responseBody);

				// Assuming the balance is in the "UserData" section and is a string like "KES 235.9784"
				string balanceString = string.Empty;
				if (parsedResponse is not null)
				{
					balanceString = parsedResponse.UserData.Balance;
				}
				// Remove the currency prefix (KES) and parse the numeric part
				decimal balance = 0m;

				if (decimal.TryParse(balanceString.Replace("KES", "").Trim(), out balance))
				{
					return new SmsBalanceResponse
					{
						Balance = balance
					};
				}
				else
				{
					// If parsing fails, return an error
					return new SmsBalanceResponse
					{
						ErrorMessage = "Error parsing the balance from the response."
					};
				}
			}
			catch (HttpRequestException e)
			{
				// Handle specific HTTP request exceptions
				return new SmsBalanceResponse
				{
					ErrorMessage = $"Error fetching balance: {e.Message}"
				};
			}
			catch (Exception e)
			{
				// Handle any other exceptions
				return new SmsBalanceResponse
				{
					ErrorMessage = $"An unexpected error occurred: {e.Message}"
				};
			}
		}

		// Define a root object for deserialization based on the JSON response structure
		public class RootObject
		{
			[JsonProperty("UserData")]
			public UserData UserData { get; set; } = new UserData();
		}

		public class UserData
		{
			[JsonProperty("balance")]
			public string Balance { get; set; } = string.Empty;
		}


		public async Task<(decimal Balance, string Message)> FetchCreditBalance(string sender)
		{
			decimal balance = 0m;
			string? username;
			string? apiKey;
			string? from;
			if (sender == "Progas")
			{
				username = _proAfricaIsTalkingSettings.Username;
				apiKey = _proAfricaIsTalkingSettings.ApiKey;
				_ = _proAfricaIsTalkingSettings.SMSSenderId;
			}
			else if (sender == "Otogas")
			{
				username = _africaIsTalkingSettings.Username;
				apiKey = _africaIsTalkingSettings.ApiKey;
				from = _africaIsTalkingSettings.SMSSenderId;
			}
			else if (sender == "ProtoSafety")
			{
				username = _safetyAfricasTalking.Username;
				apiKey = _safetyAfricasTalking.ApiKey;
				from = _safetyAfricasTalking.SMSSenderId;
			}
			else
			{
				return (0m, "Invalid sender!");
			}

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("apiKey", apiKey);
				string url = $"https://api.africastalking.com/version1/user?username={username}";

				HttpResponseMessage response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();

				var parsedResponse = JsonConvert.DeserializeObject<RootObject>(responseBody);
				string balanceString = parsedResponse?.UserData?.Balance ?? "KES 0";
				if (decimal.TryParse(balanceString.Replace("KES", "").Trim(), out decimal parsedBalance))
				{
					balance = parsedBalance;
				}
			}

			return (balance, $"Credit balance is {balance}");
		}

		public class XmlDatas
		{
			public string xmlData { get; set; } = string.Empty;
		}
		public async Task<ServiceResponse> ProcessSmsCallback(string callbackData)
		{
			try
			{

				// Validate callback data
				if (string.IsNullOrWhiteSpace(callbackData))
					return ServiceResponse<object>.Information("Callback data cannot be empty.");

				var parameters = System.Web.HttpUtility.ParseQueryString(callbackData);

				// Extract parameters
				var phoneNumber = parameters["phoneNumber"];
				var retryCount = parameters["retryCount"];
				var messageId = parameters["id"];
				var status = parameters["status"];
				var networkCode = parameters["networkCode"];

				// Validate required parameters
				if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(messageId) || string.IsNullOrEmpty(status))
				{
					return ServiceResponse<object>.Information("Missing required callback parameters.");
				}

				// Define log file path (use configurable path)
				string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "RecipientsLog.txt");

				// Ensure the directory exists
				var directory = Path.GetDirectoryName(filePath);
				if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				// Write logs and update database
				using (var fileWriter = new StreamWriter(filePath, true))
				{
					await fileWriter.WriteLineAsync($"[{DateTime.UtcNow}] Callback Data: {callbackData}");
					await fileWriter.WriteLineAsync($"PhoneNumber: {phoneNumber}, Status: {status}, MessageId: {messageId}, NetworkCode: {networkCode}, RetryCount: {retryCount}");

					// Update the database
					var message = await _context.BulkMessageLogs.FirstOrDefaultAsync(m => m.MessageId == messageId);
					if (message != null)
					{
						message.DeliveryStatus = status;
						_context.BulkMessageLogs.Update(message);
					}

					var callback = new SmsCallbacks
					{
						MessageId = messageId,
						Cost = 0,
						DateAdded = DateTime.UtcNow,
						NetworkCode = networkCode ?? "",
						FailureReason = "",
						PhoneNumber = phoneNumber,
						Status = status
					};
					_context.SmsCallbacks.Add(callback);


					// Save database changes
					await _context.SaveChangesAsync();
				}

				return ServiceResponse<object>.Success("Callback processed successfully.");
			}
			catch (Exception ex)
			{
				// Log exception details
				Console.WriteLine($"Error: {ex.Message}");
				return ServiceResponse<object>.Error("An error occurred while processing the callback.");
			}
		}


		public async Task<ServiceResponse<object>> BulkMessages(IFormFile file, string message, string sender)
		{
			try
			{
				var str = new StringBuilder().Append(message);

				var batchNumber = _setups.GenerateSaleId();
				// Initialize Africa's Talking settings based on the sender
				string? username = string.Empty;
				string? apiKey = string.Empty;
				string? from = string.Empty;

				if (file == null)
					return ServiceResponse<object>.Information("File is empty!", null);
				if (string.IsNullOrEmpty(sender))
					return ServiceResponse<object>.Information("Sender is empty!", null);
				if (string.IsNullOrEmpty(message))
					return ServiceResponse<object>.Information("Message is empty!", null);

				if (sender == "Progas")
				{
					username = _proAfricaIsTalkingSettings.Username;
					apiKey = _proAfricaIsTalkingSettings.ApiKey;
					from = _proAfricaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "Otogas")
				{
					username = _africaIsTalkingSettings.Username;
					apiKey = _africaIsTalkingSettings.ApiKey;
					from = _africaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "ProtoSafety")
				{
					username = _safetyAfricasTalking.Username;
					apiKey = _safetyAfricasTalking.ApiKey;
					from = _safetyAfricasTalking.SMSSenderId;
				}
				else
				{
					return ServiceResponse<object>.Information("Invalid sender!", null);
				}

				if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
					return ServiceResponse<object>.Information("Invalid file format. Please upload an Excel file.", null);

				var phoneNumberList = new List<string?>();

				using var stream = new MemoryStream();
				await file.CopyToAsync(stream);
				using var workbook = new XLWorkbook(stream);
				var worksheet = workbook.Worksheet(1);
				var row = worksheet.LastRowUsed();
				if (row == null)
					return ServiceResponse<object>.Information("No data found in the Excel file!", null);

				var phoneNumbers = worksheet.CellsUsed().Select(x => x.Value.ToString()).ToList();
				phoneNumbers.RemoveAll(x => string.IsNullOrWhiteSpace(x));
				// Process phone numbers
				phoneNumberList = phoneNumbers
					.Select(static x =>
					{
						if (x.Length == 9)
							return "+254" + x;
						else if (x.Length == 10 && x.StartsWith("0"))
							return string.Concat("+254", x.AsSpan(1)); // Remove the leading '0'
						else if (x.Length == 12 && x.StartsWith("254"))
							return "+" + x; // Ensure it starts with '+'
						else if (x.Length == 13 && x.StartsWith("+254"))
							return x; // Valid number
						else
							return null; // Invalid phone number
					})
					.Where(x => x != null)
					.Distinct()
					.ToList();

				if (phoneNumberList.Count == 0)
					return ServiceResponse<object>.Information("No valid phone numbers found!", null);

				// Split phone numbers into batches of 10,000
				const int batchSize = 10000;
				var totalBatches = (int)Math.Ceiling((double)phoneNumberList.Count / batchSize);

				using var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Add("apiKey", apiKey);

				for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
				{
					var currentBatch = phoneNumberList.Skip(batchIndex * batchSize).Take(batchSize).ToList();
					var recipients = string.Join(",", currentBatch);

					var payload = new FormUrlEncodedContent(
					[
						new KeyValuePair<string, string>("username", username ?? ""),
						new KeyValuePair<string, string>("to", recipients),
						new KeyValuePair<string, string>("message", str.ToString()),
						new KeyValuePair<string, string>("from", from)
					]);

					var response = await httpClient.PostAsync("https://api.africastalking.com/version1/messaging", payload);
					var result = await response.Content.ReadAsStringAsync();

					if (response.Content.Headers.ContentType?.MediaType != "text/xml")
					{
						Console.WriteLine($"Unexpected response format: {result}");
						return ServiceResponse<object>.Error("Unexpected response format from the server.", null);
					}

					try
					{
						try
						{
							var xmlDoc = new XmlDocument();
							xmlDoc.LoadXml(result);

							var recipientsNode = xmlDoc.SelectNodes("//Recipient") ?? throw new Exception("Invalid XML structure.");

							var bulkMessageLogs = recipientsNode
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
									StatusCode = recipientNode["statusCode"]?.InnerText.ToString() ?? string.Empty,
									Cost = recipientNode["cost"]?.InnerText ?? string.Empty,
									MessageId = recipientNode["messageId"]?.InnerText ?? string.Empty,
									BatchNumber = batchNumber,
									Timestamp = DateTime.UtcNow,
									DeliveryStatus = "Sent"
								})
								.ToList();


							var senderName = sender;
							var userName = _authentications.Name();
							string xhtml = EmailBody();

							xhtml = xhtml.Replace("{{batchnumber}}", batchNumber)
										 .Replace("{{userName}}", userName)
										 .Replace("{{recipientName}}", userName)
										 .Replace("{{senderName}}", senderName)
										 .Replace("{{norecipients}}",bulkMessageLogs.Count.ToString())
										 .Replace("{{message}}", message) 
										 .Replace("{{startDate}}",DateTime.UtcNow.Date.AddDays(-1).ToString("yyyy-MM-dd"))
										 .Replace("{{endDate}}", DateTime.UtcNow.Date.AddDays(1).ToString("yyyy-MM-dd"));

							if (bulkMessageLogs.Count != 0)
							{
								_context.BulkMessageLogs.AddRange(bulkMessageLogs);

								await _context.SaveChangesAsync();
								var email = _context.Users.Where(x => x.UserCode == _authentications.Usercode()).Select(x => x.Email).First();

								if(email != null) 
								if (email.EndsWith("@protoenergy.com"))
								{
									_ = Task.Run(() => _emailService.SendEmail(email,null,"Bulk SMS", xhtml));
								}
							}
							else
							{
								// Log a warning or take appropriate action if no valid recipients are found
								Console.WriteLine("No valid recipients found in the XML.");
							}
						}
						catch (Exception ex)
						{
							// Log the exception or handle it as needed
							Console.WriteLine($"Error processing XML: {ex.Message}");
							// You can rethrow or handle based on your application's needs
							throw;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"XML Parsing error: {ex.Message}");
						Console.WriteLine($"Response content: {result}");
						return ServiceResponse<object>.Error("Failed to parse server response.", null);
					}

					await Task.Delay(1000); // Throttle requests if necessary
				}

				return ServiceResponse<object>.Success($"{sender} Bulk messages sent successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while sending OTP: {ex.Message}", null);
			}
		}

		public async Task<ServiceResponse<object>> BulkMessages(List<string> phoneNumbers, string message, string sender)
		{
			try
			{
				var str = new StringBuilder().Append(message);

				var batchNumber = _setups.GenerateSaleId();
				// Initialize Africa's Talking settings based on the sender
				string? username = string.Empty;
				string? apiKey = string.Empty;
				string? from = string.Empty;

				if (phoneNumbers == null)
					return ServiceResponse<object>.Information("No phonenumbers to send to", null);

				if (string.IsNullOrEmpty(sender))
					return ServiceResponse<object>.Information("Sender is empty!", null);

				if (string.IsNullOrEmpty(message))
					return ServiceResponse<object>.Information("Message is empty!", null);

				if (sender == "Progas")
				{
					username = _proAfricaIsTalkingSettings.Username;
					apiKey = _proAfricaIsTalkingSettings.ApiKey;
					from = _proAfricaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "Otogas")
				{
					username = _africaIsTalkingSettings.Username;
					apiKey = _africaIsTalkingSettings.ApiKey;
					from = _africaIsTalkingSettings.SMSSenderId;
				}
				else if (sender == "ProtoSafety")
				{
					username = _africaIsTalkingSettings.Username;
					apiKey = _africaIsTalkingSettings.ApiKey;
					from = _africaIsTalkingSettings.SMSSenderId;
				}
				else
				{
					return ServiceResponse<object>.Information("Invalid sender!", null);
				}

			
				var phoneNumberList = new List<string?>();

				phoneNumbers.RemoveAll(x => string.IsNullOrWhiteSpace(x));
				// Process phone numbers
				phoneNumberList = phoneNumbers
					.Select(static x =>
					{
						if (x.Length == 9)
							return "+254" + x;
						else if (x.Length == 10 && x.StartsWith("0"))
							return string.Concat("+254", x.AsSpan(1)); // Remove the leading '0'
						else if (x.Length == 12 && x.StartsWith("254"))
							return "+" + x; // Ensure it starts with '+'
						else if (x.Length == 13 && x.StartsWith("+254"))
							return x; // Valid number
						else
							return null; // Invalid phone number
					})
					.Where(x => x != null)
					.Distinct()
					.ToList();

				if (phoneNumberList.Count == 0)
					return ServiceResponse<object>.Information("No valid phone numbers found!", null);

				// Split phone numbers into batches of 10,000
				const int batchSize = 10000;
				var totalBatches = (int)Math.Ceiling((double)phoneNumberList.Count / batchSize);

				using var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Add("apiKey", apiKey);

				for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
				{
					var currentBatch = phoneNumberList.Skip(batchIndex * batchSize).Take(batchSize).ToList();
					var recipients = string.Join(",", currentBatch);

					var payload = new FormUrlEncodedContent(
					[
						new KeyValuePair<string, string>("username", username ?? ""),
						new KeyValuePair<string, string>("to", recipients),
						new KeyValuePair<string, string>("message", str.ToString()),
						new KeyValuePair<string, string>("from", from)
					]);

					var response = await httpClient.PostAsync("https://api.africastalking.com/version1/messaging", payload);
					var result = await response.Content.ReadAsStringAsync();

					if (response.Content.Headers.ContentType?.MediaType != "text/xml")
					{
						Console.WriteLine($"Unexpected response format: {result}");
						return ServiceResponse<object>.Error("Unexpected response format from the server.", null);
					}

					try
					{
						try
						{
							var xmlDoc = new XmlDocument();
							xmlDoc.LoadXml(result);

							var recipientsNode = xmlDoc.SelectNodes("//Recipient") ?? throw new Exception("Invalid XML structure.");

							var bulkMessageLogs = recipientsNode
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
									StatusCode = recipientNode["statusCode"]?.InnerText.ToString() ?? string.Empty,
									Cost = recipientNode["cost"]?.InnerText ?? string.Empty,
									MessageId = recipientNode["messageId"]?.InnerText ?? string.Empty,
									BatchNumber = batchNumber,
									Timestamp = DateTime.UtcNow,
									DeliveryStatus = "Sent"
								})
								.ToList();


							var senderName = sender == "Progas" ? "Progas" : "Otogas";
							var userName = _authentications.Name();
							string xhtml = EmailBody();

							xhtml = xhtml.Replace("{{batchnumber}}", batchNumber)
										 .Replace("{{userName}}", userName)
										 .Replace("{{recipientName}}", userName)
										 .Replace("{{senderName}}", senderName)
										 .Replace("{{norecipients}}", bulkMessageLogs.Count.ToString())
										 .Replace("{{message}}", message)
										 .Replace("{{startDate}}", DateTime.UtcNow.Date.AddDays(-1).ToString("yyyy-MM-dd"))
										 .Replace("{{endDate}}", DateTime.UtcNow.Date.AddDays(1).ToString("yyyy-MM-dd"));

							if (bulkMessageLogs.Count != 0)
							{
								_context.BulkMessageLogs.AddRange(bulkMessageLogs);

								await _context.SaveChangesAsync();
								var email = _context.Users.Where(x => x.UserCode == _authentications.Usercode()).Select(x => x.Email).First();

								if (email != null)
								{
									if (email.EndsWith("@protoenergy.com"))
									{
										_ = Task.Run(() => _emailService.SendEmail(email, null, "Bulk SMS", xhtml));
									}
								}
							}
							else
							{
								// Log a warning or take appropriate action if no valid recipients are found
								Console.WriteLine("No valid recipients found in the XML.");
							}
						}
						catch (Exception ex)
						{
							// Log the exception or handle it as needed
							Console.WriteLine($"Error processing XML: {ex.Message}");
							// You can rethrow or handle based on your application's needs
							throw;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"XML Parsing error: {ex.Message}");
						Console.WriteLine($"Response content: {result}");
						return ServiceResponse<object>.Error("Failed to parse server response.", null);
					}

					await Task.Delay(1000); // Throttle requests if necessary
				}

				return ServiceResponse<object>.Success($"{sender} Bulk messages sent successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while sending OTP: {ex.Message}", null);
			}
		}

		//get data from BulkMessageLog where batchNumber = batchno

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
                        <a href="https://otogas.protoenergy.com/otogas/Messaging/DownloadBulkMessages?sender=Otogas&batchNumber={{batchnumber}}&startDate={{startDate}}&endDate={{endDate}}" class="download-link">Download Messages</a>
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

		//schedule sms messages to be sent on table RescheduledMessages in the database upload an exce file with the contacts 
		//and the message to be sent

		public async Task<string> ScheduleMessagesFromFileAsync(IFormFile file, DateTime scheduledDate,string SenderId)
		{
			try
			{
				if (file == null || file.Length == 0)
				{
					return "No file uploaded.";
				}

				// Create a temporary file path to save the uploaded file
				var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

				// Save the uploaded file to the temp directory
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				// Load the Excel file
				using var package = new ExcelPackage(new FileInfo(filePath));
				var worksheet = package.Workbook.Worksheets[0];

				// Create a list to hold the scheduled messages
				var rescheduledMessages = new List<RescheduledMessages>();

				// Iterate through rows in Excel, assuming the first row is headers
				for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // starting from 2 assuming row 1 is header
				{
					var phoneNumber = worksheet.Cells[row, 1].Text; // Assuming phone numbers are in column 1
					var message = worksheet.Cells[row, 2].Text;    // Assuming messages are in column 2

					// Validate phone number and message
					if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(message))
					{
						continue; // Skip invalid rows
					}

					// Add the new RescheduledMessage to the list
					rescheduledMessages.Add(new RescheduledMessages
					{
						PhoneNumber = phoneNumber,
						Message = message,
						ScheduledSendingdate = scheduledDate,
						IsSent = false,  // Mark as unsent
						SenderId = SenderId,
						DateCreated = DateTime.UtcNow,
						DateSent = DateTime.UtcNow,
					
						
					});
				}

				// Insert the messages into the database
				await _context.RescheduledMessages.AddRangeAsync(rescheduledMessages);
				await _context.SaveChangesAsync();

				return $"{rescheduledMessages.Count} messages scheduled successfully.";
			}
			catch (Exception ex)
			{
				// Handle exceptions (e.g., file format issues)
				return $"Error: {ex.Message}";
			}
		}





		public async Task<ServiceResponse<object>> GetBulkMessages(int pageNumber, int pageSize, string sender)
		{
			pageNumber = pageNumber < 1 ? 1 : pageNumber;
			pageSize = pageSize < 1 ? 15 : pageSize;
			var response = new ServiceResponse<object>();
			var message = "";
			try
			{
				// Validate pagination parameters
				if (pageNumber < 1 || pageSize < 1)
				{
					return ServiceResponse<object>.Information("Invalid page number or page size.", null);
				}

				// Calculate the items to skip
				int skip = (pageNumber - 1) * pageSize;

				// Fetch paginated messages
				var messages = await _context.BulkMessageLogs
					.Select(bm => new
					{
						bm.Id,
						bm.MessageId,
						bm.StatusCode,
						bm.Cost,
						bm.RecipientNumber,
						bm.Message,
						bm.DeliveryStatus,
						bm.BatchNumber,
						bm.Timestamp,
						bm.Sender,
					

					}).Where(bm => bm.Sender == sender).OrderByDescending(x => x.Id)
					.Skip(skip)
					.Take(pageSize)
					.ToListAsync();

				// Get total record count
				var totalRecords = await _context.BulkMessageLogs.CountAsync();
				var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

				// Prepare response
				var data = new
				{
					Messages = messages,
					TotalRecords = totalRecords,
					TotalPages = totalPages,
					CurrentPage = pageNumber
				};

				message = "Bulk messages retrieved successfully.";
				return ServiceResponse<object>.Success(message, data);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong", null);
			}
		}
		public async Task<ServiceResponse<MemoryStream>> DownloadBulkMessages([Required] string sender,string? batchNumber, DateTime? startDate, DateTime? endDate)
		{
			try
			{
				// Build the query with filters
				var query = _context.BulkMessageLogs.AsQueryable();

				if (!string.IsNullOrEmpty(batchNumber))
				{
					query = query.Where(bm => bm.BatchNumber == batchNumber);
				}

				if (startDate.HasValue)
				{
					query = query.Where(bm => bm.Timestamp >= startDate.Value);
				}

				if (endDate.HasValue)
				{
					query = query.Where(bm => bm.Timestamp <= endDate.Value);
				}

				if (!string.IsNullOrEmpty(sender))
				{
					query = query.Where(bm => bm.Sender == sender);
				}

				// Fetch messages
				var messages = await query
					.Select(bm => new
					{
						bm.MessageId,
						bm.StatusCode,
						bm.Cost,
						bm.RecipientNumber,
						bm.Message,
						bm.DeliveryStatus,
						bm.BatchNumber,
						bm.Timestamp,
						bm.Sender,
						
					})
					.ToListAsync();

				// Create an Excel file
				var workbook = new ClosedXML.Excel.XLWorkbook();
				var worksheet = workbook.Worksheets.Add("BulkMessages");
				var currentRow = 1;

				// Add header row
				worksheet.Cell(currentRow, 1).Value = "Message ID";
				worksheet.Cell(currentRow, 2).Value = "Status Code";
				worksheet.Cell(currentRow, 3).Value = "Cost";
				worksheet.Cell(currentRow, 4).Value = "Recipient Number";
				worksheet.Cell(currentRow, 5).Value = "Message";
				worksheet.Cell(currentRow, 6).Value = "Delivery Status";
				worksheet.Cell(currentRow, 7).Value = "Batch Number";
				worksheet.Cell(currentRow, 8).Value = "Timestamp";

				// Add data rows
				foreach (var message in messages)
				{
					currentRow++;
					worksheet.Cell(currentRow, 1).Value = message.MessageId;
					worksheet.Cell(currentRow, 2).Value = message.StatusCode;
					worksheet.Cell(currentRow, 3).Value = message.Cost;
					worksheet.Cell(currentRow, 4).Value = message.RecipientNumber;
					worksheet.Cell(currentRow, 5).Value = message.Message;
					worksheet.Cell(currentRow, 6).Value = message.DeliveryStatus;
					worksheet.Cell(currentRow, 7).Value = message.BatchNumber;
					worksheet.Cell(currentRow, 8).Value = message.Timestamp;
				}

				//convert to a table
				var range = worksheet.Range(worksheet.FirstCellUsed(), worksheet.LastCellUsed());
				var table = range.CreateTable();
				
				table.ShowAutoFilter = false;
				// table theme
				table.Theme = XLTableTheme.TableStyleLight13;
				worksheet.Columns().AdjustToContents();

				// Save Excel file to memory stream
				var memoryStream = new MemoryStream();
				workbook.SaveAs(memoryStream);
				memoryStream.Seek(0, SeekOrigin.Begin);
				
				// Return the Excel file stream
				return ServiceResponse<MemoryStream>.Success("Bulk messages exported successfully.", memoryStream);
			}
			catch (Exception ex)
			{
				return ServiceResponse<MemoryStream>.Error($"Something went wrong: {ex.Message}", null);
			}
		}

		public class ResponseData
		{
			public SMSMessageData SMSMessageData { get; set; } = new SMSMessageData();
		}

		public class SMSMessageData
		{
			public string Message { get; set; } = string.Empty;
			public List<Recipient> Recipients { get; set; } = [];
		}

		public class Recipient
		{
			public string Number { get; set; } = string.Empty;
			public string Status { get; set; } = string.Empty;
			public int StatusCode { get; set; }
			public string Cost { get; set; } = string.Empty;
			public string MessageId { get; set; } = string.Empty;
		}

	}
}
