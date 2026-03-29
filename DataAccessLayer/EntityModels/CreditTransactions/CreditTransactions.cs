using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.CreditTransactions
{
	public class CreditTransactions : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string CustomerCode { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Credit  { get; set; }
		[Precision(18, 2)]
		public decimal Debit { get; set; }
		[Required, StringLength(50), Unicode(false)]
		public string SaleId { get; set; } = string.Empty;
		[StringLength(50), Unicode(false)]
		public string TransactionReference { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string VehicleCode { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string StationCode { get; set; } = string.Empty;
	}
}
