using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Emit;

namespace FuelFlow.MiddleWares 
{
	public class TokenExpiration
	{

		private readonly RequestDelegate _next;
		private readonly IMemoryCache _cache;

		public TokenExpiration(RequestDelegate next, IMemoryCache cache)
		{
			_next = next;
			_cache = cache;
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			var appCode = context.Request.Headers["AppCode"].FirstOrDefault()?.ToString();

			if (!string.IsNullOrEmpty(token))
			{
				var AppCode = $"AppCode-{appCode}";
				var key = $"Token-{token}";

				
					if (_cache.TryGetValue(key, out _))
					{
						// Refresh sliding expiration (reset 5 min timer)
						_cache.Set(key, true, new MemoryCacheEntryOptions
						{
							SlidingExpiration = TimeSpan.FromMinutes(5)
						});
					}
					else
					{
						// Token has expired due to inactivity
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						await context.Response.WriteAsync("Token expired due to inactivity.");
						return;
					}
				
			}

			await _next(context);
		}
	}

	public class FileUploadOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var fileParams = context.MethodInfo.GetParameters()
				.Where(p => p.ParameterType == typeof(IFormFile));

			if (!fileParams.Any()) return;

			operation.RequestBody = new OpenApiRequestBody
			{
				Content = {
				["multipart/form-data"] = new OpenApiMediaType
				{
					Schema = new OpenApiSchema
					{
						Type = "object",
						Properties = {
							[fileParams.First().Name] = new OpenApiSchema
							{
								Type = "string",
								Format = "binary"
							}
						},
						Required = { fileParams.First().Name }
					}
				}
			}
			};
		}
	}
}
