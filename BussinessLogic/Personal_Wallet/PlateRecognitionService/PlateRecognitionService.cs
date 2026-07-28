using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.PlateRecognition;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BussinessLogic.PlateRecognitionService
{
	public class PlateRecognitionService : IPlateRecognitionService
	{
		private readonly OTOContext _context;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _config;

		// Platerecognizer Snapshot Cloud endpoint.
		private const string SnapshotCloudUrl = "https://api.platerecognizer.com/v1/plate-reader/";

		// Platerecognizer's documented hard cap is 3.5MB per upload, recommended
		// resolution ~1980x1080 landscape. We target comfortably under that
		// (3.0MB) to leave margin for JPEG re-encoding overhead.
		private const long TargetMaxBytes = 3_000_000;
		private const int MaxDimensionPx = 1920;
		private const int InitialJpegQuality = 85;
		private const int MinJpegQuality = 40;
		private const int QualityStep = 10;

		public PlateRecognitionService(
			OTOContext context,
			IHttpClientFactory httpClientFactory,
			IConfiguration config)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
			_config = config;
		}

		public async Task<ServiceResponse<PlateVerificationDto>> VerifyWalletVehicleAsync(
			string base64Image, string customerCode, CancellationToken ct)
		{
			// 1. Vehicles registered to this wallet/customer account.
			var registeredPlates = await _context.Vehicles
				.AsNoTracking()
				.Where(v => v.CustomerCode == customerCode)
				.Select(v => v.VehicleRegistrationNumber)
				.ToListAsync(ct);

			if (registeredPlates.Count == 0)
				return ServiceResponse<PlateVerificationDto>.Information(
					"No vehicles are registered under this wallet account", null);

			if (string.IsNullOrWhiteSpace(base64Image))
				return ServiceResponse<PlateVerificationDto>.Error("No image data was provided", null);

			// 2. Decode the base64 payload. Handles both a raw base64 string and
			// a data URI like "data:image/jpeg;base64,/9j/4AAQ...".
			var payload = base64Image;
			var commaIndex = base64Image.IndexOf(',');
			if (base64Image.StartsWith("data:", StringComparison.OrdinalIgnoreCase) && commaIndex >= 0)
				payload = base64Image[(commaIndex + 1)..];

			byte[] rawImageBytes;
			try
			{
				rawImageBytes = Convert.FromBase64String(payload);
			}
			catch (FormatException)
			{
				return ServiceResponse<PlateVerificationDto>.Error("Invalid base64 image data", null);
			}

			if (rawImageBytes.Length == 0)
				return ServiceResponse<PlateVerificationDto>.Error("Decoded image data was empty", null);

			// 3. Downscale/recompress before sending to Platerecognizer. This
			// runs regardless of the incoming size — normalizing everything to
			// JPEG also sidesteps any mismatched mimeType issues from the client.
			byte[] imageBytes;
			try
			{
				imageBytes = await DownscaleAndCompressAsync(rawImageBytes, ct);
			}
			catch (Exception ex) when (ex is SixLabors.ImageSharp.UnknownImageFormatException or SixLabors.ImageSharp.InvalidImageContentException)
			{
				return ServiceResponse<PlateVerificationDto>.Error(
					"The uploaded file isn't a readable image. Please retake the photo.", null);
			}

			if (imageBytes.Length > TargetMaxBytes)
			{
				// Even at minimum quality it's still too big — fail fast instead
				// of burning a Platerecognizer lookup credit on a guaranteed 413.
				return ServiceResponse<PlateVerificationDto>.Error(
					"Photo is too large even after compression. Please retake in landscape, closer to the plate.", null);
			}

			// 4. Call Platerecognizer Snapshot Cloud.
			var apiToken = _config["PlateRecognizer:ApiToken"]
				?? throw new InvalidOperationException("PlateRecognizer:ApiToken is not configured");

			using var client = _httpClientFactory.CreateClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiToken);

			using var form = new MultipartFormDataContent();
			using var byteContent = new ByteArrayContent(imageBytes);
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

			form.Add(byteContent, "upload", "plate.jpg");
			form.Add(new StringContent("ke"), "regions");

			using var response = await client.PostAsync(SnapshotCloudUrl, form, ct);
			var raw = await response.Content.ReadAsStringAsync(ct);

			if (!response.IsSuccessStatusCode)
				return ServiceResponse<PlateVerificationDto>.Error(
					$"Plate recognition service error ({(int)response.StatusCode})", null);

			using var doc = JsonDocument.Parse(raw);
			if (!doc.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
				return ServiceResponse<PlateVerificationDto>.Information(
					"No plate detected in the photo. Please retake and try again.", null);

			var best = results.EnumerateArray()
				.OrderByDescending(r => r.GetProperty("score").GetDouble())
				.First();

			var recognizedPlate = Normalize(best.GetProperty("plate").GetString() ?? "");
			var confidence = best.GetProperty("score").GetDouble();

			var candidates = results.EnumerateArray()
				.Select(r => Normalize(r.GetProperty("plate").GetString() ?? ""))
				.ToList();

			// 5. Compare against registered plates (normalized: uppercase, no spaces/hyphens).
			var matchedPlate = registeredPlates.FirstOrDefault(p => Normalize(p) == recognizedPlate);

			var dto = new PlateVerificationDto(
				Matched: matchedPlate is not null,
				RecognizedPlate: recognizedPlate,
				MatchedVehicleRegistration: matchedPlate,
				Confidence: confidence,
				CandidatePlates: candidates
			);

			return matchedPlate is not null
				? ServiceResponse<PlateVerificationDto>.Success("Plate verified", dto)
				: ServiceResponse<PlateVerificationDto>.Information(
					$"Plate {recognizedPlate} does not match any vehicle registered to this wallet account", dto);
		}

		/// <summary>
		/// Resizes to a max dimension of MaxDimensionPx (preserving aspect ratio)
		/// and re-encodes as JPEG, stepping quality down until under TargetMaxBytes
		/// or MinJpegQuality is hit — whichever comes first.
		/// </summary>
		private static async Task<byte[]> DownscaleAndCompressAsync(byte[] originalBytes, CancellationToken ct)
		{
			using var image = Image.Load(originalBytes);

			if (image.Width > MaxDimensionPx || image.Height > MaxDimensionPx)
			{
				image.Mutate(x => x.Resize(new ResizeOptions
				{
					Mode = ResizeMode.Max,
					Size = new Size(MaxDimensionPx, MaxDimensionPx)
				}));
			}

			var quality = InitialJpegQuality;
			byte[] encoded;

			while (true)
			{
				using var ms = new MemoryStream();
				await image.SaveAsync(ms, new JpegEncoder { Quality = quality }, ct);
				encoded = ms.ToArray();

				if (encoded.Length <= TargetMaxBytes || quality <= MinJpegQuality)
					break;

				quality -= QualityStep;
			}

			return encoded;
		}

		private static string Normalize(string plate) =>
			Regex.Replace(plate, @"[\s\-]", "").ToUpperInvariant();
	}
}