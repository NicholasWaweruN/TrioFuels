using Microsoft.AspNetCore.Mvc;
using Safaricom_Daraja.Stk_Push;

namespace FuelFlow.Controllers;

/// <summary>
/// Temporary diagnostic endpoint — remove after the 400.002.02 issue is resolved.
/// Call: POST /fuelflow/daraja/diagnose
/// </summary>
[ApiController]
[Route("fuelflow/daraja")]
public sealed class DarajaDiagnosticController(
	StkPushDiagnosticService diagnosticService,
	ILogger<DarajaDiagnosticController> logger) : ControllerBase
{
	/// <summary>
	/// Fires a live STK Push attempt and writes every field to daraja_diagnostics.csv.
	/// Check the CSV at {AppContext.BaseDirectory}/daraja_diagnostics.csv
	/// or watch the logs for DIAGNOSTIC lines.
	/// </summary>
	[HttpPost("diagnose")]
	public async Task<IActionResult> RunDiagnostic(
		[FromQuery] string phone = "0715821303",
		[FromQuery] string tillNumber = "5617668",
		[FromQuery] long amount = 1,
		CancellationToken ct = default)
	{
		logger.LogWarning(
			">>> DIAGNOSTIC RUN STARTED — Phone={Phone} Till={Till} Amount={Amount}",
			phone, tillNumber, amount);

		await diagnosticService.RunDiagnosticAsync(
			phone, amount, tillNumber,
			accountReference: "DIAG_TEST",
			description: "DiagTest",
			ct: ct);

		var csvPath = Path.Combine(AppContext.BaseDirectory, "daraja_diagnostics.csv");
		var csvExists = System.IO.File.Exists(csvPath);

		return Ok(new
		{
			message = "Diagnostic complete. Check logs for DIAGNOSTIC lines and CSV file.",
			csvPath,
			csvExists,
			hint = "Search logs for 'DIAGNOSTIC PAYLOAD' and 'DIAGNOSTIC RESPONSE'"
		});
	}
}