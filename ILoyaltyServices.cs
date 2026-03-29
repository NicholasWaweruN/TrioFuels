using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;

// ============================================================================
// REQUIRED ADDITIONS to support Loyalty Points payment
// ============================================================================

// 1. Add to PaymetMethod enum/constants
// --------------------------------------------------------------------------

// 2. Add to ILoyaltyServices interface
// --------------------------------------------------------------------------
namespace BussinessLogic.Sales.NewSales
{
	public interface ILoyaltyServices
	{
		// NEW — required by HandleLoyaltyAsync
		Task<decimal> GetPointsBalance(string customerCode);
		Task DeductLoyaltyPoints(string customerCode, decimal points, string saleId);
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
	}
}