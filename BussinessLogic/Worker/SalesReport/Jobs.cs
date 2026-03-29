using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Worker.SalesReport
{
	public class Jobs
	{
		private readonly OTOContext _context;
		public Jobs(OTOContext context)
		{
			_context = context;
		}
		public async Task<bool> HasServiceRun(string jobCode, DateTime date)
		{
			var hasrun = await (from j in _context.OtogasJobs
								where j.JobCode == jobCode
								&& j.LastRun.Date == date.Date
								select j.HasRun).FirstOrDefaultAsync();
			return hasrun;
		}
	}


}
