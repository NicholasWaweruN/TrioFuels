using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;

namespace BussinessLogic.Sales.MissingSales
{
	public interface IMisingSale
	{
		Task<ServiceResponse<object>> AddSalesAsync(MisingSaleDto sales);
		Task<ServiceResponse> DeferVariance(string shiftNumber);
		Task<ServiceResponse> OffWriteVariance(string shiftNumber);
		Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber);
		Task<MisingSale.StationData> StationsName(string dispenserCode);
		Task<string> StoreNumber(string dispenserCode);
		Task<ServiceResponse<object>> ValidateVoucherAsync(string voucherNo);
		Task<ServiceResponse<byte[]>> WalletTopUps(DateTime dateFrom, DateTime dateTo);
	}
}