// ═══════════════════════════════════════════════════════════════
// IC2BService.cs  —  Public contract
// ═══════════════════════════════════════════════════════════════

using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Daraja.Services;

public interface IC2BService
{
	/// <summary>Registers C2B URLs for the master shortcode.</summary>
	Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default);

	/// <summary>Registers validation and confirmation URLs for a given shortcode.</summary>
	Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default);

	/// <summary>Registers all configured tills individually (required for Buy Goods).</summary>
	Task RegisterAllTillsAsync(CancellationToken ct = default);

	/// <summary>Validates an incoming C2B payment. Returns Reject() to block unknown transactions.</summary>
	C2BValidationResponse Validate(C2BValidationRequest request);

	/// <summary>Persists a confirmed C2B payment to the ledger.</summary>
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}


// ═══════════════════════════════════════════════════════════════
// C2BService.cs  —  Thin orchestrator
// ═══════════════════════════════════════════════════════════════

public sealed class C2BService : IC2BService
{
	private readonly C2BRegistrar _registrar;
	private readonly C2BValidator _validator;
	private readonly C2BConfirmationHandler _confirmationHandler;
	private readonly IOptionsMonitor<DarajaConfig> _options;

	public C2BService(
		C2BRegistrar registrar,
		C2BValidator validator,
		C2BConfirmationHandler confirmationHandler,
		IOptionsMonitor<DarajaConfig> options)
	{
		_registrar = registrar;
		_validator = validator;
		_confirmationHandler = confirmationHandler;
		_options = options;
	}

	public Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(CancellationToken ct = default)
		=> _registrar.RegisterAsync(_options.CurrentValue.C2BShortCode, ct);

	public Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode, CancellationToken ct = default)
		=> _registrar.RegisterAsync(shortCode, ct);

	public Task RegisterAllTillsAsync(CancellationToken ct = default)
		=> _registrar.RegisterAllTillsAsync(ct);

	public C2BValidationResponse Validate(C2BValidationRequest request)
		=> _validator.Validate(request);

	public Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
		=> _confirmationHandler.HandleAsync(request, ct);
}


// ═══════════════════════════════════════════════════════════════
// C2BRegistrar.cs  —  URL registration only
// ═══════════════════════════════════════════════════════════════

public sealed class C2BRegistrar
{
	private readonly IHttpClientFactory _httpFactory;
	private readonly IDarajaTokenService _tokenService;
	private readonly IOptionsMonitor<DarajaConfig> _options;
	private readonly ILogger<C2BRegistrar> _logger;

	public C2BRegistrar(
		IHttpClientFactory httpFactory,
		IDarajaTokenService tokenService,
		IOptionsMonitor<DarajaConfig> options,
		ILogger<C2BRegistrar> logger)
	{
		_httpFactory = httpFactory;
		_tokenService = tokenService;
		_options = options;
		_logger = logger;
	}

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterAsync(
		string shortCode,
		CancellationToken ct = default)
	{
		var cfg = _options.CurrentValue;
		var client = await BuildAuthenticatedClientAsync(ct);

		var payload = new C2BRegisterRequest
		{
			ShortCode = shortCode,
			ResponseType = "Completed",
			ValidationURL = SanitizeUrl(cfg.C2BValidationUrl),
			ConfirmationURL = SanitizeUrl(cfg.C2BConfirmationUrl),
		};

		var response = await client.PostAsJsonAsync("/mpesa/c2b/v2/registerurl", payload, ct);
		var result = await response.Content.ReadFromJsonAsync<C2BRegisterResponse>(cancellationToken: ct);

		if (!response.IsSuccessStatusCode || result is null)
		{
			_logger.LogError(
				"C2B URL registration failed. ShortCode={ShortCode} Status={Status}",
				shortCode, response.StatusCode);
			return DarajaResult<C2BRegisterResponse>.Fail("Registration failed");
		}

		_logger.LogInformation("C2B URLs registered. ShortCode={ShortCode}", shortCode);
		return DarajaResult<C2BRegisterResponse>.Ok(result);
	}

	/// <summary>
	/// Registers each configured till individually.
	/// For Buy Goods, each till number must be registered as its own shortcode.
	/// </summary>
	public async Task RegisterAllTillsAsync(CancellationToken ct = default)
	{
		var tills = _options.CurrentValue.Tills;

		foreach (var till in tills)
		{
			var result = await RegisterAsync(till.TillNumber, ct);
			if (!result.Success)
				_logger.LogWarning(
					"Till registration failed. Till={Till} Name={Name}",
					till.TillNumber, till.Name);
		}
	}

