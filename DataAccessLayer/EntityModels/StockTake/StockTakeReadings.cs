using DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.StockTake
{
	public class TotalizerReadings : BaseEntity
	{
		public string NozzlesCode { get; set; } = string.Empty;
		public decimal Reading { get; set; }
	}

	public class StockTakeScheduler 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string DispenserCode { get; set; } = string.Empty;
		public bool IsTaken { get; set; } = false;
		public DateTime TakeDateTime { get; set; }
	}
}
