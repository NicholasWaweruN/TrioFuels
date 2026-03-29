using AfricasTalkingCS;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Personal_Wallet
{
	public class Personal_Wallet_Authentication
	{
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly Personal_Wallet_Transactions _personal_Wallet;
		public Personal_Wallet_Authentication(ICommonSetups setups,IAuthCommonTasks authentication,OTOContext context, Personal_Wallet_Transactions personal_Wallet)
		{
			_setups = setups;
			_authentication = authentication;
			_context = context;
			_personal_Wallet = personal_Wallet;
		}


	}
}
