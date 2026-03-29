using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BusinessLogic.Worker.SalesReport
{
	public class WorkerRecipients : IWorkerRecipients
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _auth;


		public WorkerRecipients(OTOContext context, IAuthCommonTasks auth)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_auth = auth ?? throw new ArgumentNullException(nameof(auth));

			var connection = _context.Database.GetConnectionString();
		}

		public async Task<Mails?> GetRecipients(string reportCode)
		{
			// Validate input
			if (string.IsNullOrWhiteSpace(reportCode))
			{
				throw new ArgumentException("Report code cannot be null, empty, or whitespace.", nameof(reportCode));
			}

			// Default recipients (could be moved to config)
			const string defaultRecipient = "wawerun@protoenergy.com";

			try
			{
				// Query data using LINQ with explicit null handling
				var email = await _context.Emails
					.Where(e => e.ReportCode == reportCode)
					.Select(e => new
					{
						ToEmails = !string.IsNullOrWhiteSpace(e.To) ? e.To : defaultRecipient,
						CcEmails = !string.IsNullOrWhiteSpace(e.ToCC) ? e.ToCC : defaultRecipient
					})
					.AsNoTracking() // Improve performance since we're just reading
					.FirstOrDefaultAsync();

				// Return result or default if not found
				return new Mails
				{
					ToEmails = email?.ToEmails ?? defaultRecipient,
					CcEmails = email?.CcEmails ?? defaultRecipient
				};
			}
			catch (Exception ex) when (ex is not ArgumentException) // Don't catch argument exceptions
			{
				// Consider logging the exception here
				// _logger.LogError(ex, "Failed to retrieve email recipients for report {ReportCode}", reportCode);

				// Return default recipients
				return new Mails
				{
					ToEmails = defaultRecipient,
					CcEmails = defaultRecipient
				};
			}
		}

	}
	public class Mails
	{
		public string ToEmails { get; set; } = string.Empty;
		public string CcEmails { get; set; } = string.Empty;
	}

}
