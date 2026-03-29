using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Messaging;
using Microsoft.EntityFrameworkCore;
using AfricasTalkingCS;
using BussinessLogic.Worker.OtherReports;

namespace AfricasTalkingSmsCallback.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[AllowAnonymous]
	public class SmsCallbackController : ControllerBase
	{
		private readonly ILogger<SmsCallbackController> _logger;
		private readonly OTOContext _context;
		private readonly SalesReport_Summary salesReport_Summary;
		public SmsCallbackController(ILogger<SmsCallbackController> logger,OTOContext context,SalesReport_Summary salesReport_)
		{
			_logger = logger;
			_context = context;
			salesReport_Summary = salesReport_;

		}

		[HttpPost("Receive")]
		[AllowAnonymous]
		public IActionResult ReceiveSmsCallback(SmsDeliveryStatusCallback request)
		{ 
			////Process the SMS callback data(you can log, save to database, etc.)
			//// Example: log message details

			var callback = new SmsCallbacks
			{
				NetworkCode = request.networkCode ?? string.Empty,
				Cost = 0,
				MessageId = request.id ?? string.Empty,
				PhoneNumber = request.phoneNumber ?? string.Empty,
				Status = request.status ?? string.Empty,
				DateAdded = DateTime.UtcNow,
				FailureReason = request.failureReason ?? string.Empty,

			};
			_context.SmsCallbacks.Add(callback);
			_context.SaveChanges();

			return Ok(); // Respond with 200 OK to acknowledge receipt
		}

		[HttpPost("process-callback")]
		public async Task<IActionResult> ProcessCallback()
		{
			var logPrefix = "SMS Callback";
			try
			{
				// Read and parse form data
				var form = await Request.ReadFormAsync();

				var phoneNumber = form["phoneNumber"].ToString();
				var retryCount = form["retryCount"].ToString();
				var messageId = form["id"].ToString();
				var status = form["status"].ToString();
				var networkCode = form["networkCode"].ToString();
				var failureReason = form["failureReason"].ToString();
				// Validate required parameters
				if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(messageId) || string.IsNullOrWhiteSpace(status))
				{
					_logger.LogWarning("{LogPrefix}: Missing required parameters in callback data.", logPrefix);
					return BadRequest(new { Message = "Missing required parameters: phoneNumber, id, or status." });
				}

				_logger.LogInformation("{LogPrefix}: Received callback for MessageId {MessageId}", logPrefix, messageId);

				// Update database
				using var transaction = await _context.Database.BeginTransactionAsync();

				var message = await _context.BulkMessageLogs.FirstOrDefaultAsync(m => m.MessageId == messageId);
				if (message != null)
				{
					message.DeliveryStatus = status;
					_context.BulkMessageLogs.Update(message);
				}

				var callback = new SmsCallbacks
				{
					MessageId = messageId,
					Cost = 0,
					DateAdded = DateTime.UtcNow,
					NetworkCode = networkCode,
					FailureReason = failureReason,
					PhoneNumber = phoneNumber,
					Status = status
				};
				_context.SmsCallbacks.Add(callback);

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				_logger.LogInformation("{LogPrefix}: Successfully processed callback for MessageId {MessageId}", logPrefix, messageId);
				return Ok(new { Message = "Callback processed successfully." });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{LogPrefix}: Error processing callback.", logPrefix);
				return StatusCode(500, new { Message = "An error occurred while processing the callback." });
			}
		}
		[HttpGet]
		[Route("GenerateMonthlyStationReportsToStream")]
		[Authorize(Roles = "can view customer statement")]
		public IActionResult GenerateMonthlyStationReports(int year, int month)
		{
			// Generate the report as a memory stream
			var result = salesReport_Summary.GenerateMonthlyStationReportsToStream(year, month);

			if (result == null)
			{
				return BadRequest("An error occurred while exporting the station reports.");
			}

			// Convert the memory stream to a byte array
			var fileBytes = result.ToArray();

			// Return the file as a downloadable response
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"StationReports_{year}_{month}.xlsx");
		}

	}

	public class SmsDeliveryStatusCallback
	{
		public string? id { get; set; } = string.Empty;
		public string?status { get; set; } = string.Empty;
		public string? phoneNumber { get; set; } = string.Empty;
		public string? networkCode { get; set; } = string.Empty;
		public string? failureReason { get; set; } = string.Empty;
		public int? retryCount { get; set; }
	}

}
