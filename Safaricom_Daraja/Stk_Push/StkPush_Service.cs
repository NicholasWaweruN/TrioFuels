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
	Task<DarajaResult<StkPushResponse>> InitiateAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference,
		string description = "Payment",
		CancellationToken ct = default);

	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		string tillNumber,
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
			var cleanTill = tillNumber.Trim();

			// Pass the target sub-till down to the security string assembler
			var (timestamp, password) = BuildCredentials(cleanTill);

			var payload = new StkPushRequest
			{
				// 1. Set scheme explicitly to Buy Goods matching the target till
				TransactionType = "CustomerBuyGoodsOnline",

				// 2. Safaricom requires the sub-till here for processing schema validation
				BusinessShortCode = cleanTill,
				Password = password,
				Timestamp = timestamp,
				Amount = amount,
				PartyA = sanitizedPhone,

				// 3. Set the dynamic sub-till receiving the money
				PartyB = cleanTill,

				PhoneNumber = sanitizedPhone,
				CallBackURL = _cfg.StkCallbackUrl,

				AccountReference = accountReference.Length > 12
					? accountReference[..12]
					: accountReference,

				TransactionDesc = description.Length > 13
					? description[..13]
					: description
			};

			var client = await GetAuthenticatedClientAsync(ct);

			var response = await client.PostAsJsonAsync(
				"/mpesa/stkpush/v1/processrequest",
				payload,
				ct);

			var responseContent = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"STK Push failed. Status={Status} Response={Response}",
					response.StatusCode,
					responseContent);

				return DarajaResult<StkPushResponse>.Fail(
					$"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result =
				await response.Content.ReadFromJsonAsync<StkPushResponse>(
					cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail(
					"Unable to parse Safaricom response.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning(
					"STK Push rejected. Code={Code} Description={Description}",
					result.ResponseCode,
					result.ResponseDescription);

				return DarajaResult<StkPushResponse>.Fail(
					result.ResponseDescription ??
					"STK Push request rejected.");
			}

			logger.LogInformation(
				"STK Push sent. CheckoutRequestID={CheckoutRequestID}",
				result.CheckoutRequestId);

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
		string tillNumber,
		CancellationToken ct = default)
	{
		try
		{
			var cleanTill = tillNumber.Trim();
			var (timestamp, password) = BuildCredentials(cleanTill);

			var payload = new StkQueryRequest
			{
				// Query endpoints also require the target sub-till to execute tracking
				BusinessShortCode = cleanTill,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);

			var response = await client.PostAsJsonAsync(
				"/mpesa/stkpushquery/v1/query",
				payload,
				ct);

			var responseContent = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"STK Query failed. Status={Status} Response={Response}",
					response.StatusCode,
					responseContent);

				return DarajaResult<StkQueryResponse>.Fail(
					$"HTTP {(int)response.StatusCode}: {responseContent}");
			}

			var result =
				await response.Content.ReadFromJsonAsync<StkQueryResponse>(
					cancellationToken: ct);

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail(
					"Unable to parse query response.");

			return DarajaResult<StkQueryResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(
				ex,
				"STK Query failed for {CheckoutRequestId}",
				checkoutRequestId);

			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	private (string Timestamp, string Password) BuildCredentials(string shortCodeForPassword)
	{
		var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

		// Note: Use the target shortcode (the till) to create the payload's password signature 
		var passwordString =
			$"{shortCodeForPassword}" +
			$"{_cfg.PassKey}" +
			$"{timestamp}";

		var password = Convert.ToBase64String(
			Encoding.UTF8.GetBytes(passwordString));

		return (timestamp, password);
	}

	private static string SanitizePhone(string phone)
	{
		phone = phone.Trim()
					 .Replace(" ", "")
					 .Replace("+", "");

		if (phone.StartsWith("07"))
			phone = "254" + phone[1..];

		if (phone.StartsWith("01"))
			phone = "254" + phone[1..];

		if (!Regex.IsMatch(phone, @"^254\d{9}$"))
			throw new ArgumentException("Invalid phone number.");

		return phone;
	}

	private async Task<HttpClient> GetAuthenticatedClientAsync(
		CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);

		var client = httpFactory.CreateClient("Daraja");

		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);

		return client;
	}
}
