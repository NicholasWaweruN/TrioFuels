namespace BusinessLogic.Services
{
    using BussinessLogic.Authentication.CommonTasks;
    using DataAccessLayer.Common;
    using DataAccessLayer.Context;
    using DataAccessLayer.EntityModels.SetUps;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

        public class Services
    {
                private readonly OTOContext _context;
                private readonly IEmailService _emails;
                private readonly IAuthCommonTasks _authentications;

                public Services(OTOContext context, IEmailService email, IAuthCommonTasks tasks)
        {
			_context = context;
			_emails = email;
			_authentications = tasks;
        }

        //get variances from stocktakesummaries table where Variancestatus = 2 convert to a datatable

        public async Task<ServiceResponse<object>> GetVarianceReport()
        {
			try
			{
				var VarianceList = await (from ss in _context.StockTakeSummaries
										  join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
										  join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
										  join s in _context.Stations on d.StationCode equals s.StationCode
										  join shift in _context.Shifts on ss.ShiftNumber equals shift.ShiftNumber
										  where ss.VarianceStatus == ShiftStatus.Variance && shift.IsEmailSent == false
										  select new
										  {
											  shift.ShiftNumber,
											  ss.VarianceStatus
										  }).AsNoTracking().ToListAsync();
				foreach (var var in VarianceList)
				{

					// Fetch variances from database
					var variances = await (from ss in _context.StockTakeSummaries
										   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
										   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
										   join s in _context.Stations on d.StationCode equals s.StationCode
										   join u in _context.Users on ss.UserCode equals u.UserCode
										   where ss.VarianceStatus == ShiftStatus.Variance && ss.ShiftNumber == var.ShiftNumber
										   select new
										   {
											   d.DispenserCode,
											   ss.ShiftNumber,
											   ss.UserCode,
											   ss.NozzleCode,
											   ss.OpeningReading,
											   ss.ClosingReading,
											   ss.ExpectedClosingReading,
											   ss.ExpectedOpeningReading,
											   Variance = ss.ClosingVariance + ss.OpeningVariance,
											   ss.QuantitySold,
											   Status = ss.VarianceStatus == 2 ? "VARIANCE" : "CLOSED",
											   ss.DateCreated,
											   n.NozzleName,
											   d.DispenserName,
											   s.StationName,
											   s.StationCode,
											   u.PayrollNumber,
											   Name = string.Join(' ', u.FirstName, u.MiddName, u.LastName)
										   }).AsNoTracking().ToListAsync();
					// Check if no variances were found
					if (variances == null || variances.Count == 0)
						return ServiceResponse<object>.Information("No variances found", null);

					// Create and populate DataTable only if there are variances
					var dataTable = new DataTable("VarianceReport");
					dataTable.Columns.AddRange([

						new("ShiftNumber", typeof(string)),
						new("StationName", typeof(string)),
						new("DispenserName", typeof(string)),
						new("NozzleName", typeof(string)),
						new("OpeningReading", typeof(decimal)),
						new("ExpectedOpeningReading", typeof(decimal)),
						new("ClosingReading", typeof(decimal)),
						new("ExpectedClosingReading", typeof(decimal)),
						new("Variance", typeof(decimal)),
						new("QuantitySold", typeof(decimal)),
						new("Status", typeof(string)),
						new("DateCreated", typeof(DateTime)),
						new("PayrollNumber", typeof(string)),
						new("Name", typeof(string))
					]);

					// Add variances to DataTable
					foreach (var variance in variances)
					{
						dataTable.Rows.Add(
							variance.ShiftNumber,
							variance.StationName,
							variance.DispenserName,
							variance.NozzleName,
							variance.OpeningReading,
							variance.ExpectedOpeningReading,
							variance.ClosingReading,
							variance.ExpectedClosingReading,
							variance.Variance,
							variance.QuantitySold,
							variance.Status,
							variance.DateCreated,
							variance.PayrollNumber,
							variance.Name ?? string.Empty // Handle nulls gracefully
						);
					}
					var subject = $"{variances.First().StationName} {variances.First().DispenserName} Variance Shift: {variances.First().ShiftNumber}";

					// Email body
					string body = $@"
			<html>
			<body>
				<p>Dear All,</p>
				<p>Please find attached the variance report for your review.</p>
				<p><strong>Report Summary:</strong></p>
				<ul>
			   <p>Name: {variances.First().Name}</p>
					<li>Variance Date: {DateTime.UtcNow:MMMM dd, yyyy}</li>
					<li>Station: {variances.First().StationName}</li>
					<li>Dispenser: {variances.First().DispenserName}</li>
					<li>Total Variance: {variances.Sum(x => x.Variance)}</li>
					<li>Litres Sold: {variances.Sum(x => x.QuantitySold)}</li>
				</ul>
				<p>If you have any questions, feel free to reach out.</p>
				<p><strong>Best regards:</strong></p>
				<p><strong>Otopay Team:</strong></p>
			</body>
			</html>";

					var result = await GetEmailRecipients("001");
					var realResult = result.ResponseObject;
					if (realResult is null)
						return ServiceResponse<object>.Information("No email recipients found", null);
					var emailsTo = realResult.To.Split(',').ToArray();
					var emailsToCC = realResult.ToCC.Split(',').ToArray();

					// Send Email with Excel attachment
					string[] emailto = emailsTo;
					string[] emailtocc = emailsToCC;
					await _emails.SendEmailWithExcelAttachmentAsync(emailto, emailtocc, DateTime.UtcNow, subject, body, dataTable);

					return ServiceResponse<object>.Success("Variance report generated successfully", null);
				}
				return ServiceResponse<object>.Information("No variances found", null);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentications.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				// Return error response in case of failure
				return ServiceResponse<object>.Error(ex.Message, null);
			}
        }

        //get email recipients from Emails

        public async Task<ServiceResponse<EmailsDto>> GetEmailRecipients(string reportCode)
        {
			try
			{
				var emails = await (from e in _context.Emails
									select e).Where(x => x.ReportCode.Equals(reportCode)).FirstOrDefaultAsync();

				if (emails != null)
				{
					var newEmail = new EmailsDto
					{
						To = emails.To,
						ToCC = emails.ToCC,
					};
					return ServiceResponse<EmailsDto>.Success("", newEmail);

				}
				return ServiceResponse<EmailsDto>.Information("No email recipients found", null);

			}

			catch (Exception ex)
			{
				var method = ex.TargetSite;
				return ServiceResponse<EmailsDto>.Error(ex.Message, null);
			}
        }

		public class EmailsDto
		{
			public string To { get; set; } = string.Empty;
			public string ToCC { get; set; } = string.Empty;
		}
    }
}
