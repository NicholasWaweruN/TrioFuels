using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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

public sealed class C2BService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<C2BService> logger,
	OTOContext context) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;
	private static readonly TimeZoneInfo EatTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");

	// ── Registration ──────────────────────────────────────────────────────────

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterMaster] ▶ Starting master shortcode registration. C2BShortCode={C2BSC} BusinessShortCode={BSC}", _cfg.C2BShortCode, _cfg.BusinessShortCode);
		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterUrls] [Init] Called. ShortCode={SC}", shortCode);

		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		var validationUrl = SanitizeUrl(_cfg.C2BValidationUrl);
		var confirmationUrl = SanitizeUrl(_cfg.C2BConfirmationUrl);

		logger.LogDebug("[C2B][RegisterUrls] [Sanitize] ValidationUrl={VSan} | ConfirmationUrl={CSan}", validationUrl, confirmationUrl);

		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed",
			ValidationURL = validationUrl,
			ConfirmationURL = confirmationUrl
		};

		try
		{
			logger.LogInformation("[C2B][RegisterUrls] [Token] Acquiring Daraja access token...");
			string token;
			try
			{
				token = await tokenService.GetAccessTokenAsync(ct);
				logger.LogInformation("[C2B][RegisterUrls] [Token] Token acquired. Length={L}", token.Length);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] [Token] Token acquisition FAILED. Message={Msg}", ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"Token error: {ex.Message}");
			}

			var client = httpFactory.CreateClient("Daraja");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			const string endpoint = "/mpesa/c2b/v2/registerurl";
			logger.LogInformation("[C2B][RegisterUrls] [HTTP Post] Dispatching to endpoint: {Endpoint}", endpoint);

			HttpResponseMessage response;
			try
			{
				response = await client.PostAsJsonAsync(endpoint, payload, ct);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] [HTTP Post] Network transmission threw an exception. Message={Msg}", ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"HTTP error: {ex.Message}");
			}
			catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
			{
				logger.LogError(ex, "[C2B][RegisterUrls] [HTTP Post] Request timed out implicitly.");
				return DarajaResult<C2BRegisterResponse>.Fail("Request timed out");
			}

			var content = await response.Content.ReadAsStringAsync(ct);
			logger.LogInformation("[C2B][RegisterUrls] [Response] Received Status={SC} Success={Ok}", (int)response.StatusCode, response.IsSuccessStatusCode);

			if (!response.IsSuccessStatusCode)
			{
				// M-Pesa error code 500.003.1001 indicates URL is already assigned to this shortcode.
				if (content.Contains("500.003.1001"))
				{
					logger.LogInformation("[C2B][RegisterUrls] [Idempotent] URLs already registered for ShortCode={SC}", shortCode);
					return DarajaResult<C2BRegisterResponse>.Ok(new C2BRegisterResponse
					{
						ResponseCode = "0",
						ResponseDescription = "URLs already registered (idempotent)"
					});
				}

				logger.LogError("[C2B][RegisterUrls] [Error] Registration FAILED. Status={SC} Body={Body}", (int)response.StatusCode, content);
				return DarajaResult<C2BRegisterResponse>.Fail(content);
			}

			var result = JsonSerializer.Deserialize<C2BRegisterResponse>(content);
			logger.LogInformation("[C2B][RegisterUrls] [Success] Registration verified. Code={RC} Desc={Desc}", result?.ResponseCode, result?.ResponseDescription);

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (JsonException ex)
		{
			logger.LogError(ex, "[C2B][RegisterUrls] [Exception] JSON parsing failure.");
			return DarajaResult<C2BRegisterResponse>.Fail($"JSON parse error: {ex.Message}");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "[C2B][RegisterUrls] [Exception] Unhandled exception execution flow broken.");
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		logger.LogInformation("[C2B][Validate] ▶ TransID={ID} Amount={Amount} BillRefNumber={Ref}", request.TransactionId, request.TransAmount, request.BillRefNumber);

		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning("[C2B][Validate] REJECTED — BillRefNumber is null/empty. TransID={ID}", request.TransactionId);
			return Rejected("C2B00011", "Rejected — missing account reference");
		}

		var knownRefs = _cfg.Tills
			.Select(t => t.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (!knownRefs.Contains(request.BillRefNumber.Trim()))
		{
			logger.LogWarning("[C2B][Validate] REJECTED — BillRefNumber='{Ref}' mismatch. TransID={ID}", request.BillRefNumber, request.TransactionId);
			return Rejected("C2B00011", "Rejected — unknown account reference");
		}

		logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID}", request.TransactionId);
		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	public async Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][Confirm] ▶ TransID={ID} Amount={Amount} BillRefNumber={Ref}", request.TransactionId, request.TransAmount, request.BillRefNumber);

		// 1. Double check within memory context to save explicit DB query overhead
		var exists = await context.MpesaTransactions.AnyAsync(x => x.TransID == request.TransactionId, ct);
		if (exists)
		{
			logger.LogWarning("[C2B][Confirm] Duplicate transaction ignored — TransID={ID}", request.TransactionId);
			return;
		}

		var till = ResolveTill(request);
		var explicitEatTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, EatTimeZone);

		var transaction = new MpesaTransaction
		{
			TransactionType = request.TransactionType ?? "C2B",
			TransID = request.TransactionId,
			MpesaReceiptNumber = request.TransactionId,
			TransAmount = decimal.TryParse(request.TransAmount, out var amt) ? amt : 0,
			TransTime = ParseTransTime(request.TransTime),
			BusinessShortCode = request.BusinessShortCode ?? string.Empty,
			TillNumber = till?.TillNumber ?? "UNMATCHED",
			TillName = till?.Name ?? "UNMATCHED",
			PaymentMethod = "C2B",
			MSISDN = request.PhoneNumber ?? string.Empty,
			FirstName = request.FirstName ?? string.Empty,
			MiddName = request.MiddleName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,
			OrgAccountBalance = decimal.TryParse(request.OrgAccountBalance, out var bal) ? bal : 0,
			UsageBalance = decimal.TryParse(request.TransAmount, out var usage) ? usage : 0,
			Status = till is not null ? 1 : 2,  // 1 = Success, 2 = Unmatched
			DateTimeStamp = explicitEatTime,
			DateModified = explicitEatTime,
			DateCreated = explicitEatTime,
			UserCode = "Mpesa"
		};

		try
		{
			context.MpesaTransactions.Add(transaction);
			await context.SaveChangesAsync(ct);

			logger.LogInformation("[C2B][Confirm] Persisted successfully — TransID={ID} Status={Status}", request.TransactionId, transaction.Status);
		}
		catch (DbUpdateException ex)
		{
			// Catches any concurrency race condition bypassed by the AnyAsync checker
			logger.LogWarning(ex, "[C2B][Confirm] Database insertion conflict caught. Likely a duplicate TransID={ID}", request.TransactionId);
		}
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning("[C2B][ResolveTill] BillRefNumber missing from request.");
			return null;
		}

		var targetRef = request.BillRefNumber.Trim();
		var byRef = _cfg.Tills.FirstOrDefault(t => string.Equals(t.AccountReference, targetRef, StringComparison.OrdinalIgnoreCase));

		if (byRef is not null)
		{
			return byRef;
		}

		logger.LogWarning("[C2B][ResolveTill] No configurations matched BillRefNumber='{Ref}'", request.BillRefNumber);
		return null;
	}

	private static DateTime ParseTransTime(string? value)
	{
		if (value?.Length == 14 && DateTime.TryParseExact(value, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out var dt))
		{
			return dt;
		}
		return DateTime.UtcNow;
	}

	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;

		var uri = new Uri(url);
		var path = uri.AbsolutePath.Replace("//", "/").ToLowerInvariant();
		var query = uri.Query;
		return $"{uri.Scheme.ToLowerInvariant()}://{uri.Host.ToLowerInvariant()}{path}{query}";
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}