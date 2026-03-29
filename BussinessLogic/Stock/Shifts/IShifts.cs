using DataAccessLayer.Common;

namespace BussinessLogic.Stock.Shifts
{
	public interface IShifts
	{
		Task<ServiceResponse<object>> DispenserStatus();
		Task<ServiceResponse<object>> ForceCloseShift(string ShiftNumber);
		Task<ServiceResponse<object>> OpenShifts();
		Task<ServiceResponse<object>> ShiftSales();
		Task<ServiceResponse<object>> ShiftStatuses();
	}
}