using Azure.Identity;
using BusinessLogic.Authentication.AddUsers;
using BusinessLogic.Authentication.CommonTasks;
using BusinessLogic.Authentication.Token;
using BusinessLogic.Authentication.UserApplications;
using BusinessLogic.Customers.Complains;
using BusinessLogic.CustomerService;
using BusinessLogic.DashBoard;
using BusinessLogic.EmailService;
using BusinessLogic.Payments.PaymentSetups;
using BusinessLogic.Roles;
using BusinessLogic.Sales.Archive_data;
using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.Sales.MissingSales;
using BusinessLogic.Sales.Receipts;
using BusinessLogic.Sales.ReverseSales;
using BusinessLogic.Sales.Target;
using BusinessLogic.Sales.Wallet;
using BusinessLogic.Services;
using BusinessLogic.SetupService;
using BusinessLogic.Station.DispenserAssignments;
using BusinessLogic.Station.Nozzzles;
using BusinessLogic.Station.Station;
using BusinessLogic.Station.StationDispenser;
using BusinessLogic.Station.StationTank;
using BusinessLogic.Stock.Stock;
using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Analytics;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Authentication.SignIn;
using BussinessLogic.Authentication.Token;
using BussinessLogic.CouponsService;
using BussinessLogic.Customers.Vehicles;
using BussinessLogic.DashBoard;
using BussinessLogic.Messaging;
using BussinessLogic.Personal_Wallet;
using BussinessLogic.PlateDetection;
using BussinessLogic.Reports.Shifts_Clossing;
using BussinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Sales.MissingSales;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Sales.PriceApproval;
using BussinessLogic.Sales.Sales_ForeCast;
using BussinessLogic.Sales.SalesData;
using BussinessLogic.Sales.Wallet;
using BussinessLogic.Sales.Wallet.Voucher;
using BussinessLogic.Setup;
using BussinessLogic.Station.DispenserAssignments;
using BussinessLogic.Stock.Shifts;
using BussinessLogic.Stock.Stock;
using BussinessLogic.Stock.Totalizers;
using BussinessLogic.Stock.VarianceReport;
using BussinessLogic.Worker;
using BussinessLogic.Worker.Authentication;
using BussinessLogic.Worker.OtherReports;
using BussinessLogic.Worker.RecordedTotalizer_Readings;
using BussinessLogic.Worker.Roles;
using BussinessLogic.Worker.SalesReport;
using BussinessLogic.Worker.StockReports;

using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using OfficeOpenXml;
using ProtoOS.MiddleWares;
using Syncfusion.Licensing;
using Syncfusion.Pdf;
using System.Text;

Console.WriteLine("=== APP STARTING ===");

try
{
	var builder = WebApplication.CreateBuilder(args);

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


builder.Services.Configure<HostOptions>(opts =>
{
	opts.BackgroundServiceExceptionBehavior =
		BackgroundServiceExceptionBehavior.Ignore;
});

	// ========================
	// Configuration Settings
	// ========================

	var tokenKey = builder.Configuration.GetValue<string>("TokenKey");
	var accessTokenSecret = builder.Configuration["AppSettings:Token"];
	var fuelflowConnection = builder.Configuration.GetConnectionString("DefaultConnection");

	// Add these guards
	if (string.IsNullOrEmpty(fuelflowConnection)) throw new InvalidOperationException("Missing: ConnectionStrings__DefaultConnection");

	// Configure settings from appsettings
	builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
	builder.Services.Configure<AfricaIsTalkingSettings>(builder.Configuration.GetSection("AfricasTalking"));
	builder.Services.Configure<ProAfricaIsTalkingSettings>(builder.Configuration.GetSection("ProAfricasTalking"));
	builder.Services.Configure<SafetyAfricasTalking>(builder.Configuration.GetSection("SafetyAfricasTalking"));

// Syncfusion & Excel licensing
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXlceXRRQ2RcVkxxXkVWYEk=");
ExcelPackage.License.SetNonCommercialPersonal("Nicholas Waweru");

// ========================
// CORS Configuration
// ========================
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

// ========================
// PostgreSQL Database Configuration (Optimized)
// ========================

// Configure Npgsql for better performance
var dataSourceBuilder = new NpgsqlDataSourceBuilder(fuelflowConnection);
dataSourceBuilder.EnableParameterLogging();
var dataSource = dataSourceBuilder.Build();


// Main OTOContext with PostgreSQL optimizations
builder.Services.AddDbContext<OTOContext>(options =>
{
	options.UseNpgsql(dataSource, npgsqlOptions =>
	{
		// Connection pooling and performance settings
		npgsqlOptions.CommandTimeout(60);
		npgsqlOptions.EnableRetryOnFailure(
			maxRetryCount: 3,
			maxRetryDelay: TimeSpan.FromSeconds(5),
			errorCodesToAdd: null);

		// Use prepared statements for better performance
		npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

		// Migration configuration
		npgsqlOptions.MigrationsAssembly("DataAccessLayer");
	});

	// Performance optimizations
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
	options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
	options.EnableDetailedErrors(builder.Environment.IsDevelopment());

	// Configure query caching
	options.UseMemoryCache(null);

	// Batch size for bulk operations

	// Logging only in development
	if (builder.Environment.IsDevelopment())
	{
		options.LogTo(Console.WriteLine, LogLevel.Information);
	}
});

	// ========================
	// Controllers & API Configuration
	// ========================
	builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// ========================
// Identity Configuration
// ========================
builder.Services.AddIdentity<ApplicationUser, UserRoles>(options =>
{
	// User settings
	options.User.RequireUniqueEmail = true;

	// Password policies
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;

	// Lockout settings
	options.Lockout.AllowedForNewUsers = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;

	// Sign-in settings
	options.SignIn.RequireConfirmedEmail = false;
	options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<OTOContext>()
.AddDefaultTokenProviders();

// ========================
// JWT Authentication
// ========================
var key = tokenKey != null ? Encoding.ASCII.GetBytes(tokenKey) : Array.Empty<byte>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.RequireAuthenticatedSignIn = true;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero
	};
});

