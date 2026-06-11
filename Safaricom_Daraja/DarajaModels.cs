using System.Text.Json.Serialization;

namespace Safaricom_Daraja;

// ─── Auth ─────────────────────────────────────────────────────────────────────

public class DarajaTokenResponse
{
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; } = string.Empty;

	[JsonPropertyName("expires_in")]
	public string ExpiresIn { get; set; } = string.Empty;
}

// ─── STK Push ─────────────────────────────────────────────────────────────────

public class StkPushRequest
{
	// ✅ All fields explicitly named — Safaricom is case-sensitive
	[JsonPropertyName("BusinessShortCode")]
	public string BusinessShortCode { get; set; } = string.Empty;

	[JsonPropertyName("Password")]
	public string Password { get; set; } = string.Empty;

	[JsonPropertyName("Timestamp")]
	public string Timestamp { get; set; } = string.Empty;

	[JsonPropertyName("TransactionType")]
	public string TransactionType { get; set; } = "CustomerBuyGoodsOnline";

	[JsonPropertyName("Amount")]
	public long Amount { get; set; }

	[JsonPropertyName("PartyA")]
	public string PartyA { get; set; } = string.Empty;

	[JsonPropertyName("PartyB")]
	public string PartyB { get; set; } = string.Empty;

	[JsonPropertyName("PhoneNumber")]
	public string PhoneNumber { get; set; } = string.Empty;

	[JsonPropertyName("CallBackURL")]
	public string CallBackURL { get; set; } = string.Empty;

	[JsonPropertyName("AccountReference")]
	public string AccountReference { get; set; } = string.Empty;

	[JsonPropertyName("TransactionDesc")]
	public string TransactionDesc { get; set; } = string.Empty;
}

public class StkPushResponse
{
	[JsonPropertyName("MerchantRequestID")]
	public string MerchantRequestId { get; set; } = string.Empty;

	[JsonPropertyName("CheckoutRequestID")]
	public string CheckoutRequestId { get; set; } = string.Empty;

	[JsonPropertyName("ResponseCode")]
	public string ResponseCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseDescription")]
	public string ResponseDescription { get; set; } = string.Empty;

	[JsonPropertyName("CustomerMessage")]
	public string CustomerMessage { get; set; } = string.Empty;
}

// ─── STK Query ────────────────────────────────────────────────────────────────

public class StkQueryRequest
{
	[JsonPropertyName("BusinessShortCode")]
	public string BusinessShortCode { get; set; } = string.Empty;

	[JsonPropertyName("Password")]
	public string Password { get; set; } = string.Empty;

	[JsonPropertyName("Timestamp")]
	public string Timestamp { get; set; } = string.Empty;

	[JsonPropertyName("CheckoutRequestID")]
	public string CheckoutRequestID { get; set; } = string.Empty;
}

public class StkQueryResponse
{
	[JsonPropertyName("ResponseCode")]
	public string ResponseCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseDescription")]
	public string ResponseDescription { get; set; } = string.Empty;

	[JsonPropertyName("MerchantRequestID")]
	public string MerchantRequestId { get; set; } = string.Empty;

	[JsonPropertyName("CheckoutRequestID")]
	public string CheckoutRequestId { get; set; } = string.Empty;

	[JsonPropertyName("ResultCode")]
	public string ResultCode { get; set; } = string.Empty;

	[JsonPropertyName("ResultDesc")]
	public string ResultDesc { get; set; } = string.Empty;
}

// ─── STK Callback ─────────────────────────────────────────────────────────────

public class StkCallback
{
	[JsonPropertyName("Body")]
	public StkCallbackBody Body { get; set; } = new();
}

public class StkCallbackBody
{
	[JsonPropertyName("stkCallback")]
	public StkCallbackData StkCallback { get; set; } = new();
}

public class StkCallbackData
{
	[JsonPropertyName("MerchantRequestID")]
	public string MerchantRequestId { get; set; } = string.Empty;

	[JsonPropertyName("CheckoutRequestID")]
	public string CheckoutRequestId { get; set; } = string.Empty;

	[JsonPropertyName("ResultCode")]
	public int ResultCode { get; set; }

	[JsonPropertyName("ResultDesc")]
	public string ResultDesc { get; set; } = string.Empty;

	[JsonPropertyName("CallbackMetadata")]
	public StkCallbackMetadata? CallbackMetadata { get; set; }
}

public class StkCallbackMetadata
{
	[JsonPropertyName("Item")]
	public List<StkCallbackItem> Items { get; set; } = [];
}

