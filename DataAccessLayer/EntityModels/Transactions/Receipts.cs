using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Transactions
{
	public class TransactionReceipts : BaseEntity
	{
		[StringLength(100),Unicode(false)]
		public string CustomerName { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string VehicleReg { get; set; } = string.Empty;
		public double Quantity { get; set; }
		public double PricePerLitre { get; set; }
		public double Vat_Amount { get; set; }
		[StringLength(50), Unicode(false)]
		public string PaymentMethod { get; set; } = string.Empty;
		public double TotalAmount { get; set; }
		[StringLength(50), Unicode(false)]
		public string ReceiptNumber { get; set; } = string.Empty;
		public int Duplicate { get; set; } = 0;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required, StringLength(40), Unicode(false)]
		public string ServedBy { get; set; } = string.Empty ;
		[Required, StringLength(30), Unicode(false)]
		public string StationName { get; set;} = string.Empty;

	}
}

