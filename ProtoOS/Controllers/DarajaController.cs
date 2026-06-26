
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.C2bService;
using Safaricom_Daraja.Stk_Push;
using System.Text;
using System.Text.Json;

namespace FuelFlow.Controllers;

[Route("fuelflow")]
[ApiController]
public class DarajaController(IStkPushService stkPushService,
	IStkCallbackHandler stkCallbackHandler,
	IC2BService c2bService,                    // ✅ FIX: injected, not static
	IOptions<DarajaConfig> options,
	ILogger<DarajaController> logger
) : ControllerBase
{
	private readonly DarajaConfig _cfg = options.Value;

	// ─────────────────────────────────────────────
	// STK PUSH
	// ─────────────────────────────────────────────

	[HttpPost("stk/push")]
	[AllowAnonymous]
	public async Task<IActionResult> StkPush([FromBody] StkPushApiRequest req, CancellationToken ct)
	{
		logger.LogInformation("[STK][Push] ▶ Phone={Phone} Amount={Amount} TillNumber={TN} TillReference={TR} Desc={D}", req.Phone, req.Amount, req.TillNumber, req.TillReference, req.Description);

		var till = _cfg.Tills.FirstOrDefault(t => t.TillNumber == req.TillNumber || t.AccountReference == req.TillReference);

		if (till is null)
		{
			logger.LogWarning("[STK][Push] ❌ Unknown till. TillNumber={TN} TillReference={TR} " + "KnownTills=[{Tills}]", req.TillNumber, req.TillReference,string.Join(", ", _cfg.Tills.Select(t => $"{t.TillNumber}/{t.AccountReference}")));
			return BadRequest("Unknown till");
		}

		logger.LogInformation("[STK][Push] Till resolved → TillName={Name} TillNumber={TN} AccountReference={AR}", till.Name, till.TillNumber, till.AccountReference);

		var result = await stkPushService.InitiateAsync(phone: req.Phone,amount: req.Amount,tillNumber: till.TillNumber,accountReference: till.AccountReference,description: req.Description ?? "Payment",ct: ct);

		if (result.Success)
		{
			logger.LogInformation("[STK][Push] ✅ Initiated. CheckoutRequestID={CID}", result.Data?.CheckoutRequestId);
			return Ok(result.Data);
		}

		logger.LogError("[STK][Push] ❌ Failed. Error={Err}", result.ErrorMessage);
		return BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// STK QUERY
	// ─────────────────────────────────────────────

	[HttpGet("stk/query/{checkoutRequestId}")]
	[AllowAnonymous]
	public async Task<IActionResult> StkQuery(string checkoutRequestId, CancellationToken ct)
	{
		logger.LogInformation("[STK][Query] ▶ CheckoutRequestID={CID}", checkoutRequestId);

		var result = await stkPushService.QueryStatusAsync(checkoutRequestId, ct);

		if (result.Success)
		{
			logger.LogInformation("[STK][Query] ✅ Status retrieved. Data={Data}", result.Data);
			return Ok(result.Data);
		}

		logger.LogError("[STK][Query] ❌ Failed. Error={Err}", result.ErrorMessage);
		return BadRequest(result.ErrorMessage);
	}

	[HttpGet("stk/result/{checkoutRequestId}")]
	[AllowAnonymous]
	public async Task<IActionResult> StkResult(string checkoutRequestId, CancellationToken ct)
	{
		var tx = await stkPushService.GetMpesaTransaction(checkoutRequestId, ct);

		if (tx is null)
			return Ok(new { ResultCode = "pending", TransID = "", Amount = "0" });

		return tx.Status switch
		{
			1 => Ok(new { ResultCode = "0", tx.TransID, Amount = tx.TransAmount.ToString("F2") }),
			2 => Ok(new { ResultCode = "failed", TransID = tx.TransID, Amount = "0" }),
			_ => Ok(new { ResultCode = "pending", TransID = "", Amount = "0" })
		};
	}

	// ─────────────────────────────────────────────
	// STK CALLBACK
	// ─────────────────────────────────────────────

	[HttpPost("stk/callback")]
	[AllowAnonymous]
	public async Task<IActionResult> StkCallback([FromBody] StkCallback callback)
	{
		logger.LogInformation("[STK][Callback] ▶ MerchantRequestID={MID} CheckoutRequestID={CID} " + "ResultCode={RC} ResultDesc={RD}", callback.Body?.StkCallback?.MerchantRequestId, callback.Body?.StkCallback?.CheckoutRequestId, callback.Body?.StkCallback?.ResultCode, callback.Body?.StkCallback?.ResultDesc);

		await stkCallbackHandler.HandleAsync(callback);

		logger.LogInformation("[STK][Callback] ✅ Handled.");
		return Ok();
	}

	// ─────────────────────────────────────────────
	// C2B — REGISTER
	// ─────────────────────────────────────────────

	/// <summary>
	/// One-time call to register validation/confirmation URLs with Safaricom.
	/// Safe to call again — 500.003.1001 (already registered) is handled as success.
	/// </summary>
	/// 


	[AllowAnonymous]
	[HttpPost("register-tills")]
	public async Task<IActionResult> RegisterTills(CancellationToken ct)
	{
		var results = await c2bService.RegisterAllTillsAsync(ct);
		return Ok(results);
	}


	[HttpPost("daraja/c2b/register")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterC2BUrls(CancellationToken ct)
	{
		logger.LogInformation("[C2B][Register] ▶ Triggered. C2BShortCode={SC} " + "ValidationUrl={VUrl} ConfirmationUrl={CUrl}", _cfg.C2BShortCode, _cfg.C2BValidationUrl, _cfg.C2BConfirmationUrl);

		var result = await c2bService.RegisterMasterShortCodeAsync(ct); // ✅ FIX: instance call

		if (result.Success)
		{
			logger.LogInformation("[C2B][Register] ✅ Success. ResponseCode={RC} Desc={Desc}", result.Data?.ResponseCode, result.Data?.ResponseDescription);
			return Ok(result.Data);
		}

		logger.LogError("[C2B][Register] ❌ Failed. Error={Err}", result.ErrorMessage);
		return BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// C2B — VALIDATE
	// ─────────────────────────────────────────────
	#region C2B

	private static readonly JsonSerializerOptions PascalCaseOptions = new()
	{
		PropertyNamingPolicy = null // Forces output to preserve property names exactly as written in the C# model (PascalCase)
	};

	[HttpPost("daraja/c2b/register-store/{storeNumber}")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterC2BStoreNumber(string storeNumber,CancellationToken ct)
	{
		logger.LogInformation("[C2B][Register] ▶ Registering store number={SN}", storeNumber);

		var result = await c2bService.RegisterUrlsAsync(storeNumber, ct);

		return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("daraja/c2b/validate")]
	[AllowAnonymous]
	public IActionResult C2BValidate([FromBody] C2BValidationRequest? req)
	{
		// ... validation logic and logging ...

		var response = c2bService.Validate(req!);

		logger.LogInformation("[C2B][Validate] Response → ResultCode={RC} ResultDesc={RD}",response.ResultCode, response.ResultDesc);

		// Serialize explicitly to PascalCase string and return as JSON content
		var jsonString = JsonSerializer.Serialize(response, PascalCaseOptions);
		return Content(jsonString, "application/json");
	}

	// ─────────────────────────────────────────────
	// C2B — CONFIRM
	// ─────────────────────────────────────────────

	[HttpPost("daraja/c2b/confirm")]
	[AllowAnonymous]
	public async Task<IActionResult> Confirmation()
	{
		Request.EnableBuffering();
		using var reader = new StreamReader(Request.Body, leaveOpen: true);
		var rawBody = await reader.ReadToEndAsync();
		Request.Body.Position = 0;

		Console.WriteLine("══════════════════════════════════════════════════════");
		Console.WriteLine($"[C2B-RAW-BODY] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
		Console.WriteLine(rawBody);
		Console.WriteLine("══════════════════════════════════════════════════════");

		var request = JsonSerializer.Deserialize<C2BConfirmationRequest>(rawBody);
		await c2bService.HandleConfirmationAsync(request!);

		return Ok(new { ResultCode = "0", ResultDesc = "Success" });
	}
	// ── Private Utility Helpers ──────────────────────────────────────────────────

	private static string MaskPhoneNumber(string? phone)
	{
		if (string.IsNullOrWhiteSpace(phone)) return "UNKNOWN";
		if (phone.Length < 7) return phone;
		return $"{phone[..4]}****{phone[^3..]}"; // Outputs formats like: 2547****123
	}
}

	#endregion
	// ─────────────────────────────────────────────
	// DTOs
	// ─────────────────────────────────────────────
	public record StkPushApiRequest(
	string Phone,
	long Amount,
	string? TillNumber,
	string? TillReference,
	string? Description);