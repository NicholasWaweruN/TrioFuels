using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Authentication;

namespace BussinessLogic.Authentication.SignIn
{
	public interface ISignInUser
	{
		Task<ServiceResponse<object>> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword);
		Task<ServiceResponse<object>> CheckTillNumber(string dispenserCode);
		Task<ServiceResponse<object>> ForgotPassword(ResetPasswordModel reset);
		Task<Dictionary<string, decimal>> GetPriceList(string stationCode);
		Task<ServiceResponse<object>> ResetPasswordAsync(string newPassword, string confirmPassword);
		Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber);
		Task<ServiceResponse<object>> SignInUserAsync(EmailLoginModel signIn);
	}
}