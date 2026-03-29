using System.Data;

public interface IEmailService
{
	void SendEmail(string toEmail, string? toccEmail, string subject, string body);
	Task SendEmailWithExcelAttachmentAsync(string[] toEmails, string[] ccEmails, DateTime reportDate, string subject, string body, DataTable data);
}