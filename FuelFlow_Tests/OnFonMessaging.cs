using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace FuelFlow_Tests
{
	public class OnfonSmsTests
	{
		[Fact]
		public async Task SendSms_RealApi_ShouldSucceed()
		{
			using var http = new HttpClient();
			http.DefaultRequestHeaders.Add("AccessKey", "YOUR_API_KEY");

			var payload = new
			{
				SenderId = "TRIO_FUELS",
				IsUnicode = false,
				IsFlash = false,
				MessageParameters = new[]
				{
					new { Number = "254715821303", Text = "FuelFlow test SMS" }
				},
				ApiKey = "zmiEutYZe37M4DCdwTNXkfgKs2crLRAoJQ069pFja15lByq8",
				ClientId = "TrioFuels"
			};

			var response = await http.PostAsJsonAsync(
				"https://api.onfonmedia.co.ke/v1/sms/SendBulkSMS", payload);

			var body = await response.Content.ReadAsStringAsync();
			Assert.True(response.IsSuccessStatusCode, $"HTTP failed: {body}");

			using var doc = JsonDocument.Parse(body);
			var errorCode = doc.RootElement.GetProperty("ErrorCode").GetInt32();
			Assert.Equal(0, errorCode);
		}
	}
}