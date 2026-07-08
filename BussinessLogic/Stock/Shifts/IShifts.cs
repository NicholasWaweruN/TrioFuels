using DataAccessLayer.Common;
using static BussinessLogic.Stock.Shifts.Shifts;

namespace BussinessLogic.Stock.Shifts
{
	public interface IShifts
	{
		Task<List<ShiftStatusDto>> GetClosedVarianceDeferredShiftsAsync();
		Task<ServiceResponse<object>> DispenserStatus();
		Task<ServiceResponse<object>> ForceCloseShift(string ShiftNumber);
		Task<ServiceResponse<object>> OpenShifts();
		Task<ServiceResponse<object>> ShiftSales();
		Task<ServiceResponse<object>> ShiftStatuses();
	}
}