using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts;
using DataAccessLayer.EntityModels.Shifts;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Shifts
{
	public interface IShiftSupervisorReconciliationService
	{
		Task<ServiceResponse<ShiftSupervisorReconciliationResponse>> SubmitReconciliationAsync(ShiftSupervisorReconciliationRequest request);
		Task<ServiceResponse<ShiftSupervisorReconciliationResponse>> GetReconciliationAsync(string shiftNumber);
	}

	public class ShiftSupervisorReconciliationService : IShiftSupervisorReconciliationService
	{
		private readonly OTOContext _db;
		private const decimal VarianceToleranceKes = 50m;
		private readonly IAuthCommonTasks _authentication;

		public ShiftSupervisorReconciliationService(OTOContext db, IAuthCommonTasks authentication)
		{
			_db = db;
			_authentication = authentication;
		}

		public async Task<ServiceResponse<ShiftSupervisorReconciliationResponse>> SubmitReconciliationAsync(ShiftSupervisorReconciliationRequest request)
		{
			string userCode = _authentication.Usercode();

			var shiftExists = await _db.Shifts
				.AsNoTracking()
				.AnyAsync(s => s.ShiftNumber == request.ShiftNumber);

			if (!shiftExists)
				return ServiceResponse<ShiftSupervisorReconciliationResponse>.Error($"Shift {request.ShiftNumber} not found.");

			var systemTotals = await GetSystemTotalsAsync(request.ShiftNumber);

			// Tracked (no AsNoTracking) so we can update in place if a record already exists.
			var entity = await _db.ShiftSupervisorReconciliations
				.FirstOrDefaultAsync(r => r.ShiftNumber == request.ShiftNumber);

			bool isUpdate = entity is not null;

			if (entity is null)
			{
				entity = new ShiftSupervisorReconciliation
				{
					ShiftNumber = request.ShiftNumber
				};
				_db.ShiftSupervisorReconciliations.Add(entity);
			}

			entity.MpesaReceived = request.MpesaReceived;
			entity.CashReceived = request.CashReceived;
			entity.CreditReceived = request.CreditReceived;
			entity.LoyaltyPointsUsed = request.LoyaltyPointsUsed;
			entity.PdqReceived = request.PdqReceived;
			entity.SystemMpesaTotal = systemTotals.Mpesa;
			entity.SystemCashTotal = systemTotals.Cash;
			entity.SystemCreditTotal = systemTotals.Credit;
			entity.SystemLoyaltyTotal = systemTotals.Loyalty;
			entity.SystemPdqTotal = systemTotals.Pdq;
			entity.UserCode = userCode;

			await _db.SaveChangesAsync();

			var response = BuildResponse(entity);
			var message = isUpdate ? "Reconciliation updated." : "Reconciliation submitted.";
			return ServiceResponse<ShiftSupervisorReconciliationResponse>.Success(message, response);
		}

		public async Task<ServiceResponse<ShiftSupervisorReconciliationResponse>> GetReconciliationAsync(string shiftNumber)
		{
			var entity = await _db.ShiftSupervisorReconciliations
				.AsNoTracking()
				.Where(r => r.ShiftNumber == shiftNumber)
				.OrderByDescending(r => r.DateCreated)
				.FirstOrDefaultAsync();

			if (entity is null)
				return ServiceResponse<ShiftSupervisorReconciliationResponse>.Error("No reconciliation found for this shift.");

			var response = BuildResponse(entity);
			return ServiceResponse<ShiftSupervisorReconciliationResponse>.Success("Reconciliation found.", response);
		}

		private async Task<ShiftPaymentTotals> GetSystemTotalsAsync(string shiftNumber)
		{
			var totals = await (
				from t in _db.QuantityTransactions.AsNoTracking()
				join pt in _db.PaymentTypes.AsNoTracking()
					on t.PaymentTypeCode equals pt.PaymentTypeId
				where t.ShiftNumber == shiftNumber && !t.IsReversed
				group t by pt.PaymentTypeId into g
				select new { Key = g.Key, Total = g.Sum(x => x.AmountCredit - x.AmountDebit) }
			).ToListAsync();

			decimal Get(int code) => totals.FirstOrDefault(t => t.Key == code)?.Total ?? 0m;

			return new ShiftPaymentTotals
			{
				Mpesa = Get(PaymetMethod.Mpesa),
				Cash = Get(PaymetMethod.Cash),
				Credit = Get(PaymetMethod.Credit),
				Loyalty = Get(PaymetMethod.Loyalty),
				Pdq = Get(PaymetMethod.PDQ)
			};
		}

		private static ShiftSupervisorReconciliationResponse BuildResponse(ShiftSupervisorReconciliation e)
		{
			static ReconciliationLineDto Line(string cat, decimal sys, decimal sup) => new()
			{
				Category = cat,
				SystemAmount = sys,
				SupervisorAmount = sup,
				IsMatched = Math.Abs(sup - sys) <= VarianceToleranceKes
			};

			return new ShiftSupervisorReconciliationResponse
			{
				Id = e.Id,
				ShiftNumber = e.ShiftNumber,
				Lines = new()
				{
					Line("M-Pesa", e.SystemMpesaTotal, e.MpesaReceived),
					Line("Cash", e.SystemCashTotal, e.CashReceived),
					Line("Credit", e.SystemCreditTotal, e.CreditReceived),
					Line("Loyalty", e.SystemLoyaltyTotal, e.LoyaltyPointsUsed),
					Line("PDQ", e.SystemPdqTotal, e.PdqReceived),
				}
			};
		}
	}

	internal class ShiftPaymentTotals
	{
		public decimal Mpesa { get; set; }
		public decimal Cash { get; set; }
		public decimal Credit { get; set; }
		public decimal Loyalty { get; set; }
		public decimal Pdq { get; set; }
	}
}