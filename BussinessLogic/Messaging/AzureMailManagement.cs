using DataAccessLayer.DTOs.Messaging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BussinessLogic.Messaging;

public class EmailWorkflow : IEmailWorkflow
{
	private readonly SmtpSettings _smtp;

	public EmailWorkflow(IOptions<SmtpSettings> smtpOptions)
	{
		_smtp = smtpOptions.Value;
	}

	private static byte[] ConvertDataTableToCsvBytes(DataTable dataTable)
	{
		var sb = new StringBuilder();

		sb.AppendLine(string.Join(",",
			dataTable.Columns.Cast<DataColumn>()
				.Select(c => c.ColumnName)));

		foreach (DataRow row in dataTable.Rows)
		{
			sb.AppendLine(string.Join(",",
				row.ItemArray.Select(i => i?.ToString() ?? "")));
		}

		return Encoding.UTF8.GetBytes(sb.ToString());
	}

	public async Task SendEmailAsync(
		IEnumerable<string> toEmails,
		IEnumerable<string>? ccEmails,
		string subject,
		string body,
		DataTable? csvData = null)
	{
		using var mail = new MailMessage();

		mail.From = new MailAddress(_smtp.Username);
		mail.Subject = subject;
		mail.Body = body;
		mail.IsBodyHtml = true;

		foreach (var email in toEmails)
		{
			if (!string.IsNullOrWhiteSpace(email))
				mail.To.Add(email);
		}

		if (ccEmails != null)
		{
			foreach (var email in ccEmails)
			{
				if (!string.IsNullOrWhiteSpace(email))
					mail.CC.Add(email);
			}
		}

		if (csvData != null)
		{
			var csvBytes = ConvertDataTableToCsvBytes(csvData);

			mail.Attachments.Add(
				new Attachment(
					new MemoryStream(csvBytes),
					$"Report_{DateTime.UtcNow:yyyyMMdd}.csv",
					"text/csv"));
		}

		using var smtpClient = new SmtpClient(_smtp.Server, _smtp.Port)
		{
			Credentials = new NetworkCredential(
				_smtp.Username,
				_smtp.Password),
			EnableSsl = _smtp.EnableSsl
		};

		await smtpClient.SendMailAsync(mail);

		Console.WriteLine(
			$"Email '{subject}' sent to {string.Join(", ", toEmails)}");
	}
}