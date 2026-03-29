using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sales.Target
{
	public class Target 
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _auth;
		public Target(OTOContext context,IAuthCommonTasks auth)
		{
			_context = context;
			_auth = auth;
		}
		//	Add new target for the whole year distribute monthly
		public async Task<ServiceResponse> AddTargetAsync(decimal target, int year,string stationCode)
		{
			try
			{


				//check if there is target for that year
				var targetEntity = await _context.Targets.FirstOrDefaultAsync(x => x.Year == year && x.StationCode == stationCode);
				if (targetEntity != null)
				{
					return ServiceResponse<object>.Information("Target already set for this year", null);
				}
				var targets = new List<Targets>();
				for (int i = 1; i <= 12; i++)
				{
					targets.Add(new Targets
					{
						UserCode = _auth.Usercode(),
						TargetAmount = target / 12,
						Month = i,
						Year = year
					});
				}
				var message = $@"{year} target added by {_auth.Name()} on {DateTime.UtcNow}";
				await _auth.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
				await _context.Targets.AddRangeAsync(targets);
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success($"{year} Target added successfully", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occured", null);
			}
		}

		//Modify for a specific month
		public async Task ModifyTargetAsync(decimal target, int year, int month, string stationCode)
		{
			var targetEntity = await _context.Targets.FirstOrDefaultAsync(x => x.Month == month && x.Year == year && x.StationCode == stationCode);
			if (targetEntity != null)
			{
				targetEntity.TargetAmount = target;
				await _context.SaveChangesAsync();
			}
		}

		//list target for specific year join with station
		public async Task<ServiceResponse<object>> ListTargetAsync(int year)
		{
			var targets = await (from t in _context.Targets
								 join s in _context.Stations on t.StationCode equals s.StationCode
								 select new 
								 {
									 s.StationName,
									 t.TargetAmount,
									 t.TotalSales,
									 t.Month,
									 t.Year
								 }).ToListAsync();

			if(targets.Count == 0)
			{
				return ServiceResponse<object>.Information("No target set for this year", null);
			}
			return ServiceResponse<object>.Success("Targets retrieved successfully", targets);
		}

		// get the list of sales in QuantityTransactions perstation for a specific month join with targets 
	

	}
}
