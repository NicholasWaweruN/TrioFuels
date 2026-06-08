using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.DarajaTokenService;
using Serilog;

namespace Daraja.Services;

public interface IC2BService
{
	/// <summary>
	/// Registers validation and confirmation URLs for a till shortcode.
	/// Must be called once per shortcode (or after URL changes).
	/// </summary>
	Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default);

	/// <summary>
	/// Registers all configured tills at once.
	/// </summary>
	Task<Dictionary<string, DarajaResult<C2BRegisterResponse>>> RegisterAllTillsAsync(
		CancellationToken ct = default);

	/// <summary>
	/// Processes a C2B validation request.
	/// Return a rejection response to block fraudulent transactions.
	/// </summary>
	C2BValidationResponse Validate(C2BValidationRequest request);

	/// <summary>
	/// Processes a confirmed C2B payment.
	/// Persist to your database here.
	/// </summary>
	Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default);
}

public sealed class C2BService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<C2BService> logger) : IC2BService
{
	private readonly DarajaConfig _cfg = options.Value;

	public async Task<DarajaResult<C2BRegisterResponse>> RegisterUrlsAsync(
		string shortCode,
		CancellationToken ct = default)
	{
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
			var response = await client.PostAsJsonAsync(
				"/mpesa/c2b/v1/registerurl", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("C2B register failed [{StatusCode}] for {ShortCode}: {Error}",
					response.StatusCode, shortCode, error);
				return DarajaResult<C2BRegisterResponse>.Fail(error);
			}

			var result = await response.Content.ReadFromJsonAsync<C2BRegisterResponse>(
				cancellationToken: ct);

			logger.LogInformation("C2B URLs registered for shortcode {ShortCode}: {Desc}",
				shortCode, result?.ResponseDescription);

			return DarajaResult<C2BRegisterResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "C2B URL registration failed for {ShortCode}", shortCode);
			return DarajaResult<C2BRegisterResponse>.Fail(ex.Message);
		}
	}

	public async Task<Dictionary<string, DarajaResult<C2BRegisterResponse>>> RegisterAllTillsAsync(
		CancellationToken ct = default)
	{
		var results = new Dictionary<string, DarajaResult<C2BRegisterResponse>>();

		foreach (var till in _cfg.Tills)
		{
			logger.LogInformation("Registering C2B URLs for till: {Name} ({TillNumber})",
				till.Name, till.TillNumber);

			results[till.TillNumber] = await RegisterUrlsAsync(till.TillNumber, ct);

			// Safaricom rate limit — avoid hammering the API
			await Task.Delay(500, ct);
		}

		return results;
	}

	public C2BValidationResponse Validate(C2BValidationRequest request)
	{
		// ── Custom validation logic ───────────────────────────────────────────
		// Accept if the bill reference matches one of your configured till references
		var knownRefs = _cfg.Tills.Select(t => t.AccountReference).ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (!string.IsNullOrWhiteSpace(request.BillRefNumber)
			&& !knownRefs.Contains(request.BillRefNumber))
		{
			logger.LogWarning(
				"C2B Validation REJECTED — unknown BillRefNumber '{Ref}' from {Phone}",
				request.BillRefNumber, request.PhoneNumber);

			return new C2BValidationResponse
			{
				ResultCode = "C2B00011",
				ResultDesc = "Rejected — unknown account reference"
			};
		}

		logger.LogInformation(
			"C2B Validation ACCEPTED — TransID={TransId} Amount={Amount} Phone={Phone} Ref={Ref}",
			request.TransactionId, request.TransAmount, request.PhoneNumber, request.BillRefNumber);

		return new C2BValidationResponse { ResultCode = "0", ResultDesc = "Accepted" };
	}

	public async Task HandleConfirmationAsync(C2BConfirmationRequest request, CancellationToken ct = default)
	{
		// ── Persist the payment ───────────────────────────────────────────────
		// TODO: Inject your IRepository/DbContext and save the transaction.
		// Identify which till received the money via BillRefNumber or BusinessShortCode.

		var till = _cfg.Tills.FirstOrDefault(t =>
			t.TillNumber == request.BusinessShortCode ||
			string.Equals(t.AccountReference, request.BillRefNumber, StringComparison.OrdinalIgnoreCase));

		logger.LogInformation("C2B CONFIRMED — TransID={TransId} | Amount=KES {Amount} | Phone={Phone} | " +
			"Till={Till} | Ref={Ref} | Time={Time}",
			request.TransactionId,
			request.TransAmount,
			request.PhoneNumber,
			till?.Name ?? request.BusinessShortCode,
			request.BillRefNumber,
			request.TransTime);

		// Example: await _repo.SaveTransactionAsync(new Payment { ... }, ct);

		await Task.CompletedTask;
	}

	private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken ct)
	{
		var token = await tokenService.GetAccessTokenAsync(ct);
		var client = httpFactory.CreateClient("FuelFlow");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		return client;
	}
}