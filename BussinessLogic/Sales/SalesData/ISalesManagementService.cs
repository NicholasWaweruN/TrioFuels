using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;

namespace BussinessLogic.Sales.SalesData
{
	public interface ISalesManagementService
	{
		Task<ServiceResponse<object>> AllPaymentTypes();
		Task<ServiceResponse<DashBoard.DashBoard.SalesPagedResult>> AllSales(string? stationCode, string? shiftNumber = null, string? dispenserName = null, string? nozzleName = null, string? paymentTypeName = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10, string? orderByColumn = null, bool isDescending = true);
		Task<ServiceResponse<List<SalesManagementService.DashboardSalesSummaryDto>>> DashboardSalesSummary();
		Task<ServiceResponse<byte[]>> ExportCustomerTransactions();
		Task<ServiceResponse<byte[]>> ExportSalesReport(DateTime date);
		Task<ServiceResponse<SalesManagementService.PaginatedResponse<List<SalesDto>>>> FilterSales(SalesManagementService.SalesFilterDto filter);
		Task<ServiceResponse<decimal>> GetCustomerBalance(string vehicleCode);
		Task<ServiceResponse<object>> GetFuelingEventsForVehicle(string vehicleCode, int page = 1, int pageSize = 50, CancellationToken ct = default);
		Task<ServiceResponse<object>> GetPaymentTransactions(string transactionCode);
		Task<ServiceResponse<object>> GetSalesData(DateTime date);
		Task<ServiceResponse<List<SalesDto>>> GetSalesForPaymentType(string paymentTypeCode);
		Task<ServiceResponse<List<SalesDto>>> GetSalesForShift(string shiftNumber);
		Task<ServiceResponse<object>> GetSalesForShift(string shiftNumber, int pageNumber = 1, int pageSize = 10);
		Task<ServiceResponse<List<SalesDto>>> GetSalesForVehicle(string vehicleCode);
		Task<ServiceResponse<object>> GetSalesForVehicle(string vehicleCode, int pageNumber = 1, int pageSize = 10);
		Task<ServiceResponse<object>> MobileAppPaymentTypes();
		Task<ServiceResponse<byte[]>> MonthlySalesReport(int month, int year, CancellationToken ct = default);
		Task<ServiceResponse<List<ReversedSalesDto>>> ReversedSales(DateTime dateFrom, DateTime dateTo);
		Task<ServiceResponse<List<SalesDto>>> Sales(SalesListDto sales);
		Task<ServiceResponse<object>> SalesForecast();
		Task<ServiceResponse<object>> SalesPerShiftSummary();
		Task<ServiceResponse<ShiftSummaryDto>> SalesShiftSummarySummary();
		Task<ServiceResponse<List<StationSummaryDto>>> SalesSummaryPerStation(string stationCode, DateTime date);
		Task<ServiceResponse<object>> SalesThisMonth();
		Task<ServiceResponse<object>> ViewPayments(string saleId);
	}
}