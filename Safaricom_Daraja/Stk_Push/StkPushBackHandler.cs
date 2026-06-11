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

public sealed class StkCallbackHandler(OTOContext context,ILogger<StkCallbackHandler> logger) : IStkCallbackHandler
{
	private readonly OTOContext _context = context;

	public async Task HandleAsync(StkCallback callback, CancellationToken ct = default)
	{
		var data = callback?.Body?.StkCallback;

		if (data == null)
		{
			logger.LogWarning("Invalid STK callback payload");
			return;
		}

		var checkoutId = data.CheckoutRequestId;

		// ❌ FAIL CASE (do NOT write ledger)
		if (data.ResultCode != 0)
		{
			logger.LogWarning("STK FAILED — CheckoutId={CheckoutId} Desc={Desc}",checkoutId,data.ResultDesc);
			return;
		}

		var meta = data.CallbackMetadata?.Items ?? new List<StkCallbackItem>();

		var receipt = Get(meta, "MpesaReceiptNumber");
		var amount = Get(meta, "Amount");
		var phone = Get(meta, "PhoneNumber");
		var transDate = Get(meta, "TransactionDate");
		var balance = Get(meta, "Balance");


		// ⚠️ DUPLICATE PROTECTION (VERY IMPORTANT)
		var exists = await _context.MpesaTransactions.AnyAsync(x => x.MpesaReceiptNumber == receipt, ct);

		if (exists)
		{
			logger.LogWarning("Duplicate STK callback ignored — Receipt={Receipt}", receipt);
			return;
		}

		// ✔ CREATE FINAL LEDGER ENTRY (ONLY HERE)
		var transaction = new MpesaTransaction
		{
			TransactionType = "STK",
			TransID = receipt,
			MpesaReceiptNumber = receipt,
			CheckoutRequestID = checkoutId,
			MerchantRequestID = data.MerchantRequestId,
			TransAmount = decimal.TryParse(amount, out var amt) ? amt : 0,
			TransTime = ParseDate(transDate),
			BusinessShortCode = string.Empty, // optional if not in callback
			TillNumber = string.Empty,        // fill if you map tills elsewhere
			TillName = string.Empty,
			PaymentMethod = "STK",
			MSISDN = phone,
			Status = 1, // SUCCESS ONLY (ledger rule)
			DateTimeStamp = DateTime.UtcNow.AddHours(3),
			DateModified = DateTime.UtcNow.AddHours(3),
			DateCreated = DateTime.UtcNow.AddHours(3),
			FirstName = string.Empty,
			LastName = string.Empty,
			MiddName = string.Empty,
			OrgAccountBalance = decimal.TryParse(balance, out var bal) ? bal : 0, 
			UsageBalance = decimal.TryParse(amount, out var usageBalance) ? usageBalance : 0,
			UserCode = "Mpesa"
		};

		_context.MpesaTransactions.Add(transaction);
		await _context.SaveChangesAsync(ct);

		logger.LogInformation("STK LEDGER SAVED — Receipt={Receipt} Amount={Amount} Phone={Phone}",receipt, amount, phone);
	}

	// ─────────────────────────────────────────────
	// HELPERS
	// ─────────────────────────────────────────────
	
	private static string Get(List<StkCallbackItem> items, string name)
		=> items.FirstOrDefault(x => x.Name == name)?.Value?.ToString() ?? string.Empty;

	private static DateTime ParseDate(string value)
	{
		if (long.TryParse(value, out var dt))
		{
			var s = dt.ToString();
			if (s.Length == 14)
			{
				return DateTime.ParseExact(s, "yyyyMMddHHmmss", null);
			}
		}
		return DateTime.UtcNow;
	}
}