public class StkCallbackItem
{
	[JsonPropertyName("Name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("Value")]
	public object? Value { get; set; }
}

// ─── C2B Register URLs ────────────────────────────────────────────────────────

public class C2BRegisterRequest
{
	[JsonPropertyName("ShortCode")]
	public string ShortCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseType")]
	public string ResponseType { get; set; } = "Completed";

	[JsonPropertyName("ConfirmationURL")]
	public string ConfirmationURL { get; set; } = string.Empty;

	[JsonPropertyName("ValidationURL")]
	public string ValidationURL { get; set; } = string.Empty;
}

public class C2BRegisterResponse
{
	[JsonPropertyName("OriginatorCoversationID")]  // Safaricom typo — "Coversa" not "Conversa"
	public string OriginatorConversationId { get; set; } = string.Empty;

	[JsonPropertyName("ResponseCode")]
	public string ResponseCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseDescription")]
	public string ResponseDescription { get; set; } = string.Empty;
}

// ─── C2B Callbacks ────────────────────────────────────────────────────────────

public class C2BValidationRequest
{
	[JsonPropertyName("TransactionType")]
	public string TransactionType { get; set; } = string.Empty;

	[JsonPropertyName("TransID")]
	public string TransactionId { get; set; } = string.Empty;

	[JsonPropertyName("TransTime")]
	public string TransTime { get; set; } = string.Empty;

	[JsonPropertyName("TransAmount")]
	public string TransAmount { get; set; } = string.Empty;

	[JsonPropertyName("BusinessShortCode")]
	public string BusinessShortCode { get; set; } = string.Empty;

	[JsonPropertyName("BillRefNumber")]
	public string BillRefNumber { get; set; } = string.Empty;

	[JsonPropertyName("InvoiceNumber")]
	public string InvoiceNumber { get; set; } = string.Empty;

	[JsonPropertyName("OrgAccountBalance")]
	public string OrgAccountBalance { get; set; } = string.Empty;

	[JsonPropertyName("ThirdPartyTransID")]
	public string ThirdPartyTransId { get; set; } = string.Empty;

	[JsonPropertyName("MSISDN")]
	public string PhoneNumber { get; set; } = string.Empty;

	[JsonPropertyName("FirstName")]
	public string FirstName { get; set; } = string.Empty;

	[JsonPropertyName("MiddleName")]
	public string MiddleName { get; set; } = string.Empty;

	[JsonPropertyName("LastName")]
	public string LastName { get; set; } = string.Empty;
}

public class C2BValidationResponse
{
	[JsonPropertyName("ResultCode")]
	public string ResultCode { get; set; } = "0";

	[JsonPropertyName("ResultDesc")]
	public string ResultDesc { get; set; } = "Accepted";
}

public class C2BConfirmationRequest
{
	[JsonPropertyName("TransactionType")]
	public string TransactionType { get; set; } = string.Empty;

	[JsonPropertyName("TransID")]
	public string TransactionId { get; set; } = string.Empty;

	[JsonPropertyName("TransTime")]
	public string TransTime { get; set; } = string.Empty;

	[JsonPropertyName("TransAmount")]
	public string TransAmount { get; set; } = string.Empty;

	[JsonPropertyName("BusinessShortCode")]
	public string BusinessShortCode { get; set; } = string.Empty;

	[JsonPropertyName("BillRefNumber")]
	public string BillRefNumber { get; set; } = string.Empty;

	[JsonPropertyName("InvoiceNumber")]
	public string InvoiceNumber { get; set; } = string.Empty;

	[JsonPropertyName("OrgAccountBalance")]
	public string OrgAccountBalance { get; set; } = string.Empty;

	[JsonPropertyName("ThirdPartyTransID")]
	public string ThirdPartyTransId { get; set; } = string.Empty;

	[JsonPropertyName("MSISDN")]
	public string PhoneNumber { get; set; } = string.Empty;

	[JsonPropertyName("FirstName")]
	public string FirstName { get; set; } = string.Empty;

	[JsonPropertyName("MiddleName")]
	public string MiddleName { get; set; } = string.Empty;

	[JsonPropertyName("LastName")]
	public string LastName { get; set; } = string.Empty;
}

// ─── Pull Transactions ────────────────────────────────────────────────────────

public class PullTransactionRequest
{
	[JsonPropertyName("ShortCode")]
	public string ShortCode { get; set; } = string.Empty;

	[JsonPropertyName("StartDate")]
	public string StartDate { get; set; } = string.Empty;   // yyyyMMddHHmmss

	[JsonPropertyName("EndDate")]
	public string EndDate { get; set; } = string.Empty;

	[JsonPropertyName("Offset")]
	public int Offset { get; set; } = 0;
}

public class PullTransactionResponse
{
	[JsonPropertyName("ResponseCode")]
	public string ResponseCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseMessage")]
	public string ResponseMessage { get; set; } = string.Empty;

	[JsonPropertyName("Response")]
	public List<PullTransaction> Transactions { get; set; } = [];
}

public class PullTransaction
{
	[JsonPropertyName("receipt_no")]
	public string ReceiptNo { get; set; } = string.Empty;

	[JsonPropertyName("completion_time")]
	public string CompletionTime { get; set; } = string.Empty;

	[JsonPropertyName("initiation_time")]
	public string InitiationTime { get; set; } = string.Empty;

	[JsonPropertyName("sender_phone")]
	public string SenderPhone { get; set; } = string.Empty;

	[JsonPropertyName("till_number")]
	public string TillNumber { get; set; } = string.Empty;

	[JsonPropertyName("amount")]
	public decimal Amount { get; set; }

	[JsonPropertyName("system_trace_audit_number")]
	public string SystemTraceAuditNumber { get; set; } = string.Empty;

	[JsonPropertyName("bill_reference_number")]
	public string BillReferenceNumber { get; set; } = string.Empty;
}

// ─── Shared Result Wrapper ────────────────────────────────────────────────────

public class DarajaResult<T>
{
	public bool Success { get; set; }
	public string? ErrorMessage { get; set; }
	public T? Data { get; set; }

	public static DarajaResult<T> Ok(T data) => new() { Success = true, Data = data };
	public static DarajaResult<T> Fail(string error) => new() { Success = false, ErrorMessage = error };
}