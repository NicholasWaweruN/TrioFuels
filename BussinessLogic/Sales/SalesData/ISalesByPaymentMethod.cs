using DataAccessLayer.Common;

namespace BussinessLogic.Sales.SalesData
{
	public interface ISalesByPaymentMethod
	{
		Task<ServiceResponse<List<SalesByPaymentMethod.SalesByPaymentMethodDto>>> GetSalesByPaymentMethodAsync();
	}
}