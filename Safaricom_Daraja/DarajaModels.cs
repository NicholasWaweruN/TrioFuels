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

	// ── CRUCIAL ORG-TO-ORG AGGREGATOR FALLBACK FIELDS ──

	[JsonPropertyName("CommandID")]
	public string? CommandID { get; set; }

	[JsonPropertyName("InitiatorReceiverType")]
	public string? InitiatorReceiverType { get; set; }

	[JsonPropertyName("TransNo")]
	public string? TransNo { get; set; }

	[JsonPropertyName("ConversationID")]
	public string? ConversationID { get; set; }

	[JsonPropertyName("OriginatorConversationID")]
	public string? OriginatorConversationID { get; set; }
}


// ─── Pull Transactions ────────────────────────────────────────────────────────

/// <summary>
/// Request body for POST {base_uri}/pulltransactions/v1/register
/// Called once per shortcode/till to activate Pull. Safe to call repeatedly —
/// Safaricom returns ResponseCode 1001 ("already registered") if it's already active.
/// </summary>
public class PullRegisterRequest
{
	[JsonPropertyName("ShortCode")]
	public string ShortCode { get; set; } = string.Empty;

	[JsonPropertyName("RequestType")]
	public string RequestType { get; set; } = "Pull";

	[JsonPropertyName("NominatedNumber")]
	public string NominatedNumber { get; set; } = string.Empty;   // full MSISDN, e.g. "2547XXXXXXXX"

	[JsonPropertyName("CallBackURL")]
	public string CallBackURL { get; set; } = string.Empty;
}

/// <summary>
/// Response body from POST {base_uri}/pulltransactions/v1/register
/// Sample codes: 1000 = registered successfully, 1001 = shortcode already registered.
/// NOTE: Safaricom's own sample response uses "Response Status" and
/// "Response Description" — WITH spaces in the JSON key names. This is not a typo here;
/// it matches their documented sample verbatim.
/// </summary>
public class PullRegisterResponse
{
	[JsonPropertyName("ResponseRefID")]
	public string ResponseRefId { get; set; } = string.Empty;

	[JsonPropertyName("Response Status")]
	public string ResponseStatus { get; set; } = string.Empty;

	[JsonPropertyName("ShortCode")]
	public string ShortCode { get; set; } = string.Empty;

	[JsonPropertyName("Response Description")]
	public string ResponseDescription { get; set; } = string.Empty;

	public bool IsRegistered => ResponseStatus is "1000" or "1001";
}

/// <summary>
/// Request body for POST {base_uri}/pulltransactions/v1/query
/// Per Safaricom's docs, ShortCode here IS the till/paybill number you want
/// transactions for — there is no separate till-filter field. To pull for
/// multiple tills, call this once per till with that till's number as ShortCode.
/// </summary>
public class PullTransactionRequest
{
	[JsonPropertyName("ShortCode")]
	public string ShortCode { get; set; } = string.Empty;

	[JsonPropertyName("StartDate")]
	public string StartDate { get; set; } = string.Empty;   // yyyy-MM-dd HH:mm:ss

	[JsonPropertyName("EndDate")]
	public string EndDate { get; set; } = string.Empty;     // yyyy-MM-dd HH:mm:ss

	// FIX: Safaricom's documented payload sends this as a quoted string,
	// e.g. "OffSetValue":"0" — not a bare JSON number.
	[JsonPropertyName("OffSetValue")]
	public string OffSetValue { get; set; } = "0";
}

/// <summary>
/// Response body from POST {base_uri}/pulltransactions/v1/query
///
/// Response codes: 1000 = success, 1001 = success but nothing in this window,
/// 500 = shortcode has no available transactions at all.
///
/// IMPORTANT: "Response" is a NESTED array — an array containing one array of
/// transaction objects, e.g. { "Response": [ [ {...}, {...} ] ] } — not a flat
/// list. Deserializing straight into a flat List&lt;PullTransaction&gt; throws
/// a JsonException at runtime.
/// </summary>
public class PullTransactionResponse
{
	[JsonPropertyName("ResponseRefID")]
	public string ResponseRefId { get; set; } = string.Empty;

	[JsonPropertyName("ResponseCode")]
	public string ResponseCode { get; set; } = string.Empty;

	[JsonPropertyName("ResponseMessage")]
	public string ResponseMessage { get; set; } = string.Empty;

	[JsonPropertyName("Response")]
	public List<List<PullTransaction>>? Transactions { get; set; }

	/// <summary>Flattens the nested "Response" array into a single list.</summary>
	public List<PullTransaction> FlattenTransactions() =>
		Transactions?.SelectMany(page => page).ToList() ?? [];

	public bool IsSuccess => ResponseCode == "1000";

	public bool IsEmptyWindow => ResponseCode == "1001";
}

/// <summary>
/// A single C2B transaction record as returned inside the "Response" array.
/// Field names match Safaricom's actual documented sample payload.
/// </summary>
public class PullTransaction
{
	[JsonPropertyName("transactionId")]
	public string ReceiptNo { get; set; } = string.Empty;

	[JsonPropertyName("trxDate")]
	public string CompletionTime { get; set; } = string.Empty;

	[JsonPropertyName("msisdn")]
	public string SenderPhone { get; set; } = string.Empty;

	[JsonPropertyName("sender")]
	public string Sender { get; set; } = string.Empty;

	[JsonPropertyName("transactiontype")]
	public string TransactionType { get; set; } = string.Empty;

	[JsonPropertyName("billreference")]
	public string BillReferenceNumber { get; set; } = string.Empty;

	// FIX: Safaricom's sample sends amount as a quoted string ("amount": "168.00"),
	// not a JSON number. Kept as string; use GetAmountDecimal() to parse safely.
	[JsonPropertyName("amount")]
	public string Amount { get; set; } = "0";

	[JsonPropertyName("organizationname")]
	public string OrganizationName { get; set; } = string.Empty;

	/// <summary>Best-effort parse of Amount. Returns 0m if missing/malformed.</summary>
	public decimal GetAmountDecimal() =>
		decimal.TryParse(Amount, System.Globalization.NumberStyles.Number,
			System.Globalization.CultureInfo.InvariantCulture, out var parsed)
			? parsed
			: 0m;

	/// <summary>Best-effort parse of the trxDate ISO-8601 timestamp. Returns null if malformed.</summary>
	public DateTime? GetCompletionTimeUtc() =>
		DateTime.TryParse(CompletionTime, System.Globalization.CultureInfo.InvariantCulture,
			System.Globalization.DateTimeStyles.AdjustToUniversal | System.Globalization.DateTimeStyles.AssumeUniversal,
			out var parsed)
			? parsed
			: null;
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