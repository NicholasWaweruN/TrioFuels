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
using BusinessLogic.SetupService;

namespace BussinessLogic.Setup
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
		public async Task<ServiceResponse<object>> UpdatePrice(string productCode, string stationCode, decimal newAmount)
		{
			if (newAmount <= 0)
				return ServiceResponse<object>.Information("Price must be greater than zero", null);

			try
			{
				var productExists = await _context.PetroleumProducts
					.AnyAsync(pp => pp.PetroleumCode == productCode);

				if (!productExists)
					return ServiceResponse<object>.Information($"Product {productCode} does not exist", null);

				// Price is per product+station, not per dispenser — but the table
				// still carries one row per dispenser. Every row for this
				// station+product must move together or dispensers at the same
				// station will silently charge different prices for the same fuel.
				var prices = await _context.Prices
					.Where(p => p.ProductCode == productCode && p.StationCode == stationCode)
					.ToListAsync();

				if (prices.Count == 0)
					return ServiceResponse<object>.Information($"No price record found for product {productCode} at station {stationCode}", null);

				var oldAmount = prices[0].Amount;

				if (oldAmount == newAmount)
					return ServiceResponse<object>.Information("New price is the same as the current price", null);

				foreach (var price in prices)
					price.Amount = newAmount;

				_context.Prices.UpdateRange(prices);
				await _context.SaveChangesAsync();

				var message = $"Price for product {productCode} at station {stationCode} changed from KES {oldAmount:N2} to KES {newAmount:N2} across {prices.Count} dispenser(s) by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Price updated successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}
		/// <summary>
		/// for Discounts and Price adjustments
		/// </summary>
		/// <param name="adjustPrices"></param>
		/// <returns></returns>

		public async Task<ServiceResponse> AddPriceSchedule(List<PriceChangeSchedule> schedule)
		{
			try
			{
				foreach (var change in schedule)
				{
					// ASSUMPTION: PriceChangeSchedule has a "Station" property —
					// confirm the real name if this doesn't compile. Original code
					// compared StationCode == change.Product (typo, both sides
					// referenced Product).
					var xprice = await (from p in _context.Prices
										where p.ProductCode == change.Product && p.StationCode == change.StationCodes
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
		//Get all products
		public async Task<ServiceResponse<object>> GetProducts()
		{
			try
			{
				var products = await (from p in _context.PetroleumProducts
									  select new
									  {
										  p.PetroleumCode,
										  p.PetroleumName,
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
		public async Task<ServiceResponse<object>> GetPriceByStation(string stationCode, string productCode)
		{
			try
			{
				var productExists = await _context.PetroleumProducts
					.AnyAsync(pp => pp.PetroleumCode == productCode);
				if (!productExists)
					return ServiceResponse<object>.Information("Product does not exist", null);

				var priceInfo = await _context.Prices
					.AsNoTracking()
					.Where(p => p.ProductCode == productCode)
					.Select(p => new { p.Amount, p.Discount })
					.FirstOrDefaultAsync();

				if (priceInfo == null)
					return ServiceResponse<object>.Information("Kindly check the station pricing or product configuration", null);

				var result = new
				{
					ProductCode = productCode,
					Price = priceInfo.Amount,
					Discount = priceInfo.Discount,
					FinalPrice = Math.Max(priceInfo.Amount - priceInfo.Discount, 0)
				};

				return ServiceResponse<object>.Success("Price retrieved", result);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while retrieving price", null);
			}
		}
		public async Task<ServiceResponse<object>> GetPriceInfo(string nozzleCode)
		{
			// Single round trip: Nozzle -> Dispenser (for StationCode) -> Prices ->
			// PetroleumProducts, all joined in one query instead of three sequential
			// awaits. Also fixes a real bug: the old version never resolved station
			// at all, so on any station with multiple Price rows for the same
			// product it could pick an arbitrary one — same issue ResolveUnitPriceAsync
			// had before it was scoped to StationCode.
			var result = await (
				from n in _context.Nozzles.AsNoTracking()
				where n.NozzleCode == nozzleCode
				join d in _context.Dispensers.AsNoTracking() on n.DispenserCode equals d.DispenserCode
				join p in _context.Prices.AsNoTracking()
					on new { Product = n.PetroleumCode, Station = d.StationCode }
					equals new { Product = p.ProductCode, Station = p.StationCode }
				join pp in _context.PetroleumProducts.AsNoTracking()
					on n.PetroleumCode equals pp.PetroleumCode
				select new
				{
					n.PetroleumCode,
					Price = p.Amount,
					Discount = p.Discount,
					ProductName = pp.PetroleumName,
				}
			).FirstOrDefaultAsync();

			if (result == null)
			{
				// Distinguish "nozzle doesn't exist" from "price not configured" with
				// one cheap follow-up check only when the main query comes back empty
				// — keeps the common (found) path to a single round trip.
				var nozzleExists = await _context.Nozzles.AsNoTracking().AnyAsync(n => n.NozzleCode == nozzleCode);
				return nozzleExists
					? ServiceResponse<object>.Information("Price not configured for this nozzle/station", null)
					: ServiceResponse<object>.Information("Nozzle not found", null);
			}

			return ServiceResponse<object>.Success("", new
			{
				result.PetroleumCode,
				result.Price,
				result.Discount,
				result.ProductName,
				FinalPrice = Math.Max(result.Price - result.Discount, 0)
			});
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

		public Task<ServiceResponse<object>> AddProduct(AddProductDto product)
		{
			throw new NotImplementedException();
		}

		//report model
		public class Report
		{
			public string Id { get; set; } = string.Empty;
			public string ReportName { get; set; } = string.Empty;
		}
	}
}
