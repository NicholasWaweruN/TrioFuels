using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;

namespace Daraja.Services;

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
	/// Persists a confirmed C2B payment.
	/// </summary>
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}

public sealed class C2BService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<C2BService> logger
// TODO: inject your repo — e.g. IPaymentRepository paymentRepo
) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;

	// ── URL Registration ──────────────────────────────────────────────────────

	/// <inheritdoc/>
	public async Task<DarajaResult<C2BRegisterResponse>> RegisterMasterShortCodeAsync(
		CancellationToken ct = default)
	{
		// C2B URL registration is at the shortcode level, not per-till.
		// A single registration on 4161705 covers all tills under it.
		return await RegisterUrlsAsync(_cfg.C2BShortCode, ct);
	}

	/// <inheritdoc/>
	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		// ── FIX 1: Sanitize URLs — strip double slashes, normalise to lowercase ──
		// appsettings had: https://triofuels-production.up.railway.app//fuelflow/Daraja/c2b/validate
		//                                                               ^^ double slash + capital D
		var validationUrl = SanitizeUrl(_cfg.C2BValidationUrl);
		var confirmationUrl = SanitizeUrl(_cfg.C2BConfirmationUrl);

		logger.LogInformation(
			"C2B RegisterUrls — ShortCode={SC} ValidationUrl={V} ConfirmationUrl={C}",
			shortCode, validationUrl, confirmationUrl);

		try
		{
			var payload = new C2BRegisterRequest
			{
				ShortCode = shortCode,

				// "Completed" = Safaricom auto-accepts all payments without calling ValidationURL.
				// "Cancelled" requires External Validation to be enabled on shortcode 4161705
				// by Safaricom backend — email apisupport@safaricom.co.ke to request it.
				// Until then, only "Completed" is accepted in production.
				ResponseType = "Completed",

				ValidationURL = validationUrl,
				ConfirmationURL = confirmationUrl
			};

			// ── FIX 3: Use v2 endpoint — your portal shows C2B v2 ────────────────
			// v1: /mpesa/c2b/v1/registerurl  ← old, not approved for this app
			// v2: /mpesa/c2b/v2/registerurl  ← matches your Daraja app products
			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/c2b/v2/registerurl", payload, ct);
			var content = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError(
					"C2B RegisterUrls failed [{Status}] ShortCode={SC} Response={R}",
					(int)response.StatusCode, shortCode, content);
				return DarajaResult<C2BRegisterResponse>.Fail(content);
			}

			var result = await response.Content.ReadFromJsonAsync<C2BRegisterResponse>(
				cancellationToken: ct);

			logger.LogInformation(
				"C2B URLs registered ✅ ShortCode={SC} Desc={Desc}",
				shortCode, result?.ResponseDescription);

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "C2B RegisterUrls threw — ShortCode={SC}", shortCode);
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		// Reject if BillRefNumber is missing — don't accept blind payments.
		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning(
				"C2B Validation REJECTED — missing BillRefNumber Phone={Phone} TransID={Id}",
				request.PhoneNumber, request.TransactionId);
			return Rejected("C2B00011", "Rejected — missing account reference");
		}

		var knownRefs = _cfg.Tills
			.Select(t => t.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (!knownRefs.Contains(request.BillRefNumber))
		{
			logger.LogWarning(
				"C2B Validation REJECTED — unknown BillRefNumber='{Ref}' Phone={Phone} TransID={Id}",
				request.BillRefNumber, request.PhoneNumber, request.TransactionId);
			return Rejected("C2B00011", "Rejected — unknown account reference");
		}

		logger.LogInformation(
			"C2B Validation ACCEPTED — TransID={Id} Amount={Amount} Phone={Phone} Ref={Ref}",
			request.TransactionId, request.TransAmount, request.PhoneNumber, request.BillRefNumber);

		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public async Task HandleConfirmationAsync(
		C2BConfirmationRequest request,
		CancellationToken ct = default)
	{
		var till = ResolveTill(request);

		if (till is null)
		{
			// Confirmed by Safaricom but not matched to any known till.
			// Never silently drop — log a warning and persist to unmatched queue.
			logger.LogWarning(
				"C2B CONFIRMED but no matching till — ShortCode={SC} Ref={Ref} " +
				"TransID={Id} Amount=KES {Amount} Phone={Phone}",
				request.BusinessShortCode, request.BillRefNumber,
				request.TransactionId, request.TransAmount, request.PhoneNumber);

			// TODO: persist to an unmatched_transactions table
			// await _paymentRepo.SaveUnmatchedAsync(request, ct);
			return;
		}

		logger.LogInformation(
			"C2B CONFIRMED ✅ — TransID={Id} Amount=KES {Amount} Phone={Phone} " +
			"Till={TillName} ({TillNumber}) Ref={Ref} Time={Time}",
			request.TransactionId, request.TransAmount, request.PhoneNumber,
			till.Name, till.TillNumber, request.BillRefNumber, request.TransTime);

		// TODO: map to your domain entity and persist
		// var payment = new Payment
		// {
		//     TransactionId = request.TransactionId,
		//     Amount        = request.TransAmount,
		//     PhoneNumber   = request.PhoneNumber,
		//     TillId        = till.Id,
		//     BillRefNumber = request.BillRefNumber,
		//     TransactedAt  = request.TransTime,
		//     Channel       = PaymentChannel.C2B
		// };
		// await _paymentRepo.SaveAsync(payment, ct);

		await Task.CompletedTask; // remove once repo is injected
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		// 1. Match by BillRefNumber (AccountReference) — most explicit.
		//    Customer types e.g. "TILL_5617668" as the account reference.
		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var byRef = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.AccountReference, request.BillRefNumber,
					StringComparison.OrdinalIgnoreCase));

			if (byRef is not null) return byRef;
		}

		// ── FIX 4: C2B confirmation sends BusinessShortCode = head-office (4161705) ──
		// The old code compared request.BusinessShortCode == till.TillNumber
		// which would NEVER match because Safaricom sends the HO shortcode, not the till.
		// Correct fallback: if the incoming ShortCode matches our registered C2B shortcode,
		// we know it's ours but we can't pinpoint which till — return null so it goes
		// to the unmatched queue rather than being silently assigned to the wrong till.
		if (request.BusinessShortCode == _cfg.C2BShortCode ||
			request.BusinessShortCode == _cfg.BusinessShortCode)
		{
			logger.LogWarning(
				"C2B ResolveTill — ShortCode matched HO {SC} but BillRefNumber '{Ref}' " +
				"did not match any till AccountReference. Routing to unmatched.",
				request.BusinessShortCode, request.BillRefNumber);
		}

		return null;
	}

	/// <summary>
	/// Removes double slashes in path and normalises scheme+host to lowercase.
	/// Fixes: https://host.com//path/Daraja/... → https://host.com/path/daraja/...
	/// </summary>
	private static string SanitizeUrl(string url)
	{
		if (string.IsNullOrWhiteSpace(url)) return url;

		var uri = new Uri(url);
		var path = uri.AbsolutePath
			.Replace("//", "/")        // remove double slash
			.ToLowerInvariant();       // normalise case

		var query = string.IsNullOrEmpty(uri.Query) ? "" : uri.Query;
		return $"{uri.Scheme.ToLowerInvariant()}://{uri.Host.ToLowerInvariant()}{path}{query}";
	}

	private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);
		var client = httpFactory.CreateClient("Daraja");
		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token);
		return client;
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}