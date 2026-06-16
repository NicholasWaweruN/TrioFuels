using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Context;
using Safaricom_Daraja;

namespace BussinessLogic.Services.Daraja;



public interface IPullTransactionImportService
{
	/// <summary>
	/// Pulls transactions for a specific till and upserts them into MpesaTransactions.
	/// </summary>
	Task<PullImportResult> ImportForTillAsync(
		string tillNumber,
		DateTime from,
		DateTime to,
		CancellationToken ct = default);

	/// <summary>
	/// Pulls and upserts transactions for ALL configured tills.
	/// </summary>
	Task<Dictionary<string, PullImportResult>> ImportAllTillsAsync(
		DateTime from,
		DateTime to,
		CancellationToken ct = default);
}

/// <summary>
/// Result of a pull import operation for a single till.
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
		// 1. Returns DarajaResult<List<PullTransaction>> instead of the raw page object
		var pullResult = await pullService.PullAllPagesAsync(tillNumber, from, to, ct);

		if (!pullResult.Success)
		{
			logger.LogError("Pull failed for {Till}: {Error}", tillNumber, pullResult.ErrorMessage);
			return new PullImportResult(tillNumber, 0, 0, 0, pullResult.ErrorMessage);
		}

		// 2. transactions is safely evaluated as List<PullTransaction>
		var transactions = pullResult.Data!;
		if (transactions.Count == 0)
		{
			logger.LogInformation("No transactions pulled for {Till}", tillNumber);
			return new PullImportResult(tillNumber, 0, 0, 0, null);
		}

		var tillConfig = _cfg.Tills.FirstOrDefault(t => t.TillNumber == tillNumber);

		// Batch verification optimization to prevent N+1 DB calls
		var receiptNos = transactions.Select(tx => tx.ReceiptNo).ToList();
		var existingTxMap = await db.MpesaTransactions
			.Where(m => receiptNos.Contains(m.TransID))
			.ToDictionaryAsync(m => m.TransID, m => m, ct);

		var inserted = 0;
		var updated = 0;
		var skipped = 0;

		foreach (var tx in transactions)
		{
			try
			{
				if (!existingTxMap.TryGetValue(tx.ReceiptNo, out var existing))
				{
					db.MpesaTransactions.Add(new MpesaTransaction
					{
						TransactionType = "C2B",
						TransID = tx.ReceiptNo,
						TransTime = ParseTime(tx.CompletionTime),
						TransAmount = tx.Amount,
						BusinessShortCode = tillNumber,
						TillNumber = tx.TillNumber,
						TillName = tillConfig?.Name ?? string.Empty,
						PaymentMethod = "C2B",
						MpesaReceiptNumber = tx.ReceiptNo,
						MSISDN = tx.SenderPhone,
						FirstName = string.Empty,
						MiddName = string.Empty,
						LastName = string.Empty,
						OrgAccountBalance = 0,
						Status = 1,
						DateTimeStamp = DateTime.UtcNow,
						DateModified = DateTime.UtcNow,
						DateCreated = DateTime.UtcNow,
						UsageBalance = tx.Amount,
						UserCode = tx.SenderPhone,
					});
					inserted++;
				}
				else
				{
					existing.DateModified = DateTime.UtcNow;
					updated++;
				}
			}
			catch (Exception ex)
			{
				logger.LogWarning(ex, "Skipped transaction {ReceiptNo}", tx.ReceiptNo);
				skipped++;
			}
		}

		await db.SaveChangesAsync(ct);

		logger.LogInformation(
			"Import complete for {Till}: {Inserted} inserted, {Updated} updated, {Skipped} skipped",
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

	private static DateTime ParseTime(string? value)
	{
		if (string.IsNullOrWhiteSpace(value)) return DateTime.UtcNow;

		return DateTime.TryParseExact(value, "yyyyMMddHHmmss",
			null, System.Globalization.DateTimeStyles.None, out var dt)
			? dt
			: DateTime.UtcNow;
	}
}