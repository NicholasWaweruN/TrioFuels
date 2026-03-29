using BusinessLogic.Sales.Wallet;
using DataAccessLayer.Common;
using DataAccessLayer.EntityModels.Wallet;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Sales.Wallet
{
	public interface IWalletTransactions
	{
		Task<ServiceResponse<byte[]>> CustomerStatement(string customerCode);
		Task<ServiceResponse<byte[]>> CustomerStatement2(string customerCode, DateTime from);
		Task<ServiceResponse<byte[]>> CustomerStatementAsPdf(string customerCode, DateTime from);
		Task<ServiceResponse<byte[]>> ExportCustomerTransactions(string vehicleCode);
		Task<ServiceResponse<byte[]>> ExportCustomerTransactionsEplus(string vehicleCode);
		Task<ServiceResponse<List<WalletDto.CustomerBalanceDto>>> GetAllCustomerBalances();
		Task<ServiceResponse<object>> GetAllCustomerBalancesSql(string? registrationNumber = null, string? customerName = null, int pageNumber = 1, int pageSize = 15);
		Task<ServiceResponse<object>> GetCustomerBalances(string? registrationNumber = null, string? customerName = null, int pageNumber = 1, int pageSize = 15);
		Task<ServiceResponse<object>> GetCustomerPayments(string vehicleCode);
		Task<ServiceResponse<List<WalletDto.CustomerTransactionDto>>> GetCustomerStatement(string vehicleCode, DateTime startDate, DateTime endDate);
		Task<ServiceResponse> ReverseTopUpFundssWallet(WalletDto.TopUpFundsDto customerFunds);
		Task<ServiceResponse> TopUpCustomerWallet(WalletDto.TopUpCustomerWalletDto topUpCustomerWalletDto);
		Task<ServiceResponse> TopUpFundssWallet(WalletDto.TopUpFundsDto customerFunds);
		Task<ServiceResponse<List<TopUpTypesDto>>> TopUpTypes();
		Task<ServiceResponse> TransferCustomerBalance(WalletDto.TransferCustomerBalanceDto transferCustomerBalanceDto);
		Task<ServiceResponse<object>> UploadCustomerTransactions(IFormFile file, int topUpType);
		Task<ServiceResponse<object>> WalletHistories(string vRegno);
	}
}