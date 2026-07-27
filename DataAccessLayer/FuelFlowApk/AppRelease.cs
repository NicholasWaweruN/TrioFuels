using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.FuelFlowApk
{
	public class AppRelease
	{
		public int Id { get; set; }
		public string Platform { get; set; } = "android"; // future-proof for iOS
		public int VersionCode { get; set; }       // matches Android versionCode
		public string VersionName { get; set; } = default!; // e.g. "1.4.2"
		public string ApkFileName { get; set; } = default!;  // stored file name
		public string? ReleaseNotes { get; set; }
		public bool IsMandatory { get; set; }
		public DateTime ReleasedAt { get; set; } = DateTime.UtcNow;
	}
}
