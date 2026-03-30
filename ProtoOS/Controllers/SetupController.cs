using BusinessLogic.SetupService;
using BusinessLogic.Worker.PriceScheduler;
using BussinessLogic.Setup;
using DataAccessLayer.DTOs.EmailDtos;
using DataAccessLayer.DTOs.Setups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static BusinessLogic.SetupService.UserSetups;

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

		[HttpPost("ChangePrice")]
		[Authorize(Roles = "can add price")]
		public async Task<IActionResult> AddPrice(List<UpdatePrice> updatePrices)
		{
			var response = await _setupService.AddPrice(updatePrices);
			return CreateResponse(response);
		}
		#endregion

		#region Payment Management

		[HttpPost]
		[Route("AddPaymentType")]
		[Authorize(Roles = "can add a payment type")]
		public async Task<IActionResult> AddPaymentType([FromBody] string paymentType)
		{
			var response = await _setupService.AddPaymentType(paymentType);
			return CreateResponse(response);
		}

		[HttpPost]
        [Route("AddProduct")]
		[Authorize(Roles = "can add a product")]
		public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
		{
			var response = await _setupService.AddProduct(product);
			return CreateResponse(response);
		}

		[HttpGet]
		[Authorize]
		[Route("GetProducts")]  
		public async Task<IActionResult> GetProducts()
		{
			var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
			var response = await _setupService.GetProducts();
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

		[HttpPost]
		[Route("PriceSchedule")]
		[Authorize(Roles = "can schedule price change")]
		public async Task<IActionResult> AddPriceSchedule(List<PriceChangeSchedule> priceChange)
		{
		     await _setupService.AddPriceSchedule(priceChange);
			return Ok();
		}

		[HttpPost]
		[Route("RegisterPDA")]
		[Authorize(Roles = "can register pda device")]
		public async Task<IActionResult> RegisterPDA(string deviceName, string deviceIMEI, string deviceSerialNumber, string deviceModel, string dispensercode)
		{
			var response = await _setupService.RegisterPDA(deviceName, deviceIMEI, deviceSerialNumber, deviceModel, dispensercode);
			return CreateResponse(response);
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
		//PlateRecognition
		[HttpPost]
		[Route("PlateReCognition")]
		[Authorize]
		public IActionResult Reports(string base64Image)
		{
			var response = _plateRecognition.PlateNumber(base64Image);
			return CreateResponse(response);
		}
		#endregion

	}
}
