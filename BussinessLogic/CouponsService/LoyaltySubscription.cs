using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Loyalty_Program;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.CouponsService
{
	public class LoyaltyProgramSubscription : ILoyaltyProgramSubscription
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _auth;
		public LoyaltyProgramSubscription(OTOContext context,IAuthCommonTasks auth)
		{
			_context = context;
			_auth = auth;
		}
		public async Task<ServiceResponse<object>> AddSubscriptionAsync(CreateLoyaltySubscriptionDto dto)
		{
			var response = new ServiceResponse<string>();

			try
			{
				// Optional: Check if there's already an active subscription for the vehicle
				bool exists = await _context.LoyaltySubscriptions
					.AnyAsync(s => s.VehicleCode == dto.VehicleCode && !s.IsRewardClaimed);

				if (exists)
				{
					var messagex = "An active subscription already exists for this vehicle.";
					return ServiceResponse<object>.Error(messagex, null);
				}
				// Check if the coupon exists and is valid
				var coupon = await _context.Coupons
					.FirstOrDefaultAsync(c => c.CouponId == dto.CouponId);
				if (coupon is not null)
				{
					var subscription = new LoyaltySubscription
					{
						SubscriptionId = dto.SerialNumber,
						VehicleCode = dto.VehicleCode,
						CouponId = dto.CouponId,
						RewardPoints = coupon.PointsToRedeem, // Default for now, could be fetched from Coupon table
						CurrentPoints = 0,
						IsRewardClaimed = false,
						OtpCode = string.Empty,
						OtpSentDate = null,
						RewardClaimedDate = null,
						DateCreated = DateTime.UtcNow,
						UserCode = _auth.Usercode(),
						SaleIds = string.Empty 
					};
					_context.LoyaltySubscriptions.Add(subscription);
					await _context.SaveChangesAsync();
					var message = "Subscription created successfully.";
					return ServiceResponse<object>.Success(message, new
					{
						subscriptionId = subscription.Id,
						vehicleCode = subscription.VehicleCode,
						couponId = subscription.CouponId,
						rewardPoints = subscription.RewardPoints,
						
					});
				}

				return ServiceResponse<object>.Information("Coupon not found or invalid.", null);
			}
			catch (Exception ex)
			{
				var message = $"Error creating subscription: {ex.Message}";
				return ServiceResponse<object>.Error(message, null);
			}


		}
		public class CreateLoyaltySubscriptionDto
		{
			[Required]
			[MaxLength(20)]
			public required string SerialNumber  { get; set; } = string.Empty;
			[Required]
			[MaxLength(20)]
			public required string VehicleCode { get; set; } = string.Empty;

			[Required]
			public int CouponId { get; set; }
		}

	}
}
