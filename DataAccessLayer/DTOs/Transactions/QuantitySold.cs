using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.TransactionsService
{
    public class QuantitySold
    {
        public string NozzleCode { get; set; } = string.Empty;
        public string ShiftNumber { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Quantity { get; set; }
    }
}