using DataAccessLayer.Common;

namespace BusinessLogic.Sales.CommonSalesTasks
{
	public interface ICommonSalesTasks
	{
		Task<ServiceResponse<object>> ReconcileStockSummariesAsync(string shiftNumber);
		Task<ServiceResponse<object>> UpdateMpesaPaymentStatus(string transId);
	}
}