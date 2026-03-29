using Azure;
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using BussinessLogic.Customers.Vehicles;

namespace BussinessLogic.PlateDetection
{
	public interface IPlateRecognitionService
	{
		Task<ServiceResponse<object>> DetectPlateAsync(IFormFile image);
	}

	public class PlateRecognitionService : IPlateRecognitionService
	{
		private readonly string _apiUrl;
		private readonly string _token;
		private readonly RestClient _client;
		private readonly ILogger<PlateRecognitionService> _logger;
		private readonly IAuthCommonTasks _authentication;
		private readonly OtogasVehicles _vehicles;

		public PlateRecognitionService(IConfiguration config, ILogger<PlateRecognitionService> logger,
			IAuthCommonTasks authentication, OtogasVehicles vehicles)
		{
			_apiUrl = config["PlateRecognizer:ApiUrl"] ?? throw new ArgumentNullException(nameof(config), "API URL is not configured.");
			_token = config["PlateRecognizer:Token"] ?? throw new ArgumentNullException(nameof(config), "API token is not configured.");
			_client = new RestClient();
			_logger = logger;
			_authentication = authentication;
			_vehicles = vehicles;
		}

		public async Task<ServiceResponse<object>> DetectPlateAsync(IFormFile image)
		{
			if (image == null || image.Length == 0)
				return ServiceResponse<object>.Information("No image uploaded.", null);

			var tempFilePath = Path.GetTempFileName();

			try
			{
				// Save the uploaded image to a temporary file
				await using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				// Send to external plate recognition API
				var response = await UploadImageToRecognitionApiAsync(tempFilePath, image.ContentType);
				if (!response.IsSuccessful)
				{
					_logger.LogWarning("Plate recognition API error: {Message}", response.ErrorMessage);
					return ServiceResponse<object>.Error("Plate recognition API error.", null);
				}

				if (string.IsNullOrWhiteSpace(response.Content))
					return ServiceResponse<object>.Error("Empty response from Plate Recognition API.", null);

				// Parse and extract plate
				var parsedResponse = JsonConvert.DeserializeObject<PlateRecognitionResponse>(response.Content);
				var plateData = parsedResponse?.Results?.FirstOrDefault();

				if (string.IsNullOrWhiteSpace(plateData?.Plate))
					return ServiceResponse<object>.Error("No plate detected.", null);

				var plate = plateData.Plate.ToUpperInvariant();

				// Search vehicle using the plate number
				return await _vehicles.SearchVehicle(plate);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Plate recognition failed.");
				return ServiceResponse<object>.Error("Internal server error occurred.", null);
			}
			finally
			{
				try
				{
					if (File.Exists(tempFilePath))
						File.Delete(tempFilePath);
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Failed to delete temp file: {Path}", tempFilePath);
				}
			}
		}

		private async Task<RestResponse> UploadImageToRecognitionApiAsync(string filePath, string contentType)
		{
			var request = new RestRequest(_apiUrl, Method.Post);
			request.AddHeader("Authorization", _token);
			request.AlwaysMultipartFormData = true;
			request.AddFile("upload", filePath, contentType);

			return await _client.ExecuteAsync(request);
		}
	}

	// Response Models
	public class PlateRecognitionResult
	{
		public string PlateNumber { get; set; } = string.Empty;
		public double Confidence { get; set; }
		public string? Region { get; set; }
		public string? VehicleType { get; set; }
	}

	public class PlateRecognitionResponse
	{
		public List<PlateResult> Results { get; set; } = new();
	}

	public class PlateResult
	{
		public string Plate { get; set; } = string.Empty;
		public double Confidence { get; set; }
		public Region Region { get; set; } = new();
		public Car Vehicle { get; set; } = new();
	}

	public class Region
	{
		public string Code { get; set; } = string.Empty;
		public double Score { get; set; }
	}

	public class Car
	{
		public string Type { get; set; } = string.Empty;
	}

}
