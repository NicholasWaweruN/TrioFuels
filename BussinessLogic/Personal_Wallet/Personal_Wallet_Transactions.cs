using AfricasTalkingCS;
using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.EmailService;
using BusinessLogic.Sales.CommonSalesTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Personal_Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Setup;

namespace BussinessLogic.Personal_Wallet
{
    public class Personal_Wallet_Transactions
    {
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IMessagingService _messaging;
		public Personal_Wallet_Transactions(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, ICommonSalesTasks salesTasks,IMessagingService messaging)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_salesTasks = salesTasks;
			_messaging = messaging;
		}

		//Add Wallet TransactionAsync
		public async Task<ServiceResponse<object>> Personal_WalletTopUp(Personal_Wallet_TransactionDto transaction)
		{
			try
			{
				var wallet = await _context.Personal_Wallet_Customers.FirstOrDefaultAsync(x => x.WalletId == transaction.WalletId);
				if (wallet is null)
				{
					return ServiceResponse<object>.Information("Customer not found", null);
				}

				if (transaction.Amount <= 0)
				{
					return ServiceResponse<object>.Information("Invalid amount", null);
				}

				var walletTransaction = new Wallet_Transactions_Personal
				{
					Credit = transaction.Amount,
					Debit = 0,
					TransactionType = "0",
					TransactionCode = await _setups.GetCodeGenerator("TransactionId"),
					WalletId = wallet.WalletId,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					Description = "Wallet top up",
					SaleId = _setups.GenerateSaleId(),
					VehicleCode = transaction.VehicleCode,
				};

				await _context.AddAsync(walletTransaction);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Wallet transaction added successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while adding wallet transaction: {ex.Message}", null);
			}
		}

		//get wallet balance
		public async Task<ServiceResponse<decimal>> GetWalletBalance(string walletId)
		{
			try
			{
				

				var wallet = await (from w in  _context.Personal_Wallet_Customers
									join t in _context.Wallet_Transactions_Personal on w.WalletId equals t.WalletId
									where w.WalletId.Equals(walletId)
									select t ).SumAsync(x => x.Credit-x.Debit);

				return ServiceResponse<decimal>.Success("Wallet balance retrieved successfully", wallet);
			}
			catch (Exception ex)
			{
				return ServiceResponse<decimal>.Error($"An error occurred while retrieving wallet balance: {ex.Message}", 0);
			}
		}

		// get profile data with wallet balances
		public async Task<ServiceResponse<object>> GetProfileData(string phoneNumber)
		{
			try
			{
			var phoneNumber2 = _messaging.NormalizePhoneNumber(phoneNumber);	
				if (phoneNumber2 == null)
					return ServiceResponse<object>.Information($"Invalid phonenumber {phoneNumber}",null);

				var wallet = await _context.Personal_Wallet_Customers.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber2);
				if (wallet == null)
				{
					return ServiceResponse<object>.Information("Customer not found", null);
				}


				var walletBalance = await (from w in _context.Personal_Wallet_Customers
														join t in _context.Wallet_Transactions_Personal on w.WalletId equals t.WalletId
														where w.PhoneNumber.Equals(phoneNumber2)
														select t).SumAsync(x => x.Credit - x.Debit);

				var names = string.Join(" ", wallet.FirstName, wallet.MiddleName, wallet.LastName);
				var profileData = new
				{
					wallet.WalletId,
					wallet.FirstName,
					wallet.PhoneNumber,
					wallet.Email,
					wallet.IdentificationNumber,
					wallet.Receive_Receipts,
					wallet.Receive_Statements,
					wallet.Discount,
					wallet.Credit,
					Balance = walletBalance,
				};
				return ServiceResponse<object>.Success("Profile data retrieved successfully", profileData);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while retrieving profile data: {ex.Message}", null);
			}
		}


		public class Personal_Wallet_TransactionDto
		{
			public string WalletId { get; set; } = string.Empty;
			public decimal Amount { get; set; }
			public string VehicleCode { get; set; } = string.Empty;

		}

	}
}
