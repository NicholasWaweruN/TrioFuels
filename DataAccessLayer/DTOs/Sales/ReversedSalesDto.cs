using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace DataAccessLayer.DTOs.Sales
{

    public class IntValue
    {
		[Precision(18,2)]
        public decimal Value { get; set; } = 0;
    }
    public class ReversedSalesDto
    {
        public string TransactionCode { get; set; } = string.Empty;
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public string Nozzle { get; set; } = string.Empty;
        public string DispenserName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class SalesDto
    {
        public string TransactionCode { get; set; } = string.Empty;
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        public int PaymentMethod { get; set; } 
        public string Nozzle { get; set; } = string.Empty;
        public string DispenserName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ShiftNumber { get; set; } = string.Empty;
    }
}