	// ── Helpers ──────────────────────────────────────────────────

	private async Task<HttpClient> BuildAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await _tokenService.GetAccessTokenAsync(ct);
		var client = _httpFactory.CreateClient("Daraja");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		return client;
	}

	/// <summary>
	/// Normalises scheme and host to lowercase and collapses any double slashes in the path.
	/// Railway-generated URLs can occasionally include these artefacts.
	/// </summary>
	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;
		var uri = new Uri(url);
		return $"{uri.Scheme.ToLower()}://{uri.Host.ToLower()}{uri.AbsolutePath.Replace("//", "/")}{uri.Query}";
	}
}


// ═══════════════════════════════════════════════════════════════
// C2BValidator.cs  —  Validation logic only
// ═══════════════════════════════════════════════════════════════

public sealed class C2BValidator
{
	private readonly IOptionsMonitor<DarajaConfig> _options;
	private readonly ILogger<C2BValidator> _logger;

	public C2BValidator(IOptionsMonitor<DarajaConfig> options, ILogger<C2BValidator> logger)
	{
		_options = options;
		_logger = logger;
	}

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		var billRef = request.BillRefNumber?.Trim();

		// Buy Goods — no BillRefNumber expected; accept immediately.
		if (string.IsNullOrWhiteSpace(billRef))
		{
			_logger.LogInformation("C2B Validation accepted (Buy Goods / no BillRefNumber)");
			return Accept();
		}

		// Paybill — validate BillRefNumber against known account references.
		// ValidRefs is derived from current config on each call so hot-reloaded
		// tills are picked up without restarting the service.
		var validRefs = BuildValidRefs();

		if (!validRefs.Contains(billRef))
		{
			_logger.LogWarning(
				"C2B Validation rejected unknown ref. BillRef={BillRef}", billRef);
			return Reject("Invalid account reference");
		}

		_logger.LogInformation("C2B Validation accepted. BillRef={BillRef}", billRef);
		return Accept();
	}

	// ── Helpers ──────────────────────────────────────────────────

	/// <summary>
	/// Builds the valid-refs set from the current config snapshot.
	/// HashSet construction is O(n) on the number of tills — negligible
	/// compared to the inbound HTTP round-trip, and keeps the validator
	/// correct when tills are added or removed without a service restart.
	/// If your till list is large and allocation pressure matters, switch to
	/// <see cref="IOptionsMonitor{T}.OnChange"/> to invalidate a cached set.
	/// </summary>
	private HashSet<string> BuildValidRefs() =>
		_options.CurrentValue.Tills
			.Select(x => x.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

	private static C2BValidationResponse Accept() =>
		new() { ResultCode = "0", ResultDesc = "Accepted" };

	private static C2BValidationResponse Reject(string reason) =>
		new() { ResultCode = "C2B00011", ResultDesc = reason };
}


// ═══════════════════════════════════════════════════════════════
// C2BConfirmationHandler.cs  —  Ledger persistence only
// ═══════════════════════════════════════════════════════════════

public sealed class C2BConfirmationHandler
{
	// EAT = UTC+3. No DST observed — offset is constant year-round.
	private static readonly TimeZoneInfo _eat =
		TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");

	private readonly OTOContext _context;
	private readonly IOptionsMonitor<DarajaConfig> _options;
	private readonly ILogger<C2BConfirmationHandler> _logger;

	public C2BConfirmationHandler(
		OTOContext context,
		IOptionsMonitor<DarajaConfig> options,
		ILogger<C2BConfirmationHandler> logger)
	{
		_context = context;
		_options = options;
		_logger = logger;
	}

	public async Task HandleAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		_logger.LogInformation(
			"C2B Confirmation received. TransID={TransID} ShortCode={ShortCode} Phone={Phone}",
			request.TransactionId, request.BusinessShortCode, request.PhoneNumber);

