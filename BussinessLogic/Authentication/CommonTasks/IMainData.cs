
namespace BusinessLogic.Authentication.CommonTasks
{
	public interface IMainData
	{
		List<string> Apps { get; set; }
		string DispenserCode { get; set; }
		string DispenserName { get; set; }
		string Email { get; set; }
		string Names { get; set; }
		List<string> Nozzles { get; set; }
		string PayrollNumber { get; set; }
		string PhoneNumber { get; set; }
		Dictionary<string, decimal> Prices { get; set; }
		List<string> Role { get; set; }
		string ShiftNumber { get; set; }
		string StationCode { get; set; }
		string StationName { get; set; }
		string TillNumber { get; set; }
		string StoreNumber { get; set; }
		string Token { get; set; }
		string UserCode { get; set; }
		string Username { get; set; }
	}
}