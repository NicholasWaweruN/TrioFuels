using DataAccessLayer.Common;
using static BussinessLogic.Sales.SalesData.SalesByPaymentMethod;

namespace BussinessLogic.Sales.SalesData
{
	public interface ISalesByPaymentMethod
	{
		Task<ServiceResponse<List<SalesByPaymentMethod.SalesByPaymentMethodDto>>> GetSalesByPaymentMethodAsync();
		Task<ServiceResponse<List<SalesPerNozzleDto>>> GetSalesPerNozzleAsync();
	}
}