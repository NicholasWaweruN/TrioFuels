using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
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
		public Shifts(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
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


		//get shift status
		public async Task<ServiceResponse<object>> ShiftStatuses()
		{
			var userCode = _authentication.Usercode();
			var HasOpenshift = await _context.Shifts.AnyAsync(x => x.UserCode == userCode && x.ShiftStatus == ShiftStatus.Open);
			var HasPendingShift = await _context.Shifts.AnyAsync(x => x.UserCode == userCode && x.ShiftStatus == ShiftStatus.Pending);
			var HasVarianceShift = await _context.Shifts.AnyAsync(x => x.UserCode == userCode && x.ShiftStatus == ShiftStatus.Variance);
			if (HasOpenshift)
			{
				var userDispenser = await GetDispenserAssignedToUserAsync();
				//get total quantity sold for a particular shift
				var shiftNumber = await _context.Shifts.Where(x => x.UserCode == userCode && x.ShiftStatus == ShiftStatus.Open).Select(x => x.ShiftNumber).FirstOrDefaultAsync();
				if (shiftNumber != null)
				{
					var totalQuantitySold = await _context.QuantityTransactions.Where(x => x.ShiftNumber == shiftNumber).SumAsync(x => x.QuantityCredit + x.QuantityDebit);
					var gettotalevents = await _context.QuantityTransactions.Where(x => x.ShiftNumber == shiftNumber).CountAsync();
					var cashAtHand = await _context.QuantityTransactions.Where(x => x.ShiftNumber == shiftNumber && x.PaymentTypeCode == 12).SumAsync(x => x.AmountCredit-x.AmountDebit);
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
							IsStockTakeTaken =  true,
						}

					};
				}
				return ServiceResponse<object>.Success("You have an open shift", null);
			}
			else if (HasPendingShift)
			{
				var shiftNumber = await _context.Shifts.Where(x => x.UserCode == userCode && x.ShiftStatus == ShiftStatus.Pending).Select(x => x.ShiftNumber).FirstOrDefaultAsync();

				var varianceShift = await (from vs in _context.StockTakeSummaries
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

				var variances = new List<Variances>();
				foreach (var variance in varianceShift)
				{
					variances.Add(new Variances
					{
						ShiftNumber = variance.ShiftNumber,
						NozzleCode = variance.NozzleCode,
						NozzleName = variance.NozzleName,
						Variance = variance.OpeningVariance + variance.ClosingVariance
					});
				}

				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "You have a pending shift",
					ResponseObject =
						new VariancesList
						{
							ShiftStatus = ShiftStatus.Pending,
							variances = variances
						}
				};
			}
			else if (HasVarianceShift)
			{
				var userDispenser = await GetDispenserAssignedToUserAsync();
				var price = await (from p in _context.Prices
								   where p.DispenserCode == userDispenser
								   select p.Amount).FirstOrDefaultAsync();

				// get from stocksummary where shiftstatus = variance and usercode = usercode
				var varianceShift = await (from vs in _context.StockTakeSummaries
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

				var variances = new List<Variances>();
				foreach (var variance in varianceShift)
				{
					variances.Add(new Variances
					{
						ShiftNumber = variance.ShiftNumber,
						NozzleCode = variance.NozzleCode,
						NozzleName = variance.NozzleName,
						Variance = variance.OpeningVariance + variance.ClosingVariance,
						VarianceValue = (variance.OpeningVariance + variance.ClosingVariance) * price
					});
				}

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
			//get user current shift
			var ShiftNumber = await _context.Shifts.Where(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == ShiftStatus.Open).Select(x => x.ShiftNumber).FirstOrDefaultAsync();
			var shiftSales = await (from qt in _context.QuantityTransactions
									join v in _context.Vehicles on qt.VehicleCode equals v.VehicleCode
									where qt.ShiftNumber == ShiftNumber
									select new
									{
										v.VehicleRegistrationNumber,
										qt.QuantityCredit,
										Time = qt.DateCreated.Hour + ":" + qt.DateCreated.Minute + ":" + qt.DateCreated.Second,
										qt.AmountCredit,
										qt.DateCreated,
									}).OrderByDescending(x => x.DateCreated).ToListAsync();

			if (shiftSales.Count > 0)
			{

				return new ServiceResponse<object>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "Shift sales",
					ResponseObject = shiftSales.OrderBy(x => x.Time).ToList()
				};
			}
			return new ServiceResponse<object>
			{
				ResponseCode = Response.Information,
				ResponseMessage = "No sales found",
				ResponseObject = null
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