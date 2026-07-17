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
//
// ── FIX (this revision) ─────────────────────────────────────────────────
// Previously, a failed lookup (nozzle not found, no matching Price row, no
// StockTakeSummary row for this shift/nozzle) silently fell back to 0m for
// price and/or expected reading. Because VarianceAmount = LitreVariance *
// PricePerLitre, a price of 0 masked an arbitrarily large litre variance as
// KES 0 — the exact opposite of what a variance check is for. This showed
// up as nozzles with huge LitreVariance but VarianceAmount = 0, sometimes
// dragging TotalVariance down enough to flip ExceedsThreshold to false for
// the whole response even though individual nozzles were badly wrong.
//
// Lookups now return an explicit "found" flag alongside the value. Any
// nozzle where price or expected-reading data is missing is flagged via
// PriceDataMissing / ExpectedReadingMissing, is excluded from the money
// total (since KES 0 * missing price is not a real, trustworthy figure),
// and forces ExceedsThreshold = true at the response level so a data
// integrity gap can never be reported as "variance within acceptable
// range." The response also exposes DataIntegrityIssue separately so the
// client can distinguish "genuine variance breach" from "we couldn't even
// calculate this properly, go check the data."
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

		/// <summary>
		/// True when Nozzle.PetroleumCode couldn't be resolved, or no matching
		/// Price row exists for this dispenser/product. PricePerLitre will be
		/// 0m in this case, but that 0 is "unknown", not "free" — callers must
		/// NOT treat VarianceAmount as trustworthy when this is true.
		/// </summary>
		public bool PriceDataMissing { get; set; }

		/// <summary>
		/// True when no StockTakeSummary row exists for this shift/nozzle, so
		/// ExpectedReading (opening reading, and by extension the closing
		/// expected reading) is a fallback 0m rather than real data.
		/// </summary>
		public bool ExpectedReadingMissing { get; set; }

		public bool HasDataIssue => PriceDataMissing || ExpectedReadingMissing;

		public decimal LitreVariance => Math.Abs(ActualReading - ExpectedReading);

		/// <summary>
		/// KES variance for this nozzle. Deliberately 0m (not calculated) when
		/// HasDataIssue is true, since LitreVariance * 0 would misleadingly
		/// present a real litre discrepancy as zero cost. Check HasDataIssue
		/// before trusting this figure.
		/// </summary>
		public decimal VarianceAmount => HasDataIssue ? 0m : LitreVariance * PricePerLitre;
	}

	public class StockTakeVarianceCheckResponse
	{
		public decimal TotalVariance { get; set; }
		public decimal Threshold { get; set; }
		public bool ExceedsThreshold { get; set; }

		/// <summary>
		/// True if any nozzle in the breakdown had missing price or expected-
		/// reading data. This is separate from ExceedsThreshold's "genuine KES
		/// variance is too big" meaning — it means "we could not reliably
		/// calculate variance for at least one nozzle." ExceedsThreshold is
		/// still forced true alongside this so the response never claims
		/// "within acceptable range" while data is missing, but clients should
		/// check this flag to show the correct message/action to the user.
		/// </summary>
		public bool DataIntegrityIssue { get; set; }

		public string Message { get; set; } = string.Empty;
		public List<NozzleVarianceResult> NozzleBreakdown { get; set; } = [];
	}

	// ── Service ───────────────────────────────────────────────────────

	public interface IStockTakeVarianceService
	{
		Task<StockTakeVarianceCheckResponse> CheckVarianceAsync(StockTakeVarianceCheckRequest request);

		/// <summary>
		/// Backward-compatible signature (returns plain decimal, same as before this
		/// revision) for any existing callers elsewhere in the codebase. Internally
		/// delegates to TryGetCurrentRetailPriceAsync, but still collapses "not found"
		/// to 0m — so existing callers behave exactly as they did before. New code
		/// that needs to distinguish "price is 0" from "price is missing" should call
		/// TryGetCurrentRetailPriceAsync instead.
		/// </summary>
		Task<decimal> GetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode);

		/// <summary>
		/// Same lookup as GetCurrentRetailPriceAsync, but also reports whether a price
		/// was actually found, so callers (like CheckVarianceAsync) can tell "price is
		/// genuinely 0" apart from "no Nozzle/Price row exists, price is unknown."
		/// </summary>
		Task<(decimal price, bool found)> TryGetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode);

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
					DataIntegrityIssue = true,
					TotalVariance = 0m,
					Threshold = threshold
				};
			}

			var breakdown = new List<NozzleVarianceResult>();

			foreach (var reading in request.Readings)
			{
				var (expected, quantitySold, pricePerLitre, priceMissing, expectedMissing) = request.StockTakeType == 1
					? await GetExpectedOpeningReadingAsync(shift.ShiftNumber, request.DispenserCode, reading.NozzleCode)
					: await GetExpectedClosingReadingAsync(shift.ShiftNumber, request.DispenserCode, reading.NozzleCode);

				breakdown.Add(new NozzleVarianceResult
				{
					NozzleCode = reading.NozzleCode,
					ActualReading = reading.Reading,
					ExpectedReading = expected,
					QuantitySold = quantitySold,
					PricePerLitre = pricePerLitre,
					PriceDataMissing = priceMissing,
					ExpectedReadingMissing = expectedMissing
				});
			}

			var dataIntegrityIssue = breakdown.Any(b => b.HasDataIssue);

			// Only sum VarianceAmount for nozzles with clean data — nozzles with a data
			// issue already report VarianceAmount = 0m by design (see NozzleVarianceResult),
			// so they don't silently contribute a false "0 variance" into a total that
			// looks like a real, fully-calculated figure.
			var totalVariance = breakdown.Where(b => !b.HasDataIssue).Sum(b => b.VarianceAmount);
			var exceedsFromVariance = totalVariance > threshold;

			// A data integrity issue on any nozzle forces ExceedsThreshold = true.
			// We would rather over-flag a stock take for manual review than let a
			// missing price/reading row cause a real discrepancy to be waved through
			// as "within acceptable range."
			var exceeds = exceedsFromVariance || dataIntegrityIssue;

			string message;
			if (dataIntegrityIssue)
			{
				var badNozzles = string.Join(", ", breakdown.Where(b => b.HasDataIssue).Select(b => b.NozzleCode));
				message = $"Could not fully calculate variance for nozzle(s) {badNozzles} due to missing price or expected-reading data. " +
						  $"Flagged for review rather than reported as passed. Calculated variance for remaining nozzles: KES {totalVariance:N2}.";
			}
			else
			{
				message = exceeds
					? $"Combined variance of KES {totalVariance:N2} exceeds the allowed threshold of KES {threshold:N2}."
					: "Variance within acceptable range.";
			}

			return new StockTakeVarianceCheckResponse
			{
				TotalVariance = Math.Round(totalVariance, 2),
				Threshold = threshold,
				ExceedsThreshold = exceeds,
				DataIntegrityIssue = dataIntegrityIssue,
				Message = message,
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
		///
		/// Returns found = false (rather than silently defaulting to 0m as
		/// "the price") when the nozzle can't be resolved to a PetroleumCode,
		/// or when no Price row exists for that product/dispenser. Callers
		/// must treat found = false as "unknown price," not "free."
		/// </summary>
		public async Task<(decimal price, bool found)> TryGetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode)
		{
			var petroleumCode = await _db.Nozzles
				.Where(n => n.NozzleCode == nozzleCode && n.DispenserCode == dispenserCode)
				.Select(n => n.PetroleumCode)
				.FirstOrDefaultAsync();

			if (string.IsNullOrEmpty(petroleumCode))
			{
				return (0m, false);
			}

			var priceRow = await _db.Prices
				.Where(p => p.ProductCode == petroleumCode && p.DispenserCode == dispenserCode)
				.Select(p => new { p.Amount, p.Discount })
				.FirstOrDefaultAsync();

			return priceRow != null ? (priceRow.Amount - priceRow.Discount, true) : (0m, false);
		}

		/// <summary>
		/// Backward-compatible wrapper: existing callers elsewhere in the codebase
		/// expect a plain decimal, same as before this revision. This collapses
		/// "not found" to 0m to preserve that exact prior behavior for them.
		/// Internal variance logic uses TryGetCurrentRetailPriceAsync instead, so it
		/// can tell a genuine 0 price apart from a missing price row.
		/// </summary>
		public async Task<decimal> GetCurrentRetailPriceAsync(string dispenserCode, string nozzleCode)
		{
			var (price, _) = await TryGetCurrentRetailPriceAsync(dispenserCode, nozzleCode);
			return price;
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
		///
		/// Returns expectedMissing = true if no StockTakeSummary row exists for
		/// this shift/nozzle at all (rather than silently treating "no row" the
		/// same as "opening reading was genuinely 0").
		/// </summary>
		private async Task<(decimal expected, decimal quantitySold, decimal pricePerLitre, bool priceMissing, bool expectedMissing)> GetExpectedOpeningReadingAsync(
			string shiftNumber, string dispenserCode, string nozzleCode)
		{
			var openingReading = await _db.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && s.NozzleCode == nozzleCode)
				.Select(s => (decimal?)s.OpeningReading)
				.FirstOrDefaultAsync();

			var (currentPrice, priceFound) = await TryGetCurrentRetailPriceAsync(dispenserCode, nozzleCode);

			return (openingReading ?? 0m, 0m, currentPrice, !priceFound, openingReading == null);
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
		///
		/// Returns expectedMissing = true if no StockTakeSummary row exists for
		/// this shift/nozzle (opening reading unknown, not genuinely 0).
		/// QuantitySold defaults to 0m if no transactions exist, which is a
		/// legitimate value (no sales this shift), not a missing-data case.
		/// </summary>
		private async Task<(decimal expected, decimal quantitySold, decimal pricePerLitre, bool priceMissing, bool expectedMissing)> GetExpectedClosingReadingAsync(
			string shiftNumber,
			string dispenserCode,
			string nozzleCode)
		{
			var openingReadingRow = await _db.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && s.NozzleCode == nozzleCode)
				.Select(s => (decimal?)s.OpeningReading)
				.FirstOrDefaultAsync();

			var openingReading = openingReadingRow ?? 0m;

			var quantitySold = await _db.QuantityTransactions
				.Where(t => t.ShiftNumber == shiftNumber
							&& t.NozzleCode == nozzleCode
							&& t.DispenserCode == dispenserCode
							&& !t.IsReversed)
				.SumAsync(t => t.QuantityCredit);

			var (pricePerLitre, priceFound) = await TryGetCurrentRetailPriceAsync(dispenserCode, nozzleCode);

			return (openingReading + quantitySold, quantitySold, pricePerLitre, !priceFound, openingReadingRow == null);
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