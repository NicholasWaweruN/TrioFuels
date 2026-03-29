using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sales.Target
{
	public class Targets : BaseEntity
	{
		[StringLength(10),Unicode(false),Required]
		public string StationCode { get; set; }  = string.Empty;
		 [Precision(18,2)] public decimal TargetAmount { get; set; }
		 [Precision(18,2)] public decimal TotalSales { get; set; } = 0;
		public int Month { get; set; }
		public int Year { get; set; }
	}
}
