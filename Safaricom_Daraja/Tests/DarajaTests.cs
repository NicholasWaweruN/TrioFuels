//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Safaricom_Daraja.DarajaTokenService;
//using Safaricom_Daraja.Stk_Push;
//using Xunit;
//using Xunit.Abstractions;

//namespace Safaricom_Daraja.Tests.Production;

///// <summary>
///// Production integration tests for StkPushService against the live Daraja API.
/////
///// Prerequisites:
/////   1. Set environment variables before running (never hardcode credentials):
/////      DARAJA_CONSUMER_KEY      = your production Consumer Key
/////      DARAJA_CONSUMER_SECRET   = your production Consumer Secret
/////      DARAJA_PASSKEY           = your production Passkey
/////      DARAJA_CALLBACK_URL      = your public Railway callback URL
/////      DARAJA_TEST_PHONE        = a real Safaricom number you own (2547XXXXXXXXX)
/////
/////   2. Run with: dotnet test --filter Category=Production
/////      This keeps prod tests isolated from CI unit tests.
///// </summary>
//[Trait("Category", "Production")]
//public class StkPushServiceProductionTests : IAsyncLifetime
//{
//    private readonly ITestOutputHelper _output;
//    private IStkPushService _stkPushService = null!;
//    private ServiceProvider _serviceProvider = null!;

//    // ── Trio Fuels Till Numbers (from Falcon portal) ──────────────────────────
//    // Use Station 1 till for testing — swap to any active till as needed.
//    private const string TestTill = "5617660";

//    // Populated from env var in InitializeAsync
//    private string _testPhone = null!;

//    public StkPushServiceProductionTests(ITestOutputHelper output)
//    {
//        _output = output;
//    }

//    // ─── Setup ────────────────────────────────────────────────────────────────

//    public Task InitializeAsync()
//    {
//        var consumerKey = GetRequiredEnv("DARAJA_CONSUMER_KEY");
//        var consumerSecret = GetRequiredEnv("DARAJA_CONSUMER_SECRET");
//        var passKey = GetRequiredEnv("DARAJA_PASSKEY");
//        var callbackUrl = GetRequiredEnv("DARAJA_CALLBACK_URL");
//        _testPhone = GetRequiredEnv("DARAJA_TEST_PHONE");

//        var services = new ServiceCollection();

//        // Daraja config — production values from env
//        services.Configure<DarajaConfig>(cfg =>
//        {
//            cfg.ConsumerKey = consumerKey;
//            cfg.ConsumerSecret = consumerSecret;
//            cfg.PassKey = passKey;
//            cfg.StkCallbackUrl = callbackUrl;
//            cfg.BaseUrl = "https://api.safaricom.co.ke"; // production
//        });

//        // Named HttpClient for Daraja
//        services.AddHttpClient("Daraja", (sp, client) =>
//        {
//            var cfg = sp.GetRequiredService<IOptions<DarajaConfig>>().Value;
//            client.BaseAddress = new Uri(cfg.BaseUrl);
//            client.Timeout = TimeSpan.FromSeconds(30);
//        });

//        services.AddSingleton<IDarajaTokenService, DarajaTokenService>();
//        services.AddSingleton<IStkPushService, StkPushService>();
//        services.AddLogging(b => b.AddXUnit(_output).SetMinimumLevel(LogLevel.Debug));

//        _serviceProvider = services.BuildServiceProvider();
//        _stkPushService = _serviceProvider.GetRequiredService<IStkPushService>();

//        return Task.CompletedTask;
//    }

//    public async ValueTask DisposeAsync()
//    {
//        await _serviceProvider.DisposeAsync();
//    }

//    // ─── Tests ────────────────────────────────────────────────────────────────

//    /// <summary>
//    /// Happy path: sends a real KES 1 STK Push prompt to your phone.
//    /// Check your phone — you should receive the M-Pesa PIN prompt within ~5 seconds.
//    /// </summary>
//    [Fact(DisplayName = "InitiateAsync → sends STK prompt to phone for KES 1")]
//    public async Task InitiateAsync_ValidRequest_SendsPromptToPhone()
//    {
//        // Arrange
//        var accountRef = "FUELFLOW-T1";
//        var description = "Test Payment";

//        // Act
//        var result = await _stkPushService.InitiateAsync(
//            phone: _testPhone,
//            amount: 1,
//            tillNumber: TestTill,
//            accountReference: accountRef,
//            description: description);

//        // Assert
//        _output.WriteLine($"Success       : {result.Success}");
//        _output.WriteLine($"Error         : {result.Error ?? "none"}");
//        _output.WriteLine($"CheckoutReqID : {result.Data?.CheckoutRequestId ?? "N/A"}");
//        _output.WriteLine($"MerchantReqID : {result.Data?.MerchantRequestId ?? "N/A"}");
//        _output.WriteLine($"ResponseCode  : {result.Data?.ResponseCode ?? "N/A"}");
//        _output.WriteLine($"Description   : {result.Data?.ResponseDescription ?? "N/A"}");

//        Assert.True(result.Success, $"STK Push failed: {result.Error}");
//        Assert.NotNull(result.Data);
//        Assert.Equal("0", result.Data.ResponseCode);
//        Assert.False(string.IsNullOrWhiteSpace(result.Data.CheckoutRequestId));
//        Assert.False(string.IsNullOrWhiteSpace(result.Data.MerchantRequestId));
//    }

