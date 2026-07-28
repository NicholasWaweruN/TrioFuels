using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Context;
using Safaricom_Daraja;
using DataAccessLayer.Helpers;

namespace BussinessLogic.Services.Daraja;

public interface IPullTransactionImportService
{
	/// <summary>
	/// Pulls transactions for a specific till and upserts them into MpesaTransactions.
	/// </summary>
	Task<PullImportResult> ImportForTillAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default);

	/// <summary>
	/// Pulls and upserts transactions for ALL configured tills sequentially.
	/// </summary>
	Task<Dictionary<string, PullImportResult>> ImportAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default);
}

public sealed class PullTransactionImportService(
	IPullTransactionService pullService,
	OTOContext db,
	IOptions<DarajaConfig> options,
	ILogger<PullTransactionImportService> logger) : IPullTransactionImportService
{
	private readonly DarajaConfig _cfg = options.Value;

	public async Task<PullImportResult> ImportForTillAsync(
		string tillNumber, DateTime from, DateTime to, CancellationToken ct = default)
	{
		// NOTE: Daraja's Pull API has no server-side till filter — every call for this
		// shortcode returns the SAME full set of C2B transactions regardless of tillNumber.
		// Calling this once per configured till (see ImportAllTillsAsync) will therefore
		// re-fetch and re-process the identical result set multiple times. Until there's a
		// reliable way to attribute a transaction to a specific till (e.g. via
		// BillReferenceNumber if your tills map to distinct bill references), consider
		// calling PullAllPagesAsync once for the shortcode and doing till-attribution here
		// rather than looping per-till upstream.
		var pullResult = await pullService.PullAllPagesAsync(tillNumber, from, to, ct);

		if (!pullResult.Success)
		{
			logger.LogError("Data ingestion sequence halted for Till {Till}: {Error}", tillNumber, pullResult.ErrorMessage);
			return new PullImportResult(tillNumber, 0, 0, 0, pullResult.ErrorMessage);
		}

		var transactions = pullResult.Data!;
		if (transactions.Count == 0)
		{
			logger.LogInformation("No records discovered inside current ledger slot for Till {Till}", tillNumber);
			return new PullImportResult(tillNumber, 0, 0, 0, null);
		}

		var tillConfig = _cfg.Tills.FirstOrDefault(t => t.TillNumber == tillNumber);

		var receiptNos = transactions.Select(tx => tx.ReceiptNo).ToList();

		// FIX: .AsTracking() added — without it, if OTOContext has global NoTracking
		// configured (as it does elsewhere in this codebase), entities returned here
		// are untracked. Mutating existing.DateModified below would then silently fail
		// to persist on SaveChangesAsync — no exception, just a no-op update.
		var existingTxMap = await db.MpesaTransactions
			.AsTracking()
			.Where(m => receiptNos.Contains(m.TransID))
			.ToDictionaryAsync(m => m.TransID, m => m, ct);

		var inserted = 0;
		var updated = 0;
		var skipped = 0;

		foreach (var tx in transactions)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(tx.ReceiptNo))
				{
					logger.LogWarning("Skipping record with missing transaction id (ReceiptNo) for Till {Till}", tillNumber);
					skipped++;
					continue;
				}

				if (!existingTxMap.TryGetValue(tx.ReceiptNo, out var existing))
				{
					db.MpesaTransactions.Add(new MpesaTransaction
					{
						TransactionType = "Pull",
						TransID = tx.ReceiptNo,
						TransTime = tx.GetCompletionTimeUtc() ?? EatTime.Now,
						TransAmount = tx.GetAmountDecimal(),
						BusinessShortCode = _cfg.BusinessShortCode,
						// Daraja's Pull response has no till_number field — this is
						// attributed from the loop's tillNumber, not from Safaricom's data.
						TillNumber = tillNumber,
						TillName = tillConfig?.Name ?? string.Empty,
						PaymentMethod = "C2B",
						MpesaReceiptNumber = tx.ReceiptNo,
						MSISDN = tx.SenderPhone,
						FirstName = string.Empty,
						MiddName = string.Empty,
						LastName = string.Empty,
						OrgAccountBalance = 0,
						Status = 1,
						DateTimeStamp = EatTime.Now,
						DateModified = EatTime.Now,
						DateCreated = EatTime.Now,
						UsageBalance = tx.GetAmountDecimal(),
						UserCode = tx.SenderPhone,
						CheckoutRequestID = string.Empty,
						MerchantRequestID = string.Empty,
						ShiftNumber = string.Empty
					});
					inserted++;
				}
				else
				{
					existing.DateModified = EatTime.Now;
					updated++;
				}
			}
			catch (Exception ex)
			{
				logger.LogWarning(ex, "Failed mapping values for record transaction reference identifier: {ReceiptNo}", tx.ReceiptNo);
				skipped++;
			}
		}

		await db.SaveChangesAsync(ct);

		logger.LogInformation(
			"Sync batch complete for Till {Till}: {Inserted} added, {Updated} matched/modified, {Skipped} failures",
			tillNumber, inserted, updated, skipped);

		return new PullImportResult(tillNumber, inserted, updated, skipped, null);
	}

	public async Task<Dictionary<string, PullImportResult>> ImportAllTillsAsync(
		DateTime from, DateTime to, CancellationToken ct = default)
	{
		var results = new Dictionary<string, PullImportResult>();

		foreach (var till in _cfg.Tills)
		{
			results[till.TillNumber] = await ImportForTillAsync(till.TillNumber, from, to, ct);
			await Task.Delay(500, ct);
		}

		return results;
	}
}

/// <summary>
/// Result data contract mapping transaction actions safely across project layers.
/// </summary>
public record PullImportResult(
	string TillNumber,
	int Inserted,
	int Updated,
	int Skipped,
	string? Error)
{
	public bool Success => Error is null;
}