using DataAccessLayer.Common;

namespace BussinessLogic.CouponsService
{
	public interface ILoyaltyProgramSubscription
	{
		Task<ServiceResponse<object>> AddSubscriptionAsync(LoyaltyProgramSubscription.CreateLoyaltySubscriptionDto dto);
	}
}