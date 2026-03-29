using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels.Personal_Wallet
{
	public class Personal_Wallet_Customer
	{
		[Key]
		public string WalletId { get; set; } = string.Empty;
		public string UserCode { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string IdentificationNumber { get; set; } = string.Empty;
		public decimal Discount { get; set; }
		public decimal Credit { get; set; }
	}

	public class Wallet_Transactions_PersonalDto 
	{
		[Key]
		public int TransactionId { get; set; }
		public string WalletId { get; set; } = string.Empty;
		public string SaleId { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public decimal Quantity { get; set; }
		public decimal TransAmount { get; set; }
		public decimal Price { get; set; }
		public string FueledBy { get; set; } = string.Empty;
		public DateTime DateFueled { get; set; }
		public virtual ICollection<PaymentDetail> PaymentArray { get; set; } = new List<PaymentDetail>();
	}

	public class PaymentDetail
	{
		[Key]
		public int PaymentId { get; set; }
		public string TransactionId { get; set; } = string.Empty;
		public string PaymentType { get; set; } = string.Empty;
		public string TransID { get; set; } = string.Empty;
		public decimal TransAmount { get; set; }
	}
}
