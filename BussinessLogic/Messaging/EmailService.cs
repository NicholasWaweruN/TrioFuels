using DataAccessLayer.DTOs.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using Npgsql;

public class EmailService : IEmailService
{
	private readonly SmtpSettings _smtpSettings;

	public EmailService(IOptions<SmtpSettings> smtpSettings)
	{
		_smtpSettings = smtpSettings.Value;
	}

	public void SendEmail(string toEmail, string? toccEmail, string subject, string body)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(toEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
			{
				throw new ArgumentException("Email, subject, and body must be provided.");
			}

			using (SmtpClient smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
			{
				smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
				smtpClient.EnableSsl = _smtpSettings.EnableSsl;

				using MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(_smtpSettings.Username);
				mailMessage.To.Add(toEmail);
				if (!string.IsNullOrWhiteSpace(toccEmail))
				{
					mailMessage.CC.Add(toccEmail);
				}
				mailMessage.Subject = subject;
				mailMessage.Body = body;
				mailMessage.IsBodyHtml = true; // Enable HTML content

				smtpClient.Send(mailMessage);
			}

			// Use proper logging here instead of Console
			Console.WriteLine("Email sent successfully to " + toEmail);
		}
		catch (SmtpException smtpEx)
		{
			// Log specific SMTP errors
			Console.WriteLine("SMTP error occurred: " + smtpEx.Message);
		}
		catch (Exception ex)
		{
			// Log general errors
			Console.WriteLine("An error occurred while sending the email: " + ex.Message);
		}
	}

	public async Task SendEmailWithExcelAttachmentAsync(
		string[] toEmails,
		string[] ccEmails,
		DateTime reportDate,
		string subject,
		string body, DataTable data)
	{
		try
		{

			// Convert the data table to a MemoryStream containing CSV format
			MemoryStream csvStream = ConvertDataTableToCsvStream(data);

			// Send the email with the CSV data attached
			await SendEmailWithAttachmentAsync(csvStream, $"Report{reportDate:ddMMyyyy}.csv", toEmails, ccEmails, subject, body);
		}
		catch (Exception ex)
		{
			// Log the error
			Console.WriteLine($"Error occurred: {ex.Message}");
		}
	}

	// Method to fetch data from the database using stored procedure
	private static async Task<DataTable> GetSalesReportDataAsync(DateTime reportDate)
	{
		//string connectionString = ConfigurationManager.ConnectionStrings["OtogasDb"].ConnectionString;
		string storedProcedure = "OTOGASSALESREPORTS";
		DataTable dt = new DataTable();
		var connectionString = "";//.ConnectionStrings["OtogasDb"].ConnectionString;
		using (NpgsqlConnection connection = new(connectionString))
		{
			using (NpgsqlCommand command = new(storedProcedure, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				// You can pass parameters for report date or other criteria here if necessary
				command.Parameters.Add(new NpgsqlParameter("@ReportDate", reportDate));

				await connection.OpenAsync();
				using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
				{
					dt.Load(reader);
				}
			}
		}

		return dt;
	}

	// Method to convert a DataTable to a MemoryStream containing CSV data
	private static MemoryStream ConvertDataTableToCsvStream(DataTable dataTable)
	{
		var csvContent = new StringBuilder();

		// Header
		csvContent.AppendLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

		// Rows
		foreach (DataRow row in dataTable.Rows)
		{
			csvContent.AppendLine(string.Join(",", row.ItemArray.Select(item => item ?? string.Empty.ToString())));
		}

		// Convert the StringBuilder to a MemoryStream
		var csvStream = new MemoryStream();
		var writer = new StreamWriter(csvStream);
		writer.Write(csvContent.ToString());
		writer.Flush();
		csvStream.Position = 0; // Reset the stream position to the beginning

		return csvStream;
	}

	// Method to send an email with the CSV data attached as a stream
	private static async Task SendEmailWithAttachmentAsync(MemoryStream csvStream, string filename, string[] toEmails, string[] ccEmails, string subject, string body)
	{
		using MailMessage mail = new();
		mail.From = new MailAddress("Reports@protoenergy.com");
		mail.IsBodyHtml = true;
		// Add recipients
		foreach (var email in toEmails)
		{
			mail.To.Add(email);
		}
		// Add CC recipients
		foreach (var ccEmail in ccEmails)
		{
			mail.CC.Add(ccEmail);
		}

		mail.Subject = subject;
		mail.Body = body;

		// Attach the CSV data as a stream
		using Attachment attachment = new(csvStream, filename, "text/csv");
		mail.Attachments.Add(attachment);

		// Set up SMTP client and send the email
		using SmtpClient smtp = new("smtp.office365.com");
		smtp.Port = 587;
		smtp.EnableSsl = true;
		smtp.Credentials = new NetworkCredential
		{
			UserName = "Reports@protoenergy.com",
			Password = "Tag50274"
		};

		await smtp.SendMailAsync(mail);
	}

}
