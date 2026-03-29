using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Approvals
{
	public class PriceApproval : BaseEntity
	{
		[Required,StringLength(4),Unicode]
		public string ApprovalCode { get; set; } = string.Empty;
		[Precision(18,2)]
		public decimal OriginalPrice { get;set; }
		[Precision(18,2)]
		public decimal ProposedPrice { get;set; } 
		[Required, StringLength(100), Unicode]
		public string NumberPlate {  get; set; } = string.Empty;
		public bool IsApproved { get; set; }
		[Required, StringLength(100), Unicode]
		public string Notes { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode]
		public string Initiator { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode]
		public string Approver  { get; set; } = string.Empty;
		public bool  IsApprovalExecuted { get; set; }
		[Precision(18,2)]
		public decimal Quantity { get; set; } 
		public string ShiftNumber { get; set; } = string.Empty;

	}

	public class PriceApprovers : BaseEntity
	{
		[Unicode(false),StringLength(10),Required]
		public string ApprovalUserCode { get; set;} = string.Empty;
		[Unicode(false), StringLength(50), Required]
		public string AppoverName { get; set;} = string.Empty;
		public bool IsActive { get; set; } = true;
	}
}
