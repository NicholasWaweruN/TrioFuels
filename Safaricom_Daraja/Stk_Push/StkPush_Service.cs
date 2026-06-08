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
	/// <param name="description">Transaction description (max 20 chars).</param>
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
	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		CancellationToken ct = default);
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

			// 1. DYNAMIC: Pass the configured BusinessShortCode (StoreNumber) from appsettings.json
			var (timestamp, password) = BuildStkCredentials(_cfg.BusinessShortCode);

			// 2. RESILIENT: Handle potential null references cleanly before slicing
			var safeRef = string.IsNullOrWhiteSpace(accountReference) ? "Payment" : accountReference;
			var safeDesc = string.IsNullOrWhiteSpace(description) ? "Payment" : description;

			var payload = new StkPushRequest
			{
				// DYNAMIC: Reads "4161705" automatically from config
				BusinessShortCode = _cfg.BusinessShortCode,
				Password = password,
				Timestamp = timestamp,
				TransactionType = "CustomerBuyGoodsOnline",
				Amount = amount,
				PartyA = sanitizedPhone,
				// DYNAMIC: Uses the specific tillNumber passed into the method call
				PartyB = tillNumber,
				PhoneNumber = sanitizedPhone,
				CallBackURL = _cfg.StkCallbackUrl,
				// SAFE SLICING: Truncates values to respect Safaricom validation limits
				AccountReference = safeRef[..Math.Min(safeRef.Length, 12)],
				TransactionDesc = safeDesc[..Math.Min(safeDesc.Length, 20)]
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
				logger.LogWarning("STK Push rejected: {Desc}", result?.ResponseDescription);
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
		CancellationToken ct = default)
	{
		try
		{
			// Query also uses StoreNumber as BusinessShortCode
			var (timestamp, password) = BuildStkCredentials(_cfg.StoreNumber);

			var payload = new StkQueryRequest
			{
				BusinessShortCode = _cfg.StoreNumber,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(cancellationToken: ct);
			return DarajaResult<StkQueryResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Query failed for {CheckoutRequestId}", checkoutRequestId);
			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ─── Helpers ─────────────────────────────────────────────────────────────

	/// <summary>
	/// Builds Base64(ShortCode + PassKey + Timestamp) as required by Daraja.
	/// Always pass StoreNumber (head-office shortcode) here.
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