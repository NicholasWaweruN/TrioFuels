using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using static BussinessLogic.Sales.MissingSales.MisingSale;
using static BusinessLogic.Services.Services;
using static BussinessLogic.Stock.Stock.StockServicecs;
using BussinessLogic.PlateDetection;
using Syncfusion.XlsIO.Implementation.Collections;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;

namespace BussinessLogic.Stock.VarianceReport
{
	public class VarianceReport
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emails;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IEmailWorkflow _workflow;

		public VarianceReport(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups, IEmailService emails, ICommonSalesTasks salesTasks, IEmailWorkflow workflow)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
			_emails = emails;
			_salesTasks = salesTasks;
			_workflow = workflow;
		}
		public async Task<ServiceResponse<object>> GetVarianceReport(string shiftNumber)
		{
			try
			{
				// Fetch variances from database
				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   join sft in _context.Shifts on ss.ShiftNumber equals sft.ShiftNumber
									   where ss.ShiftNumber == shiftNumber
									   select new VarianceDto
									   {
										   ShiftId = sft.Id,
										   DispenserCode = d.DispenserCode,
										   ShiftNumber = ss.ShiftNumber,
										   UserCode = ss.UserCode,
										   NozzleCode = ss.NozzleCode,
										   OpeningReading = ss.OpeningReading,
										   ExpectedOpeningReading = ss.ExpectedOpeningReading,
										   ClosingReading = ss.ClosingReading,
										   ExpectedClosingReading = ss.ExpectedClosingReading,
										   Variance = ss.ClosingVariance + ss.OpeningVariance,
										   QuantitySold = ss.QuantitySold,
										   Status = ss.VarianceStatus == 2 ? "VARIANCE" : "CLOSED",
										   DateCreated = ss.DateCreated,
										   NozzleName = n.NozzleName,
										   DispenserName = d.DispenserName,
										   StationName = s.StationName,
										   StationCode = s.StationCode,
										   PayrollNumber = u.PayrollNumber,
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName }),
									   }).AsNoTracking().ToListAsync();

				if (variances.Count == 0)
				{
					return ServiceResponse<object>.Information("No variances found for the specified shift.", null);
				}

				// Populate DataTable for variances
				var dataTable = new DataTable("VarianceReport");
				dataTable.Columns.AddRange(
				[
					new("ShiftId", typeof(int)),
			new("DispenserCode", typeof(string)),
			new("ShiftNumber", typeof(string)),
			new("UserCode", typeof(string)),
			new("NozzleCode", typeof(string)),
			new("OpeningReading", typeof(decimal)),
			new("ExpectedOpeningReading", typeof(decimal)),
			new("ClosingReading", typeof(decimal)),
			new("ExpectedClosingReading", typeof(decimal)),
			new("Variance", typeof(decimal)),
			new("QuantitySold", typeof(decimal)),
			new("Status", typeof(string)),
			new("DateCreated", typeof(DateTime)),
			new("NozzleName", typeof(string)),
			new("DispenserName", typeof(string)),
			new("StationName", typeof(string)),
			new("StationCode", typeof(string)),
			new("PayrollNumber", typeof(string)),
			new("Name", typeof(string))
				]);

				foreach (var variance in variances)
				{
					dataTable.Rows.Add(
						variance.ShiftId,
						variance.DispenserCode,
						variance.ShiftNumber,
						variance.UserCode,
						variance.NozzleCode,
						variance.OpeningReading,
						variance.ExpectedOpeningReading,
						variance.ClosingReading,
						variance.ExpectedClosingReading,
						variance.Variance,
						variance.QuantitySold,
						variance.Status,
						variance.DateCreated,
						variance.NozzleName,
						variance.DispenserName,
						variance.StationName,
						variance.StationCode,
						variance.PayrollNumber,
						variance.Name ?? string.Empty
					);
				}

				// Calculate totals
				var totalVariance = variances.Sum(x => x.Variance);
				var litresSold = await _context.QuantityTransactions
					.Where(q => q.ShiftNumber == shiftNumber)
					.SumAsync(x => x.QuantityCredit - x.QuantityDebit);

				// Email subject and body
				var subject = $"{variances.First().StationName} {variances.First().DispenserName} Variance Shift: {variances.First().ShiftNumber} (ShiftId: {variances.First().ShiftId})" +
							  (totalVariance == 0 ? " - Variance Cleared Automatically" : "");
				var body = CreateEmailBody(variances, totalVariance, litresSold, totalVariance == 0);

				// Fetch and validate email recipients
				var result = await GetEmailRecipients("001");
				var realResult = result.ResponseObject;
				if (realResult?.To == null || !realResult.To.Any())
				{
					return ServiceResponse<object>.Information("No email recipients found", null);
				}
				var emailsTo = realResult.To.Split(',').ToArray();
				var emailsToCC = realResult.ToCC.Split(',').ToArray();

				var emailsTonew = realResult.To.Split(',').ToList();
				var emailsToCCnew = realResult.ToCC.Split(',').ToList();

				// Send email with Excel attachment
				//await _workflow.SendEmailAsync(emailsTonew,emailsToCCnew,subject,body,dataTable);
				//var conversation =  await _workflow.GetLatestEmailAsync(subject);
				
				//var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber);
				//if (shift != null && conversation is not null)
				//{
				//	shift.EmailConversationId = conversation.ConversationId ?? string.Empty;
				//	shift.IsReplySent = false;
				//	_context.Shifts.Update(shift);
				//	await _context.SaveChangesAsync();
				//}

			    await _emails.SendEmailWithExcelAttachmentAsync(emailsTo, emailsToCC, DateTime.UtcNow, subject, body, dataTable);
				return ServiceResponse<object>.Success("Variance report generated successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}
		private static string CreateEmailBody(IEnumerable<VarianceDto> variances, decimal totalVariance, decimal litresSold, bool varianceCleared)
		{
			var firstVariance = variances.First();
			_ = varianceCleared ? "Variance Cleared Automatically" : "Variance";
			return $@"
    <html>
    <body>
        <p>Dear {(varianceCleared ? "Team" : "All")},</p>
        <p>{(varianceCleared ? "This shift's variance falls within the acceptable limit of less than 1 liter and has been automatically cleared." : "Please find attached the variance report for your review.")}</p>
        <p><strong>Report Summary:</strong></p>
        <ul>
            <li><strong>Name:</strong> {firstVariance.Name}</li>
            <li><strong>Variance Date:</strong> {DateTime.UtcNow:MMMM dd, yyyy}</li>
            <li><strong>Station:</strong> {firstVariance.StationName}</li>
            <li><strong>Dispenser:</strong> {firstVariance.DispenserName}</li>
            <li><strong>Shift ID:</strong> {firstVariance.ShiftId}</li>
            <li><strong>Shift Number:</strong> {firstVariance.ShiftNumber}</li>
            <li><strong>Total Variance:</strong> {totalVariance}</li>
            <li><strong>Litres Sold:</strong> {litresSold}</li>
        </ul>
        <p>If you have any questions, please feel free to reach out.</p>
        <p><strong>Best regards,</strong></p>
        <p>The Otopay Team</p>
    </body>
    </html>";
		}
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
					if (_authentication.Usercode() == "00008")
					{
						newEmail = new EmailsDto
						{
							To = "wawerun@protoenergy.com",
							ToCC = "wawerun@protoenergy.com"
						};
					}

					return ServiceResponse<EmailsDto>.Success("", newEmail);

				}
				return ServiceResponse<EmailsDto>.Information("No email recipients found", null);

			}
			catch (Exception )

			{

				return ServiceResponse<EmailsDto>.Error("Something went wrong", null);
			}
		}
		public async Task<ServiceResponse<object>> ClearVariance(string shiftNumber)
		{
			try
			{
				var vehicle = GetVehicleAsync(shiftNumber);
				var vprice = await (from p in _context.Prices
									where p.ProductCode.Equals("")
									select p.Amount).FirstOrDefaultAsync();

				// Fetch variances with a raw SQL query
				var variances = await _context.StockTakeSummaries
					.FromSqlRaw("SELECT * FROM StockTakeSummaries WHERE ShiftNumber = {0}", shiftNumber)
					.AsNoTracking()
					.ToListAsync();

				// Fetch dispenserId with a raw SQL query
				var dispenserId = await _context.Shifts
					.FromSqlRaw("SELECT TOP 1 DispenserCode FROM Shifts WHERE ShiftNumber = {0}", shiftNumber)
					.Select(s => s.DispenserCode)
					.AsNoTracking()
					.FirstOrDefaultAsync();

				// Fetch stationCode with a raw SQL query
				var stationCode = await _context.Dispensers
					.FromSqlRaw("SELECT TOP 1 StationCode FROM Dispensers WHERE DispenserCode = {0}", dispenserId ?? "")
					.Select(s => s.StationCode)
					.FirstOrDefaultAsync();

				// Calculate total variance
				var totalVariance = variances.Sum(x => x.ClosingVariance);
				var highest = variances.Max(x => Math.Abs(x.ClosingVariance));
				//if (highest > 3)
				//{
				//	var result = await NozzleQuantityTransfer(shiftNumber);
				//}

				if (Math.Abs(totalVariance) <= 1)
				{
					foreach (var variance in variances)
					{
						if (Math.Abs(variance.ClosingVariance) > 0)
						{
							var saleId = _setups.GenerateSaleId();
							// Insert QuantityTransaction via raw SQL
							await _context.Database.ExecuteSqlRawAsync(@"
							INSERT INTO QuantityTransactions 
							(DateCreated, UserCode, NozzleCode, QuantityCredit, QuantityDebit, ShiftNumber, SaleId, 
							PaymentTypeCode, VehicleCode, DispenserCode, AmountCredit, AmountDebit, IsReversed, Price, StationCode)
							VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})",
								DateTime.UtcNow, variance.UserCode ?? "", variance.NozzleCode, variance.ClosingVariance, 0, shiftNumber, saleId,
								3, variance.UserCode ?? "", dispenserId ?? "", 0, 0, false, 0, stationCode ?? "");

							// Insert PaymentTransaction via raw SQL
							await _context.Database.ExecuteSqlRawAsync(@"
							INSERT INTO PaymentTransactions (DateCreated, UserCode, PaymentRefrence, SaleId, TransactionAmount, TransactionAmountDebit)
							VALUES ({0}, {1}, '', {2}, {3}, 0)", 
							DateTime.UtcNow, variance.UserCode ?? "", saleId, variance.ClosingVariance);

							// Update VarianceStatus in StockTakeSummary via raw SQL
							await _context.Database.ExecuteSqlRawAsync(@"
							UPDATE StockTakeSummaries SET VarianceStatus = {0} WHERE Id = {1} AND NozzleCode = {2}",
								ShiftStatus.Closed, variance.Id, variance.NozzleCode);
						}
					}

					// Commit all changes
					await _context.SaveChangesAsync();
					await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
					var message = $"Variance of quantity {totalVariance:N2} of ShiftNumber {shiftNumber} has been cleared on {DateTime.UtcNow} by system service, it falls within the recommended variance bracket";
					await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

					return ServiceResponse<object>.Success("Variance cleared successfully", null);
				}

				return ServiceResponse<object>.Information("Variance not cleared", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
		{
			return await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => new Vehicle
				{
					ProductCode = v.ProductCode,
					VehicleRegistration = v.VehicleRegistrationNumber,
					CreditLimit = v.CreditLimit,
				}).FirstOrDefaultAsync() ?? new Vehicle();
		}
	}
}