// ========================
// Swagger Configuration
// ========================
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Fuel Flow API",
		Version = "v2",
		Description = "Fuel Flow API - PostgreSQL Optimized"
	});

	option.OperationFilter<FileUploadOperationFilter>();

	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer",
	});

	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

// ========================
// Azure Graph Service
// ========================
builder.Services.AddScoped<GraphServiceClient>(sp =>
{
	var clientId = builder.Configuration["AzureAd:ClientId"];
	var tenantId = builder.Configuration["AzureAd:TenantId"];
	var clientSecret = builder.Configuration["AzureAd:ClientSecret"];

	var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
	return new GraphServiceClient(credential, ["https://graph.microsoft.com/.default"]);
});

// ========================
// Dependency Injection - Services
// ========================
builder.Services.AddHttpClient();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Authentication & Authorization//
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<UserRoles>>();
builder.Services.AddScoped<IAuthCommonTasks, AuthCommonTasks>();
builder.Services.AddScoped<ITokenManagement, TokenManagement>();
builder.Services.AddScoped<IUserSetups, UserSetups>();
builder.Services.AddScoped<IRegisterUsers, RegisterUsers>();
builder.Services.AddScoped<ISignInUser, SignInUser>();
builder.Services.AddScoped<IUserRole, UserRole>();
builder.Services.AddScoped<IAppsService, AppsService>();

// Common Services
builder.Services.AddScoped<ICommonSetups, CommonSetups>();
builder.Services.AddScoped<ICommonSalesTasks, CommonSalesTasks>();
builder.Services.AddTransient<Services>();
builder.Services.AddScoped<DataSets>();

// Customer Services
builder.Services.AddScoped<Customers, Customers>();
builder.Services.AddScoped<Complain, Complain>();
builder.Services.AddScoped<OtogasVehicles, OtogasVehicles>();


// Station Management
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IStationDispensers, StationDispensers>();
builder.Services.AddScoped<IDispenserNozzle, DispenserNozzle>();
builder.Services.AddScoped<IDispenserAssigments, DispenserAssigments>();
builder.Services.AddScoped<IStationTanks, StationTanks>();

// Stock Management
builder.Services.AddScoped<IStockServicecs, StockServicecs>();
builder.Services.AddScoped<IShifts, Shifts>();
builder.Services.AddScoped<VarianceReport>();
builder.Services.AddScoped<StockTakeSummaryReport>();
builder.Services.AddScoped<ReadingsTotalizers>();
builder.Services.AddScoped<ITotalizerDailyReport, TotalizerDailyReport>();
builder.Services.AddScoped<IShiftClosingReport, ShiftClosingReportService>();

// Sales Services
builder.Services.AddScoped<ISales, Sales>();
builder.Services.AddScoped<ISalesManagementService, SalesManagementService>();
builder.Services.AddScoped<IReverseSales, ReverseSales>();
builder.Services.AddScoped<IMissingSales, MissingSales>();
builder.Services.AddScoped<IMisingSale, MisingSale>();
builder.Services.AddScoped<ShiftsSales>();
builder.Services.AddScoped<Target>();
builder.Services.AddScoped<Archive_Data>();
builder.Services.AddScoped<Forecast>();
builder.Services.AddScoped<IGasPriceApproval, GasPriceApproval>();

