using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Helpers
{
	public static class EatTime
	{
		private static readonly TimeZoneInfo _eatZone =
			TimeZoneInfo.FindSystemTimeZoneById("Africa/Nairobi");

		public static DateTime Now =>
			TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _eatZone);
	}
}
