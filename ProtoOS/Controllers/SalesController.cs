using BusinessLogic.DashBoard;
using BusinessLogic.Sales.Archive_data;
using BusinessLogic.Sales.MissingSales;
using BusinessLogic.Sales.Receipts;
using BusinessLogic.Sales.ReverseSales;
using BusinessLogic.Sales.Wallet;
using BussinessLogic.CouponsService;
using BussinessLogic.Sales.MissingSales;
using BussinessLogic.Sales.NewSales;
using BussinessLogic.Sales.SalesData;
using BussinessLogic.Sales.Wallet;
using DataAccessLayer.DTOs.Sales;
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
		private readonly Statements _statements;
		private readonly ILoyaltyProgramSubscription _subscription;
		private readonly ICouponsService _coupons;

		public SalesController(ISalesManagementService salesService, ISales sales, IWalletTransactions wallet,
			IDashBoard dashBoard, IMissingSales missing, IReverseSales reverse, IEmailService emailService,
			IMisingSale misingSale,Archive_Data archive,ReceiptService receipt,Statements statements, 
			ILoyaltyProgramSubscription loyaltyServices,ICouponsService coupons)
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
		}

		private IActionResult CreateResponse<T>(T response) => Ok(response);

		#region Sales Management Endpoints

		[HttpPost]
		[Route("AddSale")]
		[Authorize(Roles = "can add a sale")]
		public async Task<IActionResult> AddSale([FromBody] AddsaleDto sale)
		{
			var response = await _addingSales.AddSalesAsync(sale);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("ValidateVoucher")]
		public async Task<IActionResult> ValiDateVoucher(string VoucherNo)
		{
			var response = await _misingSale.ValidateVoucherAsync(VoucherNo);
			return CreateResponse(response);
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

		[HttpPost]
		[Route("SalesShiftSummarySummary")]
		[Authorize(Roles = "can view sales shift summary")]
		public async Task<IActionResult> SalesShiftSummarySummary()
		{
			var response = await _salesService.SalesShiftSummarySummary();
			return CreateResponse(response);
		}

		[HttpGet]
		[Authorize]
		[Route("MobileAppPaymentTypes")]
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
		

		//events for a specifi vehicle
		[HttpGet]
		[Route("GetFuelingEventsForVehicle/{vehicleCode}")]
		[Authorize(Roles = "can view fueling events for vehicle")]
		public async Task<IActionResult> GetFuelingEventsForVehicle(string vehicleCode)
		{
			var response = await _salesService.GetFuelingEventsForVehicle(vehicleCode);
			return CreateResponse(response);
		}

		#endregion
		#region Wallet Management Endpoints
		[HttpGet]
		[Authorize(Roles = "can view all customer balances")]
		[Route("GetAllCustomerBalances")]
		public async Task<IActionResult> GetAllCustomerBalances()
		{
			var response = await _wallet.GetAllCustomerBalances();
			return CreateResponse(response);
		}

		[HttpPost]
		[Authorize(Roles = "can view customer statement")]
		[Route("GetCustomerStatement/{vehicleCode}/{startDate}/{endDate}")]
		public async Task<IActionResult> GetCustomerStatement(string vehicleCode, DateTime startDate, DateTime endDate)
		{
			var response = await _wallet.GetCustomerStatement(vehicleCode, startDate, endDate);
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
		[Route("GetEmployeePrice")]
		[Authorize(Roles = "can view employee price")]
		public async Task<IActionResult> GetEmployeeAsync(string userCode)
		{
			var response = await _missing.GetEmployeeAsync(userCode);
			return CreateResponse(response);
		}

		[HttpPost]
		[Authorize(Roles = "can view customerBalances")]
		[Route("GetAllCustomerBalances2")]
		public async Task<IActionResult> GetAllCustomerBalances(string regNo)
		{
			var response = await _wallet.GetAllCustomerBalances();
			return CreateResponse(response);
		}



		[HttpPost]
		[Authorize(Roles = "can view customerbalances")]
		[Route("get-all-customer-balances-sql")]
		public async Task<IActionResult> GetAllCustomerBalancesSql(string? registrationNumber = null,
		string? customerName = null,
		int pageNumber = 1,
		int pageSize = 15)
		{
			var response = await _wallet.GetAllCustomerBalancesSql(registrationNumber,customerName,pageNumber,pageSize);
			return CreateResponse(response);
		}

		//upload
		[HttpPost]
		[Route("UploadSalesData")]
		[Consumes("multipart/form-data")]
		[Authorize(Roles = "can top up customer wallet")]
		public async Task<IActionResult> UploadSalesData(IFormFile file,int topUpType) 
		{
			var response = await _wallet.UploadCustomerTransactions(file, topUpType);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("export-customer-transactions/{vehicleCode}")]
		[Authorize(Roles = "can view customer statement")]
		public async Task<IActionResult> ExportCustomerTransactions(string vehicleCode)
		{
			var result = await _wallet.ExportCustomerTransactions(vehicleCode);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.xlsx");
		}

		[HttpGet]
		[Route("export-customer-transactions-pdf/{customerCode}")]
		[Authorize(Roles = "can view customer statement")]
		public async Task<IActionResult> ExportCustomerTransactionsPdf(string customerCode,DateTime from)
		{

			var result = await _wallet.CustomerStatementAsPdf(customerCode,from);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return NotFound("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.pdf");
		}

		[HttpGet]
		[Route("CustomerAllVehiclesStatement/{customerCode}")]
		[Authorize(Roles = "can view a vehicle statement")]
		public async Task<IActionResult> CustomerAllVehiclesStatement(string customerCode, DateTime from)
		{

			var result = await _wallet.CustomerStatement2(customerCode,from);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return NotFound("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.xlsx");
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
		[Route("export-customer-wallet-balances")]
		[Authorize(Roles = "can export customer wallet balances excel")]
		public async Task<IActionResult> ExportCustomerTransactions()
		{
			var result = await _salesService.ExportCustomerTransactions();
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return NotFound("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customer-wallet.xlsx");
		}

		[HttpGet]
		[Route("ViewPayments/{saleId}")]
		[Authorize(Roles = "can view payments")]
		public async Task<IActionResult> ViewPayments(string saleId)
		{
			var response = await _salesService.ViewPayments(saleId);
			return CreateResponse(response);
		}

		[HttpGet]
		[Route("ExportWalletStatement/{vehicleCode}")]
		[Authorize(Roles = "can view customer statement")]
		public async Task<IActionResult> ExportCustomerTransactionsEplus(string vehicleCode)
		{
			var result = await _wallet.ExportCustomerTransactionsEplus(vehicleCode);
			if (result.ResponseCode != 1)
			{

				return BadRequest(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.xlsx");
		}
		#endregion

		[HttpGet]
		[Route("GetSalesData")]
		[Authorize(Roles = "can view sales data")]
		public async Task<IActionResult> GetSalesData(DateTime date)
		{
			var response = await _salesService.GetSalesData(date);
			return CreateResponse(response);
		}

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
		
		[HttpGet]
		[Route("CustomerStatement/{customerCode}")]
		[Authorize(Roles = "can download customer statements")]
		public async Task<IActionResult> CustomerStatement(string customerCode)
		{
			var result = await _wallet.CustomerStatement(customerCode);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.xlsx");
		}
		// archive 
		[HttpGet]
		[Authorize(Roles = "can view archive data")]
		[Route("monthly_archive_data")]
		public async Task<IActionResult> Monthly_Archive_Data([FromQuery] int month, [FromQuery] int year)
		{
			string name = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
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
			string name = DateTime.UtcNow.ToString().Replace("/","").Replace("-","").Replace(" ","");
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

		[HttpGet]
		[Authorize(Roles = "can view wallet top ups")]
		[Route("WalletTopUps")]
		public async Task<IActionResult> WalletTopUps(DateTime dateFrom, DateTime dateTo)
		{
			string name = DateTime.UtcNow.ToString().Replace("/", "").Replace("-", "").Replace(" ", "");
			var result = await _misingSale.WalletTopUps(dateFrom, dateTo);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return BadRequest("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"WalletTopUps{name}.xlsx");
		}

		[HttpGet]
		[Route("customer-transactions-pdf/{customerCode}")]
		[Authorize(Roles = "can view transactions statement")]
		public async Task<IActionResult> CustomerTransactionsPdf(string customerCode, DateTime from)
		{

			var result = await _statements.CustomerStatementAsPdf(customerCode, from);
			if (result.ResponseCode != 1)
			{
				return NotFound(result.ResponseMessage);  // Return appropriate error response
			}

			var fileBytes = result.ResponseObject;
			if (fileBytes == null)
			{
				return NotFound("An error occurred while exporting the customer transactions");
			}
			return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerStatement.pdf");
		}
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

	}
}

