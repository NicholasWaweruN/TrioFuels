using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkPushService
{
	/// <summary>
	/// Initiates Lipa na M-Pesa STK Push to a customer's phone.
	/// </summary>
	/// <param name="phone">Customer phone in 2547XXXXXXXX format.</param>
	/// <param name="amount">Amount in KES (whole number).</param>
	/// <param name="storeNumber">7-digit Store Number for this till (e.g. 5545198).</param>
	/// <param name="tillNumber">7-digit Till Number (e.g. 5617668) — stored in AccountReference for tracking.</param>
	/// <param name="accountReference">Reference shown on customer's phone (max 12 chars).</param>
	/// <param name="description">Transaction description (max 13 chars).</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string storeNumber,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default);

	/// <summary>
	/// Queries the status of a previous STK Push request.
	/// </summary>
	/// <param name="checkoutRequestId">The CheckoutRequestID from the original STK Push response.</param>
	/// <param name="storeNumber">The same Store Number used during initiation.</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		string storeNumber,
		CancellationToken ct = default);
}

public sealed class StkPushService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<StkPushService> logger)
	: IStkPushService
{
	private readonly DarajaConfig _cfg = options.Value;

	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string storeNumber,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default)
	{
		try
		{
			if (amount <= 0)
				return DarajaResult<StkPushResponse>.Fail("Amount must be greater than zero.");

			var sanitizedPhone = SanitizePhone(phone);
			var cleanStore = storeNumber.Trim();
			var cleanTill = tillNumber.Trim();

			// For LNM Buy Goods (each till has its own Store Number):
			// - BusinessShortCode = Store Number (e.g. 5545198)
			// - PartyB            = Store Number (same value)
			// - Password          = Base64(StoreNumber + PassKey + Timestamp)
			// - TransactionType   = CustomerPayBillOnline
			var (timestamp, password) = BuildCredentials(cleanStore);

			var safeRef = string.IsNullOrWhiteSpace(accountReference) ? cleanTill : accountReference;
			var safeDesc = string.IsNullOrWhiteSpace(description) ? "Payment" : description;

			var payload = new StkPushRequest
			{
				TransactionType = "CustomerPayBillOnline",
				BusinessShortCode = cleanStore,
				Password = password,
				Timestamp = timestamp,
				Amount = amount,
				PartyA = sanitizedPhone,
				PartyB = cleanStore,
				PhoneNumber = sanitizedPhone,
				CallBackURL = _cfg.StkCallbackUrl,
				AccountReference = safeRef[..Math.Min(safeRef.Length, 12)],
				TransactionDesc = safeDesc[..Math.Min(safeDesc.Length, 13)]
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpush/v1/processrequest", payload, ct);
			var responseContent = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"STK Push failed. Status={Status} Response={Response}",
					response.StatusCode, responseContent);

				return DarajaResult<StkPushResponse>.Fail($"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail("Unable to parse Safaricom response.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning(
					"STK Push rejected. Code={Code} Description={Description}",
					result.ResponseCode, result.ResponseDescription);

				return DarajaResult<StkPushResponse>.Fail(result.ResponseDescription ?? "STK Push request rejected.");
			}

			logger.LogInformation(
				"STK Push sent to {Phone} for KES {Amount} on store {Store} (till {Till}). CheckoutRequestID={Id}",
				sanitizedPhone, amount, cleanStore, cleanTill, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Push failed");
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		string storeNumber,
		CancellationToken ct = default)
	{
		try
		{
			var cleanStore = storeNumber.Trim();
			var (timestamp, password) = BuildCredentials(cleanStore);

			var payload = new StkQueryRequest
			{
				BusinessShortCode = cleanStore,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);
			var responseContent = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"STK Query failed. Status={Status} Response={Response}",
					response.StatusCode, responseContent);

				return DarajaResult<StkQueryResponse>.Fail($"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail("Unable to parse query response.");

			return DarajaResult<StkQueryResponse>.Ok(result);
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
	/// Always pass the Store Number here.
	/// </summary>
	private (string Timestamp, string Password) BuildCredentials(string shortCode)
	{
		DateTime kenyanTime;
		try
		{
			var eatTimeZone = OperatingSystem.IsWindows()
				? TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")
				: TimeZoneInfo.FindSystemTimeZoneById("Africa/Nairobi");

			kenyanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eatTimeZone);
		}
		catch
		{
			// Fallback: UTC+3
			kenyanTime = DateTime.UtcNow.AddHours(3);
		}

		var timestamp = kenyanTime.ToString("yyyyMMddHHmmss");
		var raw = $"{shortCode}{_cfg.PassKey}{timestamp}";
		var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));

		return (timestamp, password);
	}

	private static string SanitizePhone(string phone)
	{
		phone = phone.Trim().Replace(" ", "").Replace("+", "");

		if (phone.StartsWith("07") || phone.StartsWith("01"))
			phone = "254" + phone[1..];

		if (!Regex.IsMatch(phone, @"^254\d{9}$"))
			throw new ArgumentException($"Invalid phone number format: {phone}");

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