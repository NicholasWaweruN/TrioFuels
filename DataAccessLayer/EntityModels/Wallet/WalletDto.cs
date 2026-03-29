using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Wallet
{
	public class WalletDto
	{
		public class TransferCustomerBalanceDto
		{
			[Required]
			public string FromVehicleCode { get; set; } = string.Empty;
			[Required]
			public string ToVehicleCode { get; set; } = string.Empty;
			[Required]
			 [Precision(18,2)] public decimal Amount { get; set; } = 0;
		}
		public class TopUpCustomerWalletDto
		{
			public int PaymentType { get; set; } = 0;
			public string VehicleCode { get; set; } = string.Empty;
			public string TransactionReference { get; set; } = string.Empty;
			[Precision(18,2)] public decimal Amount { get; set; } = 0;
		}
		public class TopUpFundsDto
		{
			public string CustomerCode { get; set; } = string.Empty;
			public string TransactionReference { get; set; } = string.Empty;
			 [Precision(18,2)] public decimal Amount { get; set; } = 0;

		}
		public class CustomerBalanceDto
		{
			public string CustomerCode { get; set; } = string.Empty;
			public string CustomerName { get; set; } = string.Empty;
			public string VehicleCode { get; set; } = string.Empty;
			public string RegistrationNumber { get; set; } = string.Empty;
			 [Precision(18,2)] public decimal Balance { get; set; }
		}
		public class CustomerTransactionDto : BaseEntity
		{
			public string Description { get; set; } = string.Empty;
			 [Precision(18,2)] public decimal Credit { get; set; }
			 [Precision(18,2)] public decimal Debit { get; set; }
			 [Precision(18,2)] public decimal RunningBalance { get; set; }
		}
	}
}
