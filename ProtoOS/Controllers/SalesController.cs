using BusinessLogic.DashBoard;
using BusinessLogic.Sales.Archive_data;
using BusinessLogic.Sales.MissingSales;
using BusinessLogic.Sales.Receipts;
using BusinessLogic.Sales.ReverseSales;
using BusinessLogic.Sales.Wallet;
using BussinessLogic.CouponsService;
using BussinessLogic.Messaging;
using BussinessLogic.Sales.MissingSales;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Sales.SalesData;
using BussinessLogic.Sales.Wallet;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static BusinessLogic.Sales.Archive_data.Archive_Data;
using static BussinessLogic.CouponsService.LoyaltyProgramSubscription;
using static DataAccessLayer.EntityModels.Wallet.WalletDto;

namespace FuelFlow.Controllers
{
	[Route("fuelflow/[controller]")]
	[ApiController]
	[Authorize]
	public class SalesController : ControllerBase
	{
		private readonly ISalesManagementService _salesService;
		private readonly ISales _addingSales;
		private readonly IWalletTransactions _wallet;
		private readonly IDashBoard _dashBoard;
		private readonly IMissingSales _missing;
		private readonly IReverseSales _reverse;
		private readonly IEmailService _emailService;
		private readonly IMisingSale _misingSale;
		private readonly Archive_Data _archive;
		private readonly ReceiptService _receipt;
		private readonly ICustomerStatementService _statements;
		private readonly ILoyaltyProgramSubscription _subscription;
		private readonly ICouponsService _coupons;
		private readonly ISalesByPaymentMethod _salesByPaymentMethod;

		public SalesController(ISalesManagementService salesService, ISales sales, IWalletTransactions wallet,
			IDashBoard dashBoard, IMissingSales missing, IReverseSales reverse, IEmailService emailService,
			IMisingSale misingSale, Archive_Data archive, ReceiptService receipt, ICustomerStatementService statements,
			ILoyaltyProgramSubscription loyaltyServices, ICouponsService coupons, ISalesByPaymentMethod salesByPaymentMethod)
		{
			_salesService = salesService;
			_addingSales = sales;
			_wallet = wallet;
			_dashBoard = dashBoard;
			_missing = missing;
			_reverse = reverse;
			_emailService = emailService;
			_misingSale = misingSale;
			_archive = archive;
			_receipt = receipt;
			_statements = statements;
			_subscription = loyaltyServices;
			_coupons = coupons;
			_salesByPaymentMethod = salesByPaymentMethod;
		}

		private OkObjectResult CreateResponse<T>(T response) => Ok(response);

		#region Sales Management Endpoints

		[HttpPost]
		[Route("AddSale")]
		[Authorize(Roles = "can add a sale")]
		public async Task<IActionResult> AddSale([FromBody] AddsaleDto sale)
		{
			var response = await _addingSales.AddSalesAsync(sale);
			return CreateResponse(response);
		}

		[HttpGet("mpesa/confirm/{transId}")]
		public async Task<IActionResult> ConfirmMpesaManual(string transId, CancellationToken ct)
		{
			var result = await _addingSales.ConfirmMpesaManualAsync(transId, ct);

			return CreateResponse(result);
		}





		[HttpPost]
		[Route("DeferVariance")]
		[Authorize(Roles = "can defer a variance")]
		public async Task<IActionResult> DeferVariance(string shiftNumber)
		{
			var response = await _missing.DeferVariance(shiftNumber);
			return CreateResponse(response);
		}

		[HttpPost]
		[Authorize(Roles = "can write off a variance")]
		[Route("WriteOffVariance")]
		public async Task<IActionResult> WriteOffVariance(string shiftNumber)
		{
			var response = await _missing.OffWriteVariance(shiftNumber);
			return CreateResponse(response);
		}



		[HttpPost]
		[Route("TransferSaleToAnotherNozzle")]
		[Authorize(Roles = "can transfer sale to another nozzle")]
		public async Task<IActionResult> TransferSaleToAnotherNozzle(string transactionCode, string nozzleCode)
		{
			var response = await _reverse.TransferSaleToAnotherNozzle(transactionCode, nozzleCode);
			return CreateResponse(response);
		}



