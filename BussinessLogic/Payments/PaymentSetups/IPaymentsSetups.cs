using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Payments;
using static BussinessLogic.Payments.PaymentSetups.PaymentsSetups;

namespace BussinessLogic.Payments.PaymentSetups
{
	public interface IPaymentsSetups
	{
		Task<ServiceResponse<object>> ActivateMpesa(string transId);
		Task<ServiceResponse<MpesaTransactionDto>> AddMpesaTransaction(MpesaC2BPayment mpesaC2BPayment);
		Task<ServiceResponse<object>> AddTill(addTillNumberDto till);
		Task<ServiceResponse<object>> AssignTillToDispenser(AssignTillToDispenserDto assignTill);
		Task<ServiceResponse<object>> BlockMpesa(string transId);
		Task<ServiceResponse<byte[]>> ExportMpesaTransactions(string? tillNumber, string? dateFrom, string? dateTo, string? transId, CancellationToken ct = default);
		Task<ServiceResponse<object>> GetMpesaCodeUsage(string transId);
		Task<ServiceResponse<object>> GetTills();
		Task<ServiceResponse<List<UnusedMpesaTransactionDto>>> GetUnusedMpesaTransactionsAsync();
		Task<ServiceResponse<object>> MpesaTransactions(string? tillNumber, DateTime? dateFrom, DateTime? dateTo, string? transId);
		Task<ServiceResponse<object>> UpdateTill(UpdateTillDto till);
	}
}