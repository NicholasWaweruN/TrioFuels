using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Safaricom_Daraja.DarajaTokenService;

public interface IDarajaTokenService
{
	Task<string> GetAccessTokenAsync(CancellationToken ct = default);
}

public sealed class DarajaTokenService : IDarajaTokenService
{
	private readonly IHttpClientFactory _factory;
	private readonly IMemoryCache _cache;
	private readonly DarajaConfig _config;
	private readonly ILogger<DarajaTokenService> _logger;

	private const string CacheKey = "daraja-token";

	public DarajaTokenService(
		IHttpClientFactory factory,
		IMemoryCache cache,
		IOptions<DarajaConfig> options,
		ILogger<DarajaTokenService> logger)
	{
		_factory = factory;
		_cache = cache;
		_config = options.Value;
		_logger = logger;
	}

	public async Task<string> GetAccessTokenAsync(CancellationToken ct = default)
	{
		if (_cache.TryGetValue(CacheKey, out string? token))
		{
			return token!;
		}

		var client = _factory.CreateClient("Daraja");

		var credentials = Convert.ToBase64String(
			Encoding.UTF8.GetBytes(
				$"{_config.ConsumerKey}:{_config.ConsumerSecret}"));

		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Basic", credentials);

		HttpResponseMessage response;

		try
		{
			response = await client.GetAsync(
				"/oauth/v1/generate?grant_type=client_credentials",
				ct);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Daraja token request failed (network error)");
			throw;
		}

		var body = await response.Content.ReadAsStringAsync(ct);

		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError(
				"Daraja token failed. Status:{Status} Body:{Body}",
				response.StatusCode,
				body);

			throw new Exception("Failed to obtain Daraja access token.");
		}

		DarajaTokenResponse? result;

		try
		{
			result = JsonSerializer.Deserialize<DarajaTokenResponse>(
				body,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Invalid token JSON: {Body}", body);
			throw;
		}

		if (string.IsNullOrWhiteSpace(result?.AccessToken))
		{
			_logger.LogError("Daraja returned empty access token: {Body}", body);
			throw new Exception("Invalid Daraja token response.");
		}

		// Cache with safety buffer (55 min instead of 60)
		_cache.Set(
			CacheKey,
			result.AccessToken,
			TimeSpan.FromMinutes(55));

		_logger.LogInformation("Daraja access token generated successfully.");

		return result.AccessToken;
	}
}



public sealed class DarajaTokenResponse
{
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; } = string.Empty;

	[JsonPropertyName("expires_in")]
	public string ExpiresIn { get; set; } = string.Empty;
}