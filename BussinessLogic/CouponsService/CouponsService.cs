using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Loyalty_Program;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace BussinessLogic.CouponsService
{
	public class CouponsService : ICouponsService
	{
		private readonly OTOContext _context;
		public CouponsService(OTOContext context)
		{
			_context = context;
		}
		public async Task<ServiceResponse<object>> GetAllCouponsAsync()
		{

			try
			{
				var coupons = await (from c in _context.Coupons
									 select new CouponDto
									 {
										 CouponId = c.CouponId,
										 CouponCode = c.CouponCode,
										 Amount = c.Amount,
										 PointsToRedeem = c.PointsToRedeem,
									 }).ToListAsync();


				if (coupons.Count == 0)
				{
					return ServiceResponse<object>.Information("No data found", null);
				}
				return ServiceResponse<object>.Success("Coupons retrieved successfully", coupons);

			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while retrieving coupons: " + ex.Message, null);
			}

		}
		//register coupon services


		public class CouponDto
		{
			public int CouponId { get; set; }
			public string CouponCode { get; set; } = string.Empty;
			public decimal Amount { get; set; }
			public int PointsToRedeem { get; set; }
		}

	}
}
