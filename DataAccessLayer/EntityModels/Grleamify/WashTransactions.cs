using DataAccessLayer.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class CarWashTransaction : BaseEntity
	{
		public long ShiftId { get; set; }
		public CarWashShift Shift { get; set; } = null!;

		[StringLength(30), Unicode(false)]
		public string ReceiptNumber { get; set; } = string.Empty;
		public decimal TotalAmount { get; set; }

		public int PaymentMethod { get; set; } // see CarWashPaymetMethod constants

		public decimal AmountReceived { get; set; } = 0;  // cash only
		public decimal Change { get; set; } = 0;          // cash only

		[StringLength(15), Unicode(false)]
		public string? PhoneNumber { get; set; }       // M-Pesa STK only
		[StringLength(20), Unicode(false)]
		public string? MpesaReference { get; set; }    // M-Pesa STK only
		public bool IsReversed { get; set; } = false;
		public ICollection<CarWashTransactionItem> Items { get; set; } = new List<CarWashTransactionItem>();
	}
}