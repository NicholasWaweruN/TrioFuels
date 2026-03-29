using Microsoft.Graph.Models;
using System.Data;

namespace BussinessLogic.Messaging
{
	public interface IEmailWorkflow
	{
		Task<Message?> GeEmailWithConversationIdAsync(string subjectFilter);
		Task<Message?> GetLatestEmailAsync(string subjectFilter);
		Task ReplyToEmailAsync(Message originalMessage, string replyBody, DataTable? csvData = null);
		Task RunFullWorkflowAsync(IEnumerable<string> toEmails, IEnumerable<string>? ccEmails, DataTable csvData);
		Task SendEmailAsync(IEnumerable<string> toEmails, IEnumerable<string>? ccEmails, string subject, string body, DataTable? csvData = null);
	}
}