namespace OnfonSms;

// ── Request ──────────────────────────────────────────────────────────────────

public class SmsRequest
{
    public string SenderId { get; set; } = string.Empty;
    public bool IsUnicode { get; set; } = false;
    public bool IsFlash { get; set; } = false;
    public string? ScheduleDateTime { get; set; }   // "yyyy-MM-dd HH:mm" or null
    public List<SmsMessage> MessageParameters { get; set; } = new();
    public string ApiKey { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
}

public class SmsMessage
{
    public string Number { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

// ── Response ─────────────────────────────────────────────────────────────────

public class SmsResponse
{
    public int ErrorCode { get; set; }
    public string ErrorDescription { get; set; } = string.Empty;
    public List<SmsResult> Data { get; set; } = new();
}

public class SmsResult
{
    public string MobileNumber { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
}

// ── Settings (bind from appsettings.json) ────────────────────────────────────

public class OnfonSettings
{
    public const string Section = "Onfon";

    public string ApiKey { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SenderId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://api.onfonmedia.co.ke/v1/sms/SendBulkSMS";
}
