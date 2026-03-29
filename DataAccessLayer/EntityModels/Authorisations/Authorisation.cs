using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Authorisations
{
	public class GasPriceAuthorizedPrice : BaseEntity
	{

		[Required] [MaxLength(50)] public string VehicleCode { get; set; } = string.Empty;
		[Required] [MaxLength(50)] public string ProductCode { get; set; } = string.Empty;
		[Precision(18,2)]  public decimal AuthorizedPricePerLitre { get; set; }
		[Precision(18, 2)] public decimal OriginalPrice  { get; set; }
		[MaxLength(100)]   public string Approver { get; set; } = string.Empty;
		public DateTime DateApproved  { get; set; }
		public bool IsApproved { get; set; } = false;
	}

}
