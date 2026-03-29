using DataAccessLayer.Common;

namespace BusinessLogic.EmailService
{
	public interface IMessagingService
	{
		string GetOtp();
		bool IsValidPhoneNumber(string phoneNumber);
		string NormalizePhoneNumber(string phoneNumber);
		Task<ServiceResponse<bool>> SaveOtpAsync(string phoneNumber, string otp);
		bool SendBulkSms(string recepients, string message);
		Task<ServiceResponse<object>> SendOTPAsync(string phoneNumber);
		Task<bool> SendSms(string recepient, string otp);
		Task<bool> SendSmsAsync(string recipient, string otp);
	}
}