using Daraja.Services;
using FuelFlow.Services.Daraja;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;
using Safaricom_Daraja.Stk_Push;

namespace FuelFlow.Controllers;

[Route("fuelflow/[controller]")]
[ApiController]
public class DarajaController(
	IStkPushService stkPushService,
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
	public async Task<IActionResult> StkPush([FromBody] StkPushApiRequest req,CancellationToken ct)
	{
		logger.LogInformation("[STK][Push] ▶ Phone={Phone} Amount={Amount} TillNumber={TN} TillReference={TR} Desc={D}",req.Phone, req.Amount, req.TillNumber, req.TillReference, req.Description);

		var till = _cfg.Tills.FirstOrDefault(t =>t.TillNumber == req.TillNumber ||t.AccountReference == req.TillReference);

		if (till is null)
		{
			logger.LogWarning("[STK][Push] ❌ Unknown till. TillNumber={TN} TillReference={TR} " +"KnownTills=[{Tills}]",req.TillNumber, req.TillReference,
				string.Join(", ", _cfg.Tills.Select(t => $"{t.TillNumber}/{t.AccountReference}")));

			return BadRequest("Unknown till");
		}

		logger.LogInformation("[STK][Push] Till resolved → TillName={Name} TillNumber={TN} AccountReference={AR}",till.Name, till.TillNumber, till.AccountReference);

		var result = await stkPushService.InitiateAsync(
			phone: req.Phone,
			amount: req.Amount,
			tillNumber: till.TillNumber,
			accountReference: till.AccountReference,
			description: req.Description ?? "Payment",
			ct: ct);

		if (result.Success)
		{
			logger.LogInformation("[STK][Push] ✅ Initiated. CheckoutRequestID={CID}",result.Data?.CheckoutRequestId);
			return Ok(result.Data);
		}

		logger.LogError("[STK][Push] ❌ Failed. Error={Err}", result.ErrorMessage);
		return BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// STK QUERY
	// ─────────────────────────────────────────────

	[HttpGet("stk/query/{checkoutRequestId}")]
	public async Task<IActionResult> StkQuery(string checkoutRequestId,CancellationToken ct)
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

	// ─────────────────────────────────────────────
	// STK CALLBACK
	// ─────────────────────────────────────────────

	[HttpPost("stk/callback")]
	public async Task<IActionResult> StkCallback([FromBody] StkCallback callback)
	{
		logger.LogInformation("[STK][Callback] ▶ MerchantRequestID={MID} CheckoutRequestID={CID} " +"ResultCode={RC} ResultDesc={RD}",callback.Body?.StkCallback?.MerchantRequestId,callback.Body?.StkCallback?.CheckoutRequestId,callback.Body?.StkCallback?.ResultCode,callback.Body?.StkCallback?.ResultDesc);

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
	[HttpPost("c2b/register")]
	public async Task<IActionResult> RegisterC2BUrls(CancellationToken ct)
	{
		logger.LogInformation("[C2B][Register] ▶ Triggered. C2BShortCode={SC} " +"ValidationUrl={VUrl} ConfirmationUrl={CUrl}",_cfg.C2BShortCode, _cfg.C2BValidationUrl, _cfg.C2BConfirmationUrl);

		var result = await c2bService.RegisterMasterShortCodeAsync(ct); // ✅ FIX: instance call

		if (result.Success)
		{
			logger.LogInformation("[C2B][Register] ✅ Success. ResponseCode={RC} Desc={Desc}",result.Data?.ResponseCode, result.Data?.ResponseDescription);
			return Ok(result.Data);
		}

		logger.LogError("[C2B][Register] ❌ Failed. Error={Err}", result.ErrorMessage);
		return BadRequest(result.ErrorMessage);
	}

	// ─────────────────────────────────────────────
	// C2B — VALIDATE
	// ─────────────────────────────────────────────


	[HttpPost("c2b/register-store/{storeNumber}")]
	public async Task<IActionResult> RegisterC2BStoreNumber(
	string storeNumber,
	CancellationToken ct)
	{
		logger.LogInformation(
			"[C2B][Register] ▶ Registering store number={SN}", storeNumber);

		var result = await c2bService.RegisterUrlsAsync(storeNumber, ct);

		return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("c2b/validate")]
	public IActionResult C2BValidate([FromBody] C2BValidationRequest req)
	{
		// Log the raw body Safaricom sent — critical for debugging what they actually send
		logger.LogInformation(
			"[C2B][Validate] ▶ Raw request — TransID={ID} TransType={TT} " +"TransTime={Time} Amount={Amount} BSC={BSC} BillRefNumber={Ref} " +"InvoiceNumber={Inv} OrgAccountBalance={Bal} ThirdPartyTransID={TPID} " +
			"Phone={Phone} FirstName={FN} MiddleName={MN} LastName={LN}",
			req.TransactionId, req.TransactionType, req.TransTime,
			req.TransAmount, req.BusinessShortCode, req.BillRefNumber,
			req.InvoiceNumber, req.OrgAccountBalance, req.ThirdPartyTransId,
			req.PhoneNumber, req.FirstName, req.MiddleName, req.LastName);

		var response = c2bService.Validate(req); // ✅ FIX: was missing c2bService

		logger.LogInformation(
			"[C2B][Validate] Response → ResultCode={RC} ResultDesc={RD}",
			response.ResultCode, response.ResultDesc);

		// Safaricom expects 200 OK with ResultCode in body — never return 4xx
		return Ok(response);
	}

	// ─────────────────────────────────────────────
	// C2B — CONFIRM
	// ─────────────────────────────────────────────

	[HttpPost("c2b/confirm")]
	public async Task<IActionResult> C2BConfirm(
		[FromBody] C2BConfirmationRequest req,
		CancellationToken ct)
	{
		
			Console.WriteLine("🔥 LIVE C2B CALLBACK HIT");

	
		// Log the raw body Safaricom sent
		logger.LogInformation(
			"[C2B][Confirm] ▶ Raw request — TransID={ID} TransType={TT} " +
			"TransTime={Time} Amount={Amount} BSC={BSC} BillRefNumber={Ref} " +
			"InvoiceNumber={Inv} OrgAccountBalance={Bal} ThirdPartyTransID={TPID} " +
			"Phone={Phone} FirstName={FN} MiddleName={MN} LastName={LN}",
			req.TransactionId, req.TransactionType, req.TransTime,
			req.TransAmount, req.BusinessShortCode, req.BillRefNumber,
			req.InvoiceNumber, req.OrgAccountBalance, req.ThirdPartyTransId,
			req.PhoneNumber, req.FirstName, req.MiddleName, req.LastName);

		await c2bService.HandleConfirmationAsync(req, ct); // ✅ FIX: was missing c2bService

		logger.LogInformation(
			"[C2B][Confirm] ✅ Handled. TransID={ID}", req.TransactionId);

		// Safaricom expects 200 OK — any non-200 triggers a retry storm
		return Ok();
	}
}

// ─────────────────────────────────────────────
// DTOs
// ─────────────────────────────────────────────
public record StkPushApiRequest(
	string Phone,
	long Amount,
	string? TillNumber,
	string? TillReference,
	string? Description);