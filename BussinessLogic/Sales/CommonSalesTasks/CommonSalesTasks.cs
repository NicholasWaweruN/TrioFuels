using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace BussinessLogic.Sales.CommonSalesTasks
{

	public class CommonSalesTasks : ICommonSalesTasks
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly IEmailWorkflow _workflow;
		private readonly IEmailService _emailService;
		private readonly ILogger<CommonSalesTasks> _logger;

		public CommonSalesTasks(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups,IEmailWorkflow workflow,IEmailService emailService, ILogger<CommonSalesTasks> logger )
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_workflow = workflow;
			_emailService = emailService;
			_logger = logger;
		}

		public async Task<ServiceResponse<object>> ReconcileStockSummariesAsync(string shiftNumber)
		{
			try
			{
				// 1. Update Stock Summaries (ALL IN SQL)
				await _context.Database.ExecuteSqlRawAsync(@"
				UPDATE ""StockTakeSummaries"" s
				SET
					""QuantitySold"" = COALESCE(q.""TotalSales"", 0),

					""ExpectedClosingReading"" =
						s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0),

					""ClosingVariance"" =
						s.""ClosingReading"" - (s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0)),

					""VarianceStatus"" =
						CASE
							WHEN ABS(
								s.""ClosingReading"" - (s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0))
							) = 0
							THEN 0   -- Closed
							ELSE 2   -- Variance
						END
				FROM (
					SELECT
						""NozzleCode"",
						SUM(""QuantityCredit"" - ""QuantityDebit"") AS ""TotalSales""
					FROM ""QuantityTransactions""
					WHERE ""ShiftNumber"" = {0}
					GROUP BY ""NozzleCode""
				) q
				WHERE s.""ShiftNumber"" = {0}
				  AND s.""NozzleCode"" = q.""NozzleCode"";", shiftNumber);

				// 2. Update Shift based on updated stock
				await _context.Database.ExecuteSqlRawAsync(@"
			UPDATE ""Shifts""
			SET ""ShiftStatus"" =
				CASE
					WHEN NOT EXISTS (
						SELECT 1
						FROM ""StockTakeSummaries""
						WHERE ""ShiftNumber"" = {0}
						  AND ABS(""ClosingReading"" - (""OpeningReading"" + ""QuantitySold"")) <> 0
					)
					THEN 0   -- Closed
					ELSE 2   -- Variance
				END
			WHERE ""ShiftNumber"" = {0};", shiftNumber);

				return ServiceResponse<object>.Success("Stock reconciled successfully",null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex,"Failed to reconcile stock summaries for shift {ShiftNumber}",shiftNumber);

				return ServiceResponse<object>.Error("An error occurred while reconciling the stock summary",null
				);
			}
		}


		public async Task<ServiceResponse<object>> SendShiftCloseEmailAsync(string shiftNumber, decimal totalsales)
		{
			try
			{
				var shift = await (from s in _context.Shifts
								   where s.ShiftNumber == shiftNumber
								   select s).FirstOrDefaultAsync();
				

				var stock = await (from st in _context.StockTakeSummaries
								   join n in _context.Nozzles on st.NozzleCode equals n.NozzleCode
								   where st.ShiftNumber == shiftNumber
								   select new
								   {
									   n.NozzleName,
									   st.OpeningReading, 
									   st.ClosingReading
								   }).ToListAsync();

				if (shift == null)
					return ServiceResponse<object>.Information("Shift not found", null);

				// Prevent duplicate emails
				if (shift.IsReplySent)
					return ServiceResponse<object>.Information("Shift close email already sent", null);

				var emailBody = BuildShiftClosingEmail(
					shiftNumber,
					_authentication.Name(),
					shift.ShiftStartTime,
					shift.ShiftEndTime,
					totalsales,
					stock.Select(s => (s.NozzleName, s.OpeningReading, s.ClosingReading)).ToList()
				);

				var subject = $"Shift {shiftNumber} Closed - Total Sales: {totalsales:N2}";
				var body = emailBody;
			



				// Mark shift as email sent
				shift.IsReplySent = true;
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Shift close email sent successfully", null);
			}
			catch (Exception ex)
			{
				// Log exception if needed
				return ServiceResponse<object>.Error($"Error sending shift close email: {ex.Message}", null);
			}
		}

		private static string BuildShiftClosingEmail(string shiftNumber, string attendantName, DateTime shiftStart, DateTime? shiftEnd, decimal totalSales, List<(string NozzleName, decimal OpeningReading, decimal ClosingReading)> stock)
		{
			var sb = new System.Text.StringBuilder();

			sb.Append($@"
				<html>
				<body style='font-family: Arial, sans-serif; color: #333;'>
					<h2 style='color:#2E86C1;'>⛽ Shift Closing Report</h2>
					<p><b>Shift Number:</b> {shiftNumber}</p>
					<p><b>Attendant:</b> 👨‍💼 {attendantName}</p>
					<p><b>Shift Start:</b> 🕒 {shiftStart:dd-MMM-yyyy HH:mm}</p>
					<p><b>Shift End:</b> 🕒 {shiftEnd:dd-MMM-yyyy HH:mm}</p>
					<p><b>Total Sales:</b> 💵 {totalSales:N2}</p>

					<h3 style='color:#117A65;'>📊 Pump Readings</h3>
					<table style='border-collapse: collapse; width:100%;'>
						<tr style='background-color:#f2f2f2;'>
							<th style='border:1px solid #ddd; padding:8px;'>🛢️ Nozzle</th>
							<th style='border:1px solid #ddd; padding:8px;'>🔓 Opening Reading</th>
							<th style='border:1px solid #ddd; padding:8px;'>🔒 Closing Reading</th>
						</tr>");

						foreach (var item in stock)
						{
							sb.Append($@"
						<tr>
							<td style='border:1px solid #ddd; padding:8px;'>{item.NozzleName}</td>
							<td style='border:1px solid #ddd; padding:8px;'>{item.OpeningReading:N2}</td>
							<td style='border:1px solid #ddd; padding:8px;'>{item.ClosingReading:N2}</td>
						</tr>");
						}

						sb.Append(@"
					</table>
					<br/>
					<p style='font-size:12px; color:#888;'>✅ This is an automated report. Please do not reply.</p>
				</body>
				</html>");

						return sb.ToString();
		}




		public async Task UpdateMpesaPaymentStatus(string transId)
		{
			var mpesaTransaction = await _context.MpesaTransactions.FirstOrDefaultAsync(mt => mt.TransID == transId);

			// Nothing to reconcile if there's no matching M-Pesa transaction record.
			if (mpesaTransaction == null)
				return;

			var amount = await _context.PaymentTransactions.Where(x => x.PaymentRefrence == transId).SumAsync(x => x.TransactionAmount - x.TransactionAmountDebit);

			var originalAmount = mpesaTransaction.TransAmount;
			var remaining = originalAmount - amount;

			if (remaining <= 0)
			{
				mpesaTransaction.UsageBalance = 0;
				mpesaTransaction.Status = 1;
			}
			else
			{
				mpesaTransaction.UsageBalance = remaining;
				mpesaTransaction.Status = 0;
			}

			await _context.SaveChangesAsync();
		}


	}
}


