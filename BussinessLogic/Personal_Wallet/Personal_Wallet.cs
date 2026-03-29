using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.EmailService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Personal_Wallet;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using BussinessLogic.Setup;
using DataAccessLayer.Authentication.Entity;

namespace BussinessLogic.Personal_Wallet
{
	public class Personal_Wallet
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly OTOContext _context;
		private readonly IMessagingService _messaging;
		private readonly IEmailService _emailService;
		private readonly ILogger<Personal_Wallet> _logger;
		private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

		public Personal_Wallet(IAuthCommonTasks authentication, ICommonSetups setups, OTOContext context, IMessagingService messaging, IEmailService emailService, ILogger<Personal_Wallet> logger, IPasswordHasher<ApplicationUser> passwordHasher)
		{
			_authentication = authentication;
			_setups = setups;
			_context = context;
			_messaging = messaging;
			_emailService = emailService;
			_logger = logger;
			_passwordHasher = passwordHasher;
		}
		public async Task<ServiceResponse<object>> AddCustomerAsync(Personal_Wallet_CustomerDto customer)
		{
			try
			{
				var normalizedPhoneNumber = _messaging.NormalizePhoneNumber(customer.PhoneNumber);
				if (normalizedPhoneNumber is null)
					return ServiceResponse<object>.Information("Invalid phone number", null);

				if (string.IsNullOrWhiteSpace(customer.Email) || !customer.Email.Contains('@'))
					return ServiceResponse<object>.Information("Invalid email address", null);

				if (await _context.Personal_Wallet_Customers.AnyAsync(x => x.PhoneNumber == normalizedPhoneNumber || x.Email == customer.Email || x.IdentificationNumber == customer.IdentificationNumber))
					return ServiceResponse<object>.Information("Customer already exists", null);

				var walletId = await _setups.GetCodeGenerator("UserCode");
				var account = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == normalizedPhoneNumber);
				bool hasAccount = account is not null;

				using var transaction = await _context.Database.BeginTransactionAsync();
				try
				{
					ApplicationUser user = account!;
					if (!hasAccount)
					{
						user = new ApplicationUser
						{
							UserName = normalizedPhoneNumber,
							Email = customer.Email,
							PhoneNumber = normalizedPhoneNumber,
							IsActive = true,
							DateCreated = DateTime.UtcNow,
							UserCode = walletId,
							DateModified = DateTime.UtcNow,
							ConcurrencyStamp = Guid.NewGuid().ToString(),
							AccessFailedCount = 0,
							EmailConfirmed = true,
							NormalizedEmail = customer.Email.ToUpperInvariant(),
							NormalizedUserName = normalizedPhoneNumber.ToUpperInvariant(),
							SecurityStamp = Guid.NewGuid().ToString(),
							PasswordHash = _passwordHasher.HashPassword(null!, normalizedPhoneNumber), // Temporary default password
							FirstName = customer.FirstName,
							LastName = customer.LastName,
							MiddName = customer.MiddleName,
							UserType = 2,
							PasswordLastUpdated = DateTime.UtcNow,
							PhoneNumberConfirmed = true,
							LockoutEnabled = true,
							TwoFactorEnabled = false,
							LockoutEnd = null,
							ModifiedBy = "System",
							CreatedBy = "System",
							PayrollNumber = "",
							StationCode = "",
							AccessApps = "05",
						};
						await _context.Users.AddAsync(user);
					}

					var wallet = new Personal_Wallet_Customers
					{
						FirstName = customer.FirstName,
						MiddleName = customer.MiddleName,
						LastName = customer.LastName,
						PhoneNumber = normalizedPhoneNumber,
						Email = customer.Email,
						IdentificationNumber = customer.IdentificationNumber,
						IsActive = true,
						Receive_Receipts = customer.Receive_Receipts,
						Receive_Statements = customer.Receive_Statements,
						Discount = 0,
						Credit = 0,
						WalletId = user!.UserCode,
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode(),
					};

											var fullName = $"{customer.FirstName} {customer.MiddleName}".Trim();
											var phone = normalizedPhoneNumber;
											var regNo = wallet.WalletId?.ToUpper();
											var now = DateTime.UtcNow;

											string sql = @"
						INSERT INTO Otogas..Vehicle (
							NameDriver, OwnerName, OwnerPhoneNumber, DriverPhoneNumber,RegNo, Status, PaymentPlan, Program, IsActive, Tankcapacity,
							DateCreated, CreatedBy, DateOfConversion, IsPrePayCustomer,ModifiedBy, Modified, AAcceptedPaybillNumber, LasstQuantityFueld,
							LastDDateFueled, IsUsingCard, IsAppoved
						)
						VALUES (
							@NameDriver, @OwnerName, @OwnerPhoneNumber, @DriverPhoneNumber,@RegNo, 'Active', 'Rental', 'Rental Plan', NULL, 0.00,@DateCreated, 
							NULL, @DateOfConversion, 1,NULL, NULL, 4113475, 12.00,@LastDDateFueled, 1, 1
						)";

					var parameters = new[]
					{
						new SqlParameter("@NameDriver", fullName),
						new SqlParameter("@OwnerName", fullName),
						new SqlParameter("@OwnerPhoneNumber", phone),
						new SqlParameter("@DriverPhoneNumber", phone),
						new SqlParameter("@RegNo", regNo), // Use wallet.WalletId if this is for `sql1`
						new SqlParameter("@DateCreated", now),
						new SqlParameter("@DateOfConversion", now),
						new SqlParameter("@LastDDateFueled", now)
					};

					await _context.Database.ExecuteSqlRawAsync(sql, parameters);


					await _context.Personal_Wallet_Customers.AddAsync(wallet);
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();

					var message = $"Dear {customer.FirstName}, welcome to our Otopay platform. Your wallet ID is {wallet.WalletId}. Kindly use it for topping up through the paybill 4113475.";
					await _messaging.SendSms(normalizedPhoneNumber ?? string.Empty, message);

					if (customer.Receive_Receipts || customer.Receive_Statements)
					{
						var subject = "Welcome to Otopay - Notifications Enabled";
						var emailContent = HtmlBody(customer.FirstName, wallet.WalletId ?? string.Empty, customer.Receive_Statements, customer.Receive_Receipts);
						_emailService.SendEmail(customer.Email, null, subject, emailContent);
					}

					return ServiceResponse<object>.Success($"The account {customer.FirstName} has been added successfully! Use reset button to set your password.", null);
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					_logger.LogError(ex, "Error occurred while creating the customer");
					return ServiceResponse<object>.Error("An error occurred while creating the customer.", null);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception in AddCustomerAsync");
				return ServiceResponse<object>.Error("Internal server error occurred.", null);
			}
		}

