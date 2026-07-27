using DataAccessLayer.Common;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class CarWashTransactionItem : BaseEntity
	{
		public long TransactionId { get; set; }
		public CarWashTransaction Transaction { get; set; } = null!;
		public long ProductId { get; set; }
		public CarWashProduct Product { get; set; } = null!;
		public int Quantity { get; set; } = 1;
		public decimal UnitPrice { get; set; } // snapshot at sale time, not live product price
		public long? PackageId { get; set; }
		public CarWashPackage? Package { get; set; }
		public Guid? PackageInstanceId { get; set; }
	}
}
