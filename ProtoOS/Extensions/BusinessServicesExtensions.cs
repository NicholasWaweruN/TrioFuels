// ── BusinessLogic ──────────────────────────────────────────────────────────
using BusinessLogic.Authentication.AddUsers;
using BusinessLogic.Authentication.CommonTasks;
using BusinessLogic.Authentication.Token;
using BusinessLogic.Authentication.UserApplications;
using BusinessLogic.Customers.Complains;
using BusinessLogic.CustomerService;
using BusinessLogic.DashBoard;
using BusinessLogic.EmailService;
using BusinessLogic.Messaging;
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
using BusinessLogic.Worker.SalesReport;

// ── BussinessLogic (legacy namespace – keep until unified) ─────────────────
using BussinessLogic.Analytics;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Authentication.SignIn;
using BussinessLogic.Authentication.Token;
using BussinessLogic.Authentication.UserApplications;
using BussinessLogic.CouponsService;
using BussinessLogic.Customers.Vehicles;
using BussinessLogic.DashBoard;
using BussinessLogic.Messaging;
using BussinessLogic.Payments.PaymentSetups;
using BussinessLogic.Personal_Wallet;
using BussinessLogic.PlateDetection;
using BussinessLogic.Reports.Shifts_Clossing;
using BussinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Sales.MissingSales;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Sales.PriceApproval;
using BussinessLogic.Sales.ReverseSales;
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

// ── External ───────────────────────────────────────────────────────────────
using Azure.Identity;
using DataAccessLayer.DTOs.Messaging;
using Microsoft.Graph;
using Syncfusion.Pdf;

namespace FuelFlow.Extensions;

public static class BusinessServicesExtensions
{
	public static IServiceCollection AddBusinessServices(
		this IServiceCollection services)
	{
		// ── Framework helpers ───────────────────────────────────────────────
		services.AddHttpClient();
		services.AddHttpContextAccessor();

		// ── Authentication ──────────────────────────────────────────────────
		services.AddScoped<IAuthCommonTasks, AuthCommonTasks>();
		services.AddScoped<ITokenManagement, TokenManagement>();
		services.AddScoped<IUserSetups, UserSetups>();
		services.AddScoped<IRegisterUsers, RegisterUsers>();
		services.AddScoped<ISignInUser, SignInUser>();
		services.AddScoped<IUserRole, UserRole>();
		services.AddScoped<IAppsService, AppsService>();

		// ── Common / shared ─────────────────────────────────────────────────
		services.AddScoped<ICommonSetups, CommonSetups>();
		services.AddScoped<ICommonSalesTasks, CommonSalesTasks>();
		//services.AddTransient<Services>();
		services.AddScoped<DataSets>();

		// ── Customer ────────────────────────────────────────────────────────
		services.AddScoped<Customers>();
		services.AddScoped<Complain>();
		services.AddScoped<OtogasVehicles>();

		// ── Station ─────────────────────────────────────────────────────────
		services.AddScoped<IStationService, StationService>();
		services.AddScoped<IStationDispensers, StationDispensers>();
		services.AddScoped<IDispenserNozzle, DispenserNozzle>();
		services.AddScoped<IDispenserAssigments, DispenserAssigments>();
		services.AddScoped<IStationTanks, StationTanks>();

		// ── Stock ───────────────────────────────────────────────────────────
		services.AddScoped<IStockServicecs, StockServicecs>();
		services.AddScoped<IShifts, Shifts>();
		services.AddScoped<VarianceReport>();
		services.AddScoped<StockTakeSummaryReport>();
		services.AddScoped<ReadingsTotalizers>();
		services.AddScoped<ITotalizerDailyReport, TotalizerDailyReport>();
		services.AddScoped<IShiftClosingReport, ShiftClosingReportService>();

		// ── Sales ───────────────────────────────────────────────────────────
		services.AddScoped<ISales, Sales>();
		services.AddScoped<ISalesManagementService, SalesManagementService>();
		services.AddScoped<IReverseSales, ReverseSales>();
		services.AddScoped<IMissingSales, MissingSales>();
		services.AddScoped<IMisingSale, MisingSale>();
		services.AddScoped<ShiftsSales>();
		services.AddScoped<Target>();
		services.AddScoped<Archive_Data>();
		services.AddScoped<Forecast>();
		services.AddScoped<IGasPriceApproval, GasPriceApproval>();

		// ── Wallet & payments ───────────────────────────────────────────────
		services.AddScoped<IWalletTransactions, WalletTransactions>();
		services.AddScoped<IPaymentsSetups, PaymentsSetups>();
		services.AddScoped<Personal_Wallet>();
		services.AddScoped<Personal_Wallet_Authentication>();
		services.AddScoped<Personal_Wallet_Transactions>();
		services.AddScoped<WalletDashBoard>();

		// ── Dashboard & reporting ───────────────────────────────────────────
		services.AddScoped<IDashBoard, DashBoard>();
		services.AddScoped<IMainData, MainData>();
		services.AddScoped<SalesReportService>();
		services.AddScoped<SalesReport_Summary>();
		services.AddScoped<PromotionReport>();
		services.AddScoped<TransactionsSummaries>();
		services.AddScoped<Statements>();
		services.AddScoped<FraudAuditReport>();
		services.AddScoped<RecordTotalizerReadingReport>();

		// ── Microsoft Graph ─────────────────────────────────────────────────
		// Required by EmailWorkflow. Reads ClientId, TenantId, ClientSecret
		// from configuration (e.g. appsettings.json or environment variables).

		// ── Messaging & communication ───────────────────────────────────────
		services.AddScoped<IMessagingService, MessagingService>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<IAfricaIsTalking, AfricaIsTalking>();
		services.AddScoped<IEmailWorkflow, EmailWorkflow>();
		services.AddScoped<BulkSms>();

		// Settings objects (resolved directly – not via IOptions<T>)
		services.AddScoped<AfricaIsTalkingSettings>();
		services.AddScoped<ProAfricaIsTalkingSettings>();
		services.AddScoped<SafetyAfricasTalking>();

		// ── Receipts ────────────────────────────────────────────────────────
		services.AddScoped<ReceiptService>();
		// PdfDocument is not thread-safe; Transient gives each call-site its own instance.
		services.AddTransient(_ => new PdfDocument());

		// ── Worker / job support ────────────────────────────────────────────
		services.AddScoped<IWorkerRecipients, WorkerRecipients>();
		services.AddScoped<Jobs>();
		services.AddScoped<AddJobs>();
		services.AddScoped<UpadteJobs>();

		// ── Plate recognition ───────────────────────────────────────────────
		services.AddScoped<PlateRecognition>();
		services.AddScoped<IPlateRecognitionService, PlateRecognitionService>();

		// ── Loyalty & coupons ───────────────────────────────────────────────
		services.AddScoped<ILoyaltyServices, LoyaltyServices>();
		services.AddScoped<ICouponsService, CouponsService>();
		services.AddScoped<ILoyaltyProgramSubscription, LoyaltyProgramSubscription>();
		services.AddScoped<IVoucherService, VoucherService>();

		return services;
	}
}