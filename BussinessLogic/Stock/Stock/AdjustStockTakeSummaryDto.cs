using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Stock.Stock
{
	public class AdjustStockTakeSummaryDto
	{
		[Required]
		public string ShiftNumber { get; set; } = string.Empty;
		[Required]
		public List<Reading> Readings { get; set; } = [];
	}

	public class Reading
	{
		[Required]
		public string NozzleCode { get; set; } = string.Empty;
		[Required]
		 [Precision(18,2)] public decimal OpeningReading { get; set; }
		[Required]
		 [Precision(18,2)] public decimal ClosingReading { get; set; }
	}
}