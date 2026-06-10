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
	/// If your org has multiple distinct shortcodes, loop over them at the call site.
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
		// A single registration covers all tills under the same shortcode.
		return await RegisterUrlsAsync(_cfg.BusinessShortCode, ct);
	}

	/// <inheritdoc/>
	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(string shortCode,CancellationToken ct = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(shortCode);

		try
		{
			var payload = new C2BRegisterRequest
			{
				ShortCode = shortCode,
				ResponseType = "Completed",
				ConfirmationURL = _cfg.C2BConfirmationUrl,
				ValidationURL = _cfg.C2BValidationUrl
			};

			var client = await GetAuthenticatedClientAsync(ct);
			var response = await client.PostAsJsonAsync("/mpesa/c2b/v1/registerurl", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("C2B register failed [{StatusCode}] for ShortCode={ShortCode}: {Error}",response.StatusCode, shortCode, error);

				return DarajaResult<C2BRegisterResponse>.Fail(error);
			}

			var result = await response.Content.ReadFromJsonAsync<C2BRegisterResponse>(cancellationToken: ct);

			logger.LogInformation("C2B URLs registered for ShortCode={ShortCode}: {Desc}",shortCode, result?.ResponseDescription);

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "C2B URL registration threw for ShortCode={ShortCode}", shortCode);
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	// ── Validation ────────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		// Reject immediately if BillRefNumber is missing — don't accept blind payments.
		if (string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			logger.LogWarning("C2B Validation REJECTED — missing BillRefNumber from Phone={Phone} TransID={TransId}",request.PhoneNumber, request.TransactionId);

			return Rejected("C2B00011", "Rejected — missing account reference");
		}

		var knownRefs = _cfg.Tills
			.Select(t => t.AccountReference)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (!knownRefs.Contains(request.BillRefNumber))
		{
			logger.LogWarning("C2B Validation REJECTED — unknown BillRefNumber='{Ref}' from Phone={Phone} TransID={TransId}",request.BillRefNumber, request.PhoneNumber, request.TransactionId);

			return Rejected("C2B00011", "Rejected — unknown account reference");
		}

		logger.LogInformation("C2B Validation ACCEPTED — TransID={TransId} Amount={Amount} Phone={Phone} Ref={Ref}",request.TransactionId, request.TransAmount, request.PhoneNumber, request.BillRefNumber);

		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	// ── Confirmation ──────────────────────────────────────────────────────────

	/// <inheritdoc/>
	public async Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		var till = ResolveTill(request);

		if (till is null)
		{
			// Confirmed by Safaricom but not matched to any known till.
			// Log a warning and route to an unmatched queue so money is never silently lost.
			logger.LogWarning("C2B CONFIRMED but no matching till — ShortCode={ShortCode} Ref={Ref} " +"TransID={TransId} Amount=KES {Amount} Phone={Phone}",request.BusinessShortCode, request.BillRefNumber,request.TransactionId, request.TransAmount, request.PhoneNumber);

			// TODO: persist to an unmatched_transactions table
			// await _paymentRepo.SaveUnmatchedAsync(request, ct);
			return;
		}

		logger.LogInformation("C2B CONFIRMED — TransID={TransId} | Amount=KES {Amount} | Phone={Phone} | " +"Till={TillName} ({TillNumber}) | Ref={Ref} | Time={Time}",request.TransactionId, request.TransAmount, request.PhoneNumber,till.Name, till.TillNumber, request.BillRefNumber, request.TransTime);

		// TODO: map to your domain entity and persist
		// var payment = new Payment
		// {
		//     TransactionId   = request.TransactionId,
		//     Amount          = request.TransAmount,
		//     PhoneNumber     = request.PhoneNumber,
		//     TillId          = till.Id,
		//     BillRefNumber   = request.BillRefNumber,
		//     TransactedAt    = request.TransTime,
		//     Channel         = PaymentChannel.C2B
		// };
		// await _paymentRepo.SaveAsync(payment, ct);

		await Task.CompletedTask; // remove once repo is injected
	}

	// ── Private helpers ───────────────────────────────────────────────────────

	private TillConfig? ResolveTill(C2BConfirmationRequest request)
	{
		// Prefer matching by AccountReference (BillRefNumber) first — it's the
		// most explicit identifier the customer typed. Fall back to BusinessShortCode
		// only when the ref is absent or ambiguous.
		if (!string.IsNullOrWhiteSpace(request.BillRefNumber))
		{
			var byRef = _cfg.Tills.FirstOrDefault(t =>
				string.Equals(t.AccountReference, request.BillRefNumber, StringComparison.OrdinalIgnoreCase));

			if (byRef is not null) return byRef;
		}

		return _cfg.Tills.FirstOrDefault(t => t.TillNumber == request.BusinessShortCode);
	}

	private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);
		var client = httpFactory.CreateClient("FuelFlow");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		return client;
	}

	private static C2BValidationResponse Rejected(string code, string desc) =>
		new() { ResultCode = code, ResultDesc = desc };
}