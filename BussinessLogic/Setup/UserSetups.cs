using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Worker.PriceScheduler;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Setups;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Products = DataAccessLayer.EntityModels.SetUps.Products;
using BussinessLogic.Setup;

namespace BusinessLogic.SetupService
{
	public class UserSetups : IUserSetups
	{
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;

		public UserSetups(OTOContext context, ICommonSetups setups, IAuthCommonTasks authentication)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
		}
		public async Task<ServiceResponse<object>> AddPrice(List<UpdatePrice> updatePrice)
		{
			try
			{
				foreach (var item in updatePrice)
				{
					var product = await _context.Prices.Where(x => x.ProductCode == item.ProductCode && x.StationCode.Equals(item.StationCode)).FirstOrDefaultAsync();
					if (product is not null)
					{
						product.Amount = item.NewPrice;
						_context.Prices.Update(product);
						await _context.SaveChangesAsync();
					}
					else
					{
						return ServiceResponse<object>.Information("Product does not exist", null);
					}
				}
				return ServiceResponse<object>.Success("Price updated successfully", null);
			}
			catch (Exception)
			{
				return new ServiceResponse<object>
				{
					ResponseCode = Response.Error,
					ResponseMessage = "An error occured while updating price",
					ResponseObject = null
				};
			}
		}



		/// <summary>
		/// for Discounts and Price adjustments
		/// </summary>
		/// <param name="adjustPrices"></param>
		/// <returns></returns>
		public async Task<ServiceResponse<object>> PriceDiscount(List<AdjustPriceDto> adjustPrices)
		{
			try
			{
				foreach (var item in adjustPrices)
				{
					var product = await _context.Prices
						.FirstOrDefaultAsync(x => x.ProductCode == item.ProductCode && x.StationCode == item.StationCode);

					if (product is null)
						return ServiceResponse<object>.Information($"Product {item.ProductCode} at station {item.StationCode} does not exist", null);

					// Apply adjustment
					product.Discount = item.AdjustmentAmount;
					_context.Prices.Update(product);
				}

				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("Price adjusted successfully", null);
			}
			catch (Exception ex)
			{
				return new ServiceResponse<object>
				{
					ResponseCode = Response.Error,
					ResponseMessage = $"An error occurred while adjusting price: {ex.Message}",
					ResponseObject = null
				};
			}
		}

