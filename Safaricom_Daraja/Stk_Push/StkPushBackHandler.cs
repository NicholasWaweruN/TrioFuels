using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Daraja;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkCallbackHandler
{
	Task HandleAsync(StkCallback callback, CancellationToken ct = default);
}

public sealed class StkCallbackHandler(
	OTOContext context,
	ILogger<StkCallbackHandler> logger) : IStkCallbackHandler
{
	public async Task HandleAsync(StkCallback callback, CancellationToken ct = default)
	{
		var data = callback?.Body?.StkCallback;

		if (data is null)
		{
			logger.LogWarning("[STK][Callback] ❌ Invalid payload — Body.StkCallback is null.");
			return;
		}

		var checkoutId = data.CheckoutRequestId;

		logger.LogInformation(
			"[STK][Callback] ▶ CheckoutRequestID={CID} MerchantRequestID={MID} " +
			"ResultCode={RC} ResultDesc={RD}",
			checkoutId, data.MerchantRequestId, data.ResultCode, data.ResultDesc);

		// ── FIX 1: Always update StkTransaction status regardless of result ─────
		var stkTx = await context.StkTransactions
			.FirstOrDefaultAsync(x => x.CheckoutRequestId == checkoutId, ct);

		if (stkTx is null)
		{
			logger.LogWarning(
				"[STK][Callback] ⚠️ No StkTransaction found for CheckoutRequestID={CID} — " +
				"was InitiateAsync called first?", checkoutId);
		}

		// ── FAIL CASE — update StkTransaction, do NOT write ledger ──────────────
		if (data.ResultCode != 0)
		{
			logger.LogWarning(
				"[STK][Callback] ❌ Payment FAILED — CheckoutID={CID} ResultCode={RC} Desc={Desc}",
				checkoutId, data.ResultCode, data.ResultDesc);

			if (stkTx is not null)
			{
				stkTx.Status = "Failed";
				stkTx.ResultCode = data.ResultCode.ToString();
				stkTx.ResultDescription = data.ResultDesc ?? string.Empty;
				stkTx.DateCompleted = DateTime.UtcNow;
				await context.SaveChangesAsync(ct);

				logger.LogInformation(
					"[STK][Callback] StkTransaction updated → Status=Failed CheckoutID={CID}", checkoutId);
			}

			return;
		}

		// ── SUCCESS — extract metadata ────────────────────────────────────────────
		var meta = data.CallbackMetadata?.Items ?? new List<StkCallbackItem>();
		var receipt = Get(meta, "MpesaReceiptNumber");
		var amount = Get(meta, "Amount");
		var phone = Get(meta, "PhoneNumber");
		var transDate = Get(meta, "TransactionDate");
		var balance = Get(meta, "Balance");

		logger.LogInformation(
			"[STK][Callback] Metadata — Receipt={R} Amount={A} Phone={P} TransDate={D} Balance={B}",
			receipt, amount, phone, transDate, balance);

		// ── DUPLICATE PROTECTION ──────────────────────────────────────────────────
		var exists = await context.MpesaTransactions
			.AnyAsync(x => x.MpesaReceiptNumber == receipt, ct);

		if (exists)
		{
			logger.LogWarning(
				"[STK][Callback] ⚠️ Duplicate — Receipt={Receipt} already in MpesaTransactions. Ignored.",
				receipt);
			return;
		}

		// ── FIX 2: Pull TillNumber + TillName from StkTransaction ────────────────
		// StkTransaction was saved during InitiateAsync and has the till info.
		var tillNumber = stkTx?.TillNumber ?? string.Empty;
		var tillName = string.Empty;

		if (!string.IsNullOrEmpty(tillNumber))
		{
			var till = await context.Tills
				.Where(t => t.TillNumber == tillNumber)
				.Select(t => t.TillName)
				.FirstOrDefaultAsync(ct);

			tillName = till ?? string.Empty;
		}

		logger.LogInformation(
			"[STK][Callback] Till resolved from StkTransaction — TillNumber={TN} TillName={Name}",
			tillNumber, tillName);

		// ── WRITE LEDGER ──────────────────────────────────────────────────────────
		var transaction = new MpesaTransaction
		{
			TransactionType = "STK",
			TransID = receipt,
			MpesaReceiptNumber = receipt,
			CheckoutRequestID = checkoutId,
			MerchantRequestID = data.MerchantRequestId,
			TransAmount = decimal.TryParse(amount, out var amt) ? amt : 0,
			TransTime = ParseDate(transDate),
			BusinessShortCode = string.Empty,
			TillNumber = tillNumber,          // ✅ FIX: filled from StkTransaction
			TillName = tillName,            // ✅ FIX: filled from Tills table
			PaymentMethod = "STK",
			MSISDN = phone,
			Status = 1,
			DateTimeStamp = DateTime.UtcNow.AddHours(3),
			DateModified = DateTime.UtcNow.AddHours(3),
			DateCreated = DateTime.UtcNow.AddHours(3),
			FirstName = string.Empty,
			LastName = string.Empty,
			MiddName = string.Empty,
			OrgAccountBalance = decimal.TryParse(balance, out var bal) ? bal : 0,
			UsageBalance = decimal.TryParse(amount, out var usage) ? usage : 0,
			UserCode = "Mpesa"
		};

		context.MpesaTransactions.Add(transaction);

		// ── FIX 3: Update StkTransaction to Completed ─────────────────────────────
		if (stkTx is not null)
		{
			stkTx.Status = "Completed";
			stkTx.MpesaReceiptNumber = receipt;
			stkTx.ResultCode = "0";
			stkTx.ResultDescription = data.ResultDesc ?? "Success";
			stkTx.DateCompleted = DateTime.UtcNow;
		}

		await context.SaveChangesAsync(ct);

		logger.LogInformation(
			"[STK][Callback] ✅ Ledger saved — Receipt={Receipt} Amount={Amount} " +
			"Phone={Phone} Till={TN} ({TillName})",
			receipt, amount, phone, tillNumber, tillName);
	}

	private static string Get(List<StkCallbackItem> items, string name)
		=> items.FirstOrDefault(x => x.Name == name)?.Value?.ToString() ?? string.Empty;

	private static DateTime ParseDate(string value)
	{
		if (long.TryParse(value, out var dt))
		{
			var s = dt.ToString();
			if (s.Length == 14)
				return DateTime.ParseExact(s, "yyyyMMddHHmmss", null);
		}
		return DateTime.UtcNow;
	}
}