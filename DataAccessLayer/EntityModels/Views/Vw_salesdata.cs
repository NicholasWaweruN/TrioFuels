using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Views
{
	using System;
	using Microsoft.EntityFrameworkCore;

	[Keyless]
	public class Vw_SalesData 
	{
		public string ShiftNumber { get; set; } = string.Empty;
		public string ShiftNumber2 { get; set; } = string.Empty;
		public string SaleId { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string DispenserName { get; set; } = string.Empty;
		public string NozzleName { get; set; } = string.Empty;
		public string Attendant_Name { get; set; } = string.Empty;
		public decimal Litres { get; set; }
		public decimal Amount { get; set; }
		public DateTime SalesDate { get; set; }
		public string PaymentType { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string TillNumber { get; set; } = string.Empty;
		public string Vehicle { get; set; } = string.Empty;
		public string StorageLocation { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string CustomerName { get; set; } = string.Empty;
		public string Transid { get; set; } = string.Empty;
		public string NozzleCode { get; set; } = string.Empty;
		public string DispenserCode { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		public decimal Discount { get; set; }
		public string VehicleCode { get; set; } = string.Empty;
		public bool HasValue { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public DateTime? ConversionDate { get; set; }
	}

}
