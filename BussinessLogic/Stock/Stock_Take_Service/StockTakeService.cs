using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BussinessLogic.Stock.Stock_Take_Service
{
	public class StockTakeService
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;

		public StockTakeService(IAuthCommonTasks authentication, OTOContext context,ICommonSetups setups)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
		}

		// Entry method for stock take
		public async Task<ServiceResponse<object>> StockTakeAsync(StockTakeDto stockTake)
		{
			foreach (var take in stockTake.Readings)
			{
				var nozzleExists = await _context.Nozzles
					.FirstOrDefaultAsync(n => n.NozzleCode == take.NozzleCode);
				if (nozzleExists is null)
					return ServiceResponse<object>.Information($"Nozzle {take.NozzleCode} does not exist.", null);

				if (take.Reading < 0)
					return ServiceResponse<object>.Information("Reading cannot be negative", null);

				if (!await IsInitialStockTakeDoneAsync(take.NozzleCode))
					return ServiceResponse<object>.Information("Initial stock take has not been done", null);
			}

			var userShift = await GetUserOpenShiftAsync();
			if (userShift != null && userShift.ShiftStatus == ShiftStatus.Variance)
				return ServiceResponse<object>.Success($"User has a variance on shift {userShift.ShiftNumber}", null);

			var dispenser = await GetDispenserAssignedToUserAsync();
			if (string.IsNullOrEmpty(dispenser))
				return ServiceResponse<object>.Information("Dispenser not found for user", null);

			if (userShift is null)
				return await CreateNewShiftAndProcessStockTakeAsync(stockTake, dispenser);
			else if (userShift.ShiftStatus == ShiftStatus.Open)
				return await ProcessExistingShiftStockTakeAsync(stockTake, userShift);

			return ServiceResponse<object>.Information("Shift already closed or in variance", null);
		}
		private async Task<ServiceResponse<object>> CreateNewShiftAndProcessStockTakeAsync(StockTakeDto stockTake, string dispenser)
		{
			var newShiftNumber = GenerateShiftNumber();
			var newShift = new Shift
			{
				IsEmailSent = false,
				ShiftNumber = newShiftNumber,
				UserCode = _authentication.Usercode(),
				ShiftStatus = ShiftStatus.Open,
				ShiftStartTime = DateTime.UtcNow,
				DateCreated = DateTime.UtcNow,
				DispenserCode = dispenser,
			};
			await _context.AddAsync(newShift);
			await _context.SaveChangesAsync();

			await ProcessStockTakeReadingsAsync(stockTake, newShiftNumber, isOpeningReading: true);
			return ServiceResponse<object>.Success("Stock take completed successfully", null);
		}
		private async Task<ServiceResponse<object>> ProcessExistingShiftStockTakeAsync(StockTakeDto stockTake, Shift shift)
		{
			await ProcessStockTakeReadingsAsync(stockTake, shift.ShiftNumber, isOpeningReading: false);
			return ServiceResponse<object>.Success("Stock take completed successfully", null);
		}

		// Handles initial stock take
		public async Task<ServiceResponse<object>> InitialStockTake(StockTakeDto initialStockTakeDto)
		{
			try
			{
				var dispenserName = await (from d in _context.Dispensers
										   join n in _context.Nozzles on d.DispenserCode equals n.DispenserCode
										   join s in _context.Stations on d.StationCode equals s.StationCode
										   where n.NozzleCode == initialStockTakeDto.Readings.First().NozzleCode
										   select new { d.DispenserName, s.StationName }).FirstOrDefaultAsync();

				if (dispenserName is null)
					return ServiceResponse<object>.Information("Dispenser does not exist", null);

				var shift = GenerateShiftNumber();

				foreach (var item in initialStockTakeDto.Readings)
				{
					var nozzleExists = await _context.Nozzles
						.FirstOrDefaultAsync(n => n.NozzleCode == item.NozzleCode);

					if (nozzleExists is null)
						return ServiceResponse<object>.Information($"Nozzle {item.NozzleCode} does not exist.", null);

					if (await IsInitialStockTakeDoneAsync(item.NozzleCode))
						return ServiceResponse<object>.Information("Initial Stock Take Already Done", null);

					await AddInitialStockTakeAsync(item, shift);
				}

				var message = $@"Initial stock take done by {_authentication.Name()} for Dispenser {dispenserName.DispenserName} at {dispenserName.StationName} Station on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Initial Stock Taken Successfully", null);
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Resets shift stock take data
		public async Task<ServiceResponse<object>> ResetShift(string shiftNumber)
		{
			try
			{
				var shift = await _context.Shifts
					.FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber);
				if (shift is null)
					return ServiceResponse<object>.Information($"{shiftNumber} does not exist", null);

				var readings = await _context.StockTakeSummaries
					.Where(r => r.ShiftNumber == shiftNumber).ToListAsync();

				foreach (var reading in readings)
				{
					reading.ClosingReading = 0;
					reading.ClosingVariance = 0;
					reading.VarianceStatus = ShiftStatus.Open;
					reading.ExpectedClosingReading = 0;
					_context.StockTakeSummaries.Update(reading);
				}

				shift.ShiftStatus = ShiftStatus.Open;
				shift.ShiftEndTime = null;
				_context.Shifts.Update(shift);

				await _context.SaveChangesAsync();
				await _authentication.AddUserTrail($"Shift {shiftNumber} was reset by {_authentication.Usercode()} on {DateTime.UtcNow}", MethodBase.GetCurrentMethod()?.Name ?? "");
				return ServiceResponse<object>.Success($"Reset for shift number {shiftNumber} was successful", null);
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Processes stock take readings
		public async Task ProcessStockTakeReadingsAsync(StockTakeDto stockTake, string shiftNumber, bool isOpeningReading)
		{
			decimal totalVariance = 0;
			var userCode = _authentication.Usercode();
			var nozzleCodes = stockTake.Readings.Select(n => n.NozzleCode).ToList();

			foreach (var nozzle in stockTake.Readings)
			{
				var expectedReading = await GetExpectedReadingAsync(nozzle.NozzleCode);
				var variance = nozzle.Reading - expectedReading;

				var stockTakeSummary = await _context.StockTakeSummaries
					.FirstOrDefaultAsync(s => s.NozzleCode == nozzle.NozzleCode && s.ShiftNumber == shiftNumber);

				if (stockTakeSummary == null)
				{
					var newStockTakeSummary = new StockTakeSummary
					{
						DateCreated = DateTime.UtcNow,
						ShiftNumber = shiftNumber,
						UserCode = userCode,
						NozzleCode = nozzle.NozzleCode,
						OpeningReading = nozzle.Reading,
						ExpectedOpeningReading = expectedReading,
						ClosingReading = 0,
						ExpectedClosingReading = 0,
						QuantitySold = 0,
						ClosingVariance = 0,
						OpeningVariance = variance,
						VarianceStatus = variance != 0 ? ShiftStatus.Variance : ShiftStatus.Open
					};
					_context.StockTakeSummaries.Add(newStockTakeSummary);
				}
				else
				{
					stockTakeSummary.ClosingReading = nozzle.Reading;
					stockTakeSummary.ClosingVariance = variance;
					stockTakeSummary.VarianceStatus = variance != 0 ? ShiftStatus.Variance : ShiftStatus.Closed;
					_context.StockTakeSummaries.Update(stockTakeSummary);
				}

				totalVariance += variance;
			}

			await _context.SaveChangesAsync();
			await NozzleQuantityTransfer(shiftNumber);
			await UpdateShiftStatusAsync(shiftNumber, totalVariance, isOpeningReading);
		}

		// Helper methods
		private async Task<bool> IsInitialStockTakeDoneAsync(string nozzleCode)
		{
			return await _context.StockTakes.AnyAsync(x => x.TakeType == 99 && x.NozzleCode == nozzleCode);
		}

		private async Task<Shift?> GetUserOpenShiftAsync()
		{
			return await _context.Shifts
				.FirstOrDefaultAsync(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == ShiftStatus.Open);
		}

		private async Task<string> GetDispenserAssignedToUserAsync()
		{
			return await _context.DispenserAssignments
				.Where(a => a.AttedantUserCode == _authentication.Usercode())
				.Select(a => a.DispenserCode)
				.FirstOrDefaultAsync() ?? string.Empty;
		}

		private async Task<decimal> GetExpectedReadingAsync(string nozzleCode)
		{
			return await _context.QuantityTransactions
				.Where(q => q.NozzleCode == nozzleCode)
				.SumAsync(x => x.QuantityCredit - x.QuantityDebit);
		}

		private async Task AddInitialStockTakeAsync(Readings item, string shift)
		{
			var stockTake = new StockTake
			{
				DateCreated = DateTime.UtcNow,
				NozzleCode = item.NozzleCode,
				ShiftNumber = shift,
				OpeningReading = item.Reading,
				ClosingReading = 0,
				UserCode = _authentication.Usercode(),
				TakeType = 99
			};
			_context.StockTakes.Add(stockTake);

			await _context.SaveChangesAsync();
		}


		// ─── Nozzle Quantity Transfer ──────────────────────────────────────────────────
		public async Task<ServiceResponse<object>> NozzleQuantityTransfer(string shiftNumber)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// ─── Step 1: Load per-nozzle variances for this shift ──────────────
				var summaries = await _context.StockTakeSummaries
					.Where(s => s.ShiftNumber == shiftNumber)
					.AsNoTracking()
					.ToListAsync();

				if (summaries.Count < 2)
					return ServiceResponse<object>.Information("Not enough nozzles to perform transfer.", null);

				// ─── Step 2: Load nozzle → dispenser map ──────────────────────────
				var nozzleCodes = summaries.Select(s => s.NozzleCode).ToList();

				var nozzleDispensers = await _context.Nozzles
					.Where(n => nozzleCodes.Contains(n.NozzleCode))
					.AsNoTracking()
					.ToDictionaryAsync(n => n.NozzleCode, n => n.DispenserCode);

				// ─── Step 3: Find swap candidate pairs ────────────────────────────
				// A valid pair: same dispenser, one large positive variance, one large
				// negative variance whose magnitudes are close to each other (within 5L).
				var swapPairs = (from over in summaries
								 join under in summaries
									 on nozzleDispensers.GetValueOrDefault(over.NozzleCode)
									 equals nozzleDispensers.GetValueOrDefault(under.NozzleCode)
								 where over.NozzleCode != under.NozzleCode
									&& over.ClosingVariance > 1          // surplus nozzle
									&& under.ClosingVariance < -1        // deficit nozzle
									&& Math.Abs(over.ClosingVariance + under.ClosingVariance)
									   < Math.Max(Math.Abs(over.ClosingVariance),
												  Math.Abs(under.ClosingVariance)) * 0.1m // within 10% net
								 orderby Math.Abs(over.ClosingVariance) descending
								 select new SwapCandidate
								 {
									 DonorNozzleCode = over.NozzleCode,   // has excess quantity
									 ReceiverNozzleCode = under.NozzleCode,  // has deficit
									 DonorVariance = over.ClosingVariance,
									 ReceiverVariance = under.ClosingVariance,
								 }).ToList();

				if (swapPairs.Count == 0)
					return ServiceResponse<object>.Information("No swap candidates found.", null);

				var auditLines = new List<string>();
				var processedPairs = new HashSet<string>(); // avoid double-processing mirror pairs

				foreach (var pair in swapPairs)
				{
					var pairKey = string.Join("|", new[] { pair.DonorNozzleCode, pair.ReceiverNozzleCode }.Order());
					if (!processedPairs.Add(pairKey)) continue;

					// ─── Step 4: Load donor transactions ──────────────────────────
					var donorTransactions = await _context.QuantityTransactions
						.Where(q => q.ShiftNumber == shiftNumber
								 && q.NozzleCode == pair.DonorNozzleCode
								 && !q.IsReversed)
						.OrderByDescending(q => q.QuantityCredit - q.QuantityDebit)
						.ToListAsync();

					// ─── Step 5: Greedy subset selection ──────────────────────────
					// Target: move enough quantity to close the receiver's deficit.
					// Receiver deficit is negative, so target = abs(receiverVariance).
					var target = Math.Abs(pair.ReceiverVariance);
					var accumulated = 0m;
					var toMove = new List<QuantityTransactions>();

					foreach (var tx in donorTransactions)
					{
						if (accumulated >= target) break;
						toMove.Add(tx);
						accumulated += tx.QuantityCredit - tx.QuantityDebit;
					}

					if (toMove.Count == 0) continue;

					// ─── Step 6: Reclassify NozzleCode ────────────────────────────
					var idsToMove = toMove.Select(t => t.Id).ToList();

					await _context.QuantityTransactions
						.Where(q => idsToMove.Contains(q.Id))
						.ExecuteUpdateAsync(q => q
							.SetProperty(x => x.NozzleCode, pair.ReceiverNozzleCode));

					auditLines.Add(
						$"Moved {toMove.Count} transaction(s) ({accumulated:N3}L) " +
						$"from nozzle {pair.DonorNozzleCode} → {pair.ReceiverNozzleCode} " +
						$"(donor variance was {pair.DonorVariance:N3}, " +
						$"receiver variance was {pair.ReceiverVariance:N3})");
				}

				if (auditLines.Count == 0)
				{
					await transaction.RollbackAsync();
					return ServiceResponse<object>.Information("No transactions were moved.", null);
				}

				// ─── Step 7: Reconcile stock summaries after reclassification ─────
				await ReconcileStockSummariesAsync(shiftNumber);

				await transaction.CommitAsync();

				var auditMessage = $"Nozzle swap correction for shift {shiftNumber}:\n" +
								   string.Join("\n", auditLines);
				await _authentication.AddUserTrail(auditMessage, nameof(NozzleQuantityTransfer));

				return ServiceResponse<object>.Success("Nozzle quantity transfer completed.", auditMessage);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("Nozzle transfer failed.", ex.Message);
			}
		}

		// ─── DTO ──────────────────────────────────────────────────────────────────────
		private sealed class SwapCandidate
		{
			public string DonorNozzleCode { get; init; } = string.Empty;
			public string ReceiverNozzleCode { get; init; } = string.Empty;
			public decimal DonorVariance { get; init; }
			public decimal ReceiverVariance { get; init; }
		}
		private async Task UpdateShiftStatusAsync(string shiftNumber, decimal totalVariance, bool isOpeningReading)
		{
			var shift = await _context.Shifts
				.FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber);
			if (shift != null)
			{
				shift.ShiftStatus = totalVariance == 0
					? isOpeningReading ? ShiftStatus.Open : ShiftStatus.Closed
					: ShiftStatus.Variance;

				if (isOpeningReading)
					shift.ShiftStartTime = DateTime.UtcNow;
				else
					shift.ShiftEndTime = DateTime.UtcNow;

				_context.Shifts.Update(shift);
				await _context.SaveChangesAsync();
			}
		}

		private async Task LogErrorTrailAsync(Exception ex)
		{
			await _authentication.ErrorTrail(new ErrorTrail
			{
				DateCreated = DateTime.UtcNow,
				ErrorCode = "004",
				ErrorMessage = ex.Message,
				Method = ex.TargetSite?.Name ?? string.Empty
			});
		}
		private static readonly Dictionary<int, string> MonthAlphabetMapping = new Dictionary<int, string>
		{
			{ 1, "LA" }, { 2, "JB" }, { 3, "VC" }, { 4, "KD" }, { 5, "WE" },
			{ 6, "XF" }, { 7, "VG" }, { 8, "QH" }, { 9, "SI" }, { 10, "BJ" }, { 11, "CK" }, { 12, "FL" }
		};

		private static readonly Dictionary<int, string> YearAlphabetMapping = new()
		{
			{ 2023,  "MN" },{ 2024, "NO" },{ 2025, "OP" },{ 2026, "PQ" },{ 2027, "QR" },{ 2028, "RS" },{ 2029, "ST" },{ 2030, "TU" }
		};
		private static readonly Dictionary<int, char> DayAlphabetMapping = new()
		{
			{ 1, 'X' }, { 2, 'Y' }, { 3, 'Z' }, { 4, 'A' }, { 5, 'B' },
			{ 6, 'C' }, { 7, 'D' }, { 8, 'E' }, { 9, 'F' }, { 10, 'G' },
			{ 11, 'H' }, { 12, 'I' }, { 13, 'J' }, { 14, 'K' }, { 15, 'L' },
			{ 16, 'M' }, { 17, 'N' }, { 18, 'O' }, { 19, 'P' }, { 20, 'Q' },
			{ 21, 'R' }, { 22, 'S' }, { 23, 'T' }, { 24, 'U' }, { 25, 'V' },
			{ 26, 'W' }, { 27, 'X' }, { 28, 'Y' }, { 29, 'Z' }, { 30, 'A' },
			{ 31, 'B' }
		};

		private static string GenerateShiftNumber()
		{
			var date = DateTime.UtcNow;
			var monthLetter = MonthAlphabetMapping[date.Month];
			var yearLetter = YearAlphabetMapping[date.Year];
			var dayLetter = DayAlphabetMapping[date.Day];
			var timePortion = date.ToString("HHmmssfff");
			var uniqueCode = $"{yearLetter}{monthLetter}{dayLetter}{timePortion}";
			return uniqueCode.ToUpper();
		}


		private async Task<ServiceResponse<object>> ReconcileStockSummariesAsync(string shiftNumber)
		{
			try
			{
				// 1. Update Stock Summaries (ALL IN SQL)
				await _context.Database.ExecuteSqlRawAsync(@"
				UPDATE ""StockTakeSummaries"" s
				SET
					""QuantitySold"" = COALESCE(q.""TotalSales"", 0),

					""ExpectedClosingReading"" =
						s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0),

					""ClosingVariance"" =
						s.""ClosingReading"" - (s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0)),

					""VarianceStatus"" =
						CASE
							WHEN ABS(
								s.""ClosingReading"" - (s.""OpeningReading"" + COALESCE(q.""TotalSales"", 0))
							) = 0
							THEN 0   -- Closed
							ELSE 2   -- Variance
						END
				FROM (
					SELECT
						""NozzleCode"",
						SUM(""QuantityCredit"" - ""QuantityDebit"") AS ""TotalSales""
					FROM ""QuantityTransactions""
					WHERE ""ShiftNumber"" = {0}
					GROUP BY ""NozzleCode""
				) q
				WHERE s.""ShiftNumber"" = {0}
				  AND s.""NozzleCode"" = q.""NozzleCode"";", shiftNumber);

				// 2. Update Shift based on updated stock
				await _context.Database.ExecuteSqlRawAsync(@"
			UPDATE ""Shifts""
			SET ""ShiftStatus"" =
				CASE
					WHEN NOT EXISTS (
						SELECT 1
						FROM ""StockTakeSummaries""
						WHERE ""ShiftNumber"" = {0}
						  AND ABS(""ClosingReading"" - (""OpeningReading"" + ""QuantitySold"")) <> 0
					)
					THEN 0   -- Closed
					ELSE 2   -- Variance
				END
			WHERE ""ShiftNumber"" = {0};", shiftNumber);

				return ServiceResponse<object>.Success("Stock reconciled successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("An error occurred while reconciling the stock summary",ex.Message);
			}
		}

	}
}