namespace BusinessLogic.Sales.Wallet
{
	using BussinessLogic.Authentication.CommonTasks;
	using BusinessLogic.Messaging;
	using BussinessLogic.Sales.Wallet;
	using ClosedXML.Excel;
	using DataAccessLayer.Common;
	using DataAccessLayer.Context;
	using DataAccessLayer.DTOs.Transactions;
	using DataAccessLayer.EntityModels.Customer;
	using DataAccessLayer.EntityModels.SetUps;
	using DataAccessLayer.EntityModels.Transactions;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Caching.Memory;
	using OfficeOpenXml;
	using OfficeOpenXml.Style;
	using Syncfusion.Pdf;
	using Syncfusion.XlsIO;
	using Syncfusion.XlsIORenderer;
	using System.Drawing;
	using System.Reflection;
	using static DataAccessLayer.EntityModels.Wallet.WalletDto;
	using ExcelHorizontalAlignment = Syncfusion.XlsIO.ExcelHorizontalAlignment;
	using BussinessLogic.Setup;

	/// <summary>
	/// Defines the <see cref="WalletTransactions" />
	/// </summary>
	public class WalletTransactions : IWalletTransactions
	{
		/// <summary>
		/// Defines the _authentication
		/// </summary>
		private readonly IAuthCommonTasks _authentication;

		/// <summary>
		/// Defines the _setups
		/// </summary>
		private readonly ICommonSetups _setups;

		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly OTOContext _context;

		/// <summary>
		/// Defines the _africaIsTalking
		/// </summary>
		private readonly IAfricaIsTalking _africaIsTalking;

		/// <summary>
		/// Defines the _emailService
		/// </summary>
		private readonly IEmailService _emailService;

		/// <summary>
		/// Defines the _cache
		/// </summary>
		private readonly IMemoryCache _cache;

		/// <summary>
		/// Initializes a new instance of the <see cref="WalletTransactions"/> class.
		/// </summary>
		/// <param name="authentication">The authentication<see cref="IAuthCommonTasks"/></param>
		/// <param name="setups">The setups<see cref="ICommonSetups"/></param>
		/// <param name="context">The context<see cref="OTOContext"/></param>
		/// <param name="africaIsTalking">The africaIsTalking<see cref="IAfricaIsTalking"/></param>
		/// <param name="emailService">The emailService<see cref="IEmailService"/></param>
		/// <param name="cache">The cache<see cref="IMemoryCache"/></param>
		public WalletTransactions(IAuthCommonTasks authentication, ICommonSetups setups, OTOContext context, IAfricaIsTalking africaIsTalking, IEmailService emailService, IMemoryCache cache)
		{
			_authentication = authentication;
			_setups = setups;
			_context = context;
			_africaIsTalking = africaIsTalking;
			_emailService = emailService;
			_cache = cache;
		}

		// Helper method to create a transaction object

