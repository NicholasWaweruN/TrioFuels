using System;
using System.Collections.Generic;
using System.Linq;
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
	/// Registers Pull for a single till/shortcode. Must be called at least once
	/// before that till's transactions become queryable. Safe to call repeatedly —
	/// Safaricom returns "already registered" (1001) rather than erroring.
	/// </summary>
	Task<DarajaResult<PullRegisterResponse>> RegisterAsync(
		string tillNumber, CancellationToken ct = default);

	/// <summary>
	/// Registers Pull for every configured till, sequentially.
	/// </summary>
	Task<Dictionary<string, DarajaResult<PullRegisterResponse>>> RegisterAllTillsAsync(
		CancellationToken ct = default);

	/// <summary>
	/// Pulls transactions for a specific till within a single offset window.
	/// tillNumber is your internal identifier — it is resolved to that till's
	/// StoreNumber (settlement shortcode) before being sent to Daraja as ShortCode,
	/// matching what RegisterAsync already uses.
	/// </summary>
	Task<DarajaResult<PullTransactionResponse>> PullAsync(
		string tillNumber, DateTime from, DateTime to, int offset = 0, CancellationToken ct = default);

	/// <summary>
	/// Pulls ALL pages for the window, handling pagination automatically.
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

	public async Task<DarajaResult<PullRegisterResponse>> RegisterAsync(string tillNumber, CancellationToken ct = default)
	{
		try
		{
			// NOTE: assumes DarajaConfig exposes PullNominatedNumber and PullCallbackUrl
			// matching the appsettings.json keys. Rename here if your DarajaConfig class
			// uses different property names for these.
			var payload = new PullRegisterRequest
			{
				ShortCode = tillNumber,
				RequestType = "Pull",
				NominatedNumber = _cfg.PullNominatedNumber,
				CallBackURL = _cfg.PullCallbackUrl
			};

			var client = httpFactory.CreateClient("Daraja");
			var accessToken = await tokenService.GetAccessTokenAsync(ct);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.PostAsJsonAsync("/pulltransactions/v1/register", payload, ct);
			var rawJson = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogError("Pull registration failed [{StatusCode}] for Till {Till}: {Body}", response.StatusCode, tillNumber, rawJson);
				return DarajaResult<PullRegisterResponse>.Fail(rawJson);
			}

			var result = JsonSerializer.Deserialize<PullRegisterResponse>(rawJson);
			if (result is null)
			{
				logger.LogError("Pull registration response for Till {Till} deserialized to null. Raw: {Raw}", tillNumber, rawJson);
				return DarajaResult<PullRegisterResponse>.Fail("Empty or unparseable response body.");
			}

			if (!result.IsRegistered)
			{
				logger.LogError("Pull registration returned unexpected status {Status} for Till {Till}: {Description}",
					result.ResponseStatus, tillNumber, result.ResponseDescription);
				return DarajaResult<PullRegisterResponse>.Fail($"{result.ResponseStatus}: {result.ResponseDescription}");
			}

			logger.LogInformation("Pull registration for Till {Till}: {Status} — {Description}", tillNumber, result.ResponseStatus, result.ResponseDescription);

			return DarajaResult<PullRegisterResponse>.Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Pull registration blew up for Till {Till}", tillNumber);
			return DarajaResult<PullRegisterResponse>.Fail(ex.Message);
		}
	}

	public async Task<Dictionary<string, DarajaResult<PullRegisterResponse>>> RegisterAllTillsAsync(CancellationToken ct = default)
	{
		var results = new Dictionary<string, DarajaResult<PullRegisterResponse>>();

		foreach (var till in _cfg.Tills)
		{
			results[till.TillNumber] = await RegisterAsync(till.StoreNumber, ct);
			await Task.Delay(300, ct);
		}

		return results;
	}

	/// <summary>
	/// Pulls transactions for a specific till within a single offset window.
	/// ShortCode in the underlying request is the STORE NUMBER (settlement shortcode),
	/// not the till number — same identifier RegisterAsync already uses. The till
	/// number customers dial is a different, non-registrable identifier for this API.
	/// tillNumber here is still your internal identifier, used to resolve the store
	/// number from config and for logging/DB attribution downstream.
	/// </summary>
	public async Task<DarajaResult<PullTransactionResponse>> PullAsync(string tillNumber, DateTime from, DateTime to, int offset = 0, CancellationToken ct = default)
	{
		try
		{
			ValidateWindow(from, to);

			var tillConfig = _cfg.Tills.FirstOrDefault(t => t.TillNumber == tillNumber);
			if (tillConfig is null)
			{
				logger.LogError("Pull aborted: no configured Till entry found for {Till}", tillNumber);
				return DarajaResult<PullTransactionResponse>.Fail($"Unknown till number: {tillNumber}");
			}

			// FIX: ShortCode must be the StoreNumber (settlement shortcode), matching
			// RegisterAsync — not the till number. The till number is customer-facing
			// and isn't recognized by Daraja as a queryable/registrable shortcode.
			var payload = new PullTransactionRequest
			{
				ShortCode = tillConfig.StoreNumber,
				StartDate = from.ToString(DateFormat),
				EndDate = to.ToString(DateFormat),
				OffSetValue = offset.ToString()
			};

			var client = httpFactory.CreateClient("Daraja");

			var accessToken = await tokenService.GetAccessTokenAsync(ct);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.PostAsJsonAsync("/pulltransactions/v1/query", payload, ct);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync(ct);
				logger.LogError("Pull failed [{StatusCode}] for Till {Till} (StoreNumber {StoreNumber}): {Error}",
					response.StatusCode, tillNumber, tillConfig.StoreNumber, error);
				return DarajaResult<PullTransactionResponse>.Fail(error);
			}

			var rawJson = await response.Content.ReadAsStringAsync(ct);
			var result = JsonSerializer.Deserialize<PullTransactionResponse>(rawJson);

			if (result is null)
			{
				logger.LogError("Pull response for Till {Till} deserialized to null. Raw: {Raw}", tillNumber, rawJson);
				return DarajaResult<PullTransactionResponse>.Fail("Empty or unparseable response body.");
			}

			// Daraja returns HTTP 200 even for "no data" and some failure cases —
			// the real status lives in ResponseCode, not the HTTP status code.
			// 1000 = success, 1001 = success but nothing in this window, 500 = shortcode has no data at all.
			if (!result.IsSuccess && !result.IsEmptyWindow)
			{
				logger.LogError("Pull returned ResponseCode {Code} for Till {Till}: {Message}",
					result.ResponseCode, tillNumber, result.ResponseMessage);
				return DarajaResult<PullTransactionResponse>.Fail(
					$"{result.ResponseCode}: {result.ResponseMessage}");
			}

			var count = result.FlattenTransactions().Count;
			logger.LogInformation("Pulled {Count} transactions for Till {Till} (StoreNumber {StoreNumber}) | offset={Offset} | ResponseCode={Code}",
				count, tillNumber, tillConfig.StoreNumber, offset, result.ResponseCode);

			return DarajaResult<PullTransactionResponse>.Ok(result);
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

			var pageTransactions = result.Data?.FlattenTransactions() ?? [];
			allTransactions.AddRange(pageTransactions);

			// Empty window (ResponseCode 1001) or a short page both mean we're done.
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