		static string HtmlBody(string Name, string walletId, bool Receive_Statements, bool Receive_Receipts)
		{
			var receiptsText = Receive_Receipts ? "<p style='color: green;'>✅ You have opted to receive transaction receipts.</p>" : "";
			var statementsText = Receive_Statements ? "<p style='color: green;'>✅ You have opted to receive account statements.</p>" : "";
			return $@"<html><head><meta charset='UTF-8'><title>Welcome to Otopay</title></head><body style='font-family: Arial, sans-serif; line-height: 1.6; background-color: #f4f4f4; padding: 20px;'><div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #ffffff;'><div style='text-align: center; font-size: 22px; font-weight: bold; color: #333;'>Welcome to Otopay!</div><div style='margin-top: 15px; font-size: 16px; color: #555;'><p>Dear <strong>{Name}</strong>,</p><p>Welcome to our Otopay platform! Your wallet ID is <span style='font-weight: bold; color: #0275d8;'>{walletId}</span>. Kindly use it for topping up through the paybill <span style='font-weight: bold; color: #0275d8;'>4113475</span>.</p>{receiptsText}{statementsText}<p style='color: red;'>Use the reset button to set your password.</p><p>Thank you for joining Otopay! 🎉</p></div><div style='margin-top: 20px; font-size: 14px; color: #777; text-align: center;'>For any assistance, contact our support team.</div></div></body></html>";
		}

