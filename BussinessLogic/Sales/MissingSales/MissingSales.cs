using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.SetupService;
using BussinessLogic.PlateDetection;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BussinessLogic.Setup;

namespace BusinessLogic.Sales.MissingSales
{
	public class MissingSales : IMissingSales
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly ICommonSalesTasks _salesTasks;

		public MissingSales(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, ICommonSalesTasks salesTasks)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_salesTasks = salesTasks;
		}


		 [Precision(18,2)] public decimal QuantityAmount { get; set; }
		public string SaleId { get; set; } = string.Empty;
		public class MpesaCodes
		{
			public string TransId { get; set; } = string.Empty;
			public decimal Amount { get; set; }
		}
		public List<MpesaCodes> mpesaCodes = [];
		//public List<MpesaCodes> MpesaCode { get; set;}
		#region Add Missing Sales

		public async Task<ServiceResponse<object>> AddMissingSales(MisingSaleDto sales)
		{
			if (sales.PaymentTypeCode == PaymetMethod.Voucher)
				return ServiceResponse<object>.Information("Voucher payment coming soon!", null);

			if (!ValidateSales(sales, out var response))
				return response;

			if (sales.PaymentDetails.Count == 0)
				return ServiceResponse<object>.Information("No payment details found", null);

			if (sales.PaymentTypeCode != PaymetMethod.Mpesa)
				sales.PaymentDetails.ForEach(p => p.TransactionReference = _setups.GenerateSaleId());

			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await ProcessMpesaPaymentsAsync(sales),
				PaymetMethod.Wallet => await ProcessWalletPaymentsAsync(sales),
				_ => await ProcessOtherPaymentsAsync(sales),
			};
		}
		private async Task<ServiceResponse<object>> ProcessMpesaPaymentsAsync(MisingSaleDto sales)
		{
			var productCode = await GetVehicleAsync(sales.VehicleCode);
			var expectedAmount = sales.Quantity * await GetPriceAsync(productCode.ProductCode, await GetAssignedStationAsync(sales));
			QuantityAmount = expectedAmount;

			var totalMpesaPayments = await ValidateAndCalculateMpesaPaymentsAsync(sales.PaymentDetails, sales.DispenserCode);
			if (totalMpesaPayments.ResponseCode != Response.Success)
				return ServiceResponse<object>.Information(totalMpesaPayments.ResponseMessage ?? "Invalid Mpesa Code", null);

			if (expectedAmount > totalMpesaPayments.ResponseObject)
				return ServiceResponse<object>.Information("Mpesa amount is less than expected (quantity * price)", null);

			return await FinalizeSaleAsync(sales, totalMpesaPayments.ResponseObject);
		}
		private async Task<ServiceResponse<object>> ProcessWalletPaymentsAsync(MisingSaleDto sales)
		{
			var saleId = _setups.GenerateSaleId();
			sales.PaymentDetails.ForEach(p => p.TransactionReference = saleId);

			var customerBalance = await GetCustomerBalanceAsync(sales.VehicleCode);
			if (customerBalance < QuantityAmount)
				return ServiceResponse<object>.Information("Insufficient balance", null);

			return await FinalizeSaleAsync(sales);
		}
		private async Task<ServiceResponse<object>> ProcessOtherPaymentsAsync(MisingSaleDto sales) =>
			await FinalizeSaleAsync(sales);
		private static bool ValidateSales(MisingSaleDto sales, out ServiceResponse<object> response)
		{

			response = ServiceResponse<object>.Information("Invalid sales data", null);

			return sales != null && sales.PaymentDetails.Count > 0 && !string.IsNullOrEmpty(sales.VehicleCode);
		}
		private async Task<ServiceResponse<int>> ValidateAndCalculateMpesaPaymentsAsync(IEnumerable<PaymentDetails> paymentDetails, string dispenserCode)
		{
			var totalMpesaPayments = 0;

			foreach (var payment in paymentDetails)
			{
				var validation = await ValidateMpesaPaymentAsync(payment.TransactionReference, dispenserCode);

				if (!validation.ResponseCode.Equals(Response.Success))
					return ServiceResponse<int>.Information(validation.ResponseMessage ?? "Invalid Mpesa Code", 0);

				if (validation.ResponseCode == Response.Success)
					totalMpesaPayments += validation.ResponseObject;

				mpesaCodes.Add(new MpesaCodes
				{
					TransId = payment.TransactionReference,
					Amount = validation.ResponseObject
				});
			}

			return ServiceResponse<int>.Success("Mpesa payments validated successfully.", totalMpesaPayments);
		}
		private async Task<ServiceResponse<int>> ValidateMpesaPaymentAsync(string transId, string dispenserId)
		{
			try
			{
				var sql = string.Empty;
				var tillNumber = await _context.Tills
					.Join(_context.Dispensers, t => t.TillNumber, d => d.TillNumber, (t, d) => new { t, d })
					.Where(td => td.d.DispenserCode == dispenserId)
					.Select(td => td.t.StoreNumber)
					.FirstOrDefaultAsync();

				if (tillNumber == null)
					return ServiceResponse<int>.Information($"Till number for dispenser ID {dispenserId} does not exist.", 0);

				sql = "SELECT TOP 1 TransId as Value FROM Protobase..MpesaC2BPayments WHERE BusinessShortCode = @p0 and TransId = @p1";
				var MpesaCodeExist = await _context.Set<ValueDto>()
					.FromSqlRaw(sql, tillNumber, transId)
					.Select(result => result.Value)
					.FirstOrDefaultAsync();

				if (MpesaCodeExist == null)
					return ServiceResponse<int>.Information("Either Mpesa Code does not exists, or it does not belong to that diepnser", 0);

				sql = @"SELECT TOP 1 CAST(UsageBalance as int) AS Amount FROM Protobase..MpesaC2BPayments WHERE BusinessShortCode = @p0 AND TransID = @p1";
				var mpesaAmount = await _context.Set<UsageBalanceDto>()
					.FromSqlRaw(sql, tillNumber, transId)
					.Select(result => result.Amount)
					.FirstOrDefaultAsync();


				if (mpesaAmount <= 0)
					return ServiceResponse<int>.Information($"Amount fully used for code {transId}.", 0);

				return ServiceResponse<int>.Success($"Valid Mpesa Code {transId}.", mpesaAmount);
			}
			catch (Exception ex)
			{
				return ServiceResponse<int>.Error($"An error occurred while validating payment: {ex.Message}", 0);
			}
		}

		//get employee 
		public async Task<UserAndPrice> GetEmployeeAsync(string userCode)
		{
			// Validate the input
			if (string.IsNullOrWhiteSpace(userCode))
				return new UserAndPrice(); // Return an empty object if input is invalid

			// Fetch user details
			var user = await (from d in _context.DispenserAssignments
							  join u in _context.Users on d.AttedantUserCode equals u.UserCode
							  
							  where d.AttedantUserCode == userCode
							  select new
							  {
								  Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName }),
								  Usercode = u.UserCode,
								  d.StationCode
							  }
							  ).FirstOrDefaultAsync();

			if (user == null)
				return new UserAndPrice(); // Return empty if user is not found

			// Fetch the price for the user's station
			var price = await (from p in _context.Prices
							   join s in _context.Stations on p.StationCode equals user.StationCode
							   join pp in _context.Products on p.ProductCode equals pp.ProductCode
							   where pp.ProductCode.Equals("02")
							   select p.Amount).FirstOrDefaultAsync();

			// If price is null, it defaults to 0
			var userPrice = new UserAndPrice
			{
				Price = price, // Handle null price
				UserCode = user.Usercode,
				Name = user.Name
			};

			return userPrice; // Return the valid userPrice object
		}


		private async Task<ServiceResponse<object>> FinalizeSaleAsync(MisingSaleDto sales, int? mpesaAmount = null)
		{
			var stationCode = await GetAssignedStationAsync(sales);

			if (sales.PaymentTypeCode == PaymetMethod.Mpesa || sales.PaymentTypeCode == PaymetMethod.Wallet ||
				sales.PaymentTypeCode == PaymetMethod.Compesation_Fuel || sales.PaymentTypeCode == PaymetMethod.Voucher ||
				sales.PaymentTypeCode == PaymetMethod.New_Conversions)
			{
				var vehicle = await GetVehicleAsync(sales.VehicleCode);
				var price = await GetPriceAsync(vehicle.ProductCode, stationCode);
				QuantityAmount = sales.Quantity * price;

				if (QuantityAmount < Math.Round(sales.Quantity * price) - 0.01m)
					return ServiceResponse<object>.Information("Transaction amount does not match quantity * price", null);
			}
			else
			{
				var employee = await GetEmployeeAsync(sales.VehicleCode);
				var expectedAmount = sales.Quantity * employee.Price;
				QuantityAmount = expectedAmount;

				if (QuantityAmount < Math.Round(sales.Quantity * employee.Price) - 0.01m)
					return ServiceResponse<object>.Information("Transaction amount does not match quantity * price", null);
			}

			await SaveTransactionDataAsync(sales, QuantityAmount, stationCode);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			foreach (var mpesa in sales.PaymentDetails)
			{
				if (sales.PaymentTypeCode == PaymetMethod.Mpesa)
					_salesTasks.UpdateMpesaPaymentStatus(mpesa.TransactionReference);
			}

			var paymentType = await _context.PaymentTypes
				.Where(p => p.PaymentTypeId == sales.PaymentTypeCode)
				.Select(p => p.PaymentTypeName)
				.FirstOrDefaultAsync();

			var message = $"{paymentType} Sales made by {_authentication.Name()} on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

			return ServiceResponse<object>.Success("Sales made successfully", null);
		}
		private async Task SaveTransactionDataAsync(MisingSaleDto sales, decimal transactionAmount, string stationCode)
        {
            var saleId = _setups.GenerateSaleId();
            var quantityTransaction = CreateQuantityTransaction(sales, stationCode, transactionAmount, saleId);
            _context.QuantityTransactions.Add(quantityTransaction);

			
			foreach (var pay in sales.PaymentDetails)
            {
				var payment = new PaymentDetails
				{
					TransactionAmount = QuantityAmount ,
					TransactionReference = pay.TransactionReference
				};
				var paymentTransaction = CreatePaymentTransaction(payment, saleId);
                _context.PaymentTransactions.Add(paymentTransaction);
            }

			await ReduceCustomerBalanceAsync(sales.VehicleCode, transactionAmount,saleId);
			await _context.SaveChangesAsync();
            await LogUserTrailAsync("Sales made");
        }
        private async Task<string> GetAssignedStationAsync(MisingSaleDto sale)
        {
            return await _context.Stations
                .Join(_context.Dispensers, s => s.StationCode, d => d.StationCode, (s, d) => new { s, d })
                .Where(sd => sd.d.DispenserCode == sale.DispenserCode)
                .Select(sd => sd.s.StationCode)
                .FirstOrDefaultAsync() ?? string.Empty;
        }
        private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
        {
            return await _context.Vehicles
                .Where(v => v.VehicleCode == vehicleCode)
                .Select(v => new Vehicle
                {
                    ProductCode = v.ProductCode,
                    VehicleRegistrationNumber = v.VehicleRegistrationNumber
                }).FirstOrDefaultAsync() ?? new Vehicle();
        }
        private async Task<decimal> GetPriceAsync(string productCode, string stationCode)
        {
            return await _context.Prices
                .Where(p => p.ProductCode == productCode && p.StationCode == stationCode)
                .Select(p => p.Amount)
                .FirstOrDefaultAsync();
        }
        private async Task<decimal> GetCustomerBalanceAsync(string vehicleCode)
        {
            return await _context.CustomerTransactions
                .Where(x => x.VehicleCode.Equals(vehicleCode))
                .SumAsync(x => x.Credit - x.Debit);
        }
        private async Task ReduceCustomerBalanceAsync(string vehicleCode, decimal amount,string transactionReference)
        {
            _context.CustomerTransactions.Add(new CustomerTransactions
            {
                VehicleCode = vehicleCode,
                Credit = 0,
                Debit = amount,
                DateCreated = DateTime.UtcNow,
				TransactionReference = transactionReference,
				UserCode = _authentication.Usercode(),
            });
            await _context.SaveChangesAsync();
        }
        private QuantityTransactions CreateQuantityTransaction(MisingSaleDto sales, string stationCode, decimal transactionAmount, string saleId)
        {
			var EmployeePrice = GetEmployeeAsync(sales.VehicleCode).Result;

			return new QuantityTransactions
            {
                ShiftNumber = sales.ShiftNumber,
                UserCode = _authentication.Usercode(),
                VehicleCode = sales.VehicleCode,
                QuantityCredit = sales.Quantity,
                DispenserCode = sales.DispenserCode,
                NozzleCode = sales.NozzleCode,
                AmountCredit = transactionAmount,
                DateCreated = DateTime.UtcNow,
                IsReversed = false,
                PaymentTypeCode = sales.PaymentTypeCode,
                SaleId = saleId,
                Price = GetPriceAsync(sales.VehicleCode, stationCode).Result == 0 ? EmployeePrice.Price : GetPriceAsync(sales.VehicleCode, stationCode).Result,
                StationCode = stationCode
            };
        }
        private PaymentTransactions CreatePaymentTransaction(PaymentDetails payment, string saleId)
        {
            return new PaymentTransactions
            {
                PaymentRefrence = payment.TransactionReference,
                TransactionAmount = payment.TransactionAmount,
                DateCreated = DateTime.UtcNow,
                UserCode = _authentication.Usercode(),
                SaleId = saleId,
				TransactionAmountDebit = 0
            };
        }
		private List<MpesaCodes> ValidateAmount()
		{
			//The aim is to make sure that the amount in mpesacodes is equal to the amount in the sales
			var ActualAmount = QuantityAmount;
			foreach (var s in mpesaCodes)
			{
				if (ActualAmount != 0)
				{
					if (ActualAmount - s.Amount > 0)
					{
						s.Amount = ActualAmount;
						ActualAmount -= s.Amount;
					}
					if (ActualAmount - s.Amount < 0)
					{
						s.Amount = (ActualAmount - s.Amount);
						ActualAmount = s.Amount;
					}
					if (ActualAmount - s.Amount == 0)
					{
						s.Amount = ActualAmount;
						ActualAmount = 0;
					}
				}
			}
			return  mpesaCodes;
		}

		private async Task LogUserTrailAsync(string action)
        {
            var message = $"{action} by {_authentication.Name()} on {DateTime.UtcNow}";
            await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
        }
        public async Task<ServiceResponse> DeferVariance(string shiftNumber)
        {
            var shift = await (from s in _context.Shifts
                               where s.ShiftNumber.Equals(shiftNumber)
                               select s).FirstOrDefaultAsync();

            var stocksummary = await (from s in _context.StockTakeSummaries
                                      where s.ShiftNumber.Equals(shiftNumber)
                                      select s).ToListAsync();
            if (shift is not null && stocksummary is not null)
            {
                shift.ShiftStatus = ShiftStatus.Pending;
                _context.Update(shift);

                foreach (var s in stocksummary)
                {
                    s.VarianceStatus = ShiftStatus.Pending;
                    _context.Update(s);
                }
                await _context.SaveChangesAsync();

				var message = $"Variance of shift {shift.ShiftNumber} has been defered untill next shift by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Variance has been defered untill next shift");
            }

            return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
        }
        public async Task<ServiceResponse> OffWriteVariance(string shiftNumber)
        {
            var shift = await (from s in _context.Shifts
                               where s.ShiftNumber.Equals(shiftNumber)
                               select s).FirstOrDefaultAsync();

            var stocksummary = await (from s in _context.StockTakeSummaries
                                      where s.ShiftNumber.Equals(shiftNumber)
                                      select s).ToListAsync();

            if (shift is not null && stocksummary is not null)
            {
                shift.ShiftStatus = ShiftStatus.Closed;
                _context.Update(shift);

                foreach (var s in stocksummary)
                {
                    s.VarianceStatus = ShiftStatus.Closed;
                    _context.Update(s);
                }
                await _context.SaveChangesAsync();

				var message = $"Variance of amount {stocksummary.Sum(x => x.ClosingVariance)} written off by {_authentication.Name()} on {DateTime.UtcNow}";
				return ServiceResponse<object>.Success("Variance has been offwritten");
            }

            return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
        }
        #endregion
    }
}