using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BussinessLogic.Apks
{
	public class DownloadApk
	{
		private readonly HttpClient _httpClient;

		public DownloadApk()
		{
			_httpClient = new HttpClient();
		}

		public async Task<bool> DownloadAsync(string apkUrl, string savePath)
		{
			try
			{

				using var response = await _httpClient.GetAsync(apkUrl, HttpCompletionOption.ResponseHeadersRead);
				response.EnsureSuccessStatusCode();

				await using var stream = await response.Content.ReadAsStreamAsync();
				await using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
				await stream.CopyToAsync(fileStream);

				Console.WriteLine($"Download completed. File saved to {savePath}");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error downloading APK: {ex.Message}");
				return false;
			}
		}
	}
}
