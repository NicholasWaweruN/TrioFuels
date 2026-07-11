using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using DataAccessLayer.EntityModels.Grleamify;
using Microsoft.EntityFrameworkCore;

namespace TrioCarWash.Services.Services;

public interface ICarWashShiftService
{
	Task<ServiceResponse<ShiftDto>> OpenShiftAsync(string userCode, OpenShiftRequestDto request);
	Task<ServiceResponse<CloseShiftResponseDto>> CloseShiftAsync(string userCode, CloseShiftRequestDto request);
	Task<CarWashShift?> GetActiveShiftAsync(string userCode); // used internally by other services
}

public class CarWashShiftService(OTOContext db) : ICarWashShiftService
{
	private readonly OTOContext _db = db;

	public async Task<CarWashShift?> GetActiveShiftAsync(string userCode) =>
		await _db.CarWashShifts
			.Where(s => s.UserCode == userCode && s.Status == CarWashShiftStatus.Open)
			.OrderByDescending(s => s.DateCreated)
			.FirstOrDefaultAsync();

	public async Task<ServiceResponse<ShiftDto>> OpenShiftAsync(string userCode, OpenShiftRequestDto request)
	{
		var existing = await GetActiveShiftAsync(userCode);
		if (existing != null)
			return ServiceResponse<ShiftDto>.Error("You already have an open shift");

		var shift = new CarWashShift
		{
			UserCode = userCode,
			Name = string.IsNullOrWhiteSpace(request.ShiftName) ? "Shift" : request.ShiftName,
			Status = CarWashShiftStatus.Open
		};

		_db.CarWashShifts.Add(shift);
		await _db.SaveChangesAsync();

		return ServiceResponse<ShiftDto>.Success("Shift opened", new ShiftDto
		{
			ShiftId = shift.Id,
			ShiftName = shift.Name,
			OpenedAt = shift.DateCreated
		});
	}

	public async Task<ServiceResponse<CloseShiftResponseDto>> CloseShiftAsync(
		string userCode, CloseShiftRequestDto request)
	{
		var shift = await GetActiveShiftAsync(userCode);
		if (shift == null)
			return ServiceResponse<CloseShiftResponseDto>.Error("No open shift to close");

		var expectedCash = await _db.CarWashTransactions
			.Where(t => t.ShiftId == shift.Id
				&& !t.IsReversed
				&& t.PaymentMethod == CarWashPaymetMethod.Cash)
			.SumAsync(t => t.TotalAmount);

		var difference = request.ActualCashCounted - expectedCash;

		if (difference != 0 && string.IsNullOrWhiteSpace(request.VarianceReason))
			return ServiceResponse<CloseShiftResponseDto>.Error(
				"A reason is required when there is a cash variance");

		shift.ExpectedCash = expectedCash;
		shift.ActualCashCounted = request.ActualCashCounted;
		shift.Difference = difference;
		shift.VarianceReason = request.VarianceReason;
		shift.Status = CarWashShiftStatus.Closed;
		shift.ClosedAt = DateTime.UtcNow;

		await _db.SaveChangesAsync();

		return ServiceResponse<CloseShiftResponseDto>.Success("Shift closed", new CloseShiftResponseDto
		{
			ShiftId = shift.Id,
			ExpectedCash = expectedCash,
			ActualCashCounted = request.ActualCashCounted,
			Difference = difference,
			ClosedAt = shift.ClosedAt.Value
		});
	}
}