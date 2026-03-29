using BusinessLogic.Customers.Complains;
using BusinessLogic.CustomerService;
using BussinessLogic.Customers.Vehicles;
using BussinessLogic.Sales.NewSales;
using DataAccessLayer.DTOs.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.CustomerService.Customers;
using static BussinessLogic.Customers.Vehicles.OtogasVehicles;


namespace ProtoOS.Controllers
{
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]

	public class CustomerController : ControllerBase
	{
		private readonly Customers _customers;
		private readonly OtogasVehicles _vehicles;
		private readonly Complain _complains;
		private readonly ILoyaltyServices _loyalty;
		public CustomerController(Customers customers, OtogasVehicles vehicles, Complain complains, ILoyaltyServices loyalty)
		{
			_customers = customers;
			_vehicles = vehicles;
			_complains = complains;
			_loyalty = loyalty;
		}


		private IActionResult CreateResponse<T>(T response) => Ok(response);

		#region Customer Methods

		[HttpPost("AddCustomer")]
		[Authorize(Roles = "can add an fuelflow customer")]
		public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
		{
			var response = await _customers.AddCustomer(customerDTO);
			return CreateResponse(response);
		}

		[HttpPost("UpdateCustomer")]
		[Authorize(Roles = "can update customer details")]
		public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO customerDTO, string customerCode)
		{
			var response = await _customers.UpdateCustomer(customerDTO, customerCode);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("GetAllCustomers/{pageNumber}/{pageSize}")]
		[Authorize(Roles = "can view all customer")]
		public async Task<IActionResult> GetAllCustomers(int pageNumber = 1, int pageSize = 10, string? customerName = null, string? customerPhone = null)
		{
			var response = await _customers.GetAllCustomers(customerName, customerPhone, pageNumber, pageSize);
			return CreateResponse(response);
		}

		[HttpPost("UpdateCustomerCreditLimit")]
		[Authorize(Roles = "can update customer credit limit")]
		public async Task<IActionResult> UpdateCustomerCreditLimit([FromBody] UpdateCustomerCreditLimitDTO customerDTO)
		{
			var response = await _customers.UpdateCustomerCreditLimit(customerDTO);
			return CreateResponse(response);
		}


		[HttpPost("CustomerDiscount")]
		[Authorize(Roles = "can update customer discount")]
		public async Task<IActionResult> CustomerDiscount([FromBody] UpdateDiscount updateDiscount)
		{
			var response = await _customers.CustomerDiscount(updateDiscount);
			return CreateResponse(response);
		}

		#endregion

		#region Vehicle Methods

		[HttpPost("AddVehicle")]
		[Authorize(Roles = "can add a vehicle")]
		public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDTO)
		{
			var response = await _vehicles.AddVehicle(vehicleDTO);
			return CreateResponse(response);
		}

		[HttpPost("DeactivateVehicle")]
		[Authorize(Roles = "can deactivate a vehicle")]
		public async Task<IActionResult> DeactivateVehicle(string vehicleCode)
		{
			var response = await _vehicles.DeactivateVehicle(vehicleCode);
			return CreateResponse(response);
		}

		[HttpPost("ActivateVehicle")]
		[Authorize(Roles = "can activate a vehicle")]
		public async Task<IActionResult> ActivateVehicle(string vehicleCode)
		{
			var response = await _vehicles.ActivateVehicle(vehicleCode);
			return CreateResponse(response);
		}

		[HttpGet("GetAllVehicles")]
		[Authorize(Roles = "can view all vehicles")]
		public async Task<IActionResult> GetAllVehicles()
		{
			var response = await _vehicles.GetAllVehicles();
			return CreateResponse(response);
		}

		[HttpGet("GetAllVehicles/{pageSize}/{pageNumber}")]
		[Authorize(Roles = "can view all vehicles")]
		public async Task<IActionResult> GetAllVehicles(int pageNumber, int pageSize, string? customerName, string? vehicleRegistrationNumber, string? productCode, bool? status)
		{
			var response = await _vehicles.GetAllVehicles(pageNumber, pageSize, customerName, vehicleRegistrationNumber, productCode, status);
			return CreateResponse(response);
		}

		[HttpPatch("UpdateVehicle")]
		[Authorize(Roles = "can update a vehicle")]
		public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleDto vehicleDTO)
		{
			var response = await _vehicles.UpdateVehicle(vehicleDTO);
			return CreateResponse(response);
		}


		[HttpGet("SearchVehicle")]
		[Authorize(Roles = "can search vehicle")]
		public async Task<IActionResult> SearchVehicle(string vehicleRegNo)
		{
			var response = await _vehicles.SearchVehicle(vehicleRegNo);
			return CreateResponse(response);
		}

		[HttpGet("SearchVehicle/{stationCode}/{vehicleRegNo}")]
		[Authorize(Roles = "can search vehicle")]
		public async Task<IActionResult> SearchVehicle(string vehicleRegNo, string stationCode, string? shiftNumber)
		{
			var response = await _vehicles.SearchVehicle(vehicleRegNo, stationCode, shiftNumber);
			return CreateResponse(response);
		}
		//get vehicle by customer code
		[HttpGet("GetVehicleByCustomerCode")]
		[Authorize(Roles = "can view customer vehicles")]
		public async Task<IActionResult> GetVehicleByCustomerCode(string customerCode)
		{
			var response = await _vehicles.GetCustomerVehicles(customerCode);
			return CreateResponse(response);
		}
		//get walkin customers
		[HttpGet("GetWalkinCustomers")]
		[Authorize]
		public async Task<IActionResult> GetWalkinCustomers()
		{
			var response = await _vehicles.GetWalkInCustomers();
			return CreateResponse(response);
		}
		#endregion

		#region Complain Methods

		[HttpPost("AddComplain")]
		[Authorize(Roles = "can add a complain")]
		public async Task<IActionResult> AddComplain([FromBody] AddComplainDto complainDTO)
		{
			var response = await _complains.AddComplain(complainDTO);
			return CreateResponse(response);
		}

		[HttpGet("GetAllComplains")]
		[Authorize(Roles = "can view all complains")]
		public async Task<IActionResult> GetAllComplains()
		{
			var response = await _complains.GetAllComplains();
			return CreateResponse(response);
		}

		[HttpPatch("ChangeComplainStatus")]
		[Authorize(Roles = "can change complain status")]
		public async Task<IActionResult> ChangeComplainStatus(string complainId)
		{
			var response = await _complains.ChangeComplainStatus(complainId);
			return CreateResponse(response);
		}
		[HttpPost("MarkVehicleAsUnInstalled")]
		[Authorize(Roles = "can uninstall a vehicle")]
		public async Task<IActionResult> MarkVehicleAsUnInstalled(string vehicleCode)
		{
			var response = await _vehicles.MarkVehicleAsUnInstalled(vehicleCode);
			return CreateResponse(response);
		}
		//
		//a vehicle to another customer
		[HttpPost("TransferVehicle")]
		[Authorize(Roles = "can transfer a vehicle")]
		public async Task<IActionResult> TransferVehicle(TransferVehicleDto transfer)
		{
			var response = await _vehicles.TransferVehicle(transfer);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("GetTankSizes")]
		[Authorize]
		public async Task<IActionResult> GetTankSizes()
		{
			var response = await _vehicles.GetTankSizes();
			return CreateResponse(response);
		}

		[HttpPost]
		[Route("RegisterNonOtogasVehicle")]
		[Authorize(Roles = "can register vehicles on forecourt")]
		public async Task<IActionResult> RegisterNonOtogasVehicle(NonOtogasVehicleDto vehicle)
		{
			var response = await _vehicles.RegisterNonOtogasVehicle(vehicle);
			return CreateResponse(response);
		}


		//Export customers
		[HttpGet("export-all-customers")]
		[Authorize(Roles = "can export customers")]
		public async Task<IActionResult> ExportAllCustomers()
		{
			var result = await _customers.ExportAllCustomers();
			if (result.ResponseCode != 1)
			{
				return BadRequest(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.xlsx");
		}

		//Update credit limit
		[HttpPost("UpdateCreditLimit")]
		[Authorize(Roles = "can update credit Limit")]
		public async Task<IActionResult> UpdateCreditLimit([FromBody] UpdateCustomerCreditLimitDTO creditLimit)
		{
			var response = await _customers.UpdateCustomerCreditLimit(creditLimit);
			return CreateResponse(response);
		}

		//merge
		[HttpPost("MergeCustomer")]
		[Authorize(Roles = "can merge customers")]
		public async Task<IActionResult> MergeCustomer(string customerCode, string customerCodeToMerge)
		{
			var response = await _vehicles.MergeCustomers(customerCode, customerCodeToMerge);
			return CreateResponse(response);
		}

		//list customers to merge
		[HttpGet("ListCustomersToMerge")]
		[Authorize(Roles = "can list customers to merge")]
		public async Task<IActionResult> ListCustomersToMerge(string customerCode)
		{
			var response = await _vehicles.ListCustomersToMerge(customerCode);
			return CreateResponse(response);
		}
		//CustomerCreditLimit
		[HttpPost]
		[Authorize(Roles = "can update customer credit limit")]
		[Route("CustomerCreditLimit")]
		public async Task<IActionResult> CustomerCreditLimit([FromBody] UpdateCreditLimitDTO limit)
		{
			var response = await _customers.CustomerCreditLimit(limit);
			return CreateResponse(response);
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="telematicDto"></param>
		/// <returns> <param name=""</param></returns>

		//add addtelematic
		[HttpPost("AddTelematic")]
		[Authorize(Roles = "can add vehicle telematic")]
		public async Task<IActionResult> AddTelematic([FromBody] TellematicDto telematicDto)
		{
			var response = await _vehicles.AddTelematic(telematicDto);
			return CreateResponse(response);
		}

		//add addtelematic
		[HttpPost("add-organisation")]
		[Authorize(Roles = "can add an organisation")]
		public async Task<IActionResult> AddOrganisations([FromBody] RegisterOrganisationDTO organisation)
		{
			var response = await _customers.Organisations(organisation);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("get-organisations")]
		[Authorize]
		public async Task<IActionResult> GetOrganisations()
		{
			var response = await _customers.OrganisationList();
			return CreateResponse(response);
		}

		[HttpGet("check-loyalty")]
		[Authorize]
		public async Task<IActionResult> CheckLoyalty(string phoneNumber)
		{
			var response = await _vehicles.CheckLoyalty(phoneNumber);
			return Ok(response);
		}

		[HttpPatch("UpdateRoyaltyPoints")]
		[Authorize(Roles = "can update a UpdateRoyaltyPoints")]
		public async Task<IActionResult> UpdateRoyaltyPoints(string customerCode, decimal points)
		{
			var response = await _vehicles.UpdateRoyaltyPoints(customerCode, points);
			return CreateResponse(response);
		}

		[HttpGet("loyalty-balance")]
		[Authorize]
		public async Task<IActionResult> GetLoyaltyBalance([FromQuery] string phoneNumber)
		{
			var result = await _loyalty.GetLoyaltyBalanceByPhoneAsync(phoneNumber);
			return Ok(result);
		}
	}
}

