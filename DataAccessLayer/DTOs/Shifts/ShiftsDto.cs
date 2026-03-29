using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Shifts
{
    public class Shiftstatus
    {
        public int ShiftStatus { get; set; }
        public string ShiftNumber { get; set; } = string.Empty;
    }
    public class Variances
    {
        public string ShiftNumber { get; set; } = string.Empty;
        public string NozzleCode { get; set; } = string.Empty;
        public string NozzleName { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Variance { get; set; }
         [Precision(18,2)] public decimal VarianceValue { get; set; }
    }

    public class VariancesList
    {
        public int ShiftStatus { get; set; }
        public List<Variances> variances { get; set; } = new List<Variances>();
    }
    public class ShiftVariance
    {
        public string ShiftStatus { get; set; } = string.Empty;
        public Variances Variances { get; set; } = new Variances();
    }
    public class ShiftSummary
    {
        public int ShiftStatus { get; set; }
        public string ShiftNumber { get; set; } = string.Empty;
        [Precision(18,2)] public decimal QuantitySold { get; set; }
        public int TotalEvents { get; set; }
		public bool IsStockTakeTaken { get; set; }
		[Precision(18, 2)] public decimal CashAtHand { get; set; }
	}
}
