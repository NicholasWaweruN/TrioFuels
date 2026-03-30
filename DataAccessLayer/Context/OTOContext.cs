using BusinessLogic.CustomerService;
using BusinessLogic.Sales.Archirve;
using BusinessLogic.Sales.Target;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.DTOs.Payments;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.Approvals;
using DataAccessLayer.EntityModels.Authorisations;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Db_Views;
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
	public class OTOContext : IdentityDbContext<ApplicationUser, UserRoles, string>
	{
		public OTOContext(DbContextOptions<OTOContext> options) : base(options) { }
        public DbSet<Codegenerator> Codegenerators { get; set; }
        public DbSet<Dispenser> Dispensers { get; set; }
        public DbSet<GasStation> Stations { get; set; }
        public DbSet<Nozzle> Nozzles { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<QuantityTransactions> QuantityTransactions { get; set; }
		public DbSet<Settings> Settings { get; set; } 
		public DbSet<MovedTransactions> MovedTransactions { get; set; }
		public DbSet<PaymentTransactions> PaymentTransactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<StockTake> StockTakes { get; set; } 
        public DbSet<StockTakeSummary> StockTakeSummaries { get; set; }
        public DbSet<Customer_Complains> Customer_Complains { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAndPermisions> RoleAndPermisions { get; set; }
		public DbSet<RoleToUser> RoleToUser { get; set; }
		public DbSet<Otps> Otps { get; set; }
        public DbSet<OtpTypes> OtpTypes { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Tills> Tills { get; set; }
        public DbSet<Tank> Tank { get; set; }
        public DbSet<PdaDevices> PdaDevices { get; set; }
        public DbSet<MpesaTransaction> MpesaTransactions { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<CustomerTransactions> CustomerTransactions { get; set; }
        public DbSet<VehicleStatusTypes> VehicleStatusTypes { get; set; }
        public DbSet<UserTrail> UserTrails { get; set; }
		public DbSet<ErrorTrail> ErrorTrails { get; set; }
		public DbSet<ProtoApps> ProtoApps { get; set; }
        public DbSet<UserApps> UserApps { get; set; }
        public DbSet<DispenserAssignment> DispenserAssignments { get; set; }
        public DbSet<TankTransactions> TankTransactions { get; set; }
        public DbSet<TankTransactionsSummary> TankTransactionsSummaries { get; set; }
        public DbSet<MessageDetails> MessageDetails { get; set; }
        public DbSet<AfricasTalkingCallback> AfricasTalkingCallback { get; set; }
        public DbSet<CustomerBalanceDto> CustomerBalanceDtos { get; set; }
        public DbSet<ApiPermisions> ApiPermisions { get; set; }
        public DbSet<TransFeredVehicles> TransFeredVehicles { get; set; }
        public DbSet<Setup> Setup  { get; set; }
		public DbSet<Walk_In_Customers> Walk_In_Customers { get; set; }
		public DbSet<TankSizes> TankSizes { get; set; }
		public DbSet<MpesaC2bPayments> MpesaC2bPayments { get; set; }
		public DbSet<FailedTransactions> FailedTransactions { get; set; }
		public DbSet<Messages> Messages { get; set; }
		public DbSet<SalesTransactions> SalesTransactions { get; set; }
		public DbSet<Targets> Targets { get; set; }
		public DbSet<CustomerFunds> CustomerFunds { get; set; }
		public DbSet<SmsCallbacks> SmsCallbacks { get; set; }
		public DbSet<TopUpTypes> TopUpTypes {  get; set; }
		public DbSet<TransactionReceipts> TransactionReceipts { get; set; }	
		public DbSet<OtogasJobs> OtogasJobs { get; set; }
		public DbSet<RescheduledMessages> RescheduledMessages  { get; set; }
		public DbSet<PriceSchedule> PriceSchedules { get; set; }
		public DbSet<BulkMessageLog> BulkMessageLogs { get; set; }
	
		public DbSet<Vouchers> Vouchers { get; set; }
		public DbSet<RoyaltyPoints> RoyaltyPoints { get; set; }
		public DbSet<Personal_Wallet_Customers> Personal_Wallet_Customers { get; set; }
		public DbSet<Wallet_Transactions_Personal> Wallet_Transactions_Personal { get; set; }
		public DbSet<TotalizerReadings> TotalizerReadings { get; set; }
		public DbSet<StockTakeScheduler> StockTakeScheduler { get; set; }
		public DbSet<Coupons> Coupons  { get; set; }
		public DbSet<LoyaltySubscription> LoyaltySubscriptions  { get; set; }
		public DbSet<Organisations> Organisations { get; set; } 
		public DbSet<Vw_SalesData> VwSalesData { get; set; }
		public DbSet<GasPriceAuthorizedPrice> GasPriceAuthorizedPrice { get; set; }
		public DbSet<PriceApproval> PriceApproval { get; set; }
		public DbSet<PriceApprovers> PriceApprovers { get; set; }
		public DbSet<PasswordHistory> PasswordHistory { get; set; }
		public DbSet<OrganisationTypes> OrganisationTypes { get; set; }
		public DbSet<CreditTransactions> CreditTransactions  { get; set; }
		public DbSet<PetroleumProducts> PetroleumProducts { get; set;  }
		public DbSet<Reports> Reports { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			ConfigureDecimalProperties(modelBuilder);
			ConfigureUnicodeProperties(modelBuilder);
			ConfigureIndexes(modelBuilder);
            ConfigureSeedData(modelBuilder);
			ConfigureKeylessEntities(modelBuilder);
        }


        private static void ConfigureDecimalProperties(ModelBuilder modelBuilder)
        {
            var decimalProperties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in decimalProperties)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }

        private static void ConfigureUnicodeProperties(ModelBuilder modelBuilder)
        {
            var unicodeProperties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string) && p.IsUnicode().HasValue);

            foreach (var property in unicodeProperties)
            { 
                property.SetIsUnicode(false);
            }
        }
		private static void ConfigureIndexes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CustomerBalanceDto>()
				.HasNoKey()
				.ToView("CustomerBalanceDto");


		modelBuilder.Entity<MpesaTransactionDto>()
				.HasNoKey()
				.ToView("MpesaTransactionDto");

			modelBuilder.Entity<MpesaTransactionsDto>()
				.HasNoKey()
				.ToView("MpesaTransactionsDto");

			modelBuilder.Entity<QuantityTransactions>()
					.Property(e => e.VehicleCode)
					.IsRequired()
					.HasMaxLength(10)
					.IsUnicode(false); // Maps to VARCHAR(10)

			modelBuilder.Entity<QuantityTransactions>()
			.Property(e => e.ShiftNumber)
			.IsRequired()
			.HasMaxLength(25)
			.IsUnicode(false); // Maps to VARCHAR(10)

			modelBuilder.Entity<QuantityTransactions>()
			.Property(e => e.NozzleCode)
			.IsRequired()
			.HasMaxLength(25)
			.IsUnicode(false); // Maps to VARCHAR(10)


			//modelBuilder.Entity<QuantityTransactions>()
			//   .HasIndex(q => new { q.QuantityCredit, q.VehicleCode, q.NozzleCode, q.RoundedDate, q.ShiftNumber })
			//   .IsUnique()
			//   .HasDatabaseName("IX_QuantityTransactions_UniqueConstraint");

			// Indexes for Codegenerator
			modelBuilder.Entity<Codegenerator>()
				.HasIndex(c => c.TypeName)
				.IsUnique()
				.HasDatabaseName("IX_Codegenerator_TypeName");

			// Indexes for Codegenerator
			modelBuilder.Entity<FailedTransactions>()
				.HasIndex(c => c.RegNo)
				.HasDatabaseName("IX_Codegenerator_RegNo");

			modelBuilder
			.Entity<Vw_SalesData>()
			.HasNoKey()
			.ToView("vw_SalesData");

			modelBuilder.Entity<Dispenser>()
				.HasIndex(d => d.DispenserCode)
				.HasDatabaseName("IX_Dispenser_DispenserCode");

			// Indexes for GasStation
			modelBuilder.Entity<GasStation>()
				.HasIndex(g => g.StationCode)
				.IsUnique()
				.HasDatabaseName("IX_GasStation_StationCode");

			modelBuilder.Entity<Organisations>()
				.HasIndex(g => g.OrganisationCode)
				.IsUnique()
				.HasDatabaseName("IX_Organisations_OrganisationCode");

			modelBuilder.Entity<Sms>()
				.HasIndex(g => g.PhoneNumber)
				.HasDatabaseName("Sms.IX_Sms_PhoneNumber");

			// Indexes for Nozzle
			modelBuilder.Entity<Nozzle>()
				.HasIndex(n => n.NozzleCode)
				.IsUnique()
				.HasDatabaseName("IX_Nozzle_NozzleCode");

			// Indexes for Price
			modelBuilder.Entity<Price>()
				.HasIndex(p => p.ProductCode)
				.HasDatabaseName("IX_Price_ProductId");

			// Indexes for PaymentType
			modelBuilder.Entity<PaymentType>()
				.HasIndex(p => p.PaymentTypeId)
				.IsUnique()
				.HasDatabaseName("IX_PaymentType_PaymentTypeId");

			// Indexes for Shift
			modelBuilder.Entity<Shift>()
				.HasIndex(s => s.ShiftNumber)
				.IsUnique()
				.HasDatabaseName("IX_Shift_ShiftNumber");

			// Indexes for Shift DispenserCode
			modelBuilder.Entity<Shift>()
				.HasIndex(s => s.DispenserCode)
				.HasDatabaseName("IX_Shift_DispenserCode");

			// Indexes for QuantityTransactions SaleId
			modelBuilder.Entity<QuantityTransactions>()
				.HasIndex(q => q.SaleId)
				.HasDatabaseName("IX_QuantityTransactions_SaleId");

			// Indexes for QuantityTransactions ShiftNumber
			modelBuilder.Entity<QuantityTransactions>()
				.HasIndex(q => q.ShiftNumber)
				.HasDatabaseName("IX_QuantityTransactions_ShiftNumber");

			// Indexes for QuantityTransactions DateCreated
			modelBuilder.Entity<QuantityTransactions>()
				.HasIndex(q => q.DateCreated)
				.HasDatabaseName("IX_QuantityTransactions_DateCreated");

			// Indexes for QuantityTransactions NozzleCode
			modelBuilder.Entity<QuantityTransactions>()
				.HasIndex(q => q.NozzleCode)
				.HasDatabaseName("IX_QuantityTransactions_NozzleCode");

			// Indexes for PaymentTransactions DateCreated
			modelBuilder.Entity<PaymentTransactions>()
				.HasIndex(p => p.DateCreated)
				.HasDatabaseName("IX_PaymentTransactions_DateCreated");

			// Indexes for PaymentTransactions PaymentRefrence
			modelBuilder.Entity<PaymentTransactions>()
				.HasIndex(p => p.PaymentRefrence)
				.HasDatabaseName("IX_PaymentTransactions_PaymentRefrence");

			// Indexes for PaymentTransactions SaleId
			modelBuilder.Entity<PaymentTransactions>()
				.HasIndex(p => p.SaleId)
				.HasDatabaseName("IX_PaymentTransactions_SaleId");

			// Indexes for Customer
			modelBuilder.Entity<Customer>()
				.HasIndex(c => c.CustomerCode)
				.IsUnique()
				.HasDatabaseName("IX_Customer_CustomerCode");

			// Indexes for Vehicle VehicleCode
			modelBuilder.Entity<Vehicle>()
				.HasIndex(v => v.VehicleCode)
				.IsUnique()
				.HasDatabaseName("IX_Vehicle_VehicleCode");

			// Indexes for Vehicle CustomerCode
			modelBuilder.Entity<Vehicle>()
				.HasIndex(v => v.CustomerCode)
				.HasDatabaseName("IX_Vehicle_CustomerCode");

			// Indexes for StockTake
			modelBuilder.Entity<StockTake>()
				.HasIndex(s => s.ShiftNumber)
				.HasDatabaseName("IX_StockTake_ShiftId");

			// Indexes for StockTakeSummary ShiftNumber
			modelBuilder.Entity<StockTakeSummary>()
				.HasIndex(s => s.ShiftNumber)
				.HasDatabaseName("IX_StockTakeSummary_ShiftNumber");

			// Indexes for StockTakeSummary NozzleCode
			modelBuilder.Entity<StockTakeSummary>()
				.HasIndex(s => s.NozzleCode)
				.HasDatabaseName("IX_StockTakeSummary_NozzleCode");

			// Indexes for Complains
			modelBuilder.Entity<Customer_Complains>()
				.HasIndex(c => c.CustomerCode)
				.IsUnique()
				.HasDatabaseName("IX_Complains_CustomerCode");

			// Indexes for Role
			modelBuilder.Entity<Role>()
				.HasIndex(r => r.RoleCode)
				.IsUnique()
				.HasDatabaseName("IX_Role_RoleCode");

			// Indexes for RoleAndPermisions RoleCode
			modelBuilder.Entity<RoleAndPermisions>()
				.HasIndex(rp => rp.RoleCode)
				.HasDatabaseName("IX_RoleAndPermisions_RoleId");

			// Indexes for RoleAndPermisions PermissionCode
			modelBuilder.Entity<RoleAndPermisions>()
				.HasIndex(rp => rp.PermissionCode)
				.HasDatabaseName("IX_RoleAndPermisions_PermissionCode");

			// Indexes for Otps
			modelBuilder.Entity<Otps>()
				.HasIndex(o => o.OTPCode)
				.HasDatabaseName("IX_Otps_OTPCode");

			// Indexes for OtpTypes
			modelBuilder.Entity<OtpTypes>()
				.HasIndex(o => o.OTPType)
				.IsUnique()
				.HasDatabaseName("IX_OtpTypes_OTPType");

			// Indexes for Sms
			modelBuilder.Entity<Sms>()
				.HasIndex(s => s.PhoneNumber)
				.HasDatabaseName("IX_Sms_PhoneNumber");

			// Indexes for Emails
			modelBuilder.Entity<Emails>()
				.HasIndex(e => e.ReportCode)
				.IsUnique()
				.HasDatabaseName("IX_Emails_ReportCode");

			// Indexes for Tills TillNumber
			modelBuilder.Entity<Tills>()
				.HasIndex(t => t.TillNumber)
				.IsUnique()
				.HasDatabaseName("IX_Tills_TillId");

			// Indexes for Tills StoreNumber
			modelBuilder.Entity<Tills>()
				.HasIndex(t => t.StoreNumber)
				.HasDatabaseName("IX_Tills_StoreNumber");

			// Indexes for Tank TankCode
			modelBuilder.Entity<Tank>()
				.HasIndex(t => t.TankCode)
				.IsUnique()
				.HasDatabaseName("IX_Tank_TankCode");

			// Indexes for PdaDevices DeviceIMEI
			modelBuilder.Entity<PdaDevices>()
				.HasIndex(p => p.DeviceIMEI)
				.HasDatabaseName("IX_PdaDevices_DeviceIMEI");

			// Indexes for PdaDevices DispenserCode
			modelBuilder.Entity<PdaDevices>()
				.HasIndex(p => p.DispenserCode)
				.HasDatabaseName("IX_PdaDevices_DispenserCode");

			// Indexes for MpesaTransaction
			modelBuilder.Entity<MpesaTransaction>()
				.HasIndex(m => m.TransID)
				.IsUnique()
				.HasDatabaseName("IX_MpesaTransaction_TransactionId");

			// Indexes for Products
			modelBuilder.Entity<Products>()
				.HasIndex(p => p.ProductCode)
				.IsUnique()
				.HasDatabaseName("IX_Products_ProductCode");

			// Indexes for CustomerTransactions VehicleCode
			modelBuilder.Entity<CustomerTransactions>()
				.HasIndex(ct => ct.VehicleCode)
				.HasDatabaseName("IX_CustomerTransactions_VehicleCode");

			// Indexes for CustomerTransactions DateCreated
			modelBuilder.Entity<CustomerTransactions>()
				.HasIndex(ct => ct.DateCreated)
				.HasDatabaseName("IX_CustomerTransactions_DateCreated");

			// Indexes for VehicleStatusTypes
			modelBuilder.Entity<VehicleStatusTypes>()
				.HasIndex(v => v.StatusCode)
				.IsUnique()
				.HasDatabaseName("IX_VehicleStatusTypes_StatusName");

			// Indexes for UserTrail
			modelBuilder.Entity<UserTrail>()
				.HasIndex(u => u.UserCode)
				.HasDatabaseName("IX_UserTrail_UserId");

			// Indexes for ProtoApps
			modelBuilder.Entity<ProtoApps>()
				.HasIndex(p => p.AppsCode)
				.IsUnique()
				.HasDatabaseName("IX_ProtoApps_AppsCode");

			// Indexes for UserApps
			modelBuilder.Entity<UserApps>()
				.HasIndex(ua => ua.AppsCode)
				.HasDatabaseName("IX_UserApps_UserId");

			// Indexes for DispenserAssignment
			modelBuilder.Entity<DispenserAssignment>()
				.HasIndex(d => d.DispenserCode)
				.HasDatabaseName("IX_DispenserAssignment_StationId");

			// Indexes for DispenserAssignment AttedantUserCode
			modelBuilder.Entity<DispenserAssignment>()
				.HasIndex(d => d.AttedantUserCode)
				.HasDatabaseName("IX_DispenserAssignment_AttedantUserCode");

			// Indexes for TankTransactions
			modelBuilder.Entity<TankTransactions>()
				.HasIndex(t => t.TankCode)
				.HasDatabaseName("IX_TankTransactions_TankCode");

			// Indexes for TankTransactionsSummary
			modelBuilder.Entity<TankTransactionsSummary>()
				.HasIndex(t => t.TankCode)
				.HasDatabaseName("IX_TankTransactionsSummary_TankId");

			// Indexes for MessageDetails
			modelBuilder.Entity<MessageDetails>()
				.HasIndex(m => m.MessageId)
				.HasDatabaseName("IX_MessageDetails_MessageId");

			// Indexes for AfricasTalkingCallback
			modelBuilder.Entity<AfricasTalkingCallback>()
				.HasIndex(a => a.MessageId)
				.HasDatabaseName("IX_AfricasTalkingCallback_MessageId");

			// Indexes for ApiPermisions
			modelBuilder.Entity<ApiPermisions>()
				.HasIndex(a => a.ApiPermission)
				.IsUnique()
				.HasDatabaseName("IX_ApiPermisions_ApiName");

			// Indexes for TransFeredVehicles
			modelBuilder.Entity<TransFeredVehicles>()
				.HasIndex(t => t.VehicleCode)
				.HasDatabaseName("IX_TransFeredVehicles_VehicleCode");

			// Indexes for TankSizes
			modelBuilder.Entity<TankSizes>()
				.HasIndex(t => t.Id)
				.IsUnique()
				.HasDatabaseName("IX_TankSizes_SizeName");

			// Indexes for MpesaC2bPayments
			modelBuilder.Entity<MpesaC2bPayments>()
				.HasIndex(m => m.TransID)
				.HasDatabaseName("IX_MpesaC2bPayments_TransactionId");

			//OrderTransaction
			modelBuilder.Entity<SalesTransactions>()
				.HasIndex(f => f.OrderId)
				.HasDatabaseName("IX_FailedTransactions_TransactionId");

		}

		private void ConfigureSeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Codegenerator>().HasData(
				new Codegenerator { Length = 5, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "UserCode", UserCode = "00001", Id = 1 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "D", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "DispenserCode", UserCode = "00001", Id = 2 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "N", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "Nozzlecode", UserCode = "00001", Id = 3 },
				new Codegenerator { Length = 3, NextNumber = 0, Prefix = "S", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "StationCode", UserCode = "00001", Id = 4 },
				new Codegenerator { Length = 5, NextNumber = 10000, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "CustomerCode", UserCode = "00001", Id = 5 },

				new Codegenerator { Length = 5, NextNumber = 10000, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "BULCUST", UserCode = "00001", Id = 6 },
				new Codegenerator { Length = 7, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "PLANID", UserCode = "00001", Id = 7 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "T", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "TANKID", UserCode = "00001", Id = 8 },
				new Codegenerator { Length = 5, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "TILLID", UserCode = "00001", Id = 9 },
				new Codegenerator { Length = 4, NextNumber = 0, Prefix = "P", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "PDAID", UserCode = "00001", Id = 10 },
				new Codegenerator { Length = 7, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "VEHICLEID", UserCode = "00001", Id = 11 },
				new Codegenerator { Length = 4, NextNumber = 0, Prefix = "PD", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "pdadevice", UserCode = "00001", Id = 14 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "productCode", UserCode = "00001", Id = 15 },
				new Codegenerator { Length = 5, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "WalkInCustomer", UserCode = "00001", Id = 16 },
				new Codegenerator { Length = 5, NextNumber = 1, Prefix = "", Suffix = "", Seed = 1, DateCreated = DateTime.UtcNow, TypeName = "VehicleCode", UserCode = "00001", Id = 17 }
				);

			modelBuilder.Entity<PaymentType>().HasData
			(
				new PaymentType { Id = 1, IsAppUsed = true, PaymentTypeId = 0, PaymentTypeName = "Mpesa", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 2, IsAppUsed = true, PaymentTypeId = 1, PaymentTypeName = "Wallet", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 4, IsAppUsed = false, PaymentTypeId = 3, PaymentTypeName = "Operational_Loss", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 6, IsAppUsed = false, PaymentTypeId = 5, PaymentTypeName = "Employee_Mpesa_Payments", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 7, IsAppUsed = false, PaymentTypeId = 6, PaymentTypeName = "Insurance", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 8, IsAppUsed = false, PaymentTypeId = 7, PaymentTypeName = "Voucher", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 9, IsAppUsed = false, PaymentTypeId = 8, PaymentTypeName = "Calibration", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 10, IsAppUsed = false, PaymentTypeId = 9, PaymentTypeName = "Compesation_Fuel", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 11, IsAppUsed = false, PaymentTypeId = 10, PaymentTypeName = "BatchVoucher", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 13, IsAppUsed = true, PaymentTypeId = 12, PaymentTypeName = "Cash", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 14, IsAppUsed = true, PaymentTypeId = 13, PaymentTypeName = "Credit", DateCreated = DateTime.UtcNow, UserCode = "00001" },
				new PaymentType { Id = 15, IsAppUsed = true, PaymentTypeId = 14, PaymentTypeName = "Loyalty", DateCreated = DateTime.UtcNow, UserCode = "00001" }
			);

			modelBuilder.Entity<ProtoApps>().HasData
				(
				new ProtoApps { Id = Guid.NewGuid(), AppsCode = "01", AppsName = "Bulk DashBoard", DateCreated = DateTime.UtcNow },
				new ProtoApps { Id = Guid.NewGuid(), AppsCode = "02", AppsName = "Bulk App", DateCreated = DateTime.UtcNow },
				new ProtoApps { Id = Guid.NewGuid(), AppsCode = "03", AppsName = "Fuel Flow DashBoard", DateCreated = DateTime.UtcNow },
				new ProtoApps { Id = Guid.NewGuid(), AppsCode = "04", AppsName = "Fuel Flow App", DateCreated = DateTime.UtcNow }
				);
			modelBuilder.Entity<ApplicationUser>().HasData(
				new ApplicationUser
				{
					Email = "nicholas@fuelflo.com",
					PhoneNumber = "+254715821303",
					PasswordHash = "AQAAAAIAAYagAAAAEE6B8ismqB4S3ovK4di5qY7F2cwEDfBiowzxCzmmnRa1w0kuyR/ADNBR4B6D0h9sew==",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					FirstName = "Admin",
					LastName = "Fuel Flow",
					NormalizedEmail = "NICHOLAS@FUELFLOW.COM",
					IsActive = true,
					UserCode = "99999",
					Id = "f9b3e4d7-5a8c-3f2d-9b6f-4a7e5d8b6f9a",
					PasswordLastUpdated = DateTime.UtcNow,
					DateModified = DateTime.UtcNow,
					DateCreated = DateTime.UtcNow,
					AccessFailedCount = 0,
					LastLoginDate = DateTime.UtcNow,

				});


			modelBuilder.Entity<GasStation>().HasData
				(
				new GasStation
				{
					Id = 1,
					DateCreated = DateTime.UtcNow,
					LocationId = "Test Station",
					IsActive = true,
					StationAddress = "Test Station",
					StationCode = "S001",
					StationName = "TEST STATION",
					UserCode = "00001"

				}
);

			modelBuilder.Entity<UserApps>().HasData
				(
				  new UserApps
				  {
					  AppsCode = "03",
					  DateCreated = DateTime.UtcNow,
					  Id = Guid.NewGuid(),
					  UserCode = "99999",
				  },
				   new UserApps
				   {
					   AppsCode = "04",
					   DateCreated = DateTime.UtcNow,
					   Id = Guid.NewGuid(),
					   UserCode = "99999",
				   }, new UserApps
				   {
					   AppsCode = "01",
					   DateCreated = DateTime.UtcNow,
					   Id = Guid.NewGuid(),
					   UserCode = "99999",
				   }, new UserApps
				   {
					   AppsCode = "02",
					   DateCreated = DateTime.UtcNow,
					   Id = Guid.NewGuid(),
					   UserCode = "99999",
				   }
				);



			modelBuilder.Entity<Dispenser>().HasData
			(
			new Dispenser
			{
				Id = 1,
				DateCreated = DateTime.UtcNow,
				IsActive = true,
				StationCode = "S001",
				UserCode = "00001",
				DispenserCode = "D01",
				DispenserName = "D1",
				StorageLocation = "kenya",
				TillNumber = "078678",
				PetroleumCode = "01"
			});

			modelBuilder.Entity<Products>().HasData
			(
			new Products
			{
				ProductCode = "02",
				ProductName = "Diesel",
				DateCreated = DateTime.UtcNow,
				IsActive = true,
				UserCode = "000001",
				Id = -1
			});

			modelBuilder.Entity<PdaDevices>().HasData
(
new PdaDevices
{
	Id = 1,
	DateCreated = DateTime.UtcNow,
	IsActive = true,
	UserCode = "99999",
	DispenserCode = "D01",
	DeviceIMEI = "1234567890",
	DeviceCode = "1234567890",
	DeviceMacAddress = "1234567890",
	DeviceModel = "1234567890",
	DeviceName = "Test PDA",
	DeviceSerialNumber = "1234567890"

});

			modelBuilder.Entity<Nozzle>().HasData
			(
			new Nozzle
			{
				Id = 1,
				DateCreated = DateTime.UtcNow,
				IsActive = true,
				NozzleCode = "N01",
				UserCode = "00001",
				DispenserCode = "D01",
				NozzleName = "N01",
			}, new Nozzle
			{
				Id = 2,
				DateCreated = DateTime.UtcNow,
				IsActive = true,
				NozzleCode = "N02",
				UserCode = "00001",
				DispenserCode = "D01",
				NozzleName = "N02",
			}
			);
			modelBuilder.Entity<DispenserAssignment>().HasData
	(
	new DispenserAssignment
	{
		Id = 1,
		StationCode = "S001",
		AssignedBy = "99999",
		AttedantUserCode = "99999",
		DateAssigned = DateTime.UtcNow,
		DispenserCode = "D01",
		IsActive = true,
	});

			modelBuilder.Entity<Tills>().HasData
	(
	new Tills
	{
		Id = 1,
		StoreNumber = "078678",
		DateCreated = DateTime.UtcNow,
		LastFetch = DateTime.UtcNow,
		TillName = "Test Till",
		IsActive = true,
		OffsetValue = 0,
		TillNumber = "078678",
		UserCode = "99999",
	});


			modelBuilder.Entity<QuantityTransactions>().HasData
				(
				   new QuantityTransactions
				   {
					   Id = 1,
					   NozzleCode = "N01",
					   AmountCredit = 0,
					   AmountDebit = 0,
					   ShiftNumber = "",
					   DateCreated = DateTime.UtcNow,
					   Discount = 0,
					   DispenserCode = "D01",
					   IsReversed = false,
					   OtpUsed = "",
					   PaymentTypeCode = 99,
					   Price = 0,
					   QuantityCredit = 50,
					   RoundedDate = DateTime.UtcNow,
					   QuantityDebit = 0,
					   SaleId = "",
					   StationCode = "S001",
					   UserCode = "99999",
					   Vat_Amount = 0,
					   VehicleCode = ""
				   },
					new QuantityTransactions
					{
						Id = 2,
						NozzleCode = "N02",
						AmountCredit = 0,
						AmountDebit = 0,
						ShiftNumber = "",
						DateCreated = DateTime.UtcNow,
						Discount = 0,
						DispenserCode = "D01",
						IsReversed = false,
						OtpUsed = "",
						PaymentTypeCode = 99,
						Price = 0,
						QuantityCredit = 50,
						RoundedDate = DateTime.UtcNow,
						QuantityDebit = 0,
						SaleId = "",
						StationCode = "S001",
						UserCode = "99999",
						Vat_Amount = 0,
						VehicleCode = ""
					}
				);
			modelBuilder.Entity<StockTake>().HasData
				(
					 new StockTake
					 {
						 ClosingReading = 0,
						 DateCreated = DateTime.UtcNow,
						 NozzleCode = "N01",
						 ShiftNumber = "",
						 OpeningReading = 50,
						 TakeType = 99,
						 UserCode = "99999",
						 Id = 1
					 }, new StockTake
					 {
						 ClosingReading = 0,
						 DateCreated = DateTime.UtcNow,
						 NozzleCode = "N02",
						 ShiftNumber = "",
						 OpeningReading = 50,
						 TakeType = 99,
						 UserCode = "99999",
						 Id = -1
					 }
				);

			modelBuilder.Entity<PetroleumProducts>().HasData
			(
		    new PetroleumProducts {Id=  1, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "01", PetroleumName = "Autogas" },
			new PetroleumProducts {Id = 2, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "02", PetroleumName = "Petrol" },
			new PetroleumProducts {Id = 3, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "03", PetroleumName = "Diesel" }
			);

			modelBuilder.Entity<Role>().HasData
			(
				  new Role { Id = 1, DateCreated = DateTime.UtcNow, RoleCode = "001", RoleName = "Administrator",UserCode = "99999" }
			);

			var reports = new[]
			{
				new { Id = "001", ReportName = "Variance Report" },
				new { Id = "002", ReportName = "Sales Report" },
				new { Id = "003", ReportName = "Monthly Sales Report" },
				new { Id = "004", ReportName = "Cumulative Variance Report" },
				new { Id = "005", ReportName = "Mpesa Usage Report" }
			};

			modelBuilder.Entity<Reports>().HasData(
				reports.Select(r => new Reports
				{
					Id = r.Id,
					ReportName = r.ReportName
				})
			);

			modelBuilder.Entity<Emails>().HasData(
				reports.Select((r, index) => new Emails
				{
					Id = index + 1,
					DateCreated = new DateTime(2024, 1, 1),
					ReportCode = r.Id,
					NotificationName = r.ReportName,
					UserCode = "99999",
					ToCC = "",
					From = "",
					To = ""
				})
			);
		}



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
