using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Messaging
{
	public class EmailWorkflow : IEmailWorkflow
	{
		private readonly GraphServiceClient _graphClient;

		// Constructor injects a pre-configured GraphServiceClient
		public EmailWorkflow(GraphServiceClient graphClient)
		{
			_graphClient = graphClient;
		}

		private static byte[] ConvertDataTableToCsvBytes(DataTable dataTable)
		{
			var sb = new StringBuilder();
			sb.AppendLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
			foreach (DataRow row in dataTable.Rows)
				sb.AppendLine(string.Join(",", row.ItemArray.Select(i => i?.ToString() ?? "")));
			return Encoding.UTF8.GetBytes(sb.ToString());
		}

		public async Task SendEmailAsync(IEnumerable<string> toEmails, IEnumerable<string>? ccEmails, string subject, string body, DataTable? csvData = null)
		{
			var message = new Message
			{
				Subject = subject,
				Body = new ItemBody { ContentType = BodyType.Html, Content = body },
				ToRecipients = toEmails.Select(e => new Recipient { EmailAddress = new EmailAddress { Address = e } }).ToList()
			};

			if (ccEmails != null)
				message.CcRecipients = ccEmails.Select(e => new Recipient { EmailAddress = new EmailAddress { Address = e } }).ToList();

			if (csvData != null)
			{
				message.Attachments =
				[
					new FileAttachment
					{
						Name = $"Report_{DateTime.UtcNow:yyyyMMdd}.csv",
						ContentType = "text/csv",
						ContentBytes = ConvertDataTableToCsvBytes(csvData)
					}
				];
			}

			await _graphClient.Users["Reports@protoenergy.com"]
				.SendMail
				.PostAsync(new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
				{
					Message = message,
					SaveToSentItems = true,

				});

			Console.WriteLine($"Sent email '{subject}' to {string.Join(", ", toEmails)}");
		}

		public async Task<Message?> GetLatestEmailAsync(string subjectFilter)
		{
			var messages = await _graphClient.Users["Reports@protoenergy.com"]
				.MailFolders["Inbox"]
				.Messages
				.GetAsync(config =>
				{
					config.QueryParameters.Top = 1;
					config.QueryParameters.Filter = $"subject eq '{subjectFilter}'";
					config.QueryParameters.Orderby = ["receivedDateTime desc"];
				});

			return messages?.Value?.FirstOrDefault();
		}

		public async Task<Message?> GeEmailWithConversationIdAsync(string subjectFilter)
		{
			var messages = await _graphClient.Users["Reports@protoenergy.com"]
				.MailFolders["Inbox"]
				.Messages
				.GetAsync(config =>
				{
					config.QueryParameters.Top = 1;
					config.QueryParameters.Filter = $"subject eq '{subjectFilter}'";
					config.QueryParameters.Orderby = ["receivedDateTime desc"];
					config.QueryParameters.Select = ["id", "subject", "from", "conversationId", "receivedDateTime", "toRecipients", "ccRecipients"];
				});

			return messages?.Value?.FirstOrDefault();
		}


		public async Task ReplyToEmailAsync(Message originalMessage, string replyBody, DataTable? csvData = null)
		{
			var replyMessage = new Message
			{
				Body = new ItemBody { ContentType = BodyType.Html, Content = replyBody }
			};

			if (csvData != null)
			{
				replyMessage.Attachments = new List<Attachment>
				{
					new FileAttachment
					{
						Name = $"Report_{DateTime.UtcNow:yyyyMMdd}.csv",
						ContentType = "text/csv",
						ContentBytes = ConvertDataTableToCsvBytes(csvData)
					}
				};
			}

			await _graphClient.Users["Reports@protoenergy.com"]
				.Messages[originalMessage.Id]
				.Reply
				.PostAsync(new Microsoft.Graph.Users.Item.Messages.Item.Reply.ReplyPostRequestBody
				{
					Message = replyMessage
				});

			Console.WriteLine($"Replied to email '{originalMessage.Subject}'");
		}

		public async Task RunFullWorkflowAsync(IEnumerable<string> toEmails, IEnumerable<string>? ccEmails, DataTable csvData)
		{
			string subject = $"ProtoOS Daily Report {DateTime.UtcNow:yyyyMMddHHmmss}";
			string body = "Hello, this is your daily report from ProtoOS.";

			// 1️⃣ Send initial email
			await SendEmailAsync(toEmails, ccEmails, subject, body, csvData);

			// 2️⃣ Poll inbox for reply
			Message? replyEmail = null;
			int attempts = 0;
			while (replyEmail == null && attempts < 20)
			{
				Console.WriteLine("Checking for reply...");
				await Task.Delay(15000); // 15 seconds
				replyEmail = await GetLatestEmailAsync(subject);
				attempts++;
			}

			// 3️⃣ Reply in same thread if reply received
			if (replyEmail != null)
				await ReplyToEmailAsync(replyEmail, "Thanks for your reply! Here is the report again.", csvData);
			else
				Console.WriteLine("No reply detected within the polling period.");
		}
	}
}