		try
		{
			// ── Idempotency guard ──────────────────────────────────
			if (await IsDuplicateAsync(request.TransactionId, ct))
			{
				_logger.LogWarning(
					"C2B duplicate ignored. TransID={TransID}", request.TransactionId);
				return;
			}

			var till = ResolveTill(request);
			var transaction = BuildTransaction(request, till);

			_context.MpesaTransactions.Add(transaction);
			await _context.SaveChangesAsync(ct);

			_logger.LogInformation(
				"C2B saved. TransID={TransID} Amount={Amount} Till={Till}",
				request.TransactionId, transaction.TransAmount, till?.Name ?? "UNKNOWN");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex,
				"C2B processing failed. TransID={TransID}", request.TransactionId);

			// Re-throw so the caller / middleware returns a non-200,
			// which causes M-Pesa to retry the confirmation callback.
			throw;
		}
	}

	// ── Till resolution ───────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		var cfg = _options.CurrentValue;
		var billRef = request.BillRefNumber?.Trim();

		// Paybill: match on BillRefNumber / AccountReference.
		if (!string.IsNullOrWhiteSpace(billRef))
		{
			var byRef = cfg.Tills.FirstOrDefault(t =>
				t.AccountReference.Equals(billRef, StringComparison.OrdinalIgnoreCase));

			if (byRef is not null) return byRef;
		}

		// Buy Goods: M-Pesa sends the till number as BusinessShortCode.
		var byTill = cfg.Tills.FirstOrDefault(t =>
			string.Equals(t.TillNumber, request.BusinessShortCode, StringComparison.OrdinalIgnoreCase));

		if (byTill is null)
			_logger.LogWarning(
				"No till mapped. BusinessShortCode={ShortCode} BillRef={BillRef} TransType={TransType}",
				request.BusinessShortCode, billRef, request.TransactionType);

		return byTill;
	}

	// ── Entity builder ────────────────────────────────────────────

	private MpesaTransaction BuildTransaction(C2BConfirmationRequest request, TillConfig? till)
	{
		var cfg = _options.CurrentValue;
		var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _eat);

		return new MpesaTransaction
		{
			TransactionType = "C2B",
			TransID = request.TransactionId,
			MpesaReceiptNumber = request.TransactionId,

			BusinessShortCode = request.BusinessShortCode ?? cfg.C2BShortCode,
			TillNumber = till?.TillNumber ?? request.BusinessShortCode ?? "UNKNOWN",
			TillName = till?.Name ?? "UNMAPPED_TILL",

			TransAmount = ParseDecimal(request.TransAmount),
			UsageBalance = ParseDecimal(request.TransAmount),
			OrgAccountBalance = ParseDecimal(request.OrgAccountBalance),

			TransTime = ParseTransTime(request.TransTime),

			MSISDN = request.PhoneNumber,
			FirstName = request.FirstName ?? string.Empty,
			MiddName = request.MiddleName ?? string.Empty,
			LastName = request.LastName ?? string.Empty,

			Status = 1,
			UserCode = "Mpesa",
			PaymentMethod = "C2B",

			CheckoutRequestID = string.Empty,
			MerchantRequestID = string.Empty,

			DateCreated = now,
			DateModified = now,
			DateTimeStamp = now,
		};
	}

	// ── Private helpers ───────────────────────────────────────────

	private Task<bool> IsDuplicateAsync(string transactionId, CancellationToken ct) =>
		_context.MpesaTransactions.AnyAsync(x => x.TransID == transactionId, ct);

	private static decimal ParseDecimal(string? value) =>
		decimal.TryParse(value, out var result) ? result : 0m;

	/// <summary>
	/// Parses M-Pesa's non-standard TransTime format "yyyyMMddHHmmss".
	/// <c>DateTime.TryParse</c> cannot handle this format — explicit
	/// <c>TryParseExact</c> with <see cref="System.Globalization.CultureInfo.InvariantCulture"/>
	/// is required.
	/// </summary>
	private static DateTime ParseTransTime(string? value)
	{
		if (value is not null &&
			DateTime.TryParseExact(
				value,
				"yyyyMMddHHmmss",
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None,
				out var dt))
		{
			return dt;
		}

		return DateTime.UtcNow;
	}
}


// ═══════════════════════════════════════════════════════════════
// C2BRegistrationStartupService.cs
// Automatically registers all tills on application startup.
// Wire up in Program.cs: builder.Services.AddHostedService<C2BRegistrationStartupService>();
// ═══════════════════════════════════════════════════════════════

