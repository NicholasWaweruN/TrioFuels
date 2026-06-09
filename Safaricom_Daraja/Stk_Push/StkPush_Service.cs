using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkPushService
{
	/// <summary>
	/// Initiates Lipa na M-Pesa STK Push to a customer's phone.
	/// </summary>
	/// <param name="phone">Customer phone in 2547XXXXXXXX format.</param>
	/// <param name="amount">Amount in KES (whole number).</param>
	/// <param name="tillNumber">Till number to receive the payment (PartyB).</param>
	/// <param name="accountReference">Reference shown on customer's phone (max 12 chars).</param>
	/// <param name="description">Transaction description (max 13 chars).</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default);

	/// <summary>
	/// Queries the status of a previous STK Push request.
	/// </summary>
	/// <param name="checkoutRequestId">The CheckoutRequestID from the original STK Push response.</param>
	/// <param name="tillNumber">The same till number used during initiation.</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(string checkoutRequestId,string tillNumber,CancellationToken ct = default);
}

public sealed class StkPushService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<StkPushService> logger) : IStkPushService
{
	private readonly DarajaConfig _cfg = options.Value;

	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default)
	{
		try
		{
			var sanitizedPhone = SanitizePhone(phone);

			// For CustomerBuyGoodsOnline (Till):
			// - BusinessShortCode = till number (e.g. 5617668)
			// - PartyB            = till number (same value)
			// - Password          = Base64(tillNumber + PassKey + Timestamp)
			// The head-office shortcode (4161705) is NOT used in STK Push for tills.
			var (timestamp, password) = BuildStkCredentials(tillNumber);

			var safeRef = string.IsNullOrWhiteSpace(accountReference) ? "Payment" : accountReference;
			var safeDesc = string.IsNullOrWhiteSpace(description) ? "Payment" : description;

			var payload = new StkPushRequest
			{
				BusinessShortCode = tillNumber,                                 // e.g. 5617668
				Password = password,                                   // Base64(tillNumber + passkey + ts)
				Timestamp = timestamp,
				TransactionType = "CustomerBuyGoodsOnline",
				Amount = amount,
				PartyA = sanitizedPhone,                            // customer phone
				PartyB = tillNumber,                                // same as BusinessShortCode for Buy Goods
				PhoneNumber = sanitizedPhone,
				CallBackURL = _cfg.StkCallbackUrl,
				AccountReference = safeRef[..Math.Min(safeRef.Length, 12)],
				TransactionDesc = safeDesc[..Math.Min(safeDesc.Length, 13)]  // max 13 chars per Safaricom docs
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpush/v1/processrequest", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("STK Push failed [{StatusCode}]: {Error}", response.StatusCode, error);
				return DarajaResult<StkPushResponse>.Fail($"HTTP {response.StatusCode}: {error}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(cancellationToken: ct);

			if (result?.ResponseCode != "0")
			{
				logger.LogWarning("STK Push rejected by Safaricom: {Desc}", result?.ResponseDescription);
				return DarajaResult<StkPushResponse>.Fail(result?.ResponseDescription ?? "Unknown error");
			}

			logger.LogInformation(
				"STK Push sent to {Phone} for KES {Amount} on till {Till}. CheckoutRequestID={Id}",
				sanitizedPhone, amount, tillNumber, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Unhandled exception in STK Push");
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		string tillNumber,
		CancellationToken ct = default)
	{
		try
		{
			// Must use the same till number used during initiation
			var (timestamp, password) = BuildStkCredentials(tillNumber);

			var payload = new StkQueryRequest
			{
				BusinessShortCode = tillNumber,   // same till number used during initiation
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("STK Query failed [{StatusCode}]: {Error}", response.StatusCode, error);
				return DarajaResult<StkQueryResponse>.Fail($"HTTP {response.StatusCode}: {error}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(cancellationToken: ct);
			return DarajaResult<StkQueryResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Query failed for {CheckoutRequestId}", checkoutRequestId);
			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ─── Helpers ──────────────────────────────────────────────────────────────

	/// <summary>
	/// Builds the STK password as Base64(shortCode + PassKey + Timestamp).
	/// For Buy Goods (Till): pass the till number.
	/// For Paybill: pass the BusinessShortCode.
	/// </summary>
	private (string Timestamp, string Password) BuildStkCredentials(string shortCode)
	{
		var eat = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
		var timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eat)
								   .ToString("yyyyMMddHHmmss");

		var raw = $"{shortCode}{_cfg.PassKey}{timestamp}";
		var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
		return (timestamp, password);
	}

	private static string SanitizePhone(string phone)
	{
		phone = phone.Trim().Replace(" ", "").Replace("+", "");
		if (phone.StartsWith("07") || phone.StartsWith("01"))
			phone = "254" + phone[1..];
		return phone;
	}

	private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);
		var client = httpFactory.CreateClient("Daraja");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		return client;
	}
}