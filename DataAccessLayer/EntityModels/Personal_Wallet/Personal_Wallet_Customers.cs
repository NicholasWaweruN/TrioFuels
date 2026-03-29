using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Personal_Wallet
{
	public class Personal_Wallet_Customers : BaseEntity 
	{
		[Required, MaxLength(20),Unicode(false)]
		public string WalletId  { get; set; } = string.Empty;
		[Required, MaxLength(50), Unicode(false)]
		public string FirstName  { get; set; } = string.Empty;
		[Required, MaxLength(50), Unicode(false)]
		public string MiddleName  { get; set; } = string.Empty;
		[MaxLength(50), Unicode(false)]
		public string LastName  { get; set; } = string.Empty;
		[Required, MaxLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required, MaxLength(50), Unicode(false)]
		public string Email { get; set; } = string.Empty;
		[Required, MaxLength(20), Unicode(false)]
		public string IdentificationNumber { get; set; } = string.Empty;
		[Precision(18, 2)] public decimal Credit { get; set; }
		public bool Receive_Receipts { get; set; }
		public bool Receive_Statements { get; set; }
		public bool IsActive { get; set; }
		public decimal Discount  { get; set; }
		[Required, MaxLength(2000), Unicode(false)]
		public string HashedPin { get; set; } = string.Empty;
	}
}
