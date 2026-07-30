using BusinessLogic.Sales.Wallet;
using DataAccessLayer.Common;
using DataAccessLayer.EntityModels.Wallet;
using Microsoft.AspNetCore.Http;
using static DataAccessLayer.EntityModels.Wallet.WalletDto;

namespace BussinessLogic.Sales.Wallet
{
	public interface IWalletTransactions
	{

		Task<ServiceResponse<byte[]>> ExportCustomerTransactions(string vehicleCode);
		Task<ServiceResponse<object>> GetCustomerPayments(string vehicleCode);
		Task<ServiceResponse<List<WalletDto.CustomerTransactionDto>>> GetCustomerStatement(string vehicleCode, DateTime startDate, DateTime endDate);
		Task<ServiceResponse> ReverseTopUpFundssWallet(WalletDto.TopUpFundsDto customerFunds);
		Task<ServiceResponse<object>> TopUpCustomerWalletAsync(TopUpCustomerWalletDto dto);
		Task<ServiceResponse> TopUpFundssWallet(WalletDto.TopUpFundsDto customerFunds);
		Task<ServiceResponse<List<TopUpTypesDto>>> TopUpTypes();
		Task<ServiceResponse> TransferCustomerBalance(WalletDto.TransferCustomerBalanceDto transferCustomerBalanceDto);
		Task<ServiceResponse<object>> WalletHistories(string vRegno);
	}
}