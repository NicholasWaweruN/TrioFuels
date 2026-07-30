using DataAccessLayer.Common;

namespace BussinessLogic.Sales.SalesData
{
	public interface IVehicleSales
	{
		Task<ServiceResponse<byte[]>> ExportFuelSalesToExcelAsync(FuelSaleFilterDto filter);
		Task<ServiceResponse<object>> GetFuelSalesAsync(FuelSaleFilterDto filter);
	}
}