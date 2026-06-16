using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;

namespace Safaricom_Daraja;

public interface IPullTransactionService
{
	/// <summary>
	/// Pulls transactions for a specific till within a single offset window.
	/// </summary>
	Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber, DateTime from, DateTime to, int offset = 0, CancellationToken ct = default);

	/// <summary>
	/// Pulls ALL pages for a till within the window, handling pagination automatically.
	/// </summary>
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

			// FIX: Pass the Parent BusinessShortCode (Head Office 4161705) as the primary owner.
			// Map the child retail StoreNumber to target the specific terminal query.
			var payload = new PullTransactionRequest
			{
				ShortCode = _cfg.BusinessShortCode,
				StartDate = from.ToString(DateFormat),
				EndDate = to.ToString(DateFormat),
				OffSetValue = offset,
				StoreNumber = tillNumber
			};

			var client = httpFactory.CreateClient("Daraja");

			// FIX: Dynamic dynamic Bearer Token injection instead of basic auth headers
			var accessToken = await tokenService.GetAccessTokenAsync(ct);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.PostAsJsonAsync("/pulltransactions/v1/query", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("Pull failed [{StatusCode}] for Till {Till}: {Error}", response.StatusCode, tillNumber, error);
				return DarajaResult<PullTransactionResponse>.Fail(error);
			}

			var rawJson = await response.Content.ReadAsStringAsync(ct);
			var result = JsonSerializer.Deserialize<PullTransactionResponse>(rawJson);

			// Fixed compilation mapping error using internal collection indicators safely
			var count = result?.Transactions?.Count ?? 0;
			logger.LogInformation("Pulled {Count} transactions for Till {Till} | offset={Offset}", count, tillNumber, offset);

			return DarajaResult<PullTransactionResponse>.Ok(result!);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull execution blew up for Till {Till}", tillNumber);
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

			var pageTransactions = result.Data?.Transactions ?? [];
			allTransactions.AddRange(pageTransactions);

			// Break loop if we run out of pages
			if (pageTransactions.Count < PageSize)
			{
				break;
			}

			offset += PageSize;
			await Task.Delay(300, ct); // Rate limit compliance buffer
		}

		logger.LogInformation("Total pipeline collection for Till {Till}: {Count} records synced.", tillNumber, allTransactions.Count);
		return DarajaResult<List<PullTransaction>>.Ok(allTransactions);
	}

	private static void ValidateWindow(DateTime from, DateTime to)
	{
		if (to <= from) throw new ArgumentException("'to' timestamp must occur after 'from' timestamp.");
		if ((to - from).TotalHours > 48) throw new ArgumentException("Daraja Pull ledger restrictions allow a maximum window of 48 hours.");
	}
}