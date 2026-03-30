using BusinessLogic.EmailService;
using BusinessLogic.SetupService;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.PlateDetection;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Customer;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.CustomerService
{
	public class Customers : ICustomers
	{
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ILogger<Customer> _logger;
		private readonly IEmailService _emailService;
		private readonly IMessagingService _messaging;

		// Constructor to initialize dependencies
		public Customers(OTOContext context, ICommonSetups setups, IAuthCommonTasks authentication, ILogger<Customer> logger,
			IEmailService emailService, IMessagingService messaging)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_logger = logger;
			_emailService = emailService;
			_messaging = messaging;

		}

		/// <summary>
		/// Adds a new customer to the database if they do not already exist.
		/// Checks for existing customer based on email, phone, or KRAPin.
		/// Generates a unique customer code and saves the customer details.
		/// Logs the action and commits the transaction.
		/// </summary>
		/// <param name="customerDTO">DTO containing customer details</param>
		/// <returns>ServiceResponse<object> indicating the result of the operation</returns>
		public async Task<ServiceResponse<object>> AddCustomer(CustomerDTO customerDTO)
		{
			try
			{
				if (customerDTO == null)
					return ServiceResponse<object>.Information("Invalid request payload", null);

				// ---------------- VALIDATIONS ----------------

				// Phone validation
				if (!_messaging.IsValidPhoneNumber(customerDTO.CustomerPhone))
					return ServiceResponse<object>.Information("The phone number provided is not valid", null);

				// Email validation
				if (string.IsNullOrWhiteSpace(customerDTO.CustomerEmail) || !customerDTO.CustomerEmail.Contains('@'))
					return ServiceResponse<object>.Information("The email provided is not valid", null);

				customerDTO.CustomerEmail = customerDTO.CustomerEmail.Trim().ToLower();

				// KRA PIN validation
				static bool IsValidKRAPinFormat(string pin)
					=> !string.IsNullOrWhiteSpace(pin) && Regex.IsMatch(pin, @"^[A-Z]{1}\d{9}[A-Z]{1}$");

				if (!IsValidKRAPinFormat(customerDTO.Krapin))
					return ServiceResponse<object>.Information($"{customerDTO.Krapin?.ToUpper()} is an invalid KRA PIN format.", null);

				// ---------------- DUPLICATE CHECK ----------------

				var existingCustomer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x =>
					   x.CustomerEmail == customerDTO.CustomerEmail
					|| x.CustomerPhone == customerDTO.CustomerPhone
					|| x.KRAPin == customerDTO.Krapin);

				if (existingCustomer != null)
				{
					if (existingCustomer.CustomerEmail.Equals(customerDTO.CustomerEmail, StringComparison.OrdinalIgnoreCase))
						return ServiceResponse<object>.Information($"Customer email {customerDTO.CustomerEmail} already exists", null);

					if (existingCustomer.CustomerPhone == customerDTO.CustomerPhone)
						return ServiceResponse<object>.Information($"Customer phone number {customerDTO.CustomerPhone} already exists", null);

					if (existingCustomer.KRAPin == customerDTO.Krapin)
						return ServiceResponse<object>.Information($"Customer KRA PIN {customerDTO.Krapin} already exists", null);
				}

				// ---------------- GENERATE CUSTOMER CODE ----------------

				var custcode = await _setups.GetCodeGenerator("CustomerCode");
				if (custcode == null)
				{
					_logger.LogWarning(
						"Customer {customername} failed to add. Code generation failed. Added by {user} at {datetime}",
						customerDTO.CustomerName,
						_authentication.Name(),
						DateTime.UtcNow);

					return ServiceResponse<object>.Information("Customer not added", null);
				}

				// ---------------- CREATE ENTITIES ----------------

				var customer = new Customer
				{
					CustomerName = _setups.SentenceCase(customerDTO.CustomerName),
					IdentificationNumber = customerDTO.IdentificationNumber,
					CustomerPhone = customerDTO.CustomerPhone,
					CustomerEmail = customerDTO.CustomerEmail,
					CustomerCode = custcode.ToString(),
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					KRAPin = customerDTO.Krapin,
					OrganisationCode = customerDTO.OrganisationCode,
					CreditLimit = customerDTO.CreditLimit,
					IsCreditCustomer = customerDTO.IsCreditCustomer
				};

				var userTrail = new UserTrail
				{
					ActionType = "AddCustomer",
					Message =
					$@"New customer account created by {_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.
					Customer Details:
					- Name: {customer.CustomerName}
					- Code: {customer.CustomerCode}
					- Phone: {customer.CustomerPhone}
					- Email: {customer.CustomerEmail}
					- Identification No: {customer.IdentificationNumber}
					- KRA PIN: {customer.KRAPin}",
					UserName = _authentication.Name(),
					UserCode = _authentication.Usercode(),
					DateCreated = DateTime.UtcNow
				};

				// ---------------- EXECUTION STRATEGY + TRANSACTION ----------------

				var strategy = _context.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					await using var transaction = await _context.Database.BeginTransactionAsync();

					_context.Customers.Add(customer);
					_context.UserTrails.Add(userTrail);

					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}); 

				// ---------------- SUCCESS ----------------

				_logger.LogInformation(
					"Customer {customer} added successfully by {user} at {datetime}",
					customer.CustomerName,
					_authentication.Name(),
					DateTime.UtcNow);

				return ServiceResponse<object>.Success(
					$"Customer {customer.CustomerName} added successfully",
					customer);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex,
					"Failed to add customer {customername} by {user}",
					customerDTO?.CustomerName,
					_authentication.Name());

				return ServiceResponse<object>.Error(
					"An unexpected error occurred while adding the customer",
					null);
			}
		}
		//Return OrganisationTypes
		public static ServiceResponse<object> OrganisationTypes()
		{
			try
			{
				var types = Enum.GetValues(typeof(OrganisationType))
					.Cast<OrganisationType>()
					.Select(ot => new
					{
						Type = ot.ToString(),
						Value = (int)ot
					})
					.ToList();
				return ServiceResponse<object>.Success("Organisation types retrieved successfully", types);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong while fetching organisation types", "");
			}
		}


		//Register Organisations
		public async Task<ServiceResponse<object>> Organisations(RegisterOrganisationDTO register)
		{
			try
			{
				// Check if organisation exists
				var exist = await _context.Organisations
					.FirstOrDefaultAsync(o => o.OrganisationName == register.OrganisationName);

				if (exist != null)
				{
					return ServiceResponse<object>.Information($"Organisation with the name '{register.OrganisationName}' already exists", "");
				}

				var neworg = new Organisations
				{
					DateCreated = DateTime.UtcNow,
					OrganisationCode = await _setups.GetCodeGenerator("organisationCode"),
					OrganisationName = register.OrganisationName,
					OrganisationPhone = register.PhoneNumber,
					OrganisationEmail = register.EmailAddress, // ✅ fixed
					OrganisationType = register.OrganisationType
				};

				await _context.AddAsync(neworg);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Information($"Organisation '{register.OrganisationName}' has been registered successfully", "");
			}
			catch (Exception)
			{
				// Consider logging ex here
				return ServiceResponse<object>.Error("Something went wrong during registration", "");
			}
		}

		//list all Organisations
		public async Task<ServiceResponse<object>> OrganisationList()
		{
			try
			{
				var organisations = await _context.Organisations
					.Select(o => new OrganisationDTO
					{
						OrganisationCode = o.OrganisationCode,
						OrganisationName = o.OrganisationName,
						OrganisationPhone = o.OrganisationPhone,
						OrganisationEmail = o.OrganisationEmail,
						OrganisationType = o.OrganisationType.ToString(), // assuming the enum
						DateCreated = o.DateCreated
					})
					.ToListAsync();

				if (organisations == null || organisations.Count == 0)
				{
					return ServiceResponse<object>.Information("No organisations found", new List<OrganisationDTO>());
				}

				return ServiceResponse<object>.Success("Organisations retrieved successfully", organisations);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong while fetching organisations", "");
			}
		}


		public class OrganisationDTO
		{
			public string OrganisationCode { get; set; } = string.Empty;
			public string OrganisationName { get; set; } = string.Empty;
			public string OrganisationPhone { get; set; } = string.Empty;
			public string OrganisationEmail { get; set; } = string.Empty;
			public string OrganisationType { get; set; } = string.Empty;
			public DateTime DateCreated { get; set; }
		}


		///

		/// <summary>
		/// Retrieves a paginated list of customers from the database.
		/// Optionally filters customers by name or phone number.
		/// Returns total record count and the requested page of customers.
		/// </summary>
		/// <param name="customerName">Optional name filter</param>
		/// <param name="customerPhone">Optional phone filter</param>
		/// <param name="pageNumber">Page number to retrieve</param>
		/// <param name="pageSize">Number of customers per page</param>
		/// <returns>ServiceResponse<object> with the paginated list of customers</returns>
		public async Task<ServiceResponse<object>> GetAllCustomers(
			string? customerName = null,
			string? customerPhone = null,
			int pageNumber = 1,
			int pageSize = 10)
		{
			try
			{
				var customersQuery = _context.Customers
					.Select(c => new
					{
						c.CustomerName,
						c.CustomerPhone,
						c.CustomerCode,
						c.CustomerEmail,
						c.DateCreated,
						c.IdentificationNumber,
						c.KRAPin,
						c.BaseLoyaltyPoints,
						c.CreditLimit,

						// ✅ Credit Balance
						CreditBalance = _context.CreditTransactions
							.Where(x => x.CustomerCode == c.CustomerCode)
							.Sum(x => (decimal?)(x.Credit - x.Debit)) ?? 0,

						// ✅ Loyalty Balance
						LoyaltyBalance = _context.RoyaltyPoints
							.Where(x => x.CustomerCode == c.CustomerCode)
							.Sum(x => (int?)(x.PointsCredit - x.PointsDebit)) ?? 0
					});

				// ✅ Filters
				if (!string.IsNullOrEmpty(customerName))
				{
					customersQuery = customersQuery
						.Where(c => c.CustomerName.Contains(customerName));
				}

				if (!string.IsNullOrEmpty(customerPhone))
				{
					customersQuery = customersQuery
						.Where(c => c.CustomerPhone.Contains(customerPhone));
				}

				// ✅ Total count
				var totalRecords = await customersQuery.CountAsync();

				// ✅ Pagination + Sorting (IMPORTANT: sort before Skip)
				var customers = await customersQuery
					.OrderByDescending(c => c.DateCreated)
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				if (customers.Any())
				{
					var result = new
					{
						TotalRecords = totalRecords,
						PageNumber = pageNumber,
						PageSize = pageSize,
						Customers = customers
					};

					return ServiceResponse<object>.Success("Customers retrieved successfully", result);
				}

				return ServiceResponse<object>.Information("No customers found", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Information($"Error: {ex.Message}", null);
			}
		}

		/// <summary>
		/// Retrieves all vehicles associated with a specific customer code.
		/// </summary>
		/// <param name="customerCode">Unique customer code</param>
		/// <returns>ServiceResponse<object> containing the list of vehicles</returns>
		public async Task<ServiceResponse<object>> GetCustomerVehicles(string customerCode)
		{

			var performedBy = _authentication.Name() ?? "Unknown User";
			try
			{
				if (string.IsNullOrWhiteSpace(customerCode))
				{
					return ServiceResponse<object>.Information("Customer code is required", null);
				}

				var vehicles = await _context.Vehicles
					.Where(v => v.CustomerCode == customerCode)
					.ToListAsync();

				if (vehicles == null || vehicles.Count == 0)
				{

					return ServiceResponse<object>.Information(
						$"No vehicles found for customer code: {customerCode}",
						null
					);
				}

				await SaveUserTrailAsync(performedBy, "GetCustomerVehicles", $"Retrieved {vehicles.Count} vehicle(s) for customerCode: {customerCode}");

				return ServiceResponse<object>.Success($"Retrieved {vehicles.Count} vehicle(s) for customer code: {customerCode}", vehicles);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving vehicles for customerCode: {CustomerCode}", customerCode);

				await SaveUserTrailAsync(performedBy, "GetCustomerVehicles",
					$"Error occurred for customerCode: {customerCode}. Exception: {ex.Message}");

				return ServiceResponse<object>.Error(
					"An unexpected error occurred while retrieving vehicles. Please try again later.",
					null
				);
			}
		}

		/// <summary>
		/// Saves a record of user actions for auditing.
		/// </summary>
		private async Task SaveUserTrailAsync(string performedBy, string action, string details)
		{
			var trail = new UserTrail
			{
				UserName = performedBy,
				ActionType = action,
				Message = details,
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
			};

			_context.UserTrails.Add(trail);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Retrieves a list of customers whose names contain the specified search term.
		/// Limits the result to 10 customers.
		/// </summary>
		/// <param name="searchTerm">Search term to match customer names</param>
		/// <returns>ServiceResponse<object> containing the list of matching customers</returns>
		public async Task<ServiceResponse<object>> GetAllCustomers(string searchTerm)
		{
			return ServiceResponse<object>.Success("Customers retrieved successfully", await _context.Customers.Where(x => x.CustomerName.Contains(searchTerm)).Take(10).ToListAsync());
		}

		/// <summary>
		/// Updates the details of an existing customer.
		/// Performs null or whitespace checks on each field before updating.
		/// Logs a user trail of the update action.
		/// </summary>
		/// <param name="updateCustomer">DTO containing the updated customer details</param>
		/// <param name="customerCode">Unique code of the customer to update</param>
		/// <returns>ServiceResponse<object> indicating the result of the update operation</returns>
		public async Task<ServiceResponse<object>> UpdateCustomer(UpdateCustomerDTO updateCustomer, string customerCode)
		{
			try
			{
				if (updateCustomer == null)
					return ServiceResponse<object>.Information("Customer details cannot be empty", null);

				if (string.IsNullOrWhiteSpace(customerCode))
					return ServiceResponse<object>.Information("Customer code cannot be empty", null);

				//validate phone number
				var isPhoneNumberValid = _messaging.IsValidPhoneNumber(updateCustomer.CustomerPhone);
				if (!isPhoneNumberValid)
				{
					return ServiceResponse<object>.Information("The phone number provided is not valid", null);
				}
				// validate kra pin format
				static bool IsValidKRAPinFormat(string pin) => Regex.IsMatch(pin, @"^[A-Z]{1}\d{9}[A-Z]{1}$");
				if (!IsValidKRAPinFormat(updateCustomer.KRAPin))
				{
					return ServiceResponse<object>.Information($"{updateCustomer.KRAPin} is an invalid KRA PIN format.", null);
				}

				var customer = _context.Customers.FirstOrDefault(x => x.CustomerCode == customerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				var updateMessages = new StringBuilder();

				// Helper method for updating fields
				void UpdateField<T>(string fieldName, T currentValue, T newValue, Action<T> updateAction)
				{
					if (!EqualityComparer<T>.Default.Equals(currentValue, newValue))
					{
						updateMessages.AppendLine($"Changed {fieldName} from {currentValue} to {newValue}");
						updateAction(newValue);
					}
				}

				// Update customer fields
				UpdateField("Customer Name", customer.CustomerName, _setups.SentenceCase(updateCustomer.CustomerName), value => customer.CustomerName = value);
				UpdateField("Customer Phone", customer.CustomerPhone, updateCustomer.CustomerPhone, value => customer.CustomerPhone = value);
				UpdateField("Customer Email", customer.CustomerEmail, updateCustomer.CustomerEmail, value => customer.CustomerEmail = value);
				UpdateField("Identification Number", customer.IdentificationNumber, updateCustomer.IdentificationNumber, value => customer.IdentificationNumber = value);
				UpdateField("KRA PIN", customer.KRAPin, updateCustomer.KRAPin, value => customer.KRAPin = value);

				// Save changes if any updates were made
				if (updateMessages.Length > 0)
				{
					_context.Customers.Update(customer);
					await _context.SaveChangesAsync();

					// Audit trail
					var message = $"{_authentication.Name()} made the following changes to customer {customer.CustomerName} : {customerCode} on {DateTime.UtcNow:yyyy-MM-dd hh:mm tt}: {updateMessages}";
					await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

					return ServiceResponse<object>.Success($"Customer {customer.CustomerName} updated successfully", customer);
				}

				return ServiceResponse<object>.Information("No changes were made to the customer details.", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while updating the customer: {ex.Message} (Customer Code: {customerCode})", null);
			}
		}


		/// <summary>
		/// DTO for updating customer details.
		/// Contains properties for name, phone, email, identification number, and KRA pin.
		/// </summary>


		public class UpdateCustomerDTO
		{
			[Required(ErrorMessage = "Customer name is required")]
			[StringLength(50, ErrorMessage = "Customer name cannot exceed 50 characters")]
			public string CustomerName { get; set; } = string.Empty;

			[Required(ErrorMessage = "Phone number is required")]
			[RegularExpression(@"^(?:254|\+254|0)?7\d{8}$", ErrorMessage = "Enter a valid Kenyan phone number (e.g. 0712345678 or +254712345678)")]
			public string CustomerPhone { get; set; } = string.Empty;

			[EmailAddress(ErrorMessage = "Enter a valid email address")]
			[StringLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
			public string CustomerEmail { get; set; } = string.Empty;

			[Required(ErrorMessage = "Identification number is required")]
			[RegularExpression(@"^\d{7,8}$", ErrorMessage = "Identification number must be 7 or 8 digits")]
			public string IdentificationNumber { get; set; } = string.Empty;
			public string KRAPin { get; set; } = string.Empty;
		}


		///	<summary>	
		///	<para>Exports all customers and their vehicles to an Excel file.</para>
		///
		///</summary>
		public async Task<ServiceResponse<byte[]>> ExportAllCustomers()
		{
			try
			{
				var allCustomers = await _context.Customers
					.Select(c => new
					{
						c.CustomerName,
						c.CustomerPhone,
						c.CustomerEmail,
						c.IdentificationNumber,
						c.KRAPin,
						c.CreditLimit,
						c.BaseLoyaltyPoints,
						c.DateCreated
					})
					.ToListAsync();

				if (!allCustomers.Any())
				{
					return ServiceResponse<byte[]>.Information("No customers found", null);
				}

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add("AllCustomers");

				// Title
				worksheet.Cell(1, 1).Value = "All Customers Report";
				worksheet.Range(1, 1, 1, 8).Merge().Style
					.Font.SetBold().Font.SetFontSize(14).Font.SetFontColor(XLColor.White);

				worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.DarkBlue;
				worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				// Headers
				string[] headers =
				{
					"Customer Name", "Phone", "Email", "ID Number",
					"KRA Pin", "Credit Limit", "Loyalty Points", "Date Created"
		};

				for (int i = 0; i < headers.Length; i++)
				{
					var cell = worksheet.Cell(2, i + 1);
					cell.Value = headers[i];
					cell.Style.Font.SetBold();
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
					cell.Style.Fill.BackgroundColor = XLColor.LightGray;
				}

				// Data
				int row = 3;
				foreach (var c in allCustomers)
				{
					worksheet.Cell(row, 1).Value = c.CustomerName;
					worksheet.Cell(row, 2).Value = c.CustomerPhone;
					worksheet.Cell(row, 3).Value = c.CustomerEmail ?? "";
					worksheet.Cell(row, 4).Value = c.IdentificationNumber ?? "";
					worksheet.Cell(row, 5).Value = c.KRAPin ?? "";
					worksheet.Cell(row, 6).Value = c.CreditLimit;
					worksheet.Cell(row, 7).Value = c.BaseLoyaltyPoints;
					worksheet.Cell(row, 8).Value = c.DateCreated;

					worksheet.Cell(row, 8).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";

					row++;
				}

				// Footer
				int footerRow = row + 2;
				string exportInfo = $"Exported by {_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm}";

				worksheet.Cell(footerRow, 1).Value = exportInfo;
				worksheet.Range(footerRow, 1, footerRow, 8).Merge().Style
					.Font.SetItalic().Font.SetFontSize(10).Font.SetFontColor(XLColor.Gray);

				worksheet.Columns().AdjustToContents();

				// Audit trail
				await _context.UserTrails.AddAsync(new UserTrail
				{
					UserName = _authentication.Name(),
					ActionType = "ExportAllCustomers",
					Message = "Exported all customers to Excel",
					DateCreated = DateTime.UtcNow
				});

				await _context.SaveChangesAsync();

				using var stream = new MemoryStream();
				workbook.SaveAs(stream);

				return ServiceResponse<byte[]>.Success("Customers exported successfully", stream.ToArray());
			}
			catch (Exception ex)
			{
				return ServiceResponse<byte[]>.Information($"Error: {ex.Message}", null);
			}
		}
		private static string MaskPhone(string phone)
		{
			if (string.IsNullOrWhiteSpace(phone) || phone.Length < 5)
				return phone ?? string.Empty;

			return string.Concat(phone.AsSpan(0, 4), new string('*', phone.Length - 7), phone.AsSpan(phone.Length - 3));
		}

		public async Task<ServiceResponse<object>> UpdateCustomerCreditLimit(UpdateCustomerCreditLimitDTO updateCustomer)
		{
			try
			{
				if (updateCustomer == null)
					return ServiceResponse<object>.Information("Customer details cannot be empty", null);

				// Check if customer code is provided
				if (string.IsNullOrWhiteSpace(updateCustomer.VehicleCode))
					return ServiceResponse<object>.Information("Customer code cannot be empty", null);

				// Find customer by code
				var customer = _context.Vehicles.FirstOrDefault(x => x.VehicleCode == updateCustomer.VehicleCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);
				var Formerlimit = customer.CreditLimit;
				//get the customer details
				var customerDetails = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customer.CustomerCode);
				if (customerDetails == null)
					return ServiceResponse<object>.Information("Customer not found", null);
				var CreditLimit = customerDetails.CreditLimit;
				if (CreditLimit > 0)
					return ServiceResponse<object>.Information($"Customer {customerDetails.CustomerName} has a credit limit, You can not add to specific vehicles", null);
				customer.CreditLimit = updateCustomer.CreditLimit;

				// Update the customer in the database
				_context.Vehicles.Update(customer);
				await _context.SaveChangesAsync();

				var message = $"Credit Limit for Vehicle {customer.VehicleRegistrationNumber} has been updated by {_authentication.Name()} on {DateTime.UtcNow} to {updateCustomer.CreditLimit:N2} from {Formerlimit:N2}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success(message, customer);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while updating the customer: {ex.Message}", null);
			}
		}
		//update credit limit for a specific customer in customerTable 
		public async Task<ServiceResponse<object>> CustomerCreditLimit(UpdateCreditLimitDTO limit)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == limit.CustomerCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				customer.CreditLimit = limit.CreditLimit;
				_context.Customers.Update(customer);

				var message = $"Credit Limit for {customer.CustomerName} has been updated by {_authentication.Name()} on {DateTime.UtcNow} to {limit.CreditLimit}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success($"Credit Limit updated to {limit.CreditLimit:N2}", customer);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error($"An error occurred while updating the customer credit limit {limit.CreditLimit:N2}", null);
			}
		}

		public async Task<ServiceResponse<object>> CustomerDiscount(UpdateDiscount discount)
		{
			try
			{
				var customer = await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == discount.VehicleCode);
				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				customer.Discount = discount.Discount;
				_context.Vehicles.Update(customer);

				var message = $"Discount for {customer.VehicleRegistrationNumber} has been updated by {_authentication.Name()} on {DateTime.UtcNow} to {discount.Discount}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				var currentemail = await (from u in _context.Users
										  where u.UserCode == _authentication.Usercode()
										  select u.Email).FirstOrDefaultAsync();

				var emails = await _context.Emails.Where(x => x.ReportCode == "007").FirstAsync();

				var to = emails.To;
				var cc = emails.ToCC + "," + currentemail is null ? string.Empty : currentemail;
				var subject = "Discount Update";
				var htmlbody = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Vehicle Discount Update</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            margin: 0;\r\n            padding: 0;\r\n            background-color: #f4f4f4;\r\n        }\r\n        .email-container {\r\n            max-width: 600px;\r\n            margin: 20px auto;\r\n            background: #ffffff;\r\n            padding: 20px;\r\n            border-radius: 5px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        .header {\r\n            text-align: center;\r\n            font-size: 20px;\r\n            font-weight: bold;\r\n            color: #333;\r\n            margin-bottom: 20px;\r\n        }\r\n        .content {\r\n            font-size: 16px;\r\n            color: #555;\r\n        }\r\n        .highlight {\r\n            font-weight: bold;\r\n            color: #007BFF;\r\n        }\r\n        .footer {\r\n            margin-top: 20px;\r\n            text-align: center;\r\n            font-size: 14px;\r\n            color: #999;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            Vehicle Discount Update Notification\r\n        </div>\r\n        <div class=\"content\">\r\n            Dear Team,<br><br>\r\n\r\n            This is to notify you that the discount for the vehicle with number plate <span class=\"highlight\">{{xxxxx}}</span> has been updated to <span class=\"highlight\">{{0.00}}</span> by <span class=\"highlight\">{{name}}</span> on <span class=\"highlight\">{{date}}</span>.\r\n\r\n            <br><br>\r\n            Kind regards,<br>\r\n            The Otopat Team\r\n        </div>\r\n        <div class=\"footer\">\r\n            &copy; 2025 Otopay Team. All rights reserved.\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";
				htmlbody = htmlbody.Replace("{{xxxxx}}", customer.VehicleRegistrationNumber).Replace("{{0.00}}", discount.Discount.ToString()).Replace("{{name}}", _authentication.Name()).Replace("{{date}}", DateTime.UtcNow.ToString());
				_emailService.SendEmail(to, cc, subject, htmlbody);
				return ServiceResponse<object>.Success($"Discount updated to {discount.Discount:N2}", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error($"An error occurred while updating the customer credit limit {discount.Discount:N2}", null);
			}
		}


		public async Task<Customer> GetCustomer(string customerCode)
		{
			var customer = await (from c in _context.Customers
								  where c.CustomerCode == customerCode
								  select c).FirstOrDefaultAsync();
			if (customer != null)
				return customer;
			return new Customer();
		}
		///	
	}

	/// <summary>
	///		

	/// </summary>



	public class UpdateCustomerCreditLimitDTO
	{
		public string VehicleCode { get; set; } = string.Empty;
		[Precision(18,2)] public decimal CreditLimit { get; set;} 
	}
	public class UpdateCreditLimitDTO
	{
		public string CustomerCode { get; set; } = string.Empty;
		[Precision(18,2)] public decimal CreditLimit { get; set; }
	}
	public class UpdateDiscount
	{
		public string VehicleCode { get; set; } = string.Empty;
		[Precision(18,2)] public decimal Discount { get; set; }
	}

}
