using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.Credit_Management
{
	public class CreditManagement
	{

		private readonly OTOContext _context;
		public CreditManagement(OTOContext context) 
		{
			_context = context;
		}

		public async Task<ServiceResponse<object>> CheckifIsAcreditCustomer(string customerCode)
		{

			var customer = await _context.Customers.Where(x => x.CustomerCode.Equals(customerCode)
								&& x.IsCreditCustomer)
							.FirstOrDefaultAsync();
			if (customer == null) 
			{
				return ServiceResponse<object>.Information("is not a credit customer",null);
			}

			return  ServiceResponse<object>.Success("is a credit customer", customer);
		}


	}


}
