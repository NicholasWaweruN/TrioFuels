using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Credit;
using DataAccessLayer.EntityModels.CreditTransactions;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BussinessLogic.Sales.Credit_Management
{
	public class CreditManagement : ICreditManagement
	{

		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		public CreditManagement(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
		}

		public async Task<ServiceResponse<object>> CheckifIsAcreditCustomer(string customerCode)
		{

			var customer = await _context.Customers.Where(x => x.CustomerCode.Equals(customerCode)
								&& x.IsCreditCustomer)
							.FirstOrDefaultAsync();
			if (customer == null)
			{
				return ServiceResponse<object>.Information("is not a credit customer", null);
			}

			return ServiceResponse<object>.Success("is a credit customer", customer);
		}

		/// <summary>
		/// Public read of a customer's current outstanding credit balance — used
		/// by the front end to show a live balance preview before recording a
		/// repayment. Thin wrapper around the same GetOutstandingCreditAsync used
		/// internally by RepayCreditAsync, so the two never drift apart.
		/// </summary>
		public async Task<ServiceResponse<object>> GetOutstandingBalanceAsync(string customerCode)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				return ServiceResponse<object>.Information("Customer code is required", null);

			var customerExists = await _context.Customers
				.AsNoTracking()
				.AnyAsync(c => c.CustomerCode == customerCode);

			if (!customerExists)
				return ServiceResponse<object>.Information("Customer not found", null);

			var outstanding = await GetOutstandingCreditAsync(customerCode);

			return ServiceResponse<object>.Success("Outstanding balance retrieved", new { Outstanding = outstanding });
		}

		// =====================================================================
		// Credit repayment — Cash / PDQ / Mpesa
		// =====================================================================

		/// <summary>
		/// Records a repayment against a customer's outstanding credit balance,
		/// via Cash, PDQ, or Mpesa (dto.PaymentTypeCode selects the branch).
		///
		/// Mirrors Sales.HandleCreditAsync's debit entry: that method adds Debit to
		/// increase exposure, this adds Credit to reduce it — GetOutstandingCreditAsync
		/// already sums (Debit - Credit), so no changes needed there.
		///
		/// Cash/PDQ: the credited amount is dto.AmountPaid exactly.
		///
		/// Mpesa: NEVER partially used. The code is row-locked (FOR UPDATE), validated
		/// against the till number, and — if it still has a positive UsageBalance —
		/// its ENTIRE remaining balance is what gets credited (dto.AmountPaid is
		/// ignored for this branch). The code's UsageBalance is then zeroed and Status
		/// flipped to fully-used, in the SAME transaction as the CreditTransactions
		/// insert, so the repayment and the balance debit either both commit or both
		/// roll back together. (This differs from Sales' sale flow, which reconciles
		/// Mpesa usage AFTER commit via a SaleId join — a repayment has no
		/// QuantityTransactions/SaleId to join through, so doing it inline here is
		/// both simpler and correct for this case. It also intentionally does NOT
		/// write a PaymentTransactions row — the full amount only needs to appear in
		/// CreditTransactions.)
		///
		/// AllowOverpayment defaults to true: a repayment (cash, PDQ slip, or the full
		/// M-Pesa balance) can exceed the outstanding balance and is still accepted,
		/// producing a negative NewBalance (a credit surplus) rather than being
		/// rejected. Pass false if you want the old strict behavior back.
		/// </summary>
		public async Task<ServiceResponse<object>> RepayCreditAsync(CreditRepaymentDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.CustomerCode))
				return ServiceResponse<object>.Information("Customer code is required", null);

			if (string.IsNullOrWhiteSpace(dto.StationCode))
				return ServiceResponse<object>.Information("Station code is required", null);

			if (dto.PaymentTypeCode == CreditRepaymentMethod.Mpesa)
			{
				if (string.IsNullOrWhiteSpace(dto.TransactionReference))
					return ServiceResponse<object>.Information("Mpesa transaction code is required", null);

				if (string.IsNullOrWhiteSpace(dto.MpesaTillNumber))
					return ServiceResponse<object>.Information("Till number is required to verify the Mpesa code", null);
			}
			else if (dto.PaymentTypeCode == CreditRepaymentMethod.Cash || dto.PaymentTypeCode == CreditRepaymentMethod.PDQ)
			{
				if (dto.AmountPaid <= 0)
					return ServiceResponse<object>.Information("Payment amount must be greater than zero", null);
			}
			else
			{
				return ServiceResponse<object>.Information("Unsupported repayment method — only Cash, PDQ, and Mpesa are accepted", null);
			}

			var customerExists = await _context.Customers
				.AsNoTracking()
				.AnyAsync(c => c.CustomerCode == dto.CustomerCode);

			if (!customerExists)
				return ServiceResponse<object>.Information("Customer not found", null);

			var outstanding = await GetOutstandingCreditAsync(dto.CustomerCode);

			if (outstanding <= 0)
				return ServiceResponse<object>.Information("This customer has no outstanding credit balance", new { Outstanding = outstanding });

			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				await using var tx = await _context.Database.BeginTransactionAsync();

				try
				{
					// For Cash/PDQ the credited amount is simply dto.AmountPaid.
					// For Mpesa it's overwritten below with the code's full balance —
					// never a partial amount, regardless of what dto.AmountPaid holds.
					decimal amountToCredit = dto.AmountPaid;

					if (dto.PaymentTypeCode == CreditRepaymentMethod.Mpesa)
					{
						var (mpesaResult, fullBalance) = await ValidateAndConsumeMpesaAsync(
							dto.TransactionReference!, dto.MpesaTillNumber!);

						if (mpesaResult is not null)
						{
							await tx.RollbackAsync();
							return mpesaResult;
						}

						amountToCredit = fullBalance;
					}

					if (!dto.AllowOverpayment && amountToCredit > outstanding)
					{
						await tx.RollbackAsync();
						return ServiceResponse<object>.Information($"Payment of {amountToCredit:N2} exceeds outstanding balance of {outstanding:N2}. " + $"Either collect {outstanding:N2} exactly, or pass AllowOverpayment: true to accept it as a credit surplus.",
							new
							{
								Outstanding = outstanding,
								AmountPaid = amountToCredit
							});
					}

					// Reusing the same id generator as sales for a unique repayment
					// reference — CreditTransactions.SaleId is required but this isn't
					// tied to any actual sale, so this is just a unique identifier for
					// the row.
					var repaymentRef = _setups.GenerateSaleId();

					var paymentMethodLabel = dto.PaymentTypeCode switch
					{
						CreditRepaymentMethod.Mpesa => "M-Pesa",
						CreditRepaymentMethod.PDQ => "PDQ",
						_ => "Cash"
					};

					_context.CreditTransactions.Add(new CreditTransactions
					{
						CustomerCode = dto.CustomerCode,
						Credit = amountToCredit,
						Debit = 0,
						SaleId = repaymentRef,
						TransactionReference = dto.TransactionReference ?? string.Empty,
						VehicleCode = dto.VehicleCode,
						StationCode = dto.StationCode,
						DateCreated = EatTime.Now,
						UserCode = _authentication.Usercode()
					});

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					var newBalance = outstanding - amountToCredit;

					await WriteCreditRepaymentAuditTrailAsync(
						dto.CustomerCode, dto.VehicleCode, dto.StationCode, paymentMethodLabel,
						amountToCredit, outstanding, newBalance, repaymentRef, dto.TransactionReference);

					var result = new CreditRepaymentResultDto(
						RepaymentRef: repaymentRef,
						PaymentTypeCode: dto.PaymentTypeCode,
						AmountPaid: amountToCredit,
						PreviousBalance: outstanding,
						NewBalance: newBalance,
						OverpaymentCredit: newBalance < 0 ? Math.Abs(newBalance) : 0
					);

					return ServiceResponse<object>.Success("Credit repayment recorded successfully", result);
				}
				catch
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error("An error occurred while recording the credit repayment.", null);
				}
			});
		}

		// =====================================================================
		// Mpesa validation + full-balance consumption (inline, inside the repayment transaction)
		// =====================================================================

		/// <summary>
		/// Locks the MpesaTransactions row (FOR UPDATE — prevents a concurrent sale
		/// or another repayment from double-spending the same code while this
		/// transaction is open), validates the till number, and — if there's a
		/// positive balance — consumes ALL of it: UsageBalance -> 0, Status -> 0
		/// (fully used). Never a partial amount.
		///
		/// Returns (null, fullBalance) on success — fullBalance is what the caller
		/// should credit. Returns (ServiceResponse, 0) on failure, which the caller
		/// rolls back and returns directly.
		/// </summary>
		private async Task<(ServiceResponse<object>? Error, decimal FullBalance)> ValidateAndConsumeMpesaAsync(
			string transId, string tillNumber)
		{
			var mpesaTx = await _context.MpesaTransactions
				.FromSqlInterpolated($@"
					SELECT * FROM ""MpesaTransactions""
					WHERE ""TransID"" = {transId}
					FOR UPDATE")
				.FirstOrDefaultAsync();

			if (mpesaTx is null)
				return (ServiceResponse<object>.Information($"Mpesa code {transId} does not exist", null), 0);

			var till = Regex.Replace(mpesaTx.TillNumber ?? string.Empty, @"\s+", "").Trim();

			if (!string.Equals(till, tillNumber.Trim(), StringComparison.OrdinalIgnoreCase))
				return (ServiceResponse<object>.Information("Mpesa code does not belong to that till", null), 0);

			if (mpesaTx.UsageBalance <= 0)
				return (ServiceResponse<object>.Information($"Mpesa code {transId} has already been fully used", null), 0);

			decimal fullBalance = mpesaTx.UsageBalance;

			mpesaTx.UsageBalance = 0;
			mpesaTx.Status = 0; // fully used
			mpesaTx.DateModified = EatTime.Now;

			_context.Entry(mpesaTx).State = EntityState.Modified;

			return (null, fullBalance);
		}

		// =====================================================================
		// Outstanding balance
		// =====================================================================

		public async Task<decimal> GetOutstandingCreditAsync(string customerCode)
		{
			var totals = await _context.CreditTransactions
				.Where(c => c.CustomerCode == customerCode)
				.GroupBy(c => c.CustomerCode)
				.Select(g => new
				{
					TotalDebit = g.Sum(x => x.Debit),
					TotalCredit = g.Sum(x => x.Credit)
				})
				.FirstOrDefaultAsync();

			if (totals == null)
				return 0m;

			return totals.TotalDebit - totals.TotalCredit;
		}

		private async Task WriteCreditRepaymentAuditTrailAsync(
			string customerCode, string vehicleCode, string stationCode, string paymentMethod,
			decimal amountPaid, decimal previousBalance, decimal newBalance, string repaymentRef,
			string? transactionReference)
		{
			var refPart = string.IsNullOrWhiteSpace(transactionReference) ? "" : $" | Ref={transactionReference}";

			var msg =
				$"{_authentication.Name()} recorded a CREDIT REPAYMENT ({paymentMethod}) | " +
				$"RepaymentRef={repaymentRef} | Customer={customerCode} | Vehicle={vehicleCode} | " +
				$"Station={stationCode} | AmountPaid={amountPaid:0.00} | " +
				$"PreviousBalance={previousBalance:0.00} | NewBalance={newBalance:0.00}{refPart} | " +
				$"At={EatTime.Now:yyyy/MM/dd HH:mm:ss} | User={_authentication.Usercode()}";

			await _authentication.AddUserTrail(msg, "CREDIT REPAYMENT");
		}

	}
}