		[HttpGet]
		[Authorize]
		[Route("MobileAppPaymentTypes")]///jjjjj
		public async Task<IActionResult> MobileAppPaymentTypes()
		{
			var response = await _salesService.MobileAppPaymentTypes();
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("AllPaymentTypes")]
		public async Task<IActionResult> AllPaymentTypes()
		{
			var response = await _salesService.AllPaymentTypes();
			return CreateResponse(response);
		}

		[HttpGet]
		[Authorize(Roles = "can view all sales")]
		[Route("AllSales")]
		public async Task<IActionResult> AllSales(string? stationCode, string? shiftNumber = null, string? dispenserName = null, string? nozzleName = null, string? paymentTypeName = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10, string? orderByColumn = null, bool isDescending = true)
		{
			var response = await _salesService.AllSales(stationCode, shiftNumber, dispenserName, nozzleName, paymentTypeName, startDate, endDate, pageNumber, pageSize, orderByColumn, isDescending);
			return CreateResponse(response);
		}

		[HttpGet]
		[Authorize(Roles = "can view dashBoard data")]
		[Route("GetDashBoardData")]
		public async Task<IActionResult> GetDashBoardData()
		{
			var response = await _dashBoard.GetDashBoardData();
			return CreateResponse(response);
		}






		[HttpPost]
		[Authorize(Roles = "can top up customer wallet")]
		[Route("TopUpCustomerWallet")]
		public async Task<IActionResult> TopUpCustomerWallet(TopUpCustomerWalletDto wallet)
		{
			var response = await _wallet.TopUpCustomerWallet(wallet);
			return CreateResponse(response);
		}

		#endregion
		#region Sales Management Endpoints
		[HttpPost]
		[Authorize(Roles = "can add a sale")]
		[Route("AddMisingSale")]
		public async Task<IActionResult> AddMissingSale(MisingSaleDto miss)
		{
			var response = await _misingSale.AddSalesAsync(miss);
			return CreateResponse(response);
		}

		[HttpPost]
		[Authorize(Roles = "can reverse a sale")]
		[Route("ReverseasaleAsync")]
		public async Task<IActionResult> ReverseSaleAsync(string saleid)
		{
			var response = await _reverse.ReverseSaleAsync(saleid);
			return CreateResponse(response);
		}



		[HttpGet]
		[Route("GetPaymentTransactions/{transactionCode}")]
		[Authorize(Roles = "can view customer balances")]
		public async Task<IActionResult> GetPaymentTransactions(string transactionCode)
		{
			var response = await _salesService.GetPaymentTransactions(transactionCode);
			return CreateResponse(response);
		}



		[HttpGet]
		[Route("ViewPayments/{saleId}")]
		[Authorize(Roles = "can view payments")]
		public async Task<IActionResult> ViewPayments(string saleId)
		{
			var response = await _salesService.ViewPayments(saleId);
			return CreateResponse(response);
		}


		#endregion



