using DataAccessLayer.Common;

namespace Safaricom_Daraja.Helpers
{
	public interface IShiftResolver
	{
		Task<ServiceResponse<string>> GetCurrentShiftByTill(string tillNumber);
	}
}