using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BussinessLogic.Analytics
{ 
	public class StockLevelReport
	{
		public string? ContainerName { get; set; } = string.Empty;
		public string? ProductTypeName { get; set; } = string.Empty;
		public string? StatesKey { get; set; } = string.Empty;
		public int? TotalStockLevel { get; set; }
		public int? AvailableStockLevel { get; set; }
		public int? ReservedStockLevel { get; set; }
		public int? InPositiveVarianceStockLevel { get; set; }
		public int? InNegativeVarianceStockLevel { get; set; }
		public int? OpeningStockAmount { get; set; }
		public DateTime? OpeningStockCreatedAt { get; set; }
		public int? ClosingStockAmount { get; set; }
		public DateTime? ClosingStockCreatedAt { get; set; }
	}

	public class DataSets
	{
		public DataSets()
		{
		}

		// Method to call the stock level report API and convert the response to GeoJSON
		public static async Task<string> GetStockLevelReportAsGeoJsonAsync()
		{
			var url = "https://data.circl.services/api/dataset";

			// Define the payload
			var payload = new
			{
				database = 67,
				type = "native",
				native = new
				{
					query = "SELECT * FROM reports.stock_level_report WHERE CAST(opening_stock_created_at AS DATE) = CURRENT_DATE - INTERVAL '1 DAY' LIMIT 10"
				}
			};

			// Serialize payload to JSON
			var jsonPayload = JsonConvert.SerializeObject(payload);

			// Create HttpClient instance
			using (HttpClient client = new())
			{
				// Add necessary headers
				client.DefaultRequestHeaders.Add("Authorization", "Bearer 1b574196-4a86-443b-a0e2-73cada195c41");
				client.DefaultRequestHeaders.Add("Cookie", "metabase.DEVICE=08ad3c0c-664f-479f-a844-97ae2a5d7ac2; metabase.SESSION=1b574196-4a86-443b-a0e2-73cada195c41; metabase.TIMEOUT=alive");

				// Set content with JSON
				var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

				// Make the POST request
				HttpResponseMessage response = await client.PostAsync(url, content);

				// Check for success
				if (response.IsSuccessStatusCode)
				{
					// Get the response content as a string
					var jsonResponse = await response.Content.ReadAsStringAsync();

					JObject? data = JObject.Parse(jsonResponse);

					// Ensure 'rows' field exists and check if it's empty
					JArray? reports = data["data"]?["rows"] as JArray;
					if (reports == null || !reports.HasValues)
					{
						return "No data available for the current date.";
					}

					// Start building GeoJSON
					StringBuilder geoJson = new StringBuilder();
					geoJson.AppendLine("{");
					geoJson.AppendLine("\"type\": \"FeatureCollection\",");
					geoJson.AppendLine("\"features\": [");

					// Loop through each report and create StockLevelReport objects
					bool firstFeature = true;
					foreach (var reportItem in reports)
					{
						if (reportItem is JObject report)
						{
							var stockReport = new StockLevelReport
							{
								ContainerName = report["container_name"]?.ToString(),
								ProductTypeName = report["product_type_name"]?.ToString(),
								TotalStockLevel = report["total_stock_level"]?.ToObject<int?>(),
								AvailableStockLevel = report["available_stock_level"]?.ToObject<int?>(),
								ReservedStockLevel = report["reserved_stock_level"]?.ToObject<int?>(),
								InPositiveVarianceStockLevel = report["in_positive_variance_stock_level"]?.ToObject<int?>(),
								InNegativeVarianceStockLevel = report["in_negative_variance_stock_level"]?.ToObject<int?>(),
								OpeningStockAmount = report["opening_stock_amount"]?.ToObject<int?>(),
								OpeningStockCreatedAt = report["opening_stock_created_at"]?.ToObject<DateTime?>(),
								ClosingStockAmount = report["closing_stock_amount"]?.ToObject<int?>(),
								ClosingStockCreatedAt = report["closing_stock_created_at"]?.ToObject<DateTime?>()
							};

							// Create a GeoJSON Feature for each stock level report
							if (!firstFeature)
							{
								geoJson.AppendLine(",");
							}
							firstFeature = false;

							geoJson.AppendLine("{");
							geoJson.AppendLine("\"type\": \"Feature\",");
							geoJson.AppendLine("\"geometry\": { \"type\": \"Point\", \"coordinates\": [0, 0] },"); // Placeholder for lat/lon
							geoJson.AppendLine("\"properties\": {");
							geoJson.AppendLine($"\"ContainerName\": \"{stockReport.ContainerName}\",");
							geoJson.AppendLine($"\"ProductTypeName\": \"{stockReport.ProductTypeName}\",");
							geoJson.AppendLine($"\"TotalStockLevel\": {stockReport.TotalStockLevel},");
							geoJson.AppendLine($"\"AvailableStockLevel\": {stockReport.AvailableStockLevel},");
							geoJson.AppendLine($"\"ReservedStockLevel\": {stockReport.ReservedStockLevel},");
							geoJson.AppendLine($"\"InPositiveVarianceStockLevel\": {stockReport.InPositiveVarianceStockLevel},");
							geoJson.AppendLine($"\"InNegativeVarianceStockLevel\": {stockReport.InNegativeVarianceStockLevel},");
							geoJson.AppendLine($"\"OpeningStockAmount\": {stockReport.OpeningStockAmount},");
							geoJson.AppendLine($"\"OpeningStockCreatedAt\": \"{stockReport.OpeningStockCreatedAt?.ToString("o")}\",");
							geoJson.AppendLine($"\"ClosingStockAmount\": {stockReport.ClosingStockAmount},");
							geoJson.AppendLine($"\"ClosingStockCreatedAt\": \"{stockReport.ClosingStockCreatedAt?.ToString("o")}\"");
							geoJson.AppendLine("}");
							geoJson.AppendLine("}");
						}
					}

					geoJson.AppendLine("]");
					geoJson.AppendLine("}");

					// Return the GeoJSON string
					return geoJson.ToString();
				}
				else
				{
					// Return error message if the request fails
					return $"Error: {response.StatusCode}";
				}
			}
		}
		public static async Task<string> GetStockLevelReportAsCsvAsync()
		{
			var url = "https://data.circl.services/api/dataset";

			// Define the payload
			var payload = new
			{
				database = 67,
				type = "native",
				native = new
				{
					query = "SELECT * FROM reports.stock_level_report WHERE CAST(opening_stock_created_at AS DATE) = CURRENT_DATE - INTERVAL '1 DAY' LIMIT 10"
				}
			};

			// Serialize payload to JSON
			var jsonPayload = JsonConvert.SerializeObject(payload);

			// Create HttpClient instance
			using (HttpClient client = new HttpClient())
			{
				// Add necessary headers
				client.DefaultRequestHeaders.Add("Authorization", "Bearer 1b574196-4a86-443b-a0e2-73cada195c41");
				client.DefaultRequestHeaders.Add("Cookie", "metabase.DEVICE=08ad3c0c-664f-479f-a844-97ae2a5d7ac2; metabase.SESSION=1b574196-4a86-443b-a0e2-73cada195c41; metabase.TIMEOUT=alive");

				// Set content with JSON
				var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

				// Make the POST request
				HttpResponseMessage response = await client.PostAsync(url, content);

				// Check for success
				if (response.IsSuccessStatusCode)
				{
					// Get the response content as a string
					var jsonResponse = await response.Content.ReadAsStringAsync();

					JObject? data = JObject.Parse(jsonResponse);

					// Ensure 'rows' field exists and check if it's empty
					JArray? reports = data["data"]?["rows"] as JArray;
					if (reports == null || !reports.HasValues)
					{
						return "No data available for the current date.";
					}

					// Start building CSV
					StringBuilder csv = new StringBuilder();

					// Add headers
					csv.AppendLine("ContainerName,ProductTypeName,TotalStockLevel,AvailableStockLevel,ReservedStockLevel,InPositiveVarianceStockLevel,InNegativeVarianceStockLevel,OpeningStockAmount,OpeningStockCreatedAt,ClosingStockAmount,ClosingStockCreatedAt");

					// Loop through each report and create StockLevelReport objects
					foreach (var reportItem in reports)
					{
						if (reportItem is JObject report)
						{
							var stockReport = new StockLevelReport
							{
								ContainerName = report["container_name"]?.ToString(),
								ProductTypeName = report["product_type_name"]?.ToString(),
								TotalStockLevel = report["total_stock_level"]?.ToObject<int?>(),
								AvailableStockLevel = report["available_stock_level"]?.ToObject<int?>(),
								ReservedStockLevel = report["reserved_stock_level"]?.ToObject<int?>(),
								InPositiveVarianceStockLevel = report["in_positive_variance_stock_level"]?.ToObject<int?>(),
								InNegativeVarianceStockLevel = report["in_negative_variance_stock_level"]?.ToObject<int?>(),
								OpeningStockAmount = report["opening_stock_amount"]?.ToObject<int?>(),
								OpeningStockCreatedAt = report["opening_stock_created_at"]?.ToObject<DateTime?>(),
								ClosingStockAmount = report["closing_stock_amount"]?.ToObject<int?>(),
								ClosingStockCreatedAt = report["closing_stock_created_at"]?.ToObject<DateTime?>()
							};

							// Append each report to the CSV
							csv.AppendLine($"{stockReport.ContainerName},{stockReport.ProductTypeName},{stockReport.TotalStockLevel},{stockReport.AvailableStockLevel},{stockReport.ReservedStockLevel},{stockReport.InPositiveVarianceStockLevel},{stockReport.InNegativeVarianceStockLevel},{stockReport.OpeningStockAmount},{stockReport.OpeningStockCreatedAt?.ToString("o")},{stockReport.ClosingStockAmount},{stockReport.ClosingStockCreatedAt?.ToString("o")}");
						}
					}

					// Return the CSV string
					return csv.ToString();
				}
				else
				{
					// Return error message if the request fails
					return $"Error: {response.StatusCode}";
				}
			}
		}


	}
}



