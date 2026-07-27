using DataAccessLayer.Common;
using DataAccessLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class CarWashPackage : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;

		public ICollection<CarWashPackageItem> Items { get; set; } = new List<CarWashPackageItem>();
		public ICollection<CarWashPackagePrice> VehicleTypePrices { get; set; } = new List<CarWashPackagePrice>();
	}

	// Which products make up this package
	public class CarWashPackageItem : BaseEntity
	{
		public long PackageId { get; set; }
		public CarWashPackage Package { get; set; } = null!;
		public long ProductId { get; set; }
		public CarWashProduct Product { get; set; } = null!;
	}

	// The discounted bundle price, per vehicle type — mirrors CarWashProductPrices
	public class CarWashPackagePrice : BaseEntity
	{
		public long PackageId { get; set; }
		public CarWashPackage Package { get; set; } = null!;
		public long VehicleTypeId { get; set; }
		public decimal Price { get; set; } // discounted total, not per-item
	}
}

