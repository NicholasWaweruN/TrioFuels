using DataAccessLayer.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Helpers;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public enum CarWashShiftStatus { Open, Closed }

	public class CarWashShift : BaseEntity
	{
		[StringLength(50), Unicode(false)]
		public string Name { get; set; } = string.Empty;   // "Morning Shift"
		public CarWashShiftStatus Status { get; set; } = CarWashShiftStatus.Open;
		// opened-at is DateCreated (inherited); closed-at only set when shift ends
		public DateTime? ClosedAt { get; set; } = EatTime.Now;
		public decimal ExpectedCash { get; set; } = 0m;
		public decimal ActualCashCounted { get; set; } = 0m;
		public decimal Difference { get; set; } = 0m;
		[StringLength(200), Unicode(true)]
		public string? VarianceReason { get; set; } =  string.Empty;
		public ICollection<CarWashTransaction> Transactions { get; set; } = new List<CarWashTransaction>();
	}
}
