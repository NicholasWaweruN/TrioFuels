
using BussinessLogic.Messaging;

namespace BusinessLogic.Messaging
{
	internal class HashSet
	{
		private List<BulkSms.PhoneNumbers> phoneNumberList;

		public HashSet(List<BulkSms.PhoneNumbers> phoneNumberList)
		{
			this.phoneNumberList = phoneNumberList;
		}
	}
}