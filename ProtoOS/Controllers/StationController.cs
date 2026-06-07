using BusinessLogic.Station.DispenserAssignments;
using BusinessLogic.Station.Nozzzles;
using BusinessLogic.Station.Station;
using BusinessLogic.Station.StationDispenser;
using BusinessLogic.Station.StationTank;
using DataAccessLayer.DTOs.Shifts.Station;
using DataAccessLayer.EntityModels.Stations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Controllers 
{
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]


	public class StationController(
		IStationService stationService,
		IStationTanks stationTanks,
		IStationDispensers stationDispensers,
		IDispenserNozzle dispenserNozzle,
		IDispenserAssigments dispenserAssigments) : ControllerBase

	{
		private readonly IStationService _stationService = stationService;
		private readonly IStationTanks _stationTanks = stationTanks;
		private readonly IStationDispensers _stationDispensers = stationDispensers;
		private readonly IDispenserAssigments _dispenserAssigments = dispenserAssigments;
		private readonly IDispenserNozzle _dispenserNozzle = dispenserNozzle;

		private IActionResult HandleResponse<T>(T response)
		{
			if (response == null)
			{
				return NotFound("No data found");
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("AddStation")]
		[Authorize(Roles = "can add a station")]
		public async Task<IActionResult> AddStation(AddStationDto addStation)
		{
			var response = await _stationService.AddStation(addStation);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("UpdateStation")]
		[Authorize(Roles = "can update a station")]
		public async Task<IActionResult> UpdateStation(UpdateStationDto updateStation)
		{
			var response = await _stationService.UpdateStation(updateStation);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("GetAllStations")]
		[Authorize(Roles = "can view all stations")]
		public async Task<IActionResult> GetAllStations()
		{
			var response = await _stationService.GetStations();
			return HandleResponse(response);
		}

		// Station Tank Endpoints
		[HttpPost]
		[Route("AddTank")]
		[Authorize(Roles = "can add a tank")]
		public async Task<IActionResult> AddTank(AddTankDto addTank)
		{
			var response = await _stationTanks.AddTank(addTank);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("UpdateTank")]
		[Authorize(Roles = "can update a tank")]
		public async Task<IActionResult> UpdateTank(UpdateTankDto updateTank)
		{
			var response = await _stationTanks.UpdateTank(updateTank);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("GetAllTanks")]
		[Authorize(Roles = "can view all tanks")]
		public async Task<IActionResult> GetAllTanks()
		{
			var response = await _stationTanks.GetTanks();
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("StationTank")]
		[Authorize(Roles = "can view tanks in a station")]
		public async Task<IActionResult> StationTank(string stationCode)
		{
			var response = await _stationTanks.StationTank(stationCode);
			return HandleResponse(response);
		}

		// Station Dispenser Endpoints
		[HttpPost]
		[Route("AddDispenser")]
		[Authorize(Roles = "can add a dispenser")]
		public async Task<IActionResult> AddDispenser(AddDispenserDto addDispenser)
		{
			var response = await _stationDispensers.AddDispenser(addDispenser);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("UpdateDispenser")]
		[Authorize(Roles = "can update a dispenser")]
		public async Task<IActionResult> UpdateDispenser(UpdateDispenserDto updateDispenser)
		{
			var response = await _stationDispensers.UpdateDispenser(updateDispenser);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("GetAllDispensers")]
		[Authorize(Roles = "can view all dispensers")]
		public async Task<IActionResult> GetAllDispensers()
		{
			var response = await _stationDispensers.ListDispensers();
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("ListStationDispensers")]
		[Authorize(Roles = "can view all in a dispensers")]
		public async Task<IActionResult> ListStationDispensers(string stationCode)
		{
			var response = await _stationDispensers.ListStationDispensers(stationCode);
			return HandleResponse(response);
		}

		// Dispenser Assignment Endpoints
		[HttpPost]
		[Route("AssignDispenser")]
		[Authorize(Roles = "can assign a dispenser")]
		public async Task<IActionResult> AssignDispenser(DispenserAssignmentDto assignDispenser)
		{
			var response = await _dispenserAssigments.AssignDispenserAsync(assignDispenser);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("UnAssignDispenser")]
		[Authorize(Roles = "can unassign a dispenser")]
		public async Task<IActionResult> UnAssignDispenserAsync(string userCode)
		{
			var response = await _dispenserAssigments.UnAssignDispenserAsync(userCode);
			return HandleResponse(response);
		}

		[HttpGet]
        [Route("GetAllDispenserAssignments")]
		[Authorize(Roles = "can view all dispenser assignments")]
		public async Task<IActionResult> GetAllDispenserAssignments()
		{
			var response = await _dispenserAssigments.GetAllDispenserAssignmentsAsync();
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("GetAllDispenserAssignments/{stationCode}")]
		[Authorize(Roles = "can view all dispenser assignments")]
		public async Task<IActionResult> GetAllDispenserAssignments(string stationCode)
		{
			var response = await _dispenserAssigments.GetAllDispenserAssignmentsAsync(stationCode);
			return HandleResponse(response);
		}

		[HttpGet("AttendantsList")]
		[Authorize(Roles = "can view all attendants")]
		public async Task<IActionResult> AttendantsList()
		{
			var response = await _dispenserAssigments.GetAllAttendantsWithOtogasApp();
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("AddNozzle")]
		[Authorize(Roles = "can add a nozzle")]
		public async Task<IActionResult> AddNozzle(AddNozzleDto addNozzle)
		{
			var response = await _dispenserNozzle.AddNozzle(addNozzle);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("UpdateNozzle")]
		[Authorize(Roles = "can update a nozzle")]
		public async Task<IActionResult> UpdateNozzle(UpdateNozzleDto updateNozzle)
		{
			var response = await _dispenserNozzle.UpdateNozzle(updateNozzle);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("GetAllNozzles")]
		[Authorize(Roles = "can view all nozzles")]
		public async Task<IActionResult> GetAllNozzles()
		{
			var response = await _dispenserNozzle.GetNozzles();
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("ListDispenserNozzles")]
		[Authorize(Roles = "can View all nozzles in a dispenser")]
		public async Task<IActionResult> ListDispenserNozzles(string dispenserCode)
		{
			var response = await _stationDispensers.ListDispenserNozzles(dispenserCode);
			return HandleResponse(response);
		}


	}
}
