using DataAccessLayer.Context;
using FuelFlow.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Logging ────────────────────────────────────────────────────────────────
builder.Services.AddApplicationLogging(builder);

//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//	options.ForwardedHeaders =
//		ForwardedHeaders.XForwardedFor |
//		ForwardedHeaders.XForwardedProto;

//	options.KnownNetworks.Clear();
//	options.KnownProxies.Clear();
//});
// ── Core framework ─────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

// ── Cross-cutting infrastructure ────────────────────────────────────────────
builder.Services.AddCorsServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// ── Data & identity ─────────────────────────────────────────────────────────
builder.Services.AddDatabaseServices(builder.Configuration, builder.Environment);
builder.Services.AddIdentityServices();
builder.Services.AddJwtAuthentication(builder.Configuration, builder.Environment);

// ── API documentation ───────────────────────────────────────────────────────
builder.Services.AddScalarConfiguration();

//___ Daraja_________________________________________________________________
builder.Services.AddDaraja(builder.Configuration);

// ── Application services ────────────────────────────────────────────────────
builder.Services.AddBusinessServices();
builder.Services.AddBackgroundWorkers();
builder.Services.AddHttpClient();
Console.WriteLine($"ENV: {builder.Environment.EnvironmentName}");

builder.Logging.ClearProviders();

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// ── Build ───────────────────────────────────────────────────────────────────
var app = builder.Build();



//using (var scope = app.Services.CreateScope())
//{
//	var services = scope.ServiceProvider;

//	var db = services.GetRequiredService<OTOContext>();

//	Console.WriteLine("Testing database connection...");

//	var canConnect = await db.Database.CanConnectAsync();

//	Console.WriteLine($"CanConnect = {canConnect}");

//	Console.WriteLine("Running migrations...");

//	await db.Database.MigrateAsync();

//	Console.WriteLine("Creating views...");

//	await ViewInitializer.UpdateViewsAsync(db);

//	await using var connection = db.Database.GetDbConnection();

//	await connection.OpenAsync();

//	await using var command = connection.CreateCommand();

//	command.CommandText = @"SELECT COUNT(*) FROM ""vw_SalesData""";

//	var count = Convert.ToInt64(await command.ExecuteScalarAsync());

//	Console.WriteLine($"vw_SalesData records: {count:N0}");

//	Console.WriteLine("Startup complete.");
//}
app.ConfigureMiddleware();

app.Run();
