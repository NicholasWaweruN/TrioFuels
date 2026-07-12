using DataAccessLayer.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class VehicleType : BaseEntity
	{
		[StringLength(50), Unicode(false)]
		public string Name { get; set; } = string.Empty;   // "Saloon", "SUV", "Pickup", "Truck"
		public bool IsActive { get; set; } = true;
		public ICollection<CarWashProductPrice> ProductPrices { get; set; } = [];

		public ICollection<CarWashTransaction> Transactions { get; set; } = [];
	}
}
