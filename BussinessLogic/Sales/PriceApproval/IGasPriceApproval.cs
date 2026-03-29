using DataAccessLayer.Common;

namespace BussinessLogic.Sales.PriceApproval
{
	public interface IGasPriceApproval
	{
		Task<ServiceResponse<object>> AddApprovalAsync(PriceApprovalDto model);
		Task<ServiceResponse<object>> AddPriceApproversAsync(string userCode);
		Task<ServiceResponse<object>> ApprovalList();
		Task<ServiceResponse<object>> AprrovePrice(string approvalCode);
		Task<ServiceResponse<List<PendingApprovalDto>>> GetPendingApprovalsAsync();
	}
}