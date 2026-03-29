using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Worker.PriceScheduler
{
	public class AddSchedule
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		public AddSchedule(OTOContext context, IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}

		//public async Task<ServiceResponse<object>> SchedulePriceChangeAsync(PriceChangeScheduleRequest request)
		//{
		//	var schedules = new List<PriceSchedule>();

		//	foreach (var dto in request.PriceSchedules)
		//	{
		//		var stationCodes = dto.StationCodes
		//			.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		//		foreach (var stationCode in stationCodes)
		//		{
		//			// Optional: Fetch current/original price
		//			var originalPrice = await _context.Prices
		//				.Where(p => p.StationCode == stationCode && p.ProductCode == dto.Product)
		//				.Select(p => p.Amount)
		//				.FirstOrDefaultAsync();

		//			var schedule = new PriceSchedule
		//			{
		//				ProductCode = dto.Product,
		//				StationCode = stationCode,
		//				ScheduledPrice = dto.NewPrice,
		//				StartTime = dto.StartTime,
		//				EndTime = dto.EndTime,
		//				OriginalPrice = originalPrice, // fallback to 0 if not found
		//				IsActive = false,
		//				Processed = false
		//			};

		//			schedules.Add(schedule);
		//		}
		//	}

		//	await _context.PriceSchedules.AddRangeAsync(schedules);
		//	await _context.SaveChangesAsync();

		//	return  ServiceResponse<object>.Success("Price change schedules added successfully.", new
		//	{
		//		schedules = schedules.Select(s => new
		//		{
		//			s.Id,
		//			s.ProductCode,
		//			s.StationCode,
		//			s.ScheduledPrice,
		//			s.StartTime,
		//			s.EndTime,
		//			s.OriginalPrice,
		//			s.IsActive,
		//			s.Processed
		//		})
		//	});

		//}

	}


	public class PriceChangeSchedule
	{
		[Required]
		public string StationCodes { get; set; } = string.Empty;
		[Required]
		public string Product { get; set; } = string.Empty;
		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "New price must be greater than zero.")]
		public decimal NewPrice { get; set; }
		[Required]
		public DateTime StartTime { get; set; }
		[Required]
		public DateTime EndTime { get; set; }
	}

}
