using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
	ILogger<C2BService> logger
) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;

	// ── Registration ──────────────────────────────────────────────────────────

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(
		CancellationToken ct = default)
	{
		logger.LogInformation(
			"[C2B][RegisterMaster] ▶ Starting master shortcode registration. " +
			"C2BShortCode={C2BSC} BusinessShortCode={BSC} C2BValidationUrl={VUrl} C2BConfirmationUrl={CUrl}",
			_cfg.C2BShortCode, _cfg.BusinessShortCode, _cfg.C2BValidationUrl, _cfg.C2BConfirmationUrl);

		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default)
	{
		var step = 0;

		logger.LogInformation(
			"[C2B][RegisterUrls] ▶ Step {S}: Called. ShortCode(raw)={SC}",
			++step, shortCode);

		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		// ── Sanitize URLs ─────────────────────────────────────────────────────
		var validationUrl = SanitizeUrl(_cfg.C2BValidationUrl);
		var confirmationUrl = SanitizeUrl(_cfg.C2BConfirmationUrl);

		logger.LogInformation(
			"[C2B][RegisterUrls] Step {S}: URL sanitization complete. " +
			"ValidationUrl (raw)={VRaw} → (sanitized)={VSan} | " +
			"ConfirmationUrl (raw)={CRaw} → (sanitized)={CSan}",
			++step,
			_cfg.C2BValidationUrl, validationUrl,
			_cfg.C2BConfirmationUrl, confirmationUrl);

		// ── Build payload ─────────────────────────────────────────────────────
		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed",
			ValidationURL = validationUrl,
			ConfirmationURL = confirmationUrl
		};

		var payloadJson = JsonSerializer.Serialize(payload);
		logger.LogInformation(
			"[C2B][RegisterUrls] Step {S}: Payload built. JSON={Json}",
			++step, payloadJson);

		try
		{
			// ── Acquire token ─────────────────────────────────────────────────
			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: Acquiring Daraja access token...", ++step);

			string token;
			try
			{
				token = await tokenService.GetAccessTokenAsync(ct);
				logger.LogInformation(
					"[C2B][RegisterUrls] Step {S}: Token acquired. " +
					"Token(first12)={T}... Length={L}",
					++step, token[..Math.Min(12, token.Length)], token.Length);
			}
			catch (Exception ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ❌ Token acquisition FAILED. " +
					"Exception={ExType} Message={Msg}",
					++step, ex.GetType().Name, ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"Token error: {ex.Message}");
			}

			// ── Build HTTP client ─────────────────────────────────────────────
			var client = httpFactory.CreateClient("Daraja");
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);

			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: HttpClient ready. " +
				"BaseAddress={Base} AuthScheme=Bearer",
				++step, client.BaseAddress?.ToString() ?? "(null — check HttpClient registration)");

			// ── POST ──────────────────────────────────────────────────────────
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
					"[C2B][RegisterUrls] Step {S}: ❌ HTTP request THREW. " +
					"ExType={ExType} StatusCode={SC} Message={Msg}",
					++step, ex.GetType().Name, ex.StatusCode, ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"HTTP error: {ex.Message}");
			}
			catch (TaskCanceledException ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ❌ Request TIMED OUT or was cancelled. " +
					"IsCancellationRequested={CR}",
					++step, ct.IsCancellationRequested);
				return DarajaResult<C2BRegisterResponse>.Fail("Request timed out");
			}

			// ── Read response ─────────────────────────────────────────────────
			var content = await response.Content.ReadAsStringAsync(ct);

			logger.LogInformation(
				"[C2B][RegisterUrls] Step {S}: Response received. " +
				"StatusCode={SC} IsSuccess={Ok} ReasonPhrase={Reason} " +
				"ContentType={CT} Body={Body}",
				++step,
				(int)response.StatusCode,
				response.IsSuccessStatusCode,
				response.ReasonPhrase,
				response.Content.Headers.ContentType?.ToString() ?? "(none)",
				content);

			// Log all response headers for deep debugging
			var headerDump = new StringBuilder();
			foreach (var h in response.Headers)
				headerDump.Append($"{h.Key}=[{string.Join(",", h.Value)}] ");
			logger.LogDebug(
				"[C2B][RegisterUrls] Step {S}: Response headers: {Headers}",
				++step, headerDump.ToString());

			// ── Handle non-success ────────────────────────────────────────────
			if (!response.IsSuccessStatusCode)
			{
				// 500.003.1001 = "URLs already registered" — idempotent, treat as success
				if (content.Contains("500.003.1001"))
				{
					logger.LogInformation(
						"[C2B][RegisterUrls] Step {S}: ✅ URLs already registered (idempotent). " +
						"ShortCode={SC} — no further action needed.",
						++step, shortCode);

					return DarajaResult<C2BRegisterResponse>.Ok(new C2BRegisterResponse
					{
						ResponseCode = "0",
						ResponseDescription = "URLs already registered (idempotent)"
					});
				}

				logger.LogError(
					"[C2B][RegisterUrls] Step {S}: ❌ Registration FAILED. " +
					"HttpStatus={SC} ShortCode={Code} Body={Body}",
					++step, (int)response.StatusCode, shortCode, content);

				return DarajaResult<C2BRegisterResponse>.Fail(content);
			}

			// ── Parse success ─────────────────────────────────────────────────
			C2BRegisterResponse? result;
			try
			{
				result = JsonSerializer.Deserialize<C2BRegisterResponse>(content);
				logger.LogInformation(
					"[C2B][RegisterUrls] Step {S}: ✅ Registration SUCCESS. " +
					"ShortCode={SC} ResponseCode={RC} ResponseDescription={Desc} " +
					"OriginatorCoversationID={OC} ConversationID={CI}",
					++step, shortCode,
					result?.ResponseCode,
					result?.ResponseDescription,
					result?.OriginatorConversationId,
					result?.OriginatorConversationId);
			}
			catch (JsonException ex)
			{
				logger.LogError(ex,
					"[C2B][RegisterUrls] Step {S}: ⚠️ HTTP 200 but JSON deserialization FAILED. " +
					"Body={Body} ExMessage={Msg}",
					++step, content, ex.Message);
				return DarajaResult<C2BRegisterResponse>.Fail($"JSON parse error: {ex.Message}");
			}

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex,
				"[C2B][RegisterUrls] ❌ Unhandled exception. " +
				"ShortCode={SC} ExType={ExType} Message={Msg} StackTrace={ST}",
				shortCode, ex.GetType().Name, ex.Message, ex.StackTrace);
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		logger.LogInformation(
			"[C2B][Validate] ▶ Incoming validation request. " +
			"TransactionType={TT} TransID={ID} TransTime={Time} " +
			"TransAmount={Amount} BusinessShortCode={BSC} BillRefNumber={Ref} " +
			"InvoiceNumber={Inv} OrgAccountBalance={Bal} ThirdPartyTransID={TPID} " +
			"MSISDN={Phone} FirstName={FN} MiddleName={MN} LastName={LN}",
			request.TransactionType, request.TransactionId, request.TransTime,
			request.TransAmount, request.BusinessShortCode, request.BillRefNumber,
			request.InvoiceNumber, request.OrgAccountBalance, request.ThirdPartyTransId,
			request.PhoneNumber, request.FirstName, request.MiddleName, request.LastName);

		// ── Gate 1: BillRefNumber present ────────────────────────────────────
		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning(
				"[C2B][Validate] ❌ REJECTED — BillRefNumber is null/empty. " +
				"TransID={ID} Phone={Phone}",
				request.TransactionId, request.PhoneNumber);
			return Rejected("C2B00011", "Rejected — missing account reference");
		}

		// ── Gate 2: BillRefNumber matches a known till ────────────────────────
		var knownRefs = _cfg.Tills
			.Select(t => t.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		logger.LogInformation(
			"[C2B][Validate] Known AccountReferences: [{Refs}] — checking BillRefNumber='{Ref}'",
			string.Join(", ", knownRefs), request.BillRefNumber);

		if (!knownRefs.Contains(request.BillRefNumber))
		{
			logger.LogWarning(
				"[C2B][Validate] ❌ REJECTED — BillRefNumber='{Ref}' not in known refs. " +
				"TransID={ID} Phone={Phone}",
				request.BillRefNumber, request.TransactionId, request.PhoneNumber);
			return Rejected("C2B00011", "Rejected — unknown account reference");
		}

		logger.LogInformation(
			"[C2B][Validate] ✅ ACCEPTED — TransID={ID} Amount={Amount} Phone={Phone} Ref={Ref}",
			request.TransactionId, request.TransAmount, request.PhoneNumber, request.BillRefNumber);

		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	public async Task HandleConfirmationAsync(
		C2BConfirmationRequest request,
		CancellationToken ct = default)
	{
		logger.LogInformation(
			"[C2B][Confirm] ▶ Incoming confirmation. " +
			"TransactionType={TT} TransID={ID} TransTime={Time} " +
			"TransAmount={Amount} BusinessShortCode={BSC} BillRefNumber={Ref} " +
			"InvoiceNumber={Inv} OrgAccountBalance={Bal} ThirdPartyTransID={TPID} " +
			"MSISDN={Phone} FirstName={FN} MiddleName={MN} LastName={LN}",
			request.TransactionType, request.TransactionId, request.TransTime,
			request.TransAmount, request.BusinessShortCode, request.BillRefNumber,
			request.InvoiceNumber, request.OrgAccountBalance, request.ThirdPartyTransId,
			request.PhoneNumber, request.FirstName, request.MiddleName, request.LastName);

		// ── Dump config tills for cross-reference ─────────────────────────────
		logger.LogInformation(
			"[C2B][Confirm] Config tills: [{Tills}]",
			string.Join(" | ", _cfg.Tills.Select(t =>
				$"Name={t.Name} TillNumber={t.TillNumber} AccountRef={t.AccountReference}")));

		logger.LogInformation(
			"[C2B][Confirm] Config shortcodes: C2BShortCode={C2BSC} BusinessShortCode={BSC}",
			_cfg.C2BShortCode, _cfg.BusinessShortCode);

		var till = ResolveTill(request);

		if (till is null)
		{
			logger.LogWarning(
				"[C2B][Confirm] ⚠️ No matching till found — routing to unmatched queue. " +
				"BusinessShortCode={BSC} BillRefNumber={Ref} TransID={ID} Amount={Amount} Phone={Phone}",
				request.BusinessShortCode, request.BillRefNumber,
				request.TransactionId, request.TransAmount, request.PhoneNumber);

			// TODO: await _paymentRepo.SaveUnmatchedAsync(request, ct);
			return;
		}

		logger.LogInformation(
			"[C2B][Confirm] ✅ Till resolved — TillName={TillName} TillNumber={TillNumber} " +
			"TransID={ID} Amount=KES {Amount} Phone={Phone} Ref={Ref} Time={Time}",
			till.Name, till.TillNumber,
			request.TransactionId, request.TransAmount,
			request.PhoneNumber, request.BillRefNumber, request.TransTime);

		// TODO: persist payment
		await Task.CompletedTask;
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		logger.LogInformation(
			"[C2B][ResolveTill] ▶ Attempting till resolution. " +
			"BusinessShortCode={BSC} BillRefNumber={Ref}",
			request.BusinessShortCode, request.BillRefNumber);

		// Strategy 1: match by BillRefNumber (AccountReference)
		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var byRef = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.AccountReference, request.BillRefNumber,
					StringComparison.OrdinalIgnoreCase));

			if (byRef is not null)
			{
				logger.LogInformation(
					"[C2B][ResolveTill] ✅ Matched by BillRefNumber='{Ref}' → " +
					"TillName={Name} TillNumber={TN}",
					request.BillRefNumber, byRef.Name, byRef.TillNumber);
				return byRef;
			}

			logger.LogWarning(
				"[C2B][ResolveTill] BillRefNumber='{Ref}' did not match any AccountReference. " +
				"KnownRefs=[{Refs}]",
				request.BillRefNumber,
				string.Join(", ", _cfg.Tills.Select(t => t.AccountReference)));
		}
		else
		{
			logger.LogWarning("[C2B][ResolveTill] BillRefNumber is null/empty — skipping ref match.");
		}

		// Strategy 2: check if ShortCode matches HO shortcode
		// NOTE: Safaricom sends head-office shortcode in C2B confirmations, not the till number.
		// So BusinessShortCode == till.TillNumber will NEVER match — this is expected behaviour.
		var shortCodeMatchesHO =
			request.BusinessShortCode == _cfg.C2BShortCode ||
			request.BusinessShortCode == _cfg.BusinessShortCode;

		logger.LogWarning(
			"[C2B][ResolveTill] ❌ Could not resolve till. " +
			"IncomingShortCode={ISC} C2BShortCode={C2BSC} BusinessShortCode={BSC} " +
			"ShortCodeMatchesHO={HOMatch} → routing to unmatched queue.",
			request.BusinessShortCode, _cfg.C2BShortCode, _cfg.BusinessShortCode, shortCodeMatchesHO);

		return null;
	}

	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;

		var uri = new Uri(url);
		var path = uri.AbsolutePath
			.Replace("//", "/")
			.ToLowerInvariant();

		var query = string.IsNullOrEmpty(uri.Query) ? "" : uri.Query;
		return $"{uri.Scheme.ToLowerInvariant()}://{uri.Host.ToLowerInvariant()}{path}{query}";
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}