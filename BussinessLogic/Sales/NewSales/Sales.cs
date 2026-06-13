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
using DataAccessLayer.EntityModels.Personal_Wallet;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace BussinessLogic.Sales.NewSales
{
	// =========================================================================
	// Immutable context — built once per transaction, threaded through the pipeline
	// =========================================================================

	internal sealed record SaleContext(
		StationData Station,
		Vehicle Vehicle,
		Customer Customer,
		decimal UnitPrice,
		decimal Discount,
		decimal Calculated,    // effective amount — what gets recorded
		decimal Requested,     // what the customer actually paid
		string TransactionRef
	);

	internal delegate Task<ServiceResponse<object>?> PaymentStepAsync(
		AddsaleDto sales, SaleContext ctx, string saleId);

	// =========================================================================
	// Sales service
	// =========================================================================

	public class Sales : ISales
	{
		private readonly ILoyaltyServices _loyalty;
		private readonly IMemoryCache _cache;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IAfricaIsTalking _isTalking;
		private readonly ReceiptService _receipt;

		public Sales(
			OTOContext context,
			ICommonSetups setups,
			IAuthCommonTasks authentication,
			ICommonSalesTasks salesTasks,
			IAfricaIsTalking isTalking,
			IMemoryCache cache,
			ReceiptService receipt,
			ILoyaltyServices loyalty)
		{
			_context = context;
			_setups = setups;
			_authentication = authentication;
			_salesTasks = salesTasks;
			_isTalking = isTalking;
			_cache = cache;
			_receipt = receipt;
			_loyalty = loyalty;
		}

		// =====================================================================
		// Entry point
		// =====================================================================

		public async Task<ServiceResponse<object>> AddSalesAsync(AddsaleDto sales)
		{
			var saleId = _setups.GenerateSaleId();

			if (await _context.QuantityTransactions.AnyAsync(q => q.SaleId == saleId))
				return Info("Duplicate sale detected, try again");

			var precheck = await ValidateDataAsync(sales);
			if (precheck.ResponseCode == Response.Information)
				return precheck;

			return sales.PaymentTypeCode switch
			{
				PaymetMethod.Mpesa => await HandleMpesaAsync(sales, saleId),
				PaymetMethod.Wallet => await HandleWalletAsync(sales, saleId),
				PaymetMethod.Voucher => await HandleVoucherAsync(sales, saleId),
				PaymetMethod.Personal_Wallet => await HandlePersonalWalletAsync(sales, saleId),
				PaymetMethod.Cash => await HandleCashAsync(sales, saleId),
				PaymetMethod.Credit => await HandleCreditAsync(sales, saleId),
				PaymetMethod.Loyalty => await HandleLoyaltyAsync(sales, saleId),
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
			string? receiptStationOverride = null)
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

					var ctx = await ResolveSaleContextAsync(sales, txRef);

					var abort = await paymentStep(sales, ctx, saleId);
					if (abort is not null) return abort;

					if (!ValidateTransactionAmount(ctx.Calculated, ctx.Requested))
						return Info("Transaction amount does not match Quantity x Price");

					StageReceipt(ctx, sales, receiptPaymentMethod,
						stationNameOverride: receiptStationOverride);

					var mpesaRefs = await PersistSaleAsync(sales, ctx, saleId);

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					// ── Clear tracker so reconcile reads fresh committed data ──
					_context.ChangeTracker.Clear();

					foreach (var transId in mpesaRefs)
						await ReconcileAndUpdateUsageBalanceAsync(transId);

					if (mpesaRefs.Count > 0)
					{
						var saved = await _context.SaveChangesAsync();
						Console.WriteLine($"[Reconcile] SaveChangesAsync saved {saved} rows");
					}

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
			string? mpesaStoreNumber = null,
			string? receiptStationOverride = null)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();
				_context.Database.AutoSavepointsEnabled = false;

				try
				{
					var ctx = await ResolveSaleContextAsync(sales, saleId);

					var abort = await paymentStep(sales, ctx, saleId);
					if (abort is not null) return abort;

					if (!ValidateTransactionAmount(ctx.Calculated, ctx.Requested))
						return Info("Transaction amount does not match Quantity x Price");

					StageReceipt(ctx, sales, receiptPaymentMethod,
						stationNameOverride: receiptStationOverride);

					var mpesaRefs = await PersistSaleAsync(sales, ctx, saleId,
						mpesaStoreNumber: mpesaStoreNumber);

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					// ── Clear tracker so reconcile reads fresh committed data ──
					_context.ChangeTracker.Clear();

					foreach (var transId in mpesaRefs)
						await ReconcileAndUpdateUsageBalanceAsync(transId);

					if (mpesaRefs.Count > 0)
					{
						var saved = await _context.SaveChangesAsync();
						Console.WriteLine($"[Reconcile] SaveChangesAsync saved {saved} rows");
					}

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
					var customer = await _context.Customers
						.Where(c => c.CustomerCode == ctx.Customer.CustomerCode
								 && c.IsCreditCustomer)
						.FirstOrDefaultAsync();

					if (customer is null)
						return Info("This customer is not approved for credit purchases.");

					var outstanding = await GetOutstandingCreditAsync(customer.CustomerCode);
					var newExposure = outstanding + ctx.Calculated;

					if (newExposure > customer.CreditLimit)
						return ServiceResponse<object>.Information(
							$"Credit limit exceeded. Limit: {customer.CreditLimit:N2}, " +
							$"Outstanding: {outstanding:N2}, This sale: {ctx.Calculated:N2}",
							new { customer.CreditLimit, Outstanding = outstanding });

					_context.CreditTransactions.Add(new CreditTransactions
					{
						CustomerCode = customer.CustomerCode,
						Credit = 0,
						Debit = ctx.Calculated,
						SaleId = sid,
						TransactionReference = ctx.TransactionRef,
						VehicleCode = s.VehicleCode,
						StationCode = ctx.Station.StationCode,
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode()
					});

					var remainingCredit = customer.CreditLimit - newExposure;
					StageQueuedSms(ctx.Vehicle.PhoneNumber, BuildSms(ctx,
						$"a credit sale of KES {ctx.Calculated:N2} for {s.Quantity:N2} litres " +
						$"has been recorded for vehicle {ctx.Vehicle.VehicleRegistration} " +
						$"at {ctx.Station.StationName} on {UtcStamp()}. " +
						$"Remaining credit: KES {remainingCredit:N2}."));

					return null;
				}
			);
		}

		private Task<ServiceResponse<object>> HandleWalletAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "WALLET SALE",
				receiptPaymentMethod: "Wallet",
				awardLoyalty: false,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, sid) =>
				{
					if (await IsWalletDormantAsync(
						_context.CustomerTransactions.Where(w => w.VehicleCode == s.VehicleCode)))
						return Info(DormantWalletMessage);

					var effectiveBal = await GetCustomerBalanceAsync(s.VehicleCode)
									 + ctx.Vehicle.CreditLimit;

					if (effectiveBal < ctx.Requested)
						return ServiceResponse<object>.Information("Insufficient balance.", effectiveBal);

					await AddCustomerTransactionAsync(s.VehicleCode, ctx.Requested, sid, s.DispenserCode);

					var newBalance = await GetCustomerBalanceAsync(s.VehicleCode);
					StageQueuedSms(ctx.Vehicle.PhoneNumber, BuildSms(ctx,
						$"{ctx.Requested:N2} KES has been deducted from the wallet " +
						$"for {s.Quantity:N2} litres at {ctx.Station.StationName} " +
						$"on {UtcStamp()}. New balance: {newBalance:N2}."));

					return null;
				}
			);
		}

		private Task<ServiceResponse<object>> HandlePersonalWalletAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "WALLET SALE",
				receiptPaymentMethod: "Wallet",
				awardLoyalty: false,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, sid) =>
				{
					if (await IsWalletDormantAsync(
						_context.Wallet_Transactions_Personal.Where(w => w.VehicleCode == s.VehicleCode)))
						return Info(DormantWalletMessage);

					var walletBalance = await GetPersonalWalletBalanceAsync(s.WalletId ?? string.Empty);
					var effectiveBal = walletBalance + ctx.Vehicle.CreditLimit;

					if (effectiveBal < ctx.Calculated)
						return ServiceResponse<object>.Information("Insufficient wallet balance", effectiveBal);

					_context.Wallet_Transactions_Personal.Add(new Wallet_Transactions_Personal
					{
						Credit = 0,
						Debit = ctx.Requested,
						TransactionType = "0",
						TransactionCode = await _setups.GetCodeGenerator("TransactionId"),
						WalletId = s.WalletId ?? string.Empty,
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode(),
						Description = $"Fuel purchase at {ctx.Station.StationName}",
						SaleId = ctx.TransactionRef,
						VehicleCode = s.VehicleCode,
						PhoneNumber = ctx.Vehicle.PhoneNumber ?? string.Empty
					});

					var newBalance = await GetCustomerBalanceAsync(s.VehicleCode);
					StageQueuedSms(ctx.Vehicle.PhoneNumber, BuildSms(ctx,
						$"KES {ctx.Requested:N2} has been deducted from your wallet " +
						$"for {s.Quantity:N2} litres at {ctx.Station.StationName} " +
						$"on {UtcStamp()} for vehicle {ctx.Vehicle.VehicleRegistration}. " +
						$"New balance: KES {newBalance:N2}. Thank you!"));

					return null;
				}
			);
		}

		private Task<ServiceResponse<object>> HandleVoucherAsync(AddsaleDto sales, string saleId)
		{
			return ExecuteSaleAsync(
				sales, saleId,
				operationType: "VOUCHER SALE",
				receiptPaymentMethod: "Voucher",
				awardLoyalty: true,
				generateRef: _ => Task.FromResult(_setups.GenerateSaleId()),
				paymentStep: async (s, ctx, _) =>
				{
					var voucherNo = s.PaymentDetails.First().TransactionReference?.Trim();
					var voucher = await _context.Vouchers
						.FirstOrDefaultAsync(v => v.VoucherNo == voucherNo);

					if (voucher is null) return Info("Invalid voucher number.");
					if (voucher.IsUsed) return Info("This voucher has already been used.");
					if (voucher.ExpiryDate < DateTime.UtcNow) return Info("This voucher has expired.");
					if (voucher.VehicleCode != s.VehicleCode) return Info("This voucher is not valid for this vehicle.");
					if (voucher.Amount != ctx.Requested) return Info("The voucher must be used once for the full amount.");

					voucher.IsUsed = true;

					var loyaltySub = await _context.LoyaltySubscriptions
						.FirstOrDefaultAsync(sub =>
							sub.VehicleCode == s.VehicleCode
							&& !sub.IsRewardClaimed
							&& sub.OtpCode == voucherNo);

					if (loyaltySub is not null)
					{
						loyaltySub.IsRewardClaimed = true;
						loyaltySub.RewardClaimedDate = DateTime.UtcNow;
					}

					StageQueuedSms(ctx.Vehicle.PhoneNumber,
						$"A voucher sale of {s.Quantity:N2} litres for {ctx.Requested:N2} Ksh " +
						$"was completed using voucher {voucher.VoucherNo} at {ctx.Station.StationName}.");

					return null;
				}
			);
		}

		private async Task<ServiceResponse<object>> HandleMpesaAsync(AddsaleDto sales, string saleId)
		{
			var mpesaCodes = sales.PaymentDetails
				.Where(p => p.TransactionReference?.Trim().Length == 10)
				.Select(p => p.TransactionReference!.Trim())
				.ToList();

			if (mpesaCodes.Count == 0)
				return Info("No valid M-Pesa codes provided");

			var dupCheck = await CheckDuplicates(sales);
			if (dupCheck.ResponseCode == Response.Information)
				return Info("Duplicate M-Pesa codes found in the transaction");

			var station = await GetStationAsync(sales.DispenserCode);

			return await ExecuteSaleRawRefAsync(
				sales, saleId,
				operationType: "MPESA SALE",
				receiptPaymentMethod: "M-Pesa",
				awardLoyalty: true,
				mpesaStoreNumber: station.StoreNumber,
				receiptStationOverride: _setups.SentenceCase(station.StationName),
				paymentStep: async (s, ctx, _) =>
				{
					var usableBal = await GetTotalUsableMpesaAsync(
						s.PaymentDetails.Select(p => p.TransactionReference),
						ctx.Station.TillNumber);

					if (usableBal < ctx.Requested)
						return Info("Insufficient funds, cannot complete the transaction");

					if (ctx.Vehicle.ProductCode is "04" or "05")
					{
						var name = _setups.SentenceCase(
							ctx.Customer.CustomerName?.Split(' ').FirstOrDefault() ?? "Customer");
						StageQueuedSms(ctx.Customer.CustomerPhone,
							$"Dear {name}, your M-Pesa payment of {ctx.Requested:N2} " +
							$"has been received for {s.Quantity:N2} litres for vehicle " +
							$"{ctx.Vehicle.VehicleRegistration} at " +
							$"{_setups.SentenceCase(ctx.Station.StationName)} " +
							$"on {DateTime.UtcNow:yyyy-MMM-dd} at {DateTime.UtcNow:HH:mm}. Thank you!");
					}

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

					StageQueuedSms(ctx.Vehicle.PhoneNumber, BuildSms(ctx,
						$"a loyalty points redemption of {pointsToDeduct:N2} points " +
						$"(KES {ctx.Calculated:N2}) for {s.Quantity:N2} litres " +
						$"has been processed for vehicle {ctx.Vehicle.VehicleRegistration} " +
						$"at {ctx.Station.StationName} on {UtcStamp()}. " +
						$"Remaining points: {remainingPoints:N2} (KES {remainingValue:N2})."));

					return null;
				}
			);
		}

		// =====================================================================
		// Context resolution
		// =====================================================================

		private async Task<SaleContext> ResolveSaleContextAsync(
			AddsaleDto sales, string transactionRef)
		{
			var station = await GetStationAsync(sales.DispenserCode);
			var vehicle = await GetVehicleAsync(sales.VehicleCode);
			var customer = await GetCustomerAsync(sales.VehicleCode);
			var (unitPrice, disc) = await GetPriceAsync(
				sales.ProductCode, station.StationCode, sales.VehicleCode);

			var requested = Math.Round(sales.PaymentDetails.Sum(x => x.TransactionAmount), 2);
			var calculated = Math.Round(unitPrice * sales.Quantity, 2);

			// If customer paid within 1 KES of calculated, use what they paid
			var effective = (requested >= calculated && requested - calculated <= 1.00m)
				? requested
				: calculated;

			return new SaleContext(
				station, vehicle, customer, unitPrice, disc,
				Calculated: effective,
				Requested: requested,
				TransactionRef: transactionRef
			);
		}

		// =====================================================================
		// Persist rows — returns M-Pesa refs for post-commit reconciliation
		// =====================================================================

		private async Task<List<string>> PersistSaleAsync(
			AddsaleDto sales, SaleContext ctx, string saleId,
			string? mpesaStoreNumber = null)
		{
			_context.QuantityTransactions.Add(
				BuildQuantityTransaction(sales, ctx, saleId));

			decimal remaining = ctx.Calculated;
			var mpesaRefs = new List<string>();

			foreach (var pay in sales.PaymentDetails)
			{
				if (remaining <= 0) break;

				decimal alloc = Math.Min(remaining, Math.Round(pay.TransactionAmount, 2));

				if (!string.IsNullOrWhiteSpace(mpesaStoreNumber)
					&& !string.IsNullOrWhiteSpace(pay.TransactionReference))
				{
					var check = await ValidateMpesaPaymentAsync(
						pay.TransactionReference, mpesaStoreNumber);
					alloc = Math.Min(alloc, Math.Max(0, check.ResponseObject ?? 0));
				}

				_context.PaymentTransactions.Add(new PaymentTransactions
				{
					PaymentRefrence = pay.TransactionReference,
					TransactionAmount = alloc,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					SaleId = saleId,
					TransactionAmountDebit = 0
				});

				if (!string.IsNullOrWhiteSpace(pay.TransactionReference))
					mpesaRefs.Add(pay.TransactionReference);

				remaining -= alloc;
			}

			return mpesaRefs;
		}

		// =====================================================================
		// M-Pesa usage balance reconciliation — runs AFTER SaveChanges + Commit
		// =====================================================================

		private async Task ReconcileAndUpdateUsageBalanceAsync(string transId)
		{
			var mpesaTx = await _context.MpesaTransactions
				.FirstOrDefaultAsync(t => t.TransID == transId);

			if (mpesaTx is null)
			{
				Console.WriteLine($"[Reconcile] ❌ MpesaTransaction not found for TransID={transId}");
				return;
			}

			var saleIds = await _context.PaymentTransactions
				.Where(p => p.PaymentRefrence == transId)
				.Select(p => p.SaleId)
				.Distinct()
				.ToListAsync();

			Console.WriteLine($"[Reconcile] TransID={transId} SaleIds=[{string.Join(",", saleIds)}]");

			var totalUsed = saleIds.Count == 0
				? 0m
				: await _context.QuantityTransactions
					.Where(q => saleIds.Contains(q.SaleId) && !q.IsReversed)
					.SumAsync(q => q.AmountCredit);

			Console.WriteLine($"[Reconcile] TransAmount={mpesaTx.TransAmount} TotalUsed={totalUsed}");

			mpesaTx.UsageBalance = Math.Max(0, mpesaTx.TransAmount - totalUsed);
			mpesaTx.Status = mpesaTx.UsageBalance <= 0 ? 0 : 1;
			mpesaTx.DateModified = DateTime.UtcNow;

			_context.Entry(mpesaTx).State = EntityState.Modified;

			Console.WriteLine($"[Reconcile] ✅ NewUsageBalance={mpesaTx.UsageBalance} Status={mpesaTx.Status}");
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
				PhoneNumber = ctx.Vehicle.PhoneNumber ?? string.Empty,
				TotalAmount = (double)ctx.Requested,
				DateCreated = DateTime.UtcNow,
				Duplicate = 0,
				VehicleReg = ctx.Vehicle.VehicleRegistration,
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
				DateCreated = DateTime.UtcNow,
				DateSent = DateTime.UtcNow,
				IsSent = false,
				Message = message,
				PhoneNumber = phone,
				ScheduledSendingdate = DateTime.UtcNow,
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
				$"Nozzle={sales.NozzleCode} | Vehicle={ctx.Vehicle.VehicleRegistration} | " +
				$"Refs={refsStr} | At={DateTime.UtcNow:yyyy/MM/dd HH:mm:ss} | " +
				$"User={_authentication.Usercode()}";

			await _authentication.AddUserTrail(msg, operationType);
		}

		// =====================================================================
		// Validation
		// =====================================================================

		private async Task<ServiceResponse<object>> ValidateDataAsync(AddsaleDto sales)
		{
			if (sales?.PaymentDetails is null || sales.PaymentDetails.Count == 0)
				return Info("Invalid sales payload");

			if (sales.PaymentTypeCode == PaymetMethod.Mpesa && sales.PaymentDetails.Count > 2)
				return Info(
					$"Hi {_authentication.Username().Split(',')[0]}, " +
					$"more than two Mpesa codes is not allowed");

			(string msg, IQueryable<bool> query)[] checks =
			[
				("Shift does not exist",
					_context.Shifts.Where(x => x.ShiftNumber == sales.ShiftNumber).Select(_ => true)),
				("Vehicle does not exist",
					_context.Vehicles.Where(x => x.VehicleCode == sales.VehicleCode).Select(_ => true)),
				("Nozzle does not exist",
					_context.Nozzles.Where(x => x.NozzleCode == sales.NozzleCode).Select(_ => true)),
				("Payment type does not exist",
					_context.PaymentTypes.Where(x => x.PaymentTypeId == sales.PaymentTypeCode).Select(_ => true)),
				("Dispenser does not exist",
					_context.Dispensers.Where(x => x.DispenserCode == sales.DispenserCode).Select(_ => true)),
			];

			foreach (var (msg, query) in checks)
				if (!await query.AnyAsync())
					return Info(msg);

			return ServiceResponse<object>.Success("Data is valid", null);
		}

		// ── Allow up to 1 KES rounding tolerance ─────────────────────────────
		private static bool ValidateTransactionAmount(decimal calculated, decimal entered)
			=> entered >= calculated || Math.Abs(entered - calculated) <= 1.00m;

		public async Task<ServiceResponse<bool>> CheckDuplicates(AddsaleDto sales)
		{
			var cutoff = DateTime.UtcNow.AddMinutes(-2);

			var exists = await _context.QuantityTransactions.AnyAsync(p =>
				p.NozzleCode == sales.NozzleCode
				&& p.VehicleCode == sales.VehicleCode
				&& p.QuantityCredit == sales.Quantity
				&& p.DateCreated >= cutoff);

			return exists
				? ServiceResponse<bool>.Information("Duplicate payment detected (ignored).", false)
				: ServiceResponse<bool>.Success("No duplicate payment found.", true);
		}

		private static async Task<bool> IsWalletDormantAsync<T>(IQueryable<T> q) where T : class
		{
			var last = await q
				.OrderByDescending(w => EF.Property<DateTime>(w, "DateCreated"))
				.Select(w => (DateTime?)EF.Property<DateTime>(w, "DateCreated"))
				.FirstOrDefaultAsync();

			return last.HasValue && last.Value < DateTime.UtcNow.AddDays(-30);
		}

		// =====================================================================
		// Pricing & M-Pesa validation
		// =====================================================================

		public async Task<(decimal NewPrice, decimal Discount)> GetPriceAsync(
			string productCode, string stationCode, string vehicleCode)
		{
			var basePrice = await _context.Prices
				.Where(p => p.ProductCode == productCode && p.StationCode == stationCode)
				.Select(p => p.Amount)
				.FirstOrDefaultAsync();

			var discount = await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => v.Discount)
				.FirstOrDefaultAsync();

			return (basePrice - discount, discount);
		}


		public async Task<ServiceResponse<MpesaManualConfirmationDto?>> ConfirmMpesaManualAsync(string transId, CancellationToken ct)
		{
			var tx = await _context.MpesaTransactions
				.Where(t => t.TransID == transId && t.Status == 1)
				.FirstOrDefaultAsync(ct);

			if (tx is null)
				return ServiceResponse<MpesaManualConfirmationDto?>.Information(
					"Transaction not found or already used", null);

			return ServiceResponse<MpesaManualConfirmationDto?>.Success("Transaction verified successfully",
				new MpesaManualConfirmationDto(
					TransID: tx.TransID,
					Amount: tx.UsageBalance.ToString(),
					TillNumber: tx.TillNumber,
					Phone: tx.MSISDN
				));
		}
		private async Task<decimal> GetTotalUsableMpesaAsync(
			IEnumerable<string?> transIds, string storeNumber)
		{
			decimal total = 0m;

			foreach (var id in transIds.Where(x => !string.IsNullOrWhiteSpace(x))!)
			{
				var r = await ValidateMpesaPaymentAsync(id!, storeNumber);
				if (r.ResponseCode == Response.Success && r.ResponseObject.HasValue)
					total += r.ResponseObject.Value;
			}

			return total;
		}

		private async Task<ServiceResponse<int?>> ValidateMpesaPaymentAsync(
			string transId, string storeNumber)
		{
			try
			{
				var usage = await GetUsageBalanceAsync(transId);

				if (usage is null)
					return ServiceResponse<int?>.Information(
						$"Mpesa code {transId} does not exist", 0);

				var store = Regex.Replace(
					usage.StoreNumber ?? string.Empty, @"\s+", "").Trim();

				if (!string.Equals(store, storeNumber.Trim(), StringComparison.OrdinalIgnoreCase))
					return ServiceResponse<int?>.Information(
						"Mpesa code does not belong to that dispenser", 0);

				if (usage.Amount <= 0)
					return ServiceResponse<int?>.Information("Mpesa code has 0 balance", 0);

				return ServiceResponse<int?>.Success($"Valid Mpesa Code {transId}.", usage.Amount);
			}
			catch (Exception ex)
			{
				return ServiceResponse<int?>.Error(
					$"Error validating Mpesa payment: {ex.Message}", 0);
			}
		}

		private async Task<UsageBalanceDto?> GetUsageBalanceAsync(string transId)
		{
			return await _context.MpesaTransactions
				.Where(t => t.TransID == transId && t.Status == 1)
				.Select(t => new UsageBalanceDto
				{
					Amount = (int)t.UsageBalance,
					StoreNumber = t.TillNumber
				})
				.FirstOrDefaultAsync();
		}

		private Task<decimal> GetOutstandingCreditAsync(string customerCode)
			=> _context.CreditTransactions
				.Where(c => c.CustomerCode == customerCode)
				.SumAsync(c => c.Debit - c.Credit);

		// =====================================================================
		// Data fetchers
		// =====================================================================

		private async Task<StationData> GetStationAsync(string dispenserCode)
		{
			var s = await (
				from sta in _context.Stations
				join d in _context.Dispensers on sta.StationCode equals d.StationCode
				join t in _context.Tills on d.TillNumber equals t.TillNumber
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

		private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
			=> await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => new Vehicle
				{
					ProductCode = v.ProductCode,
					VehicleRegistration = v.VehicleRegistrationNumber,
					CreditLimit = v.CreditLimit,
					PhoneNumber = v.PhoneNumber,
					PhoneNumber2 = v.PhoneNumber2,
					Discount = v.Discount
				})
				.FirstOrDefaultAsync() ?? new Vehicle();

		private async Task<Customer> GetCustomerAsync(string vehicleCode)
			=> await (
				from cust in _context.Customers
				join v in _context.Vehicles on cust.CustomerCode equals v.CustomerCode
				where v.VehicleCode == vehicleCode
				select new Customer
				{
					CustomerName = cust.CustomerName,
					CustomerPhone = cust.CustomerPhone,
					CustomerEmail = cust.CustomerEmail,
					Receive_Receipts = cust.Receive_Receipts,
					CustomerCode = cust.CustomerCode,
					Receive_Statements = cust.Receive_Statements
				}
			).FirstOrDefaultAsync() ?? new Customer();

		private Task<decimal> GetCustomerBalanceAsync(string vehicleCode)
			=> _context.CustomerTransactions
				.Where(x => x.VehicleCode == vehicleCode)
				.SumAsync(x => x.Credit - x.Debit);

		private Task<decimal> GetPersonalWalletBalanceAsync(string walletId)
			=> _context.Wallet_Transactions_Personal
				.Where(x => x.WalletId == walletId)
				.SumAsync(x => x.Credit - x.Debit);

		private Task<decimal> GetLoyaltyPointsBalanceAsync(string customerCode)
			=> _loyalty.GetPointsBalance(customerCode);

		// =====================================================================
		// Entity builder
		// =====================================================================

		private QuantityTransactions BuildQuantityTransaction(
			AddsaleDto sales, SaleContext ctx, string saleId)
			=> new()
			{
				ShiftNumber = sales.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleCode = sales.VehicleCode,
				QuantityCredit = sales.Quantity,
				QuantityDebit = 0,
				AmountCredit = ctx.Calculated,
				AmountDebit = 0,
				DispenserCode = sales.DispenserCode,
				NozzleCode = sales.NozzleCode,
				StationCode = ctx.Station.StationCode,
				DateCreated = DateTime.UtcNow,
				IsReversed = false,
				PaymentTypeCode = sales.PaymentTypeCode,
				SaleId = saleId,
				Price = ctx.UnitPrice,
				Vat_Amount = 0,
				Discount = ctx.Discount,
				OtpUsed = sales.OtpUsed ?? string.Empty
			};

		// =====================================================================
		// Customer wallet debit
		// =====================================================================

		private async Task AddCustomerTransactionAsync(
			string vehicleCode, decimal debitAmount,
			string saleId, string dispenserCode)
		{
			var station = await GetStationAsync(dispenserCode);

			await _context.Database.ExecuteSqlRawAsync(
				"EXEC InsertCustomerTransaction @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9",
				vehicleCode, 0, debitAmount, saleId, DateTime.UtcNow,
				string.Empty, _authentication.Usercode(), 2, 0,
				$"Fueled at {station.StationName} station");
		}

		// =====================================================================
		// Loyalty
		// =====================================================================

		private async Task SafeAwardPointsAsync(AddsaleDto sales, string saleId)
		{
			if (!sales.IsLoyalCustomer || string.IsNullOrWhiteSpace(sales.LoyaltyPhone)) return;

			try
			{
				var customerCode = await _context.Customers
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
			=> $"{DateTime.UtcNow:dd/MM/yy} {DateTime.UtcNow:hh:mm tt}";

		private static ServiceResponse<object> Info(string message)
			=> ServiceResponse<object>.Information(message, null);

		private const string DormantWalletMessage =
			"This wallet account has been dormant for more than 30 days. " +
			"Please contact your supervisor for support.";
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

	public class Vehicle
	{
		public string ProductCode { get; set; } = string.Empty;
		public string VehicleRegistration { get; set; } = string.Empty;
		[Precision(18, 2)] public decimal CreditLimit { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public string PhoneNumber2 { get; set; } = string.Empty;
		[Precision(18, 2)] public decimal Discount { get; set; }
	}

	public record MpesaManualConfirmationDto(
	string TransID,
	string Amount,
	string TillNumber,
	string Phone);
}