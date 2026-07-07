using DataAccessLayer.EntityModels.CreditTransactions;

namespace DataAccessLayer.DTOs.Credit
{
	/// <summary>
	/// Payment methods accepted for a credit repayment. Deliberately its own enum,
	/// separate from Sales' PaymetMethod — the numeric values here are fixed by
	/// convention for this flow and shouldn't shift if PaymetMethod's own values
	/// ever change.
	/// </summary>
	public enum CreditRepaymentMethod
	{
		Mpesa = 0,
		Cash = 1,
		PDQ = 2 // rare
	}

	/// <summary>
	/// Request to record a credit repayment. PaymentTypeCode drives which branch
	/// RepayCreditAsync takes — see CreditRepaymentMethod for valid values.
	///
	/// For Cash/PDQ: AmountPaid is REQUIRED — it's exactly what gets credited.
	///
	/// For Mpesa: AmountPaid is IGNORED. Mpesa codes are never partially used here —
	/// whatever the FULL remaining UsageBalance on the code is gets credited in one
	/// go, and the code is marked fully used (UsageBalance -> 0, Status -> 0).
	/// TransactionReference (the M-Pesa code) and MpesaTillNumber are REQUIRED.
	///
	/// AllowOverpayment defaults to true — repayments are accepted even if they push
	/// the customer's balance negative (a credit surplus); the response's
	/// OverpaymentCredit field reports the excess so it can be handled downstream.
	/// </summary>
	public record CreditRepaymentDto(
		string CustomerCode,
		string VehicleCode,
		string StationCode,
		CreditRepaymentMethod PaymentTypeCode,
		decimal AmountPaid,
		string? TransactionReference = null,
		string? MpesaTillNumber = null,
		bool AllowOverpayment = true
	);

	public record CreditRepaymentResultDto(
		string RepaymentRef,
		CreditRepaymentMethod PaymentTypeCode,
		decimal AmountPaid,
		decimal PreviousBalance,
		decimal NewBalance,
		decimal OverpaymentCredit
	);
}