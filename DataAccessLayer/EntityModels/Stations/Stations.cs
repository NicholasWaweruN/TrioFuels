using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Stations
{
	/// <summary>
	/// Represents a gas station with its details.
	/// </summary>
	public class GasStation : BaseEntity
	{
		[Required, StringLength(30), Unicode(false)]
		public string StationName { get; set; } = string.Empty;

		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty;

		[Required, StringLength(50), Unicode(false)]
		public string StationAddress { get; set; } = string.Empty;

		[Required, StringLength(50), Unicode(false)]
		public string LocationId { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}

	/// <summary>
	/// Represents a fuel dispenser at a gas station.
	/// </summary>
	/// 

	public class PetroleumProducts : BaseEntity
	{
		[StringLength(10), Unicode(false)]
		[Required]
		public string PetroleumCode { get; set; } = string.Empty;
		[Required,StringLength(30), Unicode(false)]
		public string PetroleumName {  get; set; } = string.Empty; 
	}

	public class Dispenser : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty; // FK to GasStation

		[Required, StringLength(40), Unicode(false)]
		public string DispenserName { get; set; } = string.Empty;

		[Required, StringLength(10), Unicode(false)]
		public string DispenserCode { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string TillNumber { get; set; } = string.Empty; // FK to Till
		public bool IsActive { get; set; }
		[Required, StringLength(20), Unicode(false)]
		public string PetroleumCode  { get; set; } = string.Empty;

		[Required, StringLength(20), Unicode(false)]
		public string StorageLocation { get; set; } = string.Empty;
	}




	/// <summary>
	/// Represents an individual nozzle in a dispenser.
	/// </summary>
	public class Nozzle : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string DispenserCode { get; set; } = string.Empty; // FK to Dispenser

		[Required, StringLength(30), Unicode(false)]
		public string NozzleName { get; set; } = string.Empty;

		[Required, StringLength(10), Unicode(false)]
		public string NozzleCode { get; set; } = string.Empty;

		public bool IsActive { get; set; }
	}

	/// <summary>
	/// Represents a fuel storage tank.
	/// </summary>
	public class Tank : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty; // FK to GasStation

		[Required, StringLength(20), Unicode(false)]
		public string TankName { get; set; } = string.Empty;

		[Required, StringLength(10), Unicode(false)]
		public string TankCode { get; set; } = string.Empty;

		public bool IsActive { get; set; }
	}

	/// <summary>
	/// Records transactions involving tanks.
	/// </summary>
	public class TankTransactions : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty; // FK to GasStation

		[Required, StringLength(10), Unicode(false)]
		public string TankCode { get; set; } = string.Empty; // FK to Tank

		[Required, StringLength(50), Unicode(false)]
		public string OrderId { get; set; } = string.Empty;

		[Required, StringLength(50), Unicode(false)]
		public string SaleId { get; set; } = string.Empty;

		public double DeliveredQuantity { get; set; }

		public double QuantitySold { get; set; }
	}

	/// <summary>
	/// Summarizes tank transaction details.
	/// </summary>
	public class TankTransactionsSummary : BaseEntity
	{
		[Required, StringLength(10), Unicode(false)]
		public string StationCode { get; set; } = string.Empty; // FK to GasStation

		[Required, StringLength(10), Unicode(false)]
		public string TankCode { get; set; } = string.Empty; // FK to Tank
		[Required, StringLength(30), Unicode(false)]
		public string OrderId { get; set; } = string.Empty; // FK to QuantityTransactions

		public double DeliveredQuantity { get; set; }

		public double QuantitySold { get; set; }

		public double ExpectedQuantity { get; set; }

		public double ActualQuantity { get; set; }

		public double Variance { get; set; }
	}

	/// <summary>
	/// Tracks deliveries of LPG or other fuels.
	/// </summary>


	/// <summary>
	/// Represents transaction tills at a station.
	/// </summary>
	public class Tills : BaseEntity
	{
		[Required, StringLength(40), Unicode(false)]
		public string TillName { get; set; } = string.Empty;

		[Required, StringLength(20), Unicode(false)]
		public string TillNumber { get; set; } = string.Empty;

		public bool IsActive { get; set; }

		[Required, StringLength(20), Unicode(false)]
		public string StoreNumber { get; set; } = string.Empty;

		public DateTime LastFetch { get; set; }

		public int OffsetValue { get; set; }
	}
}
