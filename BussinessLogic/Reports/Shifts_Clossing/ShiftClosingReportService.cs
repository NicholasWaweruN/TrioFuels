using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Reports.Shifts_Clossing.DataAccessLayer.DTOs.Reports;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Reports.Shifts_Clossing
{
	public interface IShiftClosingReport
	{
		Task<ServiceResponse<ShiftClosingReportDto>> GenerateClosingReportAsync(
			string shiftNumber, string dispenserCode);
	}

	public class ShiftClosingReportService : IShiftClosingReport
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _auth;

		public ShiftClosingReportService(OTOContext context, IAuthCommonTasks auth)
		{
			_context = context;
			_auth = auth;
		}

		public async Task<ServiceResponse<ShiftClosingReportDto>> GenerateClosingReportAsync(
			string shiftNumber, string dispenserCode)
		{
			// -----------------------------------------------------------------
			// 1. Validate shift exists
			// -----------------------------------------------------------------
			var shift = await _context.Shifts
				.Where(s => s.ShiftNumber == shiftNumber)
				.FirstOrDefaultAsync();

			if (shift is null)
				return ServiceResponse<ShiftClosingReportDto>.Information(
					"Shift not found.", null!);

			// -----------------------------------------------------------------
			// 2. Station & dispenser info
			// -----------------------------------------------------------------
			var stationInfo = await (
				from sta in _context.Stations
				join d in _context.Dispensers on sta.StationCode equals d.StationCode
				join p in _context.PetroleumProducts on d.PetroleumCode equals p.PetroleumCode into pj
				from product in pj.DefaultIfEmpty()
				where d.DispenserCode == dispenserCode
				select new
				{
					sta.StationName,
					sta.StationCode,
					d.DispenserCode,
					DispenserName = d.DispenserName ?? d.DispenserCode,
					ProductName = product.PetroleumName ?? "—"
				}
			).FirstOrDefaultAsync();

			if (stationInfo is null)
				return ServiceResponse<ShiftClosingReportDto>.Information(
					"Dispenser not found.", null!);

			// -----------------------------------------------------------------
			// 3. Nozzle-level totalizer readings
			// -----------------------------------------------------------------
			var nozzles = await (
				from n in _context.Nozzles
				where n.DispenserCode == dispenserCode
				select new { n.NozzleCode, n.NozzleName }
			).ToListAsync();

			var nozzleCodes = nozzles.Select(n => n.NozzleCode).ToList();

			// Get opening readings (recorded at shift start)
			var openingReadings = await _context.StockTakeSummaries
				.Where(t => t.ShiftNumber == shiftNumber
						 && nozzleCodes.Contains(t.NozzleCode))
				.ToDictionaryAsync(t => t.NozzleCode, t => t.OpeningReading);

			// Get closing readings (recorded at shift end, or current if not yet closed)
			var closingReadings = await _context.StockTakeSummaries
				.Where(t => t.ShiftNumber == shiftNumber
						 && nozzleCodes.Contains(t.NozzleCode))
				.ToDictionaryAsync(t => t.NozzleCode, t => t.ClosingReading);

			// -----------------------------------------------------------------
			// 4. System sales for this shift + dispenser
			// -----------------------------------------------------------------
			var salesData = await _context.QuantityTransactions
				.Where(q => q.ShiftNumber == shiftNumber
						 && q.DispenserCode == dispenserCode
						 && !q.IsReversed)
				.ToListAsync();

			// -----------------------------------------------------------------
			// 5. Payment breakdown
			// -----------------------------------------------------------------
			var paymentTypes = await _context.PaymentTypes.ToListAsync();
			var paymentTypeLookup = paymentTypes.ToDictionary(
				p => p.PaymentTypeId, p => p.PaymentTypeName ?? p.PaymentTypeId.ToString());

			var paymentBreakdown = salesData
				.GroupBy(s => s.PaymentTypeCode)
				.Select(g => new PaymentTypeSummary
				{
					PaymentTypeCode = g.Key.ToString(),
					PaymentTypeName = paymentTypeLookup.GetValueOrDefault(g.Key, g.Key.ToString()),
					TotalAmount = g.Sum(s => s.AmountCredit - s.AmountDebit),
					TotalLitres = g.Sum(s => s.QuantityCredit - s.QuantityDebit),
					TransactionCount = g.Count()
				})
				.OrderBy(p => p.PaymentTypeName)
				.ToList();

			// -----------------------------------------------------------------
			// 6. Totals
			// -----------------------------------------------------------------
			var totalSystemLitres = salesData.Sum(s => s.QuantityCredit - s.QuantityDebit);
			var totalSystemAmount = salesData.Sum(s => s.AmountCredit - s.AmountDebit);
			var totalTransactions = salesData.Count;

			// -----------------------------------------------------------------
			// 7. Nozzle breakdown + totalizer aggregation
			// -----------------------------------------------------------------
			decimal totalOpening = 0m;
			decimal totalClosing = 0m;
			var nozzleBreakdown = new List<NozzleSummary>();

			foreach (var nozzle in nozzles)
			{
				var opening = openingReadings.GetValueOrDefault(nozzle.NozzleCode, 0m);
				var closing = closingReadings.GetValueOrDefault(nozzle.NozzleCode, 0m);
				var diff = closing - opening;

				var nozzleLitres = salesData
					.Where(s => s.NozzleCode == nozzle.NozzleCode)
					.Sum(s => s.QuantityCredit - s.QuantityDebit);

				nozzleBreakdown.Add(new NozzleSummary
				{
					NozzleCode = nozzle.NozzleCode,
					NozzleName = nozzle.NozzleName ?? nozzle.NozzleCode,
					OpeningTotalizer = opening,
					ClosingTotalizer = closing,
					Difference = diff,
					SystemLitres = nozzleLitres,
					Variance = diff - nozzleLitres
				});

				totalOpening += opening;
				totalClosing += closing;
			}

			var totalizerDifference = totalClosing - totalOpening;
			var varianceLitres = totalizerDifference - totalSystemLitres;

			// Estimate variance amount using average price
			var avgPrice = totalSystemLitres > 0
				? totalSystemAmount / totalSystemLitres
				: 0m;
			var varianceAmount = varianceLitres * avgPrice;

			var varianceStatus = varianceLitres switch
			{
				0m => "OK",
				> 0m => "EXCESS",
				_ => "SHORT"
			};

			// Allow small tolerance (±0.5 litres)
			if (Math.Abs(varianceLitres) <= 0.5m)
				varianceStatus = "OK";

			// -----------------------------------------------------------------
			// 8. Build report
			// -----------------------------------------------------------------
			var report = new ShiftClosingReportDto
			{
				StationName = stationInfo.StationName,
				ShiftNumber = shiftNumber,
				AttendantName = _auth.Name(),
				AttendantCode = _auth.Usercode(),
				DispenserCode = dispenserCode,
				DispenserName = stationInfo.DispenserName,
				ProductName = stationInfo.ProductName,
				ShiftOpenedAt = shift.DateCreated,
				ShiftClosedAt = shift.ShiftEndTime ?? DateTime.UtcNow,
				ReportGeneratedAt = DateTime.UtcNow,
				OpeningTotalizer = totalOpening,
				ClosingTotalizer = totalClosing,
				TotalizerDifference = totalizerDifference,

				PaymentBreakdown = paymentBreakdown,

				TotalSystemLitres = totalSystemLitres,
				TotalSystemAmount = totalSystemAmount,
				TotalTransactions = totalTransactions,

				VarianceLitres = varianceLitres,
				VarianceAmount = varianceAmount,
				VarianceStatus = varianceStatus,

				NozzleBreakdown = nozzleBreakdown
			};

			return ServiceResponse<ShiftClosingReportDto>.Success(
				"Closing report generated", report);
		}
	}
}