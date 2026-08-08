using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Stock.VarianceReport;
using BussinessLogic.Worker.SalesReport;
using DataAccessLayer.Context;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLogic.Worker
{
	internal static class EmailBackgroundServiceHelpers
	{
		private static async Task VarianceReports(IServiceScope scope, DateTime CurrentTime)
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<OTOContext>();
			var varianceReport = scope.ServiceProvider.GetRequiredService<VarianceReport>();
			var getEmailRecipients = scope.ServiceProvider.GetRequiredService<IWorkerRecipients>();
			var shiftsales = scope.ServiceProvider.GetRequiredService<ShiftsSales>();

			var varianceShifts = await dbContext.Shifts
				.Where(s => s.ShiftStatus == 2 && !s.IsEmailSent)
				.ToListAsync();

			var twelveHoursAgo = EatTime.Now.AddHours(-14);

			var varianceShift = await dbContext.Shifts
				.Where(s => s.ShiftStartTime >= twelveHoursAgo && s.ShiftStatus != 1)
				.ToListAsync();


			foreach (var variance in varianceShifts)
			{
				var result = await varianceReport.GetVarianceReport(variance.ShiftNumber);
				if (result.ResponseCode == 1)
				{
					variance.IsEmailSent = true;
					dbContext.Update(variance);
					await dbContext.SaveChangesAsync();
				}


			}

		}
	}
}