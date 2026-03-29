using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Authentication;
using DataAccessLayer.EntityModels.SetUps;

namespace BussinessLogic.Authentication.CommonTasks
{
	public interface IAuthCommonTasks
	{
		Task<ServiceResponse<object>> AddUserTrail(string message, string actionType);
		string Email();
		Task ErrorTrail(ErrorTrail errorTrail);
		Task<ServiceResponse<object>> GetUserDetails();
		Task<ServiceResponse<UserDetailsDto>> GetUserDetailsAsync(string? userCode);
		string HashPin(string pin);
		string Name();
		string PayrollNumber();
		string PhoneNumber();
		string Usercode();
		string Username();
	}
}