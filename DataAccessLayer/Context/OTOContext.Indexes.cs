using BusinessLogic.CustomerService;
using BusinessLogic.Sales.Archirve;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.DTOs.Payments;
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
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public partial class OTOContext
    {
        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // --- Keyless views ---
            modelBuilder.Entity<CustomerBalanceDto>()
                .HasNoKey()
                .ToView("CustomerBalanceDto");

            modelBuilder.Entity<MpesaTransactionDto>()
                .HasNoKey()
                .ToView("MpesaTransactionDto");

            modelBuilder.Entity<MpesaTransactionsDto>()
                .HasNoKey()
                .ToView("MpesaTransactionsDto");

            modelBuilder.Entity<Vw_SalesData>()
                .HasNoKey()
                .ToView("vw_SalesData");

            // --- QuantityTransactions column config ---
            modelBuilder.Entity<QuantityTransactions>()
                .Property(e => e.VehicleCode)
                .IsRequired().HasMaxLength(10).IsUnicode(false);

            modelBuilder.Entity<QuantityTransactions>()
                .Property(e => e.ShiftNumber)
                .IsRequired().HasMaxLength(25).IsUnicode(false);

            modelBuilder.Entity<QuantityTransactions>()
                .Property(e => e.NozzleCode)
                .IsRequired().HasMaxLength(25).IsUnicode(false);

            // --- Codegenerator ---
            modelBuilder.Entity<Codegenerator>()
                .HasIndex(c => c.TypeName).IsUnique()
                .HasDatabaseName("IX_Codegenerator_TypeName");

            // --- FailedTransactions ---
            modelBuilder.Entity<FailedTransactions>()
                .HasIndex(c => c.RegNo)
                .HasDatabaseName("IX_Codegenerator_RegNo");

            // --- Dispenser ---
            modelBuilder.Entity<Dispenser>()
                .HasIndex(d => d.DispenserCode)
                .HasDatabaseName("IX_Dispenser_DispenserCode");

            // --- GasStation ---
            modelBuilder.Entity<GasStation>()
                .HasIndex(g => g.StationCode).IsUnique()
                .HasDatabaseName("IX_GasStation_StationCode");

            // --- Organisations ---
            modelBuilder.Entity<Organisations>()
                .HasIndex(g => g.OrganisationCode).IsUnique()
                .HasDatabaseName("IX_Organisations_OrganisationCode");

            // --- Nozzle ---
            modelBuilder.Entity<Nozzle>()
                .HasIndex(n => n.NozzleCode).IsUnique()
                .HasDatabaseName("IX_Nozzle_NozzleCode");

            // --- Price ---
            modelBuilder.Entity<Price>()
                .HasIndex(p => p.ProductCode)
                .HasDatabaseName("IX_Price_ProductId");

            // --- PaymentType ---
            modelBuilder.Entity<PaymentType>()
                .HasIndex(p => p.PaymentTypeId).IsUnique()
                .HasDatabaseName("IX_PaymentType_PaymentTypeId");

            // --- Shift ---
            modelBuilder.Entity<Shift>()
                .HasIndex(s => s.ShiftNumber).IsUnique()
                .HasDatabaseName("IX_Shift_ShiftNumber");

            modelBuilder.Entity<Shift>()
                .HasIndex(s => s.DispenserCode)
                .HasDatabaseName("IX_Shift_DispenserCode");

            // --- QuantityTransactions indexes ---
            modelBuilder.Entity<QuantityTransactions>()
                .HasIndex(q => q.SaleId)
                .HasDatabaseName("IX_QuantityTransactions_SaleId");

            modelBuilder.Entity<QuantityTransactions>()
                .HasIndex(q => q.ShiftNumber)
                .HasDatabaseName("IX_QuantityTransactions_ShiftNumber");

            modelBuilder.Entity<QuantityTransactions>()
                .HasIndex(q => q.DateCreated)
                .HasDatabaseName("IX_QuantityTransactions_DateCreated");

            modelBuilder.Entity<QuantityTransactions>()
                .HasIndex(q => q.NozzleCode)
                .HasDatabaseName("IX_QuantityTransactions_NozzleCode");

            // --- PaymentTransactions ---
            modelBuilder.Entity<PaymentTransactions>()
                .HasIndex(p => p.DateCreated)
                .HasDatabaseName("IX_PaymentTransactions_DateCreated");

            modelBuilder.Entity<PaymentTransactions>()
                .HasIndex(p => p.PaymentRefrence)
                .HasDatabaseName("IX_PaymentTransactions_PaymentRefrence");

            modelBuilder.Entity<PaymentTransactions>()
                .HasIndex(p => p.SaleId)
                .HasDatabaseName("IX_PaymentTransactions_SaleId");

            // --- Customer ---
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CustomerCode).IsUnique()
                .HasDatabaseName("IX_Customer_CustomerCode");

            // --- Vehicle ---
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.VehicleCode).IsUnique()
                .HasDatabaseName("IX_Vehicle_VehicleCode");

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.CustomerCode)
                .HasDatabaseName("IX_Vehicle_CustomerCode");

            // --- StockTake ---
            modelBuilder.Entity<StockTake>()
                .HasIndex(s => s.ShiftNumber)
                .HasDatabaseName("IX_StockTake_ShiftId");

            modelBuilder.Entity<StockTakeSummary>()
                .HasIndex(s => s.ShiftNumber)
                .HasDatabaseName("IX_StockTakeSummary_ShiftNumber");

            modelBuilder.Entity<StockTakeSummary>()
                .HasIndex(s => s.NozzleCode)
                .HasDatabaseName("IX_StockTakeSummary_NozzleCode");

            // --- Customer_Complains ---
            modelBuilder.Entity<Customer_Complains>()
                .HasIndex(c => c.CustomerCode).IsUnique()
                .HasDatabaseName("IX_Complains_CustomerCode");

            // --- Role ---
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleCode).IsUnique()
                .HasDatabaseName("IX_Role_RoleCode");

            // --- RoleAndPermisions ---
            modelBuilder.Entity<RoleAndPermisions>()
                .HasIndex(rp => rp.RoleCode)
                .HasDatabaseName("IX_RoleAndPermisions_RoleId");

            modelBuilder.Entity<RoleAndPermisions>()
                .HasIndex(rp => rp.PermissionCode)
                .HasDatabaseName("IX_RoleAndPermisions_PermissionCode");

            // --- Otps ---
            modelBuilder.Entity<Otps>()
                .HasIndex(o => o.OTPCode)
                .HasDatabaseName("IX_Otps_OTPCode");

            modelBuilder.Entity<OtpTypes>()
                .HasIndex(o => o.OTPType).IsUnique()
                .HasDatabaseName("IX_OtpTypes_OTPType");

            // --- Sms ---
            modelBuilder.Entity<Sms>()
                .HasIndex(s => s.PhoneNumber)
                .HasDatabaseName("IX_Sms_PhoneNumber");

            // --- Emails ---
            modelBuilder.Entity<Emails>()
                .HasIndex(e => e.ReportCode).IsUnique()
                .HasDatabaseName("IX_Emails_ReportCode");

            // --- Tills ---
            modelBuilder.Entity<Tills>()
                .HasIndex(t => t.TillNumber).IsUnique()
                .HasDatabaseName("IX_Tills_TillId");

            modelBuilder.Entity<Tills>()
                .HasIndex(t => t.StoreNumber)
                .HasDatabaseName("IX_Tills_StoreNumber");

            // --- Tank ---
            modelBuilder.Entity<Tank>()
                .HasIndex(t => t.TankCode).IsUnique()
                .HasDatabaseName("IX_Tank_TankCode");

            // --- PdaDevices ---
            modelBuilder.Entity<PdaDevices>()
                .HasIndex(p => p.DeviceIMEI)
                .HasDatabaseName("IX_PdaDevices_DeviceIMEI");

            modelBuilder.Entity<PdaDevices>()
                .HasIndex(p => p.DispenserCode)
                .HasDatabaseName("IX_PdaDevices_DispenserCode");

            // --- MpesaTransaction ---
            modelBuilder.Entity<MpesaTransaction>()
                .HasIndex(m => m.TransID).IsUnique()
                .HasDatabaseName("IX_MpesaTransaction_TransactionId");

            // --- Products ---
            modelBuilder.Entity<Products>()
                .HasIndex(p => p.ProductCode).IsUnique()
                .HasDatabaseName("IX_Products_ProductCode");

            // --- CustomerTransactions ---
            modelBuilder.Entity<CustomerTransactions>()
                .HasIndex(ct => ct.VehicleCode)
                .HasDatabaseName("IX_CustomerTransactions_VehicleCode");

            modelBuilder.Entity<CustomerTransactions>()
                .HasIndex(ct => ct.DateCreated)
                .HasDatabaseName("IX_CustomerTransactions_DateCreated");

            // --- VehicleStatusTypes ---
            modelBuilder.Entity<VehicleStatusTypes>()
                .HasIndex(v => v.StatusCode).IsUnique()
                .HasDatabaseName("IX_VehicleStatusTypes_StatusName");

            // --- UserTrail ---
            modelBuilder.Entity<UserTrail>()
                .HasIndex(u => u.UserCode)
                .HasDatabaseName("IX_UserTrail_UserId");

            // --- ProtoApps ---
            modelBuilder.Entity<ProtoApps>()
                .HasIndex(p => p.AppsCode).IsUnique()
                .HasDatabaseName("IX_ProtoApps_AppsCode2");

            // --- UserApps ---
            modelBuilder.Entity<UserApps>()
                .HasIndex(ua => ua.AppsCode)
                .HasDatabaseName("IX_UserApps_UserId");

            // --- DispenserAssignment ---
            modelBuilder.Entity<DispenserAssignment>()
                .HasIndex(d => d.DispenserCode)
                .HasDatabaseName("IX_DispenserAssignment_StationId");

            modelBuilder.Entity<DispenserAssignment>()
                .HasIndex(d => d.AttedantUserCode)
                .HasDatabaseName("IX_DispenserAssignment_AttedantUserCode");

            // --- TankTransactions ---
            modelBuilder.Entity<TankTransactions>()
                .HasIndex(t => t.TankCode)
                .HasDatabaseName("IX_TankTransactions_TankCode");

            modelBuilder.Entity<TankTransactionsSummary>()
                .HasIndex(t => t.TankCode)
                .HasDatabaseName("IX_TankTransactionsSummary_TankId");

            // --- MessageDetails ---
            modelBuilder.Entity<MessageDetails>()
                .HasIndex(m => m.MessageId)
                .HasDatabaseName("IX_MessageDetails_MessageId");

            // --- AfricasTalkingCallback ---
            modelBuilder.Entity<AfricasTalkingCallback>()
                .HasIndex(a => a.MessageId)
                .HasDatabaseName("IX_AfricasTalkingCallback_MessageId");

            // --- ApiPermisions ---
            modelBuilder.Entity<ApiPermisions>()
                .HasIndex(a => a.ApiPermission).IsUnique()
                .HasDatabaseName("IX_ApiPermisions_ApiName");

            // --- TransFeredVehicles ---
            modelBuilder.Entity<TransFeredVehicles>()
                .HasIndex(t => t.VehicleCode)
                .HasDatabaseName("IX_TransFeredVehicles_VehicleCode");

            // --- TankSizes ---
            modelBuilder.Entity<TankSizes>()
                .HasIndex(t => t.Id).IsUnique()
                .HasDatabaseName("IX_TankSizes_SizeName");

            // --- MpesaC2bPayments ---
            modelBuilder.Entity<MpesaC2bPayments>()
                .HasIndex(m => m.TransID)
                .HasDatabaseName("IX_MpesaC2bPayments_TransactionId");

            // --- SalesTransactions ---
            modelBuilder.Entity<SalesTransactions>()
                .HasIndex(f => f.OrderId)
                .HasDatabaseName("IX_FailedTransactions_TransactionId");
        }
    }
}
