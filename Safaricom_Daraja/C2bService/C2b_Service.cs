using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Safaricom_Daraja.C2bService;

public interface IC2BService
{
	Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default);
	Task<IEnumerable<DarajaResult<C2BRegisterResponse>>> RegisterAllTillsAsync(CancellationToken ct = default); // kept for reference/manual use only — see note below
	Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default);
	C2BValidationResponse Validate(C2BValidationRequest request);
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}

public sealed class C2BService(IHttpClientFactory httpFactory,IDarajaTokenService tokenService,IOptions<DarajaConfig> options,ILogger<C2BService> logger,OTOContext context,IShiftResolver resolver) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;
	private readonly IShiftResolver _resolver = resolver;
	private static readonly TimeZoneInfo EatTimeZone = TimeZoneInfo.FindSystemTimeZoneById(OperatingSystem.IsWindows() ? "E. Africa Standard Time" : "Africa/Nairobi");
	
	// ── Registration ──────────────────────────────────────────────────────────

	/// <summary>
	/// PRIMARY registration path. Your tills sit under a head-office aggregator
	/// shortcode (4161705) — Safaricom routes C2B callbacks at that level, not
	/// per-till. This is the only registration call you should need to run.
	/// </summary>
	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterMaster] Starting master shortcode registration. C2BShortCode={C2BSC} BusinessShortCode={BSC}",_cfg.C2BShortCode, _cfg.BusinessShortCode);
		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	/// <summary>
	/// NOTE: Left in place for diagnostic/manual use only. Given confirmed
	/// head-office aggregator routing, registering individual till numbers is
	/// expected to either fail with 400.002.02 or succeed but never actually be
	/// used by Safaricom for routing. Don't call this from startup/DI — call
	/// RegisterMasterShortCodeAsync instead.
	/// </summary>
	public async Task<IEnumerable<DarajaResult<C2BRegisterResponse>>> RegisterAllTillsAsync(CancellationToken ct = default)
	{
		var results = new List<DarajaResult<C2BRegisterResponse>>();

		foreach (var till in _cfg.Tills)
		{
			logger.LogInformation("[C2B][RegisterAllTills] Registering StoreNumber={StoreNumber} ({Name})",till.StoreNumber, till.Name);
			var result = await RegisterUrlsAsync(till.StoreNumber, ct);
			results.Add(result);
		}
		return results;
	}

	
	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default)
	{
		logger.LogInformation("[C2B][RegisterUrls] Called. ShortCode={SC}", shortCode);

		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		if (!TrySanitizeUrl(_cfg.C2BValidationUrl, out var validationUrl))
			return DarajaResult<C2BRegisterResponse>.Fail($"Invalid C2BValidationUrl: '{_cfg.C2BValidationUrl}'");

		if (!TrySanitizeUrl(_cfg.C2BConfirmationUrl, out var confirmationUrl))
			return DarajaResult<C2BRegisterResponse>.Fail($"Invalid C2BConfirmationUrl: '{_cfg.C2BConfirmationUrl}'");

		logger.LogDebug("[C2B][RegisterUrls] ValidationUrl={V} | ConfirmationUrl={C}", validationUrl, confirmationUrl);

		// FIX: ValidationURL is now sent. Daraja's C2B v2 registerurl payload
		// expects this field present even when ResponseType="Completed" causes
		// Safaricom to skip calling it — some accounts reject registration
		// outright if the field is missing from the JSON body entirely.
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
			logger.LogInformation("[C2B][RegisterUrls] Response Status={SC} Success={Ok} Body={Body}",(int)response.StatusCode, response.IsSuccessStatusCode, content);

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
			logger.LogInformation("[C2B][RegisterUrls] Registered. Code={RC} Desc={Desc}",result?.ResponseCode, result?.ResponseDescription);

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

		// IMPORTANT: With head-office aggregator routing, BusinessShortCode here
		// will almost certainly be the master shortcode (4161705), NOT an
		// individual till number. This per-till match will rarely/never hit.
		var tillMatch = _cfg.Tills.FirstOrDefault(t =>
			string.Equals(t.TillNumber, request.BusinessShortCode, StringComparison.OrdinalIgnoreCase));

		if (tillMatch is not null)
		{
			logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID} matched Till={Till} ({Name})",request.TransactionId, tillMatch.TillNumber, tillMatch.Name);
			return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
		}

		// Master shortcode match — expected common case under aggregator routing.
		if (string.Equals(request.BusinessShortCode, _cfg.C2BShortCode, StringComparison.OrdinalIgnoreCase))
		{
			logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID} matched master shortcode={SC} (till-level identity unresolved at validation stage)",request.TransactionId, _cfg.C2BShortCode);
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
				logger.LogInformation("[C2B][Validate] ACCEPTED — TransID={ID} matched BillRefNumber='{Ref}'",request.TransactionId, request.BillRefNumber);
				return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
			}
		}

		logger.LogWarning("[C2B][Validate] REJECTED — TransID={ID} BSC='{BSC}' did not match any till, master shortcode, or account reference.",request.TransactionId, request.BusinessShortCode);

		return Rejected("C2B00011", "Rejected — unrecognized shortcode or account reference");
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	public async Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		DiagnosticDump(request); // safe, observe-only — keep this until till-identifying field is confirmed

		logger.LogInformation("[C2B][Confirm] TransID={ID} Amount={Amount} BusinessShortCode={BSC} BillRefNumber={Ref} CommandID={Cmd}",request.TransactionId, request.TransAmount, request.BusinessShortCode, request.BillRefNumber, request.CommandID);

		var exists = await context.MpesaTransactions
			.AnyAsync(x => x.TransID == request.TransactionId, ct);

		if (exists)
		{
			logger.LogWarning("[C2B][Confirm] Duplicate ignored — TransID={ID}", request.TransactionId);
			return;
		}

		var transAmount = decimal.TryParse(request.TransAmount, out var amt) ? amt : 0m;
		var orgBalance = decimal.TryParse(request.OrgAccountBalance, out var bal) ? bal : 0m;

		if (transAmount <= 0)
		{
			logger.LogWarning("[C2B][Confirm] Suspicious zero/negative amount — TransID={ID} Amount={A}",request.TransactionId, request.TransAmount);
		}

		// Smart extraction for phone numbers because Org-to-Org sets request.PhoneNumber to null
		var finalPhone = request.PhoneNumber;
		if (string.IsNullOrWhiteSpace(finalPhone))
		{
			var rawSourceData = $"{request.BillRefNumber} {request.TransNo} {request.InvoiceNumber} {request.TransactionType}";
			var match = System.Text.RegularExpressions.Regex.Match(rawSourceData, @"(?:254|\+254|0)?(7|1)\d{8}");

			finalPhone = match.Success ? string.Concat("254", match.Value.AsSpan(match.Value.Length - 9)) : "ORGANIZATION_SETTLEMENT";
			
			logger.LogInformation("[C2B][Confirm] Resolved fallback phone payload. Extracted={Phone}", finalPhone);
		}

		var till = await ResolveTill(request, ct);
		var shifts = await _resolver.GetCurrentShiftByTill(till!.TillNumber);
		var shiftNumber = shifts.ResponseObject as string ?? string.Empty;

		var transaction = new MpesaTransaction
		{
			TransactionType = request.TransactionType ?? "C2B",
			TransID = request.TransactionId,
			MpesaReceiptNumber = request.TransactionId,
			TransAmount = transAmount,
			TransTime = ParseTransTime(request.TransTime, EatTime.Now),
			BusinessShortCode = request.BusinessShortCode ?? string.Empty,
			TillNumber = till?.TillNumber ?? "UNMATCHED",
			TillName = till?.TillName ?? "UNMATCHED",
			PaymentMethod = "C2B",
			MSISDN = finalPhone, // Uses resolved fallback string if standard customer field drops
			FirstName = request.FirstName ?? string.Empty,
			MiddName = request.MiddleName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,
			OrgAccountBalance = orgBalance,
			UsageBalance = transAmount,
			Status = till is not null ? 1 : 0,
			DateTimeStamp = EatTime.Now,
			DateModified = EatTime.Now,
			DateCreated = EatTime.Now,
			UserCode = "Mpesa",
			CheckoutRequestID = string.Empty,
			MerchantRequestID = string.Empty,
			ShiftNumber = shiftNumber 

		};

		try
		{
			context.MpesaTransactions.Add(transaction);
			await context.SaveChangesAsync(ct);

			logger.LogInformation("[C2B][Confirm] Persisted — TransID={ID} Status={Status} TillResolved={Resolved}",request.TransactionId, transaction.Status, till is not null);
		}
		catch (DbUpdateException ex)
		{
			logger.LogWarning(ex, "[C2B][Confirm] DB conflict — likely duplicate TransID={ID}",
				request.TransactionId);
		}
	}


	// ── Private helpers ───────────────────────────────────────────────────────

	private async Task<Tills?> ResolveTill(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		if (!string.IsNullOrWhiteSpace(request.BusinessShortCode))
		{
			var bsc = request.BusinessShortCode.Trim();

			// Match TillNumber OR StoreNumber — Safaricom sends StoreNumber as BusinessShortCode
			var byShortCode = await context.Tills
				.Where(t => t.IsActive && (t.TillNumber == bsc || t.StoreNumber == bsc))
				.FirstOrDefaultAsync(ct);

			if (byShortCode is not null)
			{
				logger.LogInformation("[C2B][ResolveTill] Matched via BusinessShortCode='{BSC}' → Till={Till} ({Name})",bsc, byShortCode.TillNumber, byShortCode.TillName);
				return byShortCode;
			}
			
			logger.LogWarning("[C2B][ResolveTill] BusinessShortCode='{BSC}' matched no configured till.", bsc);
		}

		// BillRefNumber fallback
		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var targetRef = request.BillRefNumber.Trim();
			var byRef = await context.Tills
				.Where(t => t.IsActive && (t.TillNumber == targetRef || t.StoreNumber == targetRef))
				.FirstOrDefaultAsync(ct);

			if (byRef is not null)
			{
				logger.LogInformation("[C2B][ResolveTill] Matched via BillRefNumber='{Ref}' → Till={Till} ({Name})",targetRef, byRef.TillNumber, byRef.TillName);
				return byRef;
			}
		}

		logger.LogWarning("[C2B][ResolveTill] No till matched BSC='{BSC}' BillRef='{Ref}'.",
			request.BusinessShortCode, request.BillRefNumber);
		return null;
	}

	private void DiagnosticDump(C2BConfirmationRequest request)
	{
		Console.WriteLine("══════════════════════════════════════════════════════");
		Console.WriteLine($"[C2B-DIAG] {EatTime.Now:yyyy-MM-dd HH:mm:ss} UTC");
		Console.WriteLine("──────────────────────────────────────────────────────");
		Console.WriteLine($"TransactionType     : {request.TransactionType}");
		Console.WriteLine($"TransID             : {request.TransactionId}");
		Console.WriteLine($"TransTime           : {request.TransTime}");
		Console.WriteLine($"TransAmount         : {request.TransAmount}");
		Console.WriteLine($"BusinessShortCode   : {request.BusinessShortCode}");
		Console.WriteLine($"BillRefNumber       : {request.BillRefNumber}");
		Console.WriteLine($"InvoiceNumber       : {request.InvoiceNumber}");
		Console.WriteLine($"OrgAccountBalance   : {request.OrgAccountBalance}");
		Console.WriteLine($"ThirdPartyTransID   : {request.ThirdPartyTransId}");
		Console.WriteLine($"MSISDN              : {request.PhoneNumber}");
		Console.WriteLine($"FirstName           : {request.FirstName}");
		Console.WriteLine($"MiddleName          : {request.MiddleName}");
		Console.WriteLine($"LastName            : {request.LastName}");
		Console.WriteLine("──────────────────────────────────────────────────────");

		var byShortCode = _cfg.Tills.FirstOrDefault(t =>
			string.Equals(t.TillNumber, request.BusinessShortCode, StringComparison.OrdinalIgnoreCase));

		var byRef = !string.IsNullOrWhiteSpace(request.BillRefNumber) ? _cfg.Tills.FirstOrDefault(t => string.Equals(t.AccountReference, request.BillRefNumber.Trim(), StringComparison.OrdinalIgnoreCase)) : null;

		Console.WriteLine($"Would match via BusinessShortCode → {(byShortCode is not null ? $"{byShortCode.TillNumber} ({byShortCode.Name})" : "NO MATCH")}");
		Console.WriteLine($"Would match via BillRefNumber      → {(byRef is not null ? $"{byRef.TillNumber} ({byRef.Name})" : "NO MATCH")}");
		Console.WriteLine($"Matches master shortcode ({_cfg.C2BShortCode}) → {string.Equals(request.BusinessShortCode, _cfg.C2BShortCode, StringComparison.OrdinalIgnoreCase)}");

		var isO2O = string.Equals(request.TransactionType, "Organization To Organization Transfer", StringComparison.OrdinalIgnoreCase);
		Console.WriteLine($"Is O2O sweep                       → {isO2O}");

		var rawJson = JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true });
		Console.WriteLine("──────────────────────────────────────────────────────");
		Console.WriteLine("RAW JSON (model-bound — won't show unmapped fields):");
		Console.WriteLine(rawJson);
		Console.WriteLine("══════════════════════════════════════════════════════");
	}

	private static DateTime ParseTransTime(string? value, DateTime eatFallback)
	{
		if (value?.Length == 14 && DateTime.TryParseExact(value, "yyyyMMddHHmmss", null,System.Globalization.DateTimeStyles.None, out var dt))
		{
			return dt;
		}

		return eatFallback;
	}

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