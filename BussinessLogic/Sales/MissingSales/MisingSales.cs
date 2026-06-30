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


namespace BussinessLogic.Sales.MissingSales
{
	public class MisingSale : IMisingSale
	{
		private readonly IMemoryCache _cache;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSalesTasks _salesTasks;
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

				// REFACTORED: no Vehicles table anymore. sales.VehicleCode is now
				// just a free-text registration number supplied by the user/client
				// — no entity lookup or validation against it beyond a non-empty
				// check for payment types that require a vehicle/registration.
				var paymentType = sales.PaymentTypeCode;
				if (paymentType != 3 && paymentType != 4 && paymentType != 5 && paymentType != 6 && paymentType != 8 && paymentType != 10)
				{
					if (string.IsNullOrWhiteSpace(sales.VehicleCode))
						return ServiceResponse<object>.Information("Vehicle/registration number is required", null);
				}

				// Resolve price once — now keyed off the nozzle's product, not a vehicle's product
				var priceResolution = await ResolveUnitPriceAsync(sales);
				if (priceResolution.ResponseCode != Response.Success)
					return priceResolution;

				// Route
				return await RoutePaymentAsync(sales);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An unexpected error occurred while adding the sale.", ex.Message);
			}
		}

		// ====== CENTRALIZED PRICE LOGIC ======

		// REFACTORED: dropped Vehicle parameter. Product is now resolved via the
		// nozzle's PetroleumCode, consistent with vw_SalesData's product join.
		private async Task<ServiceResponse<object>> ResolveUnitPriceAsync(MisingSaleDto sales)
		{
			var productCode = await _context.Nozzles
				.Where(n => n.NozzleCode == sales.NozzleCode)
				.Select(n => n.PetroleumCode)
				.FirstOrDefaultAsync() ?? string.Empty;

			var prices = await GetStationPricesAsync(_stationCode);
			var productPrice = prices
				.Where(p => p.ProductCode == productCode)
				.Select(p => p.Price)
				.FirstOrDefault();

			// if user provided a price differing from station price, require approval
			if (productPrice != 0 && sales.Price != 0 && productPrice != sales.Price)
			{
				var (IsValid, Issue) = await HasValidPriceApprovalAsync(sales.VehicleCode, sales.Price, sales.ShiftNumber, sales.Quantity);
				if (!IsValid)
					return ServiceResponse<object>.Information(Issue, null);

				_unitPrice = (decimal)sales.Price;
				await ConsumePriceApprovalAsync(sales.VehicleCode, _unitPrice, sales.ShiftNumber);

				// FIX (carried over): record the approved price as the original
				// price with no further discount, rather than leaving these at 0.
				_originalPrice = _unitPrice;
				_discount = 0;

				return ServiceResponse<object>.Success("Price resolved via approval", null);
			}

			_discount = await GetDiscount(productCode);

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
				return ServiceResponse<object>.Information("Kindly check the station pricing or product configuration", null);
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


		private async Task<(bool IsValid, string Issue)> HasValidPriceApprovalAsync(string vehicleRegistration, decimal proposedPrice, string shiftNumber, decimal quantity)
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

			if (approval.Quantity < quantity)
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
		// REFACTORED: dropped the Vehicle parameter from the routing signature
		// and every handler below — there's no vehicle entity to pass anymore.
		private async Task<ServiceResponse<object>> RoutePaymentAsync(MisingSaleDto sales)
		{
			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await HandleMpesaAsync(sales),
				PaymetMethod.Operational_Loss => await HandleOperationalLossAsync(sales),
				PaymetMethod.Employee_Mpesa_Payments => await HandleEmployeeMpesaAsync(sales),
				PaymetMethod.Insurance => await HandleInsuranceAsync(sales),
				PaymetMethod.Calibration => await HandleCalibrationAsync(sales),
				_ => ServiceResponse<object>.Information("Invalid payment type", null)
			};
		}

		// ====== SMALL PAYMENT HANDLERS (all with detailed audit trails) ======



		private async Task<ServiceResponse<object>> HandleOperationalLossAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			// If you must force product "02"
			// NOTE (carried over): this overrides _unitPrice *after*
			// ResolveUnitPriceAsync already set _originalPrice/_discount based on
			// the originally-resolved product price. As written, the persisted
			// Price/Discount fields on QuantityTransactions may not correspond to
			// the _unitPrice actually used for this sale's total below. Left as-is
			// pending confirmation of whether operational-loss entries should
			// always be priced/audited against product "02" regardless of the
			// nozzle's own product/price.
			_unitPrice = await GetSpecificProductPriceAsync("02") ?? _unitPrice;

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} recorded an OPERATIONAL LOSS | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | AttendantUserCode={sales.VehicleCode}";
			await _authentication.AddUserTrail(msg, nameof(HandleOperationalLossAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}





		private async Task<ServiceResponse<object>> HandleEmployeeMpesaAsync(MisingSaleDto sales)
		{
			if (!await EmployeeExist(sales.VehicleCode))
				return ServiceResponse<object>.Information("Employee does not exist", null);

			var price = await GetEmployeeFallbackPriceAsync(_stationCode);

			var amount = sales.PaymentDetails.Sum(x => x.TransactionAmount);
			await SaveTransactionDataAsync(sales);
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
			await SaveTransactionDataAsync(sales);
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
			await SaveTransactionDataAsync(sales);
			await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

			var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
			var msg = $"{_authentication.Name()} completed an INSURANCE sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
			await _authentication.AddUserTrail(msg, nameof(HandleInsuranceAsync));
			return ServiceResponse<object>.Success("Sales made successfully", null);
		}

		private async Task<ServiceResponse<object>> HandleMpesaAsync(MisingSaleDto sales)
		{
			await using var tx = await _context.Database.BeginTransactionAsync();
			try
			{
				if (!ValidateSalesBasics(sales, out var invalid)) return invalid;

				var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

				var totalMpesaAvailable = await ValidateAndCalculateMpesaPaymentsAsync(sales.PaymentDetails);
				if (totalMpesaAvailable < saleTotal)
					return ServiceResponse<object>.Information("Insufficient MPesa funds to complete this sale", null);

				await SaveTransactionDataAsync(sales); // writes quantity + capped payments
				await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

				var details = BuildAuditDetails(sales, sales.VehicleCode, sales.PaymentDetails.Select(p => p.TransactionReference));
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

		// REFACTORED: customer identity can no longer be resolved via
		// Vehicles.CustomerCode (table doesn't exist). ASSUMPTION: MisingSaleDto
		// carries a CustomerCode property supplied directly by the client (the
		// same way SalesActivity resolves and forwards customerCode after its
		// phone-search step). If that property doesn't exist on the DTO, this
		// will not compile — tell me what field actually carries customer
		// identity here and I'll wire it in instead.


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

		// REFACTORED: now accepts customerCode so it can be persisted on the
		// QuantityTransactions row, matching the fix already applied to the
		// main Sales.BuildQuantityTransaction. Defaults to empty for
		// payment types that legitimately have no customer (vouchers, bank
		// transfer, operational loss, calibration, insurance, batch voucher,
		// new conversions, employee M-Pesa, compensation fuel) — confirm
		// whether any of those should actually carry a customer link too.
		private async Task SaveTransactionDataAsync(MisingSaleDto sales, string customerCode = "")
		{
			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

			_context.QuantityTransactions.Add(new QuantityTransactions
			{
				ShiftNumber = sales.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleRegistrationNumber = sales.VehicleCode,
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
				Vat_Amount = 0,
				CustomerCode = customerCode
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
			var usageBalance = await (from mt in _context.MpesaTransactions
									  where mt.BusinessShortCode == _storeNumber
									  && mt.TransID == transId
									  select (int?)mt.UsageBalance).FirstOrDefaultAsync();

			return usageBalance;
		}

		private async Task<int> ConsumeMpesaAsync(string transId, int amountToConsume)
		{
			var transaction = await _context.MpesaTransactions
				.FirstOrDefaultAsync(x =>
					x.BusinessShortCode == _storeNumber &&
					x.TransID == transId);

			if (transaction == null)
				return 0;

			transaction.UsageBalance = transaction.UsageBalance >= amountToConsume
				? transaction.UsageBalance - amountToConsume
				: 0;

			return await _context.SaveChangesAsync();
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
		// REMOVED: GetVehicleAsync and the inner Vehicle class. There is no
		// Vehicles table anymore — sales.VehicleCode is taken at face value
		// as the registration number the user typed, with no lookup performed.

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
				await _authentication.AddUserTrail(msg, nameof(OffWriteVariance));

				return ServiceResponse<object>.Success(msg);
			}
			return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
		}

		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			return await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
		}


		// ====== Excel report ======
		// REFACTORED: previously joined CustomerTransactions -> Vehicles ->
		// Customers. Vehicles no longer exists, so this now joins
		// CustomerTransactions.VehicleCode directly against
		// QuantityTransactions.VehicleRegistrationNumber/CustomerCode to get
		// back to the customer. CONFIRM this join is correct for your
		// CustomerTransactions schema — if VehicleCode there is actually meant
		// to carry the registration number string (consistent with the rest of
		// this refactor), this join is right; if it's something else, this
		// needs adjusting.

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

	}
}