		/// <summary>
		/// The CreateCustomerTransaction
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <param name="credit">The credit<see cref="decimal"/></param>
		/// <param name="debit">The debit<see cref="decimal"/></param>
		/// <param name="reference">The reference<see cref="string"/></param>
		/// <param name="topUpType">The topUpType<see cref="int"/></param>
		/// <param name="narration">The narration<see cref="string"/></param>
		/// <returns>The <see cref="CustomerTransactions"/></returns>
		private CustomerTransactions CreateCustomerTransaction(string vehicleCode, decimal credit, decimal debit, string reference, int topUpType, string narration)
		{
			return new CustomerTransactions
			{
				DateCreated = DateTime.UtcNow,
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

		private CustomerTransactions CreateVoucherTransaction(string vehicleCode, decimal credit, decimal debit, string reference, int topUpType, string narration)
		{
			return new CustomerTransactions
			{
				DateCreated = DateTime.UtcNow,
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

		/// <summary>
		/// The CreateFundCustomerTransaction
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <param name="credit">The credit<see cref="decimal"/></param>
		/// <param name="debit">The debit<see cref="decimal"/></param>
		/// <param name="reference">The reference<see cref="string"/></param>
		/// <returns>The <see cref="CustomerTransactions"/></returns>
		private CustomerTransactions CreateFundCustomerTransaction(string vehicleCode, decimal credit, decimal debit, string reference)
		{
			return new CustomerTransactions
			{
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
				VehicleCode = vehicleCode,
				TransactionReference = _setups.GenerateSaleId(),
				Credit = credit,
				Debit = debit,
				UserReference = string.Empty,
				Narration = $"Top up ref no {reference} "
			};
		}

		// Common method to save a transaction and return a success response

		/// <summary>
		/// The SaveTransactionAsync
		/// </summary>
		/// <param name="transaction">The transaction<see cref="CustomerTransactions"/></param>
		/// <param name="successMessage">The successMessage<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		private async Task<ServiceResponse> SaveTransactionAsync(CustomerTransactions transaction, string successMessage)
		{
			_ = new ServiceResponse();
			try
			{
				var narration = $"Top up RefNo {transaction.UserReference}";
				await _context.Database.ExecuteSqlRawAsync(
					"EXEC InsertCustomerTransaction @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9",
				parameters:
				[
					transaction.VehicleCode, transaction.Credit, transaction.Debit, transaction.UserReference, transaction.DateCreated, transaction.TransactionReference, transaction.UserCode,2,transaction.TopUpType,narration
				]);

				//await _context.CustomerTransactions.AddAsync(transaction);
				//await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success(successMessage);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				return new ServiceResponse
				{
					ResponseMessage = ex.Message,
					ResponseCode = Response.Error
				};
			}
		}

		// Credit a customer account

		/// <summary>
		/// The CreditInCustomerAccount
		/// </summary>
		/// <param name="customerTransactionDto">The customerTransactionDto<see cref="CreditCustomerTransactionDto"/></param>
		/// <param name="paymentType">The paymentType<see cref="int"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		private async Task<ServiceResponse> CreditInCustomerAccount(CreditCustomerTransactionDto customerTransactionDto, int paymentType)
		{
			var transaction = CreateCustomerTransaction(customerTransactionDto.VehicleCode, customerTransactionDto.Credit, 0, "", paymentType, "");

			return await SaveTransactionAsync(transaction, "Customer Transaction Added Successfully");
		}

		// Debit a customer account

		/// <summary>
		/// The DebitInCustomerAccount
		/// </summary>
		/// <param name="customerTransactionDto">The customerTransactionDto<see cref="DebitCustomerTransactionDto"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		private async Task<ServiceResponse> DebitInCustomerAccount(DebitCustomerTransactionDto customerTransactionDto)
		{
			var transaction = CreateCustomerTransaction(customerTransactionDto.VehicleCode, 0, customerTransactionDto.Debit, "", 0, "");
			return await SaveTransactionAsync(transaction, "Customer Transaction Added Successfully");
		}

		// Get customer balance

		/// <summary>
		/// The GetCustomerBalance
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{decimal}}"/></returns>
		private async Task<ServiceResponse<decimal>> GetCustomerBalance(string vehicleCode)
		{
			var response = new ServiceResponse<decimal>();
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

		//get customer Details 

		// Get cached customer details

		/// <summary>
		/// The GetCustomerDetailsAsync
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{Customer}"/></returns>
		private async Task<Customer> GetCustomerDetailsAsync(string vehicleCode)
		{
			if (!_cache.TryGetValue(vehicleCode, out Customer? customer))
			{
				customer = await (from v in _context.Vehicles
								  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
								  where v.VehicleCode == vehicleCode
								  select c).FirstOrDefaultAsync() ?? new Customer();

				if (customer != null)
				{
					_cache.Set(vehicleCode, customer, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
				}
			}
			return customer ?? new Customer();
		}

		/// <summary>
		/// The GetCustomerDetails
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{Customer}}"/></returns>
		private async Task<ServiceResponse<Customer>> GetCustomerDetails(string vehicleCode)
		{

			try
			{
				var customer = await (from v in _context.Vehicles
									  join Customer in _context.Customers on v.CustomerCode equals Customer.CustomerCode
									  where v.VehicleCode == vehicleCode
									  select new Customer
									  {
										  CustomerCode = Customer.CustomerCode,
										  CustomerName = Customer.CustomerName,
										  CustomerPhone = v.PhoneNumber,
										  DateCreated = Customer.DateCreated,
										  CustomerEmail = Customer.CustomerEmail,
										  IdentificationNumber = Customer.IdentificationNumber,
									  }).FirstOrDefaultAsync();

				return ServiceResponse<Customer>.Success("Customer Found", customer);
			}
			catch (Exception ex)
			{
				return ServiceResponse<Customer>.Error(ex.Message, null);
			}
		}

		// Top up customer wallet

		/// <summary>
		/// The TopUpCustomerWallet
		/// </summary>
		/// <param name="topUpCustomerWalletDto">The topUpCustomerWalletDto<see cref="TopUpCustomerWalletDto"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		public async Task<ServiceResponse> TopUpCustomerWallet(TopUpCustomerWalletDto topUpCustomerWalletDto)
		{
			if (string.IsNullOrEmpty(topUpCustomerWalletDto.VehicleCode))
				return ServiceResponse<object>.Information("Kindly provide the vehicle registration number");

			var isVehicleExist = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == topUpCustomerWalletDto.VehicleCode);
			if (isVehicleExist is null)
				return ServiceResponse<object>.Information("Vehicle does not exist");

			if (await CheckIfFueledLessThan2MinutesAgo(topUpCustomerWalletDto.VehicleCode, topUpCustomerWalletDto.Amount))
				return ServiceResponse<object>.Information("Vehicle has fueled less than 2 minutes ago the same liters");

			var transaction = CreateCustomerTransaction(topUpCustomerWalletDto.VehicleCode, topUpCustomerWalletDto.Amount, 0, topUpCustomerWalletDto.TransactionReference, topUpCustomerWalletDto.PaymentType, "");
			var details = await GetCustomerDetails(topUpCustomerWalletDto.VehicleCode);

			if (details != null)
			{
				if (details.ResponseObject == null)
					return ServiceResponse<object>.Information("Customer Not Found");
				var FirstName = details.ResponseObject.CustomerName.Split(' ')[0];
				var PhoneNumber = details.ResponseObject.CustomerPhone;
				var Amount = topUpCustomerWalletDto.Amount;

				await SaveTransactionAsync(transaction, "Customer Wallet Topped Up Successfully");

				var balance = await (from ct in _context.CustomerTransactions
									 where ct.VehicleCode == topUpCustomerWalletDto.VehicleCode
									 select ct).SumAsync(x => x.Credit - x.Debit);

				await _africaIsTalking.SendSms(PhoneNumber, $"Dear {FirstName}, your wallet has been topped up with {Amount:N2} ksh on {DateTime.UtcNow:dd/MM/yy} at {DateTime.UtcNow:hh:mm tt}. Your new balance is {balance:N2} ksh. Thank you for choosing Otogas.");

				var message = $"{_authentication.Name()} has topped up {isVehicleExist.VehicleRegistrationNumber} with {Amount:N2} ksh on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");
			}
			return ServiceResponse<object>.Success($"Customer Wallet Topped Up with {topUpCustomerWalletDto.Amount:N2}");
		}

		//check if in customerTransactions table that a given vehiclecode and given quantity has fueled less than 5 minutes a ago

		/// <summary>
		/// The CheckIfFueledLessThan2MinutesAgo
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <param name="quantity">The quantity<see cref="decimal"/></param>
		/// <returns>The <see cref="Task{bool}"/></returns>
		private async Task<bool> CheckIfFueledLessThan2MinutesAgo(string vehicleCode, decimal quantity)
		{
			var lastTransaction = await _context.CustomerTransactions
				.Where(x => x.VehicleCode == vehicleCode && x.Credit == quantity)
				.OrderByDescending(x => x.DateCreated)
				.FirstOrDefaultAsync();

			if (lastTransaction == null)
				return false;

			var timeDifference = DateTime.UtcNow - lastTransaction.DateCreated;
			return timeDifference.TotalMinutes < 2;
		}

		// Get all customer balances

		/// <summary>
		/// The GetAllCustomerBalances
		/// </summary>
		/// <returns>The <see cref="Task{ServiceResponse{List{CustomerBalanceDto}}}"/></returns>
		public async Task<ServiceResponse<List<CustomerBalanceDto>>> GetAllCustomerBalances()
		{
			var newbalance = new List<CustomerBalanceDto>();
			var response = new ServiceResponse<List<CustomerBalanceDto>>();
			try
			{
				var sql = @"
                SELECT 
                    c.CustomerCode, 
                    c.CustomerName, 
                    v.VehicleCode, 
                    v.VehicleRegistrationNumber AS RegistrationNumber,
                    SUM(ct.Credit) - SUM(ct.Debit) AS Balance
                FROM CustomerTransactions ct
                INNER JOIN Vehicles v ON ct.VehicleCode = v.VehicleCode
                INNER JOIN Customers c ON v.CustomerCode = c.CustomerCode
                GROUP BY c.CustomerCode, c.CustomerName, v.VehicleCode, v.VehicleRegistrationNumber";

				var balances = await _context.CustomerBalanceDtos.FromSqlRaw(sql).ToListAsync();

				foreach (var balance in balances)
				{
					newbalance.Add(new CustomerBalanceDto
					{
						CustomerCode = balance.CustomerCode,
						CustomerName = balance.CustomerName,
						VehicleCode = balance.VehicleCode,
						RegistrationNumber = balance.RegistrationNumber,
						Balance = balance.Balance
					});
				}

				return ServiceResponse<List<CustomerBalanceDto>>.Success("Balances Found", newbalance);
			}
			catch (Exception ex)
			{
				response.ResponseMessage = ex.Message;
				response.ResponseCode = Response.Error;
			}
			return response;
		}

		/// <summary>
		/// The GetAllCustomerBalancesSql
		/// </summary>
		/// <param name="registrationNumber">The registrationNumber<see cref="string?"/></param>
		/// <param name="customerName">The customerName<see cref="string?"/></param>
		/// <param name="pageNumber">The pageNumber<see cref="int"/></param>
		/// <param name="pageSize">The pageSize<see cref="int"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{object}}"/></returns>
		public async Task<ServiceResponse<object>> GetAllCustomerBalancesSql(
		string? registrationNumber = null,
		string? customerName = null,
		int pageNumber = 1,
		int pageSize = 15)
		{
			var parameters = new[]
		   {
				new Microsoft.Data.SqlClient.SqlParameter("@RegistrationNumber", (object?)registrationNumber ?? ""),
				new Microsoft.Data.SqlClient.SqlParameter("@CustomerName", (object?)customerName ?? ""),
				new Microsoft.Data.SqlClient.SqlParameter("@PageNumber", pageNumber),
				new Microsoft.Data.SqlClient.SqlParameter("@PageSize", pageSize)
			};

			try
			{
				var results = await _context.CustomerBalanceDtos
					.FromSqlRaw("EXEC [dbo].[GetAllCustomerBalances] @RegistrationNumber, @CustomerName, @PageNumber, @PageSize", parameters)
					.ToListAsync();

				var totalRecords = results.FirstOrDefault()?.TotalRecords ?? 0;

				var response = new
				{
					TotalRecords = totalRecords,
					PageNumber = pageNumber,
					PageSize = pageSize,
					Sales = results
				};

				return ServiceResponse<object>.Success("Balances Found", response);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong, contact system admin", ex);
			}
		}

		//give me all transaction in customertransactions filter by RegistrationNumber, CustomerName

		/// <summary>
		/// The WalletHistories
		/// </summary>
		/// <param name="vRegno">The vRegno<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{object}}"/></returns>
		public async Task<ServiceResponse<object>> WalletHistories(string vRegno)
		{
			try
			{
				var history = await (from ct in _context.CustomerTransactions
									 join v in _context.Vehicles on ct.VehicleCode equals v.VehicleCode
									 join c in _context.Customers on v.CustomerCode equals c.CustomerCode
									 where v.VehicleRegistrationNumber == vRegno
									 orderby c.CustomerCode, v.VehicleRegistrationNumber, ct.DateCreated // Sort by customer, vehicle, and date
									 select new
									 {
										 c.CustomerName,
										 c.CustomerPhone,
										 v.VehicleRegistrationNumber,
										 ct.TransactionReference,
										 ct.DateCreated,
										 ct.Credit,
										 ct.Debit
									 }
						).ToListAsync();

				// Calculate the running balance
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

		// Get customer statement with running balance

		/// <summary>
		/// The GetCustomerStatement
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <param name="startDate">The startDate<see cref="DateTime"/></param>
		/// <param name="endDate">The endDate<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{List{CustomerTransactionDto}}}"/></returns>
		public async Task<ServiceResponse<List<CustomerTransactionDto>>> GetCustomerStatement(string vehicleCode, DateTime startDate, DateTime endDate)
		{
			var response = new ServiceResponse<List<CustomerTransactionDto>>();
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
					new() {
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
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<List<CustomerTransactionDto>>.Error(ex.Message, null);
			}
		}

		// Upload multiple customer transactions (batch credit)

		/// <summary>
		/// The UploadCustomerTransactions
		/// </summary>
		/// <param name="file">The file<see cref="IFormFile"/></param>
		/// <param name="topUpType">The topUpType<see cref="int"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{object}}"/></returns>
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
			var rows = worksheet.RowsUsed().Skip(1); // Skip header row

			// Extract all vehicle registration numbers from the Excel file
			var vehicleRegNos = rows.Select(row => row.Cell(2).GetValue<string>().Replace(" ", "").ToUpper()).Distinct().ToList();

			// Fetch existing vehicles in one query
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
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode()
					});
					continue;
				}

				customerTransactions.Add(new CustomerTransactions
				{
					Credit = amount,
					DateCreated = DateTime.UtcNow,
					Debit = 0,
					TransactionReference = saleId,
					UserCode = _authentication.Usercode(),
					UserReference = saleId,
					VehicleCode = vehicleCode,
					Narration = "Batch Credit Upload",
					TopUpType = topUpType,
					BatchNumber = batchNo
				});
				var phoneNumber = await(from v in _context.Vehicles
										where v.VehicleCode.Equals(vehicleCode)
										select v.PhoneNumber).FirstAsync();

				var messages = $"Congrats! 🎉 You have been awarded a fuel voucher  No: {saleId} ⛽ Amount: {amount} Redeem at any of our OTOGas Stations. ";
				await _africaIsTalking.SendSms(phoneNumber,messages);
			}

