using DataAccessLayer.Common;

namespace DataAccessLayer.EntityModels.Grleamify
{
	/// <summary>
	/// One row = the price of one CarWashProduct for one VehicleType.
	/// Falls back to CarWashProduct.Price when no row exists for a given vehicle type.
	/// </summary>
	public class CarWashProductPrice : BaseEntity
	{
		public long ProductId { get; set; }
		public CarWashProduct Product { get; set; } = null!;

		public long VehicleTypeId { get; set; }
		public VehicleType VehicleType { get; set; } = null!;

		public decimal Price { get; set; }
	}
}
