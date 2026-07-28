using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.PlateRecognition;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

		// Platerecognizer Snapshot Cloud endpoint. Verify the exact path and
		// whether "ke" is a supported region code against your Platerecognizer
		// dashboard/docs — drop the "regions" field entirely if not supported,
		// recognition still works without it, just without Kenya-specific parsing.
		private const string SnapshotCloudUrl = "https://api.platerecognizer.com/v1/plate-reader/";

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
			IFormFile image, string customerCode, CancellationToken ct)
		{
			// 1. Vehicles registered to this wallet/customer account.
			// NOTE: assumes Vehicles has a CustomerCode column linking a vehicle
			// to the customer/wallet it's registered under — adjust the property
			// name below if your schema links them differently (e.g. a separate
			// CustomerVehicles join table).
			var registeredPlates = await _context.Vehicles
				.AsNoTracking()
				.Where(v => v.CustomerCode == customerCode)
				.Select(v => v.VehicleRegistrationNumber)
				.ToListAsync(ct);

			if (registeredPlates.Count == 0)
				return ServiceResponse<PlateVerificationDto>.Information(
					"No vehicles are registered under this wallet account", null);

			// 2. Call Platerecognizer Snapshot Cloud.
			var apiToken = _config["PlateRecognizer:ApiToken"]
				?? throw new InvalidOperationException("PlateRecognizer:ApiToken is not configured");

			using var client = _httpClientFactory.CreateClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiToken);

			using var form = new MultipartFormDataContent();
			await using var imageStream = image.OpenReadStream();
			using var streamContent = new StreamContent(imageStream);
			streamContent.Headers.ContentType =
				new MediaTypeHeaderValue(string.IsNullOrWhiteSpace(image.ContentType) ? "image/jpeg" : image.ContentType);

			form.Add(streamContent, "upload", image.FileName);
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

			// 3. Compare against registered plates (normalized: uppercase, no spaces/hyphens).
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

		private static string Normalize(string plate) =>
			Regex.Replace(plate, @"[\s\-]", "").ToUpperInvariant();
	}
}