using BusinessLogic.Worker.PriceScheduler;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Setups;

namespace BusinessLogic.SetupService
{
	public interface IUserSetups
	{
		Task<ServiceResponse<object>> AddPaymentType(string paymentType);
		Task<ServiceResponse<object>> AddPrice(List<UpdatePrice> updatePrice);
		Task<ServiceResponse> AddPriceSchedule(List<PriceChangeSchedule> schedule);
		Task<ServiceResponse<object>> AddProduct(AddProductDto product);
		Task<ServiceResponse<object>> AddRecipients(int type, string reportCode, string email);
		Task<ServiceResponse<object>> ChangePriceForAllStations(string productCode, decimal newPrice);
		Task<ServiceResponse<object>> GetProducts();
		Task<ServiceResponse<object>> GetRecipients(string reportCode);
		Task<ServiceResponse<object>> RegisterPDA(string deviceName, string deviceIMEI, string deviceSerialNumber, string deviceModel, string dispensercode);
		Task<ServiceResponse<object>> RemoveEmailRecipients(string email, string reportCode);
		List<UserSetups.Report> Reports();
	}
}