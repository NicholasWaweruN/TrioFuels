using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Sales
{
    public class SalesListDto
    {
        public string? StationCode { get; set; }
        public string? DispenserCode { get; set; }
        public string? ShiftNumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    public class StationPrices
    {
        public string StationCode { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string StationAddress { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<PriceDetails> Prices { get; set; } = new List<PriceDetails>();
    }

    public class PriceDetails
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Amount { get; set; }
    }
	public class CustomerTransactionSummary
	{
		[Precision(18, 2)]
		public decimal TotalCredit { get; set; }
		[Precision(18, 2)]
		public decimal TotalDebit { get; set; }
		[Precision(18, 2)]
		public decimal OpeningBalance { get; set; }
		[Precision(18,2)]
		public decimal ClosingBalance { get; set; }
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		public string CustomerName { get; set; } = string.Empty;
	}
	public class AllCredits
	{
		public string CustomerName { get; set; } = string.Empty; // e.g., "Greenspoon Ltd"
		public string VehicleRegistrationNumber { get; set; } = string.Empty; // e.g., "KDN802R"
		[Precision(18, 2)]
		public decimal Credit { get; set; } // e.g., 15000.00
		public string ProductName { get; set; } = string.Empty;// e.g., "Rental"
		public DateTime DateCreated { get; set; } // e.g., 2024-12-16 11:28:04.6333333
		public string TransactionReference { get; set; } = string.Empty; // e.g., "SLG6NB0BWE"
	}
	public class SalesTransaction
	{
		public string ShiftNumber { get; set; } = string.Empty;
		public string SaleId { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string Attendant_Name { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Litres { get; set; }
		[Precision(18, 2)]
		public decimal Amount { get; set; }
		public DateTime SalesDate { get; set; }
		public string PaymentType { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Price { get; set; }
		public int TillNumber { get; set; }
		public string Vehicle { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		public string CustomerName { get; set; } = string.Empty;
		public string Transid { get; set; } = string.Empty;
	}

}
