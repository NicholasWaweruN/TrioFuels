using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Credit;

namespace BussinessLogic.Sales.Credit_Management
{
	public interface ICreditManagement
	{
		Task<ServiceResponse<object>> CheckifIsAcreditCustomer(string customerCode);
		Task<ServiceResponse<object>> RepayCreditAsync(CreditRepaymentDto dto);
	}
}