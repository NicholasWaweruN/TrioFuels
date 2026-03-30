using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Payments.PaymentSetups;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Payments;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BussinessLogic.Payments.PaymentSetups 
{
	public class PaymentsSetups : IPaymentsSetups
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ILogger<PaymentsSetups> _logger;

		public PaymentsSetups(IAuthCommonTasks authentication, OTOContext context, ILogger<PaymentsSetups> logger)
		{
			_context = context;
			_authentication = authentication;
			_logger = logger;
		}

		// Helper to log user trail
		private async Task LogUserTrailAsync(string action)
		{
			var message = $"{action} by {_authentication.Name()} On {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
		}

		// Helper to return error responses
		private ServiceResponse<object> GetErrorResponse(string message)
		{
			return ServiceResponse<object>.Error(message, null);
		}
		// Add Till Numbers
		public async Task<ServiceResponse<object>> AddTill(addTillNumberDto till)
		{
			if (till is null)
			{
				return ServiceResponse<object>.Information("Till details cannot be empty", null);
			}
			var tillexists = await _context.Tills.FirstOrDefaultAsync(x => x.TillNumber == till.TillNumber);
			if (tillexists is not null)
				return ServiceResponse<object>.Information(@$"Till {till.TillNumber} already exists", null);

			var tillnumber = new Tills
			{
				TillName = till.TillName,
				TillNumber = till.TillNumber,
				StoreNumber = till.StoreNumber,
				DateCreated = DateTime.UtcNow,
				IsActive = true,
			};

			try
			{
				_context.Tills.Add(tillnumber);
				await _context.SaveChangesAsync();
				await LogUserTrailAsync($"Till {till.TillNumber} added by {_authentication.Name()} on {DateTime.UtcNow} ");

				return ServiceResponse<object>.Success($"Till {till.TillNumber} added successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding till");
				return GetErrorResponse("Error adding till");
			}
		}
		// Get all Till Numbers
		public async Task<ServiceResponse<object>> GetTills()
		{
			try
			{
				var tills = await _context.Tills.ToListAsync();
				if (tills.Count == 0)
					return ServiceResponse<object>.Information("No tills added", null);
				return ServiceResponse<object>.Success("Tills retrieved successfully", tills);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting tills");
				return GetErrorResponse("Error getting tills");
			}
		}
		// Update Till Numbers
		public async Task<ServiceResponse<object>> UpdateTill(UpdateTillDto till)
		{
			if (till is null)
				return ServiceResponse<object>.Information($"TillNumber can not be empty", null);

			var tillExists = await _context.Tills.FirstOrDefaultAsync(x => x.TillNumber == till.TillNumber);
			if (tillExists is null)
				return ServiceResponse<object>.Information("Till number not found for update", null);

			try
			{
				tillExists.TillName = till.TillName;
				tillExists.StoreNumber = till.StoreNumber;

				var message = $"Till {till.TillNumber} updated by {_authentication} on date {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("Till number updated successfully", null);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating till");
				return GetErrorResponse("Error updating till");
			}
		}
		// Assign till to a Dispenser
		public async Task<ServiceResponse<object>> AssignTillToDispenser(AssignTillToDispenserDto assignTill)
		{
			if (assignTill is null)
				return ServiceResponse<object>.Information("AssignTill details cannot be empty", null);

			var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.DispenserCode == assignTill.DispenserCode);
			if (dispenser is null)
				return ServiceResponse<object>.Information("Dispenser does not exist", null);

			try
			{
				dispenser.TillNumber = assignTill.TillNumber;
				_context.Update(dispenser);
				await _context.SaveChangesAsync();
				await LogUserTrailAsync($"Till {assignTill.TillNumber} assigned to Dispenser {assignTill.DispenserCode} by {_authentication.Name()} on {DateTime.UtcNow}");

				return ServiceResponse<object>.Success("Till assigned to dispenser successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error assigning till to dispenser");
				return GetErrorResponse("Error assigning till to dispenser");
			}
		}
		//get the till assigned to a dispenser pass in the dispenser code
		private async Task<ServiceResponse<string>> GetTillAssignedToDispenser(string dispenserCode)
		{
			if (string.IsNullOrWhiteSpace(dispenserCode))
				return ServiceResponse<string>.Information("Dispenser code cannot be empty", null);

			var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.DispenserCode == dispenserCode);
			if (dispenser is null)
				return ServiceResponse<string>.Information("Dispenser does not exist", null);

			if (string.IsNullOrWhiteSpace(dispenser.TillNumber))
				return ServiceResponse<string>.Information("No till assigned to dispenser", null);

			try
			{
				var till = await _context.Tills.FirstOrDefaultAsync(x => x.TillNumber == dispenser.TillNumber);
				if (till is null)
					return ServiceResponse<string>.Information("Till not found", null);
				return ServiceResponse<string>.Success("Till assigned to dispenser retrieved successfully", till.StoreNumber);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting till assigned to dispenser");
				return ServiceResponse<string>.Error("Error getting till assigned to dispenser", null);
			}
		}
		//execute sql get statement pasing parameters transid and ShortCode
		public async Task<ServiceResponse<object>> ValidateMpesaCode(string transId, string tillNumber)
		{
			var shortCode2 = await GetTillAssignedToDispenser(tillNumber);
			var shortCode = shortCode2.ResponseObject;

			if (string.IsNullOrWhiteSpace(transId) || string.IsNullOrWhiteSpace(shortCode))
				return ServiceResponse<object>.Information("Transaction Id and Short Code cannot be empty", null);

			var sql = $@"Select UsageBalance from Protobase..MpesaC2bPayments where BusinessShortCode = '{shortCode}' and Transid = '{transId}'";
			try
			{
				var mpesaTransaction = await _context.MpesaTransactions.FromSqlRaw(sql).FirstOrDefaultAsync();
				if (mpesaTransaction is null)
					return ServiceResponse<object>.Information("Mpesa transaction not found", null);
				return ServiceResponse<object>.Success("Mpesa transaction retrieved successfully", mpesaTransaction);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting mpesa transaction");
				return GetErrorResponse("Error getting mpesa transaction");
			}
		}
		//use the transid to confirm payment from database protoBase mpesaC2bPayments
		public async Task<ServiceResponse<object>> ConfirmPayment(string transId, string dispenserCode)
		{
			if (string.IsNullOrWhiteSpace(transId))
				return ServiceResponse<object>.Information("Mpesa Transactioncode cannot be empty", null);
			
			var sql = @"select TransID, BusinessShortCode, t.StoreNumber, t.TillNumber as Till, UsageBalance,DateTimeStamp,Mp.Status 
                from Protobase..MpesaC2bPayments Mp
                inner join Protobase..TillNumber t on Mp.BusinessShortCode = t.StoreNumber
                where Trim(TransId) = @transId";

			try
			{
				// Using Microsoft.Data.SqlClient.SqlParameter
				var param = new SqlParameter("@transId", transId.Trim());
				var result = _context.Database.SqlQueryRaw<MpesaTransactionDto>(sql, param);

				var transaction = result.AsNoTracking().FirstOrDefault();

				if (transaction == null)
					return ServiceResponse<object>.Information("Mpesa transaction not found", null);
				else
				{
					if (transaction.Status == 3)
						return ServiceResponse<object>.Information($"This transaction code is blocked", null);

					var date = transaction.DateTimeStamp;
					if (date.AddDays(-7) > DateTime.UtcNow)
						return ServiceResponse<object>.Information($"Transaction code {transaction.TransID} has expired", null);

					var till = await (from d in _context.Dispensers
									  where d.TillNumber == transaction.Till
									  where d.DispenserCode == dispenserCode
									  select d).FirstOrDefaultAsync();
					if (till == null)
						return ServiceResponse<object>.Information("Transaction Code does not belong to that till", null);

					if (till != null && transaction.UsageBalance > 0)
						return ServiceResponse<object>.Success($"The balance on this Mpesa code is {transaction.UsageBalance}", transaction);

					if (transaction.UsageBalance == 0)
						return ServiceResponse<object>.Information($"The balance is 0.00 can not transact.", 0);

					return ServiceResponse<object>.Information("Transaction Code does not belong to that till", null);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving Mpesa transaction for TransId: {TransId}", transId);
				return GetErrorResponse("Error retrieving Mpesa transaction");
			}
		}
		//Mpesa Payments

		public async Task<ServiceResponse<object>> ConfirmGaragePayment(string transId)
		{
			if (string.IsNullOrWhiteSpace(transId))
			{
				return ServiceResponse<object>.Information("Mpesa Transactioncode cannot be empty", null);
			}

			var sql = @"select TransID, BusinessShortCode, t.StoreNumber, t.TillNumber as Till, UsageBalance,DateTimeStamp,Mp.Status 
					from Protobase..MpesaC2bPayments Mp
					inner join Protobase..TillNumber t on Mp.BusinessShortCode = t.StoreNumber
					where Trim(TransId) = @transId";

			try
			{
				// Using Microsoft.Data.SqlClient.SqlParameter
				var param = new SqlParameter("@transId", transId.Trim());
				var result = _context.Database.SqlQueryRaw<MpesaTransactionDto>(sql, param);

				var transaction = await result.AsNoTracking().FirstOrDefaultAsync();

				if (transaction == null)
					return ServiceResponse<object>.Information("Mpesa transaction not found", null);
				else
				{
					if (transaction.Status == 3)
						return ServiceResponse<object>.Information($"This transaction code is blocked", null);

					var date = transaction.DateTimeStamp;
					if (date.AddDays(-7) > DateTime.UtcNow)
						return ServiceResponse<object>.Information($"Transaction code {transaction.TransID} has expired", null);

					
					if (transaction.UsageBalance > 0)
						return ServiceResponse<object>.Success($"The balance on this Mpesa code is {transaction.UsageBalance}", transaction);

					if (transaction.UsageBalance == 0)
						return ServiceResponse<object>.Information($"The balance is 0.00 can not transact.", 0);

					return ServiceResponse<object>.Information("Transaction Code does not belong to that till", null);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving Mpesa transaction for TransId: {TransId}", transId);
				return GetErrorResponse("Error retrieving Mpesa transaction");
			}
		}

		public async Task<ServiceResponse<object>> MpesaTransactions(string? tillNumber, DateTime? dateFrom, DateTime? dateTo, string? transId)
		{


			var parameters = new List<SqlParameter>();

			var sql = $@"
				select Mp.TransID, Mp.BusinessShortCode,TransAmount, t.StoreNumber, t.TillNumber as Till, Mp.UsageBalance,DateTimeStamp,
				iif(Mp.Status = 0,'Has Usage Balance',iif(Mp.status = 1,'Fully Used',iif(mp.status = 3,'Blocked',''))) as Status 
				from Protobase..MpesaC2bPayments Mp
				inner join Protobase..TillNumber t on Cast(Mp.BusinessShortCode as varchar(50)) = cast(t.StoreNumber as varchar(50))
				where cast(t.StoreNumber as varchar(50)) in
			   (SELECT StoreNumber COLLATE Latin1_General_CI_AS FROM Tills) ";

			if (!string.IsNullOrWhiteSpace(transId))
			{
				sql += " and Mp.TransID = @transId";
				parameters.Add(new SqlParameter("@transId", transId));
			}
			if (!string.IsNullOrWhiteSpace(tillNumber))
			{
				sql += " and t.TillNumber = @tillNumber";
				parameters.Add(new SqlParameter("@tillNumber", tillNumber));
			}
			if (dateFrom.HasValue)
			{
				sql += " and cast(Mp.DateTimeStamp as Date) >= @dateFrom";
				parameters.Add(new SqlParameter("@dateFrom", dateFrom.Value));
			}
			if (dateTo.HasValue)
			{
				sql += " and cast(Mp.DateTimeStamp as Date) <= @dateTo";
				parameters.Add(new SqlParameter("@dateTo", dateTo));
			}
			try
			{
				var result = _context.Database.SqlQueryRaw<MpesaTransactionsDto>(sql, parameters.ToArray());
				var transactions = await result.AsNoTracking().ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No Mpesa transactions found", null);

				return ServiceResponse<object>.Success("Mpesa transactions retrieved successfully", transactions);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting mpesa transactions");
				return GetErrorResponse("Error getting mpesa transactions");
			}
		}
		//update MpesaTransactions status to 3 for blocked
		public async Task<ServiceResponse<object>> BlockMpesa(string transId)
		{
			if (string.IsNullOrEmpty(transId))
				return ServiceResponse<object>.Information("TransId cannot be empty", null);

			var query = @"UPDATE Protobase..MpesaC2bPayments SET status = 3 WHERE TransID = @TransId";
			try
			{
				var message = $"Mpesa transaction code {transId} blocked by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				await _context.Database.ExecuteSqlRawAsync(query, new SqlParameter("@TransId", transId));
				return ServiceResponse<object>.Success("Mpesa transaction blocked successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error blocking mpesa transaction");
				return GetErrorResponse("Error blocking mpesa transaction");
			}
		}
		public async Task<ServiceResponse<object>> ActivateMpesa(string transId)
		{
			if (string.IsNullOrEmpty(transId))
				return ServiceResponse<object>.Information("TransId cannot be empty", null);

			var query = @"Update Protobase..MpesaC2bPayments SET status = 0,DateTimeStamp = getdate() WHERE TransID = @TransId";
			try
			{
				var message = $"Mpesa transaction code {transId} activated by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				await _context.Database.ExecuteSqlRawAsync(query, new SqlParameter("@TransId", transId));
				return ServiceResponse<object>.Success("Mpesa transaction blocked successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error blocking mpesa transaction");
				return GetErrorResponse("Error blocking mpesa transaction");
			}
		}
		//Add MpesaTransaction
		public async Task<ServiceResponse<MpesaTransactionDto>> AddMpesaTransaction(MpesaC2BPayment mpesaC2BPayment)
		{
			if (mpesaC2BPayment is null)
				return ServiceResponse<MpesaTransactionDto>.Information("Mpesa transaction details cannot be empty", null);

			var transTime = DateTime.UtcNow.ToString().Replace("/", "").Replace("-", "");

			var query = @"INSERT INTO Protobase..MpesaC2BPayments ( TransID, TransTime, TransAmount,BusinessShortCode, MSISDN, DateTimeStamp, status, UsageBalance, AddedBy) 
				VALUES (@TransID,@TransTime, @Amount, @BusinessShortCode, @PhoneNumber,getdate(),0,@UsageBalance,@UserCode)";

			try
			{

				await _context.Database.ExecuteSqlRawAsync(query,
					new SqlParameter("@TransID", mpesaC2BPayment.TransID),
					new SqlParameter("@TransTime", transTime),
					new SqlParameter("@Amount", mpesaC2BPayment.Amount),
					new SqlParameter("@BusinessShortCode", mpesaC2BPayment.BusinessShortCode),
					new SqlParameter("@PhoneNumber", mpesaC2BPayment.PhoneNumber),
					new SqlParameter("@UsageBalance", mpesaC2BPayment.Amount),
					new SqlParameter("@UserCode", _authentication.Usercode()));
				var message = $"Mpesa transaction code {mpesaC2BPayment.TransID} added by {_authentication.Name()} on {DateTime.UtcNow}";
				return ServiceResponse<MpesaTransactionDto>.Success("Mpesa transaction added successfully", null);
			}
			catch (Exception)
			{
				_logger.LogInformation("Error adding mpesa transaction");
				return ServiceResponse<MpesaTransactionDto>.Error("Something went wrong",null);
			}
		}

		//view a code usage 
		public async Task<ServiceResponse<object>> GetMpesaCodeUsage(string transId)
		{
			var sql = @$"Select Vehicle,ShiftNumber,SaleId,StationName,DispenserName,NozzleName,Attendant_Name as AttendantName,
				Litres,Amount,SalesDate,Price,TillNumber,Transid from vw_SalesData where Transid  like  '%{transId}%'";
			try
			{
				var result = _context.Database.SqlQueryRaw<FuelSale>(sql);
				var transactions = await result.AsNoTracking().ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No Mpesa transactions found", null);

				return ServiceResponse<object>.Success("Mpesa transactions retrieved successfully", transactions);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting mpesa transactions");
				return GetErrorResponse("Error getting mpesa transactions");
			}
		}
		//Add till 
	}
}
