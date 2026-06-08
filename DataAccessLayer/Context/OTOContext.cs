using BusinessLogic.CustomerService;
using BusinessLogic.Sales.Archirve;
using BusinessLogic.Sales.Target;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.EntityModels.Approvals;
using DataAccessLayer.EntityModels.Authorisations;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Emails;
using DataAccessLayer.EntityModels.Loyalty_Program;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.Personal_Wallet;
using DataAccessLayer.EntityModels.ProtoBase;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.EntityModels.StockTake;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.EntityModels.Views;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public partial class OTOContext : IdentityDbContext<ApplicationUser, UserRoles, string>
    {
        public OTOContext(DbContextOptions<OTOContext> options) : base(options) { }

        // --- Station & Hardware ---
        public DbSet<GasStation> Stations { get; set; }
        public DbSet<Dispenser> Dispensers { get; set; }
        public DbSet<Nozzle> Nozzles { get; set; }
        public DbSet<Tank> Tank { get; set; }
        public DbSet<TankSizes> TankSizes { get; set; }
        public DbSet<PdaDevices> PdaDevices { get; set; }
        public DbSet<Tills> Tills { get; set; }
        public DbSet<DispenserAssignment> DispenserAssignments { get; set; }
        public DbSet<TotalizerReadings> TotalizerReadings { get; set; }

        // --- Products & Pricing ---
        public DbSet<Products> Products { get; set; }
        public DbSet<PetroleumProducts> PetroleumProducts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<PriceSchedule> PriceSchedules { get; set; }
        public DbSet<GasPriceAuthorizedPrice> GasPriceAuthorizedPrice { get; set; }
        public DbSet<PriceApproval> PriceApproval { get; set; }
        public DbSet<PriceApprovers> PriceApprovers { get; set; }

        // --- Transactions ---
        public DbSet<QuantityTransactions> QuantityTransactions { get; set; }
        public DbSet<PaymentTransactions> PaymentTransactions { get; set; }
        public DbSet<MovedTransactions> MovedTransactions { get; set; }
        public DbSet<FailedTransactions> FailedTransactions { get; set; }
        public DbSet<SalesTransactions> SalesTransactions { get; set; }
        public DbSet<CustomerTransactions> CustomerTransactions { get; set; }
        public DbSet<TankTransactions> TankTransactions { get; set; }
        public DbSet<TankTransactionsSummary> TankTransactionsSummaries { get; set; }
        public DbSet<CreditTransactions> CreditTransactions { get; set; }
        public DbSet<TransactionReceipts> TransactionReceipts { get; set; }

        // --- M-Pesa ---
        public DbSet<MpesaTransaction> MpesaTransactions { get; set; }
        public DbSet<MpesaC2bPayments> MpesaC2bPayments { get; set; }

        // --- Customers ---
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleStatusTypes> VehicleStatusTypes { get; set; }
        public DbSet<TransFeredVehicles> TransFeredVehicles { get; set; }
        public DbSet<Walk_In_Customers> Walk_In_Customers { get; set; }
        public DbSet<Customer_Complains> Customer_Complains { get; set; }
        public DbSet<CustomerFunds> CustomerFunds { get; set; }
        public DbSet<CustomerBalanceDto> CustomerBalanceDtos { get; set; }

        // --- Loyalty & Wallet ---
        public DbSet<RoyaltyPoints> RoyaltyPoints { get; set; }
        public DbSet<Vouchers> Vouchers { get; set; }
        public DbSet<Coupons> Coupons { get; set; }
        public DbSet<LoyaltySubscription> LoyaltySubscriptions { get; set; }
        public DbSet<Personal_Wallet_Customers> Personal_Wallet_Customers { get; set; }
        public DbSet<Wallet_Transactions_Personal> Wallet_Transactions_Personal { get; set; }

        // --- Shifts & Stock ---
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<StockTake> StockTakes { get; set; }
        public DbSet<StockTakeSummary> StockTakeSummaries { get; set; }
        public DbSet<StockTakeScheduler> StockTakeScheduler { get; set; }

        // --- Messaging & Notifications ---
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<MessageDetails> MessageDetails { get; set; }
        public DbSet<AfricasTalkingCallback> AfricasTalkingCallback { get; set; }
        public DbSet<SmsCallbacks> SmsCallbacks { get; set; }
        public DbSet<BulkMessageLog> BulkMessageLogs { get; set; }
        public DbSet<RescheduledMessages> RescheduledMessages { get; set; }

        // --- Auth, Roles & Security ---
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAndPermisions> RoleAndPermisions { get; set; }
        public DbSet<RoleToUser> RoleToUser { get; set; }
        public DbSet<ApiPermisions> ApiPermisions { get; set; }
        public DbSet<Otps> Otps { get; set; }
        public DbSet<OtpTypes> OtpTypes { get; set; }
        public DbSet<PasswordHistory> PasswordHistory { get; set; }

        // --- Apps & Audit ---
        public DbSet<ProtoApps> ProtoApps { get; set; }
        public DbSet<UserApps> UserApps { get; set; }
        public DbSet<UserTrail> UserTrails { get; set; }
        public DbSet<ErrorTrail> ErrorTrails { get; set; }

        // --- Setup & Config ---
        public DbSet<Codegenerator> Codegenerators { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Setup> Setup { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<TopUpTypes> TopUpTypes { get; set; }
        public DbSet<Targets> Targets { get; set; }
        public DbSet<OtogasJobs> OtogasJobs { get; set; }

        // --- Organisations ---
        public DbSet<Organisations> Organisations { get; set; }
        public DbSet<OrganisationTypes> OrganisationTypes { get; set; }

        // --- Reports & Views ---
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Vw_SalesData> VwSalesData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureDecimalProperties(modelBuilder);
            ConfigureUnicodeProperties(modelBuilder);
            ConfigureIndexes(modelBuilder);
            ConfigureSeedData(modelBuilder);
            ConfigureKeylessEntities(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
