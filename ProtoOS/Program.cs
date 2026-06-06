using FuelFlow.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.AddDatabaseServices(builder.Configuration, builder.Environment);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration, builder.Environment);
builder.Services.AddSwaggerServices();
builder.Services.AddApplicationServices();
builder.Services.AddBackgroundWorkers();

var app = builder.Build();

// Middleware Pipeline
app.ConfigureMiddleware(builder.Environment);

app.Run();