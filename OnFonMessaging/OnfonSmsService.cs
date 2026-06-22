using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnFonMessaging;
using System.Net.Http.Json;
using System.Text.Json;

namespace OnfonSms;

// ── Interface ─────────────────────────────────────────────────────────────────

public interface ISmsService
{
    /// <summary>Send to a single recipient.</summary>
    Task<SmsResponse> SendAsync(string phoneNumber, string message, CancellationToken ct = default);

    /// <summary>Send the same message to multiple recipients.</summary>
    Task<SmsResponse> SendBulkAsync(IEnumerable<string> phoneNumbers, string message, CancellationToken ct = default);

    /// <summary>Send individual messages to each recipient (personalised text).</summary>
    Task<SmsResponse> SendPersonalisedAsync(IEnumerable<SmsMessage> messages, CancellationToken ct = default);

    /// <summary>Schedule a message for later delivery.</summary>
    Task<SmsResponse> SendScheduledAsync(string phoneNumber, string message, DateTime scheduleAt, CancellationToken ct = default);
}

// ── Implementation ────────────────────────────────────────────────────────────

public class OnfonSmsService : ISmsService
{
    private readonly HttpClient _http;
    private readonly OnfonSettings _settings;
    private readonly ILogger<OnfonSmsService> _logger;

    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNamingPolicy = null   // keep PascalCase — Onfon API expects it
    };

    public OnfonSmsService(
        HttpClient http,
        IOptions<OnfonSettings> settings,
        ILogger<OnfonSmsService> logger)
    {
        _http = http;
        _settings = settings.Value;
        _logger = logger;
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public Task<SmsResponse> SendAsync(string phoneNumber, string message, CancellationToken ct = default)
        => SendPersonalisedAsync([new SmsMessage { Number = Normalise(phoneNumber), Text = message }], ct);

    public Task<SmsResponse> SendBulkAsync(IEnumerable<string> phoneNumbers, string message, CancellationToken ct = default)
    {
        var messages = phoneNumbers
            .Select(n => new SmsMessage { Number = Normalise(n), Text = message })
            .ToList();

        return SendPersonalisedAsync(messages, ct);
    }

    public Task<SmsResponse> SendPersonalisedAsync(IEnumerable<SmsMessage> messages, CancellationToken ct = default)
        => PostAsync(BuildRequest(messages.ToList(), scheduleAt: null), ct);

    public Task<SmsResponse> SendScheduledAsync(string phoneNumber, string message, DateTime scheduleAt, CancellationToken ct = default)
    {
        var msgs = new List<SmsMessage>
        {
            new() { Number = Normalise(phoneNumber), Text = message }
        };
        return PostAsync(BuildRequest(msgs, scheduleAt), ct);
    }

    // ── Core POST ─────────────────────────────────────────────────────────────

    private async Task<SmsResponse> PostAsync(SmsRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Sending {Count} SMS via Onfon to: {Numbers}",
            request.MessageParameters.Count,
            string.Join(", ", request.MessageParameters.Select(m => m.Number)));

        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await _http.PostAsJsonAsync(string.Empty, request, _jsonOpts, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Onfon HTTP request failed");
            throw;
        }

        var body = await httpResponse.Content.ReadAsStringAsync(ct);
        _logger.LogDebug("Onfon raw response: {Body}", body);

        if (!httpResponse.IsSuccessStatusCode)
        {
            _logger.LogError("Onfon returned HTTP {Status}: {Body}", httpResponse.StatusCode, body);
            throw new HttpRequestException($"Onfon API error {httpResponse.StatusCode}: {body}");
        }

        var result = JsonSerializer.Deserialize<SmsResponse>(body, _jsonOpts)
                     ?? throw new InvalidOperationException("Empty response from Onfon API");

        if (result.ErrorCode != 0)
        {
            _logger.LogWarning("Onfon API error {Code}: {Description}", result.ErrorCode, result.ErrorDescription);
        }
        else
        {
            _logger.LogInformation("Onfon SMS sent. MessageIds: {Ids}",
                string.Join(", ", result.Data.Select(d => d.MessageId)));
        }

        return result;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private SmsRequest BuildRequest(List<SmsMessage> messages, DateTime? scheduleAt) => new()
    {
        SenderId = _settings.SenderId,
        ApiKey = _settings.ApiKey,
        ClientId = _settings.ClientId,
        IsUnicode = false,
        IsFlash = false,
        ScheduleDateTime = scheduleAt.HasValue
            ? scheduleAt.Value.ToString("yyyy-MM-dd HH:mm")
            : null,
        MessageParameters = messages
    };

    /// <summary>
    /// Normalise Kenyan numbers to international format 254XXXXXXXXX.
    /// Handles: 07XXXXXXXX, +254XXXXXXXXX, 254XXXXXXXXX
    /// </summary>
    private static string Normalise(string phone)
    {
        phone = phone.Trim().Replace(" ", "").Replace("-", "");

        if (phone.StartsWith('+'))
            phone = phone[1..];

        if (phone.StartsWith("07") || phone.StartsWith("01"))
            phone = "254" + phone[1..];

        return phone;
    }
}
