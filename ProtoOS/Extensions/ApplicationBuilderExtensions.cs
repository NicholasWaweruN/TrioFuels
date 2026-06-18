using Microsoft.AspNetCore.HttpOverrides;
namespace FuelFlow.Extensions;

public static class ApplicationBuilderExtensions
{
	public static WebApplication ConfigureMiddleware(this WebApplication app)
	{
		// Must come first – rewrites Host/scheme before any middleware reads them.
		app.UseForwardedHeaders();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseCors("AllowAll");

		// app.UseHttpsRedirection(); // optional but recommended

		app.UseStaticFiles();

		app.UseAuthentication();
		app.UseAuthorization();

		// ONLY middleware here

		// ── UI middleware (safe BEFORE endpoints OR after grouping carefully)
		app.UseScalarUi();

		// ── Endpoints LAST (IMPORTANT)
		app.MapHealthChecks("/health");
		app.MapControllers();

		return app;
	}
}