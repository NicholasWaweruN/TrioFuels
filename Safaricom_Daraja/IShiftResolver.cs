using DataAccessLayer.Common;

namespace Safaricom_Daraja
{
	public interface IShiftResolver
	{
		Task<ServiceResponse<string>> GetCurrentShiftByTill(string tillNumber);
	}
}