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

}
public sealed class PullTransactionService(
	IHttpClientFactory httpFactory,
	IOptions<DarajaConfig> options,
	IDarajaTokenService tokenService, // 1. Inject your token service here
	ILogger<PullTransactionService> logger) : IPullTransactionService
{
	private readonly DarajaConfig _cfg = options.Value;
	private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
	private const int PageSize = 100;

	public async Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber, DateTime from, DateTime to, int offset = 0, CancellationToken ct = default)
	{
		try
		{
			ValidateWindow(from, to);
			var payload = new PullTransactionRequest
			{
				ShortCode = tillNumber,
				StartDate = from.ToString(DateFormat),
				EndDate = to.ToString(DateFormat),
				OffSetValue = offset
			};

			// 2. Get client and dynamically apply the Bearer token
			var client = httpFactory.CreateClient("Daraja");

			// Assuming your IDarajaTokenService has a method like GetTokenAsync() 
			// that handles caching and returns the string token.
			var accessToken = await tokenService.GetAccessTokenAsync(ct);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.PostAsJsonAsync("/pulltransactions/v1/query", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("Pull failed [{StatusCode}] for {Till}: {Error}", response.StatusCode, tillNumber, error);
				return DarajaResult<PullTransactionResponse>.Fail(error);
			}

			var raw = await response.Content.ReadAsStringAsync(ct);
			var result = JsonSerializer.Deserialize<PullTransactionResponse>(raw);
			logger.LogInformation("Pulled {Count} transactions for {Till} | offset={Offset}", result?.Transactions.Count ?? 0, tillNumber, offset);
			return DarajaResult<PullTransactionResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull failed for {Till}", tillNumber);
			return DarajaResult<PullTransactionResponse>.Fail(ex.Message);
		}
	}

	// ... Keep PullAllPagesAsync and PullAllTillsAsync exactly as they are ...

	private static void ValidateWindow(DateTime from, DateTime to)
	{
		if (to <= from) throw new ArgumentException("'to' must be after 'from'.");
		if ((to - from).TotalHours > 48) throw new ArgumentException("Daraja Pull API allows a maximum window of 48 hours.");
	}

	// 3. You can safely delete the GetBasicAuthClient() method entirely
}