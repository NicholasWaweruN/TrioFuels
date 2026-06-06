

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
using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Analytics;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Authentication.SignIn;
using BussinessLogic.Authentication.Token;
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
using BussinessLogic.Worker.SalesReport;
using BussinessLogic.Worker.StockReports;
using DataAccessLayer.Authentication.Entity;
using DataAccessLayer.DTOs.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Pdf;

namespace FuelFlow.Extensions
{
	public static class BusinessServicesExtensions
	{
		public static IServiceCollection AddBusinessServices(
			this IServiceCollection services)
		{

			services.AddScoped<UserManager<ApplicationUser>>();
			services.AddScoped<RoleManager<UserRoles>>();

			services.AddScoped<IAuthCommonTasks, AuthCommonTasks>();
			services.AddScoped<ITokenManagement, TokenManagement>();
			services.AddScoped<IUserSetups, UserSetups>();
			services.AddScoped<IRegisterUsers, RegisterUsers>();
			services.AddScoped<ISignInUser, SignInUser>();
			services.AddScoped<IUserRole, UserRole>();
			services.AddScoped<IAppsService, AppsService>();

			services.AddScoped<ICommonSetups, CommonSetups>();
			services.AddScoped<ICommonSalesTasks, CommonSalesTasks>();

			services.AddTransient<Services>();
			services.AddScoped<DataSets>();
			// Customer Services
			services.AddScoped<Customers>();
			services.AddScoped<Complain>();
			services.AddScoped<OtogasVehicles>();

			// Station Management
			services.AddScoped<IStationService, StationService>();
			services.AddScoped<IStationDispensers, StationDispensers>();
			services.AddScoped<IDispenserNozzle, DispenserNozzle>();
			services.AddScoped<IDispenserAssigments, DispenserAssigments>();
			services.AddScoped<IStationTanks, StationTanks>();

			// Stock Management
			services.AddScoped<IStockServicecs, StockServicecs>();
			services.AddScoped<IShifts, Shifts>();
			services.AddScoped<VarianceReport>();
			services.AddScoped<StockTakeSummaryReport>();
			services.AddScoped<ReadingsTotalizers>();
			services.AddScoped<ITotalizerDailyReport, TotalizerDailyReport>();
			services.AddScoped<IShiftClosingReport, ShiftClosingReportService>();

			// Sales Services
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

			// Wallet & Payment Services
			services.AddScoped<IWalletTransactions, WalletTransactions>();
			services.AddScoped<IPaymentsSetups, PaymentsSetups>();
			services.AddScoped<Personal_Wallet>();
			services.AddScoped<Personal_Wallet_Authentication>();
			services.AddScoped<Personal_Wallet_Transactions>();
			services.AddScoped<WalletDashBoard>();

			// Dashboard & Reports
			services.AddScoped<IDashBoard, DashBoard>();
			services.AddScoped<IMainData, MainData>();
			services.AddScoped<SalesReportService>();
			services.AddScoped<SalesReport_Summary>();
			services.AddScoped<PromotionReport>();
			services.AddScoped<TransactionsSummaries>();
			services.AddScoped<Statements>();
			services.AddScoped<FraudAuditReport>();
			services.AddScoped<RecordTotalizerReadingReport>();

			// Communication Services
			services.AddScoped<IMessagingService, MessagingService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<AfricaIsTalking>();
			services.AddScoped<BussinessLogic.Messaging.IEmailWorkflow,BussinessLogic.Messaging.EmailWorkflow>();
			services.AddScoped<BulkSms>();
			services.AddScoped<AfricaIsTalkingSettings>();
			services.AddScoped<ProAfricaIsTalkingSettings>();
			services.AddScoped<SafetyAfricasTalking>();

			// Receipt & Document Services
			services.AddScoped<ReceiptService>();
			services.AddTransient<PdfDocument>(_ => new PdfDocument());

			// Worker Services
			services.AddScoped<IWorkerRecipients, WorkerRecipients>();
			services.AddScoped<Jobs>();
			services.AddScoped<AddJobs>();
			services.AddScoped<UpadteJobs>();

			// Plate Recognition
			services.AddScoped<PlateRecognition>();
			services.AddScoped<IPlateRecognitionService, PlateRecognitionService>();

			// Loyalty & Coupons
			services.AddScoped<ILoyaltyServices, LoyaltyServices>();
			services.AddScoped<ICouponsService, CouponsService>();
			services.AddScoped<ILoyaltyProgramSubscription, LoyaltyProgramSubscription>();
			services.AddScoped<IVoucherService, VoucherService>();

			// Background Services
			services.AddSingleton<IHostedService, EmailBackgroundService>();
			services.AddSingleton<IHostedService, SalesSummaryWorker>();
			services.AddSingleton<IHostedService, AuthenticationWorker>();

			return services;
		}
	}
}