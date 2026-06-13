using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace FuelFlow.Extensions;

public static class ScalarExtensions
{
	public static IServiceCollection AddScalarConfiguration(
		this IServiceCollection services)
	{
		services.AddOpenApi(options =>
		{
			// JWT Bearer Security
			options.AddDocumentTransformer((document, context, ct) =>
			{
				document.Components ??= new OpenApiComponents();

				document.Components.SecuritySchemes ??=
					new Dictionary<string, IOpenApiSecurityScheme>();

				document.Components.SecuritySchemes["Bearer"] =
					new OpenApiSecurityScheme
					{
						Type = SecuritySchemeType.Http,
						Scheme = "bearer",
						BearerFormat = "JWT",
						Description = "Enter your JWT token"
					};

				return Task.CompletedTask;
			});

			options.AddDocumentTransformer((document, context, ct) =>
			{
				document.Servers =
				[
					new OpenApiServer
					{
						Url = "https://triofuels-production.up.railway.app"
					}
				];

				return Task.CompletedTask;
			});

			// Sort endpoints alphabetically
			options.AddDocumentTransformer((document, context, ct) =>
			{
				if (document.Paths is not null)
				{
					var sorted = document.Paths
						.OrderBy(x => x.Key, StringComparer.OrdinalIgnoreCase)
						.ToList();

					document.Paths.Clear();

					foreach (var (key, value) in sorted)
					{
						document.Paths.Add(key, value);
					}
				}

				return Task.CompletedTask;
			});
		});

		return services;
	}

	public static IEndpointRouteBuilder UseScalarUi(
		this IEndpointRouteBuilder app)
	{
		app.MapOpenApi();

		app.MapScalarApiReference(options =>
		{
			options.Title = "FuelFlow API Reference";
			options.Theme = ScalarTheme.BluePlanet;
			options.Layout = ScalarLayout.Classic;

			options.DarkMode = true;
			options.HideDarkModeToggle = false;

			options.ShowSidebar = true;
			options.HideModels = true;
			options.HideSearch = false;
			options.DefaultOpenAllTags = true;

			options.ShowOperationId = false;
			options.OperationTitleSource = OperationTitleSource.Summary;

			options.HideClientButton = false;
			options.HideTestRequestButton = false;

			options.PersistentAuthentication = true;

			options.WithOpenApiRoutePattern("/openapi/{documentName}.json");

			options.DefaultHttpClient =
				new KeyValuePair<ScalarTarget, ScalarClient>(
					ScalarTarget.CSharp,
					ScalarClient.HttpClient);

			options.AddPreferredSecuritySchemes("Bearer");

			options.AddHttpAuthentication("Bearer", auth =>
			{
				auth.Token = string.Empty;
			});

			options.ExpandAllModelSections = false;
			options.ExpandAllResponses = true;
			options.OrderRequiredPropertiesFirst = true;

			options.DocumentDownloadType = DocumentDownloadType.Json;
		});

		return app;
	}
}