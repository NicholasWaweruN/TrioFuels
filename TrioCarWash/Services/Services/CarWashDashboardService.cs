using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using Microsoft.EntityFrameworkCore;
using TrioCarWash.Services.Services;

namespace FuelFlow.Services.CarWash;

public interface ICarWashDashboardService
{
	Task<ServiceResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(string userCode);
}

public class CarWashDashboardService : ICarWashDashboardService
{
	private readonly OTOContext _db;
	private readonly ICarWashShiftService _shiftService;

	public CarWashDashboardService(OTOContext db, ICarWashShiftService shiftService)
	{
		_db = db;
		_shiftService = shiftService;
	}

	public async Task<ServiceResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(string userCode)
	{
		var shift = await _shiftService.GetActiveShiftAsync(userCode);
		if (shift == null)
			return ServiceResponse<DashboardSummaryDto>.Information("No active shift", new DashboardSummaryDto
			{
				ShiftActive = false
			});

		var totals = await _db.CarWashTransactions
			.AsNoTracking()
			.Where(t => t.ShiftId == shift.Id && !t.IsReversed)
			.GroupBy(t => 1)
			.Select(g => new
			{
				Total = g.Sum(t => t.TotalAmount),
				Cash = g.Where(t => t.PaymentMethod == CarWashPaymetMethod.Cash).Sum(t => t.TotalAmount),
				Mpesa = g.Where(t => t.PaymentMethod == CarWashPaymetMethod.Mpesa).Sum(t => t.TotalAmount),
				Count = g.Count()
			})
			.FirstOrDefaultAsync();

		var dto = new DashboardSummaryDto
		{
			ShiftId = shift.Id,
			ShiftName = shift.Name,
			ShiftActive = true,
			OpenedAt = shift.DateCreated,
			TotalSales = totals?.Total ?? 0,
			CashSales = totals?.Cash ?? 0,
			MpesaSales = totals?.Mpesa ?? 0,
			TransactionCount = totals?.Count ?? 0
		};

		return ServiceResponse<DashboardSummaryDto>.Success("OK", dto);
	}
}