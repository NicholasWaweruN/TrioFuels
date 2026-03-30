using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
	[Keyless]
	public class SalesReportRow
	{
		public string? SaleId { get; set; }
		public DateTime SalesDate { get; set; }
		public string? TransId { get; set; }
		public string? StationName { get; set; }
		public string? AttendantName { get; set; }
		public string? CustomerName { get; set; }
		public string? TillNumber { get; set; }
		public string? ShiftNumber { get; set; }
		public string? Vehicle { get; set; }
		public string? ProductName { get; set; }
		public string? PaymentType { get; set; }
		public decimal? Litres { get; set; }
		public decimal? Price { get; set; }
		public decimal? Discount { get; set; }
		public decimal? Amount { get; set; }
		public string? DispenserName { get; set; }
		public string? NozzleName { get; set; }
		public string? StorageLocation { get; set; }
		public decimal? RunningBalance { get; set; }
	}
}