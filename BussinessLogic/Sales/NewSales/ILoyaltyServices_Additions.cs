// ============================================================================
// REQUIRED ADDITIONS to support Loyalty Points payment
// ============================================================================

// 1. Add to PaymetMethod enum/constants
// --------------------------------------------------------------------------
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Sales.NewSales
{



	// 2. Add to ILoyaltyServices interface
	// --------------------------------------------------------------------------
	public interface ILoyaltyServices
	{

		// NEW — required by HandleLoyaltyAsync
		Task<decimal> GetPointsBalance(string customerCode);
		Task DeductLoyaltyPoints(string customerCode, decimal points, string saleId);
		Task<ServiceResponse<object>> AddLoyaltyPoints(string customerCode, decimal litres, string saleId);
	
		Task<ServiceResponse<object>> GetLoyaltyBalanceByPhoneAsync(string phoneNumber);
	}

	// 3. Example implementation sketch for the two new methods
	// --------------------------------------------------------------------------
	public class LoyaltyServices : ILoyaltyServices
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;

		public LoyaltyServices(OTOContext context, IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}


		public async Task<decimal> GetPointsBalance(string customerCode)
		{
			// Assuming a LoyaltyTransactions table with Credit/Debit columns:
			return await _context.RoyaltyPoints
				.Where(t => t.CustomerCode == customerCode)
				.SumAsync(t => t.PointsCredit - t.PointsDebit);
		}

		public async Task DeductLoyaltyPoints(string customerCode, decimal points, string saleId)
		{
			_context.RoyaltyPoints.Add(new RoyaltyPoints
			{
				CustomerCode = customerCode,
				PointsCredit = 0,
				PointsDebit = points,
				SaleId = saleId,
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode()
			});

			await _context.SaveChangesAsync();
		}
		public async Task<ServiceResponse<object>> AddLoyaltyPoints(string customerCode, decimal litres, string saleId)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				return ServiceResponse<object>.Information("Customer code is required.", null);

			if (litres <= 0)
				return ServiceResponse<object>.Information("Litres must be greater than zero.", null);

			try
			{
				var vehicle = await _context.Customers
					.Where(v => v.CustomerCode == customerCode)
					.Select(v => new { v.CustomerCode, v.BaseLoyaltyPoints })
					.FirstOrDefaultAsync();

				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found.", null);

				if (vehicle.BaseLoyaltyPoints <= 0)
					return ServiceResponse<object>.Information("Vehicle is not eligible for royalty points.", null);

				var earnedPoints = vehicle.BaseLoyaltyPoints * litres;

				if (earnedPoints <= 0)
					return ServiceResponse<object>.Information("No points earned for this transaction.", null);

				var strategy = _context.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					await using var transaction = await _context.Database.BeginTransactionAsync();

					var addPoints = new RoyaltyPoints
					{
						DateCreated = DateTime.UtcNow,
						Litres = litres,
						PointsCredit = earnedPoints,
						PointsDebit = 0,
						SaleId = saleId,
						UserCode = string.Empty,
						CustomerCode = customerCode,
					};

					await _context.RoyaltyPoints.AddAsync(addPoints);
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				});

				return ServiceResponse<object>.Success("Points added successfully.");
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while adding royalty points.", ex.Message);
			}
		}

		public async Task<ServiceResponse<object>> GetLoyaltyBalanceByPhoneAsync(string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
				return ServiceResponse<object>.Information("Phone number is required.", null);

			var customer = await _context.Customers
				.Where(c => c.CustomerPhone == phoneNumber.Trim())
				.Select(c => new
				{
					c.CustomerCode,
					c.CustomerName,
					c.CustomerPhone
				})
				.FirstOrDefaultAsync();

			if (customer is null)
				return ServiceResponse<object>.Information(
					"No loyalty account found for this phone number.", null);

			var pointsBalance = await GetPointsBalance(customer.CustomerCode);

			return ServiceResponse<object>.Success("Loyalty balance retrieved", new
			{
				pointsBalance,
				customer.CustomerCode,
				customer.CustomerName
			});
		}
	}
}