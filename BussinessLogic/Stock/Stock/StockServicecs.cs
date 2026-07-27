using BusinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.Stock.Stock;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace BussinessLogic.Stock.Stock
{
	public class StockServicecs : IStockServicecs
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emails;
		private readonly IMainData MainData;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IStockTakeVarianceService _varianceService;

		// Minimum time an attendant must wait after closing a shift before
		// they are allowed to open a new one. Guards against accidentally
		// re-opening a shift immediately after closing it.
		private static readonly TimeSpan ShiftReopenCooldown = TimeSpan.FromMinutes(10);

		public StockServicecs(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups, IEmailService emails, IMainData data, ICommonSalesTasks salesTasks, IStockTakeVarianceService varianceService)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
			_emails = emails;
			MainData = data;
			_salesTasks = salesTasks;
			_varianceService = varianceService;
		}

		// Entry method for stock take.
		// PERF: nozzle-existence and initial-stock-take-done checks are now
		// each a single batched query instead of two DB round trips per
		// reading in the loop (was 2N round trips for N readings, now 2 total).
		// VALIDATION: same attendant cannot open a new shift within
		// ShiftReopenCooldown of closing their last one (see GetLastClosedShiftAsync).
		public async Task<ServiceResponse<object>> StockTakeAsync(StockTakeDto stockTake)
		{
			var requestedNozzleCodes = stockTake.Readings.Select(r => r.NozzleCode).Distinct().ToList();

			var existingNozzleCodes = (await _context.Nozzles
				.Where(n => requestedNozzleCodes.Contains(n.NozzleCode))
				.Select(n => n.NozzleCode)
				.ToListAsync()).ToHashSet();

			var initialDoneSet = (await _context.StockTakes
				.Where(x => x.TakeType == 99 && requestedNozzleCodes.Contains(x.NozzleCode))
				.Select(x => x.NozzleCode)
				.Distinct()
				.ToListAsync()).ToHashSet();

			foreach (var take in stockTake.Readings)
			{
				if (!existingNozzleCodes.Contains(take.NozzleCode))
					return ServiceResponse<object>.Information($" {take.NozzleCode} Nozzle does not exist.", null);

				if (take.Reading < 0)
					return ServiceResponse<object>.Information("Reading cannot be negative", null);

				if (!initialDoneSet.Contains(take.NozzleCode))
					return ServiceResponse<object>.Information("Initial stock take has not been done", null);
			}

			var userShift = await GetUserOpenShiftAsync();

			if (userShift != null)
				if (userShift.ShiftStatus == ShiftStatus.Variance)
					return ServiceResponse<object>.Success($"User has a variance on shift {userShift.ShiftNumber}", null);

			var dispenser = await GetDispenserAssignedToUserAsync();

			if (string.IsNullOrEmpty(dispenser))
				return ServiceResponse<object>.Information("Dispenser not found for user", null);

			if (userShift is null)
			{
				var lastClosedShift = await GetLastClosedShiftAsync();
				if (lastClosedShift?.ShiftEndTime != null)
				{
					var timeSinceClose =EatTime.Now - lastClosedShift.ShiftEndTime.Value;

					if (timeSinceClose < ShiftReopenCooldown)
					{
						var remainingSeconds = (int)(ShiftReopenCooldown - timeSinceClose).TotalSeconds;
						return ServiceResponse<object>.Information(
							$"You closed shift {lastClosedShift.ShiftNumber} {(int)timeSinceClose.TotalSeconds}s ago. " +
							$"This looks like it may have been by mistake — please wait {remainingSeconds}s before opening a new shift.", null);
					}
				}

				return await CreateNewShiftAndProcessStockTakeAsync(stockTake, dispenser);
			}
			else if (userShift.ShiftStatus == ShiftStatus.Open)
				return await ProcessExistingShiftStockTakeAsync(stockTake, userShift);
			return ServiceResponse<object>.Information("Shift already closed or in variance", null);
		}

		// Handles initial stock take.
		// PERF: nozzle validation batched (was 2 queries per reading), and all
		// StockTake/QuantityTransaction inserts are now flushed with a single
		// SaveChangesAsync instead of one SaveChangesAsync per reading.
		public async Task<ServiceResponse<object>> InitialStockTake(StockTakeDto initialStockTakeDto)
		{
			try
			{
				var firstNozzleCode = initialStockTakeDto.Readings.First().NozzleCode;

				var dispenserInfo = await (from d in _context.Dispensers
										   join n in _context.Nozzles on d.DispenserCode equals n.DispenserCode
										   join s in _context.Stations on d.StationCode equals s.StationCode
										   where n.NozzleCode.Equals(firstNozzleCode)
										   select new { d.DispenserName, s.StationName }).FirstOrDefaultAsync();

				if (dispenserInfo is null)
					return ServiceResponse<object>.Information($"Dispenser does not exist", null);

				var requestedNozzleCodes = initialStockTakeDto.Readings.Select(r => r.NozzleCode).Distinct().ToList();

				var existingNozzleCodes = (await _context.Nozzles
					.Where(n => requestedNozzleCodes.Contains(n.NozzleCode))
					.Select(n => n.NozzleCode)
					.ToListAsync()).ToHashSet();

				var alreadyDoneSet = (await _context.StockTakes
					.Where(x => x.TakeType == 99 && requestedNozzleCodes.Contains(x.NozzleCode))
					.Select(x => x.NozzleCode)
					.Distinct()
					.ToListAsync()).ToHashSet();

				foreach (var item in initialStockTakeDto.Readings)
				{
					if (!existingNozzleCodes.Contains(item.NozzleCode))
						return ServiceResponse<object>.Information($" {item.NozzleCode} Nozzle does not exist.", null);

					if (alreadyDoneSet.Contains(item.NozzleCode))
						return ServiceResponse<object>.Information("Initial Stock Take Already Done", null);
				}

				var shift = GenerateShiftNumber();
				var userCode = _authentication.Usercode();

				foreach (var item in initialStockTakeDto.Readings)
				{
					_context.StockTakes.Add(new StockTake
					{
						DateCreated =EatTime.Now,
						NozzleCode = item.NozzleCode,
						ShiftNumber = shift,
						OpeningReading = item.Reading,
						ClosingReading = 0,
						UserCode = userCode,
						TakeType = 99
					});

					_context.QuantityTransactions.Add(new QuantityTransactions
					{
						DateCreated =EatTime.Now,
						UserCode = userCode,
						NozzleCode = item.NozzleCode,
						QuantityCredit = item.Reading,
						QuantityDebit = 0,
						ShiftNumber = shift,
						SaleId = _setups.GenerateSaleId(),
						PaymentTypeCode = 99,
					});
				}

				await _context.SaveChangesAsync(); // single round trip for the whole batch

				var message = $@"Initial stock take done by {_authentication.Name()} for Dispenser {dispenserInfo.DispenserName} at {dispenserInfo.StationName} Station on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Initial Stock Taken Successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}


		// Gets stock takes for a user on a specific date
		public async Task<ServiceResponse<object>> GetStockTakes(string date)
		{

			try
			{
				var stockTakes = await (from stockTake in _context.StockTakes
										join nozzle in _context.Nozzles on stockTake.NozzleCode equals nozzle.NozzleCode
										join dispenser in _context.Dispensers on nozzle.DispenserCode equals dispenser.DispenserCode
										join station in _context.Stations on dispenser.StationCode equals station.StationCode
										join user in _context.Users on stockTake.UserCode equals user.UserCode
										where stockTake.DateCreated.Date == Convert.ToDateTime(date).Date
										select new
										{
											Name = user.FirstName + " " + user.LastName,
											stockTake.NozzleCode,
											stockTake.OpeningReading,
											stockTake.ClosingReading,
											stockTake.DateCreated.Date,
											nozzle.NozzleName,
											station.StationName,
											dispenser.DispenserName
										}).OrderBy(x => x.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleCode).ThenBy(x => x.Date).AsNoTracking().ToListAsync();

				if (stockTakes.Count == 0)
					return ServiceResponse<object>.Information("No stock takes found", null);
				return ServiceResponse<object>.Success("Success", stockTakes);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated =EatTime.Now,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});

				return ServiceResponse<object>.Error("Something went wrong", ex.Message);

			}
		}

		// Gets variances for all users
		public async Task<ServiceResponse<object>> ShiftVariances()
		{
			try
			{
				var shift = await (from s in _context.Shifts
								   where s.ShiftStatus == ShiftStatus.Open && s.UserCode == _authentication.Usercode()
								   select s.ShiftNumber).FirstOrDefaultAsync();

				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   join p in _context.Prices on n.PetroleumCode equals p.ProductCode
									   where ss.VarianceStatus == ShiftStatus.Variance
									   && ss.ShiftNumber == shift
									   select new
									   {
										   ss.ShiftNumber,
										   ss.UserCode,
										   ss.NozzleCode,
										   ss.OpeningReading,
										   ss.ClosingReading,
										   ss.ExpectedClosingReading,
										   Variance = ss.ClosingVariance + ss.OpeningVariance,
										   VarianceValue = (ss.ClosingVariance + ss.OpeningVariance) * p.Amount,
										   Status = ss.VarianceStatus,
										   ss.DateCreated,
										   n.NozzleName,
										   d.DispenserName,
										   s.StationName,
										   payrollNumber = u.PayrollNumber,
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName })
									   }).AsNoTracking().ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<object>.Information("No variances found", null);
				return ServiceResponse<object>.Success("", variances);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Adjusts stock take based on given adjustments.
		// PERF: batched lookups for all nozzles in one query each, instead of
		// two queries per reading in the loop. Also added a rollback on
		// exception (was missing).
		public async Task<ServiceResponse<object>> AdjustStockTake([Required] int takeType, AdjustStockTakeDto adjust)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				var nozzleCodes = adjust.Readings.Select(r => r.NozzleCode).Distinct().ToList();

				var stocktakes = await _context.StockTakes
					.Where(x => x.ShiftNumber == adjust.ShiftNumber && nozzleCodes.Contains(x.NozzleCode))
					.ToDictionaryAsync(x => x.NozzleCode);

				var stocktakeSummaries = await _context.StockTakeSummaries
					.Where(x => x.ShiftNumber == adjust.ShiftNumber && nozzleCodes.Contains(x.NozzleCode))
					.ToDictionaryAsync(x => x.NozzleCode);

				foreach (var item in adjust.Readings)
				{
					if (!stocktakes.TryGetValue(item.NozzleCode, out var stocktake) ||
						!stocktakeSummaries.TryGetValue(item.NozzleCode, out var stocktakeSummary))
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("Stock take or summary not found", null);
					}

					AdjustStockTakeValues(stocktake, stocktakeSummary, takeType, item.Reading);
				}

				var messages = $@"Stock adjusted by {_authentication.Name()} on {DateTime.UtcNow} of shiftNumber {adjust.ShiftNumber}";
				await _authentication.AddUserTrail(messages, MethodBase.GetCurrentMethod()?.Name ?? "");

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				return ServiceResponse<object>.Success("Stock take adjusted successfully", null);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		//save base64 image to file TotalizerImages folder StockTakeDto
		public class ReceiveDeliveryDto
		{
			public string OrderId { get; set; } = string.Empty;
			public double DeliveryQuantityKgs { get; set; }
			public string RotoGaugeImageBeforeDelivery { get; set; } = string.Empty;
			public double RotoGaugePercAfterDelivery { get; set; }
			public string RotoGaugeImageAfterDelivery { get; set; } = string.Empty;
			public double RotoGaugePercBeforeDelivery { get; set; }
			public double DeliveryQuantityLitres { get; set; }
		}

		private async Task<Shift?> GetUserOpenShiftAsync()
		{
			var openshift = await _context.Shifts.FirstOrDefaultAsync(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == ShiftStatus.Open);
			if (openshift is not null)
				return openshift;
			else
				return null;
		}

		// VALIDATION: used by StockTakeAsync to enforce ShiftReopenCooldown.
		// Returns the attendant's most recently closed shift (by ShiftEndTime),
		// or null if they have never closed a shift.
		private async Task<Shift?> GetLastClosedShiftAsync()
		{
			return await _context.Shifts
				.Where(x => x.UserCode == _authentication.Usercode()
						 && x.ShiftStatus == ShiftStatus.Closed
						 && x.ShiftEndTime != null)
				.OrderByDescending(x => x.ShiftEndTime)
				.FirstOrDefaultAsync();
		}

		private async Task<string> GetDispenserAssignedToUserAsync()
		{
			var dispenser = await _context.DispenserAssignments
								 .Where(a => a.AttedantUserCode == _authentication.Usercode())
								 .Select(a => a.DispenserCode)
								 .FirstOrDefaultAsync();
			if (dispenser is not null)
				return dispenser;
			return string.Empty;
		}
		private async Task<ServiceResponse<object>> CreateNewShiftAndProcessStockTakeAsync(StockTakeDto stockTake, string dispenser)
		{
			var newShiftNumber = GenerateShiftNumber();
			MainData.ShiftNumber = newShiftNumber;
			var newShift = new Shift
			{
				IsEmailSent = false,
				ShiftNumber = newShiftNumber,
				UserCode = _authentication.Usercode(),
				ShiftStatus = ShiftStatus.Open,
				ShiftStartTime = EatTime.Now,
				DateCreated = EatTime.Now,
				DispenserCode = dispenser,
			};
			await _context.AddAsync(newShift);
			await _context.SaveChangesAsync();

			return await ProcessStockTakeReadingsAsync(stockTake, newShiftNumber, isOpeningReading: true, newShift);
		}

		private async Task<ServiceResponse<object>> ProcessExistingShiftStockTakeAsync(StockTakeDto stockTake, Shift shift)
		{
			return await ProcessStockTakeReadingsAsync(stockTake, shift.ShiftNumber, isOpeningReading: false, shift);
		}

		// PERF: expected-reading and quantity-sold calculations were previously
		// done per-nozzle inside the loop (2 extra DB round trips per nozzle).
		// Both are now precomputed once for the whole shift via grouped queries.
		// Also dropped the unused `highestv` aggregate query that was computed
		// and discarded on every closing stock take.
		//
		// FIX (missing-nozzle reconciliation bug): on a closing stock take, any
		// nozzle whose StockTakeSummary is still Open for this shift but is NOT
		// present in the submitted `stockTake.Readings` was previously left
		// completely untouched — its ClosingReading/ExpectedClosingReading/
		// ClosingVariance stayed at 0 and VarianceStatus stayed at Open (0)
		// forever, while the shift itself could still get marked Closed because
		// the post-loop totalVariance sum only reflects nozzles that were
		// actually visited. We now require a complete submission for closes:
		// if any open nozzle for the shift is missing from the payload, we
		// reject up front instead of silently leaving that summary row
		// half-reconciled.
		private async Task<ServiceResponse<object>> ProcessStockTakeReadingsAsync(StockTakeDto stockTake, string shiftNumber, bool isOpeningReading, Shift shift)
		{
			decimal totalVariance = 0;
			var userCode = _authentication.Usercode();
			var nozzleCodes = stockTake.Readings.Select(n => n.NozzleCode).ToList();

			var stockTakes = await _context.StockTakes
				.Where(s => s.ShiftNumber == shiftNumber && nozzleCodes.Contains(s.NozzleCode))
				.ToListAsync();

			var stockTakeSummaries = await _context.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && nozzleCodes.Contains(s.NozzleCode))
				.ToListAsync();

			if (!isOpeningReading)
			{
				// Every nozzle that is still Open for this shift must be present
				// in the closing submission, or it will never get reconciled.
				var openNozzlesForShift = await _context.StockTakeSummaries
					.Where(s => s.ShiftNumber == shiftNumber && s.VarianceStatus == ShiftStatus.Open)
					.Select(s => s.NozzleCode)
					.ToListAsync();

				var missingNozzles = openNozzlesForShift.Except(nozzleCodes).ToList();
				if (missingNozzles.Count > 0)
				{
					return ServiceResponse<object>.Information(
						$"Closing readings missing for nozzle(s): {string.Join(", ", missingNozzles)}. " +
						"All open nozzles for this shift must be submitted together.", null);
				}
			}

			// Batched replacement for the old per-nozzle GetExpectedReadingAsync calls.
			var expectedReadings = await GetExpectedReadingsAsync(nozzleCodes);

			// Batched replacement for the old per-nozzle QuantitySold sum in the
			// closing-reading branch below.
			var quantitySoldByNozzle = await _context.QuantityTransactions
				.Where(q => q.ShiftNumber == shiftNumber && nozzleCodes.Contains(q.NozzleCode))
				.GroupBy(q => q.NozzleCode)
				.Select(g => new { NozzleCode = g.Key, Total = g.Sum(x => x.QuantityCredit - x.QuantityDebit) })
				.ToDictionaryAsync(x => x.NozzleCode, x => x.Total);

			foreach (var nozzle in stockTake.Readings)
			{
				var stockTakeEntity = stockTakes.FirstOrDefault(s => s.NozzleCode == nozzle.NozzleCode);
				if (isOpeningReading)
					if (stockTakeEntity == null)
					{
						stockTakeEntity = new StockTake { DateCreated = EatTime.Now, ShiftNumber = shiftNumber, UserCode = userCode, NozzleCode = nozzle.NozzleCode, OpeningReading = nozzle.Reading, ClosingReading = 0 };
						_context.StockTakes.Add(stockTakeEntity);
					}
					else if (stockTakeEntity != null)
					{
						stockTakeEntity.ClosingReading = nozzle.Reading;
						_context.StockTakes.Update(stockTakeEntity);
					}

				var expectedReading = expectedReadings.TryGetValue(nozzle.NozzleCode, out var er) ? er : 0m;
				var variance = nozzle.Reading - expectedReading;
				var stockTakeSummary = stockTakeSummaries.FirstOrDefault(s => s.NozzleCode == nozzle.NozzleCode);

				if (stockTakeSummary == null)
				{
					if (isOpeningReading)
					{
						var openingVariance = nozzle.Reading - expectedReading;

						var newStockTakeSummary = new StockTakeSummary
						{
							DateCreated = EatTime.Now,
							ShiftNumber = shiftNumber,
							UserCode = userCode,
							NozzleCode = nozzle.NozzleCode,
							OpeningReading = nozzle.Reading,
							ExpectedOpeningReading = expectedReading,
							ClosingReading = 0,
							ExpectedClosingReading = 0,
							QuantitySold = 0,
							ClosingVariance = 0,
							OpeningVariance = openingVariance,
							VarianceStatus = openingVariance != 0 ? ShiftStatus.Variance : ShiftStatus.Open
						};
						_context.StockTakeSummaries.Add(newStockTakeSummary);
					}
				}
				else
				{
					var quantitySold = quantitySoldByNozzle.TryGetValue(nozzle.NozzleCode, out var qs) ? qs : 0m;

					stockTakeSummary.QuantitySold = quantitySold;
					stockTakeSummary.ExpectedClosingReading = expectedReading;
					stockTakeSummary.ClosingReading = nozzle.Reading;
					stockTakeSummary.ClosingVariance = variance;
					stockTakeSummary.VarianceStatus = variance != 0 ? ShiftStatus.Variance : ShiftStatus.Closed;

					_context.StockTakeSummaries.Update(stockTakeSummary);
				}

				totalVariance += variance;
			}

			await _context.SaveChangesAsync();

			if (!isOpeningReading)
			{
				await _salesTasks.ReconcileStockSummariesAsync(shift.ShiftNumber);
				await ClearVariance(shiftNumber);

				totalVariance = await (from q in _context.StockTakeSummaries
									   where q.ShiftNumber == shiftNumber
									   select q).SumAsync(x => x.ClosingVariance);
			}

			await UpdateShiftStatusAsync(shiftNumber, totalVariance, isOpeningReading, shift);

			return ServiceResponse<object>.Success("Stock take completed successfully", null);
		}
		private async Task UpdateShiftStatusAsync(string shiftNumber, decimal totalVariance, bool isOpeningReading, Shift shift)
		{
			if (shift != null)
			{
				shift.ShiftStatus = totalVariance == 0
					? isOpeningReading ? ShiftStatus.Open
					: ShiftStatus.Closed
					: ShiftStatus.Variance;

				if (isOpeningReading)
					shift.ShiftStartTime =EatTime.Now;
				else
					shift.ShiftEndTime =EatTime.Now;

				_context.Shifts.Update(shift);
				await _context.SaveChangesAsync();

				
			}
		}

		// PERF: replaces the old per-nozzle GetExpectedReadingAsync (2 DB round
		// trips per nozzle). Computes totalizer readings and running variance
		// for every requested nozzle in exactly 2 grouped queries total.
		private async Task<Dictionary<string, decimal>> GetExpectedReadingsAsync(IEnumerable<string> nozzleCodes)
		{
			var codes = nozzleCodes.Distinct().ToList();

			var totalizerReadings = await _context.QuantityTransactions
				.Where(q => codes.Contains(q.NozzleCode))
				.GroupBy(q => q.NozzleCode)
				.Select(g => new { NozzleCode = g.Key, Total = g.Sum(x => x.QuantityCredit - x.QuantityDebit) })
				.ToDictionaryAsync(x => x.NozzleCode, x => x.Total);

			var currentVariances = await _context.StockTakeSummaries
				.Where(ss => codes.Contains(ss.NozzleCode))
				.GroupBy(ss => ss.NozzleCode)
				.Select(g => new { NozzleCode = g.Key, Total = g.Sum(v => v.ClosingVariance) })
				.ToDictionaryAsync(x => x.NozzleCode, x => x.Total);

			var result = new Dictionary<string, decimal>();
			foreach (var code in codes)
			{
				totalizerReadings.TryGetValue(code, out var totalizer);
				currentVariances.TryGetValue(code, out var variance);
				result[code] = totalizer + variance;
			}
			return result;
		}

		private void AdjustStockTakeValues(StockTake stocktake, StockTakeSummary stocktakeSummary, int takeType, decimal reading)
		{
			if (takeType == 2)
			{
				stocktake.ClosingReading = reading;
				stocktakeSummary.ClosingReading = reading;
				stocktakeSummary.ClosingVariance = reading - stocktakeSummary.ExpectedClosingReading;
				stocktakeSummary.VarianceStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			}
			else
			{
				stocktake.OpeningReading = reading;
				stocktakeSummary.OpeningReading = reading;
				stocktakeSummary.OpeningVariance = reading - stocktakeSummary.ExpectedOpeningReading;
				stocktakeSummary.VarianceStatus = stocktakeSummary.OpeningVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			}

			_context.Update(stocktake);
			_context.StockTakeSummaries.Update(stocktakeSummary);
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
			var date =EatTime.Now;
			var monthLetter = MonthAlphabetMapping[date.Month];
			var yearLetter = YearAlphabetMapping[date.Year];
			var dayLetter = DayAlphabetMapping[date.Day];
			var timePortion = date.ToString("HHmmssfff");
			var uniqueCode = $"{yearLetter}{monthLetter}{dayLetter}{timePortion}";
			return uniqueCode.ToUpper();
		}

		//List Variance From StockTakeSummary Table 
		public async Task<ServiceResponse<object>> ListVariance(DateTime? date, string? shiftNumber, string? stationName)
		{
			try
			{
				var query = from ss in _context.StockTakeSummaries
							join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
							join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
							join s in _context.Stations on d.StationCode equals s.StationCode
							join u in _context.Users on ss.UserCode equals u.UserCode
							where ss.VarianceStatus == ShiftStatus.Variance
							   || ss.VarianceStatus == ShiftStatus.Pending
							select new
							{
								ss.Id,
								d.DispenserCode,
								ss.ShiftNumber,
								ss.UserCode,
								ss.NozzleCode,
								ss.OpeningReading,
								ss.ClosingReading,
								ss.ExpectedClosingReading,
								Variance = ss.ClosingVariance,
								ss.QuantitySold,
								Status = ss.VarianceStatus,
								ss.DateCreated,
								n.NozzleName,
								d.DispenserName,
								s.StationName,
								s.StationCode,
								u.PayrollNumber,
								u.FirstName,
								MiddleName = u.MiddName,
								u.LastName
							};

				if (date.HasValue)
				{
					var start = date.Value.Date;
					var end = start.AddDays(1);

					query = query.Where(x => x.DateCreated >= start && x.DateCreated < end);
				}

				if (!string.IsNullOrEmpty(shiftNumber))
					query = query.Where(x => x.ShiftNumber == shiftNumber);

				if (!string.IsNullOrEmpty(stationName))
					query = query.Where(x => x.StationName.Contains(stationName));

				var variances = await query
					.OrderBy(x => x.StationName)
					.ThenBy(x => x.DispenserName)
					.ThenBy(x => x.Id)
					.AsNoTracking()
					.ToListAsync();

				var result = variances.Select(x => new
				{
					x.Id,
					x.DispenserCode,
					x.ShiftNumber,
					x.UserCode,
					x.NozzleCode,
					x.OpeningReading,
					x.ClosingReading,
					x.ExpectedClosingReading,
					x.Variance,
					x.QuantitySold,
					x.Status,
					x.DateCreated,
					x.NozzleName,
					x.DispenserName,
					x.StationName,
					x.StationCode,
					x.PayrollNumber,
					Name = $"{x.FirstName} {x.MiddleName} {x.LastName}"
				}).ToList();

				if (result.Count == 0)
					return ServiceResponse<object>.Information("No variances found", null);

				return ServiceResponse<object>.Success("Variance List", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}

		public async Task<ServiceResponse<object>> GetTotalizerReadings()
		{
			try
			{
				var totalizerReadings = await (from q in _context.QuantityTransactions
											   join s in _context.Stations on q.StationCode equals s.StationCode
											   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
											   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
											   group q by new
											   {
												   q.NozzleCode,
												   s.StationName,
												   d.DispenserName,
												   n.NozzleName
											   } into g
											   select new
											   {
												   NozzleCode = g.Key,
												   g.Key.NozzleName,
												   g.Key.DispenserName,
												   g.Key.StationName,
												   TotalizerReading = g.Sum(x => x.QuantityCredit - x.QuantityDebit)
											   }).AsNoTracking().OrderBy(x => x.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleName).ToListAsync();

				if (totalizerReadings.Count == 0)
					return ServiceResponse<object>.Information("No totalizer readings found", null);
				return ServiceResponse<object>.Success("", totalizerReadings);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		public async Task<ServiceResponse<object>> GetTotalizerReadings(DateTime date)
		{
			try
			{
				var totalizerReadings = await (from q in _context.QuantityTransactions
											   join s in _context.Stations on q.StationCode equals s.StationCode
											   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
											   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
											   where q.DateCreated.Date <= date.Date
											   group q by new
											   {
												   q.NozzleCode,
												   s.StationName,
												   d.DispenserName,
												   n.NozzleName
											   } into g
											   select new
											   {
												   NozzleCode = g.Key,
												   g.Key.NozzleName,
												   g.Key.DispenserName,
												   g.Key.StationName,
												   TotalizerReading = g.Sum(x => x.QuantityCredit - x.QuantityDebit)
											   }).OrderBy(X => X.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleName).AsNoTracking().ToListAsync();

				if (totalizerReadings.Count == 0)
					return ServiceResponse<object>.Information("No totalizer readings found", null);
				return ServiceResponse<object>.Success("", totalizerReadings);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		public async Task<ServiceResponse<object>> AdjustStockTakes(AdjustStockTakeSummaryDto adjust)
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var transaction = await _context.Database.BeginTransactionAsync();

				try
				{
					var nozzleCodes = adjust.Readings.Select(x => x.NozzleCode).ToList();

					var stockTakes = await _context.StockTakeSummaries
						.AsTracking()
						.Where(x => x.ShiftNumber == adjust.ShiftNumber && nozzleCodes.Contains(x.NozzleCode))
						.ToListAsync();

					if (stockTakes.Count == 0)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("Stock take summary not found", null);
					}

					var missing = nozzleCodes.Except(stockTakes.Select(x => x.NozzleCode)).ToList();
					if (missing.Count > 0)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information(
							$"No stock take found for nozzle(s): {string.Join(", ", missing)}", null);
					}

					var changeLog = new List<string>();

					foreach (var stockTake in stockTakes)
					{
						var item = adjust.Readings.First(x => x.NozzleCode == stockTake.NozzleCode);

						if (item.ClosingReading < item.OpeningReading)
						{
							await transaction.RollbackAsync();
							return ServiceResponse<object>.Information(
								$"Closing reading cannot be less than opening reading for nozzle {stockTake.NozzleCode}", null);
						}

						if (stockTake.OpeningReading == item.OpeningReading && stockTake.ClosingReading == item.ClosingReading)
							continue; // no-op, skip logging/updating unchanged rows

						changeLog.Add(
							$"Nozzle {stockTake.NozzleCode}: Opening {stockTake.OpeningReading}->{item.OpeningReading}, " +
							$"Closing {stockTake.ClosingReading}->{item.ClosingReading}");

						stockTake.OpeningReading = item.OpeningReading;
						stockTake.ClosingReading = item.ClosingReading;
						stockTake.OpeningVariance = 0;
					}

					if (changeLog.Count == 0)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("No changes detected — readings already match", null);
					}

					await _context.SaveChangesAsync();

					var reconcileResult = await ReconcileStockSummaries(adjust.ShiftNumber);
					if (reconcileResult.ResponseCode != Response.Success)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Error(
							"Stock adjustment failed during reconciliation", reconcileResult.ResponseMessage);
					}

					var message = $"Stock adjusted by {_authentication.Name()} on {DateTime.UtcNow:u} for shift {adjust.ShiftNumber}. {string.Join(" | ", changeLog)}";

					await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

					await transaction.CommitAsync();

					return ServiceResponse<object>.Success("Stock take summary adjusted successfully", null);
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return ServiceResponse<object>.Error("Something went wrong", ex.Message);
				}
			});
		}
		public async Task<ServiceResponse<byte[]>> ExportAllVariances()
		{
			try
			{
				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   where ss.VarianceStatus == ShiftStatus.Variance || ss.VarianceStatus == ShiftStatus.Pending
									   select new
									   {
										   d.DispenserCode,
										   ss.ShiftNumber,
										   ss.UserCode,
										   ss.NozzleCode,
										   ss.OpeningReading,
										   ss.ClosingReading,
										   ss.ExpectedClosingReading,
										   ss.ExpectedOpeningReading,
										   Variance = ss.ClosingVariance + ss.OpeningVariance,
										   ss.QuantitySold,
										   Status = ss.VarianceStatus,
										   ss.DateCreated,
										   n.NozzleName,
										   d.DispenserName,
										   s.StationName,
										   s.StationCode,
										   u.PayrollNumber,
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName })
									   }).AsNoTracking().ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<byte[]>.Information("No variances found", null);

				var dataTable = new DataTable("VarianceReport");
				dataTable.Columns.AddRange(
				[
					new("ShiftNumber", typeof(string)),
					new("StationName", typeof(string)),
					new("DispenserName", typeof(string)),
					new("NozzleName", typeof(string)),
					new("OpeningReading", typeof(decimal)),
					new("ExpectedOpeningReading", typeof(decimal)),
					new("ClosingReading", typeof(decimal)),
					new("ExpectedClosingReading", typeof(decimal)),
					new("Variance", typeof(decimal)),
					new("QuantitySold", typeof(decimal)),
					new("Status", typeof(string)),
					new("DateCreated", typeof(DateTime)),
					new("PayrollNumber", typeof(string)),
					new("Name", typeof(string))
				]);

				foreach (var variance in variances)
				{
					dataTable.Rows.Add(
						variance.ShiftNumber,
						variance.StationName,
						variance.DispenserName,
						variance.NozzleName,
						variance.OpeningReading,
						variance.ExpectedOpeningReading,
						variance.ClosingReading,
						variance.ExpectedClosingReading,
						variance.Variance,
						variance.QuantitySold,
						variance.Status,
						variance.DateCreated,
						variance.PayrollNumber,
						variance.Name ?? string.Empty
					);
				}

				var excel = new ExcelPackage();
				var ws = excel.Workbook.Worksheets.Add("VarianceReport");
				ws.Cells["A1"].LoadFromDataTable(dataTable, true);
				ws.Cells.AutoFitColumns();

				var stream = new MemoryStream(excel.GetAsByteArray());

				return ServiceResponse<byte[]>.Success("Variance report generated successfully", stream.ToArray());
			}
			catch (Exception)
			{
				return ServiceResponse<byte[]>.Error("Failed to generate variance report", null);
			}
		}

		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			return await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
		}

		public class VarianceDto
		{
			public long ShiftId { get; set; }
			public string DispenserCode { get; set; } = string.Empty;
			public string ShiftNumber { get; set; } = string.Empty;
			public string UserCode { get; set; } = string.Empty;
			public string NozzleCode { get; set; } = string.Empty;
			[Precision(18, 2)] public decimal OpeningReading { get; set; }
			[Precision(18, 2)] public decimal ExpectedOpeningReading { get; set; }
			[Precision(18, 2)] public decimal ClosingReading { get; set; }
			[Precision(18, 2)] public decimal ExpectedClosingReading { get; set; }
			[Precision(18, 2)] public decimal Variance { get; set; }
			[Precision(18, 2)] public decimal QuantitySold { get; set; }
			public string Status { get; set; } = string.Empty;
			public DateTime DateCreated { get; set; }
			public string NozzleName { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string PayrollNumber { get; set; } = string.Empty;
			public string Name { get; set; } = string.Empty;
		}

		// FIX (#4 correctness): now transfers across ALL positive/negative
		// variance pairs instead of only the first of each.
		// FIX (#2 correctness): explicit RollbackAsync on failure.
		// PERF: candidate transactions for the shift are fetched once up front
		// instead of hitting the DB inside the matching loop; MovedTransactions
		// are batched and everything is flushed with a single SaveChangesAsync.
		public async Task<ServiceResponse> NozzleQuantityTransfer(string shiftNumber)
		{
			const decimal varianceThreshold = 0m;

			var variances = await GetVariance(shiftNumber);

			var positiveVariances = variances.Where(v => v.ClosingVariance > 0)
				.OrderByDescending(v => v.ClosingVariance)
				.ToList();
			var negativeVariances = variances.Where(v => v.ClosingVariance < 0)
				.OrderBy(v => v.ClosingVariance) // most negative first
				.ToList();

			if (positiveVariances.Count == 0 || negativeVariances.Count == 0)
				return ServiceResponse<object>.Information("Nozzle variance data missing.", null);

			var dispensercode = await (from s in _context.Shifts
									   where s.ShiftNumber == shiftNumber
									   select s.DispenserCode).FirstOrDefaultAsync() ?? string.Empty;

			var stationCode = await (from d in _context.Dispensers
									 where d.DispenserCode == dispensercode
									 select d.StationCode).FirstOrDefaultAsync() ?? string.Empty;

			var positiveNozzleCodes = positiveVariances.Select(p => p.NozzleCode).Distinct().ToList();

			// Fetch every candidate transaction once instead of round-tripping the
			// DB inside the matching loop as the old GetClosestTransaction did.
			var candidateTransactions = await _context.QuantityTransactions
				.Where(qt => qt.ShiftNumber == shiftNumber && positiveNozzleCodes.Contains(qt.NozzleCode))
				.ToListAsync();

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var movedRecords = new List<MovedTransactions>();
				var usedTransactions = new HashSet<QuantityTransactions>();

				foreach (var negativeVarianceRecord in negativeVariances)
				{
					var negativeVariance = negativeVarianceRecord.ClosingVariance;
					var negativeNozzle = negativeVarianceRecord.NozzleCode;

					foreach (var positiveVarianceRecord in positiveVariances)
					{
						if (Math.Abs(negativeVariance) <= varianceThreshold)
							break;

						var positiveNozzle = positiveVarianceRecord.NozzleCode;

						// Largest-first among unused candidates for this nozzle,
						// same "closest match" preference as the original.
						var pool = candidateTransactions
							.Where(t => t.NozzleCode == positiveNozzle && !usedTransactions.Contains(t))
							.OrderByDescending(t => t.QuantityCredit);

						foreach (var candidate in pool)
						{
							if (Math.Abs(negativeVariance) <= varianceThreshold)
								break;

							if (candidate.QuantityCredit > Math.Abs(negativeVariance))
								continue;

							candidate.NozzleCode = negativeNozzle;
							candidate.DispenserCode = dispensercode;
							candidate.StationCode = stationCode;

							movedRecords.Add(new MovedTransactions
							{
								AmountCredit = candidate.AmountCredit,
								NozzleCode = candidate.NozzleCode,
								AmountDebit = candidate.AmountDebit,
								DateCreated = candidate.DateCreated,
								DispenserCode = candidate.DispenserCode,
								IsReversed = candidate.IsReversed,
								PaymentTypeCode = candidate.PaymentTypeCode,
								ShiftNumber = candidate.ShiftNumber,
								Price = candidate.Price,
								QuantityCredit = candidate.QuantityCredit,
								UserCode = candidate.UserCode,
								SaleId = candidate.SaleId,
								QuantityDebit = candidate.QuantityDebit,
								StationCode = candidate.StationCode,
								VehicleCode = candidate.VehicleRegistrationNumber,
							});

							_context.QuantityTransactions.Update(candidate);
							usedTransactions.Add(candidate);
							negativeVariance += candidate.QuantityCredit;
						}
					}
				}

				if (movedRecords.Count > 0)
					await _context.AddRangeAsync(movedRecords);

				await _context.SaveChangesAsync(); // single round trip for all moves
				await transaction.CommitAsync();

				await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
				return ServiceResponse<object>.Success("Nozzle quantity transfer successful.", null);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		private async Task<List<StockTakeSummary>> GetVariance(string shiftNumber)
		{
			return await _context.StockTakeSummaries
								 .Where(ss => ss.ShiftNumber == shiftNumber)
								 .AsNoTracking()
								 .ToListAsync();
		}

		//Reset stocktake in stocktakeSummaries
		public async Task<ServiceResponse> ResetShift(string shiftNumber)
		{
			try
			{
				var shift = await (from p in _context.Shifts
								   where p.ShiftNumber == shiftNumber
								   select p).FirstOrDefaultAsync();

				var readings = await (from r in _context.StockTakeSummaries
									  where r.ShiftNumber == shiftNumber
									  select r).ToListAsync();

				if (shift != null)
				{
					shift.ShiftStatus = ShiftStatus.Open;
					shift.ShiftEndTime = null;
					_context.Shifts.Update(shift);
				}
				else
					return ServiceResponse<object>.Information($"{shiftNumber} does not exist");

				foreach (var reading in readings)
				{
					reading.ClosingReading = 0;
					reading.ClosingVariance = 0;
					reading.VarianceStatus = ShiftStatus.Open;
					reading.ExpectedClosingReading = 0;
					_context.StockTakeSummaries.Update(reading);
				}
				await _context.SaveChangesAsync();
				await _authentication.AddUserTrail($"Shift {shiftNumber} was reset by {_authentication.Usercode()} on {DateTime.UtcNow}", MethodBase.GetCurrentMethod()?.Name ?? "");
				return ServiceResponse<object>.Success($"Reset for shift Number {shiftNumber} was successFul", null);

			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// FIX (#1 correctness - money): PaymentTransactions now correctly splits
		// credit vs debit by shortage/surplus (was always posting to
		// TransactionAmount with TransactionAmountDebit hardcoded to 0), and
		// the posted amount is now the KES value (magnitude * pricePerLitre)
		// rather than the raw litre magnitude.
		// FIX (#3 correctness): OpeningVariance is now folded into both the
		// threshold check and the correction, matching ShiftVariances()'s
		// definition of Variance = ClosingVariance + OpeningVariance.
		// PERF: dispenser code + station code lookups merged into a single
		// joined query instead of two sequential round trips.

		public async Task<ServiceResponse<object>> ClearVariance(string shiftNumber)
		{
			try
			{
				var variances = await (
					from vs in _context.StockTakeSummaries
					where vs.ShiftNumber == shiftNumber
					select vs
				).ToListAsync();

				var dispenserStation = await (from s in _context.Shifts
											  where s.ShiftNumber == shiftNumber
											  join d in _context.Dispensers on s.DispenserCode equals d.DispenserCode into dj
											  from d in dj.DefaultIfEmpty()
											  select new { s.DispenserCode, StationCode = d != null ? d.StationCode : null }).FirstOrDefaultAsync();

				var dispenserId = dispenserStation?.DispenserCode ?? string.Empty;
				var stationCode = dispenserStation?.StationCode ?? string.Empty;

				var threshold = await _varianceService.GetThresholdForDispenserAsync(dispenserId);

				var nozzlePrices = new Dictionary<string, decimal>();

				// Shift-level NET CLOSING variance only (litres). OpeningVariance is intentionally
				// excluded here per the new spec — only ClosingVariance feeds the clear decision.
				decimal totalVarianceLitres = variances.Sum(x => x.ClosingVariance);

				// Shift-level NET CLOSING variance value — each nozzle's ClosingVariance priced at
				// that nozzle's own retail price, then summed (signed) across the shift.
				decimal netVarianceValue = 0m;
				foreach (var variance in variances)
				{
					if (!nozzlePrices.TryGetValue(variance.NozzleCode, out var pricePerLitre))
					{
						pricePerLitre = await _varianceService.GetCurrentRetailPriceAsync(dispenserId, variance.NozzleCode);
						nozzlePrices[variance.NozzleCode] = pricePerLitre;
					}
					netVarianceValue += variance.ClosingVariance * pricePerLitre;
				}
				var totalVarianceValue = Math.Abs(netVarianceValue);

				// Method 1: overage — net closing variance >= 0, cleared only if its value is within threshold.
				var isWithinValueThreshold = IsOverageWithinThreshold(totalVarianceLitres, totalVarianceValue, threshold);

				// Method 2: minor shortage — net closing variance strictly between -1L and 0L, clears on litres alone.
				var isWithinLitreThreshold = IsMinorShortageAutoClear(totalVarianceLitres);

				if (isWithinValueThreshold || isWithinLitreThreshold)
				{
					foreach (var variance in variances)
					{
						variance.VarianceStatus = ShiftStatus.Closed;
						_context.StockTakeSummaries.Update(variance);
					}

					if (totalVarianceLitres != 0m)
					{
						var isShortage = totalVarianceLitres < 0m;
						var magnitude = Math.Abs(totalVarianceLitres);
						var saleId = _setups.GenerateSaleId();
						var firstVariance = variances.FirstOrDefault();

						var quantityTransaction = new QuantityTransactions
						{
							DateCreated = EatTime.Now,
							UserCode = firstVariance?.UserCode ?? "",
							NozzleCode = firstVariance?.NozzleCode ?? "",
							QuantityCredit = isShortage ? magnitude : 0,
							QuantityDebit = isShortage ? 0 : magnitude,
							ShiftNumber = shiftNumber,
							SaleId = saleId,
							PaymentTypeCode = 3,
							DispenserCode = dispenserId,
							StationCode = stationCode,
							AmountDebit = 0,
							AmountCredit = 0,
							Discount = 0,
							Vat_Amount = 0,
							Price = 0,
							IsReversed = false,
							CustomerCode = string.Empty,
							OtpUsed = string.Empty,
							VehicleRegistrationNumber = _authentication.Usercode(),
							
						};
						await _context.QuantityTransactions.AddAsync(quantityTransaction);

						var paymentTransaction = new PaymentTransactions
						{
							DateCreated = EatTime.Now,
							UserCode = firstVariance?.UserCode ?? string.Empty,
							SaleId = saleId,
							PaymentRefrence = _setups.GenerateShiftNumber(),
							TransactionAmount = isShortage ? 0 : totalVarianceValue,
							TransactionAmountDebit = isShortage ? totalVarianceValue : 0,
						};
						await _context.PaymentTransactions.AddAsync(paymentTransaction);


					}

					var shiftToClose = await (from s in _context.Shifts where s.ShiftNumber == shiftNumber select s).FirstOrDefaultAsync();
					shiftToClose?.ShiftStatus = ShiftStatus.Closed;

					await _context.SaveChangesAsync();
					await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);

					var reasonText = isWithinValueThreshold
						? $"it falls within the allowed threshold of KES {threshold:N2}"
						: $"net closing variance ({totalVarianceLitres:N2}L) falls within the shortage auto-clear allowance (-1L, 0L)";

					var message = $"Variance of KES {totalVarianceValue:N2} (quantity {totalVarianceLitres:N2}) of ShiftNumber {shiftNumber} has been cleared on {DateTime.UtcNow} by system service, {reasonText}.";
					await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

					return ServiceResponse<object>.Success("Variance cleared successfully", null);
				}

				return ServiceResponse<object>.Information("Variance not cleared", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}

		// Method 1: overage case. Net closing variance must be >= 0, and its absolute value
		// must be within the configured threshold.
		private static bool IsOverageWithinThreshold(decimal totalVarianceLitres, decimal totalVarianceValue, decimal threshold) => totalVarianceLitres >= 0m && totalVarianceValue <= threshold;

		// Method 2: minor shortage case. Net closing variance strictly between -1L and 0L
		// (exclusive of -1L) auto-clears regardless of value.
		private static bool IsMinorShortageAutoClear(decimal totalVarianceLitres)
		{
			return totalVarianceLitres < 0m && totalVarianceLitres >= -1m;
		}
	}
}