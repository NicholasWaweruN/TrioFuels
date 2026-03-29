using DataAccessLayer.DTOs.Messaging;

namespace BusinessLogic.Messaging
{
	public interface IAfricaIsTalking
	{
		void ReceiveCallBacks(SmsCallbackRequest callback);
		void SendBulkSms(List<string> recipients, string message);
		Task<bool> SendSms(string phoneNumber, string message);
		List<string> UploadContacts(List<string> phoneNumbers);
	}
}