		public async Task<ServiceResponse<object>> GetAllCustomersAsync(
			string? WalletId = null,
			string? PhoneNumber = null,
			string? name = null,
			string? IdentificationNumber = null,
			int page = 1,
			int pageSize = 20)
		{
			try
			{
				var query = _context.Personal_Wallet_Customers.AsQueryable();

				if (!string.IsNullOrWhiteSpace(WalletId))
				{
					query = query.Where(p => p.WalletId.ToLower().Contains(WalletId.ToLower()));
				}

				if (!string.IsNullOrWhiteSpace(PhoneNumber))
				{
					query = query.Where(p => p.PhoneNumber.ToLower().Contains(PhoneNumber.ToLower()));
				}

				if (!string.IsNullOrWhiteSpace(IdentificationNumber))
				{
					query = query.Where(p => p.IdentificationNumber.ToLower().Contains(IdentificationNumber.ToLower()));
				}

				if (!string.IsNullOrWhiteSpace(name))
				{
					var lowerName = name.Trim().ToLower();
					query = query.Where(c =>
						$"{c.FirstName} {c.MiddleName} {c.LastName}".ToLower().Contains(lowerName));
				}

				var totalCount = await query.CountAsync();

				var customers = await query
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				var result = new
				{
					TotalCount = totalCount,
					Page = page,
					PageSize = pageSize,
					TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
					Data = customers
				};

				return ServiceResponse<object>.Success("Customers retrieved successfully", result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in GetAllCustomersAsync");
				return ServiceResponse<object>.Error("Failed to retrieve customers.", null);
			}
		}




		//get customer balance return walletid,balance,identificationNumber,customerName

		public async Task<ServiceResponse<object>> GetCustomerBalanceAsync(string walletId)
		{
			try
			{
				var customer = await _context.Personal_Wallet_Customers
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.WalletId == walletId);

				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				var transactionSummary = await _context.Wallet_Transactions_Personal
					.Where(x => x.WalletId == walletId)
					.GroupBy(x => x.WalletId)
					.Select(g => new
					{
						TotalCredit = g.Sum(x => x.Credit),
						TotalDebit = g.Sum(x => x.Debit)
					})
					.FirstOrDefaultAsync();

				decimal balance = transactionSummary != null
					? transactionSummary.TotalCredit - transactionSummary.TotalDebit
					: 0;

				return ServiceResponse<object>.Success("Customer balance retrieved successfully", new
				{
					customer.WalletId,
					customer.IdentificationNumber,
					Name = string.Join(' ',customer.FirstName,customer.MiddleName,customer.LastName),
					Balance = balance
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"❌ Error in GetCustomerBalanceAsync for WalletId: {walletId}");
				return ServiceResponse<object>.Error("Failed to retrieve customer balance.", null);
			}
		}


		public async Task<ServiceResponse<object>> SearchCustomerByPhoneNumberAsync(string phoneNumber)
		{
			try
			{
				var phoneNumber2 = _messaging.NormalizePhoneNumber(phoneNumber);
				var customer = await _context.Personal_Wallet_Customers.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber2);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				return ServiceResponse<object>.Success("Customer found", customer);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in SearchCustomerByPhoneNumberAsync");
				return ServiceResponse<object>.Error("Failed to search customer.", null);
			}
		}

		public async Task<ServiceResponse<object>> SearchCustomerByWalletIdAsync(string walletId)
		{
			try
			{
				var customer = await _context.Personal_Wallet_Customers.FirstOrDefaultAsync(x => x.WalletId == walletId);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				return ServiceResponse<object>.Success("Customer found", customer);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in SearchCustomerByWalletIdAsync");
				return ServiceResponse<object>.Error("Failed to search customer.", null);
			}
		}


		public async Task<ServiceResponse<object>> SearchCustomerByNationalId(string nationalId)
		{
			try
			{
				var customer = await _context.Personal_Wallet_Customers.FirstOrDefaultAsync(x => x.IdentificationNumber == nationalId);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				return ServiceResponse<object>.Success("Customer found", customer);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in SearchCustomerByNationalId");
				return ServiceResponse<object>.Error("Failed to search customer.", null);
			}
		}
		public class Personal_Wallet_CustomerDto
		{
			public string FirstName { get; set; } = string.Empty;
			public string MiddleName { get; set; } = string.Empty;
			public string LastName { get; set; } = string.Empty;
			[Required, MaxLength(20), Unicode(false)]
			public string PhoneNumber { get; set; } = string.Empty;
			[Required, MaxLength(50), Unicode(false)]
			public string Email { get; set; } = string.Empty;
			[Required, MaxLength(20), Unicode(false)]
			public string IdentificationNumber { get; set; } = string.Empty;
			public bool Receive_Receipts { get; set; }
			public bool Receive_Statements { get; set; }
		}
	}
}
