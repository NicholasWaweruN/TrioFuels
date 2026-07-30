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
	using System.Text.RegularExpressions;
	using static DataAccessLayer.EntityModels.Wallet.WalletDto;

	/// <summary>
	/// Defines the <see cref="WalletTransactions" />
	/// The wallet belongs to the CUSTOMER, not the vehicle. A customer may have several vehicles;
	/// all of them draw from and top up the same single balance. VehicleCode on a transaction row
	/// is kept only to record which vehicle was used for that particular transaction.
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
		/// Deposits funds into a customer's wallet (credit). VehicleCode identifies which vehicle
		/// was used to make the deposit, but the balance itself lives on the customer (resolved
		/// from the vehicle). This is the counterpart to <see cref="WithdrawFromCustomerWallet"/>.
		/// </summary>
		public async Task<ServiceResponse<object>> TopUpCustomerWalletAsync(TopUpCustomerWalletDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.CustomerCode))
				return ServiceResponse<object>.Information("Customer code is required", null);

			if (string.IsNullOrWhiteSpace(dto.TransactionReference))
				return ServiceResponse<object>.Information("Transaction reference is required", null);

			if (dto.Amount <= 0)
				return ServiceResponse<object>.Information("Deposit amount must be greater than zero", null);

			// ⚠️ ASSUMPTION: swapped the vehicle-code lookup for a customer-code one,
			// same as before — confirm this is really how you fetch a customer elsewhere.
			var customer = await GetCustomerDetailsByCustomerCodeAsync(dto.CustomerCode);
			if (customer is null)
				return ServiceResponse<object>.Information("Customer not found", null);

			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();

				try
				{
					decimal amountToCredit = dto.Amount;

					// ---- Mpesa validation happens BEFORE we stage the wallet row,
					// exactly like RepayCreditAsync: a bad code never gets near the ledger. ----
					if (dto.PaymentType == 2)
					{
						var mpesaTx = await _context.MpesaTransactions
							.FromSqlInterpolated($@"
                        SELECT * FROM ""MpesaTransactions""
                        WHERE ""TransID"" = {dto.TransactionReference}
                        FOR UPDATE")
							.FirstOrDefaultAsync();

						if (mpesaTx is null)
						{
							await tx.RollbackAsync();
							return ServiceResponse<object>.Information($"Mpesa code {dto.TransactionReference} does not exist", null);
						}

						var (error, fullBalance) = ValidateAndConsumeMpesa(mpesaTx, mpesaTx.TillNumber);

						if (error is not null)
						{
							await tx.RollbackAsync();
							return error;
						}

						// Same rule as credit repayment: ValidateAndConsumeMpesa always
						// consumes the FULL balance, so we credit the wallet for that
						// same full amount rather than dto.Amount, to keep the two
						// ledgers from drifting apart.
						amountToCredit = fullBalance;
					}

					// ⚠️ ASSUMPTION: CreateCustomerTransaction's vehicle-code param can be
					// string.Empty for a wallet top-up not tied to a vehicle — confirm the
					// schema allows that, same caveat as your original code.
					var transaction = CreateCustomerTransaction(
						customer.CustomerCode, string.Empty, amountToCredit, 0,
						dto.TransactionReference, dto.PaymentType, "Wallet deposit");

					var saveResult = await SaveTransactionAsync(transaction);
					if (saveResult.ResponseCode == Response.Error)
					{
						await tx.RollbackAsync();
						// SaveTransactionAsync returns the non-generic ServiceResponse;
						// cast to the generic ServiceResponse<object> this method returns.
						return (ServiceResponse<object>)saveResult;
					}

					await tx.CommitAsync();

					var newBalance = await GetCustomerBalance(customer.CustomerCode);
					var firstName = customer.CustomerName.Split(' ')[0];

					await _africaIsTalking.SendSms(customer.CustomerPhone,
						$"Dear {firstName}, your wallet has been topped up with {amountToCredit:N2} ksh on " +
						$"{DateTime.UtcNow:dd/MM/yy} at {DateTime.UtcNow:hh:mm tt}. Your new balance is " +
						$"{newBalance.ResponseObject:N2} ksh. Thank you for choosing Otogas.");

					var message = $"{_authentication.Name()} has topped up customer {customer.CustomerName} " +
								  $"({customer.CustomerCode}) with {amountToCredit:N2} ksh on {DateTime.UtcNow}";
					await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

					return ServiceResponse<object>.Success(
						$"Customer wallet topped up with {amountToCredit:N2}", new { NewBalance = newBalance.ResponseObject });
				}
				catch
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error("An error occurred while topping up the wallet.", null);
				}
			});
		}
		private async Task<Customer> GetCustomerDetailsByCustomerCodeAsync(string customerCode)
		{
			return await _context.Customers
				.FirstOrDefaultAsync(x => x.CustomerCode == customerCode) ?? new Customer();
		}

		private (ServiceResponse<object>? Error, decimal FullBalance) ValidateAndConsumeMpesa(MpesaTransaction mpesaTx, string tillNumber)
		{
			var till = Regex.Replace(mpesaTx.TillNumber ?? string.Empty, @"\s+", "").Trim();

			if (!string.Equals(till, tillNumber?.Trim(), StringComparison.OrdinalIgnoreCase))
				return (ServiceResponse<object>.Information("Mpesa code does not belong to that till", null), 0);

			if (mpesaTx.UsageBalance <= 0)
				return (ServiceResponse<object>.Information($"Mpesa code {mpesaTx.TransID} has already been fully used", null), 0);

			decimal fullBalance = mpesaTx.UsageBalance;

			mpesaTx.UsageBalance = 0;
			mpesaTx.Status = 0; // fully used
			mpesaTx.DateModified = EatTime.Now;

			_context.Entry(mpesaTx).State = EntityState.Modified;

			return (null, fullBalance);
		}

		/// <summary>
		/// Withdraws (debits) funds from a customer's wallet. Fails if the wallet does not have sufficient balance.
		/// VehicleCode identifies which vehicle triggered the withdrawal; the balance check happens against the
		/// customer wallet, not the vehicle.
		/// NOTE: balance check + insert are two separate round trips. Under high concurrency two simultaneous
		/// withdrawals on the same wallet (even from two different vehicles belonging to the same customer)
		/// could both pass the check before either commits. If that's a real risk for you, wrap this in a
		/// serializable transaction or add a Postgres CHECK/trigger that rejects a resulting negative balance
		/// as a backstop. This risk is now higher than before, since multiple vehicles can hit one wallet.
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

			var balanceResponse = await GetCustomerBalance(customer.CustomerCode);
			if (balanceResponse.ResponseCode == Response.Error)
				return ServiceResponse<object>.Error("Could not verify wallet balance, please try again");

			if (balanceResponse.ResponseObject < withdrawDto.Amount)
				return ServiceResponse<object>.Information($"Insufficient wallet balance. Available balance is {balanceResponse.ResponseObject:N2} ksh");

			var transaction = CreateCustomerTransaction(customer.CustomerCode, vehicle.VehicleCode, 0, withdrawDto.Amount, withdrawDto.TransactionReference, withdrawDto.WithdrawalType, withdrawDto.Narration ?? "Wallet withdrawal");

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
		/// DESIGN DECISION: since wallets are now per-customer, this only makes sense between two DIFFERENT
		/// customers (transferring within the same customer's own vehicles would be moving money from a
		/// wallet to itself). If both vehicles resolve to the same CustomerCode, this now short-circuits with
		/// an Information response instead of writing two no-op ledger rows. Confirm this is the behaviour you
		/// want — the alternative is to silently allow it (it's a net-zero pair of rows either way).
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

			var fromCustomer = await GetCustomerDetailsAsync(transferCustomerBalanceDto.FromVehicleCode);
			var toCustomer = await GetCustomerDetailsAsync(transferCustomerBalanceDto.ToVehicleCode);
			if (fromCustomer is null || toCustomer is null)
				return ServiceResponse<object>.Information("Could not resolve the customer for one of these vehicles");

			if (fromCustomer.CustomerCode == toCustomer.CustomerCode)
				return ServiceResponse<object>.Information("Both vehicles belong to the same customer wallet — no transfer needed");

			var fromBalance = await GetCustomerBalance(fromCustomer.CustomerCode);
			if (fromBalance.ResponseObject < transferCustomerBalanceDto.Amount)
				return ServiceResponse<object>.Information($"Insufficient balance on {fromVehicle.VehicleRegistrationNumber}'s wallet to complete this transfer");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var narration = $"{transferCustomerBalanceDto.Amount:N2} transferred from {fromCustomer.CustomerName} to {toCustomer.CustomerName}";

				var fromTransaction = CreateCustomerTransaction(fromCustomer.CustomerCode, fromVehicle.VehicleCode, 0, transferCustomerBalanceDto.Amount, "", 5, narration);
				var toTransaction = CreateCustomerTransaction(toCustomer.CustomerCode, toVehicle.VehicleCode, transferCustomerBalanceDto.Amount, 0, "", 5, narration);

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
		/// Current wallet balance (sum of credits minus debits) for a customer, shared across all
		/// their vehicles. Takes a CustomerCode now, not a VehicleCode.
		/// </summary>
		public async Task<ServiceResponse<decimal>> GetCustomerBalance(string customerCode)
		{
			try
			{
				var balance = await _context.CustomerTransactions
					.Where(x => x.CustomerCode == customerCode)
					.SumAsync(x => x.Credit - x.Debit);

				return ServiceResponse<decimal>.Success("Balance Found", balance);
			}
			catch (Exception ex)
			{
				return ServiceResponse<decimal>.Error(ex.Message, 0);
			}
		}

		/// <summary>
		/// Convenience overload for callers that only have a VehicleCode on hand (e.g. a POS screen
		/// that scanned a vehicle). Resolves the owning customer first, then returns the shared balance.
		/// </summary>
		public async Task<ServiceResponse<decimal>> GetCustomerBalanceByVehicle(string vehicleCode)
		{
			var customer = await GetCustomerDetailsAsync(vehicleCode);
			if (customer is null)
				return ServiceResponse<decimal>.Information("Customer Not Found", 0);

			return await GetCustomerBalance(customer.CustomerCode);
		}

		/// <summary>
		/// Paginated, filterable wallet balances — one row PER CUSTOMER (not per vehicle, since the
		/// wallet is now shared). registrationNumber filters to customers who own a matching vehicle.
		/// DESIGN DECISION: the old CustomerBalanceDto had VehicleCode/RegistrationNumber columns tied
		/// to a single vehicle, which no longer makes sense for a shared wallet. This returns an
		/// anonymous shape with CustomerCode, CustomerName, Balance instead — update/trim
		/// CustomerBalanceDto in DataAccessLayer.DTOs.Transactions to match (or tell me its current
		/// shape and I'll wire it back to a named DTO).
		/// </summary>
		public async Task<ServiceResponse<object>> GetCustomerBalances(string? registrationNumber = null, string? customerName = null, int pageNumber = 1, int pageSize = 15)
		{
			try
			{
				var query = _context.CustomerTransactions
					.Join(_context.Customers, ct => ct.CustomerCode, c => c.CustomerCode, (ct, c) => new { ct, c })
					.GroupBy(g => new { g.c.CustomerCode, g.c.CustomerName })
					.Select(group => new
					{
						CustomerCode = group.Key.CustomerCode,
						CustomerName = group.Key.CustomerName,
						Balance = group.Sum(x => x.ct.Credit) - group.Sum(x => x.ct.Debit)
					});

				if (!string.IsNullOrEmpty(customerName))
					query = query.Where(q => q.CustomerName.Contains(customerName));

				if (!string.IsNullOrEmpty(registrationNumber))
				{
					var matchingCustomerCodes = _context.Vehicles
						.Where(v => v.VehicleRegistrationNumber.Contains(registrationNumber))
						.Select(v => v.CustomerCode);

					query = query.Where(q => matchingCustomerCodes.Contains(q.CustomerCode));
				}

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
		/// All wallet transactions for a customer, across every vehicle they own, with a running balance.
		/// DESIGN DECISION: previously this took a single vehicle's reg no and only showed that vehicle's
		/// rows. Since the wallet is now shared, I resolve the owning customer from the reg no supplied
		/// and then return the FULL customer wallet history (every vehicle), each row tagged with which
		/// vehicle it came from. If you actually want a "which transactions used this specific vehicle"
		/// report (not a wallet statement), that's a different, simpler query — let me know and I'll add
		/// it as a separate method instead of overloading this one.
		/// </summary>
		public async Task<ServiceResponse<object>> WalletHistories(string vRegno)
		{
			try
			{
				var customer = await GetCustomerDetailsAsync(
					await _context.Vehicles.Where(v => v.VehicleRegistrationNumber == vRegno).Select(v => v.VehicleCode).FirstOrDefaultAsync() ?? string.Empty);

				if (customer is null)
					return ServiceResponse<object>.Information("Vehicle/customer not found", null);

				var history = await (from ct in _context.CustomerTransactions
									 join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
									 where ct.CustomerCode == customer.CustomerCode
									 orderby ct.DateCreated
									 select new
									 {
										 customer.CustomerName,
										 customer.CustomerPhone,
										 VehicleRegistrationNumber = v.VehicleRegistrationNumber,
										 ct.TransactionReference,
										 ct.DateCreated,
										 ct.Credit,
										 ct.Debit
									 }).ToListAsync();

				decimal runningBalance = 0;
				var runningBalanceHistory = history.Select(item =>
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
				}).ToList();

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
		/// Wallet statement for a customer over a date range, with an opening balance line.
		/// DESIGN DECISION: now takes customerCode instead of vehicleCode (the wallet is the
		/// customer's, so this is the natural key). If your controller only has a vehicleCode at
		/// the call site, resolve it first via GetCustomerDetailsAsync and pass customer.CustomerCode in.
		/// </summary>
		public async Task<ServiceResponse<List<CustomerTransactionDto>>> GetCustomerStatement(string customerCode, DateTime startDate, DateTime endDate)
		{
			try
			{
				var customerExists = await _context.Customers.AnyAsync(x => x.CustomerCode == customerCode);
				if (!customerExists)
					return ServiceResponse<List<CustomerTransactionDto>>.Information("Customer does not exist", null);

				var startingBalance = await _context.CustomerTransactions
					.Where(x => x.CustomerCode == customerCode && x.DateCreated < startDate)
					.SumAsync(x => x.Credit - x.Debit);

				var transactions = await _context.CustomerTransactions
					.Where(x => x.CustomerCode == customerCode && x.DateCreated >= startDate && x.DateCreated <= endDate)
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
		/// All deposit transactions (Credit column) recorded for a customer's wallet, across every
		/// vehicle. Takes vehicleCode (as before, for callers that only have that on hand) and
		/// resolves the customer internally.
		/// </summary>
		public async Task<ServiceResponse<object>> GetCustomerPayments(string vehicleCode)
		{
			try
			{
				var customer = await GetCustomerDetailsAsync(vehicleCode);
				if (customer is null)
					return ServiceResponse<object>.Information("Customer Not Found", null);

				var transactions = await (from ct in _context.CustomerTransactions
										  join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
										  where ct.CustomerCode == customer.CustomerCode
										  select new
										  {
											  customer.CustomerName,
											  customer.CustomerPhone,
											  VehicleRegistrationNumber = v.VehicleRegistrationNumber,
											  ct.TransactionReference,
											  ct.DateCreated,
											  Payments = ct.Credit
										  }).ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No transactions found for the specified customer", null);

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
		/// Batch-credits multiple CUSTOMER wallets from an uploaded Excel file (column 1 = amount,
		/// column 2 = vehicle reg no). The vehicle reg no is only used to look up which customer/vehicle
		/// pair to tag the row with; the credit lands on the customer's shared wallet.
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

			// Now carries CustomerCode alongside VehicleCode, since the credit lands on the customer wallet.
			var existingVehicles = await _context.Vehicles
				.AsNoTracking()
				.Where(v => vehicleRegNos.Contains(v.VehicleRegistrationNumber.Replace(" ", "").ToUpper()))
				.ToDictionaryAsync(
					v => v.VehicleRegistrationNumber.Replace(" ", "").ToUpper(),
					v => new { v.VehicleCode, v.CustomerCode });

			var batchNo = _setups.GenerateSaleId();

			foreach (var row in rows)
			{
				var saleId = _setups.GenerateSaleId();
				decimal amount = row.Cell(1).GetValue<decimal>();
				string vehicleRegNo = row.Cell(2).GetValue<string>().Replace(" ", "").ToUpper();

				if (!existingVehicles.TryGetValue(vehicleRegNo, out var vehicleInfo))
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
					VehicleCode = vehicleInfo.VehicleCode,
					CustomerCode = vehicleInfo.CustomerCode,
					Narration = "Batch Credit Upload",
					TopUpType = topUpType,
				});

				var phoneNumber = await (from v in _context.Vehicles
										 where v.VehicleCode.Equals(vehicleInfo.VehicleCode)
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
		/// Manual top-up of a separate "customer funds" pool (distinct from the CustomerTransactions
		/// wallet ledger). Unchanged — this was already keyed by CustomerCode.
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
		/// Reverses a previous "customer funds" top-up. Unchanged — already keyed by CustomerCode.
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
		/// Exports a customer's full wallet transaction history (across every vehicle they own) to a
		/// password-protected Excel workbook. DESIGN DECISION: previously this took a vehicleCode and
		/// showed only that vehicle's rows with a running balance — that running balance is now
		/// meaningless for a single vehicle since the wallet is shared. I resolve the customer from the
		/// vehicleCode passed in, then export the FULL customer history, with a Vehicle Used column so
		/// you can still see which vehicle each transaction came from.
		/// </summary>
		public async Task<ServiceResponse<byte[]>> ExportCustomerTransactions(string vehicleCode)
		{
			try
			{
				var isVehicleExist = await _context.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
				if (!isVehicleExist)
					return ServiceResponse<byte[]>.Information("Vehicle does not exist", null);

				var customer = await GetCustomerDetailsAsync(vehicleCode);
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer of the vehicle not found", null);

				var transactions = await (from ct in _context.CustomerTransactions
										  join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
										  where ct.CustomerCode == customer.CustomerCode
										  orderby ct.DateCreated
										  select new
										  {
											  ct.TransactionReference,
											  ct.DateCreated,
											  ct.Credit,
											  ct.Debit,
											  VehicleUsed = v.VehicleRegistrationNumber
										  }).ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add(customer.CustomerName);

				ApplyTitleStyle(worksheet, "Wallet Statement", "A1:F1");

				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Customer Code:";
				worksheet.Cell(4, 2).Value = customer.CustomerCode;
				StyleDetailsBlock(worksheet.Range("A2:B4"));

				string[] headers = { "Transaction Reference", "Date Created", "Vehicle Used", "Credit", "Debit", "Running Balance" };
				for (int i = 0; i < headers.Length; i++)
					worksheet.Cell(6, i + 1).Value = headers[i];
				StyleHeaderRow(worksheet.Range("A6:F6"));

				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 1).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 2).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 3).Value = transactions[i].VehicleUsed;
					worksheet.Cell(row, 4).Value = transactions[i].Credit;
					worksheet.Cell(row, 5).Value = transactions[i].Debit;
					worksheet.Cell(row, 6).Value = runningBalance;

					worksheet.Cell(row, 2).Style.DateFormat.Format = "yyyy-mm-dd";
					worksheet.Range(row, 4, row, 6).Style.NumberFormat.Format = "#,##0.00";

					ApplyRowStyle(worksheet, row, i, 6);
				}

				var lastRow = transactions.Count + 7;
				worksheet.Cell(lastRow, 5).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 6).Value = runningBalance;
				StyleTotalRow(worksheet.Range(lastRow, 5, lastRow, 6));
				worksheet.Cell(lastRow, 6).Style.NumberFormat.Format = "#,##0.00";

				worksheet.Columns().AdjustToContents();
				worksheet.SheetView.FreezeRows(6);
				workbook.Protect(customer.CustomerCode);

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
		/// Exports a customer-level wallet statement (across all their vehicles) from a given start date,
		/// including an opening balance line, to Excel. Already took customerCode before — unchanged in
		/// shape, but now benefits from the CustomerCode column directly instead of the VehicleCode-based
		/// workaround in GetCustomerTransactionsAsync (see MIGRATION_NOTES.md for the bug this fixes).
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

		private CustomerTransactions CreateCustomerTransaction(string customerCode, string vehicleCode, decimal credit, decimal debit, string reference, int topUpType, string narration)
		{
			return new CustomerTransactions
			{
				DateCreated = EatTime.Now,
				UserCode = _authentication.Usercode(),
				CustomerCode = customerCode,
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
		/// Still the main resolver used everywhere a caller only has a VehicleCode on hand.
		/// </summary>
		private async Task<Customer?> GetCustomerDetailsAsync(string vehicleCode)
		{
			if (string.IsNullOrEmpty(vehicleCode))
				return null;

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

		/// <summary>
		/// FIX: previously filtered balanceBefore by `x.VehicleCode == customerCode`, comparing a
		/// vehicle code against a customer code — that could never match, so the opening balance line
		/// on customer statements was effectively always 0. Now filters by the real CustomerCode column.
		/// </summary>
		private async Task<List<TransactionDto>> GetCustomerTransactionsAsync(string customerCode, DateTime from)
		{
			var balanceBefore = await _context.CustomerTransactions
				.Where(x => x.CustomerCode == customerCode && x.DateCreated.Date <= from.Date)
				.SumAsync(x => x.Credit - x.Debit);

			var transactions = await (from c in _context.CustomerTransactions
									  join v in _context.Vehicles on c.VehicleCode equals v.VehicleCode
									  where c.CustomerCode == customerCode
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