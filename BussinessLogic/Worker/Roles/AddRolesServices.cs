using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BussinessLogic.Worker.Roles
{
	public sealed class StringTupleComparer : IEqualityComparer<(string, string)>
	{
		private static readonly StringComparer C = StringComparer.OrdinalIgnoreCase;
		public bool Equals((string, string) x, (string, string) y) =>
			C.Equals(x.Item1, y.Item1) && C.Equals(x.Item2, y.Item2);
		public int GetHashCode((string, string) obj)
		{
			unchecked { return (C.GetHashCode(obj.Item1) * 397) ^ C.GetHashCode(obj.Item2); }
		}
		public static readonly StringTupleComparer OrdinalIgnoreCase = new();
	}

	public class ApiPermissionRoleSeeder : IHostedService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<ApiPermissionRoleSeeder> _logger;

		private static readonly string[] TargetAssemblyNames = new[] { "FuelFlow", "FuelFlow" };

		public ApiPermissionRoleSeeder(IServiceProvider serviceProvider, ILogger<ApiPermissionRoleSeeder> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRoles>>();
			var context = scope.ServiceProvider.GetRequiredService<OTOContext>();

			_logger.LogInformation("🚀 Starting API Permission Role Seeder...");

			var controllerAssemblies = ResolveTargetAssemblies();
			var rolesToCreate = ExtractControllerRolePermissions(controllerAssemblies);

			foreach (var (apiPermission, roleName) in rolesToCreate)
			{
				var role = await EnsureIdentityRoleAsync(roleManager, roleName, apiPermission);
				await EnsureBusinessRoleMappingsAsync(context, roleName, role.Id, cancellationToken);
			}

			_logger.LogInformation("✅ API Permission Role Seeder completed successfully.");
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

		// -----------------------------------------------------------
		// 🔧 Helper Methods
		// -----------------------------------------------------------

		private static IEnumerable<Assembly> ResolveTargetAssemblies()
		{
			var loaded = AppDomain.CurrentDomain.GetAssemblies();
			var found = new List<Assembly>();

			foreach (var name in TargetAssemblyNames.Distinct(StringComparer.OrdinalIgnoreCase))
			{
				var match = loaded.FirstOrDefault(a => a.GetName().Name!.Equals(name, StringComparison.OrdinalIgnoreCase));
				if (match != null)
				{
					found.Add(match);
					continue;
				}
				try
				{
					var loadedByName = Assembly.Load(name);
					if (loadedByName != null) found.Add(loadedByName);
				}
				catch { }
			}

			if (found.Count == 0)
				found.Add(Assembly.GetExecutingAssembly());

			return found.Distinct();
		}

		private static string? InferLegacyHttpMethod(MethodInfo method)
		{
			if (method.GetCustomAttribute<HttpPostAttribute>(true) != null) return "POST";
			if (method.GetCustomAttribute<HttpPutAttribute>(true) != null) return "PUT";
			if (method.GetCustomAttribute<HttpDeleteAttribute>(true) != null) return "DELETE";
			if (method.GetCustomAttribute<HttpPatchAttribute>(true) != null) return "PATCH";
			if (method.GetCustomAttribute<HttpHeadAttribute>(true) != null) return "HEAD";
			if (method.GetCustomAttribute<HttpOptionsAttribute>(true) != null) return "OPTIONS";
			return null;
		}

		private static HashSet<(string ApiPermission, string RoleName)> ExtractControllerRolePermissions(IEnumerable<Assembly> assemblies)
		{
			var rolesToCreate = new HashSet<(string ApiPermission, string RoleName)>(StringTupleComparer.OrdinalIgnoreCase);

			foreach (var asm in assemblies)
			{
				foreach (var type in asm.GetTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract))
				{
					var controllerName = type.Name.EndsWith("Controller", StringComparison.Ordinal)
						? type.Name[..^"Controller".Length]
						: type.Name;

					var classRoles = type.GetCustomAttributes<AuthorizeAttribute>(true)
						.Where(a => !string.IsNullOrWhiteSpace(a.Roles))
						.SelectMany(a => a.Roles!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
						.ToHashSet(StringComparer.OrdinalIgnoreCase);

					var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

					foreach (var method in methods)
					{
						var methodRoles = method.GetCustomAttributes<AuthorizeAttribute>(true)
							.Where(a => !string.IsNullOrWhiteSpace(a.Roles))
							.SelectMany(a => a.Roles!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
							.ToHashSet(StringComparer.OrdinalIgnoreCase);

						if (classRoles.Count == 0 && methodRoles.Count == 0)
							continue;

						var httpAttr = method.GetCustomAttributes().OfType<HttpMethodAttribute>().FirstOrDefault();
						var httpMethod = httpAttr?.HttpMethods?.FirstOrDefault()
										 ?? InferLegacyHttpMethod(method)
										 ?? "GET";

						var actionSegment = httpAttr?.Template?.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
											?? method.GetCustomAttribute<RouteAttribute>(true)?.Template?.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
											?? method.Name;

						var apiPermission = $"{httpMethod.ToUpperInvariant()}:{controllerName}:{actionSegment}";

						foreach (var roleName in classRoles.Union(methodRoles, StringComparer.OrdinalIgnoreCase))
							rolesToCreate.Add((apiPermission, roleName));
					}
				}
			}

			return rolesToCreate;
		}

		// -----------------------------------------------------------
		// 🧩 FIXED ROLE CREATION / UPDATE METHOD
		// -----------------------------------------------------------
		private async Task<UserRoles> EnsureIdentityRoleAsync(RoleManager<UserRoles> roleManager, string roleName, string apiPermission)
		{
			var existingRole = await roleManager.FindByNameAsync(roleName);

			if (existingRole != null)
			{
				// ✅ Avoid reattaching the tracked entity
				existingRole.ApiPermission = apiPermission;
				await roleManager.UpdateNormalizedRoleNameAsync(existingRole);

				var updateResult = await roleManager.UpdateAsync(existingRole);
				if (!updateResult.Succeeded)
					_logger.LogError("❌ Failed to update Identity role {RoleName}: {Errors}", roleName,
						string.Join(", ", updateResult.Errors.Select(e => e.Description)));
				else
					_logger.LogInformation("🔄 Updated Identity role: {RoleName}", roleName);

				return existingRole;
			}
			else
			{
				var newRole = new UserRoles
				{
					Id = Guid.NewGuid().ToString(),
					Name = roleName,
					NormalizedName = roleName.ToUpperInvariant(),
					ApiPermission = apiPermission
				};

				// ✅ New role creation


				var createResult = await roleManager.CreateAsync(newRole);
				if (!createResult.Succeeded)
					_logger.LogError("❌ Failed to create Identity role {RoleName}: {Errors}",
						roleName, string.Join(", ", createResult.Errors.Select(e => e.Description)));
				else
					_logger.LogInformation("✅ Created new Identity role: {RoleName}", roleName);

				return newRole;
			}
		}

		// -----------------------------------------------------------
		// 🧩 FIXED BUSINESS ROLE MAPPINGS
		// -----------------------------------------------------------
		private async Task EnsureBusinessRoleMappingsAsync(OTOContext context, string roleName, string permissionId, CancellationToken cancellationToken)
		{
			context.ChangeTracker.Clear(); // 🧹 Clear tracked entities to prevent conflicts

			var userCode = "99999";
			var now = DateTime.UtcNow;
			var roleCode = "001";			

			if (!(roleCode == "001" && await context.RoleToUser.AnyAsync(r => r.RoleCode == "001" && r.UserCode == "99999", cancellationToken)))
			{
				if (!await context.RoleToUser.AnyAsync(r => r.RoleCode == roleCode && r.UserCode == userCode, cancellationToken))
				{
					context.RoleToUser.Add(new RoleToUser
					{
						RoleCode = roleCode,
						UserCode = userCode,
						DateCreated = now
					});
				}
			}

			if (!await context.RoleAndPermisions.AnyAsync(r =>  r.PermissionCode == permissionId, cancellationToken))
			{
				context.RoleAndPermisions.Add(new RoleAndPermisions
				{
					RoleCode = roleCode,
					PermissionCode = permissionId,
					UserCode = userCode,
					DateCreated = now
				});
			}

			await context.SaveChangesAsync(cancellationToken);
		}
	}
}
