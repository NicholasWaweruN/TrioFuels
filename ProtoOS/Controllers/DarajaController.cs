using Daraja.Services;
using FuelFlow.Services.Daraja;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.Stk_Push;
using Serilog.Core;

namespace FuelFlow.Controllers;

[Route("fuelflow/[controller]")]
[ApiController]
public class DarajaController(
	IStkPushService stkPushService,
	IC2BService c2bService,
	//IPullTransactionImportService pullImportService,
	IStkCallbackHandler stkCallbackHandler,
	IOptions<DarajaConfig> options
) : ControllerBase
{
	private readonly DarajaConfig _cfg = options.Value;

	// ─────────────────────────────────────────────
	// STK PUSH (PAYBILL-BASED)
	// ─────────────────────────────────────────────
	[HttpPost("stk/push")]
	public async Task<IActionResult> StkPush(
		[FromBody] StkPushApiRequest req,
		CancellationToken ct)
	{
		// ✔ Validate till internally only (NOT for STK routing)
		var till = _cfg.Tills.FirstOrDefault(t =>
			t.TillNumber == req.TillNumber ||
			t.AccountReference == req.TillReference
		);

		if (till is null)
			return BadRequest("Unknown till");

		// ✔ STK PUSH DOES NOT USE TILL NUMBER
		var result = await stkPushService.InitiateAsync(
			phone: req.Phone,
			amount: req.Amount,
		   tillNumber: till.TillNumber,        // "5617668"

			// 🔥 THIS is what replaces till in Safaricom world
			accountReference: till.AccountReference,

			description: req.Description ?? "Payment",
			ct: ct
		);

		return result.Success
			? Ok(result.Data)
			: BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// STK QUERY (NO TILL REQUIRED)
	// ─────────────────────────────────────────────
	[HttpGet("stk/query/{checkoutRequestId}")]
	public async Task<IActionResult> StkQuery(
		string checkoutRequestId,
		CancellationToken ct)
	{
		var result = await stkPushService.QueryStatusAsync(checkoutRequestId, ct);

		return result.Success
			? Ok(result.Data)
			: BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// CALLBACK (SAFE HANDLER)
	// ───────────────────────────────────────
	//
	// ──────
	[HttpPost("stk/callback")]
	public async Task<IActionResult> StkCallback([FromBody] StkCallback callback)
	{
		Console.WriteLine("🔥 CALLBACK HIT CONTROLLER");

		await stkCallbackHandler.HandleAsync(callback);
		return Ok();
	}

	// ─────────────────────────────────────────────
	// C2B
	// ─────────────────────────────────────────────

	/// <summary>
	/// One-time call to register validation/confirmation URLs with Safaricom.
	/// Targets the master BusinessShortCode (4161705), not individual tills.
	/// </summary>
	[HttpPost("c2b/register")]
	public async Task<IActionResult> RegisterC2BUrls(CancellationToken ct)
	{
		var result = await c2bService.RegisterMasterShortCodeAsync(ct);

		return result.Success
			? Ok(result.Data)
			: BadRequest(result.ErrorMessage);
	}

	[HttpPost("c2b/validate")]
	public IActionResult C2BValidate([FromBody] C2BValidationRequest req)
	{
		// Safaricom expects a 200 OK with ResultCode in the body — never a 4xx.
		return Ok(c2bService.Validate(req));
	}

	[HttpPost("c2b/confirm")]
	public async Task<IActionResult> C2BConfirm(
		[FromBody] C2BConfirmationRequest req,
		CancellationToken ct)
	{
		await c2bService.HandleConfirmationAsync(req, ct);

		// Safaricom expects a 200 OK — any non-200 triggers a retry storm.
		return Ok();
	}

	// ─────────────────────────────────────────────
	// PULL TRANSACTIONS
	// ─────────────────────────────────────────────
	//[HttpPost("pull")]
	//public async Task<IActionResult> PullTransactions(
	//	[FromBody] PullApiRequest req,
	//	CancellationToken ct)
	//{
	//	var from = req.From ?? DateTime.UtcNow.AddHours(-24);
	//	var to = req.To ?? DateTime.UtcNow;

	//	if (!string.IsNullOrWhiteSpace(req.TillNumber))
	//	{
	//		var result = await pullImportService.ImportForTillAsync(req.TillNumber, from, to, ct);
	//		return result.Success ? Ok(result) : BadRequest(result.Error);
	//	}

	//	var all = await pullImportService.ImportAllTillsAsync(from, to, ct);
	//	return Ok(all);
	//}
}

// ─────────────────────────────────────────────
// DTOs
// ─────────────────────────────────────────────
public record StkPushApiRequest(
	string Phone,
	long Amount,
	string? TillNumber,        // used ONLY for internal lookup
	string? TillReference,     // optional alias
	string? Description);