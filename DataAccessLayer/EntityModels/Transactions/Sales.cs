using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels.Transactions
{
	public class QuantityTransactions : BaseEntity
	{

		[Required, StringLength(30), Unicode(false)]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required, StringLength(6), Unicode(false)]
		public string NozzleCode { get; set; } = string.Empty;
		[Required, StringLength(6), Unicode(false)]
		public string DispenserCode { get; set; } = string.Empty;
		[Required, StringLength(8), Unicode(false)]
		public string StationCode { get; set; } = string.Empty;
		[Required, StringLength(15), Unicode(false)]
		public string VehicleCode { get; set; } = string.Empty;
		[Precision(18, 2)] public decimal QuantityCredit { get; set; }
		[Precision(18, 2)] public decimal QuantityDebit { get; set; }
		[Precision(18, 2)] public decimal AmountCredit { get; set; }
		[Precision(18, 2)] public decimal AmountDebit { get; set; }
		[Precision(18, 2)] public decimal Discount { get; set; }
		[Precision(18, 2)] public decimal Vat_Amount { get; set; }

		[Required, StringLength(40), Unicode(false)]
		public string SaleId { get; set; } = string.Empty;
		[Precision(18, 2)] public decimal Price { get; set; }
		public int PaymentTypeCode { get; set; }
		public bool IsReversed { get; set; } = false;

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? RoundedDate { get; set; }
		[Required, StringLength(15), Unicode(false)]
		public string OtpUsed { get; set; } = string.Empty;
	}
	public class Vouchers : BaseEntity
	{
		public decimal Amount { get; set; }
		public string VoucherNo { get; set; } = string.Empty;
		public bool IsUsed { get; set; } = false;
		public string VehicleCode { get; set; } = string.Empty; 
		public DateTime ExpiryDate { get; set; }
	}

	public class RoyaltyPoints  : BaseEntity
	{
		public string CustomerCode { get; set; } = string.Empty;
		public decimal Litres  { get; set; }
		public decimal PointsCredit { get; set; } = 0m;
		public decimal PointsDebit  { get; set; } = 0m;
		public string SaleId { get; set; } = string.Empty;
	}
	public class MovedTransactions : BaseEntity
	{
		[Required, StringLength(20), Unicode(false)]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required, StringLength(5), Unicode(false)]
		public string NozzleCode { get; set; } = string.Empty;
		[Required, StringLength(5), Unicode(false)]
		public string DispenserCode { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
		public string VehicleCode { get; set; } = string.Empty;
		[Precision(18,2)] public decimal QuantityCredit { get; set; }
		[Precision(18,2)] public decimal QuantityDebit { get; set; }
		[Precision(18,2)] public decimal AmountCredit { get; set; }
		[Precision(18,2)] public decimal AmountDebit { get; set; }
		[Required, StringLength(30), Unicode(false)]
		public string SaleId { get; set; } = string.Empty;
		[Precision(18,2)] public decimal Price { get; set; }
		public int PaymentTypeCode { get; set; }
		public bool IsReversed { get; set; } = false; 
	}
	public class PaymentTransactions : BaseEntity
    {
         [Precision(18,2)] public decimal TransactionAmountDebit { get; set; }
        [Required, StringLength(30), Unicode(false)]
        public string SaleId { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string PaymentRefrence { get; set; } = string.Empty;
         [Precision(18,2)] public decimal TransactionAmount { get; set; }
	}


	[Microsoft.EntityFrameworkCore.Index(nameof(TransID), IsUnique = true)]
	public class MpesaTransaction : BaseEntity
	{
		// Id is int — inherited from BaseEntity

		[Required, StringLength(50)]
		public string TransactionType { get; set; } = string.Empty;

		[Required, StringLength(100)]
		public string TransID { get; set; } = string.Empty;

		public DateTime TransTime { get; set; }

		[Precision(18, 2)]
		public decimal TransAmount { get; set; }

		[Required, StringLength(20)]
		public string BusinessShortCode { get; set; } = string.Empty;

		// ─── Till info ────────────────────────────────────────────────────────────

		[StringLength(20)]
		public string TillNumber { get; set; } = string.Empty;

		[StringLength(50)]
		public string TillName { get; set; } = string.Empty;

		// ─── Payment method: "STK" or "C2B" ──────────────────────────────────────

		[StringLength(10)]
		public string PaymentMethod { get; set; } = string.Empty;

		// ─── STK Push specific ────────────────────────────────────────────────────

		[StringLength(100)]
		public string? CheckoutRequestID { get; set; }

		[StringLength(100)]
		public string? MerchantRequestID { get; set; }

		// ─── Receipt ──────────────────────────────────────────────────────────────

		[StringLength(50)]
		public string MpesaReceiptNumber { get; set; } = string.Empty;

		// ─── Customer info ────────────────────────────────────────────────────────

		[Required, StringLength(20)]
		public string MSISDN { get; set; } = string.Empty;

		[StringLength(30)]
		public string FirstName { get; set; } = string.Empty;

		[StringLength(30)]
		public string MiddName { get; set; } = string.Empty;

		[StringLength(30)]
		public string LastName { get; set; } = string.Empty;

		// ─── Balances ─────────────────────────────────────────────────────────────

		[Precision(18, 2)]
		public decimal OrgAccountBalance { get; set; }

		[Precision(18, 2)]
		public decimal UsageBalance { get; set; }

		// ─── Status: 0=Pending, 1=Success, 2=Failed ──────────────────────────────

		public int Status { get; set; }

		public DateTime DateTimeStamp { get; set; }

		public DateTime DateModified { get; set; } = DateTime.UtcNow;
	}
	public class Settings
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string SetupCode { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public decimal Value { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	}
}