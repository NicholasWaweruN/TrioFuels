using BussinessLogic.Sales.PriceApproval;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FuelFlow.Controllers 
{
	[ApiController]
	[Route("fuelflow/[controller]")]
	[Authorize] // Optional: enforce authentication
	public class GasPriceApprovalController : ControllerBase
	{
		private readonly IGasPriceApproval _gasPriceApproval;

		public GasPriceApprovalController(IGasPriceApproval gasPriceApproval)
		{
			_gasPriceApproval = gasPriceApproval;
		}
		/// <summary>
		/// Add a new gas price approval request.
		/// </summary>
		[HttpPost("add-price-for-approval")]
		[Authorize(Roles = "can add price for approval")]
		public async Task<IActionResult> AddApproval([FromBody] PriceApprovalDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _gasPriceApproval.AddApprovalAsync(model);
			return Ok(result);
		}

		/// <summary>
		/// Get all pending price approvals.
		/// </summary>
		[HttpGet("pending")]
		[Authorize(Roles = "can view pending approvals")]
		public async Task<IActionResult> GetPendingApprovals()
		{
			var result = await _gasPriceApproval.GetPendingApprovalsAsync();
			return Ok(result);
		}

		/// <summary>
		/// Approve a price approval request.
		/// </summary>
		[HttpPost("approve/{approvalCode}")]
		[Authorize(Roles = "can approve price")]
		public async Task<IActionResult> ApprovePrice([Required]string approvalCode)
		{
			var result = await _gasPriceApproval.AprrovePrice(approvalCode);
			return Ok(result);
		}

		/// <summary>
		/// Add a price approver.
		/// </summary>
		[HttpPost("add-approver/{userCode}")]
		[Authorize(Roles = "can add price approvers")]
		public async Task<IActionResult> AddPriceApprover([Required] string userCode)
		{
			var result = await _gasPriceApproval.AddPriceApproversAsync(userCode);
			return Ok(result);
		}
	}
}
