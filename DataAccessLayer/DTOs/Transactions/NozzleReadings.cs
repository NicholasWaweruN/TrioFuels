using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTOs.Transactions
{
    public class NozzleReadingsdto
    {
         [Precision(18,2)] public decimal NozzleReading { get; set; }
        public string NozzleCode { get; set; } = string.Empty;
    }
}
