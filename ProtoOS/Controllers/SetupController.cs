using BusinessLogic.SetupService;
using BusinessLogic.Worker.PriceScheduler;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.EmailDtos;
using DataAccessLayer.DTOs.Setups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static BussinessLogic.Setup.UserSetups;

namespace FuelFlow.Controllers 
{
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]

	public class SetupController : ControllerBase
	{
		private readonly IUserSetups _setupService;
		private readonly PlateRecognition _plateRecognition;

		public SetupController(IUserSetups setupService,PlateRecognition plateRecognition )
		{
			_setupService = setupService;
			_plateRecognition = plateRecognition;
		}
		private IActionResult CreateResponse<T>(T response) => Ok(response);

		#region Price Management

	

		[HttpGet("products")]
		[Authorize]
		public async Task<IActionResult> GetProducts()
		{
			var result = await _setupService.GetProducts();
			return Ok(result);
		}

		[HttpGet("price")]
		[Authorize]
		public async Task<IActionResult> GetPriceByStation([FromQuery] string stationCode, [FromQuery] string productCode)
		{
			if (string.IsNullOrWhiteSpace(stationCode) || string.IsNullOrWhiteSpace(productCode))
				return BadRequest(ServiceResponse<object>.Information("stationCode and productCode are required", null));

			var result = await _setupService.GetPriceByStation(stationCode, productCode);
			return Ok(result);
		}

		#endregion

		#region Payment Management


		[HttpGet]
		[Authorize]
		[Route("PriceInfo")]
		public async Task<IActionResult> GetPrice(string nozzleCode)
		{
			var response = await _setupService.GetPriceInfo(nozzleCode);
			return CreateResponse(response); 
		}

		[HttpGet]
		[Route("GlobalPriceChange")]
		[Authorize(Roles = "can change price for all stations")]
		public async Task<IActionResult> GlobalPriceChange(string productCode, decimal newPrice)
		{
			var response = await _setupService.ChangePriceForAllStations(productCode, newPrice);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("pricechange")]
		[Authorize(Roles = "can change price for all stations")]
		public async Task<IActionResult> Pricechange(string productCode, decimal newPrice)
		{
			var response = await _setupService.ChangePriceForAllStations(productCode, newPrice);
			return CreateResponse(response);
		}

		[HttpPost]
		[Route("PriceSchedule")]
		[Authorize(Roles = "can schedule price change")]
		public async Task<IActionResult> AddPriceSchedule(List<PriceChangeSchedule> priceChange)
		{
		     await _setupService.AddPriceSchedule(priceChange);
			return Ok();
		}


		[HttpPost]
		[Route("AddRecipient")]
		[Authorize(Roles = "can add a recipient to an email")]
		public async Task<IActionResult> AddRecipient(string email, string reportCode,int type)
		{
			var response = await _setupService.AddRecipients(type,reportCode,email);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("GetRecipients")]
		[Authorize(Roles ="can view email recipients")]
		public async Task<IActionResult> GetRecipients(string reportCode)
		{
			var response = await _setupService.GetRecipients(reportCode);
			return CreateResponse(response);
		}

		[HttpPost]
		[Route("RemoveRecipient")]
		[Authorize(Roles = "can remove a recipient from an email")]
		public async Task<IActionResult> RemoveRecipient(string email, string reportCode)
		{
			var response = await _setupService.RemoveEmailRecipients(email, reportCode);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("Reports")]
		[Authorize(Roles = "can view reports")]
		public IActionResult Reports()
		{
			var response = _setupService.Reports();
			return CreateResponse(response);
		}
	
		#endregion

	}
}
