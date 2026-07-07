using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;

namespace BussinessLogic.Sales.MissingSales
{
	public interface IMisingSale
	{
		Task<ServiceResponse<object>> AddSalesAsync(MisingSaleDto sales);
		Task<ServiceResponse> DeferVariance(string shiftNumber);
		Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber);
		Task<MisingSale.StationData> StationsName(string dispenserCode);
		Task<string> TillNumber(string dispenserCode);
	}
}