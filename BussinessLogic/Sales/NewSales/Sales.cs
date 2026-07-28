using BusinessLogic.Messaging;
using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.Sales.Receipts;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.EntityModels.Customer;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnfonSms;
using System.Text.RegularExpressions;

namespace BussinessLogic.Sales.NewSales
{
	// =========================================================================
	// Immutable context — built once per transaction, threaded through the pipeline
	// =========================================================================

	internal sealed record SaleContext(
		StationData Station,
		CustomerInfo Customer,
		decimal UnitPrice,
		decimal Discount,
		decimal Calculated,
		decimal Requested,
		string TransactionRef,
		string VehicleRegistration,  // just the string the user entered
		decimal RawCalculated        // unrounded unitPrice * quantity — used only for the underpayment guard
	);

	internal delegate Task<ServiceResponse<object>?> PaymentStepAsync(
		AddsaleDto sales, SaleContext ctx, string saleId);

	// =========================================================================
	// Sales service
	// =========================================================================

	public class Sales : ISales
	{
		// NOTE: this class writes wallet debits (see HandleWalletAsync below) directly
		// against the same CustomerTransactions table the standalone wallet/top-up
		// service (WalletTransactions.cs) uses, deliberately bypassing that service.
		// A "TopUpType" code is used to distinguish a fuel-purchase debit from a
		// top-up, withdrawal, or transfer when reporting on the CustomerTransactions
		// ledger — adjust WalletFuelPurchaseTopUpType below to match whatever code
		// your TopUpTypes table actually uses for this.
		private const int WalletFuelPurchaseTopUpType = 9;

		private readonly ILoyaltyServices _loyalty;
		private readonly IMemoryCache _cache;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IAfricaIsTalking _isTalking;
		private readonly ReceiptService _receipt;
		private readonly ISmsService _sms;

		public Sales(
			OTOContext context,
			ICommonSetups setups,
			IAuthCommonTasks authentication,
			ICommonSalesTasks salesTasks,
			IAfricaIsTalking isTalking,
			IMemoryCache cache,
			ReceiptService receipt,
			ILoyaltyServices loyalty, ISmsService sms)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_salesTasks = salesTasks;
			_isTalking = isTalking;
			_cache = cache;
			_receipt = receipt;
			_loyalty = loyalty;
			_sms = sms;
		}

		// =====================================================================
		// Entry point
		// =====================================================================

