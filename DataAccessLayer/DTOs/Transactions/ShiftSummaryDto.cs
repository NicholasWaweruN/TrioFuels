using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace DataAccessLayer.DTOs.Transactions
{
    public class ShiftSummaryDto
    {

         [Precision(18,2)] public decimal FuelingEvents { get; set; } = 0;
         [Precision(18,2)] public decimal QuantitySold { get; set; } = 0;
    }
}
