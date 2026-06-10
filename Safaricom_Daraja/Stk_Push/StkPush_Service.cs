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
	/// Initiates a Lipa na M-Pesa STK Push (Buy Goods) to a customer's phone.
	/// </summary>
	/// <param name="phone">Customer phone in 2547XXXXXXXX, 07XXXXXXXX, or 01XXXXXXXX format.</param>
	/// <param name="amount">Amount in KES (must be greater than zero).</param>
	/// <param name="tillNumber">
	///     7-digit Till Number for this station (e.g. "5617660").
	///     Used as BusinessShortCode, PartyB, and in password generation.
	/// </param>
	/// <param name="accountReference">Reference shown on the customer's phone (max 12 chars).</param>
	/// <param name="description">Transaction description (max 13 chars). Defaults to "Payment".</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkPushResponse>> InitiateAsync(string phone,long amount,string tillNumber,string accountReference,string description = "Payment",CancellationToken ct = default);

	/// <summary>
	/// Queries the status of a previous STK Push request.
	/// </summary>
	/// <param name="checkoutRequestId">The CheckoutRequestID from the original STK Push response.</param>
	/// <param name="tillNumber">The same Till Number used during initiation.</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(string checkoutRequestId,string tillNumber,CancellationToken ct = default);
}

public sealed class StkPushService(IHttpClientFactory httpFactory,IDarajaTokenService tokenService,IOptions<DarajaConfig> options,ILogger<StkPushService> logger) : IStkPushService
{
	private readonly DarajaConfig _cfg = options.Value;

	// ─── STK Push ─────────────────────────────────────────────────────────────

	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(string phone,long amount,string tillNumber,string accountReference,string description = "Payment",CancellationToken ct = default)
	{
		try
		{
			if (amount <= 0)
				return DarajaResult<StkPushResponse>.Fail("Amount must be greater than zero.");

			if (string.IsNullOrWhiteSpace(tillNumber))
				return DarajaResult<StkPushResponse>.Fail("Till number is required.");

			var sanitizedPhone = SanitizePhone(phone);
			var cleanTill = tillNumber.Trim();

			// LNM Buy Goods password uses the Till Number (not Head Office / Store Number).
			var (timestamp, password) = BuildCredentials(cleanTill);

			var safeRef = string.IsNullOrWhiteSpace(accountReference) ? cleanTill : accountReference;

			var safeDesc = string.IsNullOrWhiteSpace(description)? "Payment" : description;

			var payload = new StkPushRequest
			{
				// For LNM Buy Goods tills:
				//   TransactionType   = CustomerBuyGoodsOnline
				//   BusinessShortCode = Till Number
				//   PartyB            = Till Number
				//   Password          = Base64(TillNumber + PassKey + Timestamp)

				TransactionType = "CustomerBuyGoodsOnline",
				BusinessShortCode = cleanTill,
				Password = password,
				Timestamp = timestamp,
				Amount = amount,
				PartyA = sanitizedPhone,
				PartyB = cleanTill,
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
				logger.LogError("STK Push failed. Till={Till} Status={Status} Response={Response}",cleanTill, response.StatusCode, responseContent);

				return DarajaResult<StkPushResponse>.Fail($"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail("Unable to parse Safaricom response.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning("STK Push rejected. Till={Till} Code={Code} Description={Description}",cleanTill, result.ResponseCode, result.ResponseDescription);

				return DarajaResult<StkPushResponse>.Fail(result.ResponseDescription ?? "STK Push request rejected.");
			}

			logger.LogInformation("STK Push initiated. Phone={Phone} Amount=KES{Amount} Till={Till} " + "AccountRef={Ref} CheckoutRequestID={Id}",sanitizedPhone, amount, cleanTill, safeRef, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (ArgumentException ex)
		{
			logger.LogWarning(ex, "STK Push validation error");
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Push unexpected error. Till={Till}", tillNumber);
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	// ─── STK Query ────────────────────────────────────────────────────────────

	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		string tillNumber,
		CancellationToken ct = default)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(checkoutRequestId))
				return DarajaResult<StkQueryResponse>.Fail("CheckoutRequestID is required.");

			if (string.IsNullOrWhiteSpace(tillNumber))
				return DarajaResult<StkQueryResponse>.Fail("Till number is required.");

			var cleanTill = tillNumber.Trim();
			var (timestamp, password) = BuildCredentials(cleanTill);

			var payload = new StkQueryRequest
			{
				BusinessShortCode = cleanTill,
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
					"STK Query failed. Till={Till} Status={Status} Response={Response}",
					cleanTill, response.StatusCode, responseContent);

				return DarajaResult<StkQueryResponse>.Fail(
					$"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(
				cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail("Unable to parse query response.");

			logger.LogInformation(
				"STK Query complete. CheckoutRequestID={Id} ResultCode={Code} Description={Desc}",
				checkoutRequestId, result.ResultCode, result.ResultDesc);

			return DarajaResult<StkQueryResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex,
				"STK Query unexpected error. CheckoutRequestID={Id} Till={Till}",
				checkoutRequestId, tillNumber);

			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ─── Private Helpers ──────────────────────────────────────────────────────

	/// <summary>
	/// Builds the STK password: Base64(shortCode + PassKey + Timestamp).
	/// For Buy Goods, always pass the Till Number as shortCode.
	/// </summary>
	private (string Timestamp, string Password) BuildCredentials(string shortCode)
	{
		DateTime kenyanTime;
		try
		{
			var eatZone = OperatingSystem.IsWindows()
				? TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")
				: TimeZoneInfo.FindSystemTimeZoneById("Africa/Nairobi");

			kenyanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eatZone);
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

	/// <summary>
	/// Normalises a Kenyan phone number to the 2547XXXXXXXXX format required by Daraja.
	/// Accepts: 07XXXXXXXX, 01XXXXXXXX, +2547XXXXXXXX, 2547XXXXXXXX.
	/// </summary>
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
		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);
		return client;
	}
}