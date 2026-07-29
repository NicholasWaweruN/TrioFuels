/*
 * ⚠️ ASSUMPTIONS TO VERIFY
 * - Route prefix mirrors the SearchVehicle endpoint already used by
 *   WalletTopUp.jsx ("fuelflow/Customer/..."). Merge these actions into
 *   your existing CustomerController if you'd rather keep one controller.
 * - [Authorize] matches whatever auth attribute the rest of your
 *   fuelflow controllers use — adjust if it's policy-based or a custom
 *   attribute.
 * - Response envelope ({ responseObject, message }) matches the shape
 *   WalletTopUp.jsx already expects from SearchVehicle.
 */
using System;
using System.Threading.Tasks;
using BussinessLogic.Sales.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Controllers // rename to match your actual namespace
{
	[ApiController]
	[Route("fuelflow/Customer")]
	[Authorize]
	public class CustomerStatementController : ControllerBase
	{
		private readonly ICustomerStatementService _statementService;

		public CustomerStatementController(ICustomerStatementService statementService)
		{
			_statementService = statementService;
		}

		[HttpGet("Statement")]
		public async Task<IActionResult> GetStatement(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				return BadRequest(new { message = "customerCode is required." });

			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statementService.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			return Ok(new { responseObject = statement });
		}

		[HttpGet("Statement/Excel")]
		public async Task<IActionResult> DownloadStatementExcel(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statementService.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			var bytes = _statementService.BuildStatementExcel(statement);
			var fileName = $"Statement_{statement.CustomerCode}_{from:yyyyMMdd}_{to:yyyyMMdd}.xlsx";

			return File(
				bytes,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				fileName
			);
		}

		[HttpGet("Statement/Pdf")]
		public async Task<IActionResult> DownloadStatementPdf(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statementService.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			var bytes = _statementService.BuildStatementPdf(statement);
			var fileName = $"Statement_{statement.CustomerCode}_{from:yyyyMMdd}_{to:yyyyMMdd}.pdf";

			return File(bytes, "application/pdf", fileName);
		}
	}
}