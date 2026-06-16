using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;

namespace Safaricom_Daraja.C2bService;

public interface IC2BService
{
	Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default);
	Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default);
	C2BValidationResponse Validate(C2BValidationRequest request);
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}

public sealed class C2BService(IHttpClientFactory httpFactory,IDarajaTokenService tokenService,IOptions<DarajaConfig> options,ILogger<C2BService> logger,OTOContext context) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;

	// FIX: Use IANA timezone ID so this works on both Windows and Linux (Railway).
	//      "E. Africa Standard Time" is Windows-only and throws on Ubuntu.
	private static readonly TimeZoneInfo EatTimeZone = TimeZoneInfo.FindSystemTimeZoneById(OperatingSystem.IsWindows() ? "E. Africa Standard Time" : "Africa/Nairobi");

	// ── Registration ──────────────────────────────────────────────────────────

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterMaster] Starting master shortcode registration. C2BShortCode={C2BSC} BusinessShortCode={BSC}",_cfg.C2BShortCode, _cfg.BusinessShortCode);

		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterUrls] Called. ShortCode={SC}", shortCode);

		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		// FIX: SanitizeUrl now guards against invalid/relative URIs instead of
		//      throwing an unhandled UriFormatException at runtime.
		if (!TrySanitizeUrl(_cfg.C2BValidationUrl, out var validationUrl))
			return DarajaResult<C2BRegisterResponse>.Fail($"Invalid C2BValidationUrl: '{_cfg.C2BValidationUrl}'");

		if (!TrySanitizeUrl(_cfg.C2BConfirmationUrl, out var confirmationUrl))
			return DarajaResult<C2BRegisterResponse>.Fail($"Invalid C2BConfirmationUrl: '{_cfg.C2BConfirmationUrl}'");

		logger.LogDebug("[C2B][RegisterUrls] ValidationUrl={V} | ConfirmationUrl={C}", validationUrl, confirmationUrl);

		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed",
			ValidationURL = validationUrl,
			ConfirmationURL = confirmationUrl
		};

		try
		{
			string token;
			try
			{
				token = await tokenService.GetAccessTokenAsync(ct);
				logger.LogInformation("[C2B][RegisterUrls] Token acquired. Length={L}", token.Length);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] Token acquisition FAILED.");
				return DarajaResult<C2BRegisterResponse>.Fail($"Token error: {ex.Message}");
			}

			var client = httpFactory.CreateClient("Daraja");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			const string endpoint = "/mpesa/c2b/v2/registerurl";
			logger.LogInformation("[C2B][RegisterUrls] Dispatching to endpoint: {Endpoint}", endpoint);

			HttpResponseMessage response;
			try
			{
				response = await client.PostAsJsonAsync(endpoint, payload, ct);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] Network error.");
				return DarajaResult<C2BRegisterResponse>.Fail($"HTTP error: {ex.Message}");
			}
			catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] Request timed out.");
				return DarajaResult<C2BRegisterResponse>.Fail("Request timed out");
			}

			var content = await response.Content.ReadAsStringAsync(ct);
			logger.LogInformation("[C2B][RegisterUrls] Response Status={SC} Success={Ok}",
				(int)response.StatusCode, response.IsSuccessStatusCode);

			if (!response.IsSuccessStatusCode)
			{
				// M-Pesa error 500.003.1001 = URLs already registered for this shortcode — treat as success.
				if (content.Contains("500.003.1001"))
				{
					logger.LogInformation("[C2B][RegisterUrls] URLs already registered (idempotent). ShortCode={SC}", shortCode);
					return DarajaResult<C2BRegisterResponse>.Ok(new C2BRegisterResponse
					{
						ResponseCode = "0",
						ResponseDescription = "URLs already registered (idempotent)"
					});
				}

				logger.LogError("[C2B][RegisterUrls] Registration FAILED. Status={SC} Body={Body}",
					(int)response.StatusCode, content);
				return DarajaResult<C2BRegisterResponse>.Fail(content);
			}

			var result = JsonSerializer.Deserialize<C2BRegisterResponse>(content);
			logger.LogInformation("[C2B][RegisterUrls] Registered. Code={RC} Desc={Desc}",
				result?.ResponseCode, result?.ResponseDescription);

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (JsonException ex)
		{
			logger.LogError(ex, "[C2B][RegisterUrls] JSON parse failure.");
			return DarajaResult<C2BRegisterResponse>.Fail($"JSON parse error: {ex.Message}");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "[C2B][RegisterUrls] Unhandled exception.");
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		logger.LogInformation("[C2B][Validate] TransID={ID} Amount={Amount} BusinessShortCode={BSC} BillRefNumber={Ref}",request.TransactionId, request.TransAmount, request.BusinessShortCode, request.BillRefNumber);

		// For Buy Goods (Till), the authoritative "which till received payment" field
		// is BusinessShortCode, not BillRefNumber.
		var tillMatch = _cfg.Tills.FirstOrDefault(t =>
			string.Equals(t.TillNumber, request.BusinessShortCode, StringComparison.OrdinalIgnoreCase));

		if (tillMatch is not null)
		{
			logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID} matched Till={Till} ({Name})",
				request.TransactionId, tillMatch.TillNumber, tillMatch.Name);

			return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
		}

		// Paybill-style fallback: honour BillRefNumber as account reference if present.
		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var knownRefs = _cfg.Tills
				.Select(t => t.AccountReference)
				.Where(r => !string.IsNullOrWhiteSpace(r))
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			if (knownRefs.Contains(request.BillRefNumber.Trim()))
			{
				logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID} matched BillRefNumber='{Ref}'",
					request.TransactionId, request.BillRefNumber);

				return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
			}
		}

		logger.LogWarning(
			"[C2B][Validate] REJECTED — TransID={ID} BSC='{BSC}' did not match any till; BillRefNumber='{Ref}' did not match any account reference.",
			request.TransactionId, request.BusinessShortCode, request.BillRefNumber);

		return Rejected("C2B00011", "Rejected — unrecognized till or account reference");
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	public async Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		logger.LogInformation(
			"[C2B][Confirm] TransID={ID} Amount={Amount} BusinessShortCode={BSC} BillRefNumber={Ref}",
			request.TransactionId, request.TransAmount, request.BusinessShortCode, request.BillRefNumber);

		// Idempotency guard — also requires a UNIQUE INDEX on MpesaTransactions.TransID
		// in your EF migration to catch concurrent duplicates at the DB level.
		var exists = await context.MpesaTransactions
			.AnyAsync(x => x.TransID == request.TransactionId, ct);

		if (exists)
		{
			logger.LogWarning("[C2B][Confirm] Duplicate ignored — TransID={ID}", request.TransactionId);
			return;
		}

		// FIX: Parse TransAmount once and reuse the value. The original code
		//      re-parsed TransAmount into a second `usage` variable for UsageBalance,
		//      which was a copy-paste bug — UsageBalance should mirror TransAmount.
		var transAmount = decimal.TryParse(request.TransAmount, out var amt) ? amt : 0m;
		var orgBalance = decimal.TryParse(request.OrgAccountBalance, out var bal) ? bal : 0m;

		// FIX: Warn on zero/negative amounts — likely a misconfigured or test payload.
		if (transAmount <= 0)
		{
			logger.LogWarning("[C2B][Confirm] Suspicious zero/negative amount — TransID={ID} Amount={A}",
				request.TransactionId, request.TransAmount);
		}

		var till = ResolveTill(request);

		// FIX: All timestamps are now in EAT, including the ParseTransTime fallback.
		//      Previously ParseTransTime returned DateTime.UtcNow on failure while
		//      DateTimeStamp/DateCreated/DateModified used EAT — inconsistent record.
		var eatNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, EatTimeZone);

		var transaction = new MpesaTransaction
		{
			TransactionType = request.TransactionType ?? "C2B",
			TransID = request.TransactionId,
			MpesaReceiptNumber = request.TransactionId,
			TransAmount = transAmount,
			TransTime = ParseTransTime(request.TransTime, eatNow),
			BusinessShortCode = request.BusinessShortCode ?? string.Empty,
			TillNumber = till?.TillNumber ?? "UNMATCHED",
			TillName = till?.Name ?? "UNMATCHED",
			PaymentMethod = "C2B",
			MSISDN = request.PhoneNumber ?? string.Empty,
			FirstName = request.FirstName ?? string.Empty,
			MiddName = request.MiddleName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,
			OrgAccountBalance = orgBalance,
			UsageBalance = transAmount,   // FIX: was incorrectly re-parsing TransAmount
			Status = till is not null ? 1 : 2,
			DateTimeStamp = eatNow,
			DateModified = eatNow,
			DateCreated = eatNow,
			UserCode = "Mpesa"
		};

		try
		{
			context.MpesaTransactions.Add(transaction);
			await context.SaveChangesAsync(ct);

			logger.LogInformation("[C2B][Confirm] Persisted — TransID={ID} Status={Status}",
				request.TransactionId, transaction.Status);
		}
		catch (DbUpdateException ex)
		{
			// Catches the race-condition window between AnyAsync and SaveChangesAsync.
			// The unique index on TransID ensures the DB rejects the duplicate.
			logger.LogWarning(ex, "[C2B][Confirm] DB conflict — likely duplicate TransID={ID}",
				request.TransactionId);
		}
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		if (!string.IsNullOrWhiteSpace(request.BusinessShortCode))
		{
			var byShortCode = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.TillNumber, request.BusinessShortCode, StringComparison.OrdinalIgnoreCase));

			if (byShortCode is not null)
			{
				logger.LogInformation(
					"[C2B][ResolveTill] Matched via BusinessShortCode='{BSC}' → Till={Till} ({Name})",
					request.BusinessShortCode, byShortCode.TillNumber, byShortCode.Name);

				return byShortCode;
			}

			logger.LogWarning("[C2B][ResolveTill] BusinessShortCode='{BSC}' matched no configured till.",
				request.BusinessShortCode);
		}
		else
		{
			logger.LogWarning("[C2B][ResolveTill] BusinessShortCode missing from request.");
		}

		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var targetRef = request.BillRefNumber.Trim();
			var byRef = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.AccountReference, targetRef, StringComparison.OrdinalIgnoreCase));

			if (byRef is not null)
			{
				logger.LogInformation(
					"[C2B][ResolveTill] Matched via BillRefNumber='{Ref}' → Till={Till} ({Name})",
					targetRef, byRef.TillNumber, byRef.Name);

				return byRef;
			}

			logger.LogWarning("[C2B][ResolveTill] No till matched BSC='{BSC}' or BillRefNumber='{Ref}'.",
				request.BusinessShortCode, targetRef);
		}

		return null;
	}

	// FIX: Accepts a pre-computed EAT fallback so the fallback timestamp is
	//      consistent with DateTimeStamp/DateCreated/DateModified on the record.
	//      The original returned DateTime.UtcNow which was a different timezone.
	private static DateTime ParseTransTime(string? value, DateTime eatFallback)
	{
		if (value?.Length == 14 &&
			DateTime.TryParseExact(
				value, "yyyyMMddHHmmss", null,
				System.Globalization.DateTimeStyles.None, out var dt))
		{
			return dt;
		}

		return eatFallback;
	}

	// FIX: Replaced SanitizeUrl (which threw on invalid/relative URIs) with a
	//      TrySanitize pattern that returns false and lets the caller fail fast
	//      with a descriptive error before any HTTP call is made.
	private static bool TrySanitizeUrl(string? url, out string sanitized)
	{
		sanitized = string.Empty;

		if (string.IsNullOrWhiteSpace(url))
			return false;

		if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
			return false;

		var path = uri.AbsolutePath.Replace("//", "/").ToLowerInvariant();
		sanitized = $"{uri.Scheme.ToLowerInvariant()}://{uri.Host.ToLowerInvariant()}{path}{uri.Query}";
		return true;
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}