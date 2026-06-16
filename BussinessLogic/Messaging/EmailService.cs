using DataAccessLayer.DTOs.Messaging;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Data;
using System.Text;

namespace BussinessLogic.Messaging
{
	public class EmailService : IEmailService
	{
		private readonly SmtpSettings _smtp;
		private readonly ILogger<EmailService> _logger;

		public EmailService(IOptions<SmtpSettings> smtp, ILogger<EmailService> logger)
		{
			_smtp = smtp.Value;
			_logger = logger;
		}

		// ── Simple send (Refactored to async Task to fix timeouts) ───────────────────

		public async Task SendEmail(string toEmail, string? ccEmail, string subject, string body)
		{
			if (string.IsNullOrWhiteSpace(toEmail) ||
				string.IsNullOrWhiteSpace(subject) ||
				string.IsNullOrWhiteSpace(body))
				throw new ArgumentException("Email, subject, and body are required.");

			try
			{
				var message = BuildBaseMessage(subject, body);
				message.To.Add(MailboxAddress.Parse(toEmail));

				if (!string.IsNullOrWhiteSpace(ccEmail))
					message.Cc.Add(MailboxAddress.Parse(ccEmail));

				await ExecuteSendAsync(message);
				_logger.LogInformation("Email sent to {To}", toEmail);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error sending email via MailKit to {To}", toEmail);
				throw;
			}
		}

		// ── Send with CSV attachment ────────────────────────────────

		public async Task SendEmailWithExcelAttachmentAsync(
			string[] toEmails,
			string[] ccEmails,
			DateTime reportDate,
			string subject,
			string body,
			DataTable data)
		{
			using var csvStream = DataTableToCsvStream(data);
			var filename = $"Report{reportDate:ddMMyyyy}.csv";

			var message = BuildBaseMessage(subject, body);

			foreach (var to in toEmails.Where(e => !string.IsNullOrWhiteSpace(e)))
				message.To.Add(MailboxAddress.Parse(to));

			foreach (var cc in ccEmails.Where(e => !string.IsNullOrWhiteSpace(e)))
				message.Cc.Add(MailboxAddress.Parse(cc));

			// Create the multi-part body to append the attachment safely
			var bodyBuilder = new BodyBuilder { HtmlBody = body };

			// Reset stream position just in case before loading
			csvStream.Position = 0;
			bodyBuilder.Attachments.Add(filename, csvStream.ToArray(), ContentType.Parse("text/csv"));
			message.Body = bodyBuilder.ToMessageBody();

			await ExecuteSendAsync(message);
			_logger.LogInformation("Attachment email sent ({File}) to {Count} recipients", filename, toEmails.Length);
		}

		// ── Private helpers ─────────────────────────────────────────

		private MimeMessage BuildBaseMessage(string subject, string body)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_smtp.DisplayName, _smtp.Username));
			message.Subject = subject;

			// Default simple text/html body configuration
			var bodyBuilder = new BodyBuilder { HtmlBody = body };
			message.Body = bodyBuilder.ToMessageBody();

			return message;
		}

		private async Task ExecuteSendAsync(MimeMessage message)
		{
			using var client = new SmtpClient();

			// Automatically choose secure socket options based on port
			SecureSocketOptions options = _smtp.Port == 465
				? SecureSocketOptions.SslOnConnect
				: SecureSocketOptions.StartTls;

			// Connect, Authenticate, Send, and Disconnect cleanly
			await client.ConnectAsync(_smtp.Host, _smtp.Port, options);
			await client.AuthenticateAsync(_smtp.Username, _smtp.Password);
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}

		private static MemoryStream DataTableToCsvStream(DataTable table)
		{
			var sb = new StringBuilder();

			sb.AppendLine(string.Join(",", table.Columns
				.Cast<DataColumn>()
				.Select(c => CsvEscape(c.ColumnName))));

			foreach (DataRow row in table.Rows)
				sb.AppendLine(string.Join(",", row.ItemArray
					.Select(v => CsvEscape(v?.ToString() ?? ""))));

			return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
		}

		private static string CsvEscape(string value)
		{
			if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
				return $"\"{value.Replace("\"", "\"\"")}\"";
			return value;
		}
	}
}