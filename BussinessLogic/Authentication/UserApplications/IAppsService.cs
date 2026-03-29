using DataAccessLayer.Common;

namespace BusinessLogic.Authentication.UserApplications
{
    public interface IAppsService
    {
        Task<ServiceResponse<object>> AddUserAppsAsync(string userCode, string appsCode);
        Task<ServiceResponse<object>> AssignUserToAppAsync(string userCode, string appsCode);
        Task<ServiceResponse<object>> GetApps();
        Task<ServiceResponse<object>> GetUserAppsAsync(string userCode);
        Task<ServiceResponse<object>> RemoveUserAppsAsync(string userCode, string appsCode);
    }
}