using DataAccessLayer.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Sales
{
	public class AddsaleDto
	{
		public string ShiftNumber { get; set; } = string.Empty;
		public string VehicleCode { get; set; } = string.Empty;
		public string NozzleCode { get; set; } = string.Empty;
		public string DispenserCode { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public decimal Quantity { get; set; }
		public string? WalletId { get; set; }
		public bool IsLoyalCustomer { get; set; }
		public string? LoyaltyPhone { get; set; }
		public string? OtpUsed { get; set; }
		public decimal BaseLoyaltyPoints { get; set; }

		// ── Per-payment type code now lives here, not at sale level ──
		public List<PaymentDetailDto> PaymentDetails { get; set; } = [];

		// ── Derived — primary payment type (first payment's code) ────
		// Kept for backward compat with receipt/audit logic
		public int PaymentTypeCode => PaymentDetails.FirstOrDefault()?.PaymentTypeCode
									  ?? PaymetMethod.Cash;
	}

	public class PaymentDetailDto
	{
		public string? TransactionReference { get; set; }
		public decimal TransactionAmount { get; set; }
		public int PaymentTypeCode { get; set; }  // ← new field
	}

	public class MisingSaleDto
    {

		public string? WalletId { get; set; } = string.Empty;
        public string VehicleCode { get; set; } = string.Empty;
        [Required]
        public int PaymentTypeCode { get; set; }
        [Required]
        public string NozzleCode { get; set; } = string.Empty;
        [Required]
        public string ShiftNumber { get; set; } = string.Empty;
        [Required]
         [Precision(18,2)] public decimal Quantity { get; set; }
        [Required]
        public string DispenserCode { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
		public decimal Price { get; set; } = decimal.Zero;

        public List<PaymentDetails> PaymentDetails { get; set; } = [];
    }

	public class Personal_MisingSale 
	{
		[Required]
		public string VehicleCode { get; set; } = string.Empty;
		[Required]
		public string WalletId { get; set; } = string.Empty;
		[Required]
		public int PaymentTypeCode { get; set; }
		[Required]
		public string NozzleCode { get; set; } = string.Empty;
		[Required]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required]
		[Precision(18, 2)] public decimal Quantity { get; set; }
		[Required]
		public string DispenserCode { get; set; } = string.Empty;
		[Required]
		public string Comment { get; set; } = string.Empty;
		public decimal? Price { get; set; } = decimal.Zero;

		public List<PaymentDetails> PaymentDetails { get; set; } = [];
	}

	public class UsageBalanceDto
    {
        public int Amount { get; set; }
		public string StoreNumber { get; set; } = string.Empty;

	}

	public class ValueDto
	{
		[Required, StringLength(30), Unicode(false)]
		public string Value { get; set; } = string.Empty;
	}
	public class PaymentDetails
    {
        public string TransactionReference { get; set; } = string.Empty;
        [Precision(18,2)] public decimal TransactionAmount { get; set; } = 0;
    }
    public class StationSummaryDto
    {
        public string StationName { get; set; } = string.Empty;
        public int FuelingEvents { get; set; }
        public decimal QuantitySold { get; set; }

    }
    public class AdjustStockTakeDto
    {
        public string ShiftNumber { get; set; } = string.Empty;
        public List<NozzleReadingDto> Readings { get; set; } = [];
    }
    public class NozzleReadingDto
    {
        public string NozzleCode { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Reading { get; set; } = decimal.Zero;
    }

}
