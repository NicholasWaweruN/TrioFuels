using DataAccessLayer.Context;
using DataAccessLayer.FuelFlowApk;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogic.AppReleaseService 
{ 
	public interface IAppReleaseService
	{
		Task<AppRelease?> GetLatestAsync(string platform);
		Task<AppRelease?> GetByIdAsync(int id);
		Stream GetApkStream(AppRelease release);
		Task<AppRelease> UploadAsync(IFormFile apk, int versionCode, string versionName, string? notes, bool mandatory, string platform = "android");
	}
	public class AppReleaseService : IAppReleaseService
	{
		private readonly OTOContext _db;
		private readonly string _releasesPath = "/data/releases";

		public AppReleaseService(OTOContext db) => _db = db;

		public async Task<AppRelease?> GetLatestAsync(string platform)
		{
			return await _db.AppReleases
				.Where(r => r.Platform == platform)
				.OrderByDescending(r => r.VersionCode)
				.FirstOrDefaultAsync();
		}

		public async Task<AppRelease?> GetByIdAsync(int id)
		{
			return await _db.AppReleases.FindAsync(id);
		}

		public Stream GetApkStream(AppRelease release)
		{
			var path = Path.Combine(_releasesPath, release.ApkFileName);
			if (!System.IO.File.Exists(path))
				throw new FileNotFoundException($"APK file not found: {release.ApkFileName}");

			return System.IO.File.OpenRead(path);
		}

		public async Task<AppRelease> UploadAsync(
			IFormFile apk, int versionCode, string versionName, string? notes, bool mandatory, string platform = "android")
		{
			if (apk == null || apk.Length == 0)
				throw new ArgumentException("APK file is required.");

			var existing = await _db.AppReleases
				.Where(r => r.Platform == platform)
				.OrderByDescending(r => r.VersionCode)
				.FirstOrDefaultAsync();

			if (existing != null && versionCode <= existing.VersionCode)
				throw new InvalidOperationException(
					$"versionCode must be greater than current latest ({existing.VersionCode}).");

			Directory.CreateDirectory(_releasesPath);
			var fileName = $"gleamify-{versionCode}.apk";
			var fullPath = Path.Combine(_releasesPath, fileName);

			using (var fs = new FileStream(fullPath, FileMode.Create))
			{
				await apk.CopyToAsync(fs);
			}

			var release = new AppRelease
			{
				Platform = platform,
				VersionCode = versionCode,
				VersionName = versionName,
				ApkFileName = fileName,
				ReleaseNotes = notes,
				IsMandatory = mandatory
			};

			_db.AppReleases.Add(release);
			await _db.SaveChangesAsync();

			return release;
		}
	}
}
