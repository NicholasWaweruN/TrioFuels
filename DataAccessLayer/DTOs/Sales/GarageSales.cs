using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Sales
{
	public class GarageTransactionDto
	{
		public string TransactionId { get; set; } = string.Empty;
		public string ItemName { get; set; } = string.Empty;
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		public string ItemCode { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Quantity { get; set; }
		[Precision(18,2)]
		public decimal Price { get; set; }
		[Precision(18, 2)]
		public decimal TotalAmount { get; set; }
		[Precision(18, 2)]
		public decimal Discount { get; set; }
		[Precision(18, 2)]
		public decimal NetAmount { get; set; }
		public string PaymentMethod { get; set; } = string.Empty;
		public string TillNumber { get; set; } = string.Empty;
		public DateTime SalesDate { get; set; }
		public string MpesaReference { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string SalesAgent { get; set; } = string.Empty;

	}
}
