namespace Safaricom_Daraja;

public sealed class DarajaConfig
{
	public const string SectionName = "Daraja";

	public string ConsumerKey { get; set; } = string.Empty;
	public string ConsumerSecret { get; set; } = string.Empty;
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// Head-office shortcode (e.g. 4161705).
	/// Used as BusinessShortCode in all STK Push requests.
	/// Password is built using the individual TillNumber, not this.
	/// </summary>
	public string BusinessShortCode { get; set; } = string.Empty;

	public string PassKey { get; set; } = string.Empty;
	public string StkCallbackUrl { get; set; } = string.Empty;

	public string C2BShortCode { get; set; } = string.Empty;
	public string C2BValidationUrl { get; set; } = string.Empty;
	public string C2BConfirmationUrl { get; set; } = string.Empty;

	public string OrganizationMsisdn { get; set; } = string.Empty;
	public string PullCallbackUrl { get; set; } = string.Empty;
	public string PullNominatedNumber { get; set; } = string.Empty;

	public List<TillConfig> Tills { get; set; } = [];
}

public sealed class TillConfig
{
	public string Name { get; set; } = string.Empty;
	public string TillNumber { get; set; } = string.Empty;
	public string AccountReference { get; set; } = string.Empty;
}