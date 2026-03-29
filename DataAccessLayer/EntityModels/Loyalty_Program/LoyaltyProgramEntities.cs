using DataAccessLayer.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.EntityModels.Loyalty_Program
{
	public class Coupons 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CouponId { get; set; }
		[Required]
		[MaxLength(20)]
		public string CouponCode { get; set; } = string.Empty;
		[Column(TypeName = "decimal(10,2)")]
		public decimal Amount { get; set; }
		public int PointsToRedeem { get; set; } = 10;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public string UserCode { get; set; } = string.Empty;

	}

	public class LoyaltySubscription : BaseEntity 
	{
		[Required]
		[MaxLength(20)]
		public string SubscriptionId { get; set; } = string.Empty;
		[Required]
		[MaxLength(20)]
		public string VehicleCode  { get; set; } = string.Empty;
		public int CouponId { get; set; }
		public int RewardPoints { get; set; }
		public int CurrentPoints { get; set; } = 0;
		public bool IsRewardClaimed { get; set; } = false;
		[MaxLength(10)]
		public string OtpCode { get; set; } = string.Empty;
		public DateTime? OtpSentDate { get; set; }
		public DateTime? RewardClaimedDate { get; set; }
		[MaxLength(200)]
		public string SaleIds { get; set; } = string.Empty;
	}



}
