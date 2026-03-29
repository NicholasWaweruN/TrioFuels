using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Payments;

namespace BusinessLogic.Payments.PaymentSetups
{
	public interface IPaymentsSetups
	{
		Task<ServiceResponse<object>> ActivateMpesa(string transId);
		Task<ServiceResponse<MpesaTransactionDto>> AddMpesaTransaction(MpesaC2BPayment mpesaC2BPayment);
		Task<ServiceResponse<object>> AddTill(addTillNumberDto till);
		Task<ServiceResponse<object>> AssignTillToDispenser(AssignTillToDispenserDto assignTill);
		Task<ServiceResponse<object>> BlockMpesa(string transId);
		Task<ServiceResponse<object>> ConfirmPayment(string transId, string dispenserCode);
		Task<ServiceResponse<object>> ConfirmGaragePayment(string transId);


		
		Task<ServiceResponse<object>> GetMpesaCodeUsage(string transId);
		Task<ServiceResponse<object>> GetTills();
		Task<ServiceResponse<object>> MpesaTransactions(string? tillNumber, DateTime? dateFrom, DateTime? dateTo, string? transId);
		Task<ServiceResponse<object>> UpdateTill(UpdateTillDto till);
		Task<ServiceResponse<object>> ValidateMpesaCode(string transId, string tillNumber);
	}
}