using DataAccessLayer.Common;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Sales.Wallet.Voucher
{
	public interface IVoucherService
	{
		Task<byte[]> GenerateActiveVouchersExcelAsync();
		Task<VoucherService.PaginatedResult<VoucherService.ActiveVoucherDto>> GetAllVouchersWithVehiclesAsync (int page = 1, int pageSize = 50);
		Task<ServiceResponse<object>> UploadVouchersFromExcel(IFormFile file, string userCode);
	}
}