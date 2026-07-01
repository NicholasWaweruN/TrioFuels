using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.SalesData
{
	public class SalesByPaymentMethod : ISalesByPaymentMethod
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;

		public SalesByPaymentMethod(OTOContext context, IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}

		public async Task<ServiceResponse<List<SalesByPaymentMethodDto>>> GetSalesByPaymentMethodAsync()
		{
			var shift = await _context.Shifts
				.Where(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == 1)
				.FirstOrDefaultAsync();

			if (shift is null)
				return ServiceResponse<List<SalesByPaymentMethodDto>>.Information("No open shift found", null);

			var query =
				from qt in _context.QuantityTransactions
				where qt.ShiftNumber == shift.ShiftNumber          // ← column name, TBC
				join pm in _context.PaymentTypes
					on qt.PaymentTypeCode equals pm.PaymentTypeId
				group qt by new { pm.PaymentTypeId, pm.PaymentTypeName } into g
				select new SalesByPaymentMethodDto
				{
					PaymentName = g.Key.PaymentTypeName,
					QuantitySold = g.Sum(x => x.QuantityCredit),
					Amount = g.Sum(x => x.AmountCredit)
				};

			var results = await query.ToListAsync();

			if (results.Count == 0)
				return ServiceResponse<List<SalesByPaymentMethodDto>>.Information("No sales found for current shift", null);

			var totals = new SalesByPaymentMethodDto
			{
				PaymentName = "Total",
				QuantitySold = results.Sum(r => r.QuantitySold),
				Amount = results.Sum(r => r.Amount)
			};

			results.Add(totals);

			return ServiceResponse<List<SalesByPaymentMethodDto>>.Success("Sales by payment method retrieved", results);
		}

		public class SalesByPaymentMethodDto
		{
			public string PaymentName { get; set; } = string.Empty;
			public decimal QuantitySold { get; set; }
			public decimal Amount { get; set; }
		}

		public async Task<ServiceResponse<List<SalesPerNozzleDto>>> GetSalesPerNozzleAsync()
		{
			var shift = await _context.Shifts
				.Where(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == 1)
				.FirstOrDefaultAsync();

			if (shift is null)
				return ServiceResponse<List<SalesPerNozzleDto>>.Information("No open shift found", null);

			var query =
				from qt in _context.QuantityTransactions
				where qt.ShiftNumber == shift.ShiftNumber                    // ← column name, TBC
				join nz in _context.Nozzles
					on qt.NozzleCode equals nz.NozzleCode                    // ← FK/PK column names, TBC
				join pp in _context.PetroleumProducts
					on nz.PetroleumCode equals pp.PetroleumCode                  // ← FK/PK column names, TBC
				group new { qt, nz, pp } by new
				{
					nz.NozzleCode,
					nz.NozzleName,                                           // ← column name, TBC
					pp.PetroleumCode, 
					pp.PetroleumName                                           // ← column name, TBC
				} into g
				select new SalesPerNozzleDto
				{
					NozzleName = g.Key.NozzleName,
					ProductCode = g.Key.PetroleumCode,
					ProductName = g.Key.PetroleumName,
					QuantitySold = g.Sum(x => x.qt.QuantityCredit),          // ← column name, TBC
					Amount = g.Sum(x => x.qt.AmountCredit)                   // ← column name, TBC
				};

			var results = await query.ToListAsync();

			if (results.Count == 0)
				return ServiceResponse<List<SalesPerNozzleDto>>.Information("No sales found for current shift", null);

			var totals = new SalesPerNozzleDto
			{
				NozzleName = "Total",
				ProductCode = "",
				ProductName = "",
				QuantitySold = results.Sum(r => r.QuantitySold),
				Amount = results.Sum(r => r.Amount)
			};

			results.Add(totals);

			return ServiceResponse<List<SalesPerNozzleDto>>.Success("Sales per nozzle retrieved", results);
		}
		public class SalesPerNozzleDto
		{
			public string NozzleName { get; set; } = string.Empty;
			public string ProductCode { get; set; } = string.Empty;
			public string ProductName { get; set; } = string.Empty;
			public decimal QuantitySold { get; set; }
			public decimal Amount { get; set; }
		}
	}
}