		public async Task<ServiceResponse> AddPriceSchedule(List<PriceChangeSchedule> schedule)
		{
			try
			{
				foreach (var change in schedule)
				{
					var xprice = await (from p in _context.Prices
										where p.ProductCode == change.Product && p.StationCode == change.Product
										select p).FirstOrDefaultAsync();
					if (xprice is not null)
					{
						var price = new PriceSchedule
						{
							EndTime = change.EndTime,
							IsActive = false,
							OriginalPrice = xprice.Amount,
							Processed = false,
							ScheduledPrice = change.NewPrice,
							ProductCode = change.Product,
							StartTime = change.StartTime,
							StationCode = xprice.StationCode
						};
						await _context.AddAsync(price);
						await _context.SaveChangesAsync();
					}
				}
				return ServiceResponse<Object>.Success("Price schedule added", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<Object>.Success("Price schedule failed to add, contact admin", ex.Message);
			}
		}
		public async Task<ServiceResponse<object>> AddPaymentType(string paymentType)
		{
			try
			{
				if (string.IsNullOrEmpty(paymentType))
				{
					return ServiceResponse<object>.Information("Payment type cannot be empty", null);
				}
				else
				{
					var paymenttype = new PaymentType
					{
						PaymentTypeName = paymentType,
						DateCreated = DateTime.UtcNow,

					};
					_context.PaymentTypes.Add(paymenttype);
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Payment type added successfully", paymenttype);
				}
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while adding payment type", null);
			}
		}
		public async Task<ServiceResponse<object>> RegisterPDA(string deviceName, string deviceIMEI, string deviceSerialNumber, string deviceModel, string dispensercode)
		{
			try
			{
				if (string.IsNullOrEmpty(deviceName) || string.IsNullOrEmpty(deviceIMEI) || string.IsNullOrEmpty(deviceSerialNumber) || string.IsNullOrEmpty(dispensercode))
				{
					return ServiceResponse<object>.Information("Device name, IMEI and Serial number cannot be empty", null);
				}
				else
				{
					var device = new PdaDevices
					{
						DeviceCode = await _setups.GetCodeGenerator("pdadevice"),
						DeviceName = deviceName,
						DeviceIMEI = deviceIMEI,
						DeviceSerialNumber = deviceSerialNumber,
						DeviceModel = deviceModel,
						IsActive = true,
						DateCreated = DateTime.UtcNow,
						DispenserCode = dispensercode
					};
					_context.PdaDevices.Add(device);
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Device registered successfully", device);
				}
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while registering device", null);
			}
		}
		public async Task<ServiceResponse<object>> AddProduct(AddProductDto product)
		{
			try
			{
				var productExists = await _context.Products.Where(x => x.ProductName == product.ProductName).FirstOrDefaultAsync();
				if (productExists is null)
				{
					var productcode = await _setups.GetCodeGenerator("ProductCode");
					var newProduct = new Products
					{
						ProductCode = productcode,
						ProductName = product.ProductName,
						IsActive = true,
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode(),
						
					};
					_context.Products.Add(newProduct);

					var stations = await _context.Stations.Select(x => x.StationCode).ToListAsync();
					//Add records in the price table for every station
					foreach (var item in stations)
					{
						var price = new Price
						{
							Amount = 0,
							ProductCode = productcode,
							StationCode = item,
							DateCreated = DateTime.UtcNow,
							UserCode = _authentication.Usercode(),
							Discount = 0
						};
						_context.Prices.Add(price);
					}
					var message = $" creating product {product.ProductName} by {_authentication.Name()} on {DateTime.UtcNow} also created price for the product for every station";
					await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Product added successfully", newProduct);
				}
				else
				{
					return ServiceResponse<object>.Information("Product already exists", null);
				}

			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while adding product", null);
			}
		}
		//Get all products
		public async Task<ServiceResponse<object>> GetProducts()
		{
			try
			{
				var products = await (from p in _context.Products
									  select new
									  {
										  p.ProductCode,
										  p.ProductName,
										  p.IsActive,

									  }).ToListAsync();
				if (!products.Any())
				{
					return new ServiceResponse<object>
					{
						ResponseCode = Response.Information,
						ResponseMessage = "No products added",
						ResponseObject = null
					};
				}
				return ServiceResponse<object>.Success("Products retrieved successfully", products);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while retrieving products", null);
			}
		}

		// add PriceAdjustedByAmount


		//change price of a product for all stations at once
		public async Task<ServiceResponse<object>> ChangePriceForAllStations(string productCode, decimal newPrice)
		{
			try
			{
				var product = await _context.Prices.Where(x => x.ProductCode == productCode).ToListAsync();
				if (product.Any())
				{
					foreach (var item in product)
					{
						item.Amount = newPrice;
						_context.Prices.Update(item);
					}
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Price updated successfully", null);
				}
				else
				{
					return ServiceResponse<object>.Information("Product does not exist", null);
				}
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while updating price", null);
			}
		}
		public async Task<ServiceResponse<object>> AddRecipients(int type, string reportCode, string email)
		{
			var emails = new EmailsDto();
			if (type == 1)
			{
				emails.EmailTo = email;
			}
			else
			{
				emails.EmailCC = email;
			}
			try
			{
				var emailAddress = await (from p in _context.Emails
										  where
										 p.ReportCode.Equals(reportCode)
										  select p).FirstOrDefaultAsync();

				if (emailAddress is not null)
				{
					var emt = emails.EmailTo.Split(',').ToHashSet();
					var emc = emails.EmailCC.Split(',').ToHashSet();

					foreach (var mail in emt)
					{

						if (emailAddress.To.Contains(mail))
							return ServiceResponse<object>.Information($"Email {mail} already exists", null);
					}

					emailAddress.ToCC += emails.EmailTo + ",";
					emailAddress.ToCC += emails.EmailCC + ",";
					_context.Update(emailAddress);
					await _context.SaveChangesAsync();
				}
				else
				{
					return ServiceResponse<object>.Information("Report not found", null);
				}
				return ServiceResponse<object>.Success("Recipients added successfully", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while adding email", null);
			}
		}
		public class EmailsDto
		{
			public string EmailTo { get; set; } = string.Empty;
			public string EmailCC { get; set; } = string.Empty;
		}
		// retrieve email recipients for a report
		public async Task<ServiceResponse<object>> GetRecipients(string reportCode)
		{
			try
			{
				var emailAddress = await (from p in _context.Emails
										  where p.ReportCode.Equals(reportCode)
										  select new
										  {
											  ToEmails = p.To.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
											  CcEmails = p.ToCC.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
										  }
							  ).FirstOrDefaultAsync();
				if (_authentication.Usercode() != "00008")
				{


					if (emailAddress is not null)
					{
						// Combine the To and CC email lists and convert to a single array
						var recipients = emailAddress.ToEmails.Concat(emailAddress.CcEmails).ToArray();
						return ServiceResponse<object>.Success("Recipients retrieved successfully", recipients);
					}
					else
						return ServiceResponse<object>.Information("Report not found", null);
				}
				else
				{
					var recipient = new Recipients
					{
						ToEmail = "wawerun@protoenergy.com",
						CcEmails = "wawerun@protoenergy.com"
					};

					var recipients = recipient.ToEmail.Concat(recipient.CcEmails).ToArray();
					return ServiceResponse<object>.Success("Recipients retrieved successfully", recipient);
				}

			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while retrieving email recipients", null);
			}
		}
		//model for recipients



		public class Recipients
		{
			public string ToEmail { get; set; } = string.Empty;
			public string CcEmails { get; set; } = string.Empty;
		}
		public async Task<ServiceResponse<object>> RemoveEmailRecipients(string email, string reportCode)
		{
			try
			{
				// Fetch the email record based on report code
				var emails = await _context.Emails
										   .FirstOrDefaultAsync(x => x.ReportCode.Equals(reportCode));
				if (emails != null)
				{
					// Check and update "To" recipients
					if (!string.IsNullOrEmpty(emails.To))
					{
						var allEmails = emails.To.Split(',').ToList();
						if (allEmails.Contains(email))
						{
							allEmails.Remove(email);
							emails.To = string.Join(",", allEmails);
						}
					}

					// Check and update "CC" recipients
					if (!string.IsNullOrEmpty(emails.ToCC))
					{
						var allEmailsCC = emails.ToCC.Split(',').ToList();
						if (allEmailsCC.Contains(email))
						{
							allEmailsCC.Remove(email);
							emails.ToCC = string.Join(",", allEmailsCC);
						}
					}

					// Update email record only if any changes were made
					_context.Emails.Update(emails);
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("Email recipient removed successfully", null);
				}

				return ServiceResponse<object>.Information("No email recipients found", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}

		public List<Report> Reports()
		{
			var reportList = new List<Report>
			{
				new() { Id = "001", ReportName = "Variance Report" },
				new() { Id = "002", ReportName = "Sales Report" },
				new() { Id = "003", ReportName = "Monthly Sales Report" },
				new() { Id = "004", ReportName = "Cumulative Variance Report" },
				new() { Id = "005", ReportName = "Mpesa Usage Report" }
			};

			return reportList;
		}

		//report model
		public class Report
		{
			public string Id { get; set; } = string.Empty;
			public string ReportName { get; set; } = string.Empty;
		}
	}
}
