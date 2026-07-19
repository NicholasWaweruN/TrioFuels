using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts;
using DataAccessLayer.EntityModels.Shifts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogic.Shifts
{
	public interface IShiftSupervisorReconciliationService
	{
		Task<ShiftSupervisorReconciliationResponse> SubmitReconciliationAsync(ShiftSupervisorReconciliationRequest request, string userCode);

		Task<ShiftSupervisorReconciliationResponse?> GetReconciliationAsync(string shiftNumber);
	}

	public class ShiftSupervisorReconciliationService : IShiftSupervisorReconciliationService
	{
		private readonly OTOContext _db;
		private const decimal VarianceToleranceKes = 50m;

		public ShiftSupervisorReconciliationService(OTOContext db)
		{
			_db = db;
		}

		public async Task<ShiftSupervisorReconciliationResponse> SubmitReconciliationAsync(
			ShiftSupervisorReconciliationRequest request, string userCode)
		{
			var shiftExists = await _db.Shifts
				.AsNoTracking()
				.AnyAsync(s => s.ShiftNumber == request.ShiftNumber);

			if (!shiftExists)
				throw new InvalidOperationException($"Shift {request.ShiftNumber} not found.");

			var systemTotals = await GetSystemTotalsAsync(request.ShiftNumber);

			// NOTE: entity write, so this must go through the tracking context,
			// not a NoTracking query — same pitfall you hit with the sales flow.
			var entity = new ShiftSupervisorReconciliation
			{
				ShiftNumber = request.ShiftNumber,
				MpesaReceived = request.MpesaReceived,
				CashReceived = request.CashReceived,
				CreditReceived = request.CreditReceived,
				LoyaltyPointsUsed = request.LoyaltyPointsUsed,
				PdqReceived = request.PdqReceived,
				SystemMpesaTotal = systemTotals.Mpesa,
				SystemCashTotal = systemTotals.Cash,
				SystemCreditTotal = systemTotals.Credit,
				SystemLoyaltyTotal = systemTotals.Loyalty,
				SystemPdqTotal = systemTotals.Pdq,
				UserCode = userCode
			};

			_db.ShiftSupervisorReconciliations.Add(entity);
			await _db.SaveChangesAsync();

			return BuildResponse(entity);
		}

		public async Task<ShiftSupervisorReconciliationResponse?> GetReconciliationAsync(string shiftNumber)
		{
			var entity = await _db.ShiftSupervisorReconciliations
				.AsNoTracking()
				.Where(r => r.ShiftNumber == shiftNumber)
				.OrderByDescending(r => r.DateCreated)
				.FirstOrDefaultAsync();

			return entity is null ? null : BuildResponse(entity);
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
