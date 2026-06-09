using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;

namespace Safaricom_Daraja;

public interface IPullTransactionService
{
	/// <summary>
	/// Pulls transactions for a specific till within a time window.
	/// Daraja allows a max window of 48 hours and returns up to 100 records per page.
	/// </summary>
	Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber,
		DateTime from,
		DateTime to,
		int offset = 0,
		CancellationToken ct = default);

	/// <summary>
	/// Pulls ALL pages for a till within the window, handling pagination automatically.
	/// </summary>
	Task<DarajaResult<List<PullTransaction>>> PullAllPagesAsync(
		string tillNumber,
		DateTime from,
		DateTime to,
		CancellationToken ct = default);

	/// <summary>
	/// Pulls transactions across ALL configured tills for the given window.
	/// </summary>
	Task<Dictionary<string, DarajaResult<List<PullTransaction>>>> PullAllTillsAsync(
		DateTime from,
		DateTime to,
		CancellationToken ct = default);
}

public sealed class PullTransactionService(
	IHttpClientFactory httpFactory,
	IOptions<DarajaConfig> options,
	ILogger<PullTransactionService> logger) : IPullTransactionService
{
	private readonly DarajaConfig _cfg = options.Value;
	private const string DateFormat = "yyyyMMddHHmmss";
	private const int PageSize = 100;

	public async Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber,
		DateTime from,
		DateTime to,
		int offset = 0,
		CancellationToken ct = default)
	{
		try
		{
			ValidateWindow(from, to);

			var payload = new PullTransactionRequest
			{
				ShortCode = tillNumber,
				StartDate = from.ToString(DateFormat),
				EndDate = to.ToString(DateFormat),
				Offset = offset
			};

			var client = GetBasicAuthClient();
			var response = await client.PostAsJsonAsync("/pullpayments/v1/query", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("Pull failed [{StatusCode}] for {Till}: {Error}",
					response.StatusCode, tillNumber, error);
				return DarajaResult<PullTransactionResponse>.Fail(error);
			}

			var raw = await response.Content.ReadAsStringAsync(ct);
			var result = JsonSerializer.Deserialize<PullTransactionResponse>(raw);

			logger.LogInformation(
				"Pulled {Count} transactions for {Till} | offset={Offset}",
				result?.Transactions.Count ?? 0, tillNumber, offset);

			return DarajaResult<PullTransactionResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull failed for {Till}", tillNumber);
			return DarajaResult<PullTransactionResponse>.Fail(ex.Message);
		}
	}

	public async Task<DarajaResult<List<PullTransaction>>> PullAllPagesAsync(
		string tillNumber,
		DateTime from,
		DateTime to,
		CancellationToken ct = default)
	{
		var all = new List<PullTransaction>();
		var offset = 0;

		while (true)
		{
			var result = await PullAsync(tillNumber, from, to, offset, ct);
			if (!result.Success)
				return DarajaResult<List<PullTransaction>>.Fail(result.ErrorMessage!);

			var page = result.Data!.Transactions;
			all.AddRange(page);

			if (page.Count < PageSize) break;

			offset += PageSize;
			await Task.Delay(300, ct);
		}

		logger.LogInformation("Total pulled for {Till}: {Count} transactions", tillNumber, all.Count);
		return DarajaResult<List<PullTransaction>>.Ok(all);
	}

	public async Task<Dictionary<string, DarajaResult<List<PullTransaction>>>> PullAllTillsAsync(
		DateTime from,
		DateTime to,
		CancellationToken ct = default)
	{
		var results = new Dictionary<string, DarajaResult<List<PullTransaction>>>();

		foreach (var till in _cfg.Tills)
		{
			logger.LogInformation("Pulling transactions for till: {Name} ({Number})",
				till.Name, till.TillNumber);

			results[till.TillNumber] = await PullAllPagesAsync(till.TillNumber, from, to, ct);

			await Task.Delay(500, ct);
		}

		return results;
	}

	// ─── Helpers ──────────────────────────────────────────────────────────────

	private static void ValidateWindow(DateTime from, DateTime to)
	{
		if (to <= from)
			throw new ArgumentException("'to' must be after 'from'.");

		if ((to - from).TotalHours > 48)
			throw new ArgumentException("Daraja Pull API allows a maximum window of 48 hours.");
	}

	/// <summary>
	/// Pull API uses Basic auth directly, NOT the OAuth Bearer token.
	/// </summary>
	private HttpClient GetBasicAuthClient()
	{
		var credentials = Convert.ToBase64String(
			Encoding.UTF8.GetBytes($"{_cfg.ConsumerKey}:{_cfg.ConsumerSecret}"));

		var client = httpFactory.CreateClient("Daraja");
		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Basic", credentials);
		return client;
	}
}