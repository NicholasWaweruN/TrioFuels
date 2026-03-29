using BusinessLogic.Stock.Stock;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.Transactions;
using System.ComponentModel.DataAnnotations;

namespace BussinessLogic.Stock.Stock
{
	public interface IStockServicecs
	{
		Task<ServiceResponse<object>> AdjustStockTake([Required] int takeType, AdjustStockTakeDto adjust);
		Task<ServiceResponse<object>> AdjustStockTakes(AdjustStockTakeSummaryDto adjust);
		Task<ServiceResponse<byte[]>> ExportAllVariances();
		Task<ServiceResponse<object>> GetStockTakes(string date);
		Task<ServiceResponse<object>> GetTotalizerReadings();
		Task<ServiceResponse<object>> GetTotalizerReadings(DateTime date);
		Task<ServiceResponse<object>> InitialStockTake(StockTakeDto initialStockTakeDto);
		Task<ServiceResponse<object>> ListVariance(DateTime? date, string? shiftNumber, string? stationName);
		void MovedTransactions(MovedTransactions transactions);
		Task<ServiceResponse> NozzleQuantityTransfer(string shiftNumber);
		Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber);
		Task<ServiceResponse> ResetShift(string shiftNumber);
		Task<ServiceResponse<object>> ShiftVariances();
		Task<ServiceResponse<object>> StockTakeAsync(StockTakeDto stockTake);
	}
}