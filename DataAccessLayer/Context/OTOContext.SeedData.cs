using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Emails;
using DataAccessLayer.EntityModels.Grleamify;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
	public partial class OTOContext
	{
		private static void ConfigureSeedData(ModelBuilder modelBuilder)
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
			SeedSetting(modelBuilder);
			SeedCustomers(modelBuilder);
			SeedVehicles(modelBuilder);
			SeedPrices(modelBuilder);
			SeedVehicleTypes(modelBuilder);
			SeedCarWashProductPrices(modelBuilder);
			SeedCarWashProducts(modelBuilder);
		}
		private static readonly DateTime SeedDate = new DateTime(2026, 7, 13, 0, 0, 0, DateTimeKind.Utc);
		private const string SeedUserCode = "99999";

		private static void SeedVehicleTypes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<VehicleType>().HasData(
				new VehicleType { Id = 1, Name = "Saloon", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode},
				new VehicleType { Id = 2, Name = "SUV", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new VehicleType { Id = 3, Name = "Truck", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new VehicleType { Id = 4, Name = "Trailer", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new VehicleType { Id = 5, Name = "Motorcycle", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new VehicleType { Id = 6, Name = "Tuk Tuk", IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode }
			);
		}

		// ── Flat/base products. Price here = Saloon-tier price, used only as a
		// fallback if a vehicle type has no row in CarWashProductPrice below. ──
		private static void SeedCarWashProducts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CarWashProduct>().HasData(
				new CarWashProduct { Id = 1, Name = "Base Wash", Price = 300, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 2, Name = "Top Wash", Price = 200, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 3, Name = "Engine Wash", Price = 400, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 4, Name = "Under Wash", Price = 400, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 5, Name = "Vacuum", Price = 400, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 6, Name = "Wax Machine", Price = 1000, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 7, Name = "Waxing", Price = 400, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 8, Name = "Rim Wash", Price = 1000, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProduct { Id = 9, Name = "Buffing", Price = 500, IsActive = true, DateCreated = SeedDate, UserCode = SeedUserCode }
			);
		}

		// ── Per-vehicle-type pricing overrides. ALL VALUES ARE PLACEHOLDERS
		// except Base Wash for Saloon (300) — everything else was scaled up/down
		// by vehicle size for structure only. Replace before going live.
		//
		// VehicleType IDs: 1=Saloon, 2=SUV, 3=Truck, 4=Trailer, 5=Motorcycle, 6=Tuk Tuk
		// Product IDs:     1=Base Wash, 2=Top Wash, 3=Engine Wash, 4=Under Wash,
		//                  5=Vacuum, 6=Wax Machine, 7=Waxing, 8=Rim Wash, 9=Buffing
		private static void SeedCarWashProductPrices(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CarWashProductPrice>().HasData(
				// ── Saloon (VehicleTypeId = 1) ─────────────────────────────
				new CarWashProductPrice { Id = 1, ProductId = 1, VehicleTypeId = 1, Price = 300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 2, ProductId = 2, VehicleTypeId = 1, Price = 200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 3, ProductId = 3, VehicleTypeId = 1, Price = 400, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 4, ProductId = 4, VehicleTypeId = 1, Price = 400, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 5, ProductId = 5, VehicleTypeId = 1, Price = 400, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 6, ProductId = 6, VehicleTypeId = 1, Price = 1000, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 7, ProductId = 7, VehicleTypeId = 1, Price = 400, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 8, ProductId = 8, VehicleTypeId = 1, Price = 1000, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 9, ProductId = 9, VehicleTypeId = 1, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },

				// ── SUV (VehicleTypeId = 2) ─────────────────────────────────
				new CarWashProductPrice { Id = 10, ProductId = 1, VehicleTypeId = 2, Price = 400, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 11, ProductId = 2, VehicleTypeId = 2, Price = 250, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 12, ProductId = 3, VehicleTypeId = 2, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 13, ProductId = 4, VehicleTypeId = 2, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 14, ProductId = 5, VehicleTypeId = 2, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 15, ProductId = 6, VehicleTypeId = 2, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 16, ProductId = 7, VehicleTypeId = 2, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 17, ProductId = 8, VehicleTypeId = 2, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 18, ProductId = 9, VehicleTypeId = 2, Price = 650, DateCreated = SeedDate, UserCode = SeedUserCode },

				// ── Truck (VehicleTypeId = 3) ───────────────────────────────
				new CarWashProductPrice { Id = 19, ProductId = 1, VehicleTypeId = 3, Price = 1000, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 20, ProductId = 2, VehicleTypeId = 3, Price = 700, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 21, ProductId = 3, VehicleTypeId = 3, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 22, ProductId = 4, VehicleTypeId = 3, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 23, ProductId = 5, VehicleTypeId = 3, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 24, ProductId = 6, VehicleTypeId = 3, Price = 3300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 25, ProductId = 7, VehicleTypeId = 3, Price = 1300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 26, ProductId = 8, VehicleTypeId = 3, Price = 3300, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 27, ProductId = 9, VehicleTypeId = 3, Price = 1700, DateCreated = SeedDate, UserCode = SeedUserCode },

				// ── Trailer (VehicleTypeId = 4) ─────────────────────────────
				new CarWashProductPrice { Id = 28, ProductId = 1, VehicleTypeId = 4, Price = 1200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 29, ProductId = 2, VehicleTypeId = 4, Price = 800, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 30, ProductId = 3, VehicleTypeId = 4, Price = 1600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 31, ProductId = 4, VehicleTypeId = 4, Price = 1600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 32, ProductId = 5, VehicleTypeId = 4, Price = 1600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 33, ProductId = 6, VehicleTypeId = 4, Price = 4000, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 34, ProductId = 7, VehicleTypeId = 4, Price = 1600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 35, ProductId = 8, VehicleTypeId = 4, Price = 4000, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 36, ProductId = 9, VehicleTypeId = 4, Price = 2000, DateCreated = SeedDate, UserCode = SeedUserCode },

				// ── Motorcycle (VehicleTypeId = 5) ──────────────────────────
				new CarWashProductPrice { Id = 37, ProductId = 1, VehicleTypeId = 5, Price = 150, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 38, ProductId = 2, VehicleTypeId = 5, Price = 100, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 39, ProductId = 3, VehicleTypeId = 5, Price = 200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 40, ProductId = 4, VehicleTypeId = 5, Price = 200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 41, ProductId = 5, VehicleTypeId = 5, Price = 200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 42, ProductId = 6, VehicleTypeId = 5, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 43, ProductId = 7, VehicleTypeId = 5, Price = 200, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 44, ProductId = 8, VehicleTypeId = 5, Price = 500, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 45, ProductId = 9, VehicleTypeId = 5, Price = 250, DateCreated = SeedDate, UserCode = SeedUserCode },

				// ── Tuk Tuk (VehicleTypeId = 6) ─────────────────────────────
				new CarWashProductPrice { Id = 46, ProductId = 1, VehicleTypeId = 6, Price = 180, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 47, ProductId = 2, VehicleTypeId = 6, Price = 120, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 48, ProductId = 3, VehicleTypeId = 6, Price = 240, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 49, ProductId = 4, VehicleTypeId = 6, Price = 240, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 50, ProductId = 5, VehicleTypeId = 6, Price = 240, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 51, ProductId = 6, VehicleTypeId = 6, Price = 600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 52, ProductId = 7, VehicleTypeId = 6, Price = 240, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 53, ProductId = 8, VehicleTypeId = 6, Price = 600, DateCreated = SeedDate, UserCode = SeedUserCode },
				new CarWashProductPrice { Id = 54, ProductId = 9, VehicleTypeId = 6, Price = 300, DateCreated = SeedDate, UserCode = SeedUserCode }
			);
		}
		private static void SeedVehicles(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vehicle>().HasData(
				new Vehicle { Id = 1, CustomerCode = "C00001", VehicleCode = "V001", VehicleRegistrationNumber = "KDL849R", VehicleMake = "Toyota", VehicleModel = "Walk-In", TankCapacity = 60, ProductCode = "01", ConversionStation = "", ConversionDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, Status = "Active", NFC_CardNumber = "0000000000", TransactionPIN = "0000", PhoneNumber = "0715821303", PhoneNumber2 = string.Empty, CreditLimit = 1000m, Discount = 0m, TelematicSerialNumber = string.Empty, IsTelematicInstalled = false, TelematicInstallationDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), RoyaltyPointPerLitre = 1m }
			);
		}

		private static void SeedCustomers(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>().HasData(
				new Customer { Id = 1, CustomerName = "System Test Vehicle", CustomerPhone = "0715821303", CustomerEmail = "test@fuelflow.com", OrganisationCode = "ORG001", CustomerCode = "C00001", IdentificationNumber = "27838753", KRAPin = "", CreditLimit = 0m, Receive_Receipts = false, Receive_Statements = false, IsCreditCustomer = true, BaseLoyaltyPoints = 1 }
			);
		}

		private static void SeedCodegenerators(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Codegenerator>().HasData(
				new Codegenerator { Length = 5, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "UserCode", UserCode = "00001", Id = 1 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "D", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "DispenserCode", UserCode = "00001", Id = 2 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "N", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "NozzleCode", UserCode = "00001", Id = 3 },
				new Codegenerator { Length = 3, NextNumber = 0, Prefix = "S", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "StationCode", UserCode = "00001", Id = 4 },
				new Codegenerator { Length = 5, NextNumber = 10000, Prefix = "", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "CustomerCode", UserCode = "00001", Id = 5 },
				new Codegenerator { Length = 4, NextNumber = 0, Prefix = "PD", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "pdadevice", UserCode = "00001", Id = 14 },
				new Codegenerator { Length = 2, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "productCode", UserCode = "00001", Id = 15 },
				new Codegenerator { Length = 5, NextNumber = 0, Prefix = "", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "WalkInCustomer", UserCode = "00001", Id = 16 },
				new Codegenerator { Length = 5, NextNumber = 1, Prefix = "", Suffix = "", Seed = 1, DateCreated = EatTime.Now, TypeName = "VehicleCode", UserCode = "00001", Id = 17 }

			);
		}

		private static void SeedPaymentTypes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PaymentType>().HasData(
				new PaymentType { Id = 1, IsAppUsed = true, PaymentTypeId = 0, PaymentTypeName = "Mpesa", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 2, IsAppUsed = false, PaymentTypeId = 1, PaymentTypeName = "Wallet", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 4, IsAppUsed = false, PaymentTypeId = 3, PaymentTypeName = "Operational_Loss", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 6, IsAppUsed = false, PaymentTypeId = 5, PaymentTypeName = "Employee_Mpesa_Payments", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 7, IsAppUsed = false, PaymentTypeId = 6, PaymentTypeName = "Insurance", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 8, IsAppUsed = false, PaymentTypeId = 7, PaymentTypeName = "Voucher", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 9, IsAppUsed = false, PaymentTypeId = 8, PaymentTypeName = "Calibration", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 10, IsAppUsed = false, PaymentTypeId = 9, PaymentTypeName = "Compesation_Fuel", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 11, IsAppUsed = false, PaymentTypeId = 10, PaymentTypeName = "BatchVoucher", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 13, IsAppUsed = true, PaymentTypeId = 12, PaymentTypeName = "Cash", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 14, IsAppUsed = true, PaymentTypeId = 13, PaymentTypeName = "Credit", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 15, IsAppUsed = true, PaymentTypeId = 14, PaymentTypeName = "Loyalty", DateCreated = EatTime.Now, UserCode = "00001" },
				new PaymentType { Id = 16, IsAppUsed = true, PaymentTypeId = 15, PaymentTypeName = "PDQ", DateCreated = EatTime.Now, UserCode = "00001" }

			);
		}

		private static void SeedProtoApps(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProtoApps>().HasData(
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000003"), AppsCode = "03", AppsName = "Fuel Flow DashBoard", DateCreated = new DateTime(2024, 1, 1) },
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000004"), AppsCode = "04", AppsName = "Fuel Flow App", DateCreated = new DateTime(2024, 1, 1) },
				new ProtoApps { Id = new Guid("11111111-0000-0000-0000-000000000005"), AppsCode = "05", AppsName = "Car Wash App", DateCreated = EatTime.Now }
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
					PasswordLastUpdated = EatTime.Now,
					DateModified = EatTime.Now,
					DateCreated = EatTime.Now,
					AccessFailedCount = 0,
					LastLoginDate = EatTime.Now,
				}
			);
		}

		private static void SeedStation(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GasStation>().HasData(
				new GasStation
				{
					Id = 1,
					DateCreated = EatTime.Now,
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
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000001"), AppsCode = "03", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999", },
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000002"), AppsCode = "04", DateCreated = new DateTime(2024, 1, 1), UserCode = "99999" },
				new UserApps { Id = new Guid("22222222-0000-0000-0000-000000000003"), AppsCode = "05", DateCreated =  EatTime.Now, UserCode = "99999"}

			);
		}

		private static void SeedDispenser(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Dispenser>().HasData(
				new Dispenser { Id = 1, DateCreated = EatTime.Now, IsActive = true, StationCode = "S001", UserCode = "00001", DispenserCode = "D01", DispenserName = "D1", StorageLocation = "kenya", TillNumber = "5617668" }
			);
		}

		private static void SeedProducts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Products>().HasData(
				new Products { ProductCode = "02", ProductName = "Diesel", DateCreated = EatTime.Now, IsActive = true, UserCode = "000001", Id = 1 },
				new Products { ProductCode = "01", ProductName = "Petrol", DateCreated = EatTime.Now, IsActive = true, UserCode = "000001", Id = 2 },
				new Products { ProductCode = "03", ProductName = "Autogas", DateCreated = EatTime.Now, IsActive = true, UserCode = "000001", Id = 3 }
			);

		}

		private static void SeedPrices(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Price>().HasData(
				new Price { ProductCode = "02", Amount = 234, DateCreated = EatTime.Now, Discount = 0, UserCode = "000001", DispenserCode = "D01", StationCode = "S001", Id = 1 },
				new Price { ProductCode = "01", Amount = 214, DateCreated = EatTime.Now, Discount = 0, UserCode = "000001", DispenserCode = "D01", StationCode = "S001", Id = 2 },
				new Price { ProductCode = "03", Amount = 105, DateCreated = EatTime.Now, Discount = 0, UserCode = "000001", DispenserCode = "D01", StationCode = "S001", Id = 3 }
			);

		}

		private static void SeedPdaDevices(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PdaDevices>().HasData(
				new PdaDevices { Id = 1, DateCreated = EatTime.Now, IsActive = true, UserCode = "99999", DispenserCode = "D01", DeviceIMEI = "1234567890", DeviceCode = "1234567890", DeviceMacAddress = "1234567890", DeviceModel = "1234567890", DeviceName = "Test PDA", DeviceSerialNumber = "1234567890" },
				new PdaDevices { Id = 2, DateCreated = EatTime.Now, IsActive = true, UserCode = "99999", DispenserCode = "D02", DeviceIMEI = "1234567890", DeviceCode = "1234567890", DeviceMacAddress = "1234567890", DeviceModel = "1234567890", DeviceName = "Test PDA", DeviceSerialNumber = "1234567890" },
				new PdaDevices { Id = 3, DateCreated = EatTime.Now, IsActive = true, UserCode = "99999", DispenserCode = "D04", DeviceIMEI = "1234567890", DeviceCode = "1234567890", DeviceMacAddress = "1234567890", DeviceModel = "1234567890", DeviceName = "Test PDA", DeviceSerialNumber = "1234567890" },
				new PdaDevices { Id = 4, DateCreated = EatTime.Now, IsActive = true, UserCode = "99999", DispenserCode = "D07", DeviceIMEI = "1234567890", DeviceCode = "1234567890", DeviceMacAddress = "1234567890", DeviceModel = "1234567890", DeviceName = "Test PDA", DeviceSerialNumber = "1234567890" }
				);

		}
		/// <summary>
		/// he;llo
		/// </summary>
		/// <param name="modelBuilder"></param>
		private static void SeedNozzles(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Nozzle>().HasData(
				new Nozzle { Id = 1, DateCreated = EatTime.Now, IsActive = true, NozzleCode = "N01", UserCode = "00001", DispenserCode = "D01", NozzleName = "N01", PetroleumCode = "03" },
				new Nozzle { Id = 2, DateCreated = EatTime.Now, IsActive = true, NozzleCode = "N02", UserCode = "00001", DispenserCode = "D01", NozzleName = "N02",PetroleumCode = "01" }
			);
		}

		private static void SeedDispenserAssignment(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DispenserAssignment>().HasData(
				new DispenserAssignment { Id = 1, StationCode = "S001", AssignedBy = "99999", AttedantUserCode = "99999", DateAssigned = EatTime.Now, DispenserCode = "D01", IsActive = true }
			);
		}
		/// <summary>
		/// /
		/// </summary>
		/// <param name="modelBuilder"></param>
		private static void SeedTills(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tills>().HasData(
				new Tills { Id = 1, StoreNumber = "5545198", DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), LastFetch = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TillName = "TRIO FUELS Till 1", IsActive = true, OffsetValue = 0, TillNumber = "5617668", UserCode = "99999" },
				new Tills { Id = 2, StoreNumber = "5545196", DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), LastFetch = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TillName = "TRIO FUELS Till 2", IsActive = true, OffsetValue = 0, TillNumber = "5617666", UserCode = "99999" },
				new Tills { Id = 3, StoreNumber = "5545194", DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), LastFetch = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TillName = "TRIO FUELS Till 3", IsActive = true, OffsetValue = 0, TillNumber = "5617664", UserCode = "99999" },
				new Tills { Id = 4, StoreNumber = "5545192", DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), LastFetch = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TillName = "TRIO FUELS Till 4", IsActive = true, OffsetValue = 0, TillNumber = "5617662", UserCode = "99999" },
				new Tills { Id = 5, StoreNumber = "5545190", DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), LastFetch = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), TillName = "TRIO FUELS Till 5", IsActive = true, OffsetValue = 0, TillNumber = "5617660", UserCode = "99999" }
			);
		}

		private static void SeedQuantityTransactions(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<QuantityTransactions>().HasData(
				new QuantityTransactions { Id = 1, NozzleCode = "N01", AmountCredit = 0, AmountDebit = 0, ShiftNumber = "", DateCreated = EatTime.Now, Discount = 0, DispenserCode = "D01", IsReversed = false, OtpUsed = "", PaymentTypeCode = 99, Price = 0, QuantityCredit = 50, RoundedDate = EatTime.Now, QuantityDebit = 0, SaleId = "", StationCode = "S001", UserCode = "99999", Vat_Amount = 0, VehicleRegistrationNumber = "" },
				new QuantityTransactions { Id = 2, NozzleCode = "N02", AmountCredit = 0, AmountDebit = 0, ShiftNumber = "", DateCreated = EatTime.Now, Discount = 0, DispenserCode = "D01", IsReversed = false, OtpUsed = "", PaymentTypeCode = 99, Price = 0, QuantityCredit = 50, RoundedDate = EatTime.Now, QuantityDebit = 0, SaleId = "", StationCode = "S001", UserCode = "99999", Vat_Amount = 0, VehicleRegistrationNumber = "" }
			);
		}

		private static void SeedStockTakes(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StockTake>().HasData(
				new StockTake { ClosingReading = 0, DateCreated = EatTime.Now, NozzleCode = "N01", ShiftNumber = "", OpeningReading = 50, TakeType = 99, UserCode = "99999", Id = 1 },
				new StockTake { ClosingReading = 0, DateCreated = EatTime.Now, NozzleCode = "N02", ShiftNumber = "", OpeningReading = 50, TakeType = 99, UserCode = "99999", Id = -1 }
			);
		}

		private static void SeedSetting(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Setup>().HasData(
				new Setup { Id = 1,App_VersionCode = "0.0.0.1",PasswordExpiryDays = 30 }
			);
		}
		private static void SeedPetroleumProducts(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PetroleumProducts>().HasData(
				new PetroleumProducts { Id = 1, DateCreated = EatTime.Now, UserCode = "99999", PetroleumCode = "01", PetroleumName = "Autogas" },
				new PetroleumProducts { Id = 2, DateCreated = EatTime.Now, UserCode = "99999", PetroleumCode = "02", PetroleumName = "Petrol" },
				new PetroleumProducts { Id = 3, DateCreated = EatTime.Now, UserCode = "99999", PetroleumCode = "03", PetroleumName = "Diesel" }
			);
		}

		private static void SeedRoles(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, DateCreated = EatTime.Now, RoleCode = "001", RoleName = "Administrator", UserCode = "99999" },
				new Role { Id = 2, DateCreated = EatTime.Now, RoleCode = "002", RoleName = "SuperVisor", UserCode = "99999" },
				new Role { Id = 3, DateCreated = EatTime.Now, RoleCode = "003", RoleName = "Attendant", UserCode = "99999" },
				new Role { Id = 4, DateCreated = EatTime.Now, RoleCode = "004", RoleName = "Accountant", UserCode = "99999" }
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