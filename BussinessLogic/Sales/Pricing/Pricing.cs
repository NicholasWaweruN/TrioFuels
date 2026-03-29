using BussinessLogic.Authentication.CommonTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.Pricing
{
	public class Pricing 
	{
		IAuthCommonTasks _authentication;
		public Pricing(IAuthCommonTasks authentication) 
		{
			_authentication = authentication;
		}
	}
}
