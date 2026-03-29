using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sales.Archirve
{
	public class SalesTransactions  
	{
		[Key]
		[Required, StringLength(100), Unicode(false)]
		public string OrderId { get; set; } = string.Empty;
		public DateTime Date { get; set; }
		[Required, StringLength(8000), Unicode(false)]
		public string TransactionId { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string OutLet { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string Attendant { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string CustomerName { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string TillNumber { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string TerminalName { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string RegistrationNumber { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string Product { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string PaymentType { get; set; } = string.Empty;
		 [Precision(18,2)] public decimal Quantity { get; set; } 
		 [Precision(18,2)] public decimal UnitPrice { get; set; }
		 [Precision(18,2)] public decimal RunningReading { get; set; }  // Change to correct data type if needed
		 [Precision(18,2)] public decimal AmountPaid { get; set; }
		[Required, StringLength(100), Unicode(false)]
		public string DispenserName { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string NozzleName { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string StorageLocation { get; set; } = string.Empty;
		public DateTime OriginalDate { get; set; }
	}

}
