using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Reports.Shifts_Clossing
{
	namespace DataAccessLayer.DTOs.Reports
	{
		/// <summary>
		/// Response DTO for the shift closing report.
		/// One report per shift per dispenser.
		/// </summary>
		public class ShiftClosingReportDto
		{
			// Header
			public string StationName { get; set; } = string.Empty;
			public string ShiftNumber { get; set; } = string.Empty;
			public string AttendantName { get; set; } = string.Empty;
			public string AttendantCode { get; set; } = string.Empty;
			public string DispenserCode { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string ProductName { get; set; } = string.Empty;
			public DateTime ShiftOpenedAt { get; set; }
			public DateTime ShiftClosedAt { get; set; }
			public DateTime ReportGeneratedAt { get; set; } = DateTime.UtcNow;

			// Totalizer
			public decimal OpeningTotalizer { get; set; }
			public decimal ClosingTotalizer { get; set; }
			public decimal TotalizerDifference { get; set; } // Closing - Opening

			// Sales by payment method
			public List<PaymentTypeSummary> PaymentBreakdown { get; set; } = new();

			// Totals
			public decimal TotalSystemLitres { get; set; }   // Σ litres from all sales
			public decimal TotalSystemAmount { get; set; }    // Σ amount from all sales
			public int TotalTransactions { get; set; }

			// Variance
			public decimal VarianceLitres { get; set; }  // TotalizerDifference - TotalSystemLitres
			public decimal VarianceAmount { get; set; }   // VarianceLitres × unit price
			public string VarianceStatus { get; set; } = string.Empty; // "OK" | "SHORT" | "EXCESS"

			// Nozzle-level breakdown (if dispenser has multiple nozzles)
			public List<NozzleSummary> NozzleBreakdown { get; set; } = new();
		}

		public class PaymentTypeSummary
		{
			public string PaymentTypeName { get; set; } = string.Empty;
			public string PaymentTypeCode { get; set; } = string.Empty;
			public decimal TotalAmount { get; set; }
			public decimal TotalLitres { get; set; }
			public int TransactionCount { get; set; }
		}

		public class NozzleSummary
		{
			public string NozzleCode { get; set; } = string.Empty;
			public string NozzleName { get; set; } = string.Empty;
			public string ProductName { get; set; } = string.Empty;
			public decimal OpeningTotalizer { get; set; }
			public decimal ClosingTotalizer { get; set; }
			public decimal Difference { get; set; }
			public decimal SystemLitres { get; set; }
			public decimal Variance { get; set; }
		}
	}
}