		public async Task<ServiceResponse<object>> AddSalesAsync(AddsaleDto sales)
		{
			var saleId = _setups.GenerateSaleId();

			if (await _context.QuantityTransactions.AsNoTracking().AnyAsync(q => q.SaleId == saleId))
				return Info("Duplicate sale detected, try again");

			var precheck = await ValidateDataAsync(sales);
			if (precheck.ResponseCode == Response.Information)
				return precheck;

			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await HandleMpesaAsync(sales, saleId),
				PaymetMethod.Cash => await HandleCashAsync(sales, saleId),
				PaymetMethod.Credit => await HandleCreditAsync(sales, saleId),
				PaymetMethod.Loyalty => await HandleLoyaltyAsync(sales, saleId),
				PaymetMethod.PDQ => await HandlePDQAsync(sales, saleId),
				PaymetMethod.Wallet => await HandleWalletAsync(sales, saleId),
				_ => Info("Feature Coming Soon"),
			};
		}

		// =====================================================================
		// Unified pipeline — standard payment methods
		// =====================================================================

		private async Task<ServiceResponse<object>> ExecuteSaleAsync(
			AddsaleDto sales,
			string saleId,
			string operationType,
			string receiptPaymentMethod,
			bool awardLoyalty,
			Func<AddsaleDto, Task<string>> generateRef,
			PaymentStepAsync paymentStep,
			Func<SaleContext, AddsaleDto, Task>? postCommit = null,
			string? receiptStationOverride = null,
			StationData? station = null)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				_context.Database.AutoSavepointsEnabled = false;

				try
				{
					var txRef = await generateRef(sales);
					sales.PaymentDetails.ForEach(p => p.TransactionReference = txRef);

					var ctx = await ResolveSaleContextAsync(sales, txRef, station);

					var abort = await paymentStep(sales, ctx, saleId);
					if (abort is not null) return abort;

					if (!ValidateTransactionAmount(ctx))
						return Info("Transaction amount does not match Quantity x Price");

					StageReceipt(ctx, sales, receiptPaymentMethod,
						stationNameOverride: receiptStationOverride);

					await PersistSaleAsync(sales, ctx, saleId);

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					await WriteAuditTrailAsync(ctx, sales, operationType, saleId);

					if (awardLoyalty)
						await SafeAwardPointsAsync(sales, saleId);

					if (postCommit is not null)
						await postCommit(ctx, sales);

					return ServiceResponse<object>.Success(
						$"{operationType.Replace("SALE", "sale")} completed successfully", null);
				}
				catch
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error(
						"An error occurred while processing the transaction.", null);
				}
			});
		}

		// =====================================================================
		// Unified pipeline — M-Pesa (raw ref overload)
		// =====================================================================

		private async Task<ServiceResponse<object>> ExecuteSaleRawRefAsync(
			AddsaleDto sales,
			string saleId,
			string operationType,
			string receiptPaymentMethod,
			bool awardLoyalty,
			PaymentStepAsync paymentStep,
			string? mpesaTillNumber = null,
			string? receiptStationOverride = null,
			StationData? station = null,
			Func<Dictionary<string, int>?>? getPreValidatedUsage = null)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				_context.Database.AutoSavepointsEnabled = false;

				try
				{
					var ctx = await ResolveSaleContextAsync(sales, saleId, station);

					var abort = await paymentStep(sales, ctx, saleId);
					if (abort is not null) return abort;

					if (!ValidateTransactionAmount(ctx))
						return Info("Transaction amount does not match Quantity x Price");

					StageReceipt(ctx, sales, receiptPaymentMethod,
						stationNameOverride: receiptStationOverride);

					// getPreValidatedUsage() reads the dictionary AFTER paymentStep has
					// run and populated it (closures capture the local by reference),
					// so PersistSaleAsync can reuse balances instead of re-querying
					// and re-locking the same MpesaTransactions rows.
					var mpesaRefs = await PersistSaleAsync(sales, ctx, saleId,
						mpesaTillNumber: mpesaTillNumber,
						preValidatedUsage: getPreValidatedUsage?.Invoke());

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					// ── Clear tracker so reconcile reads fresh committed data ──
					_context.ChangeTracker.Clear();

					foreach (var transId in mpesaRefs)
						await ReconcileAndUpdateUsageBalanceAsync(transId);

					if (mpesaRefs.Count > 0)
						await _context.SaveChangesAsync();

					await WriteAuditTrailAsync(ctx, sales, operationType, saleId);

					if (awardLoyalty)
						await SafeAwardPointsAsync(sales, saleId);

					return ServiceResponse<object>.Success("Sales made successfully", null);
				}
				catch
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error(
						"An error occurred while processing the transaction.", null);
				}
			});
		}

		// =====================================================================
		// Payment handlers
		// =====================================================================

		private Task<ServiceResponse<object>> HandleCashAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "CASH SALE",
				receiptPaymentMethod: "Cash",
				awardLoyalty: true,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: (_, _, _) => Task.FromResult<ServiceResponse<object>?>(null)
			);
		}

		private Task<ServiceResponse<object>> HandlePDQAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleRawRefAsync(
				sales, saleId,
				operationType: "PDQ SALE",
				receiptPaymentMethod: "PDQ",
				awardLoyalty: true,
				paymentStep: (_, ctx, _) =>
				{
					StageQueuedSms(sales.PhoneNumber, BuildSms(ctx,
						$"a PDQ (card) payment of KES {ctx.Calculated:N2} " +
						$"for {sales.Quantity:N2} litres has been recorded " +
						$"for vehicle {sales.RegistrationNumber} " +
						$"at {ctx.Station.StationName} on {UtcStamp()}."));

					return Task.FromResult<ServiceResponse<object>?>(null);
				}
			);
		}

		private Task<ServiceResponse<object>> HandleCreditAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "CREDIT SALE",
				receiptPaymentMethod: "Credit",
				awardLoyalty: false,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, sid) =>
				{
					if (!ctx.Customer.IsCreditCustomer)
						return Info("This customer is not approved for credit purchases.");

					var outstanding = await GetOutstandingCreditAsync(ctx.Customer.CustomerCode);
					var newExposure = outstanding + ctx.Calculated;

					if (newExposure > ctx.Customer.CreditLimit)
						return ServiceResponse<object>.Information(
							$"Credit limit exceeded. Limit: {ctx.Customer.CreditLimit:N2}, " +
							$"Outstanding: {outstanding:N2}, This sale: {ctx.Calculated:N2}",
							new { ctx.Customer.CreditLimit, Outstanding = outstanding });

					_context.CreditTransactions.Add(new CreditTransactions
					{
						CustomerCode = ctx.Customer.CustomerCode,
						Credit = 0,
						Debit = ctx.Calculated,
						SaleId = sid,
						TransactionReference = ctx.TransactionRef,
						VehicleCode = s.RegistrationNumber,
						StationCode = ctx.Station.StationCode,
						DateCreated = EatTime.Now,
						UserCode = _authentication.Usercode()
					});

					var remainingCredit = ctx.Customer.CreditLimit - newExposure;
					StageQueuedSms(sales.PhoneNumber, BuildSms(ctx,
						$"a credit sale of KES {ctx.Calculated:N2} for {s.Quantity:N2} litres " +
						$"has been recorded for vehicle {sales.RegistrationNumber} " +
						$"at {ctx.Station.StationName} on {UtcStamp()}. " +
						$"Remaining credit: KES {remainingCredit:N2}."));

					return null;
				}
			);
		}

		// Wallet sale — debits the vehicle's prepaid wallet (CustomerTransactions
		// ledger) for the fuel total, inside the same DB transaction as the rest of
		// the sale. If the sale rolls back for any reason, the debit rolls back too.
		//
		// AcquireWalletLockAsync takes a Postgres advisory lock scoped to the vehicle
		// for the life of this transaction, so two concurrent wallet sales against the
		// same vehicle can't both read the pre-debit balance and both pass the
		// sufficiency check — same double-spend concern the M-Pesa path guards
		// against with FOR UPDATE row locks, just applied to an aggregate balance
		// instead of a single row.
		// Wallet sale — debits the customer's prepaid wallet (CustomerTransactions
		// ledger) for the fuel total, inside the same DB transaction as the rest of
		// the sale. If the sale rolls back for any reason, the debit rolls back too.
		//
		// Balance and lock are keyed by CustomerCode, not VehicleCode — a wallet is
		// a customer-level balance, not a per-vehicle one. VehicleCode is still
		// persisted on the CustomerTransactions row purely as a record of which
		// vehicle triggered this particular debit; it plays no part in the balance
		// calculation or the concurrency lock.
		//
		// AcquireWalletLockAsync takes a Postgres advisory lock scoped to the
		// customer for the life of this transaction, so two concurrent wallet sales
		// against the same customer (e.g. two vehicles on the same account fuelling
		// at once) can't both read the pre-debit balance and both pass the
		// sufficiency check — same double-spend concern the M-Pesa path guards
		// against with FOR UPDATE row locks, just applied to an aggregate balance
		// instead of a single row.
		private Task<ServiceResponse<object>> HandleWalletAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "WALLET SALE",
				receiptPaymentMethod: "Wallet",
				awardLoyalty: true,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, sid) =>
				{
					// s.RegistrationNumber is the plate string the attendant entered
					// (e.g. "KDA 123A") — NOT the vehicle's VehicleCode. Resolve both
					// CustomerCode and the real VehicleCode off the same row so the
					// plate number never leaks into a column expecting VehicleCode.
					var vehicle = await (from v in _context.Vehicles
										 where v.VehicleRegistrationNumber.Equals(s.RegistrationNumber)
										 select new { v.CustomerCode, v.VehicleCode })
										 .FirstOrDefaultAsync();

					if (vehicle is null || string.IsNullOrWhiteSpace(vehicle.CustomerCode))
						return Info("This vehicle is not linked to a wallet account.");

					await AcquireWalletLockAsync(vehicle.CustomerCode);

					var balance = await GetWalletBalanceAsync(vehicle.CustomerCode);

					if (balance < ctx.Calculated)
						return ServiceResponse<object>.Information(
							$"Insufficient wallet balance. Available: KES {balance:N2}, Required: KES {ctx.Calculated:N2}",
							new { Balance = balance, Required = ctx.Calculated });

					_context.CustomerTransactions.Add(new CustomerTransactions
					{
						DateCreated = EatTime.Now,
						UserCode = _authentication.Usercode(),
						CustomerCode = vehicle.CustomerCode,
						VehicleCode = vehicle.VehicleCode,       // real vehicle code, not the plate string
						TransactionReference = ctx.TransactionRef,
						Credit = 0,
						Debit = ctx.Calculated,
						UserReference = sid,
						Narration = $"Fuel purchase - {s.Quantity:N2}L at {ctx.Station.StationName}",
						TopUpType = WalletFuelPurchaseTopUpType
					});

					var customerDetails = await (from c in _context.Customers
												 where c.CustomerCode == vehicle.CustomerCode
												 select c).FirstOrDefaultAsync() ?? new Customer();

					var remainingBalance = balance - ctx.Calculated;

					string sms =
						$"Dear {FirstName(customerDetails.CustomerName)} KES {ctx.Calculated:N2} has been deducted from your wallet for {s.Quantity:N2} litres " +
						$"for vehicle {sales.RegistrationNumber} at {ctx.Station.StationName} on {UtcStamp()}. " +
						$"Remaining wallet balance: KES {remainingBalance:N2}.";

					await _sms.SendAsync(ctx.Customer.CustomerPhone, sms);
					return null;
				}
			);
		}
		// Current wallet balance for a customer (sum of credits minus debits on
		// CustomerTransactions). AsNoTracking — this is a read used purely to decide
		// whether the debit below is allowed; the debit itself is a fresh insert.
		private Task<decimal> GetWalletBalanceAsync(string customerCode)
			=> _context.CustomerTransactions
				.AsNoTracking()
				.Where(x => x.CustomerCode == customerCode)
				.SumAsync(x => x.Credit - x.Debit);

		// Takes a Postgres transaction-scoped advisory lock keyed on the customer
		// code. Released automatically on COMMIT/ROLLBACK of the surrounding
		// transaction — no separate unlock call needed. Serializes concurrent
		// wallet sales (or a wallet sale racing a wallet top-up/withdrawal/transfer,
		// if those also take this lock) against the same customer so the balance
		// check above can be trusted.
		private Task AcquireWalletLockAsync(string customerCode)
			=> _context.Database.ExecuteSqlInterpolatedAsync(
				$@"SELECT pg_advisory_xact_lock(hashtext({customerCode}))");
		private async Task<ServiceResponse<object>> HandleMpesaAsync(AddsaleDto sales, string saleId)
		{
			var mpesaCodes = sales.PaymentDetails
				.Where(p => p.TransactionReference?.Trim().Length == 10)
				.Select(p => p.TransactionReference!.Trim())
				.ToList();

			if (mpesaCodes.Count == 0)
				return Info("No valid M-Pesa codes provided");

			//var dupCheck = await CheckDuplicates(sales);
			//if (dupCheck.ResponseCode == Response.Information)
			//	return Info("Duplicate M-Pesa codes found in the transaction");

			var station = await GetStationAsync(sales.DispenserCode); // resolved ONCE, reused below

			// Populated inside paymentStep, then read back by ExecuteSaleRawRefAsync
			// via the getPreValidatedUsage closure — avoids re-locking the same
			// MpesaTransactions rows a second time inside PersistSaleAsync.
			Dictionary<string, int>? usageCache = null;

			return await ExecuteSaleRawRefAsync(
				sales, saleId,
				operationType: "MPESA SALE",
				receiptPaymentMethod: "M-Pesa",
				awardLoyalty: true,
				mpesaTillNumber: station.TillNumber, // ✅ verification now consistently uses TillNumber
				receiptStationOverride: _setups.SentenceCase(station.StationName),
				station: station,
				getPreValidatedUsage: () => usageCache,
				paymentStep: async (s, ctx, _) =>
				{
					var codes = s.PaymentDetails
						.Where(p => !string.IsNullOrWhiteSpace(p.TransactionReference))
						.Select(p => p.TransactionReference!)
						.Distinct()
						.ToList();

					var usage = new Dictionary<string, int>();
					decimal total = 0m;

					foreach (var code in codes)
					{
						var r = await ValidateMpesaPaymentAsync(code, ctx.Station.TillNumber);
						if (r.ResponseCode != Response.Success)
							return Info(r.ResponseMessage!);

						usage[code] = r.ResponseObject ?? 0;
						total += r.ResponseObject ?? 0;
					}

					usageCache = usage;

					if (total < ctx.Requested)
						return Info("Insufficient funds, cannot complete the transaction");

					StageQueuedSms(ctx.Customer.CustomerPhone,
						BuildSms(ctx,
							$"your M-Pesa payment of KES {ctx.Requested:N2} " +
							$"has been received for {s.Quantity:N2} litres for vehicle " +
							$"{sales.RegistrationNumber} at " +
							$"{_setups.SentenceCase(ctx.Station.StationName)} " +
							$"on {EatTime.Now:yyyy-MMM-dd} at {EatTime.Now:HH:mm}. Thank you!"));

					return null;
				}
			);
		}

		private Task<ServiceResponse<object>> HandleLoyaltyAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "LOYALTY SALE",
				receiptPaymentMethod: "Loyalty Points",
				awardLoyalty: false,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, sid) =>
				{
					if (!s.IsLoyalCustomer || string.IsNullOrWhiteSpace(s.LoyaltyPhone))
						return Info("A valid loyalty account is required for this payment method.");

					var customerCode = await _context.Customers
						.AsNoTracking()
						.Where(c => c.CustomerPhone == s.LoyaltyPhone)
						.Select(c => c.CustomerCode)
						.FirstOrDefaultAsync();

					if (string.IsNullOrEmpty(customerCode))
						return Info("Loyalty customer not found.");

					var pointsBalance = await GetLoyaltyPointsBalanceAsync(customerCode);

					if (pointsBalance <= 0)
						return Info("No loyalty points available.");

					var pointsMonetaryValue = pointsBalance * ctx.UnitPrice;

					if (pointsMonetaryValue < ctx.Calculated)
					{
						var pointsNeeded = Math.Ceiling(ctx.Calculated / ctx.UnitPrice);
						return ServiceResponse<object>.Information(
							$"Insufficient loyalty points. Available: {pointsBalance:N2} " +
							$"(KES {pointsMonetaryValue:N2}), Required: {pointsNeeded:N2} points " +
							$"(KES {ctx.Calculated:N2}).",
							new { PointsBalance = pointsBalance, MonetaryValue = pointsMonetaryValue });
					}

					var pointsToDeduct = Math.Ceiling(ctx.Calculated / ctx.UnitPrice);
					await _loyalty.DeductLoyaltyPoints(customerCode, pointsToDeduct, sid);

					var remainingPoints = pointsBalance - pointsToDeduct;
					var remainingValue = remainingPoints * ctx.UnitPrice;

					StageQueuedSms(sales.PhoneNumber, BuildSms(ctx,
						$"a loyalty points redemption of {pointsToDeduct:N2} points " +
						$"(KES {ctx.Calculated:N2}) for {s.Quantity:N2} litres " +
						$"has been processed for vehicle {sales.RegistrationNumber} " +
						$"at {ctx.Station.StationName} on {UtcStamp()}. " +
						$"Remaining points: {remainingPoints:N2} (KES {remainingValue:N2})."));

					return null;
				}
			);
		}

		private Task<decimal> GetLoyaltyPointsBalanceAsync(string customerCode)
		=> _loyalty.GetPointsBalance(customerCode);

		// =====================================================================
		// Context resolution
		// =====================================================================

		// ResolveSaleContextAsync — rounding rule:
		//
		//   calculated = unitPrice * quantity, e.g. 999.45
		//   ceilingCalculated = round the fuel total UP to the next whole shilling, e.g. 1000
		//   roundedRequested  = what the customer sent, rounded to the nearest whole shilling
		//   effective = the SMALLER of the two
		//
		// Examples (calculated = 999.45, so ceilingCalculated = 1000):
		//   sent 1000  -> effective = min(1000, 1000) = 1000   (take the full 1000)
		//   sent 1001  -> effective = min(1001, 1000) = 1000   (never take more than the ceiling)
		//   sent 999   -> effective = min(999, 1000)  = 999    (never top up an underpayment)
		//   sent 1005  -> effective = min(1005, 1000) = 1000
		//
		// The underpayment guard (ValidateTransactionAmount) is checked against the RAW
		// calculated value (999.45), not the rounded "effective" figure — otherwise a
		// genuinely short payment (e.g. customer sends 500 against a 999.45 sale) would
		// slip through, since effective is always <= whatever was sent by construction.
		//
		// `station` — pass a pre-resolved StationData to skip the join query entirely
		// (e.g. HandleMpesaAsync already needs the till number before this runs).
		private async Task<SaleContext> ResolveSaleContextAsync(
			AddsaleDto sales, string transactionRef, StationData? station = null)
		{
			station ??= await GetStationAsync(sales.DispenserCode);
			var customer = await GetCustomerByPhoneAsync(sales.PhoneNumber);
			var (unitPrice, disc) = await GetPriceByNozzleAsync(sales.NozzleCode);

			var requested = Math.Round(sales.PaymentDetails.Sum(x => x.TransactionAmount), 2);
			var calculated = Math.Round(unitPrice * sales.Quantity, 2);

			var roundedRequested = Math.Round(requested, 0, MidpointRounding.AwayFromZero);
			var ceilingCalculated = Math.Ceiling(calculated);

			// Never charge more than the fuel total (rounded up), and never charge
			// more than what the customer actually sent.
			var effective = Math.Min(roundedRequested, ceilingCalculated);

			return new SaleContext(
				station, customer, unitPrice, disc,
				Calculated: effective,           // always a whole number now
				Requested: roundedRequested,
				TransactionRef: transactionRef,
				VehicleRegistration: sales.RegistrationNumber,
				RawCalculated: calculated        // unrounded, e.g. 999.45
			);
		}

		// =====================================================================
		// Persist rows — returns M-Pesa refs for post-commit reconciliation
		// =====================================================================

		private async Task<List<string>> PersistSaleAsync(
			AddsaleDto sales, SaleContext ctx, string saleId,
			string? mpesaTillNumber = null,
			Dictionary<string, int>? preValidatedUsage = null)
		{
			_context.QuantityTransactions.Add(
				BuildQuantityTransaction(sales, ctx, saleId));

			decimal remaining = ctx.Calculated;
			var mpesaRefs = new List<string>();

			foreach (var pay in sales.PaymentDetails)
			{
				if (remaining <= 0) break;

				// ctx.Calculated / remaining is now always a whole number, so each
				// allocation is rounded to a whole number too — no stray cents
				// (e.g. 44.70) creep back into PaymentTransactions.
				decimal alloc = Math.Min(
					remaining,
					Math.Round(pay.TransactionAmount, 0, MidpointRounding.AwayFromZero));

				if (!string.IsNullOrWhiteSpace(mpesaTillNumber)
					&& !string.IsNullOrWhiteSpace(pay.TransactionReference))
				{
					int usable;

					if (preValidatedUsage is not null
						&& preValidatedUsage.TryGetValue(pay.TransactionReference, out var cached))
					{
						// Already validated (and row-locked) once in the paymentStep —
						// reuse instead of hitting MpesaTransactions again.
						usable = cached;
					}
					else
					{
						var check = await ValidateMpesaPaymentAsync(
							pay.TransactionReference, mpesaTillNumber);
						usable = Math.Max(0, check.ResponseObject ?? 0);
					}

					alloc = Math.Min(alloc, usable);
				}

				_context.PaymentTransactions.Add(new PaymentTransactions
				{
					PaymentRefrence = pay.TransactionReference,
					TransactionAmount = alloc,
					DateCreated = EatTime.Now,
					UserCode = _authentication.Usercode(),
					SaleId = saleId,
					TransactionAmountDebit = 0
				});

				if (!string.IsNullOrWhiteSpace(pay.TransactionReference)
					&& !string.IsNullOrWhiteSpace(mpesaTillNumber))
					mpesaRefs.Add(pay.TransactionReference);

				remaining -= alloc;
			}

			return mpesaRefs;
		}

		// =====================================================================
		// M-Pesa reconciliation — runs AFTER SaveChanges + Commit
		// =====================================================================

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

		// =====================================================================
		// Staging helpers
		// =====================================================================

		private void StageReceipt(
			SaleContext ctx, AddsaleDto sales, string paymentMethod,
			string? stationNameOverride = null)
		{
			_context.TransactionReceipts.Add(new TransactionReceipts
			{
				CustomerName = ctx.Customer.CustomerName,
				PhoneNumber = ctx.Customer.CustomerPhone,
				TotalAmount = (double)ctx.Requested,
				DateCreated = EatTime.Now,
				Duplicate = 0,
				VehicleReg = sales.RegistrationNumber,
				ReceiptNumber = ctx.TransactionRef,
				PaymentMethod = paymentMethod,
				PricePerLitre = (double)ctx.UnitPrice,
				Quantity = (double)sales.Quantity,
				StationName = stationNameOverride ?? ctx.Station.StationName,
				ServedBy = _authentication.Name().Split(',')[0],
				UserCode = _authentication.Usercode(),
				Vat_Amount = 0
			});
		}

		private void StageQueuedSms(string? phone, string? message)
		{
			if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(message)) return;

			_context.RescheduledMessages.Add(new RescheduledMessages
			{
				DateCreated = EatTime.Now,
				DateSent = EatTime.Now,
				IsSent = false,
				Message = message,
				PhoneNumber = phone,
				ScheduledSendingdate = EatTime.Now,
				SenderId = "Fuel Flow"
			});
		}

		private async Task WriteAuditTrailAsync(
			SaleContext ctx, AddsaleDto sales, string operationType, string saleId)
		{
			var refs = sales.PaymentDetails
				.Select(p => p.TransactionReference)
				.Where(r => !string.IsNullOrWhiteSpace(r))
				.ToArray();

			var refsStr = refs.Length > 0 ? $"[{string.Join(",", refs)}]" : "[]";

			var msg =
				$"{_authentication.Name()} recorded a {operationType} | " +
				$"SaleID={saleId} | Station={ctx.Station.StationName}({ctx.Station.StationCode}) | " +
				$"Qty={sales.Quantity:0.00}L | UnitPrice={ctx.UnitPrice:0.00} | " +
				$"SaleTotal={ctx.Calculated:0.00} | EnteredTotal={ctx.Requested:0.00} | " +
				$"Shift={sales.ShiftNumber} | Dispenser={sales.DispenserCode} | " +
				$"Nozzle={sales.NozzleCode} | Vehicle={sales.RegistrationNumber} | " +
				$"Customer={ctx.Customer.CustomerName}({ctx.Customer.CustomerCode}) | " +
				$"Refs={refsStr} | At={EatTime.Now:yyyy/MM/dd HH:mm:ss} | " +
				$"User={_authentication.Usercode()}";

			await _authentication.AddUserTrail(msg, operationType);
		}

		// =====================================================================
		// Validation
		// =====================================================================

		private record ValidationFlags(bool HasShift, bool HasNozzle, bool HasPaymentType, bool HasDispenser);

		private async Task<ServiceResponse<object>> ValidateDataAsync(AddsaleDto sales)
		{
			if (sales?.PaymentDetails is null || sales.PaymentDetails.Count == 0)
				return Info("Invalid sales payload");

			if (sales.PaymentTypeCode == PaymetMethod.Mpesa && sales.PaymentDetails.Count > 2)
				return Info(
					$"Hi {_authentication.Username().Split(',')[0]}, " +
					$"more than two Mpesa codes is not allowed");

			// Single round trip instead of 4 sequential AnyAsync() calls.
			var flags = await _context.Database
				.SqlQuery<ValidationFlags>($@"
					SELECT
						EXISTS(SELECT 1 FROM ""Shifts""       WHERE ""ShiftNumber""    = {sales.ShiftNumber})    AS ""HasShift"",
						EXISTS(SELECT 1 FROM ""Nozzles""      WHERE ""NozzleCode""     = {sales.NozzleCode})     AS ""HasNozzle"",
						EXISTS(SELECT 1 FROM ""PaymentTypes"" WHERE ""PaymentTypeId"" = {sales.PaymentTypeCode}) AS ""HasPaymentType"",
						EXISTS(SELECT 1 FROM ""Dispensers""   WHERE ""DispenserCode""  = {sales.DispenserCode})  AS ""HasDispenser""
				")
				.FirstAsync();

			if (!flags.HasShift) return Info("Shift does not exist");
			if (!flags.HasNozzle) return Info("Nozzle does not exist");
			if (!flags.HasPaymentType) return Info("Payment type does not exist");
			if (!flags.HasDispenser) return Info("Dispenser does not exist");

			return ServiceResponse<object>.Success("Data is valid", null);
		}

		// Underpayment guard — checks against RawCalculated (the true, unrounded
		// unitPrice * quantity, e.g. 999.45), NOT ctx.Calculated. ctx.Calculated
		// has already been capped by Math.Min(roundedRequested, ceilingCalculated)
		// in ResolveSaleContextAsync, so it is always <= what was sent by
		// construction — checking against it here would make this guard trivially
		// pass almost every time and stop catching genuine underpayments
		// (e.g. customer sends 500 against a 999.45 sale).
		private static bool ValidateTransactionAmount(SaleContext ctx)
			=> ctx.Requested >= ctx.RawCalculated
			   || Math.Abs(ctx.Requested - ctx.RawCalculated) <= 1.00m;

		public async Task<ServiceResponse<bool>> CheckDuplicates(AddsaleDto sales)
		{
			var cutoff = EatTime.Now.AddMinutes(-2);

			var exists = await _context.QuantityTransactions
				.AsNoTracking()
				.AnyAsync(p =>
					p.NozzleCode == sales.NozzleCode
					&& p.VehicleRegistrationNumber == sales.RegistrationNumber
					&& p.QuantityCredit == sales.Quantity
					&& p.DateCreated >= cutoff);

			return exists
				? ServiceResponse<bool>.Information("Duplicate payment detected (ignored).", false)
				: ServiceResponse<bool>.Success("No duplicate payment found.", true);
		}

		// =====================================================================
		// Pricing & M-Pesa validation
		// =====================================================================

		private async Task<(decimal Price, decimal Discount)> GetPriceByNozzleAsync(string nozzleCode)
		{
			var price = await (
				from n in _context.Nozzles.AsNoTracking()
				join p in _context.Prices.AsNoTracking() on n.PetroleumCode equals p.ProductCode
				where n.NozzleCode == nozzleCode
				select p.Amount
			).FirstOrDefaultAsync();

			return (price, 0m);
		}

		public async Task<ServiceResponse<MpesaManualConfirmationDto?>> ConfirmMpesaManualAsync(string transId, CancellationToken ct)
		{
			var tx = await _context.MpesaTransactions
				.AsNoTracking()
				.Where(t => t.TransID == transId && t.Status == 1)
				.FirstOrDefaultAsync(ct);

			if (tx is null)
				return ServiceResponse<MpesaManualConfirmationDto?>.Information(
					"Transaction not found or already used", null);

			return ServiceResponse<MpesaManualConfirmationDto?>.Success("Transaction verified successfully",
				new MpesaManualConfirmationDto
				(
					TransID: tx.TransID,
					Amount: tx.UsageBalance.ToString(),
					TillNumber: tx.TillNumber,
					Phone: tx.MSISDN
				));
		}


		private async Task<ServiceResponse<decimal>> GetTotalUsableMpesaAsync(IEnumerable<string?> transIds, string tillNumber)
		{
			decimal total = 0m;

			foreach (var id in transIds.Where(x => !string.IsNullOrWhiteSpace(x))!)
			{
				var r = await ValidateMpesaPaymentAsync(id!, tillNumber);

				if (r.ResponseCode != Response.Success)
					return ServiceResponse<decimal>.Information(r.ResponseMessage!, 0);

				total += r.ResponseObject ?? 0;
			}

			return ServiceResponse<decimal>.Success("Valid", total);
		}

		private async Task<ServiceResponse<int?>> ValidateMpesaPaymentAsync(string transId, string tillNumber)
		{
			try
			{
				var usage = await GetUsageBalanceAsync(transId);

				if (usage is null)
					return ServiceResponse<int?>.Information(
						$"Mpesa code {transId} does not exist", 0);

				var till = Regex.Replace(usage.TillNumber ?? string.Empty, @"\s+", "").Trim();

				if (!string.Equals(till, tillNumber.Trim(), StringComparison.OrdinalIgnoreCase))
					return ServiceResponse<int?>.Information(
						"Mpesa code does not belong to that dispenser", 0);

				if (usage.Amount <= 0)
					return ServiceResponse<int?>.Information(
						$"Mpesa code {transId} has already been fully used", 0);

				return ServiceResponse<int?>.Success($"Valid Mpesa Code {transId}.", usage.Amount);
			}
			catch (Exception ex)
			{
				return ServiceResponse<int?>.Error(
					$"Error validating Mpesa payment: {ex.Message}", 0);
			}
		}

		// ── FOR UPDATE row-locks this MpesaTransactions row for the life of the
		// ── surrounding DB transaction (see ExecuteSaleRawRefAsync's BeginTransactionAsync).
		// ── This prevents two concurrent sales against the same M-Pesa code from both
		// ── reading the pre-deduction balance and both passing validation (double-spend).
		// ── Status is intentionally NOT filtered here. Status flips to 0 once a code
		// ── is fully used (see ReconcileAndUpdateUsageBalanceAsync), and filtering on
		// ── it here would make an exhausted-but-real code look identical to one that
		// ── never existed. The Amount <= 0 check above is what should own that distinction.
		//
		// AsNoTracking() here is safe: this instance is only ever read then discarded
		// for validation — it is never mutated. The actual UPDATE happens later, in
		// ReconcileAndUpdateUsageBalanceAsync, via a separate tracked fetch. The row
		// lock itself (FOR UPDATE) is independent of EF's change-tracking and still
		// applies regardless of AsNoTracking().
		private async Task<UsageBalanceDto?> GetUsageBalanceAsync(string transId)
			=> await _context.MpesaTransactions
				.FromSqlInterpolated($@"
					SELECT * FROM ""MpesaTransactions""
					WHERE ""TransID"" = {transId}
					FOR UPDATE")
				.AsNoTracking()
				.Select(t => new UsageBalanceDto
				{
					Amount = (int)t.UsageBalance,
					TillNumber = t.TillNumber
				})
				.FirstOrDefaultAsync();

		private Task<decimal> GetOutstandingCreditAsync(string customerCode)
			=> _context.CreditTransactions
				.AsNoTracking()
				.Where(c => c.CustomerCode == customerCode)
				.SumAsync(c => c.Debit - c.Credit);

		// Current wallet balance for a vehicle (sum of credits minus debits on
		// CustomerTransactions). AsNoTracking — this is a read used purely to decide
		// whether the debit below is allowed; the debit itself is a fresh insert.
	

		// Takes a Postgres transaction-scoped advisory lock keyed on the vehicle code.
		// Released automatically on COMMIT/ROLLBACK of the surrounding transaction —
		// no separate unlock call needed. Serializes concurrent wallet sales (or a
		// wallet sale racing a wallet withdrawal/transfer, if those also take this
		// lock) against the same vehicle so the balance check above can be trusted.
	

		// =====================================================================
		// Data fetchers
		// =====================================================================

		private async Task<StationData> GetStationAsync(string dispenserCode)
		{
			var s = await (
				from sta in _context.Stations.AsNoTracking()
				join d in _context.Dispensers.AsNoTracking() on sta.StationCode equals d.StationCode
				join t in _context.Tills.AsNoTracking() on d.TillNumber equals t.TillNumber
				where d.DispenserCode == dispenserCode
				select new StationData
				{
					StationName = sta.StationName,
					StationCode = sta.StationCode,
					TillNumber = d.TillNumber,
					StoreNumber = t.StoreNumber
				}
			).FirstOrDefaultAsync();

			return s ?? new StationData();
		}

		private async Task<VehicleInfo> GetVehicleByRegAsync(string registrationNumber)
			=> await _context.Vehicles
				.AsNoTracking()
				.Where(v => v.VehicleRegistrationNumber == registrationNumber)
				.Select(v => new VehicleInfo
				{
					VehicleRegistration = v.VehicleRegistrationNumber,
					PhoneNumber = v.PhoneNumber,
					PhoneNumber2 = v.PhoneNumber2
				})
				.FirstOrDefaultAsync()
			?? new VehicleInfo { VehicleRegistration = registrationNumber };

		private async Task<CustomerInfo> GetCustomerByPhoneAsync(string phone)
			=> await _context.Customers
				.AsNoTracking()
				.Where(c => c.CustomerPhone == phone)
				.Select(c => new CustomerInfo
				{
					CustomerName = c.CustomerName,
					CustomerPhone = c.CustomerPhone,
					CustomerEmail = c.CustomerEmail,
					CustomerCode = c.CustomerCode,
					Receive_Receipts = c.Receive_Receipts,
					Receive_Statements = c.Receive_Statements,
					IsCreditCustomer = c.IsCreditCustomer,
					CreditLimit = c.CreditLimit
				})
				.FirstOrDefaultAsync() ?? new CustomerInfo();

		// =====================================================================
		// Entity builder
		// =====================================================================

		private QuantityTransactions BuildQuantityTransaction(
			AddsaleDto sales, SaleContext ctx, string saleId)
			=> new()
			{
				ShiftNumber = sales.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleRegistrationNumber = sales.RegistrationNumber,
				QuantityCredit = sales.Quantity,
				QuantityDebit = 0,
				AmountCredit = ctx.Calculated,
				AmountDebit = 0,
				DispenserCode = sales.DispenserCode,
				NozzleCode = sales.NozzleCode,
				StationCode = ctx.Station.StationCode,
				DateCreated = EatTime.Now,
				IsReversed = false,
				PaymentTypeCode = sales.PaymentTypeCode,
				SaleId = saleId,
				Price = ctx.UnitPrice,
				Vat_Amount = 0,
				Discount = ctx.Discount,
				OtpUsed = sales.OtpUsed ?? string.Empty,
				CustomerCode = sales.LoyaltyCustomerCode ?? string.Empty
			};

		// =====================================================================
		// Loyalty
		// =====================================================================

		private async Task SafeAwardPointsAsync(AddsaleDto sales, string saleId)
		{
			if (!sales.IsLoyalCustomer || string.IsNullOrWhiteSpace(sales.LoyaltyPhone)) return;

			try
			{
				var customerCode = await _context.Customers
					.AsNoTracking()
					.Where(x => x.CustomerPhone == sales.LoyaltyPhone)
					.Select(x => x.CustomerCode)
					.FirstOrDefaultAsync();

				if (string.IsNullOrEmpty(customerCode)) return;

				var pointsEarned = sales.Quantity * sales.BaseLoyaltyPoints;
				await _loyalty.AddLoyaltyPoints(customerCode, pointsEarned, saleId);
			}
			catch { /* loyalty failure must never undo a committed sale */ }
		}

		// =====================================================================
		// Utilities
		// =====================================================================

		private static string FirstName(string? fullName)
			=> (fullName ?? "Customer").Split(' ')[0];

		private static string BuildSms(SaleContext ctx, string body)
			=> $"Dear {FirstName(ctx.Customer.CustomerName)}, {body}";

		private static string UtcStamp()
			=> $"{EatTime.Now:dd/MM/yy} {EatTime.Now:hh:mm tt}";

		private static ServiceResponse<object> Info(string message)
			=> ServiceResponse<object>.Information(message, null);
	}

	// =========================================================================
	// Small models
	// =========================================================================

	public class StationData
	{
		public string StationName { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		public string TillNumber { get; set; } = string.Empty;
		public string StoreNumber { get; set; } = string.Empty;
	}

	public class VehicleInfo
	{
		public string VehicleRegistration { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string PhoneNumber2 { get; set; } = string.Empty;
	}

	public class CustomerInfo
	{
		public string CustomerName { get; set; } = string.Empty;
		public string CustomerPhone { get; set; } = string.Empty;
		public string CustomerEmail { get; set; } = string.Empty;
		public string CustomerCode { get; set; } = string.Empty;
		public bool Receive_Receipts { get; set; }
		public bool Receive_Statements { get; set; }
		public bool IsCreditCustomer { get; set; }
		public decimal CreditLimit { get; set; }
	}

	public record MpesaManualConfirmationDto(
		string TransID,
		string Amount,
		string TillNumber,
		string Phone);
}