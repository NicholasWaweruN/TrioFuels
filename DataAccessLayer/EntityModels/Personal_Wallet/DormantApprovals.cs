using DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Personal_Wallet
{
	public class DormantApprovals : BaseEntity
	{
		public class DormantWalletApproval : BaseEntity
		{
			public bool ApproverCode { get; set; } 
		}
	}
}
