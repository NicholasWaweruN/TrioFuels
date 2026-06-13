using BusinessLogic.EmailService;
using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.Personal_Wallet;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using UsageBalanceDto = DataAccessLayer.DTOs.Sales.UsageBalanceDto;


namespace BussinessLogic.Sales.MissingSales
{
	public class MisingSale : IMisingSale
	{
		private readonly IMemoryCache _cache;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSalesTasks  _salesTasks;
		private readonly IMessagingService _isTalking;

		public MisingSale(OTOContext context, ICommonSetups setups, IAuthCommonTasks authentication, ICommonSalesTasks salesTasks, IMessagingService isTalking, IMemoryCache cache)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_salesTasks = salesTasks;
			_isTalking = isTalking;
			_cache = cache;
		}

		// State for the current operation
		private decimal _unitPrice = 0m;
		private string _saleId = string.Empty;
		private string _storeNumber = string.Empty;
		private string _stationCode = string.Empty;
		private string _stationName = string.Empty;
		private decimal _discount = 0;
		private decimal _originalPrice = 0;

		// ===== PUBLIC ENTRYPOINT =====
		public async Task<ServiceResponse<object>> AddSalesAsync(MisingSaleDto sales)
		{
			try
			{
				_saleId = await GenerateUniqueSaleIdAsync();

				var coreValidation = await ValidateCoreEntitiesAsync(sales);
				if (coreValidation.ResponseCode != Response.Success)
					return coreValidation;

				await LoadStationContextAsync(sales.DispenserCode);

				// Load vehicle context only for methods that require it
				var paymentType = sales.PaymentTypeCode;
				Vehicle? vehicle = null;
				if (paymentType != 3 && paymentType != 4 && paymentType != 5 && paymentType != 6 && paymentType != 8 && paymentType != 10)
				{
					vehicle = await GetVehicleAsync(sales.VehicleCode);
					if (vehicle == null || string.IsNullOrWhiteSpace(vehicle.VehicleRegistration))
						return ServiceResponse<object>.Information("Vehicle not found", null);
					if (string.IsNullOrWhiteSpace(vehicle.ProductCode))
						return ServiceResponse<object>.Information("Vehicle has no associated product", null);
				}

				// Resolve price once
				var priceResolution = await ResolveUnitPriceAsync(sales, vehicle ?? new Vehicle());
				if (priceResolution.ResponseCode != Response.Success)
					return priceResolution;

				// Route
				return await RoutePaymentAsync(sales, vehicle ?? new Vehicle());
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An unexpected error occurred while adding the sale.", ex.Message);
			}
		}

		// ====== CENTRALIZED PRICE LOGIC ======




		private async Task<ServiceResponse<object>> ResolveUnitPriceAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			var prices = await GetStationPricesAsync(_stationCode);
			var productPrice = prices
				.Where(p => p.ProductCode == vehicle.ProductCode)
				.Select(p => p.Price)
				.FirstOrDefault();

			// if user provided a price differing from station price, require approval
			if (productPrice != 0 && sales.Price != 0 && productPrice != sales.Price)
			{
				var (IsValid, Issue) = await HasValidPriceApprovalAsync(vehicle.VehicleRegistration, sales.Price, sales.ShiftNumber,sales.Quantity);
				if (!IsValid)
					return ServiceResponse<object>.Information(Issue, null);

				_unitPrice = (decimal)sales.Price;
				await ConsumePriceApprovalAsync(vehicle.VehicleRegistration, _unitPrice, sales.ShiftNumber);
				return ServiceResponse<object>.Success("Price resolved via approval", null);
			}

			_discount  = await GetDiscount(vehicle.ProductCode);

			// normal path
			if (productPrice == 0)
			{
				_unitPrice = await GetEmployeeFallbackPriceAsync(_stationCode);
			}
			else
			{
				_unitPrice = productPrice;
			}

