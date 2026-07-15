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

		public long VehicleTypeId { get; set; }
		public VehicleType VehicleType { get; set; } = null!;

		[StringLength(30), Unicode(false)]
		public string ReceiptNumber { get; set; } = string.Empty;
		public decimal TotalAmount { get; set; }
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		public int PaymentMethod { get; set; } // see CarWashPaymetMethod constants
		public decimal AmountReceived { get; set; } = 0;  // cash only
		public decimal Change { get; set; } = 0;          // cash only

		[StringLength(15), Unicode(false)]
		public string? PhoneNumber { get; set; }       // M-Pesa STK only
		[StringLength(20), Unicode(false)]
		public string? MpesaReference { get; set; }    // M-Pesa STK only
		public bool IsReversed { get; set; } = false;
		public ICollection<CarWashTransactionItem> Items { get; set; } = new List<CarWashTransactionItem>();
		public long? CustomerId { get; set; }       // NEW — FK to CarwashCustomer
		public decimal DiscountAmount { get; set; }
		public decimal AmountDue { get; set; }  //        public decimal DiscountAmount { get; set; }  // NEW — standing + negotiated, combined
											   //        public decimal AmountDue { get; set; }       // NEW — TotalAmount - DiscountAmount
	}

	public class CarwashCustomer
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty; // unique, searchable

		public bool IsCreditCustomer { get; set; }
		public decimal CreditLimit { get; set; }        // max they can owe
		public decimal CurrentBalance { get; set; }      // what they currently owe (running total)

		public bool IsDiscountCustomer { get; set; }
		public decimal DiscountAmount { get; set; }      // e.g. 30 KES, mostly applied on base wash

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<CarwashCreditTransaction> CreditTransactions { get; set; } = new List<CarwashCreditTransaction>();
	}

	public class CarwashCreditTransaction
	{
		public long Id { get; set; }
		public int CarwashCustomerId { get; set; }
		public decimal Debit { get; set; }        // credit extended
		public decimal Credit { get; set; }       // payment received
		public decimal RunningBalance { get; set; }
		public string? Description { get; set; }
		public long? SaleId { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
