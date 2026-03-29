using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class StockTake : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string ShiftNumber { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string NozzleCode { get; set; } = string.Empty;
        [Precision(18,2)] public decimal ClosingReading { get; set; }
        [Precision(18,2)] public decimal OpeningReading { get; set; }
        public int TakeType { get; set; }
    }
    public class StockTakeSummary : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string ShiftNumber { get; set; } = string.Empty;
        [Required, StringLength(4), Unicode(false)]
        public string NozzleCode { get; set; } = string.Empty;
        [Precision(18,2)] public decimal OpeningReading { get; set; }
        [Precision(18,2)] public decimal ExpectedOpeningReading { get; set; }
        [Precision(18,2)] public decimal OpeningVariance { get; set; }
        [Precision(18,2)] public decimal ClosingReading { get; set; }
        [Precision(18,2)] public decimal QuantitySold { get; set; }
        [Precision(18,2)] public decimal ExpectedClosingReading { get; set; }
        [Precision(18,2)] public decimal ClosingVariance { get; set; }
        public int VarianceStatus { get; set; }//VARIANCE,PENDING,CLOSED
    }
    //delivered stock to a certain station and certain tank model

}
