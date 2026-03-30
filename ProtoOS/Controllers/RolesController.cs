using BusinessLogic.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FuelFlow.Controllers 
{
    [ApiController]
    [Route("fuelflow/[controller]")]
    [Authorize(Roles = "can manage user roles")]
	public class UserRolesController : ControllerBase
    {
        private readonly IUserRole _userRolesService;
		
        public UserRolesController(IUserRole userRolesService)
        {
            _userRolesService = userRolesService;
        }
		 
        [HttpGet("get-all-permisions")]
        [Authorize(Roles = "can view all permissions")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _userRolesService.GetAllPermisions();
            return Ok(result);
        }

        [HttpPost("add-role")]
        [Authorize(Roles="can add a role")]
        public async Task<IActionResult> AddUserRoles(string roleName)
        {
            var result = await _userRolesService.AddUserRoles(roleName);
            return Ok(result);
        }

        [HttpPost("assign-permissions-to-role")]
        [Authorize(Roles="can assign permissions to a role")]
        public async Task<IActionResult> AssignPermissionsToRole(string roleId, [FromBody] List<string> permissionIds)
        {
            var result = await _userRolesService.PermisionsToARole(roleId, permissionIds);
            return Ok(result);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(Roles="can assign a role to a user")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleId)
        {
            var result = await _userRolesService.AssignRolesToUser(roleId, userId);
            return Ok(result);
        }

        [HttpGet("get-user-permissions/{userId}")]
        [Authorize(Roles="can view user permissions")]
        public async Task<IActionResult> GetUserPermissions(string userId)
        {
            var result = await _userRolesService.GetAllPermisionsAssignedToUser(userId);
            return Ok(result);
        }

        [HttpGet("get-user-roles/{userId}")]
        [Authorize(Roles="can view user roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var result = await _userRolesService.GetAllRolesAssignedToUser(userId);
            return Ok(result);
        }

        [HttpPost("remove-role-from-user")]
        [Authorize(Roles="can remove a role from a user")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleId)
        {
            var result = await _userRolesService.RemoveRoleFromUser(userId, roleId);
            return Ok(result);
        }

        [HttpGet("get-users-assigned-to-role/{roleId}")]
        [Authorize(Roles="can view users assigned to a role")]
        public async Task<IActionResult> GetUsersAssignedToRole(string roleId)
        {
            var result = await _userRolesService.GetAllUsersAssignedToRole(roleId);
            return Ok(result);
        }

        [HttpPost("assign-user-to-role")]
        [Authorize(Roles="can assign a user to a role")]
        public async Task<IActionResult> AssignUserToRole(string userId, string roleId)
        {
            var result = await _userRolesService.AssignRolesToUser(userId, roleId);
            return Ok(result);
        }

		[HttpGet("get-all-roles")]
		[Authorize(Roles = "can view all roles")]
		public async Task<IActionResult>GetRoles()
		{
			var result = await _userRolesService.GetAllRoles();
			return Ok(result);
		}

		[HttpGet("get-roles-permisions")]
		[Authorize(Roles = "can view roles and permissions")]

		public async Task<IActionResult> RolePermissions(string RoleCode)
		{
			var result = await _userRolesService.RolePermissions(RoleCode);
			return Ok(result);
		}

		[HttpPost("remove-permissions-to-role")]
		[Authorize(Roles = "can remove permission from a role")]
		public async Task<IActionResult> RolePermissions(string RoleCode,List<string> permissionIds)
		{
			var result = await _userRolesService.RemovePermisionsFromARole(RoleCode,permissionIds);
			return Ok(result);
		}
	}
}
