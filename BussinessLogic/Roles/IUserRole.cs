using DataAccessLayer.Common;

namespace BusinessLogic.Roles
{
	public interface IUserRole
	{
		Task<ServiceResponse<object>> AddUserRoles(string roleName);
		Task<ServiceResponse<object>> AssignRolesToUser(string roleId, string userId);
		Task<ServiceResponse<object>> AssignRoleToUser(string userId, string roleId);
		Task<ServiceResponse<object>> GetAllPermisions();
		Task<ServiceResponse<object>> GetAllPermisionsAssignedToUser(string userId);
		Task<ServiceResponse<object>> GetAllRoles();
		Task<ServiceResponse<object>> GetAllRolesAssignedToUser(string userId);
		Task<ServiceResponse<object>> GetAllUsersAssignedToRole(string roleId);
		Task<ServiceResponse<object>> PermisionsToARole(string roleCode, List<string> permissionIds);
		Task<ServiceResponse<object>> RemovePermisionsFromARole(string roleCode, List<string> permissionIds);
		Task<ServiceResponse<object>> RemoveRoleFromUser(string userId, string roleId);
		Task<ServiceResponse<object>> RolePermissions(string RoleCode);
	}
}