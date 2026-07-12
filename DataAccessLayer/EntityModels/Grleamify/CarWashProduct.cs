using DataAccessLayer.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class CarWashProduct : BaseEntity
	{
		[StringLength(50), Unicode(false)]
		public string Name { get; set; } = string.Empty;      // "Basic Wash", "Vacuum", "Waxing"
		public decimal Price { get; set; }                     // fallback/base price
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Optional per-vehicle-type overrides. If a VehicleType has no row here
		/// for this product, use the base Price above.
		/// </summary>
		public ICollection<CarWashProductPrice> VehicleTypePrices { get; set; } = new List<CarWashProductPrice>();
	}
}
