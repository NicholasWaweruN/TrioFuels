using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BusinessLogic.Sales.MissingSales
{
	public class MissingSales(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, ICommonSalesTasks salesTasks) : IMissingSales
	{
		private readonly OTOContext _context = context;
		private readonly IAuthCommonTasks _authentication = authentication;
		private readonly ICommonSetups _setups = setups;
		private readonly ICommonSalesTasks _salesTasks = salesTasks;

		public async Task<ServiceResponse> DeferVariance(string shiftNumber)
		{
			var shift = await (from s in _context.Shifts
							   where s.ShiftNumber.Equals(shiftNumber)
							   select s).FirstOrDefaultAsync();

			var stocksummary = await (from s in _context.StockTakeSummaries
									  where s.ShiftNumber.Equals(shiftNumber)
									  select s).ToListAsync();
			if (shift is not null && stocksummary is not null)
			{
				shift.ShiftStatus = ShiftStatus.Pending;
				_context.Update(shift);

				foreach (var s in stocksummary)
				{
					s.VarianceStatus = ShiftStatus.Pending;
					_context.Update(s);
				}
				await _context.SaveChangesAsync();

				var message = $"Variance of shift {shift.ShiftNumber} has been defered untill next shift by {_authentication.Name()} on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Variance has been defered untill next shift");
			}

			return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
		}
		public async Task<ServiceResponse> OffWriteVariance(string shiftNumber)
		{
			var shift = await (from s in _context.Shifts
							   where s.ShiftNumber.Equals(shiftNumber)
							   select s).FirstOrDefaultAsync();

			var stocksummary = await (from s in _context.StockTakeSummaries
									  where s.ShiftNumber.Equals(shiftNumber)
									  select s).ToListAsync();

			if (shift is not null && stocksummary is not null)
			{
				shift.ShiftStatus = ShiftStatus.Closed;
				_context.Update(shift);

				foreach (var s in stocksummary)
				{
					s.VarianceStatus = ShiftStatus.Closed;
					_context.Update(s);
				}
				await _context.SaveChangesAsync();

				var message = $"Variance of amount {stocksummary.Sum(x => x.ClosingVariance)} written off by {_authentication.Name()} on {DateTime.UtcNow}";
				return ServiceResponse<object>.Success("Variance has been offwritten");
			}

			return ServiceResponse<object>.Information("Shift or Stock Summary Not Found");
		}

	}
}