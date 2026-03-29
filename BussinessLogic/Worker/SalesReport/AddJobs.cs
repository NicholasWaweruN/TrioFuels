using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;

namespace BusinessLogic.Worker.SalesReport
{
	public class AddJobs
	{
		private readonly OTOContext _context;
		public AddJobs(OTOContext context)
		{
			_context = context;
		}
		public async Task AddJob(string jobCode, DateTime date)
		{
			var jobs = new OtogasJobs
			{
				HasRun = false,
				JobCode = jobCode,
				JobName = "Sale Report Job",
				JobStatus = 0,
				LastRun = date.Date,
			};
			await _context.AddAsync(jobs);
			await _context.SaveChangesAsync();
		}
	}

}
