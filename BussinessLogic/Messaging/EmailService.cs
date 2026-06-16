using DataAccessLayer.DTOs.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

public class EmailService : IEmailService
{
	private readonly SmtpSettings _smtp;
	private readonly ILogger<EmailService> _logger;

	public EmailService(IOptions<SmtpSettings> smtp, ILogger<EmailService> logger)
	{
		_smtp = smtp.Value;
		_logger = logger;
	}

	// ── Simple send ────────────────────────────────────────────

	public void SendEmail(string toEmail,string? ccEmail,string subject,string body)
	{
		if (string.IsNullOrWhiteSpace(toEmail) ||
			string.IsNullOrWhiteSpace(subject) ||
			string.IsNullOrWhiteSpace(body))
			throw new ArgumentException("Email, subject, and body are required.");

		try
		{
			using var client = BuildSmtpClient();
			using var message = BuildMessage(subject, body);

			message.To.Add(toEmail);
			if (!string.IsNullOrWhiteSpace(ccEmail))
				message.CC.Add(ccEmail);

			client.Send(message);
			_logger.LogInformation("Email sent to {To}", toEmail);
		}
		catch (SmtpException ex)
		{
			_logger.LogError(ex, "SMTP error sending to {To}", toEmail);
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
		await using var csvStream = DataTableToCsvStream(data);
		var filename = $"Report{reportDate:ddMMyyyy}.csv";

		await SendWithAttachmentAsync(csvStream, filename, toEmails, ccEmails, subject, body);
	}

	// ── Private helpers ─────────────────────────────────────────

	private SmtpClient BuildSmtpClient() =>
		new(_smtp.Host, _smtp.Port)
		{
			Credentials = new NetworkCredential(_smtp.Username, _smtp.Password),
			EnableSsl = _smtp.EnableSsl,
		};

	private MailMessage BuildMessage(string subject, string body) =>
		new()
		{
			From = new MailAddress(_smtp.Username, _smtp.DisplayName),
			Subject = subject,
			Body = body,
			IsBodyHtml = true,
		};

	private async Task SendWithAttachmentAsync(
		MemoryStream stream,
		string filename,
		string[] toEmails,
		string[] ccEmails,
		string subject,
		string body)
	{
		using var client = BuildSmtpClient();
		using var message = BuildMessage(subject, body);
		using var attach = new Attachment(stream, filename, "text/csv");

		foreach (var to in toEmails) message.To.Add(to);
		foreach (var cc in ccEmails) message.CC.Add(cc);
		message.Attachments.Add(attach);

		await client.SendMailAsync(message);
		_logger.LogInformation("Attachment email sent ({File}) to {Count} recipients", filename, toEmails.Length);
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

		var ms = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
		// position already 0 — no manual reset needed
		return ms;
	}

	// Wrap values that contain commas, quotes, or newlines
	private static string CsvEscape(string value)
	{
		if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
			return $"\"{value.Replace("\"", "\"\"")}\"";
		return value;
	}
}

public class SmtpSettings
{
	public string Host { get; set; } = "smtp.gmail.com";
	public int Port { get; set; } = 587;
	public bool EnableSsl { get; set; } = true;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string DisplayName { get; set; } = string.Empty;
}