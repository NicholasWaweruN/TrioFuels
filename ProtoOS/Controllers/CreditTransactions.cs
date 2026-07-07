using BussinessLogic.Sales.Credit_Management;
using DataAccessLayer.DTOs.Credit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Controllers
{
	[ApiController]
	[Route("fuelflow/Sales/[controller]")]
	public class CreditTransactionsController : ControllerBase
	{
		private readonly ICreditManagement _creditManagement;

		public CreditTransactionsController(ICreditManagement creditManagement)
		{
			_creditManagement = creditManagement;
		}

		/// <summary>
		/// Records a credit repayment (Cash, PDQ, or Mpesa — see CreditRepaymentMethod).
		/// This mutates data (inserts a CreditTransactions row, and for Mpesa also
		/// consumes the M-Pesa code's balance), so it's a POST with the details in
		/// the request body rather than a GET with no way to pass them.
		/// </summary>
		[HttpPost("repay")]
		[Authorize]
		public async Task<IActionResult> RepayCredit([FromBody] CreditRepaymentDto dto)
		{
			var result = await _creditManagement.RepayCreditAsync(dto);
			return Ok(result);
		}

		/// <summary>
		/// Checks whether a customer is approved for credit purchases.
		/// </summary>
		[HttpGet("is-credit-customer")]
		[Authorize]
		public async Task<IActionResult> CheckIfIsCreditCustomer([FromQuery] string customerCode)
		{
			var result = await _creditManagement.CheckifIsAcreditCustomer(customerCode);
			return Ok(result);
		}
	}
}