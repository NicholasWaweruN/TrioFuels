// ─────────────────────────────────────────────────────────────────────────────
// DarajaController.cs — wire into your ASP.NET Core app
// Add attribute routing or Minimal API endpoints as needed.
// ─────────────────────────────────────────────────────────────────────────────


using Daraja.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.Stk_Push;

namespace FuelFlow.Controllers;

[Route("fuelflow/[controller]")]
[ApiController]
public class DarajaController(
	IStkPushService stkPushService,
	IC2BService c2bService,
	IPullTransactionService pullService,
	IStkCallbackHandler stkCallbackHandler,
	IOptions<DarajaConfig> options) : ControllerBase
{
	private readonly DarajaConfig _cfg = options.Value;

	// ─── STK Push ─────────────────────────────────────────────────────────────

	[HttpPost("stk/push")]
	public async Task<IActionResult> StkPush([FromBody] StkPushApiRequest req, CancellationToken ct)
	{
		// Resolve till by name or number
		var till = _cfg.Tills.FirstOrDefault(t =>
			t.TillNumber == req.TillNumber || t.AccountReference == req.TillReference)
			?? throw new ArgumentException("Unknown till");

		var result = await stkPushService.InitiateAsync(
			req.Phone, req.Amount, till.TillNumber, till.AccountReference,
			req.Description ?? "Payment", ct);

		return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
	}

	// tillNumber is required so the password can be rebuilt correctly for the query
	[HttpGet("stk/query/{checkoutRequestId}")]
	public async Task<IActionResult> StkQuery(
		string checkoutRequestId,
		[FromQuery] string tillNumber,   // e.g. GET /stk/query/ws_CO_123?tillNumber=5617668
		CancellationToken ct)
	{
		if (string.IsNullOrWhiteSpace(tillNumber))
			return BadRequest("tillNumber query parameter is required.");

		var result = await stkPushService.QueryStatusAsync(checkoutRequestId, tillNumber, ct);
		return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
	}

	// ─── STK Callback (called by Safaricom) ──────────────────────────────────

	[HttpPost("stk/callback")]
	public async Task<IActionResult> StkCallback([FromBody] StkCallback callback, CancellationToken ct)
	{
		await stkCallbackHandler.HandleAsync(callback, ct);
		return Ok();
	}

	// ─── C2B Register ─────────────────────────────────────────────────────────

	[HttpPost("c2b/register")]
	public async Task<IActionResult> RegisterC2BUrls(CancellationToken ct)
	{
		var results = await c2bService.RegisterAllTillsAsync(ct);
		return Ok(results);
	}

	// ─── C2B Validation (called by Safaricom) ────────────────────────────────

	[HttpPost("c2b/validate")]
	public IActionResult C2BValidate([FromBody] C2BValidationRequest req)
	{
		var response = c2bService.Validate(req);
		return Ok(response);
	}

	// ─── C2B Confirmation (called by Safaricom) ───────────────────────────────

	[HttpPost("c2b/confirm")]
	public async Task<IActionResult> C2BConfirm([FromBody] C2BConfirmationRequest req, CancellationToken ct)
	{
		await c2bService.HandleConfirmationAsync(req, ct);
		return Ok();
	}

	// ─── Pull Transactions ────────────────────────────────────────────────────

	[HttpPost("pull")]
	public async Task<IActionResult> PullTransactions(
		[FromBody] PullApiRequest req, CancellationToken ct)
	{
		// Default: last 24 hours for all tills
		var from = req.From ?? DateTime.UtcNow.AddHours(-24);
		var to = req.To ?? DateTime.UtcNow;

		if (req.TillNumber is not null)
		{
			var result = await pullService.PullAllPagesAsync(req.TillNumber, from, to, ct);
			return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
		}
		else
		{
			var results = await pullService.PullAllTillsAsync(from, to, ct);
			return Ok(results);
		}
	}
}

// ─── API request DTOs ────────────────────────────────────────────────────────

public record StkPushApiRequest(
	string Phone,
	long Amount,
	string? TillNumber,
	string? TillReference,
	string? Description);

public record PullApiRequest(
	string? TillNumber,
	DateTime? From,
	DateTime? To);