using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Authentication;
using System.ComponentModel.DataAnnotations;
using static BussinessLogic.Authentication.SignIn.SignInUser;

namespace BussinessLogic.Authentication.SignIn
{
	public interface ISignInUser
	{
		Task<ServiceResponse<object>> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword);
		Task<ServiceResponse<object>> CheckTillNumber(string dispenserCode);
		Task<ServiceResponse<object>> ForgotPassword(ResetPasswordModel reset);
		Task<ServiceResponse<object>> GetAttendantsForLogin(string stationCode);
		Task<Dictionary<string, decimal>> GetPriceList(string stationCode);
		Task<ServiceResponse<object>> PinSignIn(PinSignInModel model);
		Task<ServiceResponse<object>> ResetPasswordAsync(string newPassword, string confirmPassword);
		Task<ServiceResponse<object>> SendOTP(string phoneNumber);
		Task<ServiceResponse<object>> SendOTPAsync([EmailAddress] string email);
		Task<ServiceResponse<object>> SetAttendantPin(string targetUserCode, string newPin);
		Task<ServiceResponse<object>> SignInUserAsync(EmailLoginModel signIn);
		Task<ServiceResponse<object>> VerifyOTPAsync(string phoneNumber, string otp);
	}
}