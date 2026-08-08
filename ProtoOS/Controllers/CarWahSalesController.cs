using DataAccessLayer.Common;
using DataAccessLayer.DTOs.CarWash;
using FuelFlow.Services.CarWash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrioCarWash.Services.Services;

namespace FuelFlow.Controllers
{
	[ApiController]
	[Route("fuelflow/[controller]")]
	[Authorize]
	public class CarWashShiftController(ICarWashShiftService shiftService) : ControllerBase
	{
		private readonly ICarWashShiftService _shiftService = shiftService;

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
	public class CarWashSalesController(ICarWashSalesService salesService) : ControllerBase
	{
		private readonly ICarWashSalesService _salesService = salesService;

		private string? UserCode => User.FindFirst("UserCode")?.Value;

		[HttpGet("GetVehicleTypes")]
		public async Task<IActionResult> GetVehicleTypes() => Ok(await _salesService.GetVehicleTypesAsync());

		[HttpGet("GetProducts")]
		public async Task<IActionResult> GetProducts([FromQuery] long vehicleTypeId) =>
			Ok(await _salesService.GetProductsAsync(vehicleTypeId));

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
	public class CarWashDashboardController(ICarWashDashboardService dashboardService) : ControllerBase
	{
		private readonly ICarWashDashboardService _dashboardService = dashboardService;

		[HttpGet("GetSummary")]
		public async Task<IActionResult> GetSummary()
		{
			var userCode = User.FindFirst("UserCode")?.Value;
			if (string.IsNullOrEmpty(userCode))
				return Unauthorized(ServiceResponse<object>.Error("Missing UserCode claim"));
			return Ok(await _dashboardService.GetDashboardSummaryAsync(userCode));
		}
	}

	[ApiController]
	[Route("fuelflow/carwash/customers")]
	[Authorize]
	public class CarwashCustomersController(ICarwashCustomerService service) : ControllerBase
	{
		private readonly ICarwashCustomerService _service = service;

		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] AddCarwashCustomerDto dto)
		{
			try
			{
				var customer = await _service.AddCustomerAsync(dto);
				return Ok(customer);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search([FromQuery] string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
				return BadRequest(new { message = "phoneNumber is required." });

			var results = await _service.SearchByPhoneAsync(phoneNumber);
			return Ok(results);
		}

		[HttpPost("credit-transactions")]
		public async Task<IActionResult> AddCreditTransaction([FromBody] CreateCreditTransactionDto dto)
		{
			try
			{
				var transaction = await _service.AddCreditTransactionAsync(dto);
				return Ok(transaction);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}


		[ApiController]
		[Route("api/car-wash/packages")]
		public class CarWashPackageController(ICarWashPackageService packageService) : ControllerBase
		{
			private readonly ICarWashPackageService _packageService = packageService;

			/// <summary>
			/// Get all active packages priced for a given vehicle type.
			/// </summary>
			[HttpGet]
			public async Task<IActionResult> GetPackages([FromQuery] long vehicleTypeId)
			{
				var response = await _packageService.GetPackagesAsync(vehicleTypeId);
				return Ok(response);
			}

			/// <summary>
			/// Get a single active package priced for a given vehicle type.
			/// </summary>
			[HttpGet("{packageId:long}")]
			public async Task<IActionResult> GetPackageById(long packageId, [FromQuery] long vehicleTypeId)
			{
				var response = await _packageService.GetPackageByIdAsync(packageId, vehicleTypeId);
				return Ok(response);
			}

			/// <summary>
			/// Create a new package bundling at least two products, with per-vehicle-type pricing.
			/// </summary>
			[HttpPost]
			public async Task<IActionResult> CreatePackage([FromBody] CreatePackageDto dto)
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _packageService.CreatePackageAsync(dto);
				return response.ResponseCode == 1
					? CreatedAtAction(nameof(GetPackageById), new { packageId = response.ResponseObject?.PackageId }, response)
					: BadRequest(response);
			}

			/// <summary>
			/// Update an existing active package's name, product composition, and pricing.
			/// </summary>
			[HttpPut("{packageId:long}")]
			public async Task<IActionResult> UpdatePackage(long packageId, [FromBody] UpdatePackageDto dto)
			{
				if (packageId != dto.PackageId)
					return BadRequest("Route packageId does not match body PackageId");

				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var response = await _packageService.UpdatePackageAsync(dto);
				return response.ResponseCode == 1 ? Ok(response) : BadRequest(response);
			}

			/// <summary>
			/// Soft-deletes (deactivates) a package. Historical sales retain their reference.
			/// </summary>
			[HttpDelete("{packageId:long}")]
			public async Task<IActionResult> DeactivatePackage(long packageId)
			{
				var response = await _packageService.DeactivatePackageAsync(packageId);
				return response.ResponseCode == 1 ? Ok(response) : BadRequest(response);
			}
		}
	}
}