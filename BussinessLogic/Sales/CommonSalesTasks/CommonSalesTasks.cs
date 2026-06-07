using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Sales.CommonSalesTasks
{

	public class CommonSalesTasks : ICommonSalesTasks
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly IEmailWorkflow _workflow;
		private readonly IEmailService _emailService;

		public CommonSalesTasks(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups,IEmailWorkflow workflow,IEmailService emailService )
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_workflow = workflow;
			_emailService = emailService;
		}

		public async Task<ServiceResponse<object>> ReconcileStockSummariesAsync(string shiftNumber)
		{
			try
			{
				// Retrieve StockTakeSummaries using a raw SQL query
				var stockSummary = await _context.StockTakeSummaries
							 .FromSqlRaw("SELECT * FROM StockTakeSummaries WHERE ShiftNumber = {0}", shiftNumber)
							 .AsTracking()
							 .ToListAsync();

				if (stockSummary.Count == 0)
					return ServiceResponse<object>.Information("No stocktake summary found", null);

				foreach (var stock in stockSummary)
				{
					// Calculate total sales for each stock entry using raw SQL
					var totalSales = await _context.QuantityTransactions
									.Where(s => s.ShiftNumber == shiftNumber && s.NozzleCode == stock.NozzleCode)
									.SumAsync(x => x.QuantityCredit - x.QuantityDebit);

					// Update stock values directly in SQL
					stock.QuantitySold = totalSales;
					stock.ClosingVariance = stock.ClosingReading - (stock.OpeningReading + totalSales);
					stock.ExpectedClosingReading = stock.OpeningReading + totalSales;
					stock.VarianceStatus = stock.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;

					// Update each stock summary record
					await _context.Database.ExecuteSqlRawAsync(@"UPDATE StockTakeSummaries SET QuantitySold = {0}, ClosingVariance = {1}, VarianceStatus = {2}, ExpectedClosingReading = {3} Where Id = {4}",
						stock.QuantitySold, stock.ClosingVariance, stock.VarianceStatus, stock.ExpectedClosingReading, stock.Id);
				}

				// Check shift variance status based on stock summaries and update it
				var isShiftClosed = stockSummary.All(x => x.VarianceStatus == ShiftStatus.Closed);
				var shiftStatus = isShiftClosed ? ShiftStatus.Closed : ShiftStatus.Variance;

				await _context.Database.ExecuteSqlRawAsync(@"
				UPDATE Shifts
				SET ShiftStatus = {0}
				WHERE ShiftNumber = {1}", shiftStatus, shiftNumber);

				return ServiceResponse<object>.Success("Stock reconciled successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
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

		private static string BuildShiftClosingEmail(
		string shiftNumber,
		string attendantName,
		DateTime shiftStart,
		DateTime? shiftEnd,
		decimal totalSales,
		List<(string NozzleName, decimal OpeningReading, decimal ClosingReading)> stock)
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




		public void UpdateMpesaPaymentStatus(string transId)
		{
			//var query = @"
			//	DECLARE @Amount DECIMAL = ISNULL((SELECT SUM(TransactionAmount)-SUM(TransactionAmountDebit) FROM ProtoOs..PaymentTransactions WHERE PaymentRefrence = @TransId), 0); 
				
			//	DECLARE @OriginalAmount DECIMAL = (SELECT TransAmount FROM Protobase..MpesaC2Bpayments WHERE TransId = @TransId);  
			//	IF (@OriginalAmount - @Amount <= 0)       
			//	BEGIN          
			//		UPDATE Protobase..MpesaC2Bpayments SET UsageBalance = 0, status = 1 WHERE TransID = @TransId;              
			//	END   
			//	ELSE IF (@OriginalAmount - @Amount > 0)    
			//	BEGIN                   
			//		UPDATE Protobase..MpesaC2Bpayments  SET UsageBalance = (@OriginalAmount - @Amount), status = 0 WHERE TransID = @TransId;       
			//	END";
			//_context.Database.ExecuteSqlRaw(query, new SqlParameter("@TransId", transId));
		}


	}
}
