using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;

namespace Daraja.Services;

public sealed class C2BService : IC2BService
{
	private readonly IHttpClientFactory _httpFactory;
	private readonly IDarajaTokenService _tokenService;
	private readonly DarajaConfig _cfg;
	private readonly ILogger<C2BService> _logger;
	private readonly OTOContext _context;

	public C2BService(
		IHttpClientFactory httpFactory,
		IDarajaTokenService tokenService,
		IOptions<DarajaConfig> options,
		ILogger<C2BService> logger,
		OTOContext context)
	{
		_httpFactory = httpFactory;
		_tokenService = tokenService;
		_cfg = options.Value;
		_logger = logger;
		_context = context;
	}

	// ─────────────────────────────────────────────
	// URL REGISTRATION
	// ─────────────────────────────────────────────

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(
		CancellationToken ct = default)
	{
		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default)
	{
		var validationUrl = SanitizeUrl(_cfg.C2BValidationUrl);
		var confirmationUrl = SanitizeUrl(_cfg.C2BConfirmationUrl);

		var client = await GetClientAsync(ct);

		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed", // Buy Goods + Paybill safe mode
			ValidationURL = validationUrl,
			ConfirmationURL = confirmationUrl
		};

		var response = await client.PostAsJsonAsync(
			"/mpesa/c2b/v2/registerurl",
			payload,
			ct);

		var result = await response.Content
			.ReadFromJsonAsync<C2BRegisterResponse>(cancellationToken: ct);

		if (!response.IsSuccessStatusCode || result == null)
		{
			_logger.LogError("C2B URL registration failed {Status}", response.StatusCode);
			return DarajaResult<C2BRegisterResponse>.Fail("Registration failed");
		}

		_logger.LogInformation("C2B URLs registered for {SC}", shortCode);
		return DarajaResult<C2BRegisterResponse>.Ok(result);
	}

	// ─────────────────────────────────────────────
	// VALIDATION (Paybill only / optional for Buy Goods)
	// ─────────────────────────────────────────────

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		var refValue = request.BillRefNumber?.Trim();

		if (string.IsNullOrWhiteSpace(refValue))
		{
			_logger.LogWarning("C2B Validation missing BillRefNumber");
			return Accept(); // allow Buy Goods / flexible mode
		}

		var validRefs = _cfg.Tills
			.Select(x => x.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (!validRefs.Contains(refValue))
		{
			_logger.LogWarning("C2B Validation rejected ref {Ref}", refValue);
			return Reject("Invalid account reference");
		}

		_logger.LogInformation("C2B Validation accepted {Ref}", refValue);
		return Accept();
	}

	// ─────────────────────────────────────────────
	// CONFIRMATION (MAIN LEDGER ENTRY POINT)
	// ─────────────────────────────────────────────

	public async Task HandleConfirmationAsync(
		C2BConfirmationRequest request,
		CancellationToken ct = default)
	{
		var till = ResolveTill(request);

		var transactionId = request.TransactionId;

		// ── IDEMPOTENCY (CRITICAL)
		var exists = await _context.MpesaTransactions
			.AnyAsync(x => x.TransID == transactionId, ct);

		if (exists)
		{
			_logger.LogWarning("Duplicate C2B ignored {Id}", transactionId);
			return;
		}

		var amount = decimal.TryParse(request.TransAmount, out var amt) ? amt : 0;

		var transaction = new MpesaTransaction
		{
			TransactionType = "C2B",
			TransID = transactionId,
			MpesaReceiptNumber = transactionId,

			BusinessShortCode = request.BusinessShortCode ?? _cfg.C2BShortCode,

			TillNumber = till?.TillNumber ?? request.BusinessShortCode ?? "UNKNOWN",
			TillName = till?.Name ?? "UNMAPPED_TILL",

			TransAmount = amount,
			UsageBalance = amount,

			TransTime = TryParseDate(request.TransTime),

			MSISDN = request.PhoneNumber,

			FirstName = request.FirstName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,
			MiddName = request.MiddleName ?? string.Empty,

			OrgAccountBalance = decimal.TryParse(request.OrgAccountBalance, out var bal) ? bal : 0,

			Status = 1,
			UserCode = "Mpesa",

			DateCreated = DateTime.UtcNow.AddHours(3),
			DateModified = DateTime.UtcNow.AddHours(3),
			DateTimeStamp = DateTime.UtcNow.AddHours(3)
		};

		_context.MpesaTransactions.Add(transaction);
		await _context.SaveChangesAsync(ct);

		_logger.LogInformation(
			"C2B SAVED ✓ {Id} Amount={Amount} Till={Till}",
			transactionId, amount, till?.Name ?? "UNKNOWN");
	}

	// ─────────────────────────────────────────────
	// TILL RESOLUTION (BUY GOODS + PAYBILL SAFE)
	// ─────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		var refValue = request.BillRefNumber?.Trim();

		if (!string.IsNullOrWhiteSpace(refValue))
		{
			var byRef = _cfg.Tills.FirstOrDefault(x =>
				x.AccountReference.Equals(refValue, StringComparison.OrdinalIgnoreCase));

			if (byRef != null) return byRef;
		}

		// Buy Goods fallback
		var byTill = _cfg.Tills.FirstOrDefault(x =>
			x.TillNumber == request.BusinessShortCode);

		return byTill;
	}

	// ─────────────────────────────────────────────
	// HELPERS
	// ─────────────────────────────────────────────

	private async Task<HttpClient> GetClientAsync(CancellationToken ct)
	{
		var token = await _tokenService.GetAccessTokenAsync(ct);
		var client = _httpFactory.CreateClient("Daraja");

		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);

		return client;
	}

	private static DateTime TryParseDate(string value)
	{
		if (DateTime.TryParse(value, out var dt))
			return dt;

		return DateTime.UtcNow;
	}

	private static C2BValidationResponse Accept() =>
		new() { ResultCode = "0", ResultDesc = "Accepted" };

	private static C2BValidationResponse Reject(string reason) =>
		new() { ResultCode = "C2B00011", ResultDesc = reason };

	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;

		var uri = new Uri(url);
		return $"{uri.Scheme.ToLower()}://{uri.Host.ToLower()}{uri.AbsolutePath.Replace("//", "/")}{uri.Query}";
	}
}
public interface IC2BService
{
	/// <summary>
	/// Registers validation and confirmation URLs for a given shortcode.
	/// Must be called once per shortcode (or after URL changes).
	/// </summary>
	Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default);

	/// <summary>
	/// Registers C2B URLs for the master shortcode once.
	/// A single registration covers all tills under the same head-office shortcode.
	/// </summary>
	Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(
		CancellationToken ct = default);

	/// <summary>
	/// Validates an incoming C2B payment request.
	/// Return a rejection response to block fraudulent or unknown transactions.
	/// </summary>
	C2BValidationResponse Validate(C2BValidationRequest request);

	/// <summary>
	/// Persists a confirmed C2B payment to the ledger.
	/// </summary>
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}