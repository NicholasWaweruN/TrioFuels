using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace FuelFlow.Extensions;

public static class ScalarExtensions
{
	/// <summary>
	/// Registers the built-in .NET OpenAPI document generator and
	/// configures the Bearer security scheme for the Scalar UI.
	/// Call this during service registration (before <c>builder.Build()</c>).
	/// </summary>
	public static IServiceCollection AddScalarConfiguration(
		this IServiceCollection services)
	{
		services.AddOpenApi(options =>
		{
			// ── Security scheme ─────────────────────────────────────────
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

			// ── Sort paths A → Z ────────────────────────────────────────
			options.AddDocumentTransformer((document, context, ct) =>
			{
				if (document.Paths is not null)
				{
					
					var sorted = document.Paths
						.OrderBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
						.ToList();

					document.Paths.Clear();

					foreach (var (key, value) in sorted)
						document.Paths.Add(key, value);
				}

				return Task.CompletedTask;
			});
		});

		return services;
	}

	/// <summary>
	/// Maps the OpenAPI JSON document (<c>/openapi/v1.json</c>) and
	/// the interactive Scalar UI (<c>/scalar/v1</c>).
	/// Call this in the middleware pipeline (after <c>builder.Build()</c>).
	/// </summary>
	public static IEndpointRouteBuilder UseScalarUi(
		this IEndpointRouteBuilder app)
	{
		app.MapOpenApi();

		app.MapScalarApiReference(static options =>
		{
			// ── Appearance ──────────────────────────────────────────────────
			options.Title = "FuelFlow API Reference";
			options.Theme = ScalarTheme.BluePlanet;
			options.Layout = ScalarLayout.Classic;
			options.Favicon = "/favicon.svg";
			options.DarkMode = true;
			options.HideDarkModeToggle = false;
			

			// ── Sidebar & navigation ────────────────────────────────────────
			options.ShowSidebar = true;
			options.HideModels = true;
			options.HideSearch = false;
			options.DefaultOpenAllTags = true;
			options.ShowOperationId = false;
			options.OperationTitleSource = OperationTitleSource.Summary;

			// ── Request behaviour ───────────────────────────────────────────
			options.HideClientButton = false;
			options.HideTestRequestButton = false;
			options.PersistentAuthentication = false;

			// ── Default HTTP client: C# HttpClient ──────────────────────────
			options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(
				ScalarTarget.CSharp,
				ScalarClient.HttpClient);

			// ── Authentication (Bearer / JWT) ───────────────────────────────
			options.AddPreferredSecuritySchemes("Bearer");
			options.AddHttpAuthentication("Bearer", auth =>
			{
				auth.Token = string.Empty;
			});

			// ── Schema display ──────────────────────────────────────────────
			options.ExpandAllModelSections = false;
			options.ExpandAllResponses = false;
			options.OrderRequiredPropertiesFirst = true;

			// ── Document download ───────────────────────────────────────────
			options.DocumentDownloadType = DocumentDownloadType.None;
		});

		return app;
	}
}