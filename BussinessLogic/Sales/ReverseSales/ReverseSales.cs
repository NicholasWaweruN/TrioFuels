using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Sales.ReverseSales
{
	public class ReverseSales : IReverseSales
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly ICommonSalesTasks _salesTasks;

		public ReverseSales(
			OTOContext context,
			IAuthCommonTasks authentication,
			ICommonSetups setups,
			ICommonSalesTasks salesTasks)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_salesTasks = salesTasks;
		}

		/// <summary>
		/// Reverse a sale by creating compensating Quantity & Payment transactions.
		/// Atomic: one DB transaction, one SaveChanges, then reconcile.
		/// </summary>
		public async Task<ServiceResponse<object>> ReverseSaleAsync(string saleId)
		{
			await using var dbTx = await _context.Database.BeginTransactionAsync();
			try
			{
				// --- Load & validate state -------------------------------------------------------
				var sale = await GetSaleByIdAsync(saleId);
				if (sale == null)
					return ServiceResponse<object>.Information("Sale not found", null);

				// If any reversed quantity row already exists for this SaleId, treat as reversed
				var isSaleReversed = await _context.QuantityTransactions
					.AnyAsync(x => x.SaleId == saleId && x.IsReversed == true);
				if (isSaleReversed || sale.IsReversed)
					return ServiceResponse<object>.Information("Sale already reversed", null);

				if (string.IsNullOrWhiteSpace(sale.ShiftNumber))
					return ServiceResponse<object>.Information("Shift not found", null);

				var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftNumber == sale.ShiftNumber);
				if (shift == null)
					return ServiceResponse<object>.Information("Shift not found", null);

				if (shift.ShiftStatus == ShiftStatus.Closed)
					return ServiceResponse<object>.Information("Shift is closed, cannot reverse sale", null);

				var transactionCode = sale.SaleId;

				// --- Stage domain changes (no SaveChanges yet) -----------------------------------
				// Wallet: add a customer ledger debit to cancel wallet credit (or mirror behavior)
				if (sale.PaymentTypeCode == PaymetMethod.Wallet)
					AddCustomerTransactionIfVehiclePresent(sale.VehicleCode, sale.AmountDebit, transactionCode);

				// Add compensating quantity transaction and mark original as reversed
				AddReversedQuantityTransactionAndMarkOriginal(sale, transactionCode);

				// Add compensating payment transactions (and stage Mpesa updates)
				AddReversedPaymentTransactions(sale);

				// Trail entry
				_context.UserTrails.Add(new UserTrail
				{
					UserCode = _authentication.Usercode(),
					UserName = _authentication.Name(),
					ActionType = "ReverseSale",
					Message = await BuildReverseSaleMessage(sale),
					ShiftNumber = sale.ShiftNumber,
					DateCreated = DateTime.UtcNow
				});



				// --- Persist once ---------------------------------------------------------------
				await _context.SaveChangesAsync();

				// Commit DB transaction first, then reconcile (reconcile can re-query summaries)
				await dbTx.CommitAsync();

				// Out-of-transaction follow-up (safe to fail independently)
				await _salesTasks.ReconcileStockSummariesAsync(sale.ShiftNumber);

				return ServiceResponse<object>.Success("Sale reversed successfully", null);
			}
			catch (Exception ex)
			{
				await dbTx.RollbackAsync();
				return ServiceResponse<object>.Error($"An error occurred while reversing sale: {ex.Message}", null);
			}
		}

		/// <summary>
		/// Move a sale to another nozzle. Only allowed when the shift is in Variance.
		/// </summary>
		public async Task<ServiceResponse<object>> TransferSaleToAnotherNozzle(string transactionCode, string nozzleCode)
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

				// Only allow transfers when shift is in Variance
				if (shift.ShiftStatus != ShiftStatus.Variance)
					return ServiceResponse<object>.Information("Nozzle transfer allowed only when shift is in Variance", null);

				// Check sale state
				if (sale.IsReversed)
					return ServiceResponse<object>.Information("Sale already reversed, cannot be moved to another nozzle", null);

				if (sale.NozzleCode == nozzleCode)
					return ServiceResponse<object>.Information("Sale is already on the specified nozzle", null);

				var nozzleExists = await _context.Nozzles.AnyAsync(n => n.NozzleCode == nozzleCode);
				if (!nozzleExists)
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
					DateCreated = DateTime.UtcNow,
					ShiftNumber = sale.ShiftNumber
				});

				await _context.SaveChangesAsync();
				await dbTx.CommitAsync();

				// Reconcile after commit
				await _salesTasks.ReconcileStockSummariesAsync(sale.ShiftNumber);

				return ServiceResponse<object>.Success("Sale transferred successfully", null);
			}
			catch (Exception ex)
			{
				await dbTx.RollbackAsync();
				return ServiceResponse<object>.Error($"An error occurred while transferring sale: {ex.Message}", null);
			}
		}

		// ============================== Helpers (no SaveChanges here) ==============================

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
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
				TransactionReference = transactionCode
			});
		}

		/// <summary>
		/// Creates a reversing quantity row and marks the original as reversed.
		/// </summary>
		private void AddReversedQuantityTransactionAndMarkOriginal(QuantityTransactions sale, string transactionCode)
		{
			// Compensating transaction mirrors credits/debits
			var reversed = new QuantityTransactions
			{
				ShiftNumber = sale.ShiftNumber,
				UserCode = _authentication.Usercode(),
				VehicleCode = sale.VehicleCode,
				DispenserCode = sale.DispenserCode,
				NozzleCode = sale.NozzleCode,
				StationCode = sale.StationCode,
				Price = sale.Price,

				QuantityDebit = sale.QuantityCredit,  // mirror
				QuantityCredit = 0,                   // no credit on reversal
				AmountDebit = sale.AmountCredit,      // mirror
				AmountCredit = 0,

				PaymentTypeCode = sale.PaymentTypeCode,
				SaleId = sale.SaleId,                 // keep same SaleId linkage
				DateCreated = DateTime.UtcNow,
				IsReversed = true
			};

			_context.QuantityTransactions.Add(reversed);

			// Mark original as reversed to prevent further actions
			if (!sale.IsReversed)
			{
				sale.IsReversed = true;
				_context.QuantityTransactions.Update(sale);
			}
		}

		/// <summary>
		/// Adds reversing payment rows for the sale's payment transactions.
		/// If Mpesa, schedules status updates (side-effect via _salesTasks) per payment reference.
		/// </summary>
		private void AddReversedPaymentTransactions(QuantityTransactions sale)
		{
			var paymentTransactions = _context.PaymentTransactions
				.Where(x => x.SaleId == sale.SaleId)
				.AsNoTracking() // read-only
				.ToList();

			foreach (var p in paymentTransactions)
			{
				_context.PaymentTransactions.Add(new PaymentTransactions
				{
					PaymentRefrence = p.PaymentRefrence,
					TransactionAmount = 0,
					TransactionAmountDebit = p.TransactionAmount,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					SaleId = p.SaleId
				});

				// If Mpesa, flag/update original payment status (domain behavior retained)
				if (sale.PaymentTypeCode == PaymetMethod.Mpesa && !string.IsNullOrWhiteSpace(p.PaymentRefrence))
				{
					// Assuming UpdateMpesaPaymentStatus is fire-and-forget (as per your original code)
					_salesTasks.UpdateMpesaPaymentStatus(p.PaymentRefrence);
				}
			}
		}

		private async Task<(string stationName, string nozzleName, string numberPlate)> GetStationAndNozzleNames(string stationCode, string nozzleCode, string vehicleCode)
		{
			// Defaults in case not found
			string stationName = "Unknown Station";
			string nozzleName = "Unknown Nozzle";
			string numberPlate = "Unknown Vehicle";

			var station = await _context.Stations.FirstOrDefaultAsync(s => s.StationCode == stationCode);
			if (station != null)
				stationName = station.StationName;

			var nozzle = await _context.Nozzles.FirstOrDefaultAsync(n => n.NozzleCode == nozzleCode);
			if (nozzle != null)
				nozzleName = nozzle.NozzleName;


			var numberplate  = await _context.Vehicles.FirstOrDefaultAsync(s => s.VehicleCode == vehicleCode);
			if (numberplate != null)
				numberPlate = numberplate.VehicleRegistrationNumber ;

			return (stationName, nozzleName,numberPlate);
		}

		private async Task<string> BuildReverseSaleMessage(QuantityTransactions sale)
		{
			var (stationName, nozzleName,numberPlate) = await GetStationAndNozzleNames(sale.StationCode, sale.NozzleCode,sale.VehicleCode);

			return $"User '{_authentication.Name()}' (Code: {_authentication.Usercode()}) reversed sale [SaleId={sale.SaleId}] " +
				   $"on Shift [{sale.ShiftNumber}] for Station [{stationName}] (Code: {sale.StationCode}), " +
				   $"Nozzle [{nozzleName}] (Code: {sale.NozzleCode}), Vehicle [{numberPlate}]. " +
				   $"Previous State: IsReversed={sale.IsReversed}, Qty={sale.QuantityCredit}, Amount={sale.AmountCredit}. " +
				   $"New State: IsReversed=true. Action Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
		}

	}
}
