using DataAccessLayer.Common;

namespace BusinessLogic.Sales.ReverseSales
{
	public interface IReverseSales
	{
		Task<ServiceResponse<object>> ReverseSaleAsync(string saleId);
		Task<ServiceResponse<object>> TransferSaleToAnotherNozzle(string transactionCode, string nozzleCode);
	}
}