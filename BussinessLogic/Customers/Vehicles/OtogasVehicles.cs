using BusinessLogic.CustomerService;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BussinessLogic.Customers.Vehicles
{
	// This class manages vehicle-related operations, including adding, updating, activating/deactivating, and searching for vehicles.
	public class OtogasVehicles
	{
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ILogger<OtogasVehicles> _logger;
		private readonly IEmailService _emailService; 
		

		public OtogasVehicles(OTOContext context, ICommonSetups setups, IAuthCommonTasks authentication, ILogger<OtogasVehicles> logger,IEmailService emailService)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_logger = logger;
			_emailService = emailService;
		}

		// Adds a new vehicle to the system after validating and normalizing the registration number.
		public async Task<ServiceResponse<object>> AddVehicle(VehicleDto vehicleDTO)
		{
			try
			{
				if (vehicleDTO == null)
					return ServiceResponse<object>.Information("Invalid request payload", null);

				_logger.LogInformation("Received parameters: {@VehicleDTO}", vehicleDTO);

				// ---------------- NORMALIZE + VALIDATE REG NUMBER ----------------

				var normalizedNumber = NormalizeRegistrationNumber(vehicleDTO.VehicleRegistrationNumber);

				if (string.IsNullOrWhiteSpace(normalizedNumber))
					return ServiceResponse<object>.Information("Vehicle registration number is required", null);

				if (!IsValidVehicleNumber(normalizedNumber))
					return ServiceResponse<object>.Information(
						$"Invalid vehicle registration number {vehicleDTO.VehicleRegistrationNumber}. Must be 7 characters",
						null);

				vehicleDTO.VehicleRegistrationNumber = normalizedNumber;

				// ---------------- CHECK DUPLICATE ----------------

				if (await VehicleExists(normalizedNumber))
					return ServiceResponse<object>.Information(
						$"Vehicle registration number {vehicleDTO.VehicleRegistrationNumber} already exists",
						null);

				// ---------------- GENERATE VEHICLE CODE ----------------

				var vehicleCode = await _setups.GetCodeGenerator("VehicleCode");
				if (vehicleCode == null)
				{
					_logger.LogWarning("Vehicle code generation failed for {reg}", normalizedNumber);
					return ServiceResponse<object>.Information("Failed to generate vehicle code. Vehicle not added.", null);
				}

				// ---------------- CREATE ENTITY ----------------

				var vehicle = SaveVehicle(vehicleDTO, vehicleCode);

				// ---------------- EXECUTION STRATEGY + TRANSACTION ----------------

				var strategy = _context.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					await using var transaction = await _context.Database.BeginTransactionAsync();

					_context.Vehicles.Add(vehicle);
					await _context.SaveChangesAsync();

					// build audit AFTER save so data is guaranteed persisted
					var productName = await GetProductName(vehicleDTO.ProductCode);

					var message =
						$"Vehicle added with details: " +
						$"Registration Number: {vehicle.VehicleRegistrationNumber}, " +
						$"Make: {vehicleDTO.VehicleMake}, " +
						$"Model: {vehicleDTO.VehicleModel}, " +
						$"PhoneNumber: {vehicleDTO.PhoneNumber}, " +
						$"ProductName: {productName}, " +
						$"Tank Capacity: {vehicleDTO.TankCapacity}, " +
						$"Added by: {_authentication.Username()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

					await _authentication.AddUserTrail(message, nameof(AddVehicle));

					_logger.LogInformation(message);

					await transaction.CommitAsync();
				});

				// ---------------- SUCCESS ----------------

				return ServiceResponse<object>.Success("Vehicle added successfully", vehicle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while adding the vehicle");

				return ServiceResponse<object>.Error(
					"An unexpected error occurred. Vehicle not added.",
					null);
			}
		}
		//add 
		async Task<string> GetProductName(string productCode)
		{ 
			var products = await _context.Products.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefaultAsync();
			if (products != null)
				return products.ProductName ?? string.Empty;
			return string.Empty;
		}

		// Deactivates an existing vehicle based on its code.
		public async Task<ServiceResponse<object>> DeactivateVehicle(string vehicleCode)
		{
			try
			{
				var vehicle = await GetVehicleByCodeAsync(vehicleCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found", null);

				vehicle.IsActive = false;
				_context.Vehicles.Update(vehicle);
				await _context.SaveChangesAsync();

				var message = $"Vehicle with number plate {vehicle.VehicleRegistrationNumber} was deactivate by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Vehicle deactivated successfully", vehicle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during deactivating vehicle");
				return ServiceResponse<object>.Error("Vehicle not deactivated", null);
			}
		}

		// Activates a vehicle that was previously deactivated.
		public async Task<ServiceResponse<object>> ActivateVehicle(string vehicleCode)
		{
			try
			{
				var vehicle = await GetVehicleByCodeAsync(vehicleCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found", null);

				vehicle.IsActive = true;
				_context.Vehicles.Update(vehicle);
				await _context.SaveChangesAsync();

				var message = $"Vehicle with number plate {vehicle.VehicleRegistrationNumber} was activate by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success($"Vehicle {vehicle.VehicleRegistrationNumber} activated", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during activating vehicle");
				return ServiceResponse<object>.Error("Vehicle not activated", null);
			}
		}

		// Updates an existing vehicle's details.
		public async Task<ServiceResponse<object>> UpdateVehicle(UpdateVehicleDto vehicleDTO)
		{
			try
			{
				if (vehicleDTO == null)
					return ServiceResponse<object>.Information("Vehicle details cannot be empty", null);

				if (string.IsNullOrWhiteSpace(vehicleDTO.VehicleCode))
					return ServiceResponse<object>.Information("Vehicle code cannot be empty", null);

				var vehicle = await GetVehicleByCodeAsync(vehicleDTO.VehicleCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found", null);

				// Normalize and validate the vehicle registration number
				var normalizedNumber = NormalizeRegistrationNumber(vehicleDTO.VehicleNumber);
				if (!IsValidVehicleNumber(normalizedNumber))
					return ServiceResponse<object>.Information($"Invalid vehicle registration number {vehicleDTO.VehicleNumber}. Must be 7 characters", null);


				var updateMessages = new StringBuilder();

				// Helper method for updating fields
				void UpdateField<T>(string fieldName, T currentValue, T newValue, Action<T> updateAction)
				{
					if (!EqualityComparer<T>.Default.Equals(currentValue, newValue))
					{
						updateMessages.AppendLine($"Changed {fieldName} from {currentValue} to {newValue}");
						updateAction(newValue);
					}
				}

				// Update vehicle fields
				UpdateField("Vehicle Registration Number", vehicle.VehicleRegistrationNumber, vehicleDTO.VehicleNumber, value => vehicle.VehicleRegistrationNumber = value.ToUpper());
				UpdateField("Vehicle Make", vehicle.VehicleMake, vehicleDTO.VehicleMake, value => vehicle.VehicleMake = value);
				UpdateField("Vehicle Model", vehicle.VehicleModel, vehicleDTO.VehicleModel, value => vehicle.VehicleModel = value);
				UpdateField("Tank Capacity", vehicle.TankCapacity, vehicleDTO.TankCapacity, value => vehicle.TankCapacity = value);
				UpdateField("Product Code", vehicle.ProductCode, vehicleDTO.ProductCode, value => vehicle.ProductCode = value);
				UpdateField("Conversion Station", vehicle.ConversionStation, vehicleDTO.ConversionStation, value => vehicle.ConversionStation = value);
				UpdateField("Conversion Date", vehicle.ConversionDate, vehicleDTO.ConversionDate, value => vehicle.ConversionDate = value);
				UpdateField("Phone Number", vehicle.PhoneNumber, vehicleDTO.PhoneNumber, value => vehicle.PhoneNumber = value);
				UpdateField("Phone Number 2", vehicle.PhoneNumber2, vehicleDTO.PhoneNumber2, value => vehicle.PhoneNumber2 = value);

				// Save changes if any updates were made
				if (updateMessages.Length > 0)
				{
					_context.Vehicles.Update(vehicle);
					await _context.SaveChangesAsync();

					// Audit trail
					var message = $"{_authentication.Name()} made the following changes to vehicle {vehicle.VehicleCode} on {DateTime.UtcNow:yyyy-MM-dd hh:mm tt}: {updateMessages}";
					await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

					return ServiceResponse<object>.Success($"Vehicle {vehicle.VehicleRegistrationNumber} updated successfully", vehicle);
				}

				return ServiceResponse<object>.Information("No changes were made to the vehicle details.", null);
			}
			catch (Exception ex)
			{	
				return ServiceResponse<object>.Error($"An error occurred while updating the vehicle: {ex.Message} (Vehicle Code: {vehicleDTO.VehicleCode})", null);
			}
		}

		// Adds a new vehicle status type to the system.
		public async Task<ServiceResponse<object>> AddVehicleStatus(string description)
		{
			try
			{
				var statusCode = await _setups.GetCodeGenerator("StatusCode");
				if (statusCode == null)
					return ServiceResponse<object>.Information("Status code generation failed", null);

				var status = new VehicleStatusTypes
				{
					StatusCode = statusCode,
					Description = description,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					IsActive = true
				};

				await _context.VehicleStatusTypes.AddAsync(status);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Vehicle status added successfully", status);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during adding vehicle status");
				return ServiceResponse<object>.Error("Vehicle status not added", null);
			}
		}

		// Retrieves all vehicle status types.
		public async Task<ServiceResponse<object>> GetVehicleStatus()
		{
			try
			{
				var statuses = await _context.VehicleStatusTypes.AsNoTracking().ToListAsync();
				return ServiceResponse<object>.Success("Vehicle statuses retrieved successfully", statuses);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during retrieving vehicle statuses");
				return ServiceResponse<object>.Error("Vehicle statuses not retrieved", null);
			}
		}


		// Updates the status of a specific vehicle based on its code.
		public async Task<ServiceResponse<object>> UpdateVehicleStatus(string vehicleCode, string statusCode)
		{
			try
			{
				var vehicle = await GetVehicleByCodeAsync(vehicleCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found", null);

				vehicle.Status = statusCode;
				vehicle.IsActive = false;
				_context.Vehicles.Update(vehicle);
				await _context.SaveChangesAsync();

				await LogVehicleAction(vehicle.VehicleRegistrationNumber, "status updated");

				return ServiceResponse<object>.Success("Vehicle status updated successfully", vehicle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during updating vehicle status");
				return ServiceResponse<object>.Error("Vehicle status not updated", null);
			}
		}

		// Retrieves a paginated list of vehicles with optional filters for customer name, registration number, product code, and status.
		public async Task<ServiceResponse<object>> GetAllVehicles(
			int page,
			int pageSize,
			string? customerName = null,
			string? registrationNumber = null,
			string? productCode = null,
			bool? status = null)
		{
			try
			{
				// Join Vehicles -> Customers -> Products
				var query = from v in _context.Vehicles.AsNoTracking()
							join c in _context.Customers.AsNoTracking()
								on v.CustomerCode equals c.CustomerCode
							join p in _context.Products.AsNoTracking()
								on v.ProductCode equals p.ProductCode into productJoin
							from p in productJoin.DefaultIfEmpty() // Left join for optional Product
							select new
							{
								v.VehicleCode,
								v.VehicleRegistrationNumber,
								v.VehicleModel,
								v.VehicleMake,
								v.ConversionDate,
								v.ConversionStation,
								v.ProductCode,
								p.ProductName,
								v.TankCapacity,
								v.IsActive,
								c.CustomerName,
								v.CustomerCode,
								CustomerPhone = v.PhoneNumber,
								c.CustomerEmail,
								v.DateCreated,
								v.CreditLimit,
								HasTelematic = v.IsTelematicInstalled,
								v.TelematicSerialNumber
							};

				// Apply filters
				if (!string.IsNullOrWhiteSpace(customerName))
					query = query.Where(v => v.CustomerName.Contains(customerName));

				if (!string.IsNullOrWhiteSpace(registrationNumber))
					query = query.Where(v => v.VehicleRegistrationNumber.Contains(registrationNumber));

				if (!string.IsNullOrWhiteSpace(productCode))
					query = query.Where(v => v.ProductCode == productCode);

				if (status.HasValue)
					query = query.Where(v => v.IsActive == status.Value);

				// Get total count
				var totalRecords = await query.CountAsync();

				// Fetch paginated results
				var data = await query
					.OrderByDescending(v => v.DateCreated)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				// Add RowNo manually
				var rowNoStart = (page - 1) * pageSize + 1;
				var vehiclesWithRowNo = data.Select((v, index) => new VehicleDto2
				{
					RowNo = rowNoStart + index,
					VehicleCode = v.VehicleCode,
					VehicleRegistrationNumber = v.VehicleRegistrationNumber,
					VehicleModel = v.VehicleModel,
					VehicleMake = v.VehicleMake,
					ConversionDate = v.ConversionDate,
					ConversionStation = v.ConversionStation,
					ProductCode = v.ProductCode,
					ProductName = v.ProductName,
					TankCapacity = v.TankCapacity,
					IsActive = v.IsActive,
					CustomerName = v.CustomerName,
					CustomerCode = v.CustomerCode,
					CustomerPhone = v.CustomerPhone,
					CustomerEmail = v.CustomerEmail,
					DateCreated = v.DateCreated,
					CreditLimit = v.CreditLimit,
					HasTelematic = v.HasTelematic,
					TelematicSerialNumber = v.TelematicSerialNumber,
				}).ToList();

				var pagedResult = new
				{
					TotalRecords = totalRecords,
					PageNumber = page,
					PageSize = pageSize,
					Vehicles = vehiclesWithRowNo
				};

				return ServiceResponse<object>.Success("Vehicles retrieved successfully", pagedResult);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving vehicles");
				return ServiceResponse<object>.Error("Vehicles not retrieved", null);
			}
		}


		// Retrieves a list of all vehicles.
		public async Task<ServiceResponse<object>> GetAllVehicles()
		{
			try
			{
				var vehicles = await _context.Vehicles
					.AsNoTracking()
					.Join(_context.Customers.AsNoTracking(), v => v.CustomerCode, c => c.CustomerCode, (v, c) => new
					{
						v.VehicleRegistrationNumber,
						v.VehicleModel,
						v.VehicleMake,
						v.ConversionDate,
						v.ConversionStation,
						v.ProductCode,
						v.TankCapacity,
						v.IsActive,
						c.CustomerName,
						c.CustomerCode,
						CustomerPhone = v.PhoneNumber,
						c.CustomerEmail,
						v.DateCreated,
						v.UserCode,
						v.VehicleCode,
						v.IsTelematicInstalled,
						v.TelematicSerialNumber

					})
					.OrderByDescending(v => v.DateCreated)
					.ToListAsync();

				return ServiceResponse<object>.Success("Vehicles retrieved successfully", vehicles);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all vehicles");
				return ServiceResponse<object>.Error("Vehicles not retrieved", null);
			}
		}

		// Retrieves a specific vehicle based on its unique code.
		public async Task<ServiceResponse<object>> GetVehicleByCode(string vehicleCode)
		{
			try
			{
				var vehicle = await _context.Vehicles
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.VehicleCode.Replace(" ", "") == vehicleCode.Replace(" ", ""));

				if (vehicle != null)
					return ServiceResponse<object>.Success("Vehicle retrieved successfully", vehicle);
				else
					return ServiceResponse<object>.Information("Vehicle not found", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving vehicle by code");
				return ServiceResponse<object>.Error("Vehicle not retrieved", null);
			}
		}

		// Searches for a vehicle by its registration number within a specific station.


		public async Task<ServiceResponse<object>> SearchVehicle(string vehicleRegNo)
		{
			try
			{
				// Normalize and validate
				if (string.IsNullOrWhiteSpace(vehicleRegNo))
					return ServiceResponse<object>.Error("Number plate is required", null);

				vehicleRegNo = vehicleRegNo.Replace(" ", "").ToUpper();

				// Regex pattern: Kenya standard number plate e.g., KAA123A
				var pattern = @"^K[A-Z]{2}[0-9]{3}[A-Z]$";

				// Tuktuk pattern: KTWA123A
				var tuktukPattern = @"^K[A-Z]{3}[0-9]{3}[A-Z]$";

				if (!Regex.IsMatch(vehicleRegNo, pattern) && !Regex.IsMatch(vehicleRegNo, tuktukPattern))
					return ServiceResponse<object>.Error($"{vehicleRegNo.ToUpper()} is invalid, please try again!", null);

				var userCode = _authentication.Usercode();

				// Get stationCode
				var stationCode = await _context.DispenserAssignments
					.Where(d => d.AttedantUserCode == userCode)
					.Join(_context.Dispensers, d => d.DispenserCode, ds => ds.DispenserCode, (d, ds) => ds.StationCode)
					.FirstOrDefaultAsync();

				return await SearchVehicleByStation(vehicleRegNo, stationCode ?? string.Empty,null);
			}
			catch (Exception ex)
			{
				// Log exception here if you have a logger
				return ServiceResponse<object>.Error("Vehicle not retrieved", ex.Message);
			}
		}


		// Searches for a vehicle by its registration number and a specific station code.
		public async Task<ServiceResponse<object>> SearchVehicle(string vehicleRegNo, string stationCode,string? shiftNumber )
		{
			try
			{
				return await SearchVehicleByStation(vehicleRegNo, stationCode,shiftNumber);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				_logger.LogError(ex, "Error searching vehicle with station code");
				return ServiceResponse<object>.Error("Vehicle not retrieved", null);
			}
		}

		// Private method to search for a vehicle within a specific station and calculate its wallet balance.
		private async Task<ServiceResponse<object>> SearchVehicleByStation(
		string vehicleRegNo,
		string stationCode,
		string? shiftNumber)
		{
			vehicleRegNo = vehicleRegNo.Replace(" ", "").ToUpper();

			// 1️⃣ Get vehicle
			var vehicle = await _context.Vehicles
				.AsNoTracking()
				.Where(v => v.VehicleRegistrationNumber == vehicleRegNo)
				.Select(v => new
				{
					v.VehicleCode,
					v.ProductCode,
					v.VehicleRegistrationNumber,
					v.PhoneNumber,
					v.CreditLimit,
					v.CustomerCode
				})
				.FirstOrDefaultAsync();


			if (vehicle == null)
				return ServiceResponse<object>.WalkInCustomer("Vehicle not found", null);

			var customer = await _context.Customers
			.AsNoTracking()
			.Where(x => x.CustomerCode == vehicle.CustomerCode && x.IsCreditCustomer == true)
			.Select(x => new
			{
				x.CreditLimit,
				x.CustomerCode,
				x.IsCreditCustomer,
				x.CustomerName
			})
			.FirstOrDefaultAsync();

			if (customer == null)
				return ServiceResponse<object>.Information("Customer not found", null);

			var outstandingCredit = await _context.CreditTransactions
				.Where(c => c.CustomerCode == customer.CustomerCode)
				.SumAsync(c => c.Debit - c.Credit);

			var Crediresult = new
			{
				customer.CustomerName,
				customer.CreditLimit,
				customer.CustomerCode,
				customer.IsCreditCustomer,
				OutstandingCredit = outstandingCredit,
				AvailableCredit = customer.CreditLimit - outstandingCredit
			};

			var today = DateTime.UtcNow.Date;

			// 2️⃣ Loyalty subscription
			var hasLoyaltySubscription = await _context.LoyaltySubscriptions
				.AnyAsync(x => x.VehicleCode == vehicle.VehicleCode);

			// 3️⃣ Wallet balance
			var walletBalance = await _context.CustomerTransactions
				.Where(ct => ct.VehicleCode == vehicle.VehicleCode)
				.SumAsync(x => (decimal?)x.Credit - x.Debit) ?? 0;

			// 4️⃣ Voucher balance
			var voucherBalance = await _context.RoyaltyPoints
				.Where(p => p.CustomerCode == vehicle.CustomerCode)
				.SumAsync(x => (decimal?)x.PointsCredit - x.PointsDebit) ?? 0;

			// 5️⃣ Active vouchers
			var activeVoucher = await _context.Vouchers
				.AsNoTracking()
				.Where(v =>
					v.VehicleCode == vehicle.VehicleCode &&
					!v.IsUsed &&
					v.ExpiryDate >= DateTime.UtcNow)
				.Select(v => new
				{
					v.VoucherNo,
					v.Amount,
					ExpiryDate = v.ExpiryDate.Date
				})
				.ToListAsync();

			// 6️⃣ Price + discount in one query
			var priceInfo = await _context.Prices
				.Where(p =>
					p.ProductCode == vehicle.ProductCode &&
					p.StationCode == stationCode)
				.Select(p => new
				{
					p.Amount,
					p.Discount
				})
				.FirstOrDefaultAsync();

			var discount = priceInfo?.Discount ?? 0;
			var stationPrice = priceInfo?.Amount ?? 0;

			// 7️⃣ Approved price
			var approvedPrice = await _context.PriceApproval
				.Where(pa =>
					pa.NumberPlate == vehicleRegNo &&
					pa.ShiftNumber == shiftNumber &&
					pa.IsApproved &&
					!pa.IsApprovalExecuted)
				.Select(pa => pa.ProposedPrice)
				.FirstOrDefaultAsync();

			// 8️⃣ Today's fuel
			var todayFuel = await (
				from t in _context.QuantityTransactions
				join s in _context.Stations on t.StationCode equals s.StationCode
				join p in _context.PaymentTypes on t.PaymentTypeCode equals p.PaymentTypeId
				where t.DateCreated >= today &&
					  t.DateCreated < today.AddDays(1) &&
					  t.VehicleCode == vehicle.VehicleCode
				select new
				{
					s.StationName,
					p.PaymentTypeName,
					t.AmountCredit
				}).ToListAsync();

			// 9️⃣ Final price calculation
			var finalPrice = approvedPrice > 0
				? approvedPrice
				: Math.Max(stationPrice - discount, 0);

			// 🔟 Final response
			var result = new
			{
				vehicle.VehicleRegistrationNumber,
				vehicle.VehicleCode,
				vehicle.ProductCode,
				vehicle.PhoneNumber,
				Crediresult.CreditLimit,
				WalletBalance = walletBalance,
				VoucherBalance = voucherBalance,
				HasLoyaltySubscription = hasLoyaltySubscription,
				HasDiscount = discount > 0,
				ActiveVoucher = activeVoucher,
				TodayFuel = todayFuel,
				Price = finalPrice,
				Crediresult.IsCreditCustomer,
				Crediresult.OutstandingCredit,
				Crediresult.AvailableCredit,
				Crediresult.CustomerCode,
				Crediresult.CustomerName,
				
			};

			return ServiceResponse<object>.Success("Vehicle retrieved successfully", result);
		}
		// Transfers a vehicle to a new customer and saves the previous customer as part of transfer history.
		public async Task<ServiceResponse<object>> TransferVehicle(TransferVehicleDto transferVehicle)
		{
			try
			{
				var vehicle = await GetVehicleByCodeAsync(transferVehicle.VehicleCode);
				if (vehicle == null)
					return ServiceResponse<object>.Information("Vehicle not found", null);

				var previousCustomerCode = vehicle.CustomerCode;
				var transferredVehicle = new TransFeredVehicles
				{
					VehicleCode = vehicle.VehicleCode,
					TransFerDate = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					CustomerCode = previousCustomerCode,
					NewCustomerCode = transferVehicle.CustomerCode,
					DateCreated = DateTime.UtcNow,
					ConversionDate = vehicle.ConversionDate,
					ConversionStation = vehicle.ConversionStation,
					ProductCode = vehicle.ProductCode,
					TankCapacity = vehicle.TankCapacity,
					IsActive = false,
					VehicleMake = vehicle.VehicleMake,
					VehicleModel = vehicle.VehicleModel,
					VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
					Status = "Transferred",
					NFC_CardNumber = vehicle.NFC_CardNumber,
					TransactionPIN = vehicle.TransactionPIN,
				};

				vehicle.CustomerCode = transferVehicle.CustomerCode;
				_context.Update(vehicle);

				await _context.TransFeredVehicles.AddAsync(transferredVehicle);
				await _context.SaveChangesAsync();

				var message = $"Vehicle with registration number was transfered from customer {vehicle.CustomerCode} to {previousCustomerCode} on {DateTime.UtcNow} by {_authentication.Name()}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
				return ServiceResponse<object>.Success("Vehicle transferred successfully", vehicle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during transferring vehicle");
				return ServiceResponse<object>.Error("Vehicle not transferred", null);
			}
		}

		// Marks a vehicle as uninstalled (e.g., the vehicle is no longer part of the system).
		public async Task<ServiceResponse<VehicleUninstallDto>> MarkVehicleAsUnInstalled(string vehicleCode)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var vehicle = await GetVehicleByCodeAsync(vehicleCode);
				if (vehicle == null)
					return ServiceResponse<VehicleUninstallDto>.Information("Vehicle not found", null);

				if (vehicle.Status == VehicleStatus.Uninstalled)
				{
					return ServiceResponse<VehicleUninstallDto>.Information("Vehicle is already marked as uninstalled",
						new VehicleUninstallDto
						{
							VehicleCode = vehicle.VehicleCode,
							VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
							Status = vehicle.Status.ToString(),
							IsTelematicInstalled = vehicle.IsTelematicInstalled,
							TelematicSerialNumber = vehicle.TelematicSerialNumber,
							IsActive = vehicle.IsActive
						});
				}

				// Keep old serial for logging
				var oldSerial = vehicle.TelematicSerialNumber;

				// Update vehicle
				vehicle.Status = VehicleStatus.Uninstalled;
				vehicle.IsTelematicInstalled = false;
				vehicle.TelematicSerialNumber = string.Empty;
				vehicle.IsActive = false;

				_context.Entry(vehicle).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				// Audit log
				var message = $"Vehicle {vehicle.VehicleRegistrationNumber} was uninstalled by " +
							  $"{_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC. " +
							  $"Previous Serial: {oldSerial}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? nameof(MarkVehicleAsUnInstalled));

				await transaction.CommitAsync();

				// Return DTO (not EF entity)
				var responseDto = new VehicleUninstallDto
				{
					VehicleCode = vehicle.VehicleCode,
					VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
					Status = vehicle.Status.ToString(),
					IsTelematicInstalled = vehicle.IsTelematicInstalled,
					TelematicSerialNumber = vehicle.TelematicSerialNumber,
					IsActive = vehicle.IsActive
				};

				var To  = await (from c in _context.Emails
										 where c.ReportCode == "016"
										 select c.ToCC ).FirstOrDefaultAsync();

				var CC  = await (from c in _context.Emails
											where c.ReportCode == "016"
											select c.ToCC).FirstOrDefaultAsync();
				CC = string.Join(",", CC, _authentication.Email());


				_emailService.SendEmail(To is null ? "reports@protoenergy.com" : To, CC, "Vehicle Uninstalled Notification",
					BuildVehicleUninstalledEmail(_authentication.Name(), vehicle.VehicleRegistrationNumber, oldSerial, _authentication.Name()));

				return ServiceResponse<VehicleUninstallDto>.Success("Vehicle marked as uninstalled", responseDto);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				_logger.LogError(ex, $"[{nameof(MarkVehicleAsUnInstalled)}] Error during vehicle uninstall");
				return ServiceResponse<VehicleUninstallDto>.Error("Vehicle not updated", null);
			}
		}

		//se
		private string BuildVehicleUninstalledEmail(string approverName, string vehicleReg, string oldSerial, string initiator)
		{
			return $@"
					<html>
					<head>
						<meta name='viewport' content='width=device-width, initial-scale=1.0'/>
					</head>
					<body style='font-family:Segoe UI, sans-serif; color:#333; line-height:1.5;'>
						<p>👋 Hello <strong>{approverName}</strong>,</p>

						<p>The following vehicle has been <strong>marked as uninstalled</strong> ✅.</p>

						<h3 style='color:#d9534f;'>📌 Vehicle Details:</h3>
						<ul>
							<li>🚘 <b>Registration Number:</b> {vehicleReg}</li>
							<li>🔌 <b>Previous Telematic Serial:</b> {oldSerial}</li>
							<li>👤 <b>Uninstalled By:</b> {initiator}</li>
							<li>🕒 <b>Date:</b> {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC</li>
						</ul>

						<p>🙏 Thank you for keeping the records up to date.</p>

						<p>Best regards,<br/>💼 Your System</p>
					</body>
					</html>";
		}
		// DTO for vehicle uninstall response

		public class VehicleUninstallDto
		{
			public string VehicleCode { get; set; } = string.Empty;
			public string VehicleRegistrationNumber { get; set; } = string.Empty;
			public string Status { get; set; } = string.Empty;
			public bool IsTelematicInstalled { get; set; }
			public string TelematicSerialNumber { get; set; } = string.Empty;
			public bool IsActive { get; set; }
		}


		// Retrieves all available tank sizes.
		public async Task<ServiceResponse<object>> GetTankSizes()
		{
			try
			{
				var tanksSize = await (from s in _context.TankSizes
									   select s).ToListAsync();
				return ServiceResponse<object>.Success("Tank sizes retrieved successfully", tanksSize);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				return ServiceResponse<object>.Information("Tank sizes not retrieved", null);
			}
		}

		// Registers a non-otogas vehicle (e.g., a walk-in customer vehicle) to the system.
		public async Task<ServiceResponse<object>> RegisterNonOtogasVehicle(NonOtogasVehicleDto nonOtogasVehicle)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(nonOtogasVehicle.VehicleRegistrationNumber))
					return ServiceResponse<object>.Information("Must provide vehicle number plate", null);

				if (await _context.Walk_In_Customers.AnyAsync(v => v.VehicleRegistrationNumber == nonOtogasVehicle.VehicleRegistrationNumber))
					return ServiceResponse<object>.Information("Vehicle already registered", null);

				var dispenserTask = GetUserDispenser();
				var vehicleCodeTask = _setups.GetCodeGenerator("VehicleCode");

				await Task.WhenAll(dispenserTask, vehicleCodeTask);

				var dispenser = await dispenserTask;
				var vehicleCode = await vehicleCodeTask;

				if (vehicleCode == null)
					return ServiceResponse<object>.Information("Vehicle not added", null);

				var station = await GetStationName(dispenser);

				var vehicle = new Walk_In_Customers
				{
					KitType = nonOtogasVehicle.KitType,
					VehicleRegistrationNumber = nonOtogasVehicle.VehicleRegistrationNumber,
					ProductCode = "03",
					IsActive = true,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					Name = nonOtogasVehicle.Name,
					PhoneNumber = nonOtogasVehicle.PhoneNumber,
					VehicleMake = nonOtogasVehicle.VehicleModel,
				};

				await using var transaction = await _context.Database.BeginTransactionAsync();

				_context.Walk_In_Customers.Add(vehicle);
				await _context.SaveChangesAsync();
				await _context.Database.ExecuteSqlRawAsync("EXEC RegisterWalkinCustomers");

				await transaction.CommitAsync();

				var message = $"Vehicle {nonOtogasVehicle.VehicleRegistrationNumber} was registered by {_authentication.Name()} as a walk-in customer on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} from {station.StationName} station.";
				await _authentication.AddUserTrail(message, nameof(RegisterNonOtogasVehicle));

				return ServiceResponse<object>.Success("Vehicle added successfully", vehicle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error during adding non-Otogas vehicle");
				return ServiceResponse<object>.Error("Vehicle not added", null);
			}
		}



		// Private helper methods:
		// Checks if a vehicle already exists in the system.
		private async Task<bool> VehicleExists(string vehicleRegNumber) =>
			await _context.Vehicles.AnyAsync(x => x.VehicleRegistrationNumber == vehicleRegNumber);

		// Validates the vehicle registration number length.
		private static bool IsValidVehicleNumber(string vehicleRegNumber)
		{
			return vehicleRegNumber.Length == 7;
		}

		// Normalizes the registration number by removing spaces and converting to uppercase.
		private static string NormalizeRegistrationNumber(string registrationNumber) =>
			registrationNumber.Replace(" ", string.Empty).ToUpperInvariant();

		// Logs actions performed on a vehicle, such as adding or updating.
		private async Task LogVehicleAction(string vehicleRegNo, string action)
		{
			var message = $"Vehicle {vehicleRegNo} was {action} by {_authentication.Name()} on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
			_logger.LogInformation($"Vehicle {vehicleRegNo} {action} by {_authentication.Name()} at {DateTime.UtcNow}", vehicleRegNo, action, _authentication.Name(), DateTime.UtcNow);
		}
		//Loyalty



		// Saves a new vehicle entity to the database.
		private Vehicle SaveVehicle(VehicleDto vehicle, string vehicleCode)
		{
			return new Vehicle
			{
				VehicleModel = vehicle.VehicleModel.ToUpperInvariant(),
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
				CustomerCode = vehicle.CustomerCode,
				IsActive = true,
				VehicleCode = vehicleCode,
				ConversionDate = DateTime.UtcNow,
				ConversionStation = string.Empty,
				VehicleMake = vehicle.VehicleMake,
				ProductCode = vehicle.ProductCode,
				TankCapacity = vehicle.TankCapacity,
				Status = VehicleStatus.Active,
				VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
				PhoneNumber = vehicle.PhoneNumber,
				RoyaltyPointPerLitre = vehicle.RoyaltyPointPerLitre,
			};
		}

		// Updates an existing vehicle entity with new details.
		private void UpdateVehicleEntity(Vehicle vehicle, UpdateVehicleDto vehicleDTO)
		{
			vehicle.VehicleRegistrationNumber = NormalizeRegistrationNumber(vehicleDTO.VehicleNumber);
			vehicle.VehicleModel = vehicleDTO.VehicleModel.ToUpperInvariant();
			vehicle.VehicleMake = vehicleDTO.VehicleMake;
			vehicle.ConversionDate = vehicleDTO.ConversionDate;
			vehicle.ConversionStation = vehicleDTO.ConversionStation;
			vehicle.ProductCode = vehicleDTO.ProductCode;
			vehicle.TankCapacity = vehicleDTO.TankCapacity;
			vehicle.IsActive = true;
			vehicle.UserCode = _authentication.Usercode();
			vehicle.PhoneNumber = vehicleDTO.PhoneNumber;
		}




		// Retrieves a vehicle by its code.
		private async Task<Vehicle?> GetVehicleByCodeAsync(string vehicleCode) =>
			await _context.Vehicles.FirstOrDefaultAsync(x => x.VehicleCode == vehicleCode);

		// get vehicle for a specific customer
		public async Task<ServiceResponse<object>> GetCustomerVehicles(string customerCode)
		{
			try
			{
				var customer = await _context.Customers
					.FirstOrDefaultAsync(x => x.CustomerCode == customerCode);


				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);


				var LoyaltyPointBalance = await (from rb in _context.RoyaltyPoints
												 where rb.CustomerCode == customer.CustomerCode
												 select rb).SumAsync(x => x.PointsCredit - x.PointsDebit);


				var CreditBalance = await (from rb in _context.CreditTransactions
												 where rb.CustomerCode == customer.CustomerCode
												 select rb).SumAsync(x => x.Credit - x.Debit);


				var vehicles = await (
					from v in _context.Vehicles
					join ct in _context.Customers on v.CustomerCode equals ct.CustomerCode
					join cb in _context.CustomerTransactions
						on v.VehicleCode equals cb.VehicleCode into transactionGroup
					from cb in transactionGroup.DefaultIfEmpty() // left join
					where v.CustomerCode == customerCode
					group new { v, ct, cb } by new
					{
						v.VehicleCode,
						v.VehicleRegistrationNumber,
						v.VehicleModel,
						v.VehicleMake,
						v.ConversionDate,
						v.ConversionStation,
						v.ProductCode,
						v.TankCapacity,
						v.IsActive,
						ct.CustomerName,
						ct.CustomerCode,
						CustomerPhone = v.PhoneNumber,
						ct.CustomerEmail,
						v.DateCreated,
						v.CreditLimit,
						ct.BaseLoyaltyPoints,
					} into g
					select new
					{
						g.Key.VehicleCode,
						g.Key.VehicleRegistrationNumber,
						g.Key.VehicleModel,
						g.Key.VehicleMake,
						g.Key.ConversionDate,
						g.Key.ConversionStation,
						g.Key.ProductCode,
						g.Key.TankCapacity,
						g.Key.IsActive,
						g.Key.CustomerName,
						g.Key.CustomerCode,
						g.Key.CustomerPhone,
						g.Key.CustomerEmail,
						g.Key.DateCreated,
						g.Key.CreditLimit,
						Balance = g.Sum(x => (x.cb != null ? x.cb.Credit : 0) - (x.cb != null ? x.cb.Debit : 0))
					}).ToListAsync();

				var customerData = new
				{
					customer.CustomerName,
					customer.CustomerPhone,
					customer.CustomerEmail,
					customer.CustomerCode,
					customer.IdentificationNumber,
					customer.KRAPin,
					customer.DateCreated,
					customer.CreditLimit,
					customer.BaseLoyaltyPoints,
					CreditBalance,
					LoyaltyPointBalance
				};

				var creditBalance = await (from ct in _context.CreditTransactions
										   where ct.CustomerCode == customerCode
										   select new { ct.Credit, ct.Debit }).SumAsync(ct => ct.Credit - ct.Debit);
										  

				var totalBalance = vehicles.Sum(x => x.Balance);
				var totalVehicles = vehicles.Count;
				

				var result = new
				{
					Customer = customerData,
					TotalVehicles = totalVehicles,
					TotalBalance = totalBalance,
					CreditBalance = creditBalance,
					Vehicles = vehicles
				};

				return ServiceResponse<object>.Success("Customer vehicles retrieved successfully", result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving customer vehicles");
				return ServiceResponse<object>.Error("Customer vehicles not retrieved", new object());
			}
		}

		//Merge customers who has same name or phone number or email address choose the one to keep
		public async Task<ServiceResponse<object>> MergeCustomers(string customerCode, string customerCodeToMerge)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerCode);
				var customerToMerge = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerCodeToMerge);

				if (customer == null || customerToMerge == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				//Merge customer details
				customer.CustomerName = customer.CustomerName == customerToMerge.CustomerName ? customer.CustomerName : customerToMerge.CustomerName;
				customer.CustomerPhone = customer.CustomerPhone == customerToMerge.CustomerPhone ? customer.CustomerPhone : customerToMerge.CustomerPhone;
				customer.CustomerEmail = customer.CustomerEmail == customerToMerge.CustomerEmail ? customer.CustomerEmail : customerToMerge.CustomerEmail;
				customer.IdentificationNumber = customer.IdentificationNumber == customerToMerge.IdentificationNumber ? customer.IdentificationNumber : customerToMerge.IdentificationNumber;
				customer.KRAPin = customer.KRAPin == customerToMerge.KRAPin ? customer.KRAPin : customerToMerge.KRAPin;

				//Update customer code in vehicles
				var vehicles = await _context.Vehicles.Where(x => x.CustomerCode == customerCodeToMerge).ToListAsync();
				foreach (var vehicle in vehicles)
				{
					vehicle.CustomerCode = customerCode;
					_context.Vehicles.Update(vehicle);
				}

				//Delete customer to merge
				_context.Customers.Remove(customerToMerge);

				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Customers merged successfully", customer);
			}
			catch (Exception ex)
			{ 
				_logger.LogError(ex, "Error merging customers");
				return ServiceResponse<object>.Error("Customers not merged", null);
			}
		}

		public async Task<string> GetUserDispenser() 
		{
			var dispensercode = await (from ad in _context.DispenserAssignments
									   where ad.AttedantUserCode.Equals(_authentication.Usercode())
									   && ad.IsActive.Equals(true)
									   select ad.DispenserCode).FirstOrDefaultAsync();
			if (dispensercode != null)
				return dispensercode;
			return string.Empty;
		}
		//list customers with same name or phone number or email address to merge
		public async Task<ServiceResponse<object>> ListCustomersToMerge(string customerCode)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerCode == customerCode);

				if (customer == null)
					return ServiceResponse<object>.Information("Customer not found", null);

				var customers = await _context.Customers.Where(x => x.CustomerName == customer.CustomerName || x.CustomerPhone == customer.CustomerPhone || x.CustomerEmail == customer.CustomerEmail).ToListAsync();
				//order the customers by what they have in common with the customer
				customers = customers.OrderByDescending(x => (x.CustomerName == customer.CustomerName ? 1 : 0) + (x.CustomerPhone == customer.CustomerPhone ? 1 : 0) + (x.CustomerEmail == customer.CustomerEmail ? 1 : 0)).ToList();

				return ServiceResponse<object>.Success("Customers to merge retrieved successfully", customers);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving customers to merge");
				return ServiceResponse<object>.Error("Customers to merge not retrieved", null);
			}
		}
		private async Task<StationData> GetStationName(string dispenserCode)
		{
			var station = await (from s in _context.Stations
								 join d in _context.Dispensers on s.StationCode equals d.StationCode
								 join t in _context.Tills on d.TillNumber equals t.TillNumber
								 where d.DispenserCode == dispenserCode
								 select new
								 {
									 s.StationName,
									 s.StationCode,
									 d.TillNumber,
									 t.StoreNumber
								 }).FirstOrDefaultAsync();
			if (station == null)
				return new StationData();
			return new StationData
			{
				StationCode = station.StationCode,
				StationName = station.StationName,
			};
		}

		//get all walk in customers where vehicle registration number is not in the vehicle table
		public async Task<ServiceResponse<object>> GetWalkInCustomers()
		{
			try
			{
				var walkInCustomers = await (from w in _context.Walk_In_Customers
											 join u in _context.Users on w.UserCode equals u.UserCode
											 where !_context.Vehicles.Any(v => v.VehicleRegistrationNumber == w.VehicleRegistrationNumber)
											 select new
											 {
												 w.VehicleRegistrationNumber,
												 Customer_Name = w.Name,
												 Registration_Date = w.DateCreated,
												 w.PhoneNumber, 
												 w.KitType,
												 RegisteredBy = string.Join(" ", u.FirstName,u.MiddName,u.LastName)
											 }).ToListAsync();
				return ServiceResponse<object>.Success("Walk in customers retrieved successfully", walkInCustomers);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving walk in customers");
				return ServiceResponse<object>.Error("Walk in customers not retrieved", null);
			}
		}

		public async Task<ServiceResponse<TellematicDto>> AddTelematic(TellematicDto tellematic)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var vehicle = await _context.Vehicles
					.FirstOrDefaultAsync(v => v.VehicleCode == tellematic.VehicleCode);

				if (vehicle == null)
					return ServiceResponse<TellematicDto>.Information("Vehicle not found.", null);

				if (vehicle.IsTelematicInstalled && !string.IsNullOrEmpty(vehicle.TelematicSerialNumber))
				{
					return ServiceResponse<TellematicDto>.Information("Telematics already installed for this vehicle.",
						new TellematicDto
						{
							VehicleCode = vehicle.VehicleCode,
							TelematicSerialNumber = vehicle.TelematicSerialNumber,
							TelematicInstallationDate = vehicle.TelematicInstallationDate
						});
				}

				// Update details
				vehicle.IsTelematicInstalled = true;
				vehicle.TelematicSerialNumber = tellematic.TelematicSerialNumber;
				vehicle.TelematicInstallationDate = DateTime.UtcNow;

				_context.Entry(vehicle).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				// Audit log
				var message = $"Telematics details added for vehicle {vehicle.VehicleRegistrationNumber} " +
							  $"by {_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC. " +
							  $"Serial: {tellematic.TelematicSerialNumber}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? nameof(AddTelematic));

				// Commit DB transaction
				await transaction.CommitAsync();

				// --- 📧 Send Email Notification ---
				var notification = await _context.Emails.FirstOrDefaultAsync(n => n.ReportCode == "016"); // Example ReportCode for telematics install
				if (notification != null)
				{
					var toList = notification.To;
					var ccList = notification.ToCC;
					ccList = string.Join(",", ccList, _authentication.Email());

					// Build email body
					var emailBody = BuildTelematicInstalledEmail(
						approverName: _authentication.Name(),
						vehicleReg: vehicle.VehicleRegistrationNumber,
						serial: vehicle.TelematicSerialNumber,
						installationDate: vehicle.TelematicInstallationDate
					);

					 _emailService.SendEmail(
						toList,
						ccList,
						$"Telematics Installed - {vehicle.VehicleRegistrationNumber}",
						emailBody
					);
				}

				// Response DTO
				var responseDto = new TellematicDto
				{
					VehicleCode = vehicle.VehicleCode,
					TelematicSerialNumber = vehicle.TelematicSerialNumber,
					TelematicInstallationDate = vehicle.TelematicInstallationDate
				};

				return ServiceResponse<TellematicDto>.Success("Telematics details added successfully.", responseDto);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				_logger.LogError(ex, $"[{nameof(AddTelematic)}] Error adding telematics");
				return ServiceResponse<TellematicDto>.Error("An error occurred while adding telematics details.", null);
			}
		}

		// mark a vehicle true if it has telematciinstalled also capture telematicserialNumber
		private static string BuildTelematicInstalledEmail(string approverName, string vehicleReg, string serial, DateTime? installationDate)
		{
			return $@"
    <html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
    </head>
    <body style='font-family:Segoe UI, sans-serif; color:#333; line-height:1.5;'>
        <p>👋 Hello <strong>{approverName}</strong>,</p>

        <p>The following vehicle has been <strong>fitted with telematics</strong> ✅.</p>

        <h3 style='color:#0078D7;'>📌 Vehicle Details:</h3>
        <ul>
            <li>🚘 <b>Registration Number:</b> {vehicleReg}</li>
            <li>🔌 <b>Telematic Serial:</b> {serial}</li>
            <li>📅 <b>Installation Date:</b> {installationDate:yyyy-MM-dd HH:mm:ss} UTC</li>
            <li>👤 <b>Installed By:</b> {approverName}</li>
        </ul>

        <p>🙏 Thank you for keeping the records updated.</p>

        <p>Best regards,<br/>💼 Your System</p>
    </body>
    </html>";
		}

		public class TellematicDto
		{
			public string VehicleCode { get; set; } = string.Empty;
			public string TelematicSerialNumber { get; set; } = string.Empty;
			public DateTime? TelematicInstallationDate { get; set; }
		}

		public async Task<ServiceResponse<object>> UpdateRoyaltyPoints([Required] string customerCode, [Required] decimal points)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				return ServiceResponse<object>.Information("Customer code is required",null);

			if (points < 0)
				return ServiceResponse<object>.Information("Points cannot be negative",null);

			var customer = await _context.Customers
				.FirstOrDefaultAsync(x => x.CustomerCode == customerCode);

			if (customer == null)
				return ServiceResponse<object>.Information("Vehicle not found", null);

			customer.BaseLoyaltyPoints = points;
			
			_context.Update(customer);
			await _context.SaveChangesAsync();

			return ServiceResponse<object>.Success($"Royalty points updated for {customer.CustomerName}",null);
		}

		//check if is a loyal customer

		public async Task<ServiceResponse<object>> CheckLoyalty([Required]string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
				return ServiceResponse<object>.Information("Customer code is required", null);

			var customer = await _context.Customers
				.Where(x => x.CustomerPhone == phoneNumber)
				.Select(x => new
				{
					x.CustomerCode,
					x.CustomerName,
					x.BaseLoyaltyPoints 
				})
				.FirstOrDefaultAsync();

			if (customer == null)
				return ServiceResponse<object>.Information("Customer not found", null);

			const decimal loyaltyThreshold = 0; // define your loyalty qualification

			bool isLoyal = customer.BaseLoyaltyPoints > loyaltyThreshold; 

			return ServiceResponse<object>.Success(
				isLoyal
					? $"{customer.CustomerName} is a loyal customer with {customer.BaseLoyaltyPoints} points"
					: $"{customer.CustomerName} is not yet a loyal customer. Current points: {customer.BaseLoyaltyPoints}",
				new
				{
					customer.CustomerCode,
					customer.CustomerName,
					customer.BaseLoyaltyPoints, 
					IsLoyalCustomer = isLoyal
				});
		}

	}

	internal class StationData
	{
		public string StationCode { get; set; } = string.Empty;
		public string StationName { get; set;} = string.Empty;
	}
}
