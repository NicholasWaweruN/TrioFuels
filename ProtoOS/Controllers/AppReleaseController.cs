using BussinessLogic.AppReleaseService;
using Microsoft.AspNetCore.Mvc;

namespace FuelFlow.Controllers
{
	[ApiController]
	[Route("api/app")]
	public class AppReleaseController(IAppReleaseService appReleaseService) : ControllerBase
	{
		private readonly IAppReleaseService _appReleaseService = appReleaseService;

		[HttpGet("version/latest")]
		public async Task<IActionResult> GetLatest([FromQuery] string platform = "android")
		{
			var latest = await _appReleaseService.GetLatestAsync(platform);
			if (latest == null) return NotFound();

			var downloadUrl = $"{Request.Scheme}://{Request.Host}{Url.Action(nameof(Download), new { id = latest.Id })}";

			return Ok(new
			{
				versionCode = latest.VersionCode,
				versionName = latest.VersionName,
				downloadUrl,
				releaseNotes = latest.ReleaseNotes,
				mandatory = latest.IsMandatory
			});
		}

		[HttpGet("version/download/{id}")]
		public async Task<IActionResult> Download(int id)
		{
			var release = await _appReleaseService.GetByIdAsync(id);
			if (release == null) return NotFound();

			Stream stream;
			try
			{
				stream = _appReleaseService.GetApkStream(release);
			}
			catch (FileNotFoundException)
			{
				return NotFound("APK file missing on server.");
			}

			return File(stream, "application/vnd.android.package-archive", $"gleamify-{release.VersionName}.apk");
		}

		// Lock this down with an admin/attendant-role check or a static upload key
		[HttpPost("version/upload")]
		[RequestSizeLimit(200_000_000)]
		public async Task<IActionResult> Upload(
			IFormFile apk, int versionCode, string versionName, string? notes, bool mandatory = false, string platform = "android")
		{
			try
			{
				var release = await _appReleaseService.UploadAsync(apk, versionCode, versionName, notes, mandatory, platform);
				return Ok(new { release.Id, release.VersionCode, release.VersionName });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (InvalidOperationException ex)
			{
				return Conflict(ex.Message);
			}
		}
	}
}