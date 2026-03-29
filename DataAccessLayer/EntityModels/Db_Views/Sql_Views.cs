using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Db_Views
{
	public class OtopaySales
	{
		[Required, StringLength(20), Unicode(false)]
		public string SaleId { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string StationName { get; set; } = string.Empty;
		[Required, StringLength(5), Unicode(false)]
		public string DispenserName { get; set; } = string.Empty;
		[Required, StringLength(5), Unicode(false)]
		public string NozzleName { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string AttendantName { get; set; } = string.Empty; // Concatenated Full Name (FirstName + MiddName + LastName)
		[Precision(18, 2)]
		public decimal Litres { get; set; }       // Assuming Litres is a decimal or float
		[Precision(18, 2)]
		public decimal Amount { get; set; }       // Assuming Amount is a decimal or float
		public DateTime SalesDate { get; set; }
		[Required, StringLength(40), Unicode(false)]
		public string PaymentType { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Price { get; set; }        // Assuming Price is a decimal or float
		[Required, StringLength(20), Unicode(false)]
		public string TillNumber { get; set; } = string.Empty;       // Assuming TillNumber is an integer
		[Required, StringLength(20), Unicode(false)]
		public string Vehicle { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string StorageLocation { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string ProductName { get; set; } = string.Empty;
		[Required, StringLength(40), Unicode(false)]
		public string TransId { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string CustomerName { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string Terminal { get; set; } = string.Empty;
		[Precision(18,2)] public decimal Discount { get; set; } 
		[Precision(18,2)] public decimal RunningBalance { get; set; } 
	}

}
