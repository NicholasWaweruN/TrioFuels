using BusinessLogic.SetupService;
using BusinessLogic.Worker.PriceScheduler;
using DataAccessLayer.Common;

namespace BussinessLogic.Setup
{
	public interface IUserSetups
	{
		Task<ServiceResponse> AddPriceSchedule(List<PriceChangeSchedule> schedule);
		Task<ServiceResponse<object>> AddProduct(AddProductDto product);
		Task<ServiceResponse<object>> AddRecipients(int type, string reportCode, string email);
		Task<ServiceResponse<object>> ChangePriceForAllStations(string productCode, decimal newPrice);
		Task<ServiceResponse<object>> GetPriceByStation(string stationCode, string productCode);
		Task<ServiceResponse<object>> GetPriceInfo(string nozzleCode);
		Task<ServiceResponse<object>> GetProducts();
		Task<ServiceResponse<object>> GetRecipients(string reportCode);
		Task<ServiceResponse<object>> RemoveEmailRecipients(string email, string reportCode);
		List<UserSetups.Report> Reports();
		Task<ServiceResponse<object>> UpdatePrice(string productCode, string stationCode, decimal newAmount);
		Task<ServiceResponse<object>> PriceChange(string productCode, decimal newPrice);
	}
}