using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom_Daraja;

namespace Safaricom_Daraja.DarajaTokenService;

/// <summary>
/// Fetches and caches the Daraja OAuth token.
/// Token lifetime is 1 hour; we refresh 60 s early to be safe.
/// </summary>
public interface IDarajaTokenService
{
	Task<string> GetAccessTokenAsync(CancellationToken ct = default);
}

public sealed class DarajaTokenService(
	IHttpClientFactory httpFactory,
	IOptions<DarajaConfig> options,
	ILogger<DarajaTokenService> logger) : IDarajaTokenService
{
	private readonly DarajaConfig _cfg = options.Value;

	private string _cachedToken = string.Empty;
	private DateTime _expiresAt = DateTime.MinValue;
	private readonly SemaphoreSlim _lock = new(1, 1);

	public async Task<string> GetAccessTokenAsync(CancellationToken ct = default)
	{
		if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _expiresAt)
			return _cachedToken;

		await _lock.WaitAsync(ct);
		try
		{
			// Double-check after acquiring lock
			if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _expiresAt)
				return _cachedToken;

			var token = await FetchTokenAsync(ct);
			_cachedToken = token.AccessToken;
			_expiresAt = DateTime.UtcNow.AddSeconds(int.Parse(token.ExpiresIn) - 60);

			logger.LogInformation("Daraja token refreshed. Expires at {ExpiresAt} UTC", _expiresAt);
			return _cachedToken;
		}
		finally
		{
			_lock.Release();
		}
	}

	private async Task<DarajaTokenResponse> FetchTokenAsync(CancellationToken ct)
	{
		var client = httpFactory.CreateClient("Daraja");

		var credentials = Convert.ToBase64String(
			Encoding.UTF8.GetBytes($"{_cfg.ConsumerKey}:{_cfg.ConsumerSecret}"));

		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Basic", credentials);

		var response = await client.GetAsync("/oauth/v1/generate?grant_type=client_credentials", ct);
		response.EnsureSuccessStatusCode();

		var content = await response.Content.ReadAsStringAsync(ct);
		return JsonSerializer.Deserialize<DarajaTokenResponse>(content)
			   ?? throw new InvalidOperationException("Empty token response from Daraja.");
	}
}