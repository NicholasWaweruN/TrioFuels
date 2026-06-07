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

		// CORS must be before authentication / authorisation.
		app.UseCors("AllowAll");

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseAuthentication();
		app.UseAuthorization();

		// ── Endpoints ───────────────────────────────────────────────────────
		app.MapHealthChecks("/health");

		// Scalar: mount only when the OpenAPI document is available
		// (always in dev; guard with an env-check if you want to hide it in prod).
		app.UseScalarUi();

		app.MapControllers();

		return app;
	}
}