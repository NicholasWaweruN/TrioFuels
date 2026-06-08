using Microsoft.Extensions.Logging;
using Safaricom_Daraja;

namespace Safaricom_Daraja.Stk_Push;

/// <summary>
/// Parses and handles incoming STK Push callback payloads from Daraja.
/// Wire this up in your controller's POST endpoint.
/// </summary>
public interface IStkCallbackHandler
{
    Task HandleAsync(StkCallback callback, CancellationToken ct = default);
}

public sealed class StkCallbackHandler(ILogger<StkCallbackHandler> logger) : IStkCallbackHandler
{
    public async Task HandleAsync(StkCallback callback, CancellationToken ct = default)
    {
        var data = callback.Body.StkCallback;

        if (data.ResultCode != 0)
        {
            // Payment failed or was cancelled by customer
            logger.LogWarning(
                "STK Push FAILED — MerchantRequestID={MerchantId} CheckoutRequestID={CheckoutId} " +
                "ResultCode={Code} Desc={Desc}",
                data.MerchantRequestId, data.CheckoutRequestId,
                data.ResultCode, data.ResultDesc);

            // TODO: Update your pending transaction record to Failed status
            await Task.CompletedTask;
            return;
        }

        // Payment succeeded — extract metadata
        var meta = data.CallbackMetadata?.Items ?? [];

        var amount = GetMetaValue(meta, "Amount");
        var mpesaCode = GetMetaValue(meta, "MpesaReceiptNumber");
        var phone = GetMetaValue(meta, "PhoneNumber");
        var transDate = GetMetaValue(meta, "TransactionDate");

        logger.LogInformation(
            "STK Push SUCCESS — Receipt={Receipt} | Amount=KES {Amount} | Phone={Phone} | " +
            "Date={Date} | MerchantRequestID={MerchantId}",
            mpesaCode, amount, phone, transDate, data.MerchantRequestId);

        // TODO: Persist payment confirmation to your database
        // await _repo.ConfirmPaymentAsync(data.CheckoutRequestId, mpesaCode, amount, ct);

        await Task.CompletedTask;
    }

    private static string GetMetaValue(List<StkCallbackItem> items, string name)
        => items.FirstOrDefault(i => i.Name == name)?.Value?.ToString() ?? string.Empty;
}