// Wallet & Payment Services
builder.Services.AddScoped<IWalletTransactions, WalletTransactions>();
builder.Services.AddScoped<IPaymentsSetups, PaymentsSetups>();
builder.Services.AddScoped<Personal_Wallet>();
builder.Services.AddScoped<Personal_Wallet_Authentication>();
builder.Services.AddScoped<Personal_Wallet_Transactions>();
builder.Services.AddScoped<WalletDashBoard>();

// Dashboard & Reports
builder.Services.AddScoped<IDashBoard, DashBoard>();
builder.Services.AddScoped<IMainData, MainData>();
builder.Services.AddScoped<SalesReportService>();
builder.Services.AddScoped<SalesReport_Summary>();
builder.Services.AddScoped<PromotionReport>();
builder.Services.AddScoped<TransactionsSummaries>();
builder.Services.AddScoped<Statements>();
builder.Services.AddScoped<FraudAuditReport>();
builder.Services.AddScoped<RecordTotalizerReadingReport>();

// Communication Services
builder.Services.AddScoped<IMessagingService, MessagingService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<BusinessLogic.Messaging.IAfricaIsTalking, BussinessLogic.Messaging.AfricaIsTalking>();
builder.Services.AddScoped<BussinessLogic.Messaging.IEmailWorkflow, BussinessLogic.Messaging.EmailWorkflow>();
builder.Services.AddScoped<BulkSms>();
builder.Services.AddScoped<AfricaIsTalkingSettings>();
builder.Services.AddScoped<ProAfricaIsTalkingSettings>();
builder.Services.AddScoped<SafetyAfricasTalking>();

// Receipt & Document Services
builder.Services.AddScoped<ReceiptService>();
builder.Services.AddTransient<PdfDocument>(_ => new PdfDocument());

// Worker Services
builder.Services.AddScoped<IWorkerRecipients, WorkerRecipients>();
builder.Services.AddScoped<Jobs>();
builder.Services.AddScoped<AddJobs>();
builder.Services.AddScoped<UpadteJobs>();

// Plate Recognition
builder.Services.AddScoped<PlateRecognition>();
builder.Services.AddScoped<IPlateRecognitionService, PlateRecognitionService>();


// Loyalty & Coupons
builder.Services.AddScoped<ILoyaltyServices, LoyaltyServices>();
builder.Services.AddScoped<ICouponsService, CouponsService>();
builder.Services.AddScoped<ILoyaltyProgramSubscription, LoyaltyProgramSubscription>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

// Territory Management;

// Background Services
builder.Services.AddSingleton<IHostedService, EmailBackgroundService>();
builder.Services.AddSingleton<IHostedService, SalesSummaryWorker>();
builder.Services.AddSingleton<IHostedService, AuthenticationWorker>();
//builder.Services.AddSingleton<IHostedService, ApiPermissionRoleSeeder>();

// MVC Support
builder.Services.AddControllersWithViews();

// ========================
// Logging Configuration
// ========================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsProduction())
{
	builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
}

// ========================
// Build Application
// ========================
var app = builder.Build();

// ========================
// Middleware Pipeline
// ========================

// CORS
app.UseCors("AllowAll");

// Custom error handling for 401/403
app.Use(async (context, next) =>
{
	await next();

	if (!context.Response.HasStarted)
	{
		if (context.Response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
		{
			context.Response.ContentType = "text/plain";
			await context.Response.WriteAsync("401: Token Validation Has Failed. Request Access Denied");
		}
		else if (context.Response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden)
		{
			context.Response.ContentType = "text/plain";
			await context.Response.WriteAsync("403: You are not authorized to access this resource");
		}
	}
});

// Swagger (enabled in both Development and Production)
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fuel Flow API v3");
		c.RoutePrefix = "swagger";
	});
}



	// /HTTPS Redirection
	if (app.Environment.IsDevelopment())
	{
		app.UseHttpsRedirection();
	}

	// Static Files
	app.UseStaticFiles();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers


	// ========================
	// Run Application//
	// ========================
	// Make health check publicly accessible (no JWT required)
	app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }))
	   .AllowAnonymous();
	app.MapControllers();
	app.Run();
}
catch (Exception ex)
{
	Console.WriteLine("=== FATAL STARTUP ERROR ===");
	Console.WriteLine(ex.GetType().FullName);
	Console.WriteLine(ex.Message);
	Console.WriteLine(ex.StackTrace);
	if (ex.InnerException != null)
	{
		Console.WriteLine("=== INNER EXCEPTION ===");
		Console.WriteLine(ex.InnerException.Message);
		Console.WriteLine(ex.InnerException.StackTrace);
	}
	throw;
}