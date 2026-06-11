using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityModels.Daraja
{
	public class StkTransaction
	{
		public int Id { get; set; }

		public string CheckoutRequestId { get; set; } = string.Empty;

		public string MerchantRequestId { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public long Amount { get; set; }

		public string TillNumber { get; set; } = string.Empty;

		public string AccountReference { get; set; } = string.Empty;

		public string Status { get; set; } = "Pending";

		public string MpesaReceiptNumber { get; set; } = string.Empty;

		public string ResultCode { get; set; } = string.Empty;

		public string ResultDescription { get; set; } = string.Empty;

		public DateTime DateCreated { get; set; }

		public DateTime? DateCompleted { get; set; }
	}
}
