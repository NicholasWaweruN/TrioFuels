using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Misc
{
    //public class Complains : BaseEntity
    //{
    //    [Required, StringLength(20), Unicode(false)]
    //    public string Complainid { get; set; } = "C" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
    //    [Required, StringLength(20), Unicode(false)]
    //    public string ComplainTypeCode { get; set; } = string.Empty;
    //    [Required, StringLength(20), Unicode(false)]
    //    public string VehicleCode { get; set; } = string.Empty;
    //    public int Litres { get; set; } 
    //    public decimal Amount { get; set; }
    //    public DateTime Date { get; set; }
    //    public bool IsActive { get; set; } = true;
    //}
    public class ComplainTypes : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string ComplainType { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string ComplainTypeCode { get; set; } = string.Empty;

    }
}
