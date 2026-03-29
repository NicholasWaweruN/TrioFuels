using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using BussinessLogic.Setup;

namespace BussinessLogic.Stock.Adjust_Stock
{
	public class AdjustStock : IAdjustStock
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;

		public AdjustStock(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
		}

		public async Task<ServiceResponse<object>> AdjustStockTake([Required] int takeType, AdjustStockTakeDto adjust)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				foreach (var reading in adjust.Readings)
				{
					var stocktake = await GetStockTakeByNozzleAndShiftAsync(reading.NozzleCode, adjust.ShiftNumber);
					var stocktakeSummary = await GetStockTakeSummaryByNozzleAndShiftAsync(reading.NozzleCode, adjust.ShiftNumber);

					if (stocktake == null || stocktakeSummary == null)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("Stock take or summary not found", null);
					}

					await AdjustStockTakeValuesAsync(stocktake, stocktakeSummary, takeType, reading.Reading);
				}

				await LogStockAdjustmentTrail(adjust.ShiftNumber);
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				return ServiceResponse<object>.Success("Stock take adjusted successfully", null);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(ex);
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		private async Task AdjustStockTakeValuesAsync(StockTake stocktake, StockTakeSummary stocktakeSummary, int takeType, decimal reading)
		{
			var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.ShiftNumber.Equals(stocktake.ShiftNumber)) ?? throw new InvalidOperationException("Shift not found.");
			if (takeType == 2)
			{
				await HandleClosingReadingAsync(stocktake, stocktakeSummary, shift, reading);
			}
			else
			{
				HandleOpeningReading(stocktake, stocktakeSummary, shift, reading);
			}

			_context.Update(stocktake);
			_context.Update(shift);
			_context.StockTakeSummaries.Update(stocktakeSummary);
		}

		private async Task HandleClosingReadingAsync(StockTake stocktake, StockTakeSummary stocktakeSummary, Shift shift, decimal reading)
		{
			stocktake.ClosingReading = reading;
			stocktakeSummary.ClosingReading = reading;
			stocktakeSummary.ClosingVariance = reading - stocktakeSummary.ExpectedClosingReading;

			stocktakeSummary.VarianceStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			shift.ShiftStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;

			var subsequentSummaries = await _context.StockTakeSummaries
				.Where(x => x.OpeningVariance > 0 && x.Id > stocktakeSummary.Id && x.NozzleCode == stocktakeSummary.NozzleCode)
				.ToListAsync();

			foreach (var summary in subsequentSummaries)
			{
				summary.OpeningVariance = 0;
				summary.VarianceStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Open : ShiftStatus.Variance;
				shift.ShiftStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Open : ShiftStatus.Variance;
			}
		}

		private static void HandleOpeningReading(StockTake stocktake, StockTakeSummary stocktakeSummary, Shift shift, decimal reading)
		{
			stocktake.OpeningReading = reading;
			stocktakeSummary.OpeningReading = reading;
			stocktakeSummary.OpeningVariance = reading - stocktakeSummary.ExpectedOpeningReading;

			stocktakeSummary.VarianceStatus = stocktakeSummary.OpeningVariance == 0 ? ShiftStatus.Open : ShiftStatus.Variance;
			shift.ShiftStatus = stocktakeSummary.OpeningVariance == 0 ? ShiftStatus.Open : ShiftStatus.Variance;
		}

		private async Task LogStockAdjustmentTrail(string shiftNumber)
		{
			var message = $"Stock adjusted by {_authentication.Name()} on {DateTime.UtcNow} for shift number {shiftNumber}";
			await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
		}

		private async Task HandleExceptionAsync(Exception ex)
		{
			var methodName = ex.TargetSite?.Name ?? string.Empty;
			await _authentication.ErrorTrail(new ErrorTrail
			{
				DateCreated = DateTime.UtcNow,
				ErrorCode = "004",
				ErrorMessage = ex.Message,
				Method = methodName
			});
		}

		private Task<StockTake?> GetStockTakeByNozzleAndShiftAsync(string nozzleCode, string shiftNumber)
		{
			return _context.StockTakes.FirstOrDefaultAsync(x => x.NozzleCode == nozzleCode && x.ShiftNumber == shiftNumber);
		}

		private Task<StockTakeSummary?> GetStockTakeSummaryByNozzleAndShiftAsync(string nozzleCode, string shiftNumber)
		{
			return _context.StockTakeSummaries.FirstOrDefaultAsync(x => x.NozzleCode == nozzleCode && x.ShiftNumber == shiftNumber);
		}
	}
}
