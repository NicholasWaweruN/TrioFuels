using FuelFlow.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── Logging ────────────────────────────────────────────────────────────────
builder.Services.AddApplicationLogging(builder);

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

// ── Application services ────────────────────────────────────────────────────
builder.Services.AddBusinessServices();
builder.Services.AddBackgroundWorkers();

// ── Build ───────────────────────────────────────────────────────────────────
var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
