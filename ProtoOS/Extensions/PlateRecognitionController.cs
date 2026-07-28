using BussinessLogic.PlateRecognitionService;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.PlateRecognition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class PlateRecognitionController : ControllerBase
	{
		private readonly IPlateRecognitionService _plateService;

		public PlateRecognitionController(IPlateRecognitionService plateService)
		{
			_plateService = plateService;
		}

		[HttpPost("verify-wallet-vehicle")]
		[Consumes("application/json")]
		[RequestSizeLimit(10_000_000)]
		public async Task<IActionResult> VerifyWalletVehicle([FromBody] VerifyWalletVehicleRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Image))
				return BadRequest(ServiceResponse<object>.Information("No image received", null));

			if (string.IsNullOrWhiteSpace(request.CustomerCode))
				return BadRequest(ServiceResponse<object>.Information("customerCode is required", null));

			var result = await _plateService.VerifyWalletVehicleAsync(request.Image, request.CustomerCode, HttpContext.RequestAborted);
			return Ok(result);
		}
	}
}