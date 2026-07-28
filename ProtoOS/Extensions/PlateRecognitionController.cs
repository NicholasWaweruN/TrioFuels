
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
		[Consumes("multipart/form-data")]
		[RequestSizeLimit(10_000_000)]
		public async Task<IActionResult> VerifyWalletVehicle([FromForm] VerifyWalletVehicleRequest request)
		{
			if (request.Image is null || request.Image.Length == 0)
				return BadRequest(ServiceResponse<object>.Information("No image received", null));

			if (string.IsNullOrWhiteSpace(request.CustomerCode))
				return BadRequest(ServiceResponse<object>.Information("customerCode is required", null));

			var result = await _plateService.VerifyWalletVehicleAsync(request.Image, request.CustomerCode, HttpContext.RequestAborted);
			return Ok(result);
		}
	}
}