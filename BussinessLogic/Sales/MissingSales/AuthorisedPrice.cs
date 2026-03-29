using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.MissingSales
{
	public class AuthorisedPrice
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		public AuthorisedPrice(OTOContext context,IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}

		//
	}
}
