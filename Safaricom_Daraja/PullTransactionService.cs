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
	Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber, DateTime from, DateTime to, int offset = 0, CancellationToken ct = default);

	Task<DarajaResult<List<PullTransaction>>> PullAllPagesAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default);
}

public sealed class PullTransactionService(
	IHttpClientFactory httpFactory,
	IOptions<DarajaConfig> options,
	IDarajaTokenService tokenService,
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

			var client = httpFactory.CreateClient("Daraja");
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

			// Fix reference to internal collection here
			var count = result?.Transactions?.Count ?? 0;
			logger.LogInformation("Pulled {Count} transactions for {Till} | offset={Offset}", count, tillNumber, offset);

			return DarajaResult<PullTransactionResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull failed for {Till}", tillNumber);
			return DarajaResult<PullTransactionResponse>.Fail(ex.Message);
		}
	}

	public async Task<DarajaResult<List<PullTransaction>>> PullAllPagesAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default)
	{
		var allTransactions = new List<PullTransaction>();
		var offset = 0;

		while (true)
		{
			var result = await PullAsync(tillNumber, from, to, offset, ct);
			if (!result.Success)
			{
				return DarajaResult<List<PullTransaction>>.Fail(result.ErrorMessage!);
			}

			// Extract the flat list from the page wrapper
			var pageTransactions = result.Data?.Transactions ?? [];
			allTransactions.AddRange(pageTransactions);

			// If we got less than full page, we've hit the end of the line
			if (pageTransactions.Count < PageSize)
			{
				break;
			}

			offset += PageSize;
			await Task.Delay(300, ct); // Avoid hammering the gateway rate limit
		}

		logger.LogInformation("Total pulled for {Till}: {Count} transactions", tillNumber, allTransactions.Count);
		return DarajaResult<List<PullTransaction>>.Ok(allTransactions);
	}

	private static void ValidateWindow(DateTime from, DateTime to)
	{
		if (to <= from) throw new ArgumentException("'to' must be after 'from'.");
		if ((to - from).TotalHours > 48) throw new ArgumentException("Daraja Pull API allows a maximum window of 48 hours.");
	}
}