using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph.Models;
using Safaricom_Daraja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BussinessLogic.Services.Daraja;

public interface IPullTransactionImportService
{
	Task<Dictionary<string, ImportResult>> ImportAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default);

	Task<ImportResult> ImportTransactionsAsync(
		string tillNumber, List<PullTransaction> transactions, CancellationToken ct = default);

	Task<ImportResult> BackfillRangeAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default);

	Task<Dictionary<string, ImportResult>> BackfillAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default);
}



public sealed class PullTransactionImportService(
	OTOContext context,               // ASSUMPTION: your actual DbContext class/namespace — adjust if different
	IPullTransactionService pullService,
	IOptions<DarajaConfig> options,
	ILogger<PullTransactionImportService> logger) : IPullTransactionImportService
{
	private readonly DarajaConfig _cfg = options.Value;

	// Stay under Daraja's 48h max window per PullAsync call; 47h leaves
	// headroom against any rounding/inclusive-boundary edge cases on their side.
	private static readonly TimeSpan MaxChunk = TimeSpan.FromHours(47);

	public async Task<Dictionary<string, ImportResult>> ImportAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default)
	{
		var results = new Dictionary<string, ImportResult>();

		foreach (var till in _cfg.Tills)
		{
			try
			{
				var pullResult = await pullService.PullAllPagesAsync(till.TillNumber, from, to, ct);

				if (!pullResult.Success)
				{
					logger.LogError("Pull failed for Till {Till} [{From} - {To}]: {Error}",
						till.TillNumber, from, to, pullResult.ErrorMessage);

					results[till.TillNumber] = new ImportResult
					{
						TillNumber = till.TillNumber,
						Success = false,
						Error = pullResult.ErrorMessage
					};
					continue;
				}

				results[till.TillNumber] = await ImportTransactionsAsync(
					till.TillNumber, pullResult.Data ?? [], ct);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Import cycle blew up for Till {Till}", till.TillNumber);
				results[till.TillNumber] = new ImportResult
				{
					TillNumber = till.TillNumber,
					Success = false,
					Error = ex.Message
				};
			}

			await Task.Delay(300, ct); // rate limit compliance buffer between tills
		}

		return results;
	}

	public async Task<ImportResult> ImportTransactionsAsync(
	string tillNumber, List<PullTransaction> transactions, CancellationToken ct = default)
	{
		var result = new ImportResult { TillNumber = tillNumber };

		if (transactions.Count == 0)
			return result;

		var tillConfig = _cfg.Tills.FirstOrDefault(t => t.TillNumber == tillNumber);
		if (tillConfig is null)
		{
			logger.LogError("Import aborted: no configured Till entry found for {Till}", tillNumber);
			return new ImportResult
			{
				TillNumber = tillNumber,
				Success = false,
				Error = $"Unknown till number: {tillNumber}"
			};
		}

		var receiptNos = transactions.Select(t => t.ReceiptNo).ToList();

		var existing = await context.MpesaTransactions
			.Where(m => receiptNos.Contains(m.TransID))
			.ToDictionaryAsync(m => m.TransID, ct);

		foreach (var txn in transactions)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txn.ReceiptNo))
				{
					logger.LogWarning("Skipped a Pull record for Till {Till} with no transactionId/receipt number.", tillNumber);
					result.Skipped++;
					continue;
				}

				var amount = txn.GetAmountDecimal();
				var completionTime = txn.GetCompletionTimeUtc() ?? EatTime.Now;

				if (existing.TryGetValue(txn.ReceiptNo, out var existingRow))
				{
					// Already known — most likely inserted by C2B, or this is a
					// re-pulled overlap window. Deliberately NOT touching
					// UsageBalance/Status here: those reflect internal consumption
					// state (via RepayCreditAsync / ClearVariance) that Pull's
					// statement data has no visibility into, including "Blocked"
					// (Status 3), which Pull can never determine — so an existing
					// row's status is left exactly as-is regardless of what Pull says.
					existingRow.DateModified = EatTime.Now;

					if (string.IsNullOrWhiteSpace(existingRow.TillName) && !string.IsNullOrWhiteSpace(txn.OrganizationName))
						existingRow.TillName = txn.OrganizationName;

					context.Entry(existingRow).State = EntityState.Modified;
					result.Updated++;
				}
				else
				{
					// Genuinely missing — C2B never posted this one. Insert fresh
					// with the full amount as UsageBalance (nothing consumed yet
					// from our side), and derive Status from that balance.
					var (first, middle, last) = SplitSenderName(txn.Sender);

					context.MpesaTransactions.Add(new MpesaTransaction
					{
						TransactionType = !string.IsNullOrWhiteSpace(txn.TransactionType) ? txn.TransactionType : "Unknown",
						TransID = txn.ReceiptNo,
						TransTime = completionTime,
						TransAmount = amount,
						BusinessShortCode = tillConfig.StoreNumber,
						TillNumber = tillConfig.TillNumber,
						TillName = !string.IsNullOrWhiteSpace(txn.OrganizationName) ? txn.OrganizationName : (tillConfig.Name ?? string.Empty),
						PaymentMethod = "PULL", // tags this row as recovered via Pull, distinct from "STK"/"C2B"
						MpesaReceiptNumber = txn.ReceiptNo,
						MSISDN = txn.SenderPhone,
						FirstName = first,
						MiddName = middle,
						LastName = last,
						OrgAccountBalance = 0, // not provided by Pull's response shape
						UsageBalance = amount, // full amount, nothing consumed yet
						Status = DetermineStatus(usageBalance: amount, transAmount: amount),
						DateTimeStamp = completionTime,
						DateModified = EatTime.Now,
						CheckoutRequestID = string.Empty,
						DateCreated = EatTime.Now,
						UserCode = "Mpesa",
						MerchantRequestID = string.Empty,
						ShiftNumber = string.Empty
					});         
					result.Inserted++;
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to import receipt {ReceiptNo} for Till {Till}", txn.ReceiptNo, tillNumber);
				result.Skipped++;
			}
		}

		try
		{
			await context.SaveChangesAsync(ct);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "SaveChangesAsync failed while importing batch for Till {Till}", tillNumber);
			result.Success = false;
			result.Error = ex.Message;
		}

		return result;
	}

	/// <summary>
	/// Derives MpesaTransaction.Status from balance vs. original amount:
	/// 0=Fully Paid (balance exhausted), 1=Unused (full balance still available),
	/// 2=Partially Used (some consumed, some remains). "3=Blocked" is never
	/// derived here — it's an external decision (e.g. fraud/dispute flag) that
	/// Pull data alone can't determine, so it's only ever set elsewhere.
	/// </summary>
	private static int DetermineStatus(decimal usageBalance, decimal transAmount)
	{
		if (usageBalance <= 0)
			return 0; // Fully Paid

		if (usageBalance >= transAmount)
			return 1; // Unused

		return 2; // Partially Used
	}
	public async Task<ImportResult> BackfillRangeAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default)
	{
		if (to <= from)
			throw new ArgumentException("'to' must be after 'from'.");

		var aggregate = new ImportResult { TillNumber = tillNumber };
		var chunkStart = from;

		while (chunkStart < to)
		{
			var chunkEnd = chunkStart.Add(MaxChunk) < to ? chunkStart.Add(MaxChunk) : to;

			var pullResult = await pullService.PullAllPagesAsync(tillNumber, chunkStart, chunkEnd, ct);

			if (!pullResult.Success)
			{
				logger.LogError("Backfill chunk failed for Till {Till} [{From} - {To}]: {Error}",
					tillNumber, chunkStart, chunkEnd, pullResult.ErrorMessage);
				aggregate.Skipped++;
				aggregate.Success = false;
				aggregate.Error ??= pullResult.ErrorMessage;
			}
			else
			{
				var chunkImport = await ImportTransactionsAsync(tillNumber, pullResult.Data ?? [], ct);
				aggregate.Inserted += chunkImport.Inserted;
				aggregate.Updated += chunkImport.Updated;
				aggregate.Skipped += chunkImport.Skipped;

				if (!chunkImport.Success)
				{
					aggregate.Success = false;
					aggregate.Error ??= chunkImport.Error;
				}
			}

			chunkStart = chunkEnd;

			if (chunkStart < to)
				await Task.Delay(300, ct); // rate limit compliance buffer
		}

		logger.LogInformation(
			"Backfill complete for Till {Till} [{From} - {To}]: {Inserted} added, {Updated} matched/modified, {Skipped} failures",
			tillNumber, from, to, aggregate.Inserted, aggregate.Updated, aggregate.Skipped);

		return aggregate;
	}

	public async Task<Dictionary<string, ImportResult>> BackfillAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default)
	{
		var results = new Dictionary<string, ImportResult>();

		foreach (var till in _cfg.Tills)
		{
			results[till.TillNumber] = await BackfillRangeAsync(till.TillNumber, from, to, ct);
			await Task.Delay(300, ct);
		}

		return results;
	}

	/// <summary>
	/// Best-effort split of Daraja's single "sender" string (e.g. "JOHN A DOE")
	/// into First/Middle/Last to match MpesaTransaction's separate name fields.
	/// Falls back gracefully for 1 or 2-word names.
	/// </summary>
	private static (string First, string Middle, string Last) SplitSenderName(string sender)
	{
		if (string.IsNullOrWhiteSpace(sender))
			return (string.Empty, string.Empty, string.Empty);

		var parts = sender.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

		return parts.Length switch
		{
			0 => (string.Empty, string.Empty, string.Empty),
			1 => (parts[0], string.Empty, string.Empty),
			2 => (parts[0], string.Empty, parts[1]),
			_ => (parts[0], string.Join(' ', parts[1..^1]), parts[^1])
		};
	}
}
/// <summary>
/// Result data contract mapping transaction actions safely across project layers.
/// </summary>
public sealed class ImportResult
{
	public string TillNumber { get; set; } = string.Empty;
	public bool Success { get; set; } = true;
	public string? Error { get; set; }
	public int Inserted { get; set; }
	public int Updated { get; set; }
	public int Skipped { get; set; }
}
