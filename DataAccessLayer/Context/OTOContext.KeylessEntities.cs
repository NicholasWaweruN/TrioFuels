using BusinessLogic.CustomerService;
using BusinessLogic.Sales.Archirve;
using BusinessLogic.Sales.Target;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.Db_Views;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public partial class OTOContext
    {
        private static void ConfigureKeylessEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsageBalanceDto>().HasNoKey();
            modelBuilder.Entity<ValueDto>().HasNoKey();
            modelBuilder.Entity<OtopaySales>().HasNoKey();
            modelBuilder.Entity<IntValue>().HasNoKey();
            modelBuilder.Entity<AllCredits>().HasNoKey();
            modelBuilder.Entity<CustomerTransactionSummary>().HasNoKey();
            modelBuilder.Entity<SalesTransaction>().HasNoKey();
            modelBuilder.Entity<GarageTransactionDto>().HasNoKey();
            modelBuilder.Entity<SalesReportRow>().HasNoKey();
        }
    }
}
