using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;

namespace BussinessLogic.Sales.NewSales
{
	public interface ISales
	{
		Task<ServiceResponse<object>> AddSalesAsync(AddsaleDto sales);
		Task<ServiceResponse<bool>> CheckDuplicates(AddsaleDto sales);

		Task<ServiceResponse<MpesaManualConfirmationDto?>> ConfirmMpesaManualAsync(string transId, CancellationToken ct);
		Task<(decimal NewPrice, decimal Discount)> GetPriceAsync(string productCode, string stationCode, string vehicleCode);
	}
}