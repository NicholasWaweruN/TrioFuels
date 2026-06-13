using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Sales
{
    public class AddsaleDto
    {
        public string VehicleCode { get; set; } = string.Empty;
        public int PaymentTypeCode { get; set; } 
        public string NozzleCode { get; set; } = string.Empty;
        public string ShiftNumber { get; set; } = string.Empty;
        [Precision(18,2)] 
		public decimal Quantity { get; set; }
        public string DispenserCode { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string? WalletId { get; set; } = string.Empty;
		public string? OtpUsed { get; set; } = string.Empty;
		public string? LoyaltyPhone { get; set; }
		public string? LoyaltyCustomerCode { get; set; }
		public decimal BaseLoyaltyPoints { get; set; }
		public bool IsLoyalCustomer { get; set; }
		public List<PaymentDetails> PaymentDetails { get; set; } = [];
		public string PhoneNumber {  get; set; } = string.Empty; 
		
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
