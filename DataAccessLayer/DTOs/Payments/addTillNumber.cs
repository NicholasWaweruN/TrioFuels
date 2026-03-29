using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Payments
{
    public class addTillNumberDto
    {
        [Required]
        public string TillNumber { get; set; } = string.Empty;
        [Required]
        public string StoreNumber { get; set; } = string.Empty;
        [Required]
        public string TillName { get; set; } = string.Empty;
    }
    public class UpdateTillDto 
    {
        [Required]
        public string TillNumber { get; set; } = string.Empty;
        [Required]
        public string StoreNumber { get; set; } = string.Empty;
        [Required]
        public string TillName { get; set; } = string.Empty;
    }
    public class AssignTillToDispenserDto
    {
        [Required]
        public string TillNumber { get; set; } = string.Empty;
        [Required]
        public string DispenserCode { get; set; } = string.Empty;
    }
	public class MpesaTransactionDto
	{
		public string TransID { get; set; } = string.Empty;
		public string BusinessShortCode { get; set; } = string.Empty;
		public string StoreNumber { get; set; } = string.Empty;
		public string Till { get; set; } = string.Empty;
		public double UsageBalance { get; set; }
		public DateTime DateTimeStamp { get; set; } = new DateTime();
		public int Status { get; set; }
	}
	public class MpesaTransactionsDto
	{
		public string TransID { get; set; } = string.Empty;
		public string BusinessShortCode { get; set; } = string.Empty;
		public string StoreNumber { get; set; } = string.Empty;
		public string Till { get; set; } = string.Empty;
		public double UsageBalance { get; set; }
		public DateTime DateTimeStamp { get; set; } = new DateTime();
		public string Status { get; set; } = string.Empty;
	}
	public class MpesaC2BPayment
	{
		public string TransID { get; set; } = string.Empty; // Transaction ID
		 [Precision(18,2)] public decimal Amount { get; set; } // Transaction Amount
		public string BusinessShortCode { get; set; } = string.Empty; // Business Short Code
		public string PhoneNumber { get; set; } = string.Empty; // Mobile N

	}

	public class FuelSale
	{
		public string Vehicle { get; set; } = string.Empty;
		public string ShiftNumber { get; set; } = string.Empty;
		public string SaleId { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string DispenserName { get; set; } = string.Empty;
		public string NozzleName { get; set; } = string.Empty;
		public string AttendantName { get; set; } = string.Empty;
		 [Precision(18,2)] public decimal Litres { get; set; }
		 [Precision(18,2)] public decimal Amount { get; set; }
		public DateTime SalesDate { get; set; }
		 [Precision(18,2)] public decimal Price { get; set; }
		public string TillNumber { get; set; } = string.Empty;	
		public string TransId { get; set; } = string.Empty;
	}

}