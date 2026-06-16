using System.Data;

namespace BussinessLogic.Messaging
{
	public interface IEmailService
	{
		Task SendEmail(string toEmail, string? ccEmail, string subject, string body);

		Task SendEmailWithExcelAttachmentAsync(string[] toEmails, string[] ccEmails, DateTime reportDate, string subject, string body, DataTable data);
	}
}