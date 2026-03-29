using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Transactions
{
    public class StockTakeDto
    {
        public List<Readings> Readings { get; set; } = [];

    }
    public class Readings
    {
        [Required]
         [Precision(18,2)] public decimal Reading { get; set; }
        [Required]
        public string NozzleCode { get; set; } = string.Empty;
    }
}