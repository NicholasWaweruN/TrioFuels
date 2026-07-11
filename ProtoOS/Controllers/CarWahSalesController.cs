using DataAccessLayer.Common;
using DataAccessLayer.DTOs.CarWash;
using FuelFlow.Services.CarWash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using TrioCarWash.Services.Services;

[ApiController]
[Route("fuelflow/[controller]")]
[Authorize]
public class CarWashShiftController : ControllerBase
{
	private readonly ICarWashShiftService _shiftService;
	public CarWashShiftController(ICarWashShiftService shiftService) => _shiftService = shiftService;

	private string? UserCode => User.FindFirst("UserCode")?.Value;

	[HttpPost("OpenShift")]
	public async Task<IActionResult> OpenShift([FromBody] OpenShiftRequestDto request)
	{
		if (string.IsNullOrEmpty(UserCode))
			return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
		return Ok(await _shiftService.OpenShiftAsync(UserCode, request));
	}

	[HttpPost("CloseShift")]
	public async Task<IActionResult> CloseShift([FromBody] CloseShiftRequestDto request)
	{
		if (string.IsNullOrEmpty(UserCode))
			return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
		return Ok(await _shiftService.CloseShiftAsync(UserCode, request));
	}
}

[ApiController]
[Route("fuelflow/[controller]")]
[Authorize]
public class CarWashSalesController : ControllerBase
{
	private readonly ICarWashSalesService _salesService;
	public CarWashSalesController(ICarWashSalesService salesService) => _salesService = salesService;

	private string? UserCode => User.FindFirst("UserCode")?.Value;

	[HttpGet("GetProducts")]
	public async Task<IActionResult> GetProducts() => Ok(await _salesService.GetProductsAsync());

	[HttpPost("CreateSale")]
	public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDto request)
	{
		if (string.IsNullOrEmpty(UserCode))
			return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
		return Ok(await _salesService.CreateSaleAsync(UserCode, request));
	}

	[HttpGet("GetSalesHistory")]
	public async Task<IActionResult> GetSalesHistory([FromQuery] long shiftId)
	{
		if (string.IsNullOrEmpty(UserCode))
			return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
		return Ok(await _salesService.GetSalesHistoryAsync(shiftId));
	}
}

[ApiController]
[Route("fuelflow/[controller]")]
[Authorize]
public class CarWashDashboardController : ControllerBase
{
	private readonly ICarWashDashboardService _dashboardService;
	public CarWashDashboardController(ICarWashDashboardService dashboardService) => _dashboardService = dashboardService;

	[HttpGet("GetSummary")]
	public async Task<IActionResult> GetSummary()
	{
		var userCode = User.FindFirst("UserCode")?.Value;
		if (string.IsNullOrEmpty(userCode))
			return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
		return Ok(await _dashboardService.GetDashboardSummaryAsync(userCode));
	}
}