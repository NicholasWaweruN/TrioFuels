using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.ProtoBase
{
	public class MpesaC2bPayments
	{
		public int Id { get; set; }
		[Required, StringLength(20), Unicode(false)]
		public string TransactionType { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string TransID { get; set; } = string.Empty;
		public DateTime? TransTime { get; set; }
		 [Precision(18,2)] public decimal TransAmount { get; set; }
		[Required, StringLength(20), Unicode(false)]
		public string BusinessShortCode { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string MSISDN { get; set; } = string.Empty; // Mobile Number
		[Required, StringLength(20), Unicode(false)]
		public string FirstName { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string MiddName { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string LastName { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public decimal? OrgAccountBalance { get; set; }
		public DateTime DateTimeStamp { get; set; }
		public int Status { get; set; }
		public decimal? UsageBalance { get; set; }
	}

}
