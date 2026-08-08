using BussinessLogic.Shifts;
using DataAccessLayer.DTOs.Shifts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Controllers;

[ApiController]
[Route("fuelflow/shifts")]
public class ShiftSupervisorReconciliationController(IShiftSupervisorReconciliationService service) : ControllerBase
{
	private readonly IShiftSupervisorReconciliationService _service = service;

	[Authorize]
	[HttpPost("{shiftNumber}/supervisor-recon")]
	public async Task<IActionResult> Submit(string shiftNumber, [FromBody] ShiftSupervisorReconciliationRequest request)
	{
		request.ShiftNumber = shiftNumber; // URL is authoritative
		var result = await _service.SubmitReconciliationAsync(request);
		return Ok(result);
	}

	[Authorize]
	[HttpGet("{shiftNumber}/supervisor-recon")]
	public async Task<IActionResult> Get(string shiftNumber)
	{
		var result = await _service.GetReconciliationAsync(shiftNumber);
		return result is null ? NotFound() : Ok(result);
	}
}