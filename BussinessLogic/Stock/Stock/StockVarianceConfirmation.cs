// ─────────────────────────────────────────────────────────────────────────
// Stock Take Variance Check
//
//   ExpectedClosingReading = OpeningReading (from this shift's StockTakeSummary
//                             row, written at opening stock take) + QuantitySold
//                             (sum of QuantityCredit from QuantityTransactions
//                             for this shift/nozzle). Closing hasn't been
//                             persisted yet at this point — it's only written
//                             at close-shift — so sales + opening is the
//                             correct "expected" here.
//
//   ExpectedOpeningReading = the OpeningReading already stored on this shift's
//                             StockTakeSummary row (created at shift-open).
//                             This is a data-entry cross-check, not a
//                             comparison against a prior shift.
//
//   Litre variance is converted to money before comparing against
//   Dispenser.ThreshHold (which is a KES amount, not litres). Price-per-litre
//   for BOTH opening and closing always comes from the current retail price
//   list: Nozzle.PetroleumCode joined against Price.ProductCode, scoped to
//   the dispenser, net of Price.Discount. (No weighted-average-of-sales
//   pricing — kept simple and consistent across both stock take types.)
//
// Shift is resolved server-side from Shifts (DispenserCode + current user +
// ShiftStatus.Open) rather than trusting the client-supplied ShiftNumber —
// these tills carry real production traffic, so we don't want a stale/forged
// ShiftNumber from the app driving the expected-reading lookup.
// ─────────────────────────────────────────────────────────────────────────

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessLogic.Stock.Stock
{
	// ── DTOs ──────────────────────────────────────────────────────────

	public class NozzleReadingDto
	{
		public string NozzleCode { get; set; } = string.Empty;
		public string NozzleName { get; set; } = string.Empty;
		public decimal Reading { get; set; }
	}

	public class StockTakeVarianceCheckRequest
	{
		public string DispenserCode { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		public int StockTakeType { get; set; } // 1 = Opening, 2 = Closing
		public List<NozzleReadingDto> Readings { get; set; } = [];
	}

	public class NozzleVarianceResult
	{
		public string NozzleCode { get; set; } = string.Empty;
		public decimal ActualReading { get; set; }
		public decimal ExpectedReading { get; set; }
		public decimal QuantitySold { get; set; }
		public decimal PricePerLitre { get; set; }
		public decimal LitreVariance => Math.Abs(ActualReading - ExpectedReading);
		public decimal VarianceAmount => LitreVariance * PricePerLitre;
	}

	public class StockTakeVarianceCheckResponse
	{
		public decimal TotalVariance { get; set; }
		public decimal Threshold { get; set; }
		public bool ExceedsThreshold { get; set; }
		public string Message { get; set; } = string.Empty;
		public List<NozzleVarianceResult> NozzleBreakdown { get; set; } = [];
	}

	// ── Service ───────────────────────────────────────────────────────

	public interface IStockTakeVarianceService
	{
		Task<StockTakeVarianceCheckResponse> CheckVarianceAsync(StockTakeVarianceCheckRequest request);
		Task<decimal> GetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode);
		Task<decimal> GetThresholdForDispenserAsync(string dispenserCode);
	}

	public class StockTakeVarianceService : IStockTakeVarianceService
	{
		private readonly OTOContext _db;
		private readonly IConfiguration _config;
		private readonly IAuthCommonTasks _authentication;

		public StockTakeVarianceService(OTOContext db, IConfiguration config, IAuthCommonTasks authentication)
		{
			_db = db;
			_config = config;
			_authentication = authentication;
		}

		public async Task<StockTakeVarianceCheckResponse> CheckVarianceAsync(StockTakeVarianceCheckRequest request)
		{
			var threshold = await GetThresholdForDispenserAsync(request.DispenserCode);

			// Resolve the current open shift by dispenser only. A dispenser can only have
			// one open shift at a time (enforced at shift-open), so the closing attendant
			// may legitimately be a different user than whoever opened it (shift handover).
			// Filtering by UserCode here was a bug: it silently returned "no shift found"
			// for any handover scenario, which caused ExceedsThreshold to fall back to
			// false with no variance check actually performed.
			var shift = await _db.Shifts
				.Where(x => x.DispenserCode == request.DispenserCode
							&& x.ShiftStatus == ShiftStatus.Open)
				.FirstOrDefaultAsync();

			if (shift == null)
			{
				// No open shift is an invalid state at this point in the flow (the attendant
				// shouldn't be able to reach stock take submission without one). Treat it as
				// a hard failure rather than silently reporting "no variance" — surfacing this
				// to the client prevents a stock take from ever slipping through unchecked.
				return new StockTakeVarianceCheckResponse
				{
					Message = "No open shift found for this dispenser. Please contact support before proceeding.",
					ExceedsThreshold = true,
					TotalVariance = 0m,
					Threshold = threshold
				};
			}

			var breakdown = new List<NozzleVarianceResult>();

			foreach (var reading in request.Readings)
			{
				var (expected, quantitySold, pricePerLitre) = request.StockTakeType == 1
					? await GetExpectedOpeningReadingAsync(shift.ShiftNumber, request.DispenserCode, reading.NozzleCode)
					: await GetExpectedClosingReadingAsync(shift.ShiftNumber, request.DispenserCode, reading.NozzleCode);

				breakdown.Add(new NozzleVarianceResult
				{
					NozzleCode = reading.NozzleCode,
					ActualReading = reading.Reading,
					ExpectedReading = expected,
					QuantitySold = quantitySold,
					PricePerLitre = pricePerLitre
				});
			}

			var totalVariance = breakdown.Sum(b => b.VarianceAmount);
			var exceeds = totalVariance > threshold;

			return new StockTakeVarianceCheckResponse
			{
				TotalVariance = Math.Round(totalVariance, 2),
				Threshold = threshold,
				ExceedsThreshold = exceeds,
				Message = exceeds
					? $"Combined variance of KES {totalVariance:N2} exceeds the allowed threshold of KES {threshold:N2}."
					: "Variance within acceptable range.",
				NozzleBreakdown = breakdown
			};
		}
		/// <summary>
		/// Each dispenser carries its own variance threshold (Dispenser.ThreshHold).
		/// Falls back to a conservative appsettings default only if the dispenser
		/// can't be found, which shouldn't happen in practice since request.DispenserCode
		/// already passed the shift lookup above.
		/// </summary>
		public async Task<decimal> GetThresholdForDispenserAsync(string dispenserCode)
		{
			var threshold = await _db.Dispensers
				.Where(d => d.DispenserCode == dispenserCode)
				.Select(d => (decimal?)d.ThreshHold)
				.FirstOrDefaultAsync();

			return threshold ?? _config.GetValue<decimal?>("StockTake:VarianceThreshold") ?? 500m;
		}

		/// <summary>
		/// Current retail price for a nozzle: Nozzle.PetroleumCode joined against
		/// Price.ProductCode, scoped to this dispenser (Price also carries
		/// StationCode/DispenserCode since pricing can vary by station).
		/// Net of Discount, since that's what a customer is actually charged.
		/// This is now the single source of price-per-litre for both opening
		/// and closing stock take variance calculations.
		/// </summary>
		public async Task<decimal> GetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode)
		{
			var petroleumCode = await _db.Nozzles
				.Where(n => n.NozzleCode == nozzleCode && n.DispenserCode == dispenserCode)
				.Select(n => n.PetroleumCode)
				.FirstOrDefaultAsync();

			if (string.IsNullOrEmpty(petroleumCode))
			{
				return 0m;
			}

			var priceRow = await _db.Prices
				.Where(p => p.ProductCode == petroleumCode && p.DispenserCode == dispenserCode)
				.Select(p => new { p.Amount, p.Discount })
				.FirstOrDefaultAsync();

			return priceRow != null ? priceRow.Amount - priceRow.Discount : 0m;
		}

		/// <summary>
		/// Opening stock take: the StockTakeSummary row for this shift/nozzle
		/// already exists (created at shift-open) with OpeningReading populated.
		/// This check cross-checks the attendant's typed totalizer reading
		/// against what was already recorded for this shift, catching data-entry
		/// mistakes rather than comparing against a prior shift. Scoped
		/// explicitly to this shift via ShiftNumber, rather than inferring
		/// "current shift" from row recency.
		///
		/// Price-per-litre comes from the current retail price list.
		/// </summary>
		private async Task<(decimal expected, decimal quantitySold, decimal pricePerLitre)> GetExpectedOpeningReadingAsync(
			string shiftNumber, string dispenserCode, string nozzleCode)
		{
			var openingReading = await _db.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && s.NozzleCode == nozzleCode)
				.Select(s => (decimal?)s.OpeningReading)
				.FirstOrDefaultAsync();

			var currentPrice = await GetCurrentRetailPriceAsync(dispenserCode, nozzleCode);

			return (openingReading ?? 0m, 0m, currentPrice);
		}

		/// <summary>
		/// Closing stock take: expected reading = this shift's OpeningReading
		/// (written when the shift was opened) + sum of QuantityCredit from
		/// QuantityTransactions for this shift/nozzle, excluding reversed sales.
		/// The closing figure itself isn't persisted yet at this point — it's
		/// only written on actual close-shift submit — so sales + opening is
		/// the correct "expected" to compare the attendant's entry against.
		///
		/// Price-per-litre comes from the current retail price list — same
		/// source as opening, no weighted-average-of-sales calculation.
		/// </summary>
		private async Task<(decimal expected, decimal quantitySold, decimal pricePerLitre)> GetExpectedClosingReadingAsync(
			string shiftNumber,
			string dispenserCode,
			string nozzleCode)
		{
			var openingReading = await _db.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && s.NozzleCode == nozzleCode)
				.Select(s => (decimal?)s.OpeningReading)
				.FirstOrDefaultAsync() ?? 0m;

			var quantitySold = await _db.QuantityTransactions
				.Where(t => t.ShiftNumber == shiftNumber
							&& t.NozzleCode == nozzleCode
							&& t.DispenserCode == dispenserCode
							&& !t.IsReversed)
				.SumAsync(t => t.QuantityCredit);

			var pricePerLitre = await GetCurrentRetailPriceAsync(dispenserCode, nozzleCode);

			return (openingReading + quantitySold, quantitySold, pricePerLitre);
		}
	}

	// ── Controller action ────────────────────────────────────────────
	// Add this action to your existing StockTakeController.

	/*
    [HttpPost("check-variance")]
    public async Task<IActionResult> CheckVariance([FromBody] StockTakeVarianceCheckRequest request)
    {
        if (request?.Readings == null || !request.Readings.Any())
            return BadRequest(new { responseMessage = "No readings supplied" });

        var result = await _varianceService.CheckVarianceAsync(request);
        return Ok(result);
    }
    */
}