using DataAccessLayer.Common;

namespace BussinessLogic.CouponsService
{
	public interface ICouponsService
	{
		Task<ServiceResponse<object>> GetAllCouponsAsync();
	}
}