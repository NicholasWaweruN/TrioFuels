using DataAccessLayer.Common;
using DataAccessLayer.EntityModels.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityModels.Shifts
{
	public class ShiftSupervisorReconciliation : BaseEntity
	{
			public string ShiftNumber { get; set; } = null!;
			public decimal MpesaReceived { get; set; }
			public decimal CashReceived { get; set; }
			public decimal CreditReceived { get; set; }
			public decimal LoyaltyPointsUsed { get; set; }
			public decimal PdqReceived { get; set; }
			public decimal SystemMpesaTotal { get; set; }
			public decimal SystemCashTotal { get; set; }
			public decimal SystemCreditTotal { get; set; }
			public decimal SystemLoyaltyTotal { get; set; }
			public decimal SystemPdqTotal { get; set; }
	}
}
