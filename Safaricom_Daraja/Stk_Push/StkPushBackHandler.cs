using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Daraja;
using DataAccessLayer.EntityModels.Transactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Safaricom_Daraja.Helpers;
using ServiceStack.Configuration;

namespace Safaricom_Daraja.Stk_Push;

public interface IStkCallbackHandler
{
	Task HandleAsync(StkCallback callback, CancellationToken ct = default);
}

public sealed class StkCallbackHandler(OTOContext context, ILogger<StkCallbackHandler> logger,IShiftResolver resolver) : IStkCallbackHandler
{
	private const int MaxStkLookupAttempts = 3;
	private static readonly TimeSpan StkLookupBackoffStep = TimeSpan.FromMilliseconds(500);
	private readonly IShiftResolver _resolver = resolver;

	public async Task HandleAsync(StkCallback callback, CancellationToken ct = default)
	{
		var data = callback?.Body?.StkCallback;

		if (data is null)
		{
			logger.LogWarning("[STK][Callback] ❌ Invalid payload — Body.StkCallback is null.");
			return;
		}

		var checkoutId = data.CheckoutRequestId;

		logger.LogInformation("[STK][Callback] ▶ CheckoutRequestID={CID} MerchantRequestID={MID} " +
			"ResultCode={RC} ResultDesc={RD}", checkoutId, data.MerchantRequestId, data.ResultCode, data.ResultDesc);

		// ── FIX 1: Resolve StkTransaction, retrying briefly to cover the race where
		// Safaricom's callback arrives before InitiateAsync's SaveChangesAsync commits ──
		var stkTx = await ResolveStkTransactionAsync(checkoutId, data.MerchantRequestId, ct);

		// ── FAIL CASE — update StkTransaction, do NOT write ledger ──────────────
		if (data.ResultCode != 0)
		{
			logger.LogWarning("[STK][Callback] ❌ Payment FAILED — CheckoutID={CID} ResultCode={RC} Desc={Desc}",
				checkoutId, data.ResultCode, data.ResultDesc);

			if (stkTx is not null)
			{
				stkTx.Status = "Failed";
				stkTx.ResultCode = data.ResultCode.ToString();
				stkTx.ResultDescription = data.ResultDesc ?? string.Empty;
				stkTx.DateCompleted =EatTime.Now;

				await context.SaveChangesAsync(ct);

				logger.LogInformation("[STK][Callback] StkTransaction updated → Status=Failed CheckoutID={CID}", checkoutId);
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

		logger.LogInformation("[STK][Callback] Metadata — Receipt={R} Amount={A} Phone={P} TransDate={D} Balance={B}",
			receipt, amount, phone, transDate, balance);

		// ── FIX 2: Resolve TillNumber, TillName, BusinessShortCode from StkTransaction ──
		// StkTransaction was saved during InitiateAsync and has the till info.
		var tillNumber = stkTx?.TillNumber ?? string.Empty;
		var businessShortCode = stkTx?.BusinessShortCode ?? string.Empty; // was hardcoded to string.Empty
		var tillName = string.Empty;

		if (!string.IsNullOrEmpty(tillNumber))
		{
			var till = await context.Tills
				.Where(t => t.TillNumber == tillNumber)
				.Select(t => t.TillName)
				.FirstOrDefaultAsync(ct);

			tillName = till ?? string.Empty;
		}

		if (stkTx is null)
		{
			logger.LogError(
				"[STK][Callback] ❌ StkTransaction still unresolved after retries — CheckoutID={CID} MerchantRequestID={MID}. " +
				"Ledger row will be written WITHOUT TillNumber/TillName/BusinessShortCode and needs manual backfill.",
				checkoutId, data.MerchantRequestId);
		}
		else
		{
			logger.LogInformation("[STK][Callback] Till/ShortCode resolved from StkTransaction — TillNumber={TN} BusinessShortCode={BSC} TillName={Name}",tillNumber, businessShortCode, tillName);
		}

		// ── DUPLICATE PROTECTION ──────────────────────────────────────────────────
		var exists = await context.MpesaTransactions
			.FirstOrDefaultAsync(x => x.MpesaReceiptNumber == receipt, ct);

		if (exists is not null)
		{
			// C2B won the race — backfill the STK fields so polling can resolve
			exists.CheckoutRequestID = checkoutId;
			exists.MerchantRequestID = data.MerchantRequestId;
			exists.TransactionType = "STK";
			exists.PaymentMethod = "STK";
			exists.MSISDN = phone;
			exists.Status = 1;
			exists.DateModified = EatTime.Now;

			// FIX 3: also backfill till/shortcode if the C2B row didn't already have them
			if (string.IsNullOrEmpty(exists.TillNumber) && !string.IsNullOrEmpty(tillNumber))
				exists.TillNumber = tillNumber;

			if (string.IsNullOrEmpty(exists.TillName) && !string.IsNullOrEmpty(tillName))
				exists.TillName = tillName;

			if (string.IsNullOrEmpty(exists.BusinessShortCode) && !string.IsNullOrEmpty(businessShortCode))
				exists.BusinessShortCode = businessShortCode;

			if (stkTx is not null)
			{
				stkTx.Status = "Completed";
				stkTx.MpesaReceiptNumber = receipt;
				stkTx.ResultCode = "0";
				stkTx.ResultDescription = data.ResultDesc ?? "Success";
				stkTx.DateCompleted =EatTime.Now;
				
			}

			await context.SaveChangesAsync(ct);

			logger.LogInformation("[STK][Callback] ✅ Backfilled C2B record — Receipt={R} CheckoutID={CID}",receipt, checkoutId);
			return;
		}

		var shift = await _resolver.GetCurrentShiftByTill(tillNumber);
		var shiftNumber = shift.ResponseObject as string ?? string.Empty;

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
			BusinessShortCode = businessShortCode,   // ✅ FIX: filled from StkTransaction
			TillNumber = tillNumber,                 // ✅ filled from StkTransaction
			TillName = tillName,                     // ✅ filled from Tills table
			PaymentMethod = "STK",
			MSISDN = phone,
			Status = 1,
			DateTimeStamp = EatTime.Now,
			DateModified = EatTime.Now,
			DateCreated = EatTime.Now,
			FirstName = string.Empty,
			LastName = string.Empty,
			MiddName = string.Empty,
			OrgAccountBalance = decimal.TryParse(balance, out var bal) ? bal : 0,
			UsageBalance = decimal.TryParse(amount, out var usage) ? usage : 0,
			UserCode = "Mpesa",
			ShiftNumber = shiftNumber
		};

		context.MpesaTransactions.Add(transaction);

		// ── FIX 4: Update StkTransaction to Completed ─────────────────────────────
		if (stkTx is not null)
		{
			stkTx.Status = "Completed";
			stkTx.MpesaReceiptNumber = receipt;
			stkTx.ResultCode = "0";
			stkTx.ResultDescription = data.ResultDesc ?? "Success";
			stkTx.DateCompleted =EatTime.Now;
		}

		await context.SaveChangesAsync(ct);

		logger.LogInformation("[STK][Callback] ✅ Ledger saved — Receipt={Receipt} Amount={Amount} " +"Phone={Phone} Till={TN} ShortCode={BSC} ({TillName})",receipt, amount, phone, tillNumber, businessShortCode, tillName);
	}

	/// <summary>
	/// Looks up the StkTransaction by CheckoutRequestId, retrying with backoff to cover the
	/// race where Safaricom's callback arrives before InitiateAsync's SaveChangesAsync commits.
	/// Falls back to MerchantRequestId if CheckoutRequestId still doesn't resolve.
	/// </summary>
	private async Task<StkTransaction?> ResolveStkTransactionAsync(string checkoutId, string merchantRequestId, CancellationToken ct)
	{
		var stkTx = await context.StkTransactions.FirstOrDefaultAsync(x => x.CheckoutRequestId == checkoutId, ct);

		for (var attempt = 1; attempt <= MaxStkLookupAttempts && stkTx is null; attempt++)
		{
			logger.LogWarning("[STK][Callback] ⚠️ No StkTransaction found for CheckoutRequestID={CID} — retry {Attempt}/{Max}",checkoutId, attempt, MaxStkLookupAttempts);

			await Task.Delay(StkLookupBackoffStep * attempt, ct);

			stkTx = await context.StkTransactions
				.FirstOrDefaultAsync(x => x.CheckoutRequestId == checkoutId, ct);
		}

		if (stkTx is null && !string.IsNullOrEmpty(merchantRequestId))
		{
			stkTx = await context.StkTransactions
				.FirstOrDefaultAsync(x => x.MerchantRequestId == merchantRequestId, ct);

			if (stkTx is not null)
				logger.LogInformation("[STK][Callback] ✅ Recovered StkTransaction via MerchantRequestID fallback — CheckoutRequestID={CID}",checkoutId);
		}

		return stkTx;
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
		return EatTime.Now;
	}
}