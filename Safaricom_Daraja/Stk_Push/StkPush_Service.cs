using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkPushService
{
	Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default);

	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		CancellationToken ct = default);
}

public sealed class StkPushService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<StkPushService> logger
) : IStkPushService
{
	private readonly DarajaConfig _cfg = options.Value;
	private const string Paybill = "4161705";

	// ─────────────────────────────────────────────
	// STK PUSH
	// ─────────────────────────────────────────────
	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default)
	{
		try
		{
			if (amount <= 0)
				return DarajaResult<StkPushResponse>.Fail("Amount must be greater than zero.");

			var sanitizedPhone = SanitizePhone(phone);
			var safeRef = Truncate(string.IsNullOrWhiteSpace(accountReference) ? Paybill : accountReference, 12);
			var safeDesc = Truncate(string.IsNullOrWhiteSpace(description) ? "Payment" : description, 13);
			var (timestamp, password) = BuildCredentials();

			var payload = new StkPushRequest
			{
				TransactionType = "CustomerPayBillOnline",
				BusinessShortCode = Paybill,
				PartyB = Paybill,
				Password = password,
				Timestamp = timestamp,
				Amount = amount,
				PartyA = sanitizedPhone,
				PhoneNumber = sanitizedPhone,
				CallBackURL = _cfg.StkCallbackUrl,
				AccountReference = safeRef,
				TransactionDesc = safeDesc
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpush/v1/processrequest", payload, ct);
			var content = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError("STK push failed. StatusCode={Status} Response={Response}",
					(int)response.StatusCode, content);
				return DarajaResult<StkPushResponse>.Fail(content);
			}

			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(ct);

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail("Null response from Daraja.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning("STK push rejected. Code={Code} Desc={Desc}",
					result.ResponseCode, result.ResponseDescription);
				return DarajaResult<StkPushResponse>.Fail(result.ResponseDescription ?? "Rejected by Daraja.");
			}

			logger.LogInformation("STK push initiated. Phone={Phone} Amount={Amount} Ref={Ref} CheckoutId={Id}",sanitizedPhone, amount, safeRef, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK push unhandled exception.");
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	// ─────────────────────────────────────────────
	// STK QUERY
	// ─────────────────────────────────────────────
	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		CancellationToken ct = default)
	{
		try
		{
			var (timestamp, password) = BuildCredentials();

			var payload = new StkQueryRequest
			{
				BusinessShortCode = Paybill,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);
			var content = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError("STK query failed. StatusCode={Status} Response={Response}",
					(int)response.StatusCode, content);
				return DarajaResult<StkQueryResponse>.Fail(content);
			}

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(ct);

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail("Null response from Daraja.");

			logger.LogInformation("STK query success. CheckoutId={Id} ResultCode={Code}",checkoutRequestId, result.ResultCode);

			return DarajaResult<StkQueryResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK query unhandled exception.");
			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ─────────────────────────────────────────────
	// HELPERS
	// ─────────────────────────────────────────────

	/// <summary>
	/// Builds timestamp and password using EAT (UTC+3).
	/// Uses DateTimeOffset directly — no tzdata dependency, works on Railway Linux.
	/// </summary>
	private (string Timestamp, string Password) BuildCredentials()
	{
		var timestamp = DateTimeOffset.UtcNow
			.ToOffset(TimeSpan.FromHours(3))
			.ToString("yyyyMMddHHmmss");

		var raw = $"{Paybill}{_cfg.PassKey}{timestamp}";
		var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));

		return (timestamp, password);
	}

	private static string SanitizePhone(string phone)
	{
		phone = phone.Trim().Replace("+", "").Replace(" ", "");

		if (phone.StartsWith("07") || phone.StartsWith("01"))
			phone = "254" + phone[1..];

		if (!Regex.IsMatch(phone, @"^254\d{9}$"))
			throw new ArgumentException($"Invalid phone number format: {phone}");

		return phone;
	}

	private static string Truncate(string value, int maxLength) =>
		value.Length > maxLength ? value[..maxLength] : value;

	private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);
		var client = httpFactory.CreateClient("FuelFlow");
		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);
		return client;
	}


}



