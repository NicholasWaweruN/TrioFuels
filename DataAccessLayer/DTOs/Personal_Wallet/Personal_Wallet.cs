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

    public class CustomerStatementLineDto
    {
        public DateTime Date { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public string VehicleCode { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; } // null when no vehicle match
        public string Narration { get; set; } = string.Empty;
        public string UserReference { get; set; } = string.Empty;
        public int TopUpType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal RunningBalance { get; set; }
    }

    public class CustomerStatementDto
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<CustomerStatementLineDto> Lines { get; set; } = new();
    }
}
