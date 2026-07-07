using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using BussinessLogic.Stock.Stock;
using BussinessLogic.Stock.Variance_Service;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Stock.Shifts
{
	public class Shifts : IShifts
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly IStockTakeVarianceService _stockTakeVarianceService;
		public Shifts(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, IStockTakeVarianceService stockTakeVarianceService)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_stockTakeVarianceService = stockTakeVarianceService;
		}
		//check if a user has an open shift


		//All dispenser status either closed or open
		public async Task<ServiceResponse<object>> DispenserStatus()
		{
			var result = await (from d in _context.Dispensers
								join s in _context.Stations on d.StationCode equals s.StationCode
								join u in _context.Users on d.UserCode equals u.UserCode into userGroup // Left join with Users
								from user in userGroup.DefaultIfEmpty() // Handle nulls for left join
								let status = (from shift in _context.Shifts
											  where shift.ShiftStatus == 1 && d.DispenserCode == shift.DispenserCode
											  orderby shift.DateCreated // Modify ordering if needed for your data
											  select "Open").FirstOrDefault()
								select new
								{
									StationName = s.StationName.ToUpper(),
									d.DispenserName,
									Status = status ?? "Closed",
									UserName = user != null ? user.UserName : "",
								}).ToListAsync();

			if (result.Count > 0)
			{
				return ServiceResponse<object>.Success("Success", result);
			}

			return ServiceResponse<object>.Information("No shifts found", null);
		}



		#region SHIFTstatus

		public async Task<ServiceResponse<object>> ShiftStatuses()
		{
			var userCode = _authentication.Usercode();

			// FIX 1: one query instead of 3 AnyAsync calls. Pull the relevant shifts
			// once, then decide priority (Open > Pending > Variance) in memory.
			var relevantShifts = await _context.Shifts
				.Where(x => x.UserCode == userCode &&
							(x.ShiftStatus == ShiftStatus.Open ||
							 x.ShiftStatus == ShiftStatus.Pending ||
							 x.ShiftStatus == ShiftStatus.Variance))
				.Select(x => new { x.ShiftNumber, x.ShiftStatus })
				.ToListAsync();

			var openShift = relevantShifts.FirstOrDefault(x => x.ShiftStatus == ShiftStatus.Open);
			var pendingShift = relevantShifts.FirstOrDefault(x => x.ShiftStatus == ShiftStatus.Pending);
			var varianceShiftRow = relevantShifts.FirstOrDefault(x => x.ShiftStatus == ShiftStatus.Variance);

			if (openShift != null)
			{
				var shiftNumber = openShift.ShiftNumber;
				var userDispenser = await GetDispenserAssignedToUserAsync();

				var dispenserNozzleCodes = await _context.Nozzles
					.Where(n => n.DispenserCode == userDispenser)
					.OrderBy(n => n.NozzleCode)
					.Select(n => n.NozzleCode)
					.ToListAsync();

				var nozzle1Code = dispenserNozzleCodes.ElementAtOrDefault(0);
				var nozzle2Code = dispenserNozzleCodes.ElementAtOrDefault(1);

				// FIX 2: replaced 5 separate Sum/Count queries with a single grouped
				// query. Only aggregates by NozzleCode; cash-at-hand is filtered from
				// the same in-memory result instead of a 5th round trip.
				var perNozzle = await _context.QuantityTransactions
					.Where(x => x.ShiftNumber == shiftNumber)
					.GroupBy(x => x.NozzleCode)
					.Select(g => new
					{
						NozzleCode = g.Key,
						Quantity = g.Sum(x => x.QuantityCredit + x.QuantityDebit),
						Count = g.Count(),
						Cash = g.Where(x => x.PaymentTypeCode == 12).Sum(x => x.AmountCredit - x.AmountDebit)
					})
					.ToListAsync();

				var totalQuantitySold = perNozzle.Sum(x => x.Quantity);
				var gettotalevents = perNozzle.Sum(x => x.Count);
				var cashAtHand = perNozzle.Sum(x => x.Cash);
				var nozzle1Quantity = perNozzle.FirstOrDefault(x => x.NozzleCode == nozzle1Code)?.Quantity ?? 0m;
				var nozzle2Quantity = perNozzle.FirstOrDefault(x => x.NozzleCode == nozzle2Code)?.Quantity ?? 0m;

				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "You have an open shift",
					ResponseObject = new ShiftSummary
					{
						ShiftStatus = ShiftStatus.Open,
						ShiftNumber = shiftNumber,
						QuantitySold = totalQuantitySold,
						TotalEvents = gettotalevents,
						CashAtHand = cashAtHand,
						IsStockTakeTaken = true,
						Nozzle1 = nozzle1Quantity,
						Nozzle2 = nozzle2Quantity,
					}
				};
			}
			else if (pendingShift != null)
			{
				var varianceRows = await (from vs in _context.StockTakeSummaries
										  join s in _context.Shifts on vs.ShiftNumber equals s.ShiftNumber
										  join n in _context.Nozzles on vs.NozzleCode equals n.NozzleCode
										  where vs.UserCode == userCode && vs.VarianceStatus == ShiftStatus.Variance
										  select new
										  {
											  vs.OpeningVariance,
											  vs.ClosingVariance,
											  vs.NozzleCode,
											  n.NozzleName,
											  s.ShiftNumber,
										  }).ToListAsync();

				var variances = varianceRows.Select(v => new Variances
				{
					ShiftNumber = v.ShiftNumber,
					NozzleCode = v.NozzleCode,
					NozzleName = v.NozzleName,
					Variance = v.OpeningVariance + v.ClosingVariance
				}).ToList();

				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "You have a pending shift",
					ResponseObject = new VariancesList
					{
						ShiftStatus = ShiftStatus.Pending,
						variances = variances
					}
				};
			}
			else if (varianceShiftRow != null)
			{
				var userDispenser = await GetDispenserAssignedToUserAsync();

				var varianceRows = await (from vs in _context.StockTakeSummaries
										  join s in _context.Shifts on vs.ShiftNumber equals s.ShiftNumber
										  join n in _context.Nozzles on vs.NozzleCode equals n.NozzleCode
										  where vs.UserCode == userCode && vs.VarianceStatus == ShiftStatus.Variance
										  select new
										  {
											  vs.OpeningVariance,
											  vs.ClosingVariance,
											  vs.NozzleCode,
											  n.NozzleName,
											  s.ShiftNumber,
										  }).ToListAsync();

				// FIX 3 (revised): Task.WhenAll caused "A second operation was started
				// on this context instance" because GetCurrentRetailPriceAsync shares
				// the same DbContext, and DbContext is NOT thread-safe for concurrent
				// operations. Fetching sequentially avoids the crash. This still does
				// N round trips — if GetCurrentRetailPriceAsync's query is simple,
				// consider adding a batched GetCurrentRetailPricesAsync(dispenserCode,
				// IEnumerable<int> nozzleCodes) returning a Dictionary<int, decimal>
				// to cut this to a single query.
				var prices = new decimal[varianceRows.Count];
				for (int i = 0; i < varianceRows.Count; i++)
				{
					prices[i] = await _stockTakeVarianceService.GetCurrentRetailPriceAsync(userDispenser, varianceRows[i].NozzleCode);
				}

				var variances = varianceRows.Select((v, i) =>
				{
					var totalVarianceLitres = v.OpeningVariance + v.ClosingVariance;
					return new Variances
					{
						ShiftNumber = v.ShiftNumber,
						NozzleCode = v.NozzleCode,
						NozzleName = v.NozzleName,
						Variance = totalVarianceLitres,
						VarianceValue = totalVarianceLitres * prices[i]
					};
				}).ToList();

				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "You have a variance shift",
					ResponseObject = new VariancesList
					{
						ShiftStatus = ShiftStatus.Variance,
						variances = variances
					}
				};
			}
			else
			{
				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "Kindly Continue to open a shift",
					ResponseObject = new Shiftstatus
					{
						ShiftStatus = ShiftStatus.Closed
					}
				};
			}
		}

		#endregion


		//get shift status

		private async Task<string> GetDispenserAssignedToUserAsync()
		{
			return await _context.DispenserAssignments
				.Where(a => a.AttedantUserCode == _authentication.Usercode())
				.Select(a => a.DispenserCode)
				.FirstOrDefaultAsync() ?? string.Empty;
		}

		//get a list of sales for a particular shift
		public async Task<ServiceResponse<object>> ShiftSales()
		{
			var shiftNumber = await _context.Shifts
				.AsNoTracking()
				.Where(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == ShiftStatus.Open)
				.Select(x => x.ShiftNumber) // cast to nullable so "no shift" is detectable regardless of underlying type
				.FirstOrDefaultAsync();

			if (shiftNumber == null)
			{
				return new ServiceResponse<object>
				{
					ResponseCode = Response.Information,
					ResponseMessage = "No open shift found",
					ResponseObject = null
				};
			}

			var sales = await _context.FuelSales
				.AsNoTracking()
				.Where(x => x.ShiftNumber == shiftNumber && !x.IsReversed && x.Litres >= 0)
				.Select(qt => new
				{
					VehicleRegistrationNumber = qt.Vehicle,
					QuantityCredit = qt.Litres,
					AmountCredit = qt.Amount,
					qt.Price,
					PaymentTypeName = qt.PaymentType ?? "Unknown",
					DateCreated = qt.SalesDate,
					ReceiptNumber = qt.SaleId,
					ServedBy = qt.AttendantName,
					qt.CustomerName,
					qt.PetroleumName,
					qt.TillNumber,
					qt.StationName,
				})
				.ToListAsync();

			if (sales.Count == 0)
			{
				return new ServiceResponse<object>
				{
					ResponseCode = Response.Information,
					ResponseMessage = "No sales found",
					ResponseObject = null
				};
			}

			// Formatting done once, in memory, over the already-materialized list —
			// not inside the query expression.
			var shiftSales = sales.Select(x => new
			{
				x.VehicleRegistrationNumber,
				x.QuantityCredit,
				x.AmountCredit,
				x.Price,
				x.PaymentTypeName,
				x.DateCreated,
				Time = x.DateCreated.ToString("HH:mm:ss"),
				x.ReceiptNumber,
				x.ServedBy,
				x.CustomerName,
				x.PetroleumName,
				x.TillNumber,
				x.StationName,
			}).ToList();

			return new ServiceResponse<object>
			{
				ResponseCode = Response.Success,
				ResponseMessage = "Shift sales",
				ResponseObject = shiftSales
			};
		}

		//list all open shifts
		public async Task<ServiceResponse<object>> OpenShifts()
		{
			var result = await (from d in _context.Dispensers
								join s in _context.Stations on d.StationCode equals s.StationCode
								let status = (from shift in _context.Shifts
											  where shift.ShiftStatus == 1 && d.DispenserCode == shift.DispenserCode
											  orderby shift.DateCreated // if necessary, to select top 1, based on your data model
											  select "Open").FirstOrDefault()
								select new
								{
									StationName = s.StationName.ToUpper(),
									d.DispenserName,
									Status = status ?? "Closed"
								}).FirstOrDefaultAsync();

			if (result is not null)
			{
				return ServiceResponse<object>.Success("Success", result);
			}
			return ServiceResponse<object>.Information("no shifts found", null);
		}
		//Force close a shift
		public async Task<ServiceResponse<object>> ForceCloseShift(string ShiftNumber)
		{
			if (ShiftNumber is null)
			{
				return ServiceResponse<object>.Information("Shift number can not be empty", null);
			}
			var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftNumber == ShiftNumber);
			if (shift is null)
			{
				return ServiceResponse<object>.Information("Shift number not found", null);
			}

			var summary = await (from ss in _context.StockTakeSummaries
								 where ss.ShiftNumber == ShiftNumber
								 select ss
								 ).ToListAsync();
			foreach (var item in summary)
			{
				var quantitySold = await QuantitySold(ShiftNumber, item.NozzleCode);
				var reading = await (from a in _context.QuantityTransactions
									 where a.NozzleCode.Equals(item.NozzleCode)
									 select a).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

				item.ExpectedClosingReading = reading;
				item.ClosingReading = reading;
				_context.StockTakeSummaries.Update(item);

				await _context.SaveChangesAsync();
				await ReconcileStockSummariesAsync(item.NozzleCode, ShiftNumber);
			}

			shift.ShiftStatus = ShiftStatus.Closed;
			shift.ShiftEndTime = DateTime.UtcNow;

			_context.Shifts.Update(shift);
			await _context.SaveChangesAsync();
			return ServiceResponse<object>.Success("Shift closed successfully", null);
		}

		public class ShiftCloseDto
		{
			public string ShiftNumber { get; set; } = string.Empty;
			public string ShiftStatus { get; set; } = string.Empty;
			public string DateClosed { get; set; } = string.Empty;
		}
		//get Quantity sold for a particular shift
		private async Task<ServiceResponse<decimal>> QuantitySold(string shiftNumber, string nozzleCode)
		{
			var totalQuantitySold = await (from q in _context.QuantityTransactions
										   where q.ShiftNumber == shiftNumber
										   && q.NozzleCode == nozzleCode
										   select q).SumAsync(x => x.QuantityCredit + x.QuantityDebit);

			return new ServiceResponse<decimal>
			{
				ResponseCode = Response.Success,
				ResponseMessage = "Quantity sold",
				ResponseObject = totalQuantitySold
			};
		}
		private async Task<ServiceResponse<object>> ReconcileStockSummariesAsync(string nozzleCode, string shiftNumber)
		{
			var stockSummary = await _context.StockTakeSummaries
											 .FirstOrDefaultAsync(s => s.ShiftNumber == shiftNumber && s.NozzleCode == nozzleCode);

			if (stockSummary == null)
				return ServiceResponse<object>.Information("No stocktake summary found", null);

			var totalSales = await _context.QuantityTransactions
											.Where(s => s.ShiftNumber == shiftNumber)
											.SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			stockSummary.QuantitySold = totalSales;
			stockSummary.ClosingVariance = stockSummary.OpeningReading - stockSummary.ClosingReading + totalSales;
			stockSummary.VarianceStatus = stockSummary.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			stockSummary.ExpectedClosingReading = stockSummary.OpeningReading + totalSales;

			await _context.SaveChangesAsync();
			return ServiceResponse<object>.Success("Stock reconciled successfully", null);
		}
	}
}