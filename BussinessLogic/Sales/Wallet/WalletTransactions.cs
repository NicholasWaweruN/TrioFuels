namespace BusinessLogic.Sales.Wallet
{
	using BusinessLogic.Messaging;
	using BussinessLogic.Authentication.CommonTasks;
	using BussinessLogic.Messaging;
	using BussinessLogic.Sales.Wallet;
	using BussinessLogic.Setup;
	using ClosedXML.Excel;
	using DataAccessLayer.Common;
	using DataAccessLayer.Context;
	using DataAccessLayer.DTOs.Transactions;
	using DataAccessLayer.EntityModels.Customer;
	using DataAccessLayer.EntityModels.SetUps;
	using DataAccessLayer.EntityModels.Transactions;
	using DataAccessLayer.Helpers;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Caching.Memory;
	using System.Reflection;
	using static DataAccessLayer.EntityModels.Wallet.WalletDto;

	/// <summary>
	/// Defines the <see cref="WalletTransactions" />
	/// </summary>
	public class WalletTransactions : IWalletTransactions
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly OTOContext _context;
		private readonly IAfricaIsTalking _africaIsTalking;
		private readonly IEmailService _emailService;
		private readonly IMemoryCache _cache;

		public WalletTransactions(IAuthCommonTasks authentication, ICommonSetups setups, OTOContext context, IAfricaIsTalking africaIsTalking, IEmailService emailService, IMemoryCache cache)
		{
			_authentication = authentication;
			_setups = setups;
			_context = context;
			_africaIsTalking = africaIsTalking;
			_emailService = emailService;
			_cache = cache;
		}

		#region Deposits & Withdrawals

		/// <summary>
		/// Deposits funds into a customer's wallet (credit). This is the counterpart to <see cref="WithdrawFromCustomerWallet"/>.
		/// </summary>
		public async Task<ServiceResponse> TopUpCustomerWallet(TopUpCustomerWalletDto topUpCustomerWalletDto)
		{
			if (string.IsNullOrEmpty(topUpCustomerWalletDto.VehicleCode))
				return ServiceResponse<object>.Information("Kindly provide the vehicle registration number");

			if (topUpCustomerWalletDto.Amount <= 0)
				return ServiceResponse<object>.Information("Deposit amount must be greater than zero");

			var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == topUpCustomerWalletDto.VehicleCode);
			if (vehicle is null)
				return ServiceResponse<object>.Information("Vehicle does not exist");

		
			var customer = await GetCustomerDetailsAsync(topUpCustomerWalletDto.VehicleCode);
			if (customer is null)
				return ServiceResponse<object>.Information("Customer Not Found");

			var transaction = CreateCustomerTransaction(topUpCustomerWalletDto.VehicleCode, topUpCustomerWalletDto.Amount, 0, topUpCustomerWalletDto.TransactionReference, topUpCustomerWalletDto.PaymentType, "Wallet deposit");

			var saveResult = await SaveTransactionAsync(transaction);
			if (saveResult.ResponseCode == Response.Error)
				return saveResult;

			var newBalance = await GetCustomerBalance(topUpCustomerWalletDto.VehicleCode);
			var firstName = customer.CustomerName.Split(' ')[0];

			await _africaIsTalking.SendSms(customer.CustomerPhone,
				$"Dear {firstName}, your wallet has been topped up with {topUpCustomerWalletDto.Amount:N2} ksh on {DateTime.UtcNow:dd/MM/yy} at {DateTime.UtcNow:hh:mm tt}. Your new balance is {newBalance.ResponseObject:N2} ksh. Thank you for choosing Otogas.");

			var message = $"{_authentication.Name()} has topped up {vehicle.VehicleRegistrationNumber} with {topUpCustomerWalletDto.Amount:N2} ksh on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

			return ServiceResponse<object>.Success($"Customer Wallet Topped Up with {topUpCustomerWalletDto.Amount:N2}");
		}

		/// <summary>
		/// Withdraws (debits) funds from a customer's wallet. Fails if the wallet does not have sufficient balance.
		/// NOTE: balance check + insert are two separate round trips. Under high concurrency two simultaneous
		/// withdrawals on the same wallet could both pass the check before either commits. If that's a real risk
		/// for you, wrap this in a serializable transaction or add a Postgres CHECK/trigger that rejects a resulting
		/// negative balance as a backstop.
		/// </summary>
		public async Task<ServiceResponse> WithdrawFromCustomerWallet(WithdrawCustomerWalletDto withdrawDto)
		{
			if (string.IsNullOrEmpty(withdrawDto.VehicleCode))
				return ServiceResponse<object>.Information("Kindly provide the vehicle registration number");

			if (withdrawDto.Amount <= 0)
				return ServiceResponse<object>.Information("Withdrawal amount must be greater than zero");

			var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == withdrawDto.VehicleCode);
			if (vehicle is null)
				return ServiceResponse<object>.Information("Vehicle does not exist");

			var customer = await GetCustomerDetailsAsync(withdrawDto.VehicleCode);
			if (customer is null)
				return ServiceResponse<object>.Information("Customer Not Found");

			var balanceResponse = await GetCustomerBalance(withdrawDto.VehicleCode);
			if (balanceResponse.ResponseCode == Response.Error)
				return ServiceResponse<object>.Error("Could not verify wallet balance, please try again");

			if (balanceResponse.ResponseObject < withdrawDto.Amount)
				return ServiceResponse<object>.Information($"Insufficient wallet balance. Available balance is {balanceResponse.ResponseObject:N2} ksh");

			var transaction = CreateCustomerTransaction(withdrawDto.VehicleCode, 0, withdrawDto.Amount, withdrawDto.TransactionReference, withdrawDto.WithdrawalType, withdrawDto.Narration ?? "Wallet withdrawal");

			var saveResult = await SaveTransactionAsync(transaction);
			if (saveResult.ResponseCode == Response.Error)
				return saveResult;

			var newBalance = balanceResponse.ResponseObject - withdrawDto.Amount;
			var firstName = customer.CustomerName.Split(' ')[0];

			await _africaIsTalking.SendSms(customer.CustomerPhone,
				$"Dear {firstName}, {withdrawDto.Amount:N2} ksh has been withdrawn from your wallet on {DateTime.UtcNow:dd/MM/yy} at {DateTime.UtcNow:hh:mm tt}. Your new balance is {newBalance:N2} ksh. Thank you for choosing Otogas.");

			var message = $"{_authentication.Name()} withdrew {withdrawDto.Amount:N2} ksh from {vehicle.VehicleRegistrationNumber} on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

			return ServiceResponse<object>.Success($"Customer Wallet Debited with {withdrawDto.Amount:N2}");
		}

		/// <summary>
		/// Transfers balance between two vehicle wallets. Validates the source wallet has sufficient funds first.
		/// </summary>
		public async Task<ServiceResponse> TransferCustomerBalance(TransferCustomerBalanceDto transferCustomerBalanceDto)
		{
			if (string.IsNullOrEmpty(transferCustomerBalanceDto.FromVehicleCode) || string.IsNullOrEmpty(transferCustomerBalanceDto.ToVehicleCode))
				return ServiceResponse<object>.Information("Kindly provide both the from and to vehicle registration numbers");

			if (transferCustomerBalanceDto.Amount <= 0)
				return ServiceResponse<object>.Information("Transfer amount must be greater than zero");

			var fromVehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == transferCustomerBalanceDto.FromVehicleCode);
			if (fromVehicle is null)
				return ServiceResponse<object>.Information("From vehicle does not exist");

			var toVehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == transferCustomerBalanceDto.ToVehicleCode);
			if (toVehicle is null)
				return ServiceResponse<object>.Information("To vehicle does not exist");

			var fromBalance = await GetCustomerBalance(transferCustomerBalanceDto.FromVehicleCode);
			if (fromBalance.ResponseObject < transferCustomerBalanceDto.Amount)
				return ServiceResponse<object>.Information($"Insufficient balance on {fromVehicle.VehicleRegistrationNumber} to complete this transfer");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var narration = $"{transferCustomerBalanceDto.Amount:N2} transferred from vehicle {fromVehicle.VehicleRegistrationNumber} to {toVehicle.VehicleRegistrationNumber}";

				var fromTransaction = CreateCustomerTransaction(transferCustomerBalanceDto.FromVehicleCode, 0, transferCustomerBalanceDto.Amount, "", 5, narration);
				var toTransaction = CreateCustomerTransaction(transferCustomerBalanceDto.ToVehicleCode, transferCustomerBalanceDto.Amount, 0, "", 5, narration);

				await _context.CustomerTransactions.AddRangeAsync(fromTransaction, toTransaction);
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				var message = $"{_authentication.Name()} transferred {transferCustomerBalanceDto.Amount:N2} from {transferCustomerBalanceDto.FromVehicleCode} to {transferCustomerBalanceDto.ToVehicleCode} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Customer Balance Transferred Successfully");
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				await LogErrorAsync(ex);
				return ServiceResponse<object>.Error("An error occurred while transferring the balance: " + ex.Message);
			}
		}

		#endregion

		#region Balances & History

		/// <summary>
		/// Current wallet balance (sum of credits minus debits) for a single vehicle.
		/// </summary>
		public async Task<ServiceResponse<decimal>> GetCustomerBalance(string vehicleCode)
		{
			try
			{
				var balance = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode)
					.SumAsync(x => x.Credit - x.Debit);

				return ServiceResponse<decimal>.Success("Balance Found", balance);
			}
			catch (Exception ex)
			{
				return ServiceResponse<decimal>.Error(ex.Message, 0);
			}
		}

		/// <summary>
		/// Paginated, filterable wallet balances per vehicle (LINQ only, no stored procedure).
		/// </summary>
		public async Task<ServiceResponse<object>> GetCustomerBalances(string? registrationNumber = null, string? customerName = null, int pageNumber = 1, int pageSize = 15)
		{
			try
			{
				var query = _context.CustomerTransactions
					.Join(_context.Vehicles, ct => ct.VehicleCode, v => v.VehicleCode, (ct, v) => new { ct, v })
					.Join(_context.Customers, cv => cv.v.CustomerCode, c => c.CustomerCode, (cv, c) => new
					{
						c.CustomerCode,
						c.CustomerName,
						cv.v.VehicleCode,
						RegistrationNumber = cv.v.VehicleRegistrationNumber,
						cv.ct.Credit,
						cv.ct.Debit
					})
					.GroupBy(g => new { g.CustomerCode, g.CustomerName, g.VehicleCode, g.RegistrationNumber })
					.Select(group => new CustomerBalanceDto
					{
						CustomerCode = group.Key.CustomerCode,
						CustomerName = group.Key.CustomerName,
						VehicleCode = group.Key.VehicleCode,
						RegistrationNumber = group.Key.RegistrationNumber,
						Balance = group.Sum(x => x.Credit) - group.Sum(x => x.Debit)
					});

				if (!string.IsNullOrEmpty(registrationNumber))
					query = query.Where(q => q.RegistrationNumber.Contains(registrationNumber));

				if (!string.IsNullOrEmpty(customerName))
					query = query.Where(q => q.CustomerName.Contains(customerName));

				var totalRecords = await query.CountAsync();

				var pagedBalances = await query
					.OrderBy(q => q.CustomerName)
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				var pagedResult = new
				{
					TotalRecords = totalRecords,
					PageNumber = pageNumber,
					PageSize = pageSize,
					Sales = pagedBalances
				};

				return ServiceResponse<object>.Success("Balances Found", pagedResult);
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<object>.Error("Something went wrong, contact system admin", ex);
			}
		}

		/// <summary>
		/// All wallet transactions for a vehicle registration number, with a running balance.
		/// </summary>
		public async Task<ServiceResponse<object>> WalletHistories(string vRegno)
		{
			try
			{
				var history = await (from ct in _context.CustomerTransactions
									 join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
									 join c in _context.Customers on v.CustomerCode equals c.CustomerCode
									 where v.VehicleRegistrationNumber == vRegno
									 orderby c.CustomerCode, v.VehicleRegistrationNumber, ct.DateCreated
									 select new
									 {
										 c.CustomerName,
										 c.CustomerPhone,
										 v.VehicleRegistrationNumber,
										 ct.TransactionReference,
										 ct.DateCreated,
										 ct.Credit,
										 ct.Debit
									 }).ToListAsync();

				var runningBalanceHistory = history
					.GroupBy(x => new { x.CustomerName, x.VehicleRegistrationNumber })
					.SelectMany(group =>
					{
						decimal runningBalance = 0;
						return group.Select(item =>
						{
							runningBalance += item.Credit - item.Debit;
							return new
							{
								item.CustomerName,
								item.CustomerPhone,
								item.VehicleRegistrationNumber,
								item.TransactionReference,
								item.DateCreated,
								item.Credit,
								item.Debit,
								RunningBalance = runningBalance
							};
						});
					})
					.ToList();

				if (runningBalanceHistory.Count != 0)
					return ServiceResponse<object>.Success("Customer transactions", runningBalanceHistory);

				return ServiceResponse<object>.Information("No records found", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An Error Occured", ex.Message);
			}
		}

		/// <summary>
		/// Wallet statement for a single vehicle over a date range, with an opening balance line.
		/// </summary>
		public async Task<ServiceResponse<List<CustomerTransactionDto>>> GetCustomerStatement(string vehicleCode, DateTime startDate, DateTime endDate)
		{
			try
			{
				var vehicleExists = await _context.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
				if (!vehicleExists)
					return ServiceResponse<List<CustomerTransactionDto>>.Information("Vehicle does not exist", null);

				var startingBalance = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode && x.DateCreated < startDate)
					.SumAsync(x => x.Credit - x.Debit);

				var transactions = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode && x.DateCreated >= startDate && x.DateCreated <= endDate)
					.OrderBy(x => x.DateCreated)
					.ToListAsync();

				var runningBalance = startingBalance;

				var statement = new List<CustomerTransactionDto>
				{
					new()
					{
						DateCreated = startDate,
						Description = "Balance before this period",
						Credit = 0,
						Debit = 0,
						RunningBalance = runningBalance
					}
				};

				statement.AddRange(transactions.Select(x =>
				{
					runningBalance += x.Credit - x.Debit;
					return new CustomerTransactionDto
					{
						DateCreated = x.DateCreated,
						Description = x.TransactionReference,
						Credit = x.Credit,
						Debit = x.Debit,
						UserCode = _authentication.Usercode(),
						RunningBalance = runningBalance
					};
				}));

				return ServiceResponse<List<CustomerTransactionDto>>.Success("Statement Found", statement);
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<List<CustomerTransactionDto>>.Error(ex.Message, null);
			}
		}

		/// <summary>
		/// All deposit transactions (Credit column) recorded for a vehicle.
		/// </summary>
		public async Task<ServiceResponse<object>> GetCustomerPayments(string vehicleCode)
		{
			try
			{
				var transactions = await (from ct in _context.CustomerTransactions
										  join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
										  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
										  where v.VehicleCode == vehicleCode
										  select new
										  {
											  c.CustomerName,
											  c.CustomerPhone,
											  v.VehicleRegistrationNumber,
											  ct.TransactionReference,
											  ct.DateCreated,
											  Payments = ct.Credit
										  }).ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No transactions found for the specified vehicle", null);

				return ServiceResponse<object>.Success("Transactions found", transactions);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while fetching transactions", ex);
			}
		}

		#endregion

		#region Batch Operations

		/// <summary>
		/// Batch-credits multiple vehicle wallets from an uploaded Excel file (column 1 = amount, column 2 = vehicle reg no).
		/// </summary>
		public async Task<ServiceResponse<object>> UploadCustomerTransactions(IFormFile file, int topUpType)
		{
			_context.Database.SetCommandTimeout(600);

			if (file == null || file.Length <= 0)
				return ServiceResponse<object>.Information("File is empty!", null);

			if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
				return ServiceResponse<object>.Information("Invalid file format. Please upload an Excel file. .xlsx", null);

			var failedTransactions = new List<FailedTransactions>();
			var customerTransactions = new List<CustomerTransactions>();

			using var stream = new MemoryStream();
			await file.CopyToAsync(stream);
			using var workbook = new XLWorkbook(stream);
			var worksheet = workbook.Worksheet(1);
			var rows = worksheet.RowsUsed().Skip(1);

			var vehicleRegNos = rows.Select(row => row.Cell(2).GetValue<string>().Replace(" ", "").ToUpper()).Distinct().ToList();

			var existingVehicles = await _context.Vehicles
				.AsNoTracking()
				.Where(v => vehicleRegNos.Contains(v.VehicleRegistrationNumber.Replace(" ", "").ToUpper()))
				.ToDictionaryAsync(v => v.VehicleRegistrationNumber.Replace(" ", "").ToUpper(), v => v.VehicleCode);

			var batchNo = _setups.GenerateSaleId();

			foreach (var row in rows)
			{
				var saleId = _setups.GenerateSaleId();
				decimal amount = row.Cell(1).GetValue<decimal>();
				string vehicleRegNo = row.Cell(2).GetValue<string>().Replace(" ", "").ToUpper();

				if (!existingVehicles.TryGetValue(vehicleRegNo, out var vehicleCode))
				{
					failedTransactions.Add(new FailedTransactions
					{
						Amount = amount,
						RegNo = vehicleRegNo,
						DateCreated = EatTime.Now,
						UserCode = _authentication.Usercode()
					});
					continue;
				}

				customerTransactions.Add(new CustomerTransactions
				{
					Credit = amount,
					DateCreated = EatTime.Now,
					Debit = 0,
					TransactionReference = saleId,
					UserCode = _authentication.Usercode(),
					UserReference = saleId,
					VehicleCode = vehicleCode,
					Narration = "Batch Credit Upload",
					TopUpType = topUpType,
					BatchNumber = batchNo
				});

				var phoneNumber = await (from v in _context.Vehicles
										 where v.VehicleCode.Equals(vehicleCode)
										 select v.PhoneNumber).FirstAsync();

				var smsMessage = $"Congrats! 🎉 You have been awarded a fuel voucher No: {saleId} ⛽ Amount: {amount} Redeem at any of our OTOGas Stations.";
				await _africaIsTalking.SendSms(phoneNumber, smsMessage);
			}

			if (customerTransactions.Count != 0)
				await _context.CustomerTransactions.AddRangeAsync(customerTransactions);

			if (failedTransactions.Count != 0)
				await _context.FailedTransactions.AddRangeAsync(failedTransactions);

			await _context.SaveChangesAsync();

			string message = failedTransactions.Count != 0
				? "Some transactions uploaded successfully, but some failed due to missing vehicles."
				: "All transactions uploaded successfully.";

			return ServiceResponse<object>.Success(message, failedTransactions.Count != 0 ? (object)failedTransactions : null);
		}

		/// <summary>
		/// Manual top-up of a separate "customer funds" pool (distinct from the vehicle wallet ledger).
		/// </summary>
		public async Task<ServiceResponse> TopUpFundssWallet(TopUpFundsDto customerFunds)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer does not exist");

				var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Customer does not have a vehicle");

				var customerFund = new CustomerFunds
				{
					CustomerCode = customerFunds.CustomerCode,
					Credit = customerFunds.Amount,
					SystemReference = _setups.GenerateSaleId(),
					Debit = 0,
					UserCode = _authentication.Usercode(),
					Narration = "Funds top up",
					DateCreated = EatTime.Now,
					UserReference = customerFunds.TransactionReference
				};

				_context.CustomerFunds.Add(customerFund);
				await _context.SaveChangesAsync();

				var message = $"Customer funds topped up by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Customer funds topped up successfully");
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<object>.Error("An error occurred while topping up customer funds");
			}
		}

		/// <summary>
		/// Reverses a previous "customer funds" top-up.
		/// </summary>
		public async Task<ServiceResponse> ReverseTopUpFundssWallet(TopUpFundsDto customerFunds)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer does not exist");

				var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Customer does not have a vehicle");

				var customerFund = new CustomerFunds
				{
					CustomerCode = customerFunds.CustomerCode,
					Debit = customerFunds.Amount,
					SystemReference = _setups.GenerateSaleId(),
					Credit = 0,
					UserCode = _authentication.Usercode(),
					Narration = "Funds reversed",
					DateCreated = EatTime.Now,
					UserReference = customerFunds.TransactionReference
				};

				_context.CustomerFunds.Add(customerFund);
				await _context.SaveChangesAsync();

				var message = $"Customer funds reversed by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Customer funds reversed successfully");
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<object>.Error("An error occurred while reversing customer funds");
			}
		}

		/// <summary>
		/// The TopUpTypes
		/// </summary>
		public async Task<ServiceResponse<List<TopUpTypesDto>>> TopUpTypes()
		{
			var paymentTypes = await _context.TopUpTypes
				.Select(x => new TopUpTypesDto
				{
					TopUpTypeId = x.TopUpType,
					TopUpTypeName = x.TopUpDescription
				}).ToListAsync();

			return ServiceResponse<List<TopUpTypesDto>>.Success("Top Up types retrieved successfully", paymentTypes);
		}

		#endregion

		#region Statements & Exports (ClosedXML only)

		/// <summary>
		/// Exports a single vehicle's full wallet transaction history to a password-protected Excel workbook.
		/// </summary>
		public async Task<ServiceResponse<byte[]>> ExportCustomerTransactions(string vehicleCode)
		{
			try
			{
				var isVehicleExist = await _context.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
				if (!isVehicleExist)
					return ServiceResponse<byte[]>.Information("Vehicle does not exist", null);

				var customer = await (from v in _context.Vehicles
									  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
									  where v.VehicleCode == vehicleCode
									  select new { c.CustomerName, v.VehicleRegistrationNumber, c.CustomerPhone }).FirstOrDefaultAsync();
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer of the vehicle not found", null);

				var transactions = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode)
					.OrderBy(x => x.DateCreated)
					.ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified vehicle", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add(customer.VehicleRegistrationNumber);

				ApplyTitleStyle(worksheet, "Wallet Statement", "A1:F1");

				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Vehicle Registration:";
				worksheet.Cell(4, 2).Value = customer.VehicleRegistrationNumber;
				StyleDetailsBlock(worksheet.Range("A2:B4"));

				string[] headers = { "Transaction Reference", "Date Created", "Credit", "Debit", "Running Balance" };
				for (int i = 0; i < headers.Length; i++)
					worksheet.Cell(6, i + 1).Value = headers[i];
				StyleHeaderRow(worksheet.Range("A6:E6"));

				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 1).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 2).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 3).Value = transactions[i].Credit;
					worksheet.Cell(row, 4).Value = transactions[i].Debit;
					worksheet.Cell(row, 5).Value = runningBalance;

					worksheet.Cell(row, 2).Style.DateFormat.Format = "yyyy-mm-dd";
					worksheet.Range(row, 3, row, 5).Style.NumberFormat.Format = "#,##0.00";

					ApplyRowStyle(worksheet, row, i, 5);
				}

				var lastRow = transactions.Count + 7;
				worksheet.Cell(lastRow, 4).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 5).Value = runningBalance;
				StyleTotalRow(worksheet.Range(lastRow, 4, lastRow, 5));
				worksheet.Cell(lastRow, 5).Style.NumberFormat.Format = "#,##0.00";

				worksheet.Columns().AdjustToContents();
				worksheet.SheetView.FreezeRows(6);
				workbook.Protect(vehicleCode);

				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var content = stream.ToArray();

				var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		/// <summary>
		/// Exports a customer-level wallet statement (across all their vehicles) from a given start date, including
		/// an opening balance line, to Excel.
		/// </summary>
		public async Task<ServiceResponse<byte[]>> CustomerStatement(string customerCode, DateTime from)
		{
			try
			{
				var customer = await GetCustomerByCodeAsync(customerCode);
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer not found", null);

				var transactions = (await GetCustomerTransactionsAsync(customerCode, from))
					.OrderBy(x => x.DateCreated)
					.ToList();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add(customer.CustomerName);

				ApplyTitleStyle(worksheet, "Wallet Statement", "A1:G1");

				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Customer Email:";
				worksheet.Cell(4, 2).Value = customer.CustomerEmail;
				StyleDetailsBlock(worksheet.Range("A2:B4"));

				string[] headers = { "Row", "Registration Number", "Transaction Reference", "Date Created", "Credit", "Debit", "Running Balance" };
				for (int i = 0; i < headers.Length; i++)
					worksheet.Cell(6, i + 1).Value = headers[i];
				StyleHeaderRow(worksheet.Range("A6:G6"));

				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 1).Value = i + 1;
					worksheet.Cell(row, 2).Value = transactions[i].VehicleRegistrationNumber;
					worksheet.Cell(row, 3).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 4).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 5).Value = transactions[i].Credit;
					worksheet.Cell(row, 6).Value = transactions[i].Debit;
					worksheet.Cell(row, 7).Value = runningBalance;

					worksheet.Cell(row, 4).Style.DateFormat.Format = "yyyy-mm-dd";
					worksheet.Range(row, 5, row, 7).Style.NumberFormat.Format = "#,##0.00";

					ApplyRowStyle(worksheet, row, i, 7);
				}

				var lastRow = transactions.Count + 7;
				worksheet.Cell(lastRow, 6).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 7).Value = runningBalance;
				StyleTotalRow(worksheet.Range(lastRow, 6, lastRow, 7));
				worksheet.Cell(lastRow, 7).Style.NumberFormat.Format = "#,##0.00";

				worksheet.Columns().AdjustToContents();
				worksheet.SheetView.FreezeRows(6);

				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var content = stream.ToArray();

				var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				await LogErrorAsync(ex);
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		#endregion

		#region Private Helpers

		private CustomerTransactions CreateCustomerTransaction(string vehicleCode, decimal credit, decimal debit, string reference, int topUpType, string narration)
		{
			return new CustomerTransactions
			{
				DateCreated = EatTime.Now,
				UserCode = _authentication.Usercode(),
				VehicleCode = vehicleCode,
				TransactionReference = _setups.GenerateSaleId(),
				Credit = credit,
				Debit = debit,
				UserReference = reference,
				Narration = narration,
				TopUpType = topUpType
			};
		}

		private async Task<ServiceResponse> SaveTransactionAsync(CustomerTransactions transaction)
		{
			try
			{
				await _context.CustomerTransactions.AddAsync(transaction);
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("Transaction saved successfully");
			}
			catch (Exception ex)
			{
				return new ServiceResponse
				{
					ResponseMessage = ex.Message,
					ResponseCode = Response.Error
				};
			}
		}

	

		/// <summary>
		/// Cached lookup of a customer's details via their vehicle code (10 minute cache).
		/// </summary>
		private async Task<Customer?> GetCustomerDetailsAsync(string vehicleCode)
		{
			if (_cache.TryGetValue(vehicleCode, out Customer? cached))
				return cached;

			var customer = await (from v in _context.Vehicles
								  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
								  where v.VehicleCode == vehicleCode
								  select new Customer
								  {
									  CustomerCode = c.CustomerCode,
									  CustomerName = c.CustomerName,
									  CustomerPhone = v.PhoneNumber,
									  DateCreated = c.DateCreated,
									  CustomerEmail = c.CustomerEmail,
									  IdentificationNumber = c.IdentificationNumber
								  }).FirstOrDefaultAsync();

			if (customer != null)
				_cache.Set(vehicleCode, customer, TimeSpan.FromMinutes(10));

			return customer;
		}

		private async Task<Customer> GetCustomerByCodeAsync(string customerCode)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerCode == customerCode) ?? new Customer();
		}

		private async Task<List<TransactionDto>> GetCustomerTransactionsAsync(string customerCode, DateTime from)
		{
			var balanceBefore = await _context.CustomerTransactions
				.Where(x => x.VehicleCode == customerCode && x.DateCreated.Date <= from.Date)
				.SumAsync(x => x.Credit - x.Debit);

			var transactions = await (from c in _context.CustomerTransactions
									  join v in _context.Vehicles on c.VehicleCode equals v.VehicleCode
									  where v.CustomerCode == customerCode
									  select new TransactionDto
									  {
										  VehicleRegistrationNumber = v.VehicleRegistrationNumber,
										  TransactionReference = c.TransactionReference,
										  DateCreated = c.DateCreated,
										  Credit = c.Credit,
										  Debit = c.Debit
									  }).ToListAsync();

			if (balanceBefore != 0)
			{
				transactions.Add(new TransactionDto
				{
					VehicleRegistrationNumber = $"Balance Before {from:yyyy-MMMM-dd}",
					TransactionReference = _setups.GenerateSaleId(),
					DateCreated = from,
					Credit = balanceBefore,
					Debit = 0
				});
			}

			return transactions;
		}

		private static void ApplyTitleStyle(IXLWorksheet worksheet, string title, string range)
		{
			var titleRange = worksheet.Range(range);
			titleRange.Merge().Value = title;
			titleRange.Style.Font.Bold = true;
			titleRange.Style.Font.FontSize = 18;
			titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			titleRange.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
			titleRange.Style.Font.FontColor = XLColor.White;
			titleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
			titleRange.Style.Border.OutsideBorderColor = XLColor.DarkBlue;
		}

		private static void StyleDetailsBlock(IXLRange range)
		{
			range.Style.Font.Bold = true;
			range.Style.Fill.BackgroundColor = XLColor.LightGray;
			range.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
			range.Style.Border.OutsideBorderColor = XLColor.Black;
			range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
		}

		private static void StyleHeaderRow(IXLRange range)
		{
			range.Style.Font.Bold = true;
			range.Style.Font.FontColor = XLColor.White;
			range.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
			range.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
			range.Style.Border.BottomBorderColor = XLColor.DarkBlue;
			range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		}

		private static void ApplyRowStyle(IXLWorksheet worksheet, int row, int index, int lastColumn)
		{
			var rowRange = worksheet.Range(row, 1, row, lastColumn);
			if (index % 2 == 0)
				rowRange.Style.Fill.BackgroundColor = XLColor.LightCyan;

			rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
			rowRange.Style.Border.OutsideBorderColor = XLColor.LightGray;
		}

		private static void StyleTotalRow(IXLRange range)
		{
			range.Style.Font.Bold = true;
			range.Style.Fill.BackgroundColor = XLColor.PaleGoldenrod;
			range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
			range.Style.Border.OutsideBorderColor = XLColor.DarkGreen;
		}

		private async Task LogErrorAsync(Exception ex)
		{
			var method = ex.TargetSite;
			await _authentication.ErrorTrail(new ErrorTrail
			{
				DateCreated = EatTime.Now,
				ErrorCode = "004",
				ErrorMessage = ex.Message,
				Method = method is null ? "" : method.Name
			});
		}





		#endregion
	}

	public class WithdrawCustomerWalletDto
	{
		public string VehicleCode { get; set; } = string.Empty;
		public decimal Amount { get; set; }
		public string TransactionReference { get; set; } = string.Empty;
		public int WithdrawalType { get; set; }
		public string? Narration { get; set; }
	}

	public class PaymentTypeDto
	{
		public int PaymentTypeCode { get; set; }
		public string PaymentTypeName { get; set; } = string.Empty;
	}

	public class TopUpTypesDto
	{
		public int TopUpTypeId { get; set; }
		public string TopUpTypeName { get; set; } = string.Empty;
	}

	public class TransactionDto
	{
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; }

		[Precision(18, 2)]
		public decimal Credit { get; set; }

		[Precision(18, 2)]
		public decimal Debit { get; set; }

		[Precision(18, 2)]
		public decimal Balance { get; set; }

		[Precision(18, 2)]
		public decimal RunningBalance { get; set; }

		public string TransactionReference { get; set; } = string.Empty;
	}
}