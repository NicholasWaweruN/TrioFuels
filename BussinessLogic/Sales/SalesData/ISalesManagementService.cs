using DataAccessLayer.Common;

namespace BussinessLogic.Sales.SalesData
{
	public interface ISalesManagementService
	{
		Task<ServiceResponse<object>> AllPaymentTypes();
		Task<ServiceResponse<DashBoard.DashBoard.SalesPagedResult>> AllSales(string? stationCode, string? shiftNumber = null, string? dispenserName = null, string? nozzleName = null, string? paymentTypeName = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10, string? orderByColumn = null, bool isDescending = true);
		Task<ServiceResponse<byte[]>> ExportSalesReport(DateTime date);
		Task<ServiceResponse<object>> GetPaymentTransactions(string transactionCode);
		Task<ServiceResponse<object>> GetSalesForShift(string shiftNumber, int pageNumber = 1, int pageSize = 10);
		Task<ServiceResponse<object>> MobileAppPaymentTypes();
		Task<ServiceResponse<byte[]>> MonthlySalesReport(int month, int year, CancellationToken ct = default);
		Task<ServiceResponse<object>> SalesPerShiftSummary();
		Task<ServiceResponse<object>> ViewPayments(string saleId);
	}
}