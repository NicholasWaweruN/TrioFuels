using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTOs.Transactions
{
    public class CreditCustomerTransactionDto 
    {
        public string VehicleCode { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Credit { get; set; } = 0;
    }
    public class DebitCustomerTransactionDto
    {
        public string VehicleCode { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Debit { get; set; } = 0;
    }
}