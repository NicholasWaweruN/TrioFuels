// ── appsettings.json ─────────────────────────────────────────────────────────
/*
{
  "Onfon": {
    "ApiKey":    "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "ClientId":  "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AccessKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "SenderId":  "FUELFLOW",
    "BaseUrl":   "https://api.onfonmedia.co.ke/v1/sms/SendBulkSMS"
  }
}
*/

// ── Program.cs registration ───────────────────────────────────────────────────
/*
builder.Services.AddOnfonSms(builder.Configuration);
*/

// ── Controller / Service usage ────────────────────────────────────────────────

using OnfonSms;

namespace OnFonMessaging
{
	public class NotificationService
	{
		private readonly ISmsService _sms;

		public NotificationService(ISmsService sms) => _sms = sms;

		// 1. Single recipient
		public async Task NotifyCustomerAsync(string phone, string message)
		{
			var result = await _sms.SendAsync(phone, message);

			if (result.ErrorCode != 0)
				throw new Exception($"SMS failed: {result.ErrorDescription}");
		}

		// 2. Bulk — same message to many
		public async Task NotifyAllStationsAsync(IEnumerable<string> phones, string alert)
		{
			var result = await _sms.SendBulkAsync(phones, alert);
			// result.Data contains a MessageId per recipient
		}

		// 3. Personalised — different text per recipient
		public async Task SendReceiptsAsync(List<(string Phone, string Receipt)> items)
		{
			var messages = items.Select(x => new SmsMessage
			{
				Number = x.Phone,
				Text = x.Receipt
			});

			await _sms.SendPersonalisedAsync(messages);
		}

		// 4. Scheduled
		public async Task SendReminderAsync(string phone, string message, DateTime sendAt)
		{
			await _sms.SendScheduledAsync(phone, message, sendAt);
		}
	}
}