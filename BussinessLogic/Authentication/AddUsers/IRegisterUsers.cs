using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Authentication;

namespace BusinessLogic.Authentication.AddUsers
{
	public interface IRegisterUsers
	{
		Task<ServiceResponse<object>> ActivateUserAsync(string userCode);
		Task<ServiceResponse<object>> ConfirmPhoneNumberWithOtpAsync(string phoneNumber, string otp);
		Task<ServiceResponse<object>> DeactivateUserAsync(string userCode);
		Task<ServiceResponse<object>> GetAllUsersAsync();
		Task<ServiceResponse<object>> RegisterUserAsync(RegisterModel register);
		Task<ServiceResponse<object>> UpdateUserDetailsAsync(string userCode, UpdateUsers updateUser);
	}
}