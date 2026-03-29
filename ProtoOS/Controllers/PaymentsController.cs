using BusinessLogic.Payments.PaymentSetups;
using DataAccessLayer.DTOs.Payments;
using DataAccessLayer.EntityModels.ProtoBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static BusinessLogic.Payments.PaymentSetups.PaymentsSetups;

namespace ProtoOS.Controllers
{
	[Route("payments/[controller]")]
	[ApiController]
	[Authorize]

	public class PaymentsController : ControllerBase
	{
		private readonly IPaymentsSetups _payments;

		public PaymentsController(IPaymentsSetups payments)
		{
			_payments = payments;
		}

		private IActionResult CreateResponse<T>(T response) => Ok(response);

		#region Payment Setup Endpoints

		[HttpGet("GetAllTills")]
		[Authorize(Roles = "can view all tills")]
		public async Task<IActionResult> GetAllTills()
		{
			var response = await _payments.GetTills();
			return CreateResponse(response);
		}

		[HttpPost("AddTill")]
		[Authorize(Roles = "can add a till")]
		public async Task<IActionResult> AddTill([FromBody] addTillNumberDto till)
		{
			var response = await _payments.AddTill(till);
			return CreateResponse(response);
		}

		[HttpPost("UpdateTill")]
		[Authorize(Roles = "can update a till")]
		public async Task<IActionResult> UpdateTill([FromBody] UpdateTillDto till)
		{
			var response = await _payments.UpdateTill(till);
			return CreateResponse(response);
		}

		[HttpPost("AssignTillToDispenser")]
		[Authorize(Roles = "can assign a till to a dispenser")]
		public async Task<IActionResult> AssignTillToDispenser([FromBody] AssignTillToDispenserDto till)
		{
			var response = await _payments.AssignTillToDispenser(till);
			return CreateResponse(response);
		}
		[HttpPost]
		[Authorize(Roles = "can add a payment method")]
		[Route("ConfirmPayment/{transId}/{dispenserCode}")]
		public async Task<IActionResult> ConfirmPayment(string transId, string dispenserCode)
		{
			var response = await _payments.ConfirmPayment(transId, dispenserCode);
			return CreateResponse(response);
		}

		[HttpPost]
		[Route("ConfirmGaragePayment/{transId}")]
		[Authorize(Roles = "can confirm garage payment")]
		public async Task<IActionResult> ConfirmGaragePayment(string transId)
		{
			var response = await _payments.ConfirmGaragePayment(transId);
			return CreateResponse(response);
		}

		[HttpGet]
		[Authorize(Roles = "can view mpesa transactions")]
		[Route("MpesaTransactions")]
		public async Task<IActionResult> MpesaTransactions(string? tillNumber, DateTime? dateFrom, DateTime? dateTo, string? transId)
		{
			var response = await _payments.MpesaTransactions(tillNumber, dateFrom, dateTo, transId);
			return CreateResponse(response);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mpesa"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = "can add mpesa transactions")]
		[Route("AddMpesaTransaction")]
		public async Task<IActionResult> AddMpesaTransaction(MpesaC2BPayment mpesa)
		{
			var response = await _payments.AddMpesaTransaction(mpesa);
			return CreateResponse(response);
		}

		[HttpPut]
		[Authorize(Roles = "can block mpesa code")]
		[Route("BlockMpesa")]
		public async Task<IActionResult> BlockMpesa(string transId)
		{
			var response = await _payments.BlockMpesa(transId);
			return CreateResponse(response);
		}

		[HttpPut]
		[Authorize(Roles = "can activate mpesa code")]
		[Route("ActivateMpesa")]
		public async Task<IActionResult> ActivateMpesa(string transId)
		{
			var response = await _payments.ActivateMpesa(transId);
			return CreateResponse(response);
		}

		//GetMpesaCodeUsage
		[HttpGet]
		[Authorize(Roles = "get mpesa code usage")]
		[Route("GetMpesaCodeUsage")]
		public async Task<IActionResult> GetMpesaCodeUsage(string transId)
		{
			var response = await _payments.GetMpesaCodeUsage(transId);
			return CreateResponse(response);
		}
		#endregion
	}
}