		[HttpGet]
		[Route("ExportDailySales")]
		[Authorize(Roles = "can view daily sales data")]
		public async Task<IActionResult> ExportCustomerTransactionsEplus(DateTime date)
		{

			var reportName = "SaleReport" + date;
			var result = await _salesService.ExportSalesReport(date);
			if (result.ResponseCode != 1)
			{
				return BadRequest(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{reportName}.xlsx");
		}

		[HttpGet]
		[Route("ExportMonthlySales/{month}/{year}")]
		[Authorize(Roles = "can view monthly sales data")]
		public async Task<IActionResult> ExportCustomerTransactionsEplus(int month, int year)
		{
			string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
			var reportName = $"SaleReport{monthName}";
			var result = await _salesService.MonthlySalesReport(month, year);
			if (result.ResponseCode != 1)
			{
				return BadRequest(result.ResponseMessage);  // Return appropriate error response
			}
			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			var file = File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{reportName}.xlsx");

			return file;
		}



		// archive 
		[HttpGet]
		[Authorize(Roles = "can view archive data")]
		[Route("monthly_archive_data")]
		public async Task<IActionResult> Monthly_Archive_Data([FromQuery] int month, [FromQuery] int year)
		{
			string name = EatTime.Now.ToString("yyyyMMddHHmmss");
			var data = new ArchiveDataDto { Month = month, Year = year }; // Create the DTO from the query parameters
			var result = await _archive.GetSalesTransactionsByMonth(data);

			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ArchiveDataDto{name}.xlsx");
		}

		[HttpGet]
		[Authorize(Roles = "can view archive data")]
		[Route("day_archive_data")]
		public async Task<IActionResult> Day_Archive_Data(DateTime date)
		{
			string name = EatTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(" ", "");
			var result = await _archive.GetSalesTransactionsDate(date);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ArchiveDataDto{name}.xlsx");
		}

		[HttpPost]
		[Authorize(Roles = "can transfer wallet amount from one vehicle to another")]
		[Route("TransferWalletAmount")]
		public async Task<IActionResult> TransferWalletAmount(TransferCustomerBalanceDto balance)
		{
			var response = await _wallet.TransferCustomerBalance(balance);
			return CreateResponse(response);
		}


		//get payment Types
		[HttpGet]
		[Route("TopUpTypes")]
		[Authorize(Roles = "can view wallet top ups")]
		public async Task<IActionResult> TopUpTypes()
		{
			var response = await _wallet.TopUpTypes();
			return CreateResponse(response);
		}


		#region Customer Statement


		[HttpGet("Statement")]
		[Authorize]
		public async Task<IActionResult> GetStatement(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			if (string.IsNullOrWhiteSpace(customerCode))
				return BadRequest(new { message = "customerCode is required." });

			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statements.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			return Ok(new { responseObject = statement });
		}

		[HttpGet("Statement/Excel")]
		[Authorize]
		public async Task<IActionResult> DownloadStatementExcel(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statements.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			var bytes = _statements.BuildStatementExcel(statement);
			var fileName = $"Statement_{statement.CustomerCode}_{from:yyyyMMdd}_{to:yyyyMMdd}.xlsx";

			return File(
				bytes,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				fileName
			);
		}

		[HttpGet("Statement/Pdf")]
		[Authorize]
		public async Task<IActionResult> DownloadStatementPdf(
			[FromQuery] string customerCode,
			[FromQuery] DateTime? fromDate,
			[FromQuery] DateTime? toDate)
		{
			var from = fromDate ?? DateTime.UtcNow.AddMonths(-1);
			var to = toDate ?? DateTime.UtcNow;

			var statement = await _statements.GetCustomerStatementAsync(customerCode, from, to);
			if (statement == null)
				return NotFound(new { message = $"No customer found for code \"{customerCode}\"." });

			var bytes = _statements.BuildStatementPdf(statement);
			var fileName = $"Statement_{statement.CustomerCode}_{from:yyyyMMdd}_{to:yyyyMMdd}.pdf";

			return File(bytes, "application/pdf", fileName);
		}

		#endregion


		//[HttpGet]
		//[Route("receipt")]
		//public IActionResult Receipts(string customerName, string vehicleReg, DateTime date, string fuelType, double quantity, double pricePerLitre, string paymentMethod, string phoneNumber, string receipNumber)
		//{
		//	// Generate the HTML receipt content
		//	var htmlContent = _receipt.GenerateFuelReceiptPdf(customerName, vehicleReg, date, fuelType, quantity, pricePerLitre, paymentMethod, phoneNumber, receipNumber);

		//	if (string.IsNullOrWhiteSpace(htmlContent))
		//	{
		//		return BadRequest("An error occurred while generating the customer receipt.");
		//	}

		//	// Return the HTML content as a response
		//	return Content(htmlContent, "text/html");
		//}
		#region Loyalty
		//get all coupons
		[HttpGet]
		[Route("get-all-coupons")]
		public async Task<IActionResult> GetAllCouponsAsync()
		{
			var response = await _coupons.GetAllCouponsAsync();
			return CreateResponse(response);
		}
		//register subscriptions 
		[HttpPost]
		[Route("loyalty-program-subscription")]
		public async Task<IActionResult> GetAllCouponsAsync(CreateLoyaltySubscriptionDto createLoyalty)
		{
			var response = await _subscription.AddSubscriptionAsync(createLoyalty);
			return CreateResponse(response);
		}
		#endregion

		#region MyRegion
		[HttpGet("salesbypaymentmethod")]
		public async Task<IActionResult> GetSalesByPaymentMethodAsync()
		{
			var result = await _salesByPaymentMethod.GetSalesByPaymentMethodAsync();

			return CreateResponse(result);
		}

		[HttpGet("salespernozzle")]
		public async Task<IActionResult> GetSalesPerNozzleAsync()
		{
			var result = await _salesByPaymentMethod.GetSalesPerNozzleAsync();

			return CreateResponse(result);
		}
		#endregion
	}
}