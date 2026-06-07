
using BussinessLogic.Setup;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Roles
{
	public class UserRole : IUserRole
	{
		private readonly OTOContext _context;
		private readonly RoleManager<UserRoles> _roles;
		private readonly ILogger<UserRoles> _logger;
		private readonly ICommonSetups _setups;

		public UserRole(OTOContext context, RoleManager<UserRoles> roles, ILogger<UserRoles> logger, ICommonSetups setups)
		{
			_context = context;
			_roles = roles;
			_logger = logger;
			_setups = setups;
		}

		// Helper methods
		private async Task<bool> RoleExistsAsync(string roleName)
		{
			return await _context.Role.AnyAsync(x => x.RoleName.Equals(roleName));
		}

		private async Task<bool> UserExistsAsync(string usercode)
		{
			var user = await (from u in _context.Users
							  where u.UserCode.Equals(usercode)
							  select u).FirstOrDefaultAsync();
			if (user == null)
				return false;
			return true;

		}

		private async Task<bool> RoleAssignedToUserAsync(string userId, string roleId)
		{
			return await _context.UserRoles.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
		}
		// Add User Roles to Role Table
		public async Task<ServiceResponse<object>> AddUserRoles(string roleName)
		{
			try
			{
				if (!await RoleExistsAsync(roleName))
				{
					var role = new Role
					{
						RoleName = roleName,
						DateCreated = DateTime.UtcNow,
						RoleCode = await _setups.GetCodeGenerator("RoleCode"),
						UserCode = "99999"
					};

					await _context.Role.AddAsync(role);
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("User roles added successfully", null);
				}

				return ServiceResponse<object>.Information("Role already exists", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Assign permissions to a role
		public async Task<ServiceResponse<object>> PermisionsToARole(string roleCode, List<string> permissionIds)
		{
			try
			{
				var role = await _context.Role.FirstOrDefaultAsync(x => x.RoleCode == roleCode);
				if (role != null)
				{
					foreach (var permissionId in permissionIds)
					{
						if (await _context.Roles.AnyAsync(x => x.Id == permissionId))
						{
							var roleAndPermission = new RoleAndPermisions
							{
								RoleCode = roleCode,
								PermissionCode = permissionId

							};
							_context.RoleAndPermisions.Add(roleAndPermission);
						}
					}
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("Role and permission added successfully", null);
				}

				return ServiceResponse<object>.Information("Role does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Assign roles to a user
		public async Task<ServiceResponse<object>> AssignRolesToUser(string roleId, string userId)
		{
			try
			{
				if (await UserExistsAsync(userId) && await _context.Role.AnyAsync(x => x.RoleCode == roleId))
				{
					if (roleId == "008")
					{
						return ServiceResponse<object>.Information("The maximum number of users for this role has been reached. Cannot assign to another user.", null);
					}

					if (!await RoleAssignedToUserAsync(userId, roleId))
					{
						var RoleToUser = new RoleToUser
						{
							DateCreated = DateTime.UtcNow,
							UserCode = userId,
							RoleCode = roleId
						};
						await _context.AddRangeAsync(RoleToUser);
						await _context.SaveChangesAsync();

						return ServiceResponse<object>.Success("Role assigned successfully", null);
					}

					return ServiceResponse<object>.Information("Role already assigned to user", null);
				}

				return ServiceResponse<object>.Information("User or Role does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Get all permissions assigned to a user
		public async Task<ServiceResponse<object>> GetAllPermisionsAssignedToUser(string userId)
		{
			try
			{

				var user = await _context.Users.Where(x => x.UserCode == userId).FirstOrDefaultAsync();
				if (user is not null)
				{
					var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
					return ServiceResponse<object>.Success("Permissions fetched successfully", userRoles);
				}

				return ServiceResponse<object>.Information("User does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Get all roles assigned to a user
		public async Task<ServiceResponse<object>> GetAllRolesAssignedToUser(string userId)
		{
			try
			{
				var user = await _context.Users.Where(x => x.UserCode == userId).FirstOrDefaultAsync();
				if (user is not null)
				{
					var userRoles = await _context.Role.Where(x => x.UserCode == userId).ToListAsync();
					return ServiceResponse<object>.Success("Roles fetched successfully", userRoles);
				}

				return ServiceResponse<object>.Information("User does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}


	

		// Assign a role to a user
		public async Task<ServiceResponse<object>> AssignRoleToUser(string userId, string roleId)
		{
			try
			{
				var user = await _context.Users.Where(x => x.UserCode == userId).FirstOrDefaultAsync();
				if (user is not null)
				{
					if (await _context.Role.AnyAsync(x => x.RoleCode == roleId))
					{
						if (!await RoleAssignedToUserAsync(userId, roleId))
						{
							var userRole = new IdentityUserRole<string> { RoleId = roleId, UserId = userId };
							await _context.UserRoles.AddAsync(userRole);
							await _context.SaveChangesAsync();

							return ServiceResponse<object>.Success("Role assigned successfully", userRole);
						}
						return ServiceResponse<object>.Information("Role already assigned to user", null);
					}
				}
				return ServiceResponse<object>.Information("User or Role does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Remove a role from a user
		public async Task<ServiceResponse<object>> RemoveRoleFromUser(string userId, string roleId)
		{
			try
			{
				if (await UserExistsAsync(userId) && await _context.Role.AnyAsync(x => x.RoleCode == roleId))
				{
					var userRole = await _context.RoleToUser.FirstOrDefaultAsync(x => x.UserCode == userId && x.RoleCode == roleId);
					if (userRole != null)
					{
						_context.RoleToUser.Remove(userRole);
						await _context.SaveChangesAsync();
						return ServiceResponse<object>.Success("Role removed successfully", userRole);
					}

					return ServiceResponse<object>.Information("Role not assigned to user", null);
				}

				return ServiceResponse<object>.Information("User or Role does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		// Get all users assigned to a role
		public async Task<ServiceResponse<object>> GetAllUsersAssignedToRole(string roleId)
		{
			try
			{
				if (await _context.Role.AnyAsync(x => x.RoleCode == roleId))
				{
					var users = await (from rt in _context.RoleToUser
									   join u in _context.Users on rt.UserCode
									   equals u.UserCode
									   where rt.RoleCode.Equals(roleId)
									   select new
									   {
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName }),
										   u.UserCode,
										   u.PayrollNumber,
										   u.PhoneNumber
									   }).ToListAsync();

					var userRoles = await _context.UserRoles.Where(x => x.RoleId == roleId).ToListAsync();
					return ServiceResponse<object>.Success("Users fetched successfully", users);
				}

				return ServiceResponse<object>.Information("Role does not exist", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong", null);
			}
		}
		// Get all permissions
		public async Task<ServiceResponse<object>> GetAllPermisions()
		{
			try
			{
				var permissions = await _context.Roles.Select(r => new { r.Id, r.Name }).ToListAsync();
				return ServiceResponse<object>.Success("Permissions fetched successfully", permissions);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong, contact admin", null);
			}
		}
		public async Task<ServiceResponse<object>> RolePermissions(string RoleCode)
		{
			try
			{

				var permissions = await (
								from role in _context.Roles
								join rp in _context.RoleAndPermisions on role.Id equals rp.PermissionCode
								//join rt in _context.RoleToUser on rp.RoleCode equals rt.RoleCode
								where rp.RoleCode == RoleCode
								select new
								{
									role.Id,
									role.Name
								}).Distinct().ToListAsync();


				return ServiceResponse<object>.Success("Permissions fetched successfully", permissions);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong, contact admin", null);
			}
		}
		// Get all roles
		public async Task<ServiceResponse<object>> GetAllRoles()
		{
			try
			{
				var roles = await _context.Role.Select(r => new { r.RoleCode, r.RoleName }).ToListAsync();
				return ServiceResponse<object>.Success("Roles fetched successfully", roles);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("Something went wrong", null);
			}
		}

		public async Task<ServiceResponse<object>> RemovePermisionsFromARole(string roleCode, List<string> permissionIds)
		{

			try
			{
				var role = await _context.Role.FirstOrDefaultAsync(x => x.RoleCode == roleCode);
				if (role != null)
				{
					foreach (var permissionId in permissionIds)
					{
						var perm = await _context.Roles.AnyAsync(x => x.Id == permissionId);

						if (perm)
						{
							_context.Remove(perm);
							await _context.SaveChangesAsync();
						}
					}
					await _context.SaveChangesAsync();

					return ServiceResponse<object>.Success("Role and permission has been removed successfully", null);
				}

				return ServiceResponse<object>.Information("Role does not exist", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}


	}


}
