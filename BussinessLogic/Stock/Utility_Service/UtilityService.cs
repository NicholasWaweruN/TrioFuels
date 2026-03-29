using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

public class UtilityService
{
	// Generates a unique shift number
	public static string GenerateShiftNumber()
	{
		var date = DateTime.UtcNow;

		var yearMapping = new Dictionary<int, string>
		{
			{ 2023, "MN" }, { 2024, "NO" }, { 2025, "OP" }, { 2026, "PQ" },
			{ 2027, "QR" }, { 2028, "RS" }, { 2029, "ST" }, { 2030, "TU" }
		};

		var monthMapping = new Dictionary<int, string>
		{
			{ 1, "LA" }, { 2, "JB" }, { 3, "VC" }, { 4, "KD" }, { 5, "WE" },
			{ 6, "XF" }, { 7, "VG" }, { 8, "QH" }, { 9, "SI" }, { 10, "BJ" }, { 11, "CK" }, { 12, "FL" }
		};

		var dayMapping = new Dictionary<int, char>
		{
			{ 1, 'A' }, { 2, 'B' }, { 3, 'C' }, { 4, 'D' }, { 5, 'E' },
			{ 6, 'F' }, { 7, 'G' }, { 8, 'H' }, { 9, 'I' }, { 10, 'J' },
			{ 11, 'K' }, { 12, 'L' }, { 13, 'M' }, { 14, 'N' }, { 15, 'O' },
			{ 16, 'P' }, { 17, 'Q' }, { 18, 'R' }, { 19, 'S' }, { 20, 'T' },
			{ 21, 'U' }, { 22, 'V' }, { 23, 'W' }, { 24, 'X' }, { 25, 'Y' },
			{ 26, 'Z' }, { 27, 'A' }, { 28, 'B' }, { 29, 'C' }, { 30, 'D' }, { 31, 'E' }
		};

		var year = yearMapping[date.Year];
		var month = monthMapping[date.Month];
		var day = dayMapping[date.Day];
		var time = date.ToString("HHmmssfff");

		return $"{year}{month}{day}{time}".ToUpper();
	}

	// Logs a user action trail
	public static async Task LogUserTrailAsync(string message, IAuthCommonTasks authentication)
	{
		await authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
	}


	// Gets the expected reading for a specific nozzle
	public async Task<decimal> GetExpectedReadingAsync(string nozzleCode, OTOContext context)
	{
		var totalizerReading = await context.QuantityTransactions
			.Where(q => q.NozzleCode == nozzleCode)
			.SumAsync(q => q.QuantityCredit - q.QuantityDebit);

		var currentVariance = await context.StockTakeSummaries
			.Where(s => s.NozzleCode == nozzleCode)
			.SumAsync(s => s.ClosingVariance);

		return totalizerReading + currentVariance;
	}

	// Gets the expected reading for a specific nozzle and shift
	public static async Task<decimal> GetExpectedReadingAsync(string nozzleCode, string shiftNumber, OTOContext context)
	{
		var totalizerReading = await context.QuantityTransactions
			.Where(q => q.NozzleCode == nozzleCode)
			.SumAsync(q => q.QuantityCredit - q.QuantityDebit);

		var currentVariance = await context.StockTakeSummaries
			.Where(s => s.NozzleCode == nozzleCode && s.ShiftNumber != shiftNumber)
			.SumAsync(s => s.ClosingVariance);

		return totalizerReading + currentVariance;
	}

	// Validates a nozzle's existence
	public static async Task<bool> ValidateNozzleExistsAsync(string nozzleCode, OTOContext context)
	{
		return await context.Nozzles.AnyAsync(n => n.NozzleCode == nozzleCode);
	}

	// Validates if an initial stock take is done for a nozzle
	public static async Task<bool> IsInitialStockTakeDoneAsync(string nozzleCode, OTOContext context)
	{
		return await context.StockTakes.AnyAsync(s => s.TakeType == 99 && s.NozzleCode == nozzleCode);
	}
}
