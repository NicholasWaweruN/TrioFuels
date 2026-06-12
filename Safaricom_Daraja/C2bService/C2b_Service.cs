using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;

namespace Daraja.Services;

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
	OTOContext context                  // ✅ injected for persistence
) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;

	// ── Registration ──────────────────────────────────────────────────────────

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(
		CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterMaster] ▶ Starting master shortcode registration. " +"C2BShortCode={C2BSC} BusinessShortCode={BSC} C2BValidationUrl={VUrl} C2BConfirmationUrl={CUrl}",_cfg.C2BShortCode, _cfg.BusinessShortCode, _cfg.C2BValidationUrl, _cfg.C2BConfirmationUrl);
		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default)
	{
		var step = 0;

		logger.LogInformation("[C2B][RegisterUrls] ▶ Step {S}: Called. ShortCode(raw)={SC}",++step, shortCode);

		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		var validationUrl = SanitizeUrl(_cfg.C2BValidationUrl);
		var confirmationUrl = SanitizeUrl(_cfg.C2BConfirmationUrl);

		logger.LogInformation("[C2B][RegisterUrls] Step {S}: URL sanitization complete. " +"ValidationUrl (raw)={VRaw} → (sanitized)={VSan} | " +"ConfirmationUrl (raw)={CRaw} → (sanitized)={CSan}",
			++step,_cfg.C2BValidationUrl, validationUrl,_cfg.C2BConfirmationUrl, confirmationUrl);

		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed",
			ValidationURL = validationUrl,
			ConfirmationURL = confirmationUrl
		};

		logger.LogInformation(
			"[C2B][RegisterUrls] Step {S}: Payload built. JSON={Json}",++step, JsonSerializer.Serialize(payload));

		try
		{
			logger.LogInformation("[C2B][RegisterUrls] Step {S}: Acquiring Daraja access token...", ++step);

			string token;
			try
			{
				token = await tokenService.GetAccessTokenAsync(ct);
				logger.LogInformation("[C2B][RegisterUrls] Step {S}: Token acquired. Token(first12)={T}... Length={L}",++step, token[..Math.Min(12, token.Length)], token.Length);
			}
			catch (Exception ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ❌ Token acquisition FAILED. ExType={ExType} Message={Msg}",
					++step, ex.GetType().Name, ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"Token error: {ex.Message}");
			}

			var client = httpFactory.CreateClient("Daraja");
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);

			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: HttpClient ready. BaseAddress={Base}",
				++step, client.BaseAddress?.ToString() ?? "(null)");

			const string endpoint = "/mpesa/c2b/v2/registerurl";
			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: POSTing to {Endpoint}. Full URL={Full}",
				++step, endpoint, new Uri(client.BaseAddress!, endpoint));

			HttpResponseMessage response;
			try
			{
				response = await client.PostAsJsonAsync(endpoint, payload, ct);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ❌ HTTP request THREW. ExType={ExType} Message={Msg}",
					++step, ex.GetType().Name, ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"HTTP error: {ex.Message}");
			}
			catch (TaskCanceledException ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ❌ Request TIMED OUT. IsCancellationRequested={CR}",
					++step, ct.IsCancellationRequested);
				return DarajaResult<C2BRegisterResponse>.Fail("Request timed out");
			}

			var content = await response.Content.ReadAsStringAsync(ct);

			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: Response received. " +
				"StatusCode={SC} IsSuccess={Ok} ReasonPhrase={Reason} Body={Body}",
				++step, (int)response.StatusCode, response.IsSuccessStatusCode,
				response.ReasonPhrase, content);

			if (!response.IsSuccessStatusCode)
			{
				if (content.Contains("500.003.1001"))
				{
					logger.LogInformation(
						"[C2B][RegisterUrls] Step {S}: ✅ URLs already registered (idempotent). ShortCode={SC}",
						++step, shortCode);

					return DarajaResult<C2BRegisterResponse>.Ok(new C2BRegisterResponse
					{
						ResponseCode = "0",
						ResponseDescription = "URLs already registered (idempotent)"
					});
				}

				logger.LogError(
					"[C2B][RegisterUrls] Step {S}: ❌ Registration FAILED. HttpStatus={SC} Body={Body}",
					++step, (int)response.StatusCode, content);

				return DarajaResult<C2BRegisterResponse>.Fail(content);
			}

			C2BRegisterResponse? result;
			try
			{
				result = JsonSerializer.Deserialize<C2BRegisterResponse>(content);
				logger.LogInformation(
					"[C2B][RegisterUrls] Step {S}: ✅ Registration SUCCESS. " +
					"ShortCode={SC} ResponseCode={RC} ResponseDescription={Desc}",
					++step, shortCode, result?.ResponseCode, result?.ResponseDescription);
			}
			catch (JsonException ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ⚠️ HTTP 200 but JSON deserialization FAILED. Body={Body}",
					++step, content);
				return DarajaResult<C2BRegisterResponse>.Fail($"JSON parse error: {ex.Message}");
			}

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex,
				"[C2B][RegisterUrls] ❌ Unhandled exception. ShortCode={SC} ExType={ExType} Message={Msg}",
				shortCode, ex.GetType().Name, ex.Message);
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		logger.LogInformation(
			"[C2B][Validate] ▶ TransID={ID} Amount={Amount} BSC={BSC} " +
			"BillRefNumber={Ref} Phone={Phone}",
			request.TransactionId, request.TransAmount,
			request.BusinessShortCode, request.BillRefNumber, request.PhoneNumber);

		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning(
				"[C2B][Validate] ❌ REJECTED — BillRefNumber is null/empty. TransID={ID}",
				request.TransactionId);
			return Rejected("C2B00011", "Rejected — missing account reference");
		}

		var knownRefs = _cfg.Tills
			.Select(t => t.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		logger.LogInformation("[C2B][Validate] KnownRefs=[{Refs}] checking BillRefNumber='{Ref}'",
			string.Join(", ", knownRefs), request.BillRefNumber);

		if (!knownRefs.Contains(request.BillRefNumber))
		{
			logger.LogWarning(
				"[C2B][Validate] ❌ REJECTED — BillRefNumber='{Ref}' not in known refs. TransID={ID}",
				request.BillRefNumber, request.TransactionId);
			return Rejected("C2B00011", "Rejected — unknown account reference");
		}

		logger.LogInformation(
			"[C2B][Validate] ✅ ACCEPTED — TransID={ID} Amount={Amount} Phone={Phone} Ref={Ref}",
			request.TransactionId, request.TransAmount,
			request.PhoneNumber, request.BillRefNumber);

		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	public async Task HandleConfirmationAsync(
		C2BConfirmationRequest request,
		CancellationToken ct = default)
	{
		logger.LogInformation(
			"[C2B][Confirm] ▶ TransID={ID} Amount={Amount} BSC={BSC} " +
			"BillRefNumber={Ref} Phone={Phone} TransTime={Time}",
			request.TransactionId, request.TransAmount, request.BusinessShortCode,
			request.BillRefNumber, request.PhoneNumber, request.TransTime);

		// ── DUPLICATE PROTECTION ──────────────────────────────────────────────
		var exists = await context.MpesaTransactions
			.AnyAsync(x => x.TransID == request.TransactionId, ct);

		if (exists)
		{
			logger.LogWarning(
				"[C2B][Confirm] ⚠️ Duplicate — TransID={ID} already in MpesaTransactions. Ignored.",
				request.TransactionId);
			return;
		}

		// ── RESOLVE TILL ──────────────────────────────────────────────────────
		var till = ResolveTill(request);

		// ── PERSIST — matched or unmatched ───────────────────────────────────
		var transaction = new MpesaTransaction
		{
			TransactionType = request.TransactionType ?? "C2B",
			TransID = request.TransactionId,
			MpesaReceiptNumber = request.TransactionId,   // C2B TransID is the receipt
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
			Status = till is not null ? 1 : 2,  // 1=Success 2=Unmatched
			DateTimeStamp = DateTime.UtcNow.AddHours(3),
			DateModified = DateTime.UtcNow.AddHours(3),
			DateCreated = DateTime.UtcNow.AddHours(3),
			CheckoutRequestID = null,
			MerchantRequestID = null,
			UserCode = "Mpesa",
		};

		context.MpesaTransactions.Add(transaction);
		await context.SaveChangesAsync(ct);

		if (till is not null)
		{
			logger.LogInformation(
				"[C2B][Confirm] ✅ Persisted — TransID={ID} Amount=KES {Amount} " +
				"Phone={Phone} Till={TN} ({TillName})",
				request.TransactionId, request.TransAmount,
				request.PhoneNumber, till.TillNumber, till.Name);
		}
		else
		{
			logger.LogWarning(
				"[C2B][Confirm] ⚠️ Persisted as UNMATCHED — TransID={ID} Amount=KES {Amount} " +
				"Phone={Phone} BSC={BSC} BillRef={Ref}",
				request.TransactionId, request.TransAmount,
				request.PhoneNumber, request.BusinessShortCode, request.BillRefNumber);
		}
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		logger.LogInformation(
			"[C2B][ResolveTill] ▶ BSC={BSC} BillRefNumber={Ref}",
			request.BusinessShortCode, request.BillRefNumber);

		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var byRef = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.AccountReference, request.BillRefNumber,
					StringComparison.OrdinalIgnoreCase));

			if (byRef is not null)
			{
				logger.LogInformation(
					"[C2B][ResolveTill] ✅ Matched BillRefNumber='{Ref}' → Till={TN}",
					request.BillRefNumber, byRef.TillNumber);
				return byRef;
			}

			logger.LogWarning(
				"[C2B][ResolveTill] BillRefNumber='{Ref}' not matched. KnownRefs=[{Refs}]",
				request.BillRefNumber,
				string.Join(", ", _cfg.Tills.Select(t => t.AccountReference)));
		}
		else
		{
			logger.LogWarning("[C2B][ResolveTill] BillRefNumber is null/empty.");
		}

		logger.LogWarning(
			"[C2B][ResolveTill] ❌ No till matched — will persist as UNMATCHED. BSC={BSC}",
			request.BusinessShortCode);

		return null;
	}

	private static DateTime ParseTransTime(string? value)
	{
		if (string.IsNullOrWhiteSpace(value)) return DateTime.UtcNow;

		if (value.Length == 14 &&
			DateTime.TryParseExact(value, "yyyyMMddHHmmss", null,
				System.Globalization.DateTimeStyles.None, out var dt))
			return dt;

		return DateTime.UtcNow;
	}

	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;

		var uri = new Uri(url);
		var path = uri.AbsolutePath.Replace("//", "/").ToLowerInvariant();
		var query = string.IsNullOrEmpty(uri.Query) ? "" : uri.Query;
		return $"{uri.Scheme.ToLowerInvariant()}://{uri.Host.ToLowerInvariant()}{path}{query}";
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}