//    /// <summary>
//    /// Queries the status of a push that was just initiated.
//    /// Safaricom may return 1032 (cancelled) or 0 (success) depending on what
//    /// you do with the PIN prompt. Either is a valid response — the point is
//    /// that the query round-trip succeeds and returns a parseable result.
//    /// </summary>
//    [Fact(DisplayName = "QueryStatusAsync → returns result for a real CheckoutRequestID")]
//    public async Task QueryStatusAsync_AfterInitiate_ReturnsParsedStatus()
//    {
//        // Arrange — initiate first to get a real CheckoutRequestID
//        var initResult = await _stkPushService.InitiateAsync(
//            phone: _testPhone,
//            amount: 1,
//            tillNumber: TestTill,
//            accountReference: "FUELFLOW-QRY",
//            description: "Query Test");

//        Assert.True(initResult.Success, $"Initiation failed, cannot test query: {initResult.Error}");

//        var checkoutRequestId = initResult.Data!.CheckoutRequestId;
//        _output.WriteLine($"CheckoutRequestID: {checkoutRequestId}");

//        // Give Safaricom a moment to register the transaction
//        await Task.Delay(TimeSpan.FromSeconds(3));

//        // Act
//        var queryResult = await _stkPushService.QueryStatusAsync(
//            checkoutRequestId: checkoutRequestId,
//            tillNumber: TestTill);

//        // Assert
//        _output.WriteLine($"Query Success   : {queryResult.Success}");
//        _output.WriteLine($"Query Error     : {queryResult.Error ?? "none"}");
//        _output.WriteLine($"ResultCode      : {queryResult.Data?.ResultCode ?? "N/A"}");
//        _output.WriteLine($"ResultDesc      : {queryResult.Data?.ResultDesc ?? "N/A"}");

//        Assert.True(queryResult.Success, $"STK Query failed: {!queryResult.Success}");
//        Assert.NotNull(queryResult.Data);
//        // ResultCode 0 = paid, 1032 = cancelled by user, 1037 = timeout — all valid for a test
//        Assert.False(string.IsNullOrWhiteSpace(queryResult.Data.ResultCode));
//    }

//    /// <summary>
//    /// Validates that a zero-amount request is rejected before hitting the API.
//    /// </summary>
//    [Fact(DisplayName = "InitiateAsync → rejects amount of zero without calling API")]
//    public async Task InitiateAsync_ZeroAmount_ReturnsFail()
//    {
//        var result = await _stkPushService.InitiateAsync(
//            phone: _testPhone,
//            amount: 0,
//            tillNumber: TestTill,
//            accountReference: "ZERO-TEST");

//        Assert.False(result.Success);
//        Assert.Contains("greater than zero", result.Error, StringComparison.OrdinalIgnoreCase);
//    }

//    /// <summary>
//    /// Validates that a missing till number is rejected before hitting the API.
//    /// </summary>
//    [Fact(DisplayName = "InitiateAsync → rejects empty till number without calling API")]
//    public async Task InitiateAsync_EmptyTill_ReturnsFail()
//    {
//        var result = await _stkPushService.InitiateAsync(
//            phone: _testPhone,
//            amount: 1,
//            tillNumber: "   ",
//            accountReference: "TILL-TEST");

//        Assert.False(result.Success);
//        Assert.Contains("till number", result.Error, StringComparison.OrdinalIgnoreCase);
//    }

//    /// <summary>
//    /// Validates phone normalisation — 07XXXXXXXXX format should be accepted and normalised.
//    /// </summary>
//    [Fact(DisplayName = "InitiateAsync → accepts 07XXXXXXXXX phone format")]
//    public async Task InitiateAsync_ShortPhoneFormat_NormalisesAndSucceeds()
//    {
//        // Convert 2547XXXXXXXXX → 07XXXXXXXXX for this test
//        var shortPhone = "0" + _testPhone[3..]; // strips "254", prepends "0"

//        var result = await _stkPushService.InitiateAsync(
//            phone: shortPhone,
//            amount: 1,
//            tillNumber: TestTill,
//            accountReference: "PHONE-FMT",
//            description: "Format Test");

//        _output.WriteLine($"Success: {result.Success}");
//        _output.WriteLine($"Error  : {result.Error ?? "none"}");

//        Assert.True(result.Success, $"Phone normalisation failed: {result.Error}");
//    }

//    /// <summary>
//    /// Validates that an obviously invalid phone number is rejected cleanly.
//    /// </summary>
//    [Fact(DisplayName = "InitiateAsync → rejects invalid phone number")]
//    public async Task InitiateAsync_InvalidPhone_ReturnsFail()
//    {
//        var result = await _stkPushService.InitiateAsync(
//            phone: "123",
//            amount: 1,
//            tillNumber: TestTill,
//            accountReference: "BAD-PHONE");

//        Assert.False(result.Success);
//        Assert.Contains("Invalid phone", result.Error, StringComparison.OrdinalIgnoreCase);
//    }

//    // ─── Helpers ──────────────────────────────────────────────────────────────

//    private static string GetRequiredEnv(string key)
//    {
//        var value = Environment.GetEnvironmentVariable(key);
//        if (string.IsNullOrWhiteSpace(value))
//            throw new InvalidOperationException(
//                $"Required environment variable '{key}' is not set. " +
//                $"Set it before running production tests.");
//        return value;
//    }

//	Task IAsyncLifetime.DisposeAsync()
//	{
//		throw new NotImplementedException();
//	}
//}