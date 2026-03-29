using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Authentication;

namespace BusinessLogic.Authentication.Token
{
	public interface ITokenManagement
	{
		Task<TokenDto> CreateToken(ApplicationUser user,string appCode);
		Task<ServiceResponse<object>> ExpireTokenAsync(string token);
		Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber);
	}
}