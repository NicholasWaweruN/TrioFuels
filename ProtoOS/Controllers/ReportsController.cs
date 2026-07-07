using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BussinessLogic.Reports;

namespace FuelFlow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReportsController : ControllerBase
	{
		private readonly IAllReports _reports;

		public ReportsController(IAllReports reports)
		{
			_reports = reports;
		}

		[HttpGet("ShiftReconciliation")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> ShiftReconciliation(string shiftNumber, string? stationCode = null)
		{
			var response = await _reports.GetShiftReconciliation(shiftNumber, stationCode);

			return Ok(response);
		}

		[HttpGet("SalesByPaymentType")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> SalesByPaymentType(string? stationCode, DateTime? startDate = null, DateTime? endDate = null)
		{
			var response = await _reports.GetSalesByPaymentType(stationCode, startDate, endDate);
			return Ok(response);
		}

		[HttpGet("ShiftSummary")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> ShiftSummary(string shiftNumber, string? stationCode = null)
		{
			var response = await _reports.GetShiftSummary(shiftNumber, stationCode);
			return Ok(response);
		}

		[HttpGet("MpesaUnusedCodes")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> MpesaUnusedCodes(string shiftNumber, string? tillNumber = null)
		{
			var response = await _reports.GetMpesaUnusedCodesByShift(shiftNumber, tillNumber);
			return Ok(response);
		}

		[HttpGet("CreditGiven")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> CreditGiven(string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			var response = await _reports.GetCreditGiven(stationCode, customerCode, startDate, endDate);
			return Ok(response);
		}

		[HttpGet("CreditRepayments")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> CreditRepayments(string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			var response = await _reports.GetCreditRepayments(stationCode, customerCode, startDate, endDate);
			return Ok(response);
		}

		[HttpGet("CreditAging")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> CreditAging(string? stationCode = null)
		{
			var response = await _reports.GetCreditAging(stationCode);
			return Ok(response);
		}

		[HttpGet("StockReconciliation")]
		[Authorize(Roles = "can view all sales")]
		public async Task<IActionResult> StockReconciliation(string shiftNumber, string? stationCode = null)
		{
			var response = await _reports.GetStockReconciliation(shiftNumber, stationCode);
			return Ok(response);
		}
	}
}