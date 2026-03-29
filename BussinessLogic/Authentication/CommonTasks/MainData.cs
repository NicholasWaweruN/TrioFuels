using DocumentFormat.OpenXml.Vml;

namespace BusinessLogic.Authentication.CommonTasks
{
	public class MainData : IMainData
	{
		public MainData()
		{
			Role = [];
			Apps = [];
			Nozzles = [];
			Prices = [];
		}

		public string Username { get; set; } = string.Empty;
		public string Names { get; set; } = string.Empty;
		public string DispenserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		public string DispenserCode { get; set; } = string.Empty;
		public List<string> Role { get; set; }
		public string Token { get; set; } = string.Empty;
		public string ShiftNumber { get; set; } = string.Empty;
		public List<string> Apps { get; set; }
		public List<string> Nozzles { get; set; }
		public string PayrollNumber { get; set; } = string.Empty;
		public string UserCode { get; set; } = string.Empty;
		public string TillNumber { get; set; } = string.Empty;
		public string StoreNumber { get; set; } = string.Empty;
		public Dictionary<string, decimal> Prices { get; set; }
	}
}
