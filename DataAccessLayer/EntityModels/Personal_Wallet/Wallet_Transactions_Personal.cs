using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Personal_Wallet
{
	public class Wallet_Transactions_Personal :  BaseEntity
	{
		public string WalletId { get; set; } = string.Empty;
		public string TransactionCode { get; set; } = string.Empty;
		[Precision(12,2)] 
		public decimal Credit { get; set; }
		[Precision(12,2)] 
		public decimal Debit { get; set; }
		public string TransactionType { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string SaleId { get; set; } = string.Empty;
		public string VehicleCode { get; set; } = string.Empty;
		public string PhoneNumber { get; set;} = string.Empty;
	}
}
