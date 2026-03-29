using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Setups
{
    public class UpdatePrice
    {
        [Required]
        public string ProductCode { get; set; }  = string.Empty;
        [Required]
         [Precision(18,2)] public decimal NewPrice { get; set; }
        [Required]
        public string StationCode { get; set; } = string.Empty;

    }
    public class GlobalUpdatePrice
    {
        [Required]
        public string ProductCode { get; set; } = string.Empty;
        [Required]
         [Precision(18,2)] public decimal NewPrice { get; set; }
    }

}