			// Bulk insert transactions to reduce database round trips
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

		// Export customer transactions to Excel

		/// <summary>
		/// The ExportCustomerTransactions
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{byte[]}}"/></returns>
		public async Task<ServiceResponse<byte[]>> ExportCustomerTransactions(string vehicleCode)
		{
			try
			{

				// Check if vehicle exists
				var isVehicleExist = await _context.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
				if (!isVehicleExist)
					return ServiceResponse<byte[]>.Information("Vehicle does not exist", null);

				// Join vehicle with customer and get details
				var customer = await (from v in _context.Vehicles
									  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
									  where v.VehicleCode == vehicleCode
									  select new
									  {
										  c.CustomerName,
										  v.VehicleRegistrationNumber,
										  c.CustomerPhone
									  }).FirstOrDefaultAsync();
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer of the vehicle not found", null);

				var transactions = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode)
					.OrderBy(x => x.DateCreated)
					.ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified vehicle", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add($"{customer.VehicleRegistrationNumber}");

				// Set title style
				var titleRange = worksheet.Range("A1:F1");
				titleRange.Merge().Value = "Wallet Statement";
				titleRange.Style.Font.Bold = true;
				titleRange.Style.Font.FontSize = 18;
				titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				titleRange.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
				titleRange.Style.Font.FontColor = XLColor.White;
				titleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				titleRange.Style.Border.OutsideBorderColor = XLColor.DarkBlue;

				// Add customer details header with enhanced styling

				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Vehicle Registration:";
				worksheet.Cell(4, 2).Value = customer.VehicleRegistrationNumber;

				// Styling for customer details
				var customerDetailsRange = worksheet.Range("A2:B4");
				customerDetailsRange.Style.Font.Bold = true;
				customerDetailsRange.Style.Fill.BackgroundColor = XLColor.LightGray;
				customerDetailsRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				customerDetailsRange.Style.Border.OutsideBorderColor = XLColor.Black;
				customerDetailsRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Add headers for transaction details with enhanced styling

				worksheet.Cell(6, 1).Value = "Transaction Reference";
				worksheet.Cell(6, 2).Value = "Date Created";
				worksheet.Cell(6, 3).Value = "Credit";
				worksheet.Cell(6, 4).Value = "Debit";
				worksheet.Cell(6, 5).Value = "Running Balance";

				var headerRange = worksheet.Range("A6:E6");
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Font.FontColor = XLColor.White;
				headerRange.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
				headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
				headerRange.Style.Border.BottomBorderColor = XLColor.DarkBlue;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Add data to the worksheet with styling
				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 3).Style.NumberFormat.SetNumberFormatId(4); // Format as currency
					worksheet.Cell(row, 4).Style.NumberFormat.SetNumberFormatId(4);
					worksheet.Cell(row, 5).Style.NumberFormat.SetNumberFormatId(22);
					worksheet.Cell(row, 2).Style.NumberFormat.SetNumberFormatId(14); // Format as date
					worksheet.Cell(row, 1).Style.NumberFormat.SetNumberFormatId(0); // Format as text

