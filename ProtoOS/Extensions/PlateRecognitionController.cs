
using BussinessLogic.PlateRecognitionService;
using DataAccessLayer.Common;
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

		// POST api/PlateRecognition/verify-wallet-vehicle
		// multipart/form-data: image (file), customerCode (string)
		[HttpPost("verify-wallet-vehicle")]
		[RequestSizeLimit(10_000_000)]
		public async Task<IActionResult> VerifyWalletVehicle(
			[FromForm] IFormFile image,
			[FromForm] string customerCode)
		{
			if (image is null || image.Length == 0)
				return BadRequest(ServiceResponse<object>.Information("No image received", null));

			if (string.IsNullOrWhiteSpace(customerCode))
				return BadRequest(ServiceResponse<object>.Information("customerCode is required", null));

			var result = await _plateService.VerifyWalletVehicleAsync(image, customerCode, HttpContext.RequestAborted);
			return Ok(result);
		}
	}
}