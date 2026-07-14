using BusinessLogic.EmailService;
using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Setup;
using BussinessLogic.Stock.Stock;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

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
		private readonly ILoyaltyServices _loyalty;
		private readonly IStockTakeVarianceService _varianceService;

		public MisingSale(IStockTakeVarianceService varianceService, OTOContext context, ICommonSetups setups, IAuthCommonTasks authentication, ICommonSalesTasks salesTasks, IMessagingService isTalking, IMemoryCache cache,ILoyaltyServices loyalty)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_salesTasks = salesTasks;
			_isTalking = isTalking;
			_cache = cache;
			_loyalty = loyalty;
			_varianceService = varianceService;
		}

		// State for the current operation
		private decimal _unitPrice = 0m;
		private string _saleId = string.Empty;
		private string _tillNumber = string.Empty;
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

				if (sales.PaymentTypeCode == PaymetMethod.Mpesa)
				{
					if (string.IsNullOrWhiteSpace(sales.VehicleRegistrationNumber))
						return ServiceResponse<object>.Information("Vehicle/registration number is required", null);
				}

				// Resolve price once — now keyed directly off sales.ProductCode
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

		private async Task ReconcileAndUpdateUsageBalanceAsync(string transId)
		{
			// Tracked on purpose — this method mutates UsageBalance/Status and
			// explicitly marks the entity Modified below.
			var mpesaTx = await _context.MpesaTransactions
				.FirstOrDefaultAsync(t => t.TransID == transId);

			if (mpesaTx is null)
			{
				Console.WriteLine($"[Reconcile] ❌ MpesaTransaction not found for TransID={transId}");
				return;
			}

			// ── Sum only the amount actually allocated to THIS code, not the
			// ── full sale total — a sale can be split across two M-Pesa codes,
			// ── and each code must only be debited by its own share.
			var totalUsed = await (
				from p in _context.PaymentTransactions.AsNoTracking()
				join q in _context.QuantityTransactions.AsNoTracking() on p.SaleId equals q.SaleId
				where p.PaymentRefrence == transId && !q.IsReversed
				select p.TransactionAmount
			).SumAsync();

			mpesaTx.UsageBalance = Math.Max(0, mpesaTx.TransAmount - totalUsed);
			mpesaTx.Status = mpesaTx.UsageBalance <= 0 ? 0 : 1;
			mpesaTx.DateModified = EatTime.Now;

			_context.Entry(mpesaTx).State = EntityState.Modified;
		}

		// ====== CENTRALIZED PRICE LOGIC ======

		// REFACTORED: price is now resolved solely from sales.ProductCode
		// (sourced from PetroleumProducts on the client) against the station's
		// Prices table. All other price-calculation paths have been removed:
		// no more nozzle→PetroleumCode lookup, no discount, no price-approval
		// workflow, and no employee fallback pricing. The unit price used for
		// the sale IS the station's configured price for that product code —
		// nothing else adjusts it.
		private async Task<ServiceResponse<object>> ResolveUnitPriceAsync(MisingSaleDto sales)
		{
			var productPrice = await _context.Prices
				.Where(p => p.ProductCode == sales.ProductCode)
				.Select(p => (decimal?)p.Amount)
				.FirstOrDefaultAsync() ?? 0m;

			if (productPrice == 0)
				return ServiceResponse<object>.Information("Kindly check the station pricing or product configuration", null);

			_unitPrice = productPrice;
			_originalPrice = productPrice;
			_discount = 0;

			return ServiceResponse<object>.Success("Price resolved", null);
		}

		// ====== PAYMENT ROUTING ======
		// REFACTORED: dropped the Vehicle parameter from the routing signature
		// and every handler below — there's no vehicle entity to pass anymore.
		// REMOVED: Insurance payment type/handler.
		private async Task<ServiceResponse<object>> RoutePaymentAsync(MisingSaleDto sales)
		{
			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await HandleMpesaAsync(sales),
				PaymetMethod.Operational_Loss => await HandleOperationalLossAsync(sales),
				PaymetMethod.Employee_Mpesa_Payments => await HandleEmployeeMpesaAsync(sales),
				PaymetMethod.Calibration => await HandleCalibrationAsync(sales),
				PaymetMethod.Cash => await HandleCashAsync(sales),
				PaymetMethod.PDQ => await HandlePDQAsync(sales),
				PaymetMethod.Credit => await HandleCreditAsync(sales),
				PaymetMethod.Loyalty => await HandleLoyaltyAsync(sales),
				_ => ServiceResponse<object>.Information("Invalid payment type", null)
			};
		}

		// ====== SMALL PAYMENT HANDLERS (all with detailed audit trails) ======

		// FIXED: wrapped in a transaction so the QuantityTransactions insert
		// and PaymentTransactions insert (done inside SaveTransactionDataAsync)
		// commit or roll back together — matches HandleMpesaAsync's pattern.
		// sales.VehicleRegistrationNumber is treated purely as a registration
		// string and persisted as-is, with no lookup against Users or any
		// other table.

		private class CustomerData
		{
			public string CustomerCode = string.Empty;
			public bool IsCreditCustomer;
			public decimal CreditLimit;
		}

		private async Task<CustomerData?> GetCustomerByCodeAsync(string customerCode)
			=> await _context.Customers.AsNoTracking()
				.Where(c => c.CustomerCode == customerCode)
				.Select(c => new CustomerData
				{
					CustomerCode = c.CustomerCode,
					IsCreditCustomer = c.IsCreditCustomer,
					CreditLimit = c.CreditLimit
				})
				.FirstOrDefaultAsync();

		private Task<decimal> GetOutstandingCreditAsync(string customerCode)
			=> _context.CreditTransactions.AsNoTracking()
				.Where(c => c.CustomerCode == customerCode)
				.SumAsync(c => c.Debit - c.Credit);

		private async Task<ServiceResponse<object>> HandleCashAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales, sales.CustomerCode ?? string.Empty);

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);


					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed a CASH SALE | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | VehicleRegistration={sales.VehicleRegistrationNumber}";
					await _authentication.AddUserTrail(msg, nameof(HandleCashAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Cash sale entry rolled back: {ex.Message}", null);
				}
			});
		}

		private async Task<ServiceResponse<object>> HandlePDQAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales, sales.CustomerCode ?? string.Empty);

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);


					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed a PDQ SALE | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | VehicleRegistration={sales.VehicleRegistrationNumber}";
					await _authentication.AddUserTrail(msg, nameof(HandlePDQAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"PDQ sale entry rolled back: {ex.Message}", null);
				}
			});
		}

		private async Task<ServiceResponse<object>> HandleCreditAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);
			if (string.IsNullOrWhiteSpace(sales.CustomerCode))
				return ServiceResponse<object>.Information("Customer code is required for credit sales", null);

			var customer = await GetCustomerByCodeAsync(sales.CustomerCode);
			if (customer is null)
				return ServiceResponse<object>.Information("Customer not found", null);

			if (!customer.IsCreditCustomer)
				return ServiceResponse<object>.Information("This customer is not approved for credit purchases.", null);

			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);
			var outstanding = await GetOutstandingCreditAsync(customer.CustomerCode);
			var newExposure = outstanding + saleTotal;

			if (newExposure > customer.CreditLimit)
				return ServiceResponse<object>.Information($"Credit limit exceeded. Limit: {customer.CreditLimit:N2}, Outstanding: {outstanding:N2}, This sale: {saleTotal:N2}",new { customer.CreditLimit, Outstanding = outstanding });

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales, customer.CustomerCode);

					_context.CreditTransactions.Add(new CreditTransactions
					{
						CustomerCode = customer.CustomerCode,
						Credit = 0,
						Debit = saleTotal,
						SaleId = _saleId,
						TransactionReference = sales.PaymentDetails.FirstOrDefault()?.TransactionReference ?? _saleId,
						VehicleCode = sales.VehicleRegistrationNumber,
						StationCode = _stationCode,
						DateCreated = EatTime.Now,
						UserCode = _authentication.Usercode()
					});
					await _context.SaveChangesAsync();

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);


					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed a CREDIT SALE | SaleID={_saleId} | Station={_stationName}({_stationCode}) | Customer={customer.CustomerCode} | {details} | VehicleRegistration={sales.VehicleRegistrationNumber}";
					await _authentication.AddUserTrail(msg, nameof(HandleCreditAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Credit sale entry rolled back: {ex.Message}", null);
				}
			});
		}

		private async Task<ServiceResponse<object>> HandleLoyaltyAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);
			if (string.IsNullOrWhiteSpace(sales.CustomerCode))
				return ServiceResponse<object>.Information("A valid loyalty account is required for this payment method.", null);

			var pointsBalance = await _loyalty.GetPointsBalance(sales.CustomerCode);
			if (pointsBalance <= 0)
				return ServiceResponse<object>.Information("No loyalty points available.", null);

			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);
			var pointsMonetaryValue = pointsBalance * _unitPrice;

			if (pointsMonetaryValue < saleTotal)
			{
				var pointsNeeded = Math.Ceiling(saleTotal / _unitPrice);
				return ServiceResponse<object>.Information(
					$"Insufficient loyalty points. Available: {pointsBalance:N2} (KES {pointsMonetaryValue:N2}), Required: {pointsNeeded:N2} points (KES {saleTotal:N2}).",
					new { PointsBalance = pointsBalance, MonetaryValue = pointsMonetaryValue });
			}

			var pointsToDeduct = Math.Ceiling(saleTotal / _unitPrice);

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales, sales.CustomerCode);
					await _loyalty.DeductLoyaltyPoints(sales.CustomerCode, pointsToDeduct, _saleId);

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed a LOYALTY SALE | SaleID={_saleId} | Station={_stationName}({_stationCode}) | Customer={sales.CustomerCode} | PointsDeducted={pointsToDeduct:N2} | {details} | VehicleRegistration={sales.VehicleRegistrationNumber}";
					await _authentication.AddUserTrail(msg, nameof(HandleLoyaltyAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Loyalty sale entry rolled back: {ex.Message}", null);
				}
			});
		}
		private async Task<ServiceResponse<object>> HandleOperationalLossAsync(MisingSaleDto sales)
		{
			if (sales.Quantity == 0) return ServiceResponse<object>.Information("Quantity cannot be zero", null);

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales);

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} recorded an OPERATIONAL LOSS | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details} | VehicleRegistration={sales.VehicleRegistrationNumber}";
					await _authentication.AddUserTrail(msg, nameof(HandleOperationalLossAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Operational loss entry rolled back: {ex.Message}", null);
				}
			});
		}

		private async Task<ServiceResponse<object>> HandleEmployeeMpesaAsync(MisingSaleDto sales)
		{
			if (!ValidateSalesBasics(sales, out var invalid)) return invalid;

			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

					var totalMpesaAvailable = await ValidateAndCalculateMpesaPaymentsAsync(sales.PaymentDetails);
					if (totalMpesaAvailable < saleTotal)
					{
						await tx.RollbackAsync();
						return ServiceResponse<object>.Information("Insufficient MPesa funds to complete this sale", null);
					}

					await SaveTransactionDataAsync(sales);
					foreach (var payment in sales.PaymentDetails)
					{
						await ReconcileAndUpdateUsageBalanceAsync(payment.TransactionReference);
					}

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

					var details = BuildAuditDetails(sales, sales.VehicleRegistrationNumber, sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed an EMPLOYEE MPESA sale | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
					await _authentication.AddUserTrail(msg, nameof(HandleEmployeeMpesaAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Employee Mpesa entry rolled back: {ex.Message}", null);
				}
			});
		}

		private async Task<ServiceResponse<object>> HandleCalibrationAsync(MisingSaleDto sales)
		{
			var strategy = _context.Database.CreateExecutionStrategy();
			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					await SaveTransactionDataAsync(sales);

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

					var details = BuildAuditDetails(sales, paymentRefs: sales.PaymentDetails.Select(p => p.TransactionReference));
					var msg = $"{_authentication.Name()} completed a CALIBRATION entry | SaleID={_saleId} | Station={_stationName}({_stationCode}) | {details}";
					await _authentication.AddUserTrail(msg, nameof(HandleCalibrationAsync));

					await tx.CommitAsync();
					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error($"Calibration entry rolled back: {ex.Message}", null);
				}
			});
		}

		// REMOVED: HandleInsuranceAsync — Insurance is no longer a supported
		// payment type in RoutePaymentAsync.

		private async Task<ServiceResponse<object>> HandleMpesaAsync(MisingSaleDto sales)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				try
				{
					if (!ValidateSalesBasics(sales, out var invalid)) return invalid;

					var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

					var totalMpesaAvailable = await ValidateAndCalculateMpesaPaymentsAsync(sales.PaymentDetails);
					if (totalMpesaAvailable < saleTotal)
					{
						await tx.RollbackAsync();
						return ServiceResponse<object>.Information("Insufficient MPesa funds to complete this sale", null);
					}

					await SaveTransactionDataAsync(sales);
					foreach (var payment in sales.PaymentDetails)
					{
						await ReconcileAndUpdateUsageBalanceAsync(payment.TransactionReference);
					}

					await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);
					//await ClearVariance(sales.ShiftNumber);
					//await _salesTasks.ReconcileStockSummariesAsync(sales.ShiftNumber);

					var details = BuildAuditDetails(sales, sales.VehicleRegistrationNumber, sales.PaymentDetails.Select(p => p.TransactionReference));
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
			});
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

		// ASSUMPTION (unchanged from before): DbContext exposes PetroleumProducts
		// with a PetroleumCode property matching sales.ProductCode — confirm
		// actual entity/property names if these differ.
		private async Task<ServiceResponse<object>> ValidateCoreEntitiesAsync(MisingSaleDto sales)
		{
			var shiftExist = await _context.Shifts.AnyAsync(x => x.ShiftNumber == sales.ShiftNumber);
			if (!shiftExist) return ServiceResponse<object>.Information("Shift does not exist", null);

			var nozzleExist = await _context.Nozzles.AnyAsync(x => x.NozzleCode == sales.NozzleCode);
			if (!nozzleExist) return ServiceResponse<object>.Information("Nozzle does not exist", null);

			var productExist = await _context.PetroleumProducts.AnyAsync(x => x.PetroleumCode == sales.ProductCode);
			if (!productExist) return ServiceResponse<object>.Information("Product does not exist", null);

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
			_tillNumber = await TillNumber(dispenserCode);
		}

		private static bool ValidateSalesBasics(MisingSaleDto sales, out ServiceResponse<object> response)
		{
			response = ServiceResponse<object>.Information("Invalid sales data", null);
			if (sales == null || sales.PaymentDetails == null || sales.PaymentDetails.Count == 0 || string.IsNullOrEmpty(sales.VehicleRegistrationNumber))
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
					// FIXED: now takes a row lock for the duration of this
					// transaction instead of a plain unguarded read.
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
					DateCreated =EatTime.Now,
					UserCode = _authentication.Usercode(),
					SaleId = _saleId,
					TransactionAmountDebit = 0
				});

				remaining -= toApply;
			}

			await _context.SaveChangesAsync();

		}

		// customerCode defaults to empty for payment types that legitimately
		// have no customer link (operational loss, calibration, employee mpesa).
		private async Task SaveTransactionDataAsync(MisingSaleDto sales, string customerCode = "")
		{
			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);

			_context.QuantityTransactions.Add(new QuantityTransactions
			{
				ShiftNumber = sales.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleRegistrationNumber = sales.VehicleRegistrationNumber,
				QuantityCredit = sales.Quantity,
				QuantityDebit = 0,
				DispenserCode = sales.DispenserCode,
				NozzleCode = sales.NozzleCode,
				AmountCredit = saleTotal,
				AmountDebit = 0,
				DateCreated =EatTime.Now,
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
					string message = mpesaAmount == null ? $"M-Pesa code {transId} does not exist." : $"Amount fully used for code {transId}.";
					return ServiceResponse<int?>.Information(message, mpesaAmount);
				}
				return ServiceResponse<int?>.Success($"Valid Mpesa Code {transId}.", mpesaAmount);
			}
			catch (Exception ex)
			{
				return ServiceResponse<int?>.Error($"An error occurred while validating payment: {ex.Message}", 0);
			}
		}

		// FIXED: replaced the plain EF read with a locking read. Uses
		// SELECT ... FOR UPDATE against the connection/transaction currently
		// open on _context, so the row stays locked from the moment it's
		// read (during ValidateAndCalculateMpesaPaymentsAsync) until it's
		// consumed (ConsumeMpesaAsync) later in the same transaction. This is
		// the fix for the double-spend gap — a second concurrent caller
		// trying to read the same row will block here until the first
		// transaction commits or rolls back, at which point it sees the
		// updated (already-decremented) balance.
		//
		// NOTE: only meaningfully locks when called from inside an open
		// transaction (i.e. from HandleMpesaAsync's flow, which is the only
		// caller that matters for concurrency safety). If ever called outside
		// an explicit transaction, Postgres wraps it in an implicit one and
		// releases the lock immediately after the statement — harmless, just
		// not protective, so don't call this from a non-transactional path.
		private async Task<int?> GetUsageBalanceAsync(string transId)
		{
			var conn = _context.Database.GetDbConnection();
			if (conn.State != ConnectionState.Open) await conn.OpenAsync();

			await using var cmd = conn.CreateCommand();
			cmd.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
			cmd.CommandText = @"SELECT ""UsageBalance"" FROM ""MpesaTransactions""
                                 WHERE ""TillNumber"" = @till AND ""TransID"" = @transId
                                 FOR UPDATE";

			var pTill = cmd.CreateParameter();
			pTill.ParameterName = "till";
			pTill.Value = _tillNumber;
			cmd.Parameters.Add(pTill);

			var pTrans = cmd.CreateParameter();
			pTrans.ParameterName = "transId";
			pTrans.Value = transId;
			cmd.Parameters.Add(pTrans);

			var result = await cmd.ExecuteScalarAsync();
			return result is null or DBNull ? (int?)null : Convert.ToInt32(result);
		}

		private async Task<int> ConsumeMpesaAsync(string transId, int amountToConsume)
		{
			var transaction = await _context.MpesaTransactions
				.FirstOrDefaultAsync(x =>
					x.BusinessShortCode == _tillNumber &&
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

		public async Task<string> TillNumber(string dispenserCode)
		{
			var number = await (from s in _context.Dispensers
								join t in _context.Tills on s.TillNumber equals t.TillNumber
								where s.DispenserCode == dispenserCode
								select t.TillNumber).FirstOrDefaultAsync();
			return number ?? string.Empty;
		}
		//public async Task<ServiceResponse<object>> ClearVariance(string shiftNumber)
		//{
		//	try
		//	{
		//		var variances = await (
		//			from vs in _context.StockTakeSummaries
		//			where vs.ShiftNumber == shiftNumber
		//			select vs
		//		).ToListAsync();

		//		var dispenserStation = await (from s in _context.Shifts
		//									  where s.ShiftNumber == shiftNumber
		//									  join d in _context.Dispensers on s.DispenserCode equals d.DispenserCode into dj
		//									  from d in dj.DefaultIfEmpty()
		//									  select new { s.DispenserCode, StationCode = d != null ? d.StationCode : null }).FirstOrDefaultAsync();

		//		var dispenserId = dispenserStation?.DispenserCode ?? string.Empty;
		//		var stationCode = dispenserStation?.StationCode ?? string.Empty;

		//		var threshold = await _varianceService.GetThresholdForDispenserAsync(dispenserId);

		//		var nozzlePrices = new Dictionary<string, decimal>();

		//		// SIGNED and NETTED across all nozzles in the shift — an overage on one
		//		// nozzle offsets a shortage on another, in money terms just like in litres.
		//		// e.g. Nozzle A +8L, Nozzle B -5L => net = +3L worth, NOT (8L + 5L) = 13L worth.
		//		// Each nozzle's own retail price is used for its own portion of the net value.
		//		decimal netVarianceValue = 0m;

		//		foreach (var variance in variances)
		//		{
		//			if (!nozzlePrices.TryGetValue(variance.NozzleCode, out var pricePerLitre))
		//			{
		//				pricePerLitre = await _varianceService.GetCurrentRetailPriceAsync(dispenserId, variance.NozzleCode);
		//				nozzlePrices[variance.NozzleCode] = pricePerLitre;
		//			}

		//			var netVarianceForNozzle = variance.ClosingVariance + variance.OpeningVariance;
		//			netVarianceValue += netVarianceForNozzle * pricePerLitre; // signed - do NOT Math.Abs here
		//		}

		//		// Magnitude of the netted value — used for both the threshold check and the audit message.
		//		var totalVarianceValue = Math.Abs(netVarianceValue);

		//		// Shift-level (not per-nozzle) signed variance in litres.
		//		// e.g. Nozzle A +8L, Nozzle B -5L => shift net = +3L => overage, goes through value threshold.
		//		var totalVarianceLitres = variances.Sum(x => x.ClosingVariance + x.OpeningVariance);

		//		// Auto-clear conditions are evaluated on the SHIFT-LEVEL NET variance (all nozzles
		//		// combined), partitioned strictly by sign:
		//		//   • Shift net overage  (totalVarianceLitres > 0)          -> must pass the money-value threshold (netted value).
		//		//   • Shift net shortage (totalVarianceLitres in [-1L, 0L]) -> auto-clears on litres alone, regardless of value.
		//		//   • Shift net shortage beyond -1L (< -1L, e.g. -2L, -3L)  -> never auto-clears, under any condition.
		//		var isOverage = totalVarianceLitres > 0m;
		//		var isMinorShortage = totalVarianceLitres >= -1m && totalVarianceLitres <= 0m;

		//		var isWithinValueThreshold = isOverage && totalVarianceValue <= threshold;
		//		var isWithinLitreThreshold = isMinorShortage;

		//		if (isWithinValueThreshold || isWithinLitreThreshold)
		//		{
		//			// Mark every nozzle's variance row as closed. No per-nozzle transaction is
		//			// written here — opposing nozzles (e.g. A=+8, B=-0.08) no longer generate
		//			// their own separate debit/credit entries.
		//			foreach (var variance in variances)
		//			{
		//				variance.VarianceStatus = ShiftStatus.Closed;
		//				_context.StockTakeSummaries.Update(variance);
		//			}

		//			// Write ONE consolidated transaction pair reflecting the NET shift-level
		//			// position (totalVarianceLitres / totalVarianceValue), not one per nozzle.
		//			if (totalVarianceLitres != 0m)
		//			{
		//				var isShortage = totalVarianceLitres < 0m;
		//				var magnitude = Math.Abs(totalVarianceLitres);
		//				var saleId = _setups.GenerateSaleId();
		//				var firstVariance = variances.FirstOrDefault();

		//				var quantityTransaction = new QuantityTransactions
		//				{
		//					DateCreated =EatTime.Now,
		//					UserCode = firstVariance?.UserCode ?? "",
		//					NozzleCode = firstVariance?.NozzleCode ?? "", // TODO: confirm convention for a shift-level net entry
		//					QuantityCredit = isShortage ? magnitude : 0,
		//					QuantityDebit = isShortage ? 0 : magnitude,
		//					ShiftNumber = shiftNumber,
		//					SaleId = saleId,
		//					PaymentTypeCode = 3,
		//					DispenserCode = dispenserId,
		//					StationCode = stationCode,
		//					AmountDebit = 0,
		//					AmountCredit = 0,
		//					Discount = 0,
		//					Vat_Amount = 0,
		//					Price = 0,
		//					IsReversed = false,
		//					CustomerCode = string.Empty,
		//					OtpUsed = string.Empty,
		//					VehicleRegistrationNumber = _authentication.Usercode(),
		//				};
		//				await _context.QuantityTransactions.AddAsync(quantityTransaction);

		//				var paymentTransaction = new PaymentTransactions
		//				{
		//					DateCreated =EatTime.Now,
		//					UserCode = firstVariance?.UserCode ?? string.Empty,
		//					SaleId = saleId,
		//					PaymentRefrence = _setups.GenerateShiftNumber(),
		//					TransactionAmount = isShortage ? 0 : totalVarianceValue,
		//					TransactionAmountDebit = isShortage ? totalVarianceValue : 0
		//				};
		//				await _context.PaymentTransactions.AddAsync(paymentTransaction);
		//			}

		//			var shiftToClose = await (from s in _context.Shifts where s.ShiftNumber == shiftNumber select s).FirstOrDefaultAsync();

		//			shiftToClose?.ShiftStatus = ShiftStatus.Closed;

		//			await _context.SaveChangesAsync();
		//			await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);

		//			var reasonText = isWithinValueThreshold
		//				? $"it falls within the allowed threshold of KES {threshold:N2}"
		//				: $"net litre variance ({totalVarianceLitres:N2}L) falls within the shortage auto-clear allowance (-1L to 0L)";

		//			var message = $"Variance of KES {totalVarianceValue:N2} (quantity {totalVarianceLitres:N2}) of ShiftNumber {shiftNumber} has been cleared on {DateTime.UtcNow} by system service, {reasonText}.";
		//			await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

		//			return ServiceResponse<object>.Success("Variance cleared successfully", null);
		//		}

		//		return ServiceResponse<object>.Information("Variance not cleared", null);
		//	}
		//	catch (Exception ex)
		//	{
		//		return ServiceResponse<object>.Error(ex.Message, null);
		//	}
		//}       // ====== Variance Methods ======
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


		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			return await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
		}

		// ====== Inner types ======
		public class StationData
		{
			[StringLength(10)]
			public string StationCode { get; set; } = string.Empty;
			[StringLength(50)]
			public string StationName { get; set; } = string.Empty;
		}

		// ====== Audit detail builder (centralized, consistent) ======
		private string BuildAuditDetails(MisingSaleDto sales, string? vehicleReg = null, IEnumerable<string>? paymentRefs = null)
		{
			var saleTotal = Math.Floor(sales.Quantity * _unitPrice);
			var paidEntered = sales.PaymentDetails.Sum(p => p.TransactionAmount);
			var refs = paymentRefs == null ? "" : string.Join(", ", paymentRefs.Where(r => !string.IsNullOrWhiteSpace(r)));
			return $"Qty={sales.Quantity:N2}L | UnitPrice={_unitPrice:N2} | SaleTotal={saleTotal:N2} | EnteredPayTotal={paidEntered:N2} | Shift={sales.ShiftNumber} | Dispenser={sales.DispenserCode} | Nozzle={sales.NozzleCode} | Vehicle={vehicleReg ?? sales.VehicleRegistrationNumber} | PaymentRefs=[{refs}] | When={DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}";
		}
	}
}