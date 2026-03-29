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

	public class UpadteJobs
	{
		private readonly OTOContext _context;
		public UpadteJobs(OTOContext context)
		{
			_context = context;
		}
		public async Task UpdateStatus(string jobCode, DateTime date, bool hasrun)
		{
			var jobs = await (from j in _context.OtogasJobs
							  where j.JobCode == jobCode
							  && j.LastRun.Date == date.Date
							  select j).FirstOrDefaultAsync();
			if (jobs is not null)
			{
				jobs.HasRun = true;
				jobs.JobStatus = 1;
				_context.Update(jobs);
				await _context.SaveChangesAsync();
			}
		}

	}
}
