using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Safaricom_Daraja.DarajaTokenService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using DataAccessLayer.EntityModels.Daraja;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;

namespace Safaricom_Daraja.Stk_Push;

public sealed class StkPushService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<StkPushService> logger,
	OTOContext context) : IStkPushService
{
	private readonly DarajaConfig _cfg = options.Value;
	private readonly OTOContext _context = context;

	// ─────────────────────────────────────────────────────────────
	// STK PUSH
	// ─────────────────────────────────────────────────────────────

	public async Task<DarajaResult<StkPushResponse>> InitiateAsync(string phone,long amount,string tillNumber,string accountReference,string description = "Payment",CancellationToken ct = default)
	{
		// ── VALIDATION ──────────────────────────────────────────
		ArgumentException.ThrowIfNullOrWhiteSpace(phone);
		ArgumentException.ThrowIfNullOrWhiteSpace(tillNumber);

		if (amount <= 0)
			return DarajaResult<StkPushResponse>.Fail("Amount must be greater than zero.");

		string sanitizedPhone;

		try
		{
			sanitizedPhone = SanitizePhone(phone);
		}
		catch (ArgumentException ex)
		{
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}

		var till = await _context.Tills
			.Where(x => x.TillNumber == tillNumber)
			.FirstOrDefaultAsync(ct);

		if (till is null)
			return DarajaResult<StkPushResponse>.Fail($"Till {tillNumber} is not configured.");

		// ── INITIATE ─────────────────────────────────────────────
		try
		{
			var safeRef = Truncate(string.IsNullOrWhiteSpace(accountReference) ? tillNumber : accountReference, 12);
			var safeDesc = Truncate(string.IsNullOrWhiteSpace(description) ? "Payment" : description, 13);
			var (timestamp, password) = BuildCredentials();

			var payload = new StkPushRequest
			{
				TransactionType = "CustomerBuyGoodsOnline",
				BusinessShortCode = _cfg.BusinessShortCode,
				PartyB = till.TillNumber,
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

			// ✅ READ ONCE — avoids consuming the stream twice
			var result = await response.Content.ReadFromJsonAsync<StkPushResponse>(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError("STK Push failed — Till={Till} Status={Status}",
					tillNumber, (int)response.StatusCode);

				return DarajaResult<StkPushResponse>.Fail($"Daraja HTTP {(int)response.StatusCode}");
			}

			if (result is null)
				return DarajaResult<StkPushResponse>.Fail("Null response from Daraja.");

			if (result.ResponseCode != "0")
			{
				logger.LogWarning("STK Push rejected — Till={Till} Code={Code} Desc={Desc}",
					tillNumber, result.ResponseCode, result.ResponseDescription);

				return DarajaResult<StkPushResponse>.Fail(result.ResponseDescription ?? "Rejected by Daraja.");
			}

			// ── PERSIST PENDING TRANSACTION ───────────────────────
			var transaction = new StkTransaction
			{
				CheckoutRequestId = result.CheckoutRequestId,
				MerchantRequestId = result.MerchantRequestId,
				PhoneNumber = sanitizedPhone,
				Amount = amount,
				TillNumber = tillNumber,
				AccountReference = safeRef,
				Status = "Pending",
				DateCreated = DateTime.UtcNow
			};

			_context.StkTransactions.Add(transaction);
			await _context.SaveChangesAsync(ct);

			logger.LogInformation("STK Push initiated ✅ — Phone={Phone} Amount=KES {Amount} Till={TillName} ({Till}) Ref={Ref} CheckoutId={Id}",sanitizedPhone, amount, till.TillName, tillNumber, safeRef, result.CheckoutRequestId);

			return DarajaResult<StkPushResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Push unhandled exception — Till={Till}", tillNumber);
			return DarajaResult<StkPushResponse>.Fail(ex.Message);
		}
	}

	// ─────────────────────────────────────────────────────────────
	// STK QUERY
	// ─────────────────────────────────────────────────────────────

	/// <summary>
	/// 
	/// </summary>
	/// <param name="checkoutRequestId"></param>
	/// <param name="ct"></param>
	/// <returns></returns>
	/// 

	public async Task<MpesaTransaction?> GetMpesaTransaction(string checkoutRequestId, CancellationToken ct = default)
	{
		return await _context.MpesaTransactions
			.FirstOrDefaultAsync(x => x.CheckoutRequestID == checkoutRequestId, ct);
	}

	public async Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(
		string checkoutRequestId,
		CancellationToken ct = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(checkoutRequestId);

		try
		{
			var (timestamp, password) = BuildCredentials();

			var payload = new StkQueryRequest
			{
				BusinessShortCode = _cfg.BusinessShortCode,
				Password = password,
				Timestamp = timestamp,
				CheckoutRequestID = checkoutRequestId
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/stkpushquery/v1/query", payload, ct);

			// ✅ READ ONCE — avoids consuming the stream twice
			var result = await response.Content.ReadFromJsonAsync<StkQueryResponse>(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError("STK Query failed — CheckoutId={Id} Status={Status}",
					checkoutRequestId, (int)response.StatusCode);

				return DarajaResult<StkQueryResponse>.Fail($"Daraja HTTP {(int)response.StatusCode}");
			}

			if (result is null)
				return DarajaResult<StkQueryResponse>.Fail("Null response from Daraja.");

			// ── UPDATE TRANSACTION STATUS ─────────────────────────
			var transaction = await _context.StkTransactions
				.FirstOrDefaultAsync(x => x.CheckoutRequestId == checkoutRequestId, ct);

			if (transaction != null)
			{
				transaction.ResultCode = result.ResultCode;
				transaction.ResultDescription = result.ResultDesc;

				// ✅ FIXED result code handling (was: != "1" treated pending as failed)
				if (result.ResultCode == "0")
				{
					// SUCCESS
					transaction.Status = "Completed";
					transaction.DateCompleted = DateTime.UtcNow;
				}
				else if (result.ResultCode == "1")
				{
					// INSUFFICIENT BALANCE — still pending, don't close
					transaction.Status = "Pending";
				}
				else
				{
					// TERMINAL FAILURES: 1032 (cancelled), 1037 (timeout), 2001 (wrong PIN)
					transaction.Status = "Failed";
					transaction.DateCompleted = DateTime.UtcNow;
				}

				await _context.SaveChangesAsync(ct);
			}

			logger.LogInformation("STK Query ✅ — CheckoutId={Id} ResultCode={Code} Desc={Desc}",checkoutRequestId, result.ResultCode, result.ResultDesc);

			return DarajaResult<StkQueryResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "STK Query unhandled exception — CheckoutId={Id}", checkoutRequestId);
			return DarajaResult<StkQueryResponse>.Fail(ex.Message);
		}
	}

	// ─────────────────────────────────────────────────────────────
	// HELPERS
	// ─────────────────────────────────────────────────────────────

	private (string Timestamp, string Password) BuildCredentials()
	{
		var timestamp = DateTimeOffset.UtcNow
			.ToOffset(TimeSpan.FromHours(3))
			.ToString("yyyyMMddHHmmss");

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

// ─────────────────────────────────────────────────────────────
// INTERFACE
// ─────────────────────────────────────────────────────────────

public interface IStkPushService
{
	/// <summary>
	/// Initiates an STK Push to a specific Buy Goods till.
	/// </summary>
	/// <param name="phone">Customer phone number (07xx, 01xx, or 254xx format).</param>
	/// <param name="amount">Amount in KES — must be greater than zero.</param>
	/// <param name="tillNumber">The Buy Goods till number to receive the payment.</param>
	/// <param name="accountReference">Short label shown on customer prompt (max 12 chars).</param>
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
	/// <param name="checkoutRequestId">CheckoutRequestID returned from InitiateAsync.</param>
	/// <param name="ct">Cancellation token.</param>
	Task<DarajaResult<StkQueryResponse>> QueryStatusAsync(string checkoutRequestId,CancellationToken ct = default);
	Task<MpesaTransaction?> GetMpesaTransaction(string checkoutRequestId, CancellationToken ct = default);
}