					worksheet.Cell(row, 1).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 2).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 3).Value = transactions[i].Credit;
					worksheet.Cell(row, 4).Value = transactions[i].Debit;
					worksheet.Cell(row, 5).Value = runningBalance;

					// Styling for transaction rows
					var rowRange = worksheet.Range(row, 1, row, 5);
					if (i % 2 == 0)
					{
						rowRange.Style.Fill.BackgroundColor = XLColor.LightCyan; // Alternate row color for better readability
					}
					rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					rowRange.Style.Border.OutsideBorderColor = XLColor.LightGray;

					// Align cells based on data type
					worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}

				// Add footer with summary or additional info if needed
				var lastRow = transactions.Count + 7;

				// Add additional summary row for total balance
				worksheet.Cell(lastRow, 4).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 5).Value = runningBalance;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Font.Bold = true;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Fill.BackgroundColor = XLColor.PaleGoldenrod;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Border.OutsideBorderColor = XLColor.DarkGreen;
				worksheet.Cell(lastRow, 5).Style.NumberFormat.SetNumberFormatId(4); // Format as currency
				worksheet.Cell(lastRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Auto-fit columns for better visibility
				worksheet.Columns().AdjustToContents();

				// Freeze panes for better navigation
				worksheet.SheetView.FreezeRows(6);
				workbook.Protect(vehicleCode); // Protect the workbook with a password

				// Save the workbook to a memory stream
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var content = stream.ToArray();  // Convert stream to byte array

				var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				// Return the byte array for the file
				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		//all payments for a particular vehicle in table customerTransactions

		/// <summary>
		/// The GetCustomerPayments
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{object}}"/></returns>
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
										  }).ToListAsync(
										 );
				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No transactions found for the specified vehicle", null);

				return ServiceResponse<object>.Success("Transactions found", transactions);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while fetching transactions", ex);
			}
		}

		/// <summary>
		/// The ExportCustomerTransactionsEplus
		/// </summary>
		/// <param name="vehicleCode">The vehicleCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{byte[]}}"/></returns>
		public async Task<ServiceResponse<byte[]>> ExportCustomerTransactionsEplus(string vehicleCode)
		{
			try
			{
				// Check if vehicle exists
				var isVehicleExist = await _context.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
				if (!isVehicleExist)
					return ServiceResponse<byte[]>.Information("Vehicle does not exist", null);

				// Join vehicle with customer and get details
				var customer = await (from v in _context.Vehicles
									  join c in _context.Customers on v.CustomerCode equals c.CustomerCode
									  where v.VehicleCode == vehicleCode
									  select new
									  {
										  c.CustomerName,
										  v.VehicleRegistrationNumber,
										  c.CustomerPhone
									  }).FirstOrDefaultAsync();
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer of the vehicle not found", null);

				var transactions = await _context.CustomerTransactions
					.Where(x => x.VehicleCode == vehicleCode)
					.OrderBy(x => x.DateCreated)
					.ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified vehicle", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add($"{customer.VehicleRegistrationNumber}");

				// Set title style
				var titleRange = worksheet.Range("A1:E1");
				titleRange.Merge().Value = "Wallet Statement";
				titleRange.Style.Font.Bold = true;
				titleRange.Style.Font.FontSize = 18;
				titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				titleRange.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
				titleRange.Style.Font.FontColor = XLColor.White;
				titleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				titleRange.Style.Border.OutsideBorderColor = XLColor.DarkBlue;

				// Add customer details header with enhanced styling
				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Vehicle Registration:";
				worksheet.Cell(4, 2).Value = customer.VehicleRegistrationNumber;

				// Styling for customer details
				var customerDetailsRange = worksheet.Range("A2:B4");
				customerDetailsRange.Style.Font.Bold = true;
				customerDetailsRange.Style.Fill.BackgroundColor = XLColor.LightGray;
				customerDetailsRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				customerDetailsRange.Style.Border.OutsideBorderColor = XLColor.Black;
				customerDetailsRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Add headers for transaction details with enhanced styling
				worksheet.Cell(6, 1).Value = "Transaction Reference";
				worksheet.Cell(6, 2).Value = "Date Created";
				worksheet.Cell(6, 3).Value = "Credit";
				worksheet.Cell(6, 4).Value = "Debit";
				worksheet.Cell(6, 5).Value = "Running Balance";

				var headerRange = worksheet.Range("A6:E6");
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Font.FontColor = XLColor.White;
				headerRange.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
				headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
				headerRange.Style.Border.BottomBorderColor = XLColor.DarkBlue;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Add data to the worksheet with styling
				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 3).Style.NumberFormat.SetNumberFormatId(4); // Format as currency
					worksheet.Cell(row, 4).Style.NumberFormat.SetNumberFormatId(4);
					worksheet.Cell(row, 5).Style.NumberFormat.SetNumberFormatId(4);

					worksheet.Cell(row, 1).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 2).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 3).Value = transactions[i].Credit;
					worksheet.Cell(row, 4).Value = transactions[i].Debit;
					worksheet.Cell(row, 5).Value = runningBalance;

					// Styling for transaction rows
					var rowRange = worksheet.Range(row, 1, row, 5);
					if (i % 2 == 0)
					{
						rowRange.Style.Fill.BackgroundColor = XLColor.LightCyan; // Alternate row color for better readability
					}
					rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					rowRange.Style.Border.OutsideBorderColor = XLColor.LightGray;

					// Align cells based on data type
					worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}

				// Add footer with summary or additional info if needed
				var lastRow = transactions.Count + 7;

				// Add additional summary row for total balance
				worksheet.Cell(lastRow, 4).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 5).Value = runningBalance;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Font.Bold = true;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Fill.BackgroundColor = XLColor.PaleGoldenrod;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				worksheet.Range(lastRow, 4, lastRow, 5).Style.Border.OutsideBorderColor = XLColor.DarkGreen;
				worksheet.Cell(lastRow, 5).Style.NumberFormat.SetNumberFormatId(4); // Format as currency
				worksheet.Cell(lastRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Auto-fit columns for better visibility
				worksheet.Columns().AdjustToContents();

				// Freeze panes for better navigation
				worksheet.SheetView.FreezeRows(6);

				// Save the workbook to a memory stream
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var content = stream.ToArray();  // Convert stream to byte array

				// Return the byte array for the file
				var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		/// <summary>
		/// The CustomerStatementAsPdf
		/// </summary>
		/// <param name="customerCode">The customerCode<see cref="string"/></param>
		/// <param name="from">The from<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{byte[]}}"/></returns>
		public async Task<ServiceResponse<byte[]>> CustomerStatementAsPdf(string customerCode, DateTime from)
		{
			try
			{
				var customer = await GetCustomerByCodeAsync(customerCode);
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer not found", null);

				var transactions = await GetCustomerTransactionsAsync(customerCode, from);
				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				using var excelPackage = new ExcelPackage();
				var worksheet = excelPackage.Workbook.Worksheets.Add($"{customer.CustomerName}");

				// Set title, customer details, headers, and transactions
				ApplyTitleStyle(worksheet, "Wallet Statement");
				AddCustomerDetails(worksheet, customer);
				AddTransactionHeaders(worksheet);

				decimal runningBalance = PopulateTransactions(worksheet, transactions);
				AddTotalRunningBalanceRow(worksheet, transactions.Count + 7, runningBalance);
				FinalizeWorksheetFormatting(worksheet);

				// Save the Excel worksheet to a memory stream
				using var excelStream = new MemoryStream();
				excelPackage.SaveAs(excelStream);

				// Convert Excel to PDF using Syncfusion
				excelStream.Position = 0; // Reset the stream position to the beginning

				using var excelEngine = new ExcelEngine();
				var application = excelEngine.Excel;
				application.DefaultVersion = ExcelVersion.Excel2016;

				// Load the Excel stream into a Syncfusion workbook
				var workbook = application.Workbooks.Open(excelStream);
				var worksheetToConvert = workbook.Worksheets[0];

				// Set page layout to fit content on one page
				worksheetToConvert.PageSetup.Orientation = ExcelPageOrientation.Landscape;
				worksheetToConvert.PageSetup.FitToPagesWide = 1; // Fit to one page wide
				worksheetToConvert.PageSetup.FitToPagesTall = 1; // Fit to one page tall

				// Set up PDF document and conversion settings
				var pdfDocument = new PdfDocument();
				var converter = new XlsIORenderer();
				var pdfDocumentSettings = new XlsIORendererSettings
				{
					IsConvertBlankPage = false,
					TemplateDocument = pdfDocument,
				};

				// Convert the worksheet to PDF
				var pdf = converter.ConvertToPDF(worksheetToConvert, pdfDocumentSettings);

				// Save the PDF to a byte array
				using var pdfStream = new MemoryStream();
				pdf.Save(pdfStream);
				pdfStream.Position = 0;
				var pdfBytes = pdfStream.ToArray();

				var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<byte[]>.Success("Customer statement exported successfully as PDF", pdfBytes);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				// Add logging here (e.g., log the exception message and stack trace)
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement as PDF", null);
			}
		}

		/// <summary>
		/// The TransferCustomerBalance
		/// </summary>
		/// <param name="transferCustomerBalanceDto">The transferCustomerBalanceDto<see cref="TransferCustomerBalanceDto"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		public async Task<ServiceResponse> TransferCustomerBalance(TransferCustomerBalanceDto transferCustomerBalanceDto)
		{
			if (string.IsNullOrEmpty(transferCustomerBalanceDto.FromVehicleCode) || string.IsNullOrEmpty(transferCustomerBalanceDto.ToVehicleCode))
				return ServiceResponse<object>.Information("Kindly provide both the from and to vehicle registration numbers");

			var isFromVehicleExist = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == transferCustomerBalanceDto.FromVehicleCode);
			if (isFromVehicleExist is null)
				return ServiceResponse<object>.Information("From vehicle does not exist");

			var isToVehicleExist = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == transferCustomerBalanceDto.ToVehicleCode);
			if (isToVehicleExist is null)
				return ServiceResponse<object>.Information("To vehicle does not exist");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var narration = $"{transferCustomerBalanceDto.Amount:N2} transfered from vehicle {isFromVehicleExist.VehicleRegistrationNumber} To {isToVehicleExist.VehicleRegistrationNumber} ";
				var fromTransaction = CreateCustomerTransaction(transferCustomerBalanceDto.FromVehicleCode, 0, transferCustomerBalanceDto.Amount, "", 5, narration);
				_context.CustomerTransactions.Add(fromTransaction);

				var toTransaction = CreateCustomerTransaction(transferCustomerBalanceDto.ToVehicleCode, transferCustomerBalanceDto.Amount, 0, "", 5, narration);
				_context.CustomerTransactions.Add(toTransaction);

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				var message = $"{_authentication.Name()} Transfered {transferCustomerBalanceDto.Amount:N2} from {transferCustomerBalanceDto.FromVehicleCode} to {transferCustomerBalanceDto.ToVehicleCode} on {DateTime.UtcNow} ";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Customer Balance Transferred Successfully");
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("An error occurred while transferring the balance: " + ex.Message);
			}
		}

		//get the station Name from the shift table using the transaction shift

		/// <summary>
		/// The GetStationName
		/// </summary>
		/// <param name="shiftNumber">The shiftNumber<see cref="string"/></param>
		/// <returns>The <see cref="Task{string}"/></returns>
		private async Task<string> GetStationName(string shiftNumber)
		{
			var stationName = await (from sh in _context.Shifts
									 join d in _context.Dispensers on sh.DispenserCode equals d.DispenserCode
									 join s in _context.Stations on d.StationCode equals s.StationCode
									 where sh.ShiftNumber == shiftNumber
									 select s.StationName).FirstOrDefaultAsync();
			return stationName ?? string.Empty;
		}

		//Well paginated wallet balance with filters

		/// <summary>
		/// The GetCustomerBalances
		/// </summary>
		/// <param name="registrationNumber">The registrationNumber<see cref="string?"/></param>
		/// <param name="customerName">The customerName<see cref="string?"/></param>
		/// <param name="pageNumber">The pageNumber<see cref="int"/></param>
		/// <param name="pageSize">The pageSize<see cref="int"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{object}}"/></returns>
		public async Task<ServiceResponse<object>> GetCustomerBalances(string? registrationNumber = null, string? customerName = null, int pageNumber = 1, int pageSize = 15)
		{
			var response = new ServiceResponse<List<CustomerBalanceDto>>();
			try
			{
				// Base query to retrieve customer balances with joins
				var query = _context.CustomerTransactions
					.Join(_context.Vehicles,
						ct => ct.VehicleCode,
						v => v.VehicleCode,
						(ct, v) => new { ct, v })
					.Join(_context.Customers,
						cv => cv.v.CustomerCode,
						c => c.CustomerCode,
						(cv, c) => new
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

				// Apply filters based on input parameters
				if (!string.IsNullOrEmpty(registrationNumber))
				{
					query = query.Where(q => q.RegistrationNumber.Contains(registrationNumber));
				}

				if (!string.IsNullOrEmpty(customerName))
				{
					query = query.Where(q => q.CustomerName.Contains(customerName));
				}

				// Calculate the total number of records before pagination
				var totalRecords = await query.CountAsync();

				// Paginate the query
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
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error("Something went wrong contact system admin", ex);
			}
		}

		/// <summary>
		/// The CustomerStatement
		/// </summary>
		/// <param name="customerCode">The customerCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{byte[]}}"/></returns>
		public async Task<ServiceResponse<byte[]>> CustomerStatement(string customerCode)
		{
			try
			{
				var customer = await (from x in _context.Customers
									  where x.CustomerCode == customerCode
									  select x
										   ).FirstOrDefaultAsync();
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer not found", null);

				var transactions = await (from c in _context.CustomerTransactions
										  join v in _context.Vehicles on c.VehicleCode equals v.VehicleCode
										  where v.CustomerCode == customerCode
										  select new
										  {
											  v.VehicleRegistrationNumber,
											  c.TransactionReference,
											  c.DateCreated,
											  c.Credit,
											  c.Debit,
											  c.VehicleCode
										  }).ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add($"{customer.CustomerName}");

				// Set title style
				var titleRange = worksheet.Range("A1:G1");
				titleRange.Merge().Value = "Wallet Statement";
				titleRange.Style.Font.Bold = true;
				titleRange.Style.Font.FontSize = 20;
				titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				titleRange.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
				titleRange.Style.Font.FontColor = XLColor.White;
				titleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				titleRange.Style.Border.OutsideBorderColor = XLColor.DarkBlue;

				// Add customer details header with enhanced styling

				worksheet.Cell(2, 1).Value = "Customer Name:";
				worksheet.Cell(2, 2).Value = customer.CustomerName;
				worksheet.Cell(3, 1).Value = "Phone Number:";
				worksheet.Cell(3, 2).Value = customer.CustomerPhone;
				worksheet.Cell(4, 1).Value = "Customer Email:";
				worksheet.Cell(4, 2).Value = customer.CustomerEmail;

				// Styling for customer details
				var customerDetailsRange = worksheet.Range("A2:B4");
				customerDetailsRange.Style.Font.Bold = true;
				customerDetailsRange.Style.Fill.BackgroundColor = XLColor.LightGray;
				customerDetailsRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
				customerDetailsRange.Style.Border.OutsideBorderColor = XLColor.Black;
				customerDetailsRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Add headers for transaction details with enhanced styling

				worksheet.Cell(6, 1).Value = "Row";
				worksheet.Cell(6, 2).Value = "Registration Number";
				worksheet.Cell(6, 3).Value = "Transaction Reference";
				worksheet.Cell(6, 4).Value = "Date Created";
				worksheet.Cell(6, 5).Value = "Credit";
				worksheet.Cell(6, 6).Value = "Debit";
				worksheet.Cell(6, 7).Value = "Running Balance";

				var headerRange = worksheet.Range("A6:G6");
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Font.FontColor = XLColor.White;
				headerRange.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
				headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
				headerRange.Style.Border.BottomBorderColor = XLColor.DarkBlue;
				headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Add data to the worksheet with styling
				decimal runningBalance = 0;
				for (int i = 0; i < transactions.Count; i++)
				{
					runningBalance += transactions[i].Credit - transactions[i].Debit;
					var row = i + 7;

					worksheet.Cell(row, 7).Style.NumberFormat.SetNumberFormatId(2);
					worksheet.Cell(row, 6).Style.NumberFormat.SetNumberFormatId(2);
					worksheet.Cell(row, 5).Style.NumberFormat.SetNumberFormatId(2);
					worksheet.Cell(row, 4).Style.NumberFormat.SetNumberFormatId(22); // Format as text
					worksheet.Cell(row, 3).Style.NumberFormat.SetNumberFormatId(49); // Format as text
					worksheet.Cell(row, 2).Style.NumberFormat.SetNumberFormatId(49); // Format as text
					worksheet.Cell(row, 1).Style.NumberFormat.SetNumberFormatId(0);

					worksheet.Cell(row, 1).Value = i + 1;
					worksheet.Cell(row, 2).Value = transactions[i].VehicleRegistrationNumber;
					worksheet.Cell(row, 3).Value = transactions[i].TransactionReference;
					worksheet.Cell(row, 4).Value = transactions[i].DateCreated;
					worksheet.Cell(row, 5).Value = transactions[i].Credit;
					worksheet.Cell(row, 6).Value = transactions[i].Debit;
					worksheet.Cell(row, 7).Value = runningBalance;

					// Styling for transaction rows
					var rowRange = worksheet.Range(row, 1, row, 5);
					if (i % 2 == 0)
					{
						rowRange.Style.Fill.BackgroundColor = XLColor.LightCyan; // Alternate row color for better readability
					}
					rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					rowRange.Style.Border.OutsideBorderColor = XLColor.LightGray;

					// Align cells based on data type
					worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					worksheet.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}

				// Add footer with summary or additional info if needed
				var lastRow = transactions.Count + 7;

				// Add additional summary row for total balance
				worksheet.Cell(lastRow, 6).Value = "Total Running Balance:";
				worksheet.Cell(lastRow, 7).Value = runningBalance;
				worksheet.Range(lastRow, 6, lastRow, 7).Style.Font.Bold = true;
				worksheet.Range(lastRow, 6, lastRow, 7).Style.Fill.BackgroundColor = XLColor.PaleGoldenrod;
				worksheet.Range(lastRow, 6, lastRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				worksheet.Range(lastRow, 6, lastRow, 7).Style.Border.OutsideBorderColor = XLColor.DarkGreen;
				worksheet.Cell(lastRow, 7).Style.NumberFormat.SetNumberFormatId(2); // Format as currency
				worksheet.Cell(lastRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

				// Auto-fit columns for better visibility
				worksheet.Columns().AdjustToContents();
				worksheet.Rows().AdjustToContents();
				// Freeze panes for better navigation
				worksheet.SheetView.FreezeRows(6);

				//workbook.Protect(); // Protect the workbook with a password

				// Save the workbook to a memory stream
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var content = stream.ToArray();  // Convert stream to byte array

				// Return the byte array for the file
				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		//top up Customer Funds

		/// <summary>
		/// The TopUpFundssWallet
		/// </summary>
		/// <param name="customerFunds">The customerFunds<see cref="TopUpFundsDto"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		public async Task<ServiceResponse> TopUpFundssWallet(TopUpFundsDto customerFunds)
		{
			try
			{
				//get customer
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer does not exist");

				//check if the customer has a vehicle
				var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Customer does not have a vehicle");

				// insert into CustomerFunds
				var customerFund = new CustomerFunds
				{
					CustomerCode = customerFunds.CustomerCode,
					Credit = customerFunds.Amount,
					SystemReference = _setups.GenerateSaleId(),
					Debit = 0,
					UserCode = _authentication.Usercode(),
					Narration = $"Funds top up",
					DateCreated = DateTime.UtcNow,
					UserReference = customerFunds.TransactionReference
				};

				var message = $"Customer funds topped up by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				_context.CustomerFunds.Add(customerFund);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Customer funds topped up successfully");
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error("An error occurred while topping up customer funds");
			}
		}

		//reverse TopUpCustomerWallet

		/// <summary>
		/// The ReverseTopUpFundssWallet
		/// </summary>
		/// <param name="customerFunds">The customerFunds<see cref="TopUpFundsDto"/></param>
		/// <returns>The <see cref="Task{ServiceResponse}"/></returns>
		public async Task<ServiceResponse> ReverseTopUpFundssWallet(TopUpFundsDto customerFunds)
		{
			try
			{
				//get customer
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer does not exist");

				//check if the customer has a vehicle
				var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.CustomerCode == customerFunds.CustomerCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Customer does not have a vehicle");

				// insert into CustomerFunds
				var customerFund = new CustomerFunds
				{
					CustomerCode = customerFunds.CustomerCode,
					Debit = customerFunds.Amount,
					SystemReference = _setups.GenerateSaleId(),
					Credit = 0,
					UserCode = _authentication.Usercode(),
					Narration = $"Funds reversed",
					DateCreated = DateTime.UtcNow,
					UserReference = customerFunds.TransactionReference
				};

				var message = $"Customer funds reversed by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				_context.CustomerFunds.Add(customerFund);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Customer funds reversed successfully");
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error("An error occurred while reversing customer funds");
			}
		}

		/// <summary>
		/// The CustomerStatement2
		/// </summary>
		/// <param name="customerCode">The customerCode<see cref="string"/></param>
		/// <param name="from">The from<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{ServiceResponse{byte[]}}"/></returns>
		public async Task<ServiceResponse<byte[]>> CustomerStatement2(string customerCode, DateTime from)
		{
			try
			{
				var customer = await GetCustomerByCodeAsync(customerCode);
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer not found", null);

				var transactions = await GetCustomerTransactionsAsync(customerCode, from);
				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				transactions.OrderBy(x => x.DateCreated);
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add($"{customer.CustomerName}");

				ApplyTitleStyle(worksheet, "Wallet Statement");
				AddCustomerDetails(worksheet, customer);
				AddTransactionHeaders(worksheet);

				decimal runningBalance = PopulateTransactions(worksheet, transactions);
				AddTotalRunningBalanceRow(worksheet, transactions.Count + 7, runningBalance);
				FinalizeWorksheetFormatting(worksheet);

				var content = SaveWorkbookToByteArray(package);
				return ServiceResponse<byte[]>.Success("Customer statement exported successfully", content);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement", null);
			}
		}

		// Get the customer by code

		/// <summary>
		/// The GetCustomerByCodeAsync
		/// </summary>
		/// <param name="customerCode">The customerCode<see cref="string"/></param>
		/// <returns>The <see cref="Task{Customer}"/></returns>
		private async Task<Customer> GetCustomerByCodeAsync(string customerCode)
		{
			return await _context.Customers
				.FirstOrDefaultAsync(c => c.CustomerCode == customerCode) ?? new Customer();
		}

		// Get customer transactions

		/// <summary>
		/// The GetCustomerTransactionsAsync
		/// </summary>
		/// <param name="customerCode">The customerCode<see cref="string"/></param>
		/// <param name="from">The from<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{List{TransactionDto}}"/></returns>
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

		// Apply title style to the worksheet

		/// <summary>
		/// The ApplyTitleStyle
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		/// <param name="title">The title<see cref="string"/></param>
		private static void ApplyTitleStyle(ExcelWorksheet worksheet, string title)
		{
			worksheet.Cells["A1:G1"].Merge = true;
			worksheet.Cells["A1"].Value = title;
			worksheet.Cells["A1"].Style.Font.Bold = true;
			worksheet.Cells["A1"].Style.Font.Size = 20;
			worksheet.Cells["A1"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)Syncfusion.XlsIO.ExcelHorizontalAlignment.Center;
			worksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(93, 138, 168)); // AirForceBlue
			worksheet.Cells["A1"].Style.Font.Color.SetColor(Color.White);
			worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thick, Color.DarkBlue);
		}

		// Add customer details

		/// <summary>
		/// The AddCustomerDetails
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		/// <param name="customer">The customer<see cref="Customer"/></param>
		private static void AddCustomerDetails(ExcelWorksheet worksheet, Customer customer)
		{
			worksheet.Cells["A2"].Value = "Customer Name:";
			worksheet.Cells["B2"].Value = customer.CustomerName;
			worksheet.Cells["A3"].Value = "Phone Number:";
			worksheet.Cells["B3"].Value = customer.CustomerPhone;
			worksheet.Cells["A4"].Value = "Customer Email:";
			worksheet.Cells["B4"].Value = customer.CustomerEmail;

			// Styling for customer details
			var customerDetailsRange = worksheet.Cells["A2:B4"];
			customerDetailsRange.Style.Font.Bold = true;
			customerDetailsRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
			customerDetailsRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
			customerDetailsRange.Style.Border.BorderAround(ExcelBorderStyle.Thick, Color.Black);

			worksheet.Cells["A2:A4"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)Syncfusion.XlsIO.ExcelHorizontalAlignment.Right;
		}

		// Add headers for transaction details

		/// <summary>
		/// The AddTransactionHeaders
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		private static void AddTransactionHeaders(ExcelWorksheet worksheet)
		{
			worksheet.Cells["A6"].Value = "Row";
			worksheet.Cells["B6"].Value = "Registration Number";
			worksheet.Cells["C6"].Value = "Transaction Reference";
			worksheet.Cells["D6"].Value = "Date Created";
			worksheet.Cells["E6"].Value = "Credit";
			worksheet.Cells["F6"].Value = "Debit";
			worksheet.Cells["G6"].Value = "Running Balance";

			var headerRange = worksheet.Cells["A6:G6"];
			headerRange.Style.Font.Bold = true;
			headerRange.Style.Font.Color.SetColor(Color.White);
			headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
			headerRange.Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
			headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
			headerRange.Style.Border.Bottom.Color.SetColor(Color.DarkBlue);
			headerRange.Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)Syncfusion.XlsIO.ExcelHorizontalAlignment.Center;
		}

		// Populate transaction details in the worksheet

		/// <summary>
		/// The PopulateTransactions
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		/// <param name="transactions">The transactions<see cref="List{TransactionDto}"/></param>
		/// <returns>The <see cref="decimal"/></returns>
		private static decimal PopulateTransactions(ExcelWorksheet worksheet, List<TransactionDto> transactions)
		{
			transactions.OrderBy(x => x.DateCreated);
			decimal runningBalance = 0;
			for (int i = 0; i < transactions.Count; i++)
			{
				runningBalance += transactions[i].Credit - transactions[i].Debit;
				var row = i + 7;

				worksheet.Cells[row, 1].Value = i + 1;
				worksheet.Cells[row, 2].Value = transactions[i].VehicleRegistrationNumber;
				worksheet.Cells[row, 3].Value = transactions[i].TransactionReference;
				worksheet.Cells[row, 4].Value = transactions[i].DateCreated;
				worksheet.Cells[row, 5].Value = transactions[i].Credit;
				worksheet.Cells[row, 6].Value = transactions[i].Debit;
				worksheet.Cells[row, 7].Value = runningBalance;
				worksheet.Cells[row, 5, row, 7].Style.Numberformat.Format = "#,##0.00"; // Format as currency
				worksheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-mm-dd"; // Format as date

				ApplyRowStyling(worksheet, row, i);
			}
			return runningBalance;
		}

		// Apply row styling for transaction rows

		/// <summary>
		/// The ApplyRowStyling
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		/// <param name="row">The row<see cref="int"/></param>
		/// <param name="index">The index<see cref="int"/></param>
		private static void ApplyRowStyling(ExcelWorksheet worksheet, int row, int index)
		{
			if (index % 2 == 0)
			{
				worksheet.Cells[row, 1, row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
				worksheet.Cells[row, 1, row, 7].Style.Fill.BackgroundColor.SetColor(Color.LightCyan);
			}
			worksheet.Cells[row, 1, row, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.LightGray);
			worksheet.Cells[row, 1, row, 7].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)Syncfusion.XlsIO.ExcelHorizontalAlignment.Left;
		}

		// Add a summary row for the total running balance

		/// <summary>
		/// The AddTotalRunningBalanceRow
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		/// <param name="lastRow">The lastRow<see cref="int"/></param>
		/// <param name="runningBalance">The runningBalance<see cref="decimal"/></param>
		private static void AddTotalRunningBalanceRow(ExcelWorksheet worksheet, int lastRow, decimal runningBalance)
		{
			worksheet.Cells[lastRow, 6].Value = "Total Running Balance:";
			worksheet.Cells[lastRow, 7].Value = runningBalance;
			worksheet.Cells[lastRow, 6, lastRow, 7].Style.Font.Bold = true;
			worksheet.Cells[lastRow, 6, lastRow, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells[lastRow, 6, lastRow, 7].Style.Fill.BackgroundColor.SetColor(Color.PaleGoldenrod);
			worksheet.Cells[lastRow, 6, lastRow, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.DarkGreen);
			worksheet.Cells[lastRow, 7].Style.Numberformat.Format = "#,##0.00"; // Format as currency
			worksheet.Cells[lastRow, 7].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)ExcelHorizontalAlignment.Right;
		}

		// Finalize the worksheet formatting

		/// <summary>
		/// The FinalizeWorksheetFormatting
		/// </summary>
		/// <param name="worksheet">The worksheet<see cref="ExcelWorksheet"/></param>
		private static void FinalizeWorksheetFormatting(ExcelWorksheet worksheet)
		{
			worksheet.Cells.AutoFitColumns();
			worksheet.View.FreezePanes(7, 1); // Freeze the header row
		}

		// Save the workbook to a byte array

		/// <summary>
		/// The SaveWorkbookToByteArray
		/// </summary>
		/// <param name="package">The package<see cref="ExcelPackage"/></param>
		/// <returns>The <see cref="byte[]"/></returns>
		private byte[] SaveWorkbookToByteArray(ExcelPackage package)
		{
			return package.GetAsByteArray();
		}

		/// <summary>
		/// The TopUpTypes
		/// </summary>
		/// <returns>The <see cref="Task{ServiceResponse{List{TopUpTypesDto}}}"/></returns>
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
	}

	/// <summary>
	/// Defines the <see cref="PaymentTypeDto" />
	/// </summary>
	public class PaymentTypeDto
	{
		/// <summary>
		/// Gets or sets the PaymentTypeCode
		/// </summary>
		public int PaymentTypeCode { get; set; }

		/// <summary>
		/// Gets or sets the PaymentTypeName
		/// </summary>
		public string PaymentTypeName { get; set; } = string.Empty;
	}

	/// <summary>
	/// Defines the <see cref="TopUpTypesDto" />
	/// </summary>
	public class TopUpTypesDto
	{
		/// <summary>
		/// Gets or sets the TopUpTypeId
		/// </summary>
		public int TopUpTypeId { get; set; }

		/// <summary>
		/// Gets or sets the TopUpTypeName
		/// </summary>
		public string TopUpTypeName { get; set; } = string.Empty;
	}

	/// <summary>
	/// Defines the <see cref="TransactionDto" />
	/// </summary>
	public class TransactionDto
	{
		/// <summary>
		/// Gets or sets the VehicleRegistrationNumber
		/// </summary>
		public string VehicleRegistrationNumber { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the DateCreated
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Gets or sets the Credit
		/// </summary>
		[Precision(18, 2)]
		public decimal Credit { get; set; }

		/// <summary>
		/// Gets or sets the Debit
		/// </summary>
		[Precision(18, 2)]
		public decimal Debit { get; set; }

		/// <summary>
		/// Gets or sets the Balance
		/// </summary>
		[Precision(18, 2)]
		public decimal Balance { get; set; }

		/// <summary>
		/// Gets or sets the RunningBalance
		/// </summary>
		[Precision(18, 2)]
		public decimal RunningBalance { get; set; }

		/// <summary>
		/// Gets or sets the TransactionReference
		/// </summary>
		public string TransactionReference { get; set; } = string.Empty;
	}

}
