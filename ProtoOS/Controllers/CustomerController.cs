using BusinessLogic.Customers.Complains;
using BusinessLogic.CustomerService;
using BussinessLogic.Customers.Vehicles;
using BussinessLogic.Sales.NewSales;
using DataAccessLayer.DTOs.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.CustomerService.Customers;
using static BussinessLogic.Customers.Vehicles.OtogasVehicles;


namespace FuelFlow.Controllers 
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
		public async Task<IActionResult> SearchVehicle(string phoneNumber)
		{
			var response = await _vehicles.SearchCustomerByPhone(phoneNumber);
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
	
		#endregion

		#region Complain Methods
		//

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

	public class AddProvisionalCustomerDto
	{
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string NumberPlate { get; set; } = string.Empty;
	}
}

