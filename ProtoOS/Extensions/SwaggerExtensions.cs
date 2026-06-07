//using FuelFlow.MiddleWares;
//using Microsoft.OpenApi.Models;

//namespace FuelFlow.Extensions;

//public static class SwaggerExtensions
//{
//	public static IServiceCollection AddSwaggerServices(
//		this IServiceCollection services)
//	{
//		services.AddSwaggerGen(options =>
//		{
//			options.SwaggerDoc("v1",
//				new OpenApiInfo
//				{
//					Title = "Fuel Flow API",
//					Version = "v3"
//				});

//			options.OperationFilter<FileUploadOperationFilter>();

//			options.AddSecurityDefinition(
//				"Bearer",
//				new OpenApiSecurityScheme
//				{
//					Name = "Authorization",
//					Type = SecuritySchemeType.Http,
//					Scheme = "Bearer",
//					BearerFormat = "JWT",
//					In = ParameterLocation.Header,
//					Description = "Enter JWT token"
//				});

//			options.AddSecurityRequirement(
//				new OpenApiSecurityRequirement
//				{
//					{
//						new OpenApiSecurityScheme
//						{
//							Reference =
//								new OpenApiReference
//								{
//									Type =
//										ReferenceType.SecurityScheme,
//									Id = "Bearer"
//								}
//						},
//						Array.Empty<string>()
//					}
//				});
//		});

//		return services;
//	}
//}