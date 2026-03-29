using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.StockTake;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Stock.Totalizers
{
	public class ReadingsTotalizers
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly IEmailService _emailService;

		public ReadingsTotalizers(OTOContext context, IAuthCommonTasks authentication, IEmailService emailService)
		{
			_context = context;
			_authentication = authentication;
			_emailService = emailService;
		}

		// TAKE TOTALIZER READINGS
		public async Task<ServiceResponse<object>> RecordTotalizerReadingsAsync(List<NozzleReadingInput> nozzles)
		{
			if (nozzles == null || !nozzles.Any())
				return ServiceResponse<object>.Information("Provide nozzle readings", null);

			var strategy = _context.Database.CreateExecutionStrategy();

			return await strategy.ExecuteAsync(async () =>
			{
				using var tx = await _context.Database.BeginTransactionAsync();

				try
				{
					var nozzleCodes = nozzles.Select(n => n.NozzleCode).ToList();

					var existingNozzles = await _context.Nozzles
						.Where(n => nozzleCodes.Contains(n.NozzleCode))
						.Select(n => n.NozzleCode)
						.ToListAsync();

					var invalid = nozzleCodes.Except(existingNozzles).ToList();
					if (invalid.Any())
						return ServiceResponse<object>.Information($"Invalid nozzles: {string.Join(",", invalid)}", null);

					var today = DateTime.UtcNow.Date;

					var lastReadings = await _context.TotalizerReadings
						.Where(r => r.DateCreated < today && existingNozzles.Contains(r.NozzlesCode))
						.GroupBy(r => r.NozzlesCode)
						.Select(g => new
						{
							Code = g.Key,
							Reading = g.OrderByDescending(x => x.DateCreated)
									   .Select(x => x.Reading)
									   .FirstOrDefault()
						})
						.ToDictionaryAsync(x => x.Code, x => x.Reading);

					var userCode = _authentication.Usercode();

					var records = nozzles.Select(n => new TotalizerReadings
					{
						DateCreated = DateTime.UtcNow,
						NozzlesCode = n.NozzleCode,
						Reading = n.Reading,
						UserCode = userCode
					});

					await _context.TotalizerReadings.AddRangeAsync(records);

					var dispenser = await GetDispenserAssignedToUserAsync();

					var scheduler = await _context.StockTakeScheduler
						.FirstOrDefaultAsync(d =>
							d.DispenserCode == dispenser &&
							d.TakeDateTime >= today &&
							d.TakeDateTime < today.AddDays(1));

					if (scheduler != null)
						scheduler.IsTaken = true;

					await _context.SaveChangesAsync();
					await tx.CommitAsync();

					return ServiceResponse<object>.Success("Totalizer reading recorded successfully.", null);
				}
				catch (Exception ex)
				{
					await tx.RollbackAsync();
					return ServiceResponse<object>.Error("Failed saving readings", ex.Message);
				}
			});
		}
		private async Task<string> GetDispenserAssignedToUserAsync()
		{
			return await _context.DispenserAssignments
				.Where(a => a.AttedantUserCode == _authentication.Usercode())
				.Select(a => a.DispenserCode)
				.FirstOrDefaultAsync() ?? string.Empty;
		}

		public class NozzleReadingInput
		{
			public string NozzleCode { get; set; } = string.Empty;
			public decimal Reading { get; set; }
		}
	}
}