			if (_unitPrice == 0)
				return ServiceResponse<object>.Information("Kindly check the station pricing or if the vehicle exists", null);
			_originalPrice = _unitPrice;
			_unitPrice -= _discount;
			return ServiceResponse<object>.Success("Price resolved", null);
		}

		// ─────────────────────────────────────────────
		// Returns the highest configured discount for a product code,
		// or 0 if no Prices row exists for that product (MaxAsync throws
		// on an empty sequence, so we project to a nullable decimal first).
		// ─────────────────────────────────────────────
		private async Task<decimal> GetDiscount(string productCode)
		{
			return await _context.Prices
				.Where(d => d.ProductCode == productCode)
				.MaxAsync(d => (decimal?)d.Discount) ?? 0m;
		}


		private async Task<(bool IsValid, string Issue)> HasValidPriceApprovalAsync(string vehicleRegistration,decimal proposedPrice,string shiftNumber,decimal quantity)
		{
			var approval = await _context.PriceApproval.Where(p => p.NumberPlate == vehicleRegistration).OrderByDescending(p => p.Id).FirstOrDefaultAsync();


			if (approval == null)
				return (false, "No approval record found for this vehicle");

			if (approval.ProposedPrice != proposedPrice)
				return (false, $"Proposed price mismatch. Expected: {approval.ProposedPrice}, Got: {proposedPrice}");

			if (approval.IsApprovalExecuted)
				return (false, "Approval already executed");

			if (!approval.IsApproved)
				return (false, "Approval not granted");

			if (approval.ShiftNumber != shiftNumber)
				return (false, $"Shift number mismatch. Expected: {approval.ShiftNumber}, Got: {shiftNumber}");

			if(approval.Quantity < quantity)
				return (false, $"Approved quantity exceeded. Approved: {approval.Quantity}, Requested: {quantity}");	
			return (true, "Approval is valid");
		}


		private async Task ConsumePriceApprovalAsync(string vehicleRegistration, decimal proposedPrice, string shiftNumber)
		{
			var approval = await _context.PriceApproval.FirstOrDefaultAsync(p =>
				p.NumberPlate == vehicleRegistration &&
				p.ProposedPrice == proposedPrice &&
				p.IsApproved == true &&
				p.IsApprovalExecuted == false &&
				p.ShiftNumber == shiftNumber);

			if (approval != null)
			{
				approval.IsApprovalExecuted = true;
				_context.PriceApproval.Update(approval);
				await _context.SaveChangesAsync();
			}
		}

		private async Task<decimal> GetEmployeeFallbackPriceAsync(string stationCode)
		{
			return await _context.Prices
				.Where(x => x.StationCode == stationCode && x.ProductCode == "02")
				.Select(x => (decimal?)x.Amount)
				.MaxAsync() ?? 0m;
		}

		// ====== PAYMENT ROUTING ======
		private async Task<ServiceResponse<object>> RoutePaymentAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await HandleMpesaAsync(sales, vehicle),
				PaymetMethod.Wallet => await HandleWalletAsync(sales, vehicle),
				PaymetMethod.New_Conversions => await HandleNewConversionsAsync(sales, vehicle),
				PaymetMethod.Operational_Loss => await HandleOperationalLossAsync(sales),
				PaymetMethod.Bank_Transfer => await HandleBankTransferAsync(sales),
				PaymetMethod.Employee_Mpesa_Payments => await HandleEmployeeMpesaAsync(sales),
				PaymetMethod.Insurance => await HandleInsuranceAsync(sales),
				PaymetMethod.Voucher => await HandleVoucherAsync(sales, vehicle),
				PaymetMethod.Calibration => await HandleCalibrationAsync(sales),
				PaymetMethod.Compesation_Fuel => await HandleCompensationFuelAsync(sales, vehicle),
				PaymetMethod.BatchVoucher => await HandleBatchVoucherAsync(sales),
				PaymetMethod.Personal_Wallet => await HandlePersonalWalletAsync(sales),
				_ => ServiceResponse<object>.Information("Invalid payment type", null)
			};
		}

		// ====== SMALL PAYMENT HANDLERS (all with detailed audit trails) ======
		private async Task<ServiceResponse<object>> HandleVoucherAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			var firstRef = sales.PaymentDetails.FirstOrDefault()?.TransactionReference ?? "";
			var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherNo == firstRef);
			if (voucher == null) return ServiceResponse<object>.Information("Invalid voucher number", null);
			if (voucher.IsUsed) return ServiceResponse<object>.Information("Voucher has already been used", null);

			var totalAmount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			if (voucher.Amount != totalAmount) return ServiceResponse<object>.Information("Voucher amount must be used in full", null);
			if (voucher.ExpiryDate.Date < DateTime.UtcNow.Date) return ServiceResponse<object>.Information("This voucher has expired", null);
			if (!voucher.VehicleCode.Equals(sales.VehicleCode)) return ServiceResponse<object>.Information("This voucher cannot be used by another vehicle", null);

			voucher.IsUsed = true;
			_context.Vouchers.Update(voucher);
			await _context.SaveChangesAsync();

			await SaveTransactionDataAsync(sales, totalAmount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a VOUCHER sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleVoucherAsync));
			return ServiceResponse<object>.Success("Voucher sale completed successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleBankTransferAsync(MisingSaleDto sales)
		{
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a BANK TRANSFER sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | AttendantUserCode={sales.VehicleCode}";
			await _authentication.AddUserTrail(msg, nameof(HandleBankTransferAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleCompensationFuelAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			if (string.IsNullOrWhiteSpace(vehicle.VehicleRegistration))
				return ServiceResponse<object>.Information("Vehicle does not exist", null);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, vehicle.VehicleRegistration, sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a COMPENSATION FUEL sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleCompensationFuelAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleOperationalLossAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			// If you must force product "02"
			_unitPrice = await GetSpecificProductPriceAsync("02") ?? _unitPrice;

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} recorded an OPERATIONAL LOSS | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | AttendantUserCode={sales.VehicleCode}";
			await _authentication.AddUserTrail(msg, nameof(HandleOperationalLossAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleBatchVoucherAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);
			if (!await EmployeeExist(sales.VehicleCode)) return ServiceResponse<object>.Information("Employee does not exist", null);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a BATCH VOUCHER sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | AttendantUserCode={sales.VehicleCode}";
			await _authentication.AddUserTrail(msg, nameof(HandleBatchVoucherAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleNewConversionsAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			if (string.IsNullOrWhiteSpace(vehicle.VehicleRegistration))
				return ServiceResponse<object>.Information("Vehicle does not exist", null);

			var isValid = await ValidateCoreEntitiesAsync(sales);
			if (isValid.ResponseCode != Response.Success) return isValid;

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, vehicle.VehicleRegistration, sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a NEW CONVERSIONS sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleNewConversionsAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleEmployeeMpesaAsync(MisingSaleDto sales)
		{
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			var price = await GetEmployeeFallbackPriceAsync(_stationCode);

			var ok = await ValidateCoreEntitiesAsync(sales);
			if (ok.ResponseCode != Response.Success) return ok;

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed an EMPLOYEE MPESA sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | AttendantUserCode={sales.NozzleCode}";
			await _authentication.AddUserTrail(msg, nameof(HandleEmployeeMpesaAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleCalibrationAsync(MisingSaleDto sales)
		{
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed a CALIBRATION entry | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleCalibrationAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleInsuranceAsync(MisingSaleDto sales)
		{
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed an INSURANCE sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleInsuranceAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleMpesaAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			await using var tx = await _context.Database.BeginTransactionAsync();
			try
			{
				if (!ValidateSalesBasics(sales, out var invalid)) return invalid;

				var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

				var totalMpesaAvailable = await ValidateAndCalculateMpesaPaymentsAsync(sales.PaymentDetails);
				if (totalMpesaAvailable < saleTotal)
					return ServiceResponse<object>.Information("Insufficient MPesa funds to complete this sale", null);

				await SaveTransactionDataAsync(sales, saleTotal); // writes quantity + capped payments
				await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

				var details = BuildAuditDetails(sales, vehicle.VehicleRegistration, sales.PaymentDetails.Select(p => p.TransactionReference));
				var msg = $"{_authentication.Name()} completed an MPESA sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
				await _authentication.AddUserTrail(msg, nameof(HandleMpesaAsync));

				await tx.CommitAsync();
				return ServiceResponse<object>.Success("Sales made successfully", null);
			}
			catch (Exception ex)
			{
				await tx.RollbackAsync();
				return ServiceResponse<object>.Error($"Payment rolled back: {ex.Message}", null);
			}
		}

		private async Task<ServiceResponse<object>> HandleWalletAsync(MisingSaleDto sales, Vehicle vehicle)
		{
			var vehicleInfo = await GetVehicleAsync(sales.VehicleCode);
			decimal limit = vehicleInfo.CreditLimit;

			var customer = await GetCustomerDetails(sales.VehicleCode);
			var customerCreditBalance = await CustomerLimit(customer.CustomerCode);
			var walletBalance = await GetCustomerBalance(sales.VehicleCode);

			var available = walletBalance + limit + customerCreditBalance.ResponseObject;
			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);

			if (available < amount) return ServiceResponse<object>.Information("Insufficient balance", null);

			await AddCustomerTransactionAsync(sales.VehicleCode, amount, sales.DispenserCode);
			await SaveTransactionDataAsync(sales, amount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var newBalance = await GetCustomerBalance(sales.VehicleCode);
			var firstName = customer.CustomerName.Split(' ')[0];
			var narration = limit >= 0 && newBalance < 0 ? $" Your credit balance is {limit - newBalance:N2}" : $" New balance is {newBalance:N2}";
			var sms = $"Dear {firstName}, {amount:N2} ksh has been deducted from the wallet for purchase of {sales.Quantity:N2} liters of gas, from {_setups.SentenceCase(_stationName)}.{narration} ksh. Thank you for shopping with us";
			await _isTalking.SendSmsAsync(customer.CustomerPhone, sms);

			var details = BuildAuditDetails(sales, vehicle.VehicleRegistration, sales.PaymentDetails.Select(p => p.TransactionReference));
			var audit = $"{_authentication.Name()} completed a WALLET sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | NewBalance={newBalance:N2} | Limit={limit:N2} | AvailableBefore={available:N2}";
			await _authentication.AddUserTrail(audit, nameof(HandleWalletAsync));

			_context.Messages.Add(new Messages
			{
				Message = sms,
				PhoneNumber = customer.CustomerPhone,
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
			});
			await _context.SaveChangesAsync();

			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandlePersonalWalletAsync(MisingSaleDto sales)
		{
			if (sales.WalletId is null)
				return ServiceResponse<object>.Information("Kindly provide walletid", null);
			if (sales.PaymentDetails == null || sales.PaymentDetails.Count == 0)
				return ServiceResponse<object>.Information("No payment details provided", null);

			var totalAmount = sales.PaymentDetails.Sum(x => x.TransactionAmount);

			var customer = await GetCustomerDetails(sales.VehicleCode);
			var balance = await GetPersonalBalance(sales.WalletId);
			if (balance < totalAmount)
				return ServiceResponse<object>.Information("Insufficient balance in the personal wallet", null);

			await AddPersonalWalletTransactionAsync(sales.VehicleCode, totalAmount, sales.DispenserCode, sales.WalletId);

			await SaveTransactionDataAsync(sales, totalAmount);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var newBalance = await GetPersonalBalance(sales.WalletId);
			var firstName = customer.CustomerName.Split(' ')[0];
			var sms = $"Dear {firstName}, {totalAmount:N2} Ksh has been deducted from the wallet for purchase of {sales.Quantity:N2} liters of gas, from {_setups.SentenceCase(_stationName)}. Thank you for choosing us.";
			await _isTalking.SendSmsAsync(customer.CustomerPhone, sms);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var trail = $"{_authentication.Name()} completed a PERSONAL WALLET sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | NewBalance={newBalance:N2} | WalletId={sales.WalletId}";
			await _authentication.AddUserTrail(trail, nameof(HandlePersonalWalletAsync));

			_context.Messages.Add(new Messages
			{
				Message = sms,
				PhoneNumber = customer.CustomerPhone,
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
			});
			await _context.SaveChangesAsync();

			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		// ======== SUPPORTING HELPERS ========
		private async Task<string> GenerateUniqueSaleIdAsync()
		{
			string id;
			do
			{
				id = _setups.GenerateSaleId();
			} while (await _context.QuantityTransactions.AnyAsync(x => x.SaleId == id));
			return id;
		}

		private async Task<ServiceResponse<object>> ValidateCoreEntitiesAsync(MisingSaleDto sales)
		{
			var shiftExist = await _context.Shifts.AnyAsync(x => x.ShiftNumber == sales.ShiftNumber);
			if (!shiftExist) return ServiceResponse<object>.Information("Shift does not exist", null);

			var nozzleExist = await _context.Nozzles.AnyAsync(x => x.NozzleCode == sales.NozzleCode);
			if (!nozzleExist) return ServiceResponse<object>.Information("Nozzle does not exist", null);

			var paymentTypeExist = await _context.PaymentTypes.AnyAsync(x => x.PaymentTypeId == sales.PaymentTypeCode);
			if (!paymentTypeExist) return ServiceResponse<object>.Information("Payment type does not exist", null);

			var dispenserExist = await _context.Dispensers.AnyAsync(x => x.DispenserCode == sales.DispenserCode);
			if (!dispenserExist) return ServiceResponse<object>.Information("Dispenser does not exist", null);

			return ServiceResponse<object>.Success("Data is valid", null);
		}

		private async Task LoadStationContextAsync(string dispenserCode)
		{
			var station = await StationsName(dispenserCode);
			if (string.IsNullOrWhiteSpace(station.StationCode))
				throw new InvalidOperationException("Invalid dispenser/station mapping.");

			_stationCode = station.StationCode;
			_stationName = station.StationName;
			_storeNumber = await StoreNumber(dispenserCode);
		}

		private static bool ValidateSalesBasics(MisingSaleDto sales, out ServiceResponse<object> response)
		{
			response = ServiceResponse<object>.Information("Invalid sales data", null);
			if (sales == null || sales.PaymentDetails == null || sales.PaymentDetails.Count == 0 || string.IsNullOrEmpty(sales.VehicleCode))
				return false;
			return true;
		}

		private bool ValidateComputedAmount(decimal quantity, decimal transactionAmount)
		{
			var expected = Math.Floor(quantity * _unitPrice);
			var tolerance = 0.50m;
			return transactionAmount + tolerance >= expected;
		}

		private async Task SavePaymentTransactionsAsync(MisingSaleDto sales, decimal saleTotal)
		{
			decimal remaining = Math.Floor(saleTotal);

			foreach (var pd in sales.PaymentDetails)
			{
				if (remaining <= 0) break;

				decimal toApply = Math.Min(pd.TransactionAmount, remaining);

				if (!string.IsNullOrWhiteSpace(pd.TransactionReference))
				{
					var available = await GetUsageBalanceAsync(pd.TransactionReference) ?? 0;
					if (available <= 0) continue;

					toApply = Math.Min(toApply, available);

					var consumeInt = (int)Math.Floor(toApply);
					if (consumeInt > 0)
					{
						await ConsumeMpesaAsync(pd.TransactionReference, consumeInt);
						toApply = consumeInt; // align to actual consumed
					}
					else
					{
						continue;
					}
				}

				if (toApply <= 0) continue;

				var reference = string.IsNullOrWhiteSpace(pd.TransactionReference)
					? _setups.GenerateSaleId()
					: pd.TransactionReference;

				_context.PaymentTransactions.Add(new PaymentTransactions
				{
					PaymentRefrence = reference,
					TransactionAmount = toApply,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					SaleId = _saleId,
					TransactionAmountDebit = 0
				});

				remaining -= toApply;
			}

			await _context.SaveChangesAsync();
		}

		private async Task SaveTransactionDataAsync(MisingSaleDto sales, decimal _ /*unused*/)
		{
			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

			_context.QuantityTransactions.Add(new QuantityTransactions
			{
				ShiftNumber = sales.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleCode = sales.VehicleCode,
				QuantityCredit = sales.Quantity,
				QuantityDebit = 0,
				DispenserCode = sales.DispenserCode,
				NozzleCode = sales.NozzleCode,
				AmountCredit = saleTotal,
				AmountDebit = 0,
				DateCreated = DateTime.UtcNow,
				IsReversed = false,
				PaymentTypeCode = sales.PaymentTypeCode,
				SaleId = _saleId,
				Price = _originalPrice,
				StationCode = _stationCode,
				Discount = _discount,
				OtpUsed = string.Empty,
				Vat_Amount = 0
			});
			await _context.SaveChangesAsync();
			await SavePaymentTransactionsAsync(sales, saleTotal);
		}

		// ====== MPESA VALIDATION / PARTIAL USAGE ======
		private async Task<ServiceResponse<int?>> ValidateMpesaPaymentAsync(string transId)
		{
			try
			{
				var mpesaAmount = await GetUsageBalanceAsync(transId);
				if (mpesaAmount == null || mpesaAmount <= 0)
				{
					string message = mpesaAmount == null
						? $"M-Pesa code {transId} does not exist."
						: $"Amount fully used for code {transId}.";
					return ServiceResponse<int?>.Information(message, mpesaAmount);
				}
				return ServiceResponse<int?>.Success($"Valid Mpesa Code {transId}.", mpesaAmount);
			}
			catch (Exception ex)
			{
				return ServiceResponse<int?>.Error($"An error occurred while validating payment: {ex.Message}", 0);
			}
		}

		private async Task<int?> GetUsageBalanceAsync(string transId)
		{
			const string sql = @"SELECT TOP 1 CAST(UsageBalance as int) AS Amount 
                         FROM Protobase..MpesaC2BPayments 
                         WHERE BusinessShortCode = @p0 AND TransID = @p1";

			var shortCode = _storeNumber;
			return await _context.Set<UsageBalanceDto>()
				.FromSqlRaw(sql, shortCode, transId)
				.Select(result => result.Amount)
				.FirstOrDefaultAsync();
		}

		private async Task<int> ConsumeMpesaAsync(string transId, int amountToConsume)
		{
			const string sql = @"
				UPDATE Protobase..MpesaC2BPayments
				SET UsageBalance = CASE
					WHEN UsageBalance >= @p2 THEN UsageBalance - @p2
					ELSE 0 END
				WHERE BusinessShortCode = @p0 AND TransID = @p1";

			var shortCode = _storeNumber;
			return await _context.Database.ExecuteSqlRawAsync(sql, shortCode, transId, amountToConsume);
		}

		private async Task<int> ValidateAndCalculateMpesaPaymentsAsync(IEnumerable<PaymentDetails> paymentDetails)
		{
			int total = 0;
			foreach (var p in paymentDetails.Where(p => !string.IsNullOrWhiteSpace(p.TransactionReference)))
			{
				var validation = await ValidateMpesaPaymentAsync(p.TransactionReference);
				if (validation.ResponseCode == Response.Success && validation.ResponseObject.HasValue)
				{
					total += validation.ResponseObject.Value;
				}
			}
			return total;
		}

		// ====== LOOKUPS / QUERIES ======
		private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
		{
			return await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => new Vehicle
				{
					ProductCode = v.ProductCode,
					VehicleRegistration = v.VehicleRegistrationNumber,
					CreditLimit = v.CreditLimit,
					CustomerCode = v.CustomerCode
				})
				.FirstOrDefaultAsync() ?? new Vehicle();
		}
		public class Vehicle
		{
			public string ProductCode { get; set; } = string.Empty;
			public string VehicleRegistration { get; set; } = string.Empty;
			[Precision(18, 2)] public decimal CreditLimit { get; set; }
			public string CustomerCode { get; set; } = string.Empty;
		}

		private async Task<List<ThePrices>> GetStationPricesAsync(string stationCode)
		{
			return await _context.Prices
				.Where(p => p.StationCode == stationCode)
				.Select(p => new ThePrices
				{
					ProductCode = p.ProductCode,
					Price = p.Amount
				})
				.ToListAsync();
		}

		private async Task<decimal?> GetSpecificProductPriceAsync(string productCode)
		{
			return await _context.Prices
				.Where(p => p.StationCode == _stationCode && p.ProductCode == productCode)
				.Select(p => (decimal?)p.Amount)
				.FirstOrDefaultAsync();
		}

		private async Task<Customer> GetCustomerDetails(string vehicleCode)
		{
			var customer = await (from c in _context.Customers
								  join v in _context.Vehicles on c.CustomerCode equals v.CustomerCode
								  where v.VehicleCode == vehicleCode
								  select new Customer
								  {
									  CustomerName = c.CustomerName,
									  CustomerPhone = c.CustomerPhone,
									  CustomerEmail = c.CustomerEmail,
									  CustomerCode = c.CustomerCode
								  }).FirstOrDefaultAsync();
			return customer ?? new Customer();
		}

		private async Task<decimal> GetCustomerBalance(string vehicleCode)
		{
			return await _context.CustomerTransactions
				.Where(x => x.VehicleCode.Equals(vehicleCode))
				.SumAsync(x => x.Credit - x.Debit);
		}

		private async Task<decimal> GetPersonalBalance(string walletId)
		{
			return await _context.Wallet_Transactions_Personal
				.Where(x => x.WalletId.Equals(walletId))
				.SumAsync(x => x.Credit - x.Debit);
		}

		private async Task AddCustomerTransactionAsync(string vehicleCode, decimal debitAmount, string dispenserCode)
		{
			var station = await StationsName(dispenserCode);
			var row = new CustomerTransactions
			{
				VehicleCode = vehicleCode,
				Credit = 0,
				Debit = debitAmount,
				DateCreated = DateTime.UtcNow,
				TransactionReference = _saleId,
				UserReference = _saleId,
				UserCode = _authentication.Usercode(),
				Narration = $"Fueled at {station.StationName} station"
			};
			_context.CustomerTransactions.Add(row);
			await _context.SaveChangesAsync();
		}

		private async Task AddPersonalWalletTransactionAsync(string vehicleCode, decimal debitAmount, string dispenserCode, string walletId)
		{
			var phoneNumber = await _context.Wallet_Transactions_Personal
				.Where(p => p.WalletId.Equals(walletId))
				.Select(p => p.PhoneNumber)
				.FirstOrDefaultAsync();

			var station = await StationsName(dispenserCode);
			var row = new Wallet_Transactions_Personal
			{
				VehicleCode = vehicleCode,
				Credit = 0,
				Debit = debitAmount,
				DateCreated = DateTime.UtcNow,
				SaleId = _saleId,
				UserCode = _authentication.Usercode(),
				Description = $"Fueled at {station.StationName} station",
				PhoneNumber = phoneNumber ?? "",
				TransactionCode = _saleId,
				TransactionType = "",
				WalletId = walletId,
			};
			_context.Wallet_Transactions_Personal.Add(row);
			await _context.SaveChangesAsync();
		}

		public async Task<StationData> StationsName(string dispenserCode)
		{
			var stationName = await (from s in _context.Stations
									 join d in _context.Dispensers on s.StationCode equals d.StationCode
									 where d.DispenserCode == dispenserCode
									 select new
									 {
										 s.StationCode,
										 s.StationName,
									 }).FirstOrDefaultAsync();
			if (stationName == null) return new StationData();

			return new StationData
			{
				StationCode = stationName.StationCode,
				StationName = stationName.StationName
			};
		}

		public async Task<string> StoreNumber(string dispenserCode)
		{
			var number = await (from s in _context.Dispensers
								join t in _context.Tills on s.TillNumber equals t.TillNumber
								where s.DispenserCode == dispenserCode
								select t.StoreNumber).FirstOrDefaultAsync();
			return number ?? string.Empty;
		}

		private async Task<bool> EmployeeExist(string userCode)
		{
			return await _context.Users.AnyAsync(u => u.UserCode == userCode);
		}

		// ====== Variance Methods ======
		public async Task<ServiceResponse> DeferVariance(string shiftNumber)
		{
			var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber);
			var summaries = await _context.StockTakeSummaries.Where(s => s.ShiftNumber == shiftNumber).ToListAsync();

			if (shift is not null && summaries is not null)
			{
				shift.ShiftStatus = ShiftStatus.Pending;
				_context.Update(shift);

				foreach (var s in summaries)
				{
					s.VarianceStatus = ShiftStatus.Pending;
					_context.Update(s);
				}
				await _context.SaveChangesAsync();

				var msg = $"Variance of shift {shift.ShiftNumber} has been deferred until next shift by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(msg, nameof(DeferVariance));
				return ServiceResponse<object>.Success("Variance has been deferred until next shift");
			}
			return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
		}

		public async Task<ServiceResponse> OffWriteVariance(string shiftNumber)
		{
			var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber);
			var summaries = await _context.StockTakeSummaries.Where(s => s.ShiftNumber == shiftNumber).ToListAsync();

			if (shift is not null && summaries is not null)
			{
				shift.ShiftStatus = ShiftStatus.Closed;
				_context.Update(shift);

				foreach (var s in summaries)
				{
					s.VarianceStatus = ShiftStatus.Closed;
					s.ClosingVariance = 0;
					s.OpeningVariance = 0;
					_context.Update(s);
				}
				await _context.SaveChangesAsync();

				var msg = $"Variance written off for shift {shift.ShiftNumber} by {_authentication.Name()} on {DateTime.UtcNow}";
				return ServiceResponse<object>.Success(msg);
			}
			return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
		}

		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			return await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
		}

		private async Task<ServiceResponse<decimal>> CustomerLimit(string customerCode)
		{
			var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerCode == customerCode);
			if (customer is null) return ServiceResponse<decimal>.Information("Customer does not exist", 0);
			var balance = customer.CreditLimit;
			return ServiceResponse<decimal>.Success($"Customer balance is {balance}", balance);
		}

		// ====== Excel report (kept) ======
		public async Task<ServiceResponse<byte[]>> WalletTopUps(DateTime dateFrom, DateTime dateTo)
		{
			try
			{
				var topUps = await (from wallet in _context.CustomerTransactions.AsNoTracking()
									join vehicle in _context.Vehicles.AsNoTracking() on wallet.VehicleCode equals vehicle.VehicleCode
									join customer in _context.Customers.AsNoTracking() on vehicle.CustomerCode equals customer.CustomerCode
									join user in _context.Users.AsNoTracking() on wallet.UserCode equals user.UserCode into userJoin
									from user in userJoin.DefaultIfEmpty()
									join ttypes in _context.TopUpTypes.AsNoTracking() on wallet.TopUpType equals ttypes.TopUpType
									where wallet.DateCreated.Date >= dateFrom && wallet.DateCreated.Date <= dateTo && wallet.Credit > 0
									select new
									{
										customer.CustomerName,
										customer.CustomerPhone,
										vehicle.VehicleRegistrationNumber,
										wallet.DateCreated,
										wallet.Credit,
										wallet.TransactionReference,
										TopUpMode = ttypes.TopUpDescription,
										TopUpDescription = ttypes.TopUpType == 4 ? ttypes.TopUpDescription : "Top Up",
										AddedBy = user == null ? "MPESA PAYBILL" : string.Join(' ', new object[] { user.FirstName, user.MiddName ?? string.Empty, user.LastName })
									}).ToListAsync();

				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add("Wallet TopUps");

				worksheet.Cell(1, 1).Value = "Customer Name";
				worksheet.Cell(1, 2).Value = "Customer Phone";
				worksheet.Cell(1, 3).Value = "Vehicle Registration Number";
				worksheet.Cell(1, 4).Value = "Date Created";
				worksheet.Cell(1, 5).Value = "Credit";
				worksheet.Cell(1, 6).Value = "Transaction Reference";
				worksheet.Cell(1, 7).Value = "Added By";
				worksheet.Cell(1, 8).Value = "TopUp Mode";
				worksheet.Cell(1, 9).Value = "TopUp Description";

				for (int i = 0; i < topUps.Count; i++)
				{
					worksheet.Cell(i + 2, 1).Value = topUps[i].CustomerName;
					worksheet.Cell(i + 2, 2).Value = topUps[i].CustomerPhone;
					worksheet.Cell(i + 2, 3).Value = topUps[i].VehicleRegistrationNumber;
					worksheet.Cell(i + 2, 4).Value = topUps[i].DateCreated.ToString("yyyy-MM-dd-hh-mm-ss");
					worksheet.Cell(i + 2, 5).Value = topUps[i].Credit;
					worksheet.Cell(i + 2, 6).Value = topUps[i].TransactionReference;
					worksheet.Cell(i + 2, 7).Value = topUps[i].AddedBy;
					worksheet.Cell(i + 2, 8).Value = topUps[i].TopUpMode;
					worksheet.Cell(i + 2, 9).Value = topUps[i].TopUpDescription;
				}

				var range = worksheet.Range(1, 1, topUps.Count + 1, 9);
				var table = range.CreateTable();
				table.Theme = XLTableTheme.TableStyleLight9;

				worksheet.Columns().AdjustToContents();

				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				var excelData = stream.ToArray();

				return ServiceResponse<byte[]>.Success("Excel file generated successfully", excelData);
			}
			catch (Exception ex)
			{
				return ServiceResponse<byte[]>.Error($"An error occurred while generating the report: {ex.Message}", null);
			}
		}

		// ====== Inner types ======
		public class StationData
		{
			[StringLength(10)]
			public string StationCode { get; set; } = string.Empty;
			[StringLength(50)]
			public string StationName { get; set; } = string.Empty;
		}
		public class ThePrices
		{
			[Precision(18, 2)] public decimal Price { get; set; }
			public string ProductCode { get; set; } = string.Empty;
		}

		// ====== Audit detail builder (centralized, consistent) ======
		private string BuildAuditDetails(MisingSaleDto sales, string? vehicleReg = null, IEnumerable<string>? paymentRefs = null)
		{

			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);
			var paidEntered = sales.PaymentDetails.Sum(p => p.TransactionAmount);
			var refs = paymentRefs == null ? "" : string.Join(", ", paymentRefs.Where(r => !string.IsNullOrWhiteSpace(r)));
			return $"Qty={sales.Quantity:N2}L | UnitPrice={_unitPrice:N2} | SaleTotal={saleTotal:N2} | EnteredPayTotal={paidEntered:N2} | Shift={sales.ShiftNumber} | Dispenser={sales.DispenserCode} | Nozzle={sales.NozzleCode} | Vehicle={vehicleReg ?? sales.VehicleCode} | PaymentRefs=[{refs}] | When={DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}";
		}

		//validate voucher pass voucherNo
		public async Task<ServiceResponse<object>> ValidateVoucherAsync(string voucherNo)
		{
			if (string.IsNullOrWhiteSpace(voucherNo))
				return ServiceResponse<object>.Information("Voucher number cannot be empty", null);

			var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherNo == voucherNo);
			if (voucher == null)
				return ServiceResponse<object>.Information("Voucher does not exist", null);

			if (voucher.IsUsed)
				return ServiceResponse<object>.Information("Voucher has already been used", null);

			if (voucher.ExpiryDate < DateTime.UtcNow)
				return ServiceResponse<object>.Information("Voucher has expired", null);

			return ServiceResponse<object>.Success("Voucher is valid", voucher);

		}
	}
}
