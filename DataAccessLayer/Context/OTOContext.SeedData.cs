using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.EntityModels.Emails;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.ProtoBase;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.EntityModels.StockTake;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
	public partial class OTOContext
	{
		private void ConfigureSeedData(ModelBuilder modelBuilder)
		{
			SeedCodegenerators(modelBuilder);
			SeedPaymentTypes(modelBuilder);
			SeedProtoApps(modelBuilder);
			SeedAdminUser(modelBuilder);
			SeedStation(modelBuilder);
			SeedUserApps(modelBuilder);
			SeedDispenser(modelBuilder);
			SeedProducts(modelBuilder);
			SeedPdaDevices(modelBuilder);
			SeedNozzles(modelBuilder);
			SeedDispenserAssignment(modelBuilder);
			SeedTills(modelBuilder);
			SeedQuantityTransactions(modelBuilder);
			SeedStockTakes(modelBuilder);
			SeedPetroleumProducts(modelBuilder);
			SeedRoles(modelBuilder);
			SeedReportsAndEmails(modelBuilder);
		}

		private static void SeedCodegenerators(ModelBuilder modelBuilder)
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
		}

		private static void SeedPaymentTypes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PaymentType>().HasData(
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
		}

		private static void SeedProtoApps(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProtoApps>().HasData(
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000001"), AppsCode = "01", AppsName = "Bulk DashBoard", DateCreated = new DateTime(2024, 1, 1) },
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000002"), AppsCode = "02", AppsName = "Bulk App", DateCreated = new DateTime(2024, 1, 1) },
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000003"), AppsCode = "03", AppsName = "Fuel Flow DashBoard", DateCreated = new DateTime(2024, 1, 1) },
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000004"), AppsCode = "04", AppsName = "Fuel Flow App", DateCreated = new DateTime(2024, 1, 1) }
			);
		}

		private static void SeedAdminUser(ModelBuilder modelBuilder)
		{
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
				}
			);
		}

		private static void SeedStation(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GasStation>().HasData(
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
		}

		private static void SeedUserApps(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserApps>().HasData(
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000001"), AppsCode = "03", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999" },
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000002"), AppsCode = "04", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999" },
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000003"), AppsCode = "01", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999" },
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000004"), AppsCode = "02", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999" }
			);
		}

		private static void SeedDispenser(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Dispenser>().HasData(
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
				}
			);
		}

		private static void SeedProducts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Products>().HasData(
				new Products
				{
					ProductCode = "02",
					ProductName = "Diesel",
					DateCreated = DateTime.UtcNow,
					IsActive = true,
					UserCode = "000001",
					Id = -1
				}
			);
		}

		private static void SeedPdaDevices(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PdaDevices>().HasData(
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
				}
			);
		}

		private static void SeedNozzles(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Nozzle>().HasData(
				new Nozzle { Id = 1, DateCreated = DateTime.UtcNow, IsActive = true, NozzleCode = "N01", UserCode = "00001", DispenserCode = "D01", NozzleName = "N01" },
				new Nozzle { Id = 2, DateCreated = DateTime.UtcNow, IsActive = true, NozzleCode = "N02", UserCode = "00001", DispenserCode = "D01", NozzleName = "N02" }
			);
		}

		private static void SeedDispenserAssignment(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DispenserAssignment>().HasData(
				new DispenserAssignment
				{
					Id = 1,
					StationCode = "S001",
					AssignedBy = "99999",
					AttedantUserCode = "99999",
					DateAssigned = DateTime.UtcNow,
					DispenserCode = "D01",
					IsActive = true,
				}
			);
		}

		private static void SeedTills(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tills>().HasData(
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
				}
			);
		}

		private static void SeedQuantityTransactions(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<QuantityTransactions>().HasData(
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
		}

		private static void SeedStockTakes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StockTake>().HasData(
				new StockTake { ClosingReading = 0, DateCreated = DateTime.UtcNow, NozzleCode = "N01", ShiftNumber = "", OpeningReading = 50, TakeType = 99, UserCode = "99999", Id = 1 },
				new StockTake { ClosingReading = 0, DateCreated = DateTime.UtcNow, NozzleCode = "N02", ShiftNumber = "", OpeningReading = 50, TakeType = 99, UserCode = "99999", Id = -1 }
			);
		}

		private static void SeedPetroleumProducts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PetroleumProducts>().HasData(
				new PetroleumProducts { Id = 1, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "01", PetroleumName = "Autogas" },
				new PetroleumProducts { Id = 2, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "02", PetroleumName = "Petrol" },
				new PetroleumProducts { Id = 3, DateCreated = DateTime.UtcNow, UserCode = "99999", PetroleumCode = "03", PetroleumName = "Diesel" }
			);
		}

		private static void SeedRoles(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, DateCreated = DateTime.UtcNow, RoleCode = "001", RoleName = "Administrator", UserCode = "99999" }
			);
		}

		private static void SeedReportsAndEmails(ModelBuilder modelBuilder)
		{
			var reports = new[]
			{
				new { Id = "001", ReportName = "Variance Report"           },
				new { Id = "002", ReportName = "Sales Report"              },
				new { Id = "003", ReportName = "Monthly Sales Report"      },
				new { Id = "004", ReportName = "Cumulative Variance Report" },
				new { Id = "005", ReportName = "Mpesa Usage Report"        }
			};

			modelBuilder.Entity<Reports>().HasData(
				reports.Select(r => new Reports { Id = r.Id, ReportName = r.ReportName })
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
	}
}