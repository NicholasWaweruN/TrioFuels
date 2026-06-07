using System.Data;

namespace BussinessLogic.Messaging;

public interface IEmailWorkflow
{
	    Task SendEmailAsync(
		IEnumerable<string> toEmails,
		IEnumerable<string>? ccEmails,
		string subject,
		string body,
		DataTable? csvData = null);
}