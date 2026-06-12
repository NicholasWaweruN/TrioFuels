using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja.DarajaTokenService;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Safaricom_Daraja.Stk_Push;

/// <summary>
/// Wraps StkPushService with full CSV diagnostic logging.
/// Every STK Push attempt writes one row to daraja_diagnostics.csv
/// so you can see exactly what was sent vs what Safaricom rejected.
/// Drop this file next to StkPushService.cs and register it in DI.
/// </summary>
public sealed class StkPushDiagnosticService(
	IHttpClientFactory httpFactory,
	IDarajaTokenService tokenService,
	IOptions<DarajaConfig> options,
	ILogger<StkPushDiagnosticService> logger)
{
	private readonly DarajaConfig _cfg = options.Value;
	private static readonly string CsvPath = Path.Combine(
		AppContext.BaseDirectory, "daraja_diagnostics.csv");

	// ── CSV header ────────────────────────────────────────────────────────────
	private static readonly string[] Headers =
	[
		"Timestamp_EAT",
		"AttemptUtc",
		"ConfigBaseUrl",
		"ConfigBusinessShortCode",
		"ConfigPassKeyLength",
		"ConfigPassKeyFirst8",
		"ConfigPassKeyLast8",
		"ConfigStkCallbackUrl",
		"ConfigTillsCount",

        // Till resolved from config
        "TillName",
		"TillNumber",
		"StoreNumber",
		"TillAccountReference",

        // Payload fields sent to Safaricom
        "Payload_TransactionType",
		"Payload_BusinessShortCode",
		"Payload_PartyB",
		"Payload_PartyA",
		"Payload_PhoneNumber",
		"Payload_Amount",
		"Payload_CallBackURL",
		"Payload_AccountReference",
		"Payload_TransactionDesc",
		"Payload_Timestamp",
		"Payload_PasswordLength",
		"Payload_PasswordFirst16",
		"Payload_PasswordRaw_ShortCode",   // the shortcode used to BUILD the password

        // Token
        "AccessToken_First16",
		"AccessToken_Length",

        // HTTP
        "HTTP_Method",
		"HTTP_Url",
		"HTTP_StatusCode",

        // Response
        "Response_Raw",
		"Response_ResponseCode",
		"Response_ResponseDescription",
		"Response_MerchantRequestId",
		"Response_CheckoutRequestId",
		"Response_CustomerMessage",
		"Response_ErrorCode",
		"Response_ErrorMessage",

        // Result
        "Result_Success",
		"Result_FailReason"
	];

	// ─────────────────────────────────────────────────────────────────────────

	public async Task RunDiagnosticAsync(
		string phone,
		long amount,
		string tillNumber,
		string accountReference = "TEST",
		string description = "DiagTest",
		CancellationToken ct = default)
	{
		EnsureCsvHeader();

		var row = new Dictionary<string, string>(StringComparer.Ordinal);
		var attemptUtc = DateTime.UtcNow;

		row["AttemptUtc"] = attemptUtc.ToString("yyyy-MM-dd HH:mm:ss");

		// ── Config snapshot ───────────────────────────────────────────────────
		row["ConfigBaseUrl"] = _cfg.BaseUrl ?? "NULL";
		row["ConfigBusinessShortCode"] = _cfg.BusinessShortCode ?? "NULL";
		row["ConfigPassKeyLength"] = (_cfg.PassKey?.Length ?? 0).ToString();
		row["ConfigPassKeyFirst8"] = SafeSubstring(_cfg.PassKey, 0, 8);
		row["ConfigPassKeyLast8"] = SafeEnd(_cfg.PassKey, 8);
		row["ConfigStkCallbackUrl"] = _cfg.StkCallbackUrl ?? "NULL";
		row["ConfigTillsCount"] = (_cfg.Tills?.Count ?? 0).ToString();

		// ── Resolve till ──────────────────────────────────────────────────────
		var till = _cfg.Tills?.FirstOrDefault(t => t.TillNumber == tillNumber);
		row["TillName"] = till?.Name ?? "NOT_FOUND";
		row["TillNumber"] = till?.TillNumber ?? tillNumber;
		row["StoreNumber"] = till?.StoreNumber ?? "NULL";
		row["TillAccountReference"] = till?.AccountReference ?? "NULL";

		if (till is null)
		{
			row["Result_Success"] = "FALSE";
			row["Result_FailReason"] = $"Till {tillNumber} not in config";
			WriteCsvRow(row);
			logger.LogError("DIAGNOSTIC: Till {Till} not found in config", tillNumber);
			return;
		}

		// ── Build credentials — TEST BOTH VARIANTS ────────────────────────────
		// Variant A: password from HeadOffice shortcode (4161705) — CORRECT per Safaricom docs
		// Variant B: password from till number — what the old buggy code did
		// We always send Variant A; we log both so you can confirm the difference.

		var (timestampA, passwordA) = BuildCredentials(_cfg.BusinessShortCode ?? "");   // ✅ correct
		var (_, passwordB) = BuildCredentials(till.TillNumber);           // ❌ old bug

		row["Timestamp_EAT"] = timestampA;
		row["Payload_PasswordRaw_ShortCode"] = _cfg.BusinessShortCode ?? ""; // what we used
		row["Payload_PasswordLength"] = passwordA.Length.ToString();
		row["Payload_PasswordFirst16"] = SafeSubstring(passwordA, 0, 16);

		logger.LogInformation(
			"DIAGNOSTIC: PasswordA (HO={HO}) first16={A16} | PasswordB (Till={Till}) first16={B16}",
			_cfg.BusinessShortCode, SafeSubstring(passwordA, 0, 16),
			till.TillNumber, SafeSubstring(passwordB, 0, 16));

		// ── Sanitize phone ────────────────────────────────────────────────────
		string sanitizedPhone;
		try
		{
			sanitizedPhone = SanitizePhone(phone);
		}
		catch (Exception ex)
		{
			row["Result_Success"] = "FALSE";
			row["Result_FailReason"] = $"Phone sanitize failed: {ex.Message}";
			WriteCsvRow(row);
			return;
		}

		var safeRef = Truncate(accountReference, 12);
		var safeDesc = Truncate(description, 13);

		// ── Build payload ─────────────────────────────────────────────────────
		var payload = new StkPushRequest
		{
			TransactionType = "CustomerBuyGoodsOnline",
			BusinessShortCode = _cfg.BusinessShortCode ?? "",  // ✅ head office (4161705)
			PartyB = till.TillNumber,          // ✅ till receives the funds (5617668)
			Password = passwordA,                // ALWAYS from head-office shortcode
			Timestamp = timestampA,
			Amount = amount,
			PartyA = sanitizedPhone,
			PhoneNumber = sanitizedPhone,
			CallBackURL = _cfg.StkCallbackUrl ?? "",
			AccountReference = safeRef,
			TransactionDesc = safeDesc
		};

		row["Payload_TransactionType"] = payload.TransactionType;
		row["Payload_BusinessShortCode"] = payload.BusinessShortCode ?? "";
		row["Payload_PartyB"] = payload.PartyB;
		row["Payload_PartyA"] = payload.PartyA;
		row["Payload_PhoneNumber"] = payload.PhoneNumber;
		row["Payload_Amount"] = payload.Amount.ToString();
		row["Payload_CallBackURL"] = payload.CallBackURL;
		row["Payload_AccountReference"] = payload.AccountReference;
		row["Payload_TransactionDesc"] = payload.TransactionDesc;
		row["Payload_Timestamp"] = payload.Timestamp;

		var payloadJson = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = false });
		logger.LogWarning("DIAGNOSTIC PAYLOAD (exact JSON): {Json}", payloadJson);

		// ── Get token ─────────────────────────────────────────────────────────
		string token;
		try
		{
			token = await tokenService.GetAccessTokenAsync(ct);
			row["AccessToken_Length"] = token.Length.ToString();
			row["AccessToken_First16"] = SafeSubstring(token, 0, 16);
			logger.LogInformation("DIAGNOSTIC: Token acquired length={Len}", token.Length);
		}
		catch (Exception ex)
		{
			row["Result_Success"] = "FALSE";
			row["Result_FailReason"] = $"Token fetch failed: {ex.Message}";
			WriteCsvRow(row);
			logger.LogError(ex, "DIAGNOSTIC: Token fetch failed");
			return;
		}

		// ── HTTP call ─────────────────────────────────────────────────────────
		var url = "/mpesa/stkpush/v1/processrequest";
		row["HTTP_Method"] = "POST";
		row["HTTP_Url"] = (_cfg.BaseUrl?.TrimEnd('/') ?? "") + url;

		try
		{
			var client = httpFactory.CreateClient("Daraja");
			client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);

			logger.LogWarning(
				"DIAGNOSTIC: Sending to {Url} | BaseUrl={BaseUrl}",
				row["HTTP_Url"], _cfg.BaseUrl);

			var response = await client.PostAsJsonAsync(url, payload, ct);
			var rawBody = await response.Content.ReadAsStringAsync(ct);

			row["HTTP_StatusCode"] = ((int)response.StatusCode).ToString();
			row["Response_Raw"] = rawBody.Replace("\n", " ").Replace("\r", "");

			logger.LogWarning(
				"DIAGNOSTIC RESPONSE: Status={Status} Body={Body}",
				(int)response.StatusCode, rawBody);

			// Try parse as success
			if (response.IsSuccessStatusCode)
			{
				var success = JsonSerializer.Deserialize<StkPushResponse>(rawBody,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				row["Response_ResponseCode"] = success?.ResponseCode ?? "";
				row["Response_ResponseDescription"] = success?.ResponseDescription ?? "";
				row["Response_MerchantRequestId"] = success?.MerchantRequestId ?? "";
				row["Response_CheckoutRequestId"] = success?.CheckoutRequestId ?? "";
				row["Response_CustomerMessage"] = success?.CustomerMessage ?? "";
				row["Result_Success"] = success?.ResponseCode == "0" ? "TRUE" : "FALSE";
				row["Result_FailReason"] = success?.ResponseCode == "0"
					? "" : (success?.ResponseDescription ?? "Non-zero ResponseCode");
			}
			else
			{
				// Parse error body
				try
				{
					var err = JsonSerializer.Deserialize<DarajaErrorResponse>(rawBody,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
					row["Response_ErrorCode"] = err?.ErrorCode ?? "";
					row["Response_ErrorMessage"] = err?.ErrorMessage ?? "";
				}
				catch
				{
					row["Response_ErrorCode"] = "PARSE_FAIL";
					row["Response_ErrorMessage"] = rawBody;
				}

				row["Result_Success"] = "FALSE";
				row["Result_FailReason"] = $"HTTP {(int)response.StatusCode}: {row["Response_ErrorCode"]} {row["Response_ErrorMessage"]}";
			}
		}
		catch (Exception ex)
		{
			row["Result_Success"] = "FALSE";
			row["Result_FailReason"] = $"HTTP exception: {ex.Message}";
			logger.LogError(ex, "DIAGNOSTIC: HTTP call threw exception");
		}

		WriteCsvRow(row);

		logger.LogWarning(
			"DIAGNOSTIC COMPLETE: Success={Ok} Reason={Reason} — CSV written to {Path}",
			row["Result_Success"], row.GetValueOrDefault("Result_FailReason"), CsvPath);
	}

	// ── Helpers ───────────────────────────────────────────────────────────────

	private (string Timestamp, string Password) BuildCredentials(string shortCode)
	{
		var timestamp = DateTimeOffset.UtcNow
			.ToOffset(TimeSpan.FromHours(3))
			.ToString("yyyyMMddHHmmss");

		var raw = $"{shortCode}{_cfg.PassKey}{timestamp}";
		var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
		return (timestamp, password);
	}

	private static string SanitizePhone(string phone)
	{
		phone = phone.Trim().Replace("+", "").Replace(" ", "");
		if (phone.StartsWith("07") || phone.StartsWith("01"))
			phone = "254" + phone[1..];
		if (!Regex.IsMatch(phone, @"^254\d{9}$"))
			throw new ArgumentException($"Invalid format: {phone}");
		return phone;
	}

	private static string Truncate(string value, int maxLength) =>
		value.Length > maxLength ? value[..maxLength] : value;

	private static string SafeSubstring(string? s, int start, int len) =>
		string.IsNullOrEmpty(s) ? "NULL"
		: s.Length > start ? s.Substring(start, Math.Min(len, s.Length - start))
		: "TOO_SHORT";

	private static string SafeEnd(string? s, int len) =>
		string.IsNullOrEmpty(s) ? "NULL"
		: s.Length >= len ? s[^len..] : s;

	private void EnsureCsvHeader()
	{
		if (!File.Exists(CsvPath))
		{
			File.WriteAllText(CsvPath, string.Join(",", Headers.Select(QuoteCsv)) + "\n");
			logger.LogInformation("DIAGNOSTIC: Created CSV at {Path}", CsvPath);
		}
	}

	private void WriteCsvRow(Dictionary<string, string> row)
	{
		var values = Headers.Select(h => QuoteCsv(row.GetValueOrDefault(h, "")));
		File.AppendAllText(CsvPath, string.Join(",", values) + "\n");
	}

	private static string QuoteCsv(string value) =>
		"\"" + (value ?? "").Replace("\"", "\"\"") + "\"";
}

/// <summary>Helper DTO to deserialise Daraja 4xx error bodies.</summary>
public sealed class DarajaErrorResponse
{
	public string? RequestId { get; set; }
	public string? ErrorCode { get; set; }
	public string? ErrorMessage { get; set; }
}