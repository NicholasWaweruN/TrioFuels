using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Safaricom_Daraja.Helpers
{
	public class ShiftResolver : IShiftResolver
	{
		private readonly OTOContext _context;

		public ShiftResolver(OTOContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<string>> GetCurrentShiftByTill(string tillNumber)
		{
			var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.TillNumber == tillNumber);

			if (dispenser == null)
				return ServiceResponse<string>.Information("Till number does not exist", null);

			var shift = await _context.Shifts.Where(x => x.DispenserCode == dispenser.DispenserCode && x.ShiftStatus == ShiftStatus.Open)
				.OrderByDescending(x => x.DateCreated)
				.FirstOrDefaultAsync();

			if (shift == null)
				return ServiceResponse<string>.Information("No open shift found for this till", null);

			return ServiceResponse<string>.Success("", shift.ShiftNumber);
		}
	}
}