using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkPushService
{
	/// <summary>
	/// Initiates an STK Push to a specific Buy Goods till.
	/// </summary>
	/// <param name="phone">Customer phone number (07xx, 01xx, or 254xx format).</param>
	/// <param name="amount">Amount in KES — must be greater than zero.</param>
	/// <param name="tillNumber">The Buy Goods till number to receive the payment (e.g. "5617668").</param>
	/// <param name="accountReference">Short label shown to the customer on the prompt (max 12 chars).</param>
	/// <param name="description">Internal transaction description (max 13 chars).</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default);

	/// <summary>
	/// Queries the status of a previously initiated STK Push.
	/// </summary>
	/// <param name="checkoutRequestId">The CheckoutRequestID returned by InitiateAsync.</param>
	/// <param name="ct">Cancellation token.</param>
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

	// ── STK Push ──────────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(phone);
		ArgumentException.ThrowIfNullOrWhiteSpace(tillNumber);

		if (amount <= 0)
			return DarajaResult<StkPushResponse>.Fail("Amount must be greater than zero.");

		// Validate till belongs to this org
		var till = _cfg.Tills.FirstOrDefault(t => t.TillNumber == tillNumber);
		if (till is null)
		{
			logger.LogError("STK Push rejected — till {TillNumber} not found in config.", tillNumber);
			return DarajaResult<StkPushResponse>.Fail($"Till {tillNumber} is not configured.");
		}

		try
		{
			var sanitizedPhone = SanitizePhone(phone);
			var safeRef = Truncate(string.IsNullOrWhiteSpace(accountReference) ? tillNumber : accountReference, 12);
			var safeDesc = Truncate(string.IsNullOrWhiteSpace(description) ? "Payment" : description, 13);
			var (timestamp, password) = BuildCredentials();

			// ── Buy Goods payload ─────────────────────────────────────────────
			// BusinessShortCode + PartyB = till number (not the org shortcode)
			// Password                   = built from org shortcode (4161705) + PassKey
			// TransactionType            = CustomerBuyGoodsOnline (not CustomerPayBillOnline)
			var payload = new StkPushRequest
			{
				TransactionType = "CustomerBuyGoodsOnline",
				BusinessShortCode = tillNumber,              // e.g. 5617668
				PartyB = tillNumber,              // same till number
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
				logger.LogError(
					"STK Push failed — Till={Till} Status={Status} Response={Response}",
					tillNumber, (int)response.StatusCode, content);
				return DarajaResult<StkPushResponse>.Fail(content);
			}

			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(ct);

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail("Null response from Daraja.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning(
					"STK Push rejected by Daraja — Till={Till} Code={Code} Desc={Desc}",
					tillNumber, result.ResponseCode, result.ResponseDescription);
				return DarajaResult<StkPushResponse>.Fail(result.ResponseDescription ?? "Rejected by Daraja.");
			}

			logger.LogInformation(
				"STK Push initiated — Phone={Phone} Amount=KES {Amount} Till={Till} ({TillName}) " +
				"Ref={Ref} CheckoutId={Id}",
				sanitizedPhone, amount, tillNumber, till.Name, safeRef, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Push unhandled exception — Till={Till}", tillNumber);
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	// ── STK Query ─────────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		CancellationToken ct = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(checkoutRequestId);

		try
		{
			var (timestamp, password) = BuildCredentials();

			// Query also uses the org shortcode (4161705), not the till number
			var payload = new StkQueryRequest
			{
				BusinessShortCode = _cfg.BusinessShortCode,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);
			var content = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"STK Query failed — CheckoutId={Id} Status={Status} Response={Response}",
					checkoutRequestId, (int)response.StatusCode, content);
				return DarajaResult<StkQueryResponse>.Fail(content);
			}

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(ct);

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail("Null response from Daraja.");

			logger.LogInformation(
				"STK Query success — CheckoutId={Id} ResultCode={Code} Desc={Desc}",
				checkoutRequestId, result.ResultCode, result.ResultDesc);

			return DarajaResult<StkQueryResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Query unhandled exception — CheckoutId={Id}", checkoutRequestId);
			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	/// <summary>
	/// Builds timestamp (EAT UTC+3) and password.
	/// Password always uses the org BusinessShortCode (4161705) + PassKey —
	/// even for Buy Goods transactions where the payload uses the till number.
	/// </summary>
	private (string Timestamp, string Password) BuildCredentials()
	{
		var timestamp = DateTimeOffset.UtcNow
			.ToOffset(TimeSpan.FromHours(3))
			.ToString("yyyyMMddHHmmss");

		// Always org shortcode here — PassKey is issued against 4161705, not the tills
		var raw = $"{_cfg.BusinessShortCode}{_cfg.PassKey}{timestamp}";
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
		var client = httpFactory.CreateClient("Daraja");
		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);
		return client;
	}
}