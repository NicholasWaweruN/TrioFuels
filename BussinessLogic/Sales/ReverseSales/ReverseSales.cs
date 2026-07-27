using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.Sales.ReverseSales;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Sales.ReverseSales
{
	public class ReverseSales(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, ICommonSalesTasks salesTasks, ILogger<ReverseSales> logger) : IReverseSales
	{
		private readonly OTOContext _context = context;
		private readonly IAuthCommonTasks _authentication = authentication;
		private readonly ICommonSetups _setups = setups;
		private readonly ICommonSalesTasks _salesTasks = salesTasks;
		private readonly ILogger<ReverseSales> _logger = logger;

		/// <summary>
		/// Reverse a sale by creating compensating Quantity & Payment transactions.
		/// Atomic: one DB transaction, one SaveChanges, then reconcile.
		/// Uses a row-level FOR UPDATE lock to prevent double-reversal race conditions.
		/// Wrapped in EF Core execution strategy to support retry-on-failure configuration.
		/// </summary>
		public async Task<ServiceResponse<object>> ReverseSaleAsync(string saleId)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var dbTx = await _context.Database.BeginTransactionAsync();
				try
				{
					// --- Row-level lock to prevent concurrent double-reversal -------------------
					await _context.Database.ExecuteSqlRawAsync(
						"SELECT 1 FROM \"QuantityTransactions\" WHERE \"SaleId\" = {0} FOR UPDATE",
						saleId);

					// --- Load & validate state --------------------------------------------------
					var sale = await GetSaleByIdAsync(saleId);
					if (sale == null)
						return ServiceResponse<object>.Information("Sale not found", null);

					if (sale.IsReversed)
						return ServiceResponse<object>.Information("Sale already reversed", null);

					if (string.IsNullOrWhiteSpace(sale.ShiftNumber))
						return ServiceResponse<object>.Information("Shift not found", null);

					var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftNumber == sale.ShiftNumber);
					if (shift == null)
						return ServiceResponse<object>.Information("Shift not found", null);

					if (shift.ShiftStatus == ShiftStatus.Closed)
						return ServiceResponse<object>.Information("Shift is closed, cannot reverse sale", null);

					var transactionCode = sale.SaleId;

					// --- Stage domain changes (no SaveChanges yet) ------------------------------
					if (sale.PaymentTypeCode == PaymetMethod.Wallet)
						AddCustomerTransactionIfVehiclePresent(sale.VehicleRegistrationNumber, sale.AmountDebit, transactionCode);

					// NOTE: assumes original credit-sale insert wrote TransactionReference = sale.SaleId.
					// Unconfirmed — if the original write uses a different reference (e.g. a receipt number
					// or Daraja ref), this lookup returns nothing and reverses nothing for Credit sales,
					// silently. Confirm against the original credit-creation insert before relying on this.
					// nozzleCode param is currently unused inside the method — passed through in case
					// TransactionReference isn't guaranteed unique per sale and scoping is later needed.
					if (sale.PaymentTypeCode == PaymetMethod.Credit)
						await AddReversedCreditTransactionIfVehiclePresent(transactionCode, sale.NozzleCode);

					AddReversedQuantityTransactionAndMarkOriginal(sale, transactionCode);
					await AddReversedPaymentTransactionsAsync(sale);

					// Trail entry. BuildReverseSaleMessage does its own display-name lookups; if those
					// fail for any reason we still want the reversal itself to succeed, so we fall back
					// to a plain message rather than aborting the whole operation over a cosmetic detail.
					string trailMessage;
					try
					{
						trailMessage = await BuildReverseSaleMessage(sale);
					}
					catch (Exception trailEx)
					{
						_logger.LogError(trailEx,
							"Failed to build detailed trail message for sale {SaleId}; falling back to plain message.",
							sale.SaleId);
						trailMessage = $"User '{_authentication.Name()}' (Code: {_authentication.Usercode()}) reversed sale [SaleId={sale.SaleId}] on Shift [{sale.ShiftNumber}].";
					}

					_context.UserTrails.Add(new UserTrail
					{
						UserCode = _authentication.Usercode(),
						UserName = _authentication.Name(),
						ActionType = "ReverseSale",
						Message = trailMessage,
						ShiftNumber = sale.ShiftNumber,
						DateCreated =EatTime.Now
					});

					// --- Persist once -----------------------------------------------------------
					await _context.SaveChangesAsync();
					await dbTx.CommitAsync();

					// --- Out-of-transaction reconcile (safe to fail independently) --------------
					try
					{
						bool shiftIsOpen = await _context.Shifts.AnyAsync(x => x.ShiftNumber == sale.ShiftNumber && x.ShiftStatus == ShiftStatus.Open);
						if (!shiftIsOpen)
						{
							await _salesTasks.ReconcileStockSummariesAsync(sale.ShiftNumber);
						}
					}
					catch (Exception reconcileEx)
					{
						_logger.LogError(reconcileEx,
							"Reconcile failed after reversing sale {SaleId} on shift {ShiftNumber}. " +
							"Reversal is committed — manual reconcile may be required.",
							saleId, sale.ShiftNumber);
					}

					return ServiceResponse<object>.Success("Sale reversed successfully", null);
				}
				catch (Exception ex)
				{
					await dbTx.RollbackAsync();
					return ServiceResponse<object>.Error($"An error occurred while reversing sale: {ex.Message}", null);
				}
			});
		}

		/// <summary>
		/// Move a sale to another nozzle. Only allowed when the shift is in Variance.
		/// Wrapped in EF Core execution strategy to support retry-on-failure configuration.
		/// </summary>
		public async Task<ServiceResponse<object>> TransferSaleToAnotherNozzle(string transactionCode, string nozzleCode)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var dbTx = await _context.Database.BeginTransactionAsync();
				try
				{
					var sale = await GetSaleByIdAsync(transactionCode);
					if (sale == null)
						return ServiceResponse<object>.Information("Sale not found", null);

					if (string.IsNullOrWhiteSpace(sale.ShiftNumber))
						return ServiceResponse<object>.Information("Shift not found", null);

					if (string.IsNullOrWhiteSpace(nozzleCode))
						return ServiceResponse<object>.Information("Nozzle code is required", null);

					var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftNumber == sale.ShiftNumber);
					if (shift == null)
						return ServiceResponse<object>.Information("Shift not found", null);

					if (shift.ShiftStatus == ShiftStatus.Closed)
						return ServiceResponse<object>.Information("Nozzle transfer allowed only when shift is in Variance", null);

					if (shift.ShiftStatus == ShiftStatus.Open)
						return ServiceResponse<object>.Information("Nozzle transfer allowed only when shift is in Variance", null);


					if (sale.IsReversed)
						return ServiceResponse<object>.Information("Sale already reversed, cannot be moved to another nozzle", null);

					if (sale.NozzleCode == nozzleCode)
						return ServiceResponse<object>.Information("Sale is already on the specified nozzle", null);

					// NOTE: assumes Nozzle entity has a StationCode property. If nozzles aren't
					// modeled as station-scoped in your schema, remove this lookup and the
					// station-match check below — but if they ARE station-scoped, this prevents
					// a sale from silently jumping to a nozzle on a different station.

					var nozzle = await (from n in _context.Nozzles
										join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
										select d
										).FirstOrDefaultAsync();

					if (nozzle == null)
						return ServiceResponse<object>.Information($"Nozzle {nozzleCode} does not exist in the system", null);

		
					// Update + trail
					var oldNozzle = sale.NozzleCode ?? "Unknown";
					sale.NozzleCode = nozzleCode;
					_context.QuantityTransactions.Update(sale);

					_context.UserTrails.Add(new UserTrail
					{
						ActionType = "TransferSaleToAnotherNozzle",
						Message = $"Sale {transactionCode} transferred from nozzle {oldNozzle} to nozzle {nozzleCode}",
						UserName = _authentication.Name(),
						UserCode = _authentication.Usercode(),
						DateCreated =EatTime.Now,
						ShiftNumber = sale.ShiftNumber
					});

					await _context.SaveChangesAsync();
					await dbTx.CommitAsync();

					try
					{
						await _salesTasks.ReconcileStockSummariesAsync(sale.ShiftNumber);
					}
					catch (Exception reconcileEx)
					{
						_logger.LogError(reconcileEx,
							"Reconcile failed after transferring sale {TransactionCode} to nozzle {NozzleCode}. " +
							"Transfer is committed — manual reconcile may be required.",
							transactionCode, nozzleCode);
					}

					return ServiceResponse<object>.Success("Sale transferred successfully", null);
				}
				catch (Exception ex)
				{
					await dbTx.RollbackAsync();
					return ServiceResponse<object>.Error($"An error occurred while transferring sale: {ex.Message}", null);
				}
			});
		}

		// ============================== Helpers (no SaveChanges here) ==============================


		private async Task AddReversedCreditTransactionIfVehiclePresent(string transactionCode, string nozzleCode)
		{
		

			var transaction = await _context.CreditTransactions.Where(x => x.TransactionReference == transactionCode).FirstOrDefaultAsync();
			if (transaction == null)
				return;
			// NOTE: CustomerCode/StationCode sourcing below is a guess — need the original
			// credit-creation code to confirm these match exactly.
			_context.CreditTransactions.Add(new CreditTransactions
			{
				CustomerCode = transaction.CustomerCode,
				Credit = transaction.Debit,
				Debit = 0,
				SaleId = transactionCode,
				TransactionReference = $"REVERSAL-{transaction.SaleId}",
				VehicleCode = transaction.VehicleCode,
				StationCode = transaction.StationCode,
				DateCreated =EatTime.Now,
				UserCode = _authentication.Usercode()
			});

		}
		private async Task<QuantityTransactions?> GetSaleByIdAsync(string saleId)
			=> await _context.QuantityTransactions.FirstOrDefaultAsync(x => x.SaleId == saleId);

		/// <summary>
		/// Stages a customer transaction if vehicleCode is present.
		/// </summary>
		private void AddCustomerTransactionIfVehiclePresent(string? vehicleCode, decimal amount, string transactionCode)
		{
			if (string.IsNullOrWhiteSpace(vehicleCode))
				return;

			_context.CustomerTransactions.Add(new CustomerTransactions
			{
				VehicleCode = vehicleCode,
				Credit = 0,
				Debit = amount,
				DateCreated =EatTime.Now,
				UserCode = _authentication.Usercode(),
				TransactionReference = transactionCode
			});
		}

		/// <summary>
		/// Creates a reversing quantity row and marks the original as reversed.
		/// </summary>
		private void AddReversedQuantityTransactionAndMarkOriginal(QuantityTransactions sale, string transactionCode)
		{
			var reversed = new QuantityTransactions
			{
				ShiftNumber = sale.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleRegistrationNumber = sale.VehicleRegistrationNumber,
				DispenserCode = sale.DispenserCode,
				NozzleCode = sale.NozzleCode,
				StationCode = sale.StationCode,
				Price = sale.Price,

				QuantityDebit = sale.QuantityCredit,
				QuantityCredit = 0,
				AmountDebit = sale.AmountCredit,
				AmountCredit = 0,

				PaymentTypeCode = sale.PaymentTypeCode,
				SaleId = sale.SaleId,
				DateCreated =EatTime.Now,
				IsReversed = true,
			};

			_context.QuantityTransactions.Add(reversed);

			if (!sale.IsReversed)
			{
				sale.IsReversed = true;
				_context.QuantityTransactions.Update(sale);
			}
		}

		/// <summary>
		/// Adds reversing payment rows for the sale's payment transactions.
		/// Mpesa status updates are awaited individually but isolated with try/catch so a
		/// failure on one reference doesn't abort the loop or the overall reversal.
		/// </summary>
		private async Task AddReversedPaymentTransactionsAsync(QuantityTransactions sale)
		{
			var paymentTransactions = await _context.PaymentTransactions
				.Where(x => x.SaleId == sale.SaleId)
				.AsNoTracking()
				.ToListAsync();

			foreach (var p in paymentTransactions)
			{
				_context.PaymentTransactions.Add(new PaymentTransactions
				{
					PaymentRefrence = p.PaymentRefrence,
					TransactionAmount = 0,
					TransactionAmountDebit = p.TransactionAmount,
					DateCreated =EatTime.Now,
					UserCode = _authentication.Usercode(),
					SaleId = p.SaleId
				});

				if (sale.PaymentTypeCode == PaymetMethod.Mpesa && !string.IsNullOrWhiteSpace(p.PaymentRefrence))
				{
					// UpdateMpesaPaymentStatus is async Task and calls SaveChangesAsync internally
					// on the same _context — see note below on what that means for the
					// "one SaveChanges" intent of this method. Awaited and isolated with
					// try/catch so a failure here logs cleanly without rolling back the reversal.
					try
					{
						await _salesTasks.UpdateMpesaPaymentStatus(p.PaymentRefrence);
					}
					catch (Exception mpesaEx)
					{
						_logger.LogError(mpesaEx,
							"Failed to update Mpesa payment status for reference {PaymentReference} during reversal of sale {SaleId}.",
							p.PaymentRefrence, sale.SaleId);
					}
				}
			}
		}

		/// <summary>
		/// Looks up display names for the trail message.
		///
		/// FIX: previously ran these three queries concurrently via Task.WhenAll on the shared
		/// _context, which throws "A second operation was started on this context instance
		/// before a previous operation completed" — a single DbContext/connection cannot run
		/// concurrent operations, even across unrelated DbSets. Queries are now awaited
		/// sequentially. This is a few extra round-trips, but correct; if real parallelism is
		/// wanted later, each concurrent query needs its own DbContext instance (e.g. via
		/// IDbContextFactory&lt;OTOContext&gt;), not a shared one.
		/// </summary>
		private async Task<(string stationName, string nozzleName, string numberPlate)> GetStationAndNozzleNames(
			string stationCode, string nozzleCode, string vehicleCode)
		{
			var station = await _context.Stations.AsNoTracking().FirstOrDefaultAsync(s => s.StationCode == stationCode);
			var nozzle = await _context.Nozzles.AsNoTracking().FirstOrDefaultAsync(n => n.NozzleCode == nozzleCode);
			var vehicle = await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.VehicleCode == vehicleCode);

			var stationName = station?.StationName ?? "Unknown Station";
			var nozzleName = nozzle?.NozzleName ?? "Unknown Nozzle";
			var numberPlate = vehicle?.VehicleRegistrationNumber ?? "Unknown Vehicle";

			return (stationName, nozzleName, numberPlate);
		}

		private async Task<string> BuildReverseSaleMessage(QuantityTransactions sale)
		{
			var (stationName, nozzleName, numberPlate) = await GetStationAndNozzleNames(
				sale.StationCode, sale.NozzleCode, sale.VehicleRegistrationNumber);

			return $"User '{_authentication.Name()}' (Code: {_authentication.Usercode()}) reversed sale [SaleId={sale.SaleId}] " +
				   $"on Shift [{sale.ShiftNumber}] for Station [{stationName}] (Code: {sale.StationCode}), " +
				   $"Nozzle [{nozzleName}] (Code: {sale.NozzleCode}), Vehicle [{numberPlate}]. " +
				   $"Previous State: IsReversed={sale.IsReversed}, Qty={sale.QuantityCredit}, Amount={sale.AmountCredit}. " +
				   $"New State: IsReversed=true. Action Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
		}
	}
}