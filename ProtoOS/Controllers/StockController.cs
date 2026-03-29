using AfricasTalkingCS;
using BusinessLogic.Stock.Stock;
using BussinessLogic.Reports.Shifts_Clossing;
using BussinessLogic.Stock.Shifts;
using BussinessLogic.Stock.Stock;
using BussinessLogic.Stock.Totalizers;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static BussinessLogic.Stock.Shifts.Shifts;
using static BussinessLogic.Stock.Totalizers.ReadingsTotalizers;

namespace ProtoOS.Controllers
{
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]
	

	public class StockController : ControllerBase
	{
		private readonly IStockServicecs _stockService;
		private readonly IShifts _shiftsService;
		private readonly ReadingsTotalizers _reading;
		private readonly IShiftClosingReport _closingReport;

		public StockController(IStockServicecs stockService, 
			IShifts shiftsService, ReadingsTotalizers reading, IShiftClosingReport closingReport)
		{
			_stockService = stockService;
			_shiftsService = shiftsService;
			_reading = reading;
			_closingReport = closingReport;
		}
		private IActionResult HandleResponse<T>(T response)
		{
			if (response == null)
			{
				return NotFound("No data found");
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("StockTake")]
		[Authorize(Roles = "can stock take")]
		public async Task<IActionResult> StockTake(StockTakeDto stockTake)
		{
			var response = await _stockService.StockTakeAsync(stockTake);
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("InitialStockTake")]
		[Authorize(Roles = "can initial stock take")]
		public async Task<IActionResult> InitialStockTake(StockTakeDto initialStockTakeDto)
		{
			var response = await _stockService.InitialStockTake(initialStockTakeDto);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("Getstocktakes")]
		[Authorize(Roles = "can view stock takes")]
		public async Task<IActionResult> GetStockTakes(string date)
		{
			var response = await _stockService.GetStockTakes(date);
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("Shiftvariances")]
		[Authorize(Roles = "can view shift variances")]
		public async Task<IActionResult> ShiftVariances()
		{
			var response = await _stockService.ShiftVariances();
			return HandleResponse(response);
		}


		[HttpGet]
		[Authorize]
		[Route("DispenserStatus")]
		public async Task<IActionResult> DispenserStatus() 
		{
			var response = await _shiftsService.DispenserStatus();
			return HandleResponse(response);
		}

		[HttpGet]
		[Route("Shiftstatus")]
		[Authorize]
		public async Task<IActionResult> ShiftStatus()
		{
			var response = await _shiftsService.ShiftStatuses();
			return HandleResponse(response);
		}

	

		[HttpGet]
		[Route("Saleshistory")]
		[Authorize(Roles = "can view sales history")]
		public async Task<IActionResult> ShiftSales()
		{
			var response = await _shiftsService.ShiftSales();
			return HandleResponse(response);
		}
		[HttpGet]
		[Route("AllVariances")]
		[Authorize(Roles = "can view all Variances")]
		public async Task<IActionResult> AllVariances(DateTime? date, string? shiftNumber, string? stationName)
		{
			var response = await _stockService.ListVariance(date, shiftNumber, stationName);
			return HandleResponse(response);
		}
		//Adjsut Socktakes
		[HttpPost]
		[Route("AdjustStockTake")]
		[Authorize(Roles = "can adjust totalizer readings")]
		public async Task<IActionResult> AdjustStockTake(AdjustStockTakeSummaryDto adjust)
		{
			var response = await _stockService.AdjustStockTakes(adjust);
			return HandleResponse(response);
		}
		//export all variances
	
		//GetTotalizerReadings
		[HttpGet]
		[Route("GetTotalizerReadings")]
		[Authorize]
		public async Task<IActionResult> GetTotalizerReadings()
		{
			var response = await _stockService.GetTotalizerReadings();
			return HandleResponse(response);
		}

		[HttpPost]
		[Route("ForceCloseShift")]
		[Authorize(Roles = "can force close shift")]
		public async Task<IActionResult>ForceCloseShift(string shiftNumber)
		{
			var response = await _shiftsService.ForceCloseShift(shiftNumber);
			return HandleResponse(response);
		}
		[HttpPost]
		[Route("ResetTotalizerReading")]
		[Authorize(Roles = "can reset totalizer Reading")]
		public async Task<IActionResult> ResetShift(string shiftNumber)
		{
			var response = await _stockService.ResetShift(shiftNumber);
			return HandleResponse(response);
		}


		//GetTotalizerReadings at a particular day
		[HttpGet]
		[Route("GetTotalizerReadings/{date}")]
		[Authorize]
		public async Task<IActionResult> GetTotalizerReadings(DateTime date = default)
		{
			var response = await _stockService.GetTotalizerReadings(date);
			return HandleResponse(response);
		}
		//GetTotalizerReadings at a particular day


		[HttpGet]
		[Route("ExportVarianceReport")]
		[Authorize(Roles = "can export allvariances to excel")]
		public async Task<IActionResult> ExportVarianceReport()
		{

			var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss")+"_VarianceReport.xlsx";

			var result = await _stockService.ExportAllVariances();
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
		}
		//GetTotalizerReadings at a particular day
		[HttpPost]
		[Route("RecordTotalizerReadings")]
		[Authorize(Roles = "can record totalizer readings")]
		public async Task<IActionResult> TakeTotalizerReadings([FromBody] List<NozzleReadingInput > nozzles)
		{
			if (nozzles == null || !nozzles.Any())
				return BadRequest("Nozzles data is required.");

			var response = await _reading.RecordTotalizerReadingsAsync(nozzles);
			return HandleResponse(response);
		}



		[Authorize]
		[HttpGet("ClosingReport")]
		public async Task<IActionResult> GetClosingReport(
			[FromQuery] string shiftNumber,
			[FromQuery] string dispenserCode)
		{
			if (string.IsNullOrWhiteSpace(shiftNumber) || string.IsNullOrWhiteSpace(dispenserCode))
				return BadRequest("shiftNumber and dispenserCode are required.");

			var result = await _closingReport.GenerateClosingReportAsync(shiftNumber, dispenserCode);
			return Ok(result);
		}
		
	}

}


