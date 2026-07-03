using BusinessLogic.SetupService;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.Db_Views;
using DataAccessLayer.EntityModels.Personal_Wallet;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.Helpers;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;
using Npgsql;
using NpgsqlTypes;
using OfficeOpenXml;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using static BussinessLogic.DashBoard.DashBoard;

namespace BussinessLogic.Sales.SalesData
{
	public class SalesManagementService : ISalesManagementService
	{

		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IAuthCommonTasks _authentication;
		private readonly IEmailService _emailService;
		private readonly ILogger<SalesManagementService> _logger;

		public SalesManagementService(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, IEmailService emailService, ILogger<SalesManagementService> logger)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_emailService = emailService;
			_logger = logger;
		}

		// get all paymenttypes 
		public async Task<ServiceResponse<object>> MobileAppPaymentTypes()
		{
			try
			{
				var response = new ServiceResponse<object>();

				var paymentTypes = await (from p in _context.PaymentTypes
										  where p.IsAppUsed == true
										  select new
										  {
											  p.PaymentTypeId,
											  p.PaymentTypeName,
											  Image = _setups.GetHostUrl() + "/PaymentTypesImages/" + p.PaymentTypeName.Replace(" ", "") + ".png"
										  }).ToListAsync();

				paymentTypes.Add(new { PaymentTypeId = 0, PaymentTypeName = "Mpesa STK", Image = _setups.GetHostUrl() + "/PaymentTypesImages/" + "Mpesastk.png" });
				if (paymentTypes.Count == 0)

					return ServiceResponse<object>.Information("No Payment Types Found", null);
				return ServiceResponse<object>.Success("Payment Types Found", paymentTypes.OrderBy(x => x.PaymentTypeName));
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while fetching payment types: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<object>> AllPaymentTypes()
		{
			try
			{
				var response = new ServiceResponse<object>();
				var paymentTypes = await (from p in _context.PaymentTypes
										  select new
										  {
											  p.PaymentTypeId,
											  p.PaymentTypeName
										  }).ToListAsync();
				if (paymentTypes.Count == 0)
					return ServiceResponse<object>.Information("No Payment Types Found", null);
				return ServiceResponse<object>.Success("Payment Types Found", paymentTypes.DistinctBy(x => x.PaymentTypeName).OrderBy(x => x.PaymentTypeId));
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error($"An error occurred while fetching payment types: {ex.Message}", null);
			}

		}
		public async Task<ServiceResponse<SalesPagedResult>> AllSales(
			string? stationCode,
			string? shiftNumber = null,
			string? dispenserName = null,
			string? nozzleName = null,
			string? paymentTypeName = null,
			DateTime? startDate = null,
			DateTime? endDate = null,
			int pageNumber = 1,
			int pageSize = 10,
			string? orderByColumn = null,
			bool isDescending = true)
		{
			try
			{
				var salesQuery = from q in _context.QuantityTransactions
								 join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
								 join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
								 join s in _context.Stations on d.StationCode equals s.StationCode
								 join pp in _context.PetroleumProducts on n.PetroleumCode equals pp.PetroleumCode
								 join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
								 select new SaleTransactionDto
								 {
									 StationName = s.StationName,
									 NozzleCode = q.NozzleCode,
									 Quantity = q.QuantityCredit == 0 ? -q.QuantityDebit : q.QuantityCredit,
									 VehicleRegistrationNumber = q.VehicleRegistrationNumber,
									 DispenserName = d.DispenserName,
									 NozzleName = n.NozzleName,
									 PaymentTypeName = p.PaymentTypeName,
									 SaleId = q.SaleId,
									 DateCreated = q.DateCreated,
									 ShiftNumber = q.ShiftNumber,
									 DispenserCode = d.DispenserCode,
									 StationCode = s.StationCode,
									 PetroleumName = pp.PetroleumName,
									 Amount = q.AmountCredit == 0 ? -q.AmountDebit : q.AmountCredit,
								 };

				salesQuery = salesQuery.AsNoTracking();

				salesQuery = salesQuery.Where(s => !s.StationName.Contains("TEST"));

				// Apply filters
				if (!string.IsNullOrEmpty(stationCode))
					salesQuery = salesQuery.Where(q => q.StationCode == stationCode);

				if (!string.IsNullOrEmpty(shiftNumber))
					salesQuery = salesQuery.Where(q => q.ShiftNumber == shiftNumber);

				if (!string.IsNullOrEmpty(dispenserName))
					salesQuery = salesQuery.Where(q => q.DispenserName.Contains(dispenserName));

				if (!string.IsNullOrEmpty(nozzleName))
					salesQuery = salesQuery.Where(q => q.NozzleName.Contains(nozzleName));

				if (!string.IsNullOrEmpty(paymentTypeName))
					salesQuery = salesQuery.Where(q => q.PaymentTypeName.Contains(paymentTypeName));

				// If startDate and endDate are null, default to the current date
				if (!startDate.HasValue && !endDate.HasValue)
				{
					var currentDate = EatTime.Now;
					startDate = currentDate.AddDays(-3);
					endDate = currentDate;
				}

				if (startDate.HasValue && endDate.HasValue)
					salesQuery = salesQuery.Where(q => q.DateCreated >= startDate.Value && q.DateCreated <= endDate.Value);

				// Apply ordering
				if (!string.IsNullOrEmpty(orderByColumn))
				{
					salesQuery = orderByColumn switch
					{
						"StationName" => isDescending ? salesQuery.OrderByDescending(q => q.StationName) : salesQuery.OrderBy(q => q.StationName),
						"NozzleCode" => isDescending ? salesQuery.OrderByDescending(q => q.NozzleCode) : salesQuery.OrderBy(q => q.NozzleCode),
						"Quantity" => isDescending ? salesQuery.OrderByDescending(q => q.Quantity) : salesQuery.OrderBy(q => q.Quantity),
						"VehicleRegistrationNumber" => isDescending ? salesQuery.OrderByDescending(q => q.VehicleRegistrationNumber) : salesQuery.OrderBy(q => q.VehicleRegistrationNumber),
						"DispenserName" => isDescending ? salesQuery.OrderByDescending(q => q.DispenserName) : salesQuery.OrderBy(q => q.DispenserName),
						"NozzleName" => isDescending ? salesQuery.OrderByDescending(q => q.NozzleName) : salesQuery.OrderBy(q => q.NozzleName),
						"PaymentTypeName" => isDescending ? salesQuery.OrderByDescending(q => q.PaymentTypeName) : salesQuery.OrderBy(q => q.PaymentTypeName),
						"SaleId" => isDescending ? salesQuery.OrderByDescending(q => q.SaleId) : salesQuery.OrderBy(q => q.SaleId),
						"DateCreated" => isDescending ? salesQuery.OrderByDescending(q => q.DateCreated) : salesQuery.OrderBy(q => q.DateCreated),
						"ShiftNumber" => isDescending ? salesQuery.OrderByDescending(q => q.ShiftNumber) : salesQuery.OrderBy(q => q.ShiftNumber),
						"DispenserCode" => isDescending ? salesQuery.OrderByDescending(q => q.DispenserCode) : salesQuery.OrderBy(q => q.DispenserCode),
						"StationCode" => isDescending ? salesQuery.OrderByDescending(q => q.StationCode) : salesQuery.OrderBy(q => q.StationCode),
						"Amount" => isDescending ? salesQuery.OrderByDescending(q => q.Amount) : salesQuery.OrderBy(q => q.Amount),
						_ => salesQuery.OrderByDescending(q => q.DateCreated)
					};
				}
				else
				{
					salesQuery = salesQuery.OrderByDescending(q => q.DateCreated);
				}

				// Get total count before pagination
				var totalRecords = await salesQuery.CountAsync();

				// Apply pagination
				var sales = await salesQuery
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				var pagedResult = new SalesPagedResult
				{
					TotalRecords = totalRecords,
					PageNumber = pageNumber,
					PageSize = pageSize,
					Sales = sales
				};

				if (sales.Count == 0)
					return ServiceResponse<SalesPagedResult>.Information("No Sales Found", null);

				return ServiceResponse<SalesPagedResult>.Success("Sales Found", pagedResult);
			}
			catch (Exception ex)
			{
				return ServiceResponse<SalesPagedResult>.Error($"An error occurred while fetching sales: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<object>> GetPaymentTransactions(string transactionCode)
		{
			try
			{
				var response = new ServiceResponse<object>();

				var paymentTransactions = await (from p in _context.PaymentTransactions
												 where p.SaleId == transactionCode
												 select new
												 {
													 p.SaleId,
													 AmountCredit = p.TransactionAmount,
													 AmountDebit = p.TransactionAmountDebit,
													 p.PaymentRefrence,
													 p.DateCreated
												 }).ToListAsync();

				if (paymentTransactions.Count == 0)
					return ServiceResponse<object>.Information("No Payment Transactions Found", null);

				return ServiceResponse<object>.Success("Payment Transactions Found", paymentTransactions);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error($"An error occurred while fetching payment transactions: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<object>> GetSalesForShift(string shiftNumber, int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				var response = new ServiceResponse<object>();

				var sales = await (from q in _context.QuantityTransactions
								   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
								   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
								   join s in _context.Stations on d.StationCode equals s.StationCode
								   join v in _context.Vehicles on q.VehicleRegistrationNumber equals v.VehicleCode
								   join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
								   where q.ShiftNumber == shiftNumber
								   select new
								   {
									   s.StationName,
									   q.NozzleCode,
									   Quantity = q.QuantityCredit == 0 ? -q.QuantityDebit : q.QuantityCredit,
									   v.VehicleRegistrationNumber,
									   d.DispenserName,
									   n.NozzleName,
									   p.PaymentTypeName,
									   q.SaleId,
									   q.DateCreated,
									   q.ShiftNumber,
									   d.DispenserCode,
									   s.StationCode,
									   Amount = q.AmountCredit == 0 ? -q.AmountDebit : q.AmountCredit,
								   }).ToListAsync();

				if (sales.Count == 0)
					return ServiceResponse<object>.Information("No Sales Found", null);

				var pagedResult = new
				{
					TotalRecords = sales.Count,
					PageNumber = pageNumber,
					PageSize = pageSize,
					Sales = sales.Skip((pageNumber - 1) * pageSize).Take(pageSize)
				};

				return ServiceResponse<object>.Success("Sales Found", pagedResult);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error($"An error occurred while fetching sales: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<object>> ViewPayments(string saleId)
		{
			try
			{
				var response = new ServiceResponse<object>();

				var payments = await (from p in _context.PaymentTransactions
									  where p.SaleId == saleId
									  select new
									  {
										  p.SaleId,
										  AmountCredit = p.TransactionAmount,
										  AmountDebit = p.TransactionAmountDebit,
										  p.PaymentRefrence,
										  p.DateCreated
									  }).ToListAsync();

				if (payments.Count == 0)
					return ServiceResponse<object>.Information("No Payments Found", new object());

				return ServiceResponse<object>.Success("Payments Found", payments);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error($"An error occurred while fetching payments: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<object>> SalesPerShiftSummary()
		{
			try
			{
				var response = new ServiceResponse<object>();

				var salesSummary = await (from q in _context.QuantityTransactions
										  join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										  join s in _context.Stations on d.StationCode equals s.StationCode
										  join v in _context.Vehicles on q.VehicleRegistrationNumber equals v.VehicleCode
										  join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
										  group q by new { q.ShiftNumber, s.StationName } into g
										  select new
										  {
											  g.Key.StationName,
											  g.Key.ShiftNumber,
											  QuantitySold = g.Sum(x => x.QuantityCredit - x.QuantityDebit),
											  FuelingEvents = g.Count()
										  }).ToListAsync();

				if (salesSummary.Count == 0)
					return ServiceResponse<object>.Information("No Sales Summary Found", null);

				return ServiceResponse<object>.Success("Sales Summary Found", salesSummary);
			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<object>.Error($"An error occurred while fetching sales summary: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<byte[]>> ExportSalesReport(DateTime date)
		{
			_context.Database.SetCommandTimeout(300);

			// PostgreSQL with quoted column names (using double quotes)
			var sql = @"
        SELECT ""SaleId"", ""SalesDate"", ""TransId"", ""StationName"", ""Attendant_Name"" AS ""AttendantName"",
               ""CustomerName"", ""TillNumber"", ""StationName"" as ""Terminal"", ""ShiftNumber"", ""Vehicle"", 
               ""ProductName"", ""PaymentType"", ""Litres"", ""Price"", 0.00 as ""Discount"", ""Amount"", 
               ""DispenserName"", ""NozzleName"", ""StorageLocation"" 
        FROM ""vw_SalesData"" 
        WHERE ""SalesDate""::DATE = @p0";  // PostgreSQL cast syntax with quoted column

			var salesData = await _context.Set<OtopaySales>()
				.FromSqlRaw(sql, date.Date)
				.ToListAsync();

			if (salesData.Count == 0)
				return ServiceResponse<byte[]>.Information("No Sales Data Found", null);

			// Create a new workbook and worksheet
			var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add($"{date.Date:yy-MMMM-dd}_Report");

			// Define headers
			var headers = new string[]
			{
		"SaleId", "SalesDate", "TransId", "StationName", "AttendantName", "CustomerName",
		"TillNumber","Terminal","ShiftNumber", "Vehicle", "ProductName", "PaymentType", "Litres",
		"Price","Discount","Amount", "DispenserName", "NozzleName", "StorageLocation"
			};

			// Insert headers into the first row
			for (int i = 0; i < headers.Length; i++)
			{
				worksheet.Cell(1, i + 1).Value = headers[i];
			}

			// Populate data rows
			for (int i = 0; i < salesData.Count; i++)
			{
				worksheet.Cell(i + 2, 1).Value = salesData[i].SaleId;
				worksheet.Cell(i + 2, 2).Value = salesData[i].SalesDate;
				worksheet.Cell(i + 2, 3).Value = salesData[i].TransId;
				worksheet.Cell(i + 2, 4).Value = salesData[i].StationName;
				worksheet.Cell(i + 2, 5).Value = salesData[i].AttendantName;
				worksheet.Cell(i + 2, 6).Value = salesData[i].CustomerName;
				worksheet.Cell(i + 2, 7).Value = salesData[i].TillNumber;
				worksheet.Cell(i + 2, 8).Value = salesData[i].Terminal;
				worksheet.Cell(i + 2, 9).Value = salesData[i].ShiftNumber;
				worksheet.Cell(i + 2, 10).Value = salesData[i].Vehicle;
				worksheet.Cell(i + 2, 11).Value = salesData[i].ProductName;
				worksheet.Cell(i + 2, 12).Value = salesData[i].PaymentType;
				worksheet.Cell(i + 2, 13).Value = salesData[i].Litres;
				worksheet.Cell(i + 2, 14).Value = salesData[i].Price;
				worksheet.Cell(i + 2, 15).Value = salesData[i].Discount;
				worksheet.Cell(i + 2, 16).Value = salesData[i].Amount;
				worksheet.Cell(i + 2, 17).Value = salesData[i].DispenserName;
				worksheet.Cell(i + 2, 18).Value = salesData[i].NozzleName;
				worksheet.Cell(i + 2, 19).Value = salesData[i].StorageLocation;
			}

			// Create an Excel table
			var range = worksheet.Range(1, 1, salesData.Count + 1, headers.Length);
			var table = range.CreateTable();
			table.Theme = XLTableTheme.TableStyleLight13;
			table.SetEmphasizeFirstColumn(true);
			worksheet.Columns().AdjustToContents();

			// Auto-fit all columns
			worksheet.Columns().AdjustToContents();

			// Convert workbook to byte array
			using var stream = new MemoryStream();
			workbook.SaveAs(stream);

			var message = $"Sales Report for date {date} Exported Successfully by {_authentication.Name()} on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

			return ServiceResponse<byte[]>.Success("Sales Report Exported Successfully", stream.ToArray());
		}
		public async Task<ServiceResponse<byte[]>> MonthlySalesReport(int month, int year, CancellationToken ct = default)
		{
			// ─────────────────────────────────────────────────────────────────────────
			// Input validation
			// ─────────────────────────────────────────────────────────────────────────
			if (month < 1 || month > 12)
				return ServiceResponse<byte[]>.Information(
					"Invalid month provided. Must be between 1 and 12.", null);

			if (year < 2000 || year > DateTime.UtcNow.Year + 1)
				return ServiceResponse<byte[]>.Information(
					$"Invalid year provided. Must be between 2000 and {DateTime.UtcNow.Year + 1}.", null);

			const int MaxRows = 500_000;
			var originalTimeout = _context.Database.GetCommandTimeout();

			try
			{
				_context.Database.SetCommandTimeout(300);

				// ─────────────────────────────────────────────────────────────────────
				// Data fetch
				// ─────────────────────────────────────────────────────────────────────
				const string sql = @"
    SELECT
        ""SaleId"",
        ""SalesDate"",
        ""TransId"",
        ""StationName"",
        ""AttendantName"",
        ""CustomerName"",
        ""TillNumber"",
        ""ShiftNumber"",
        ""Vehicle"",
        ""PetroleumName""   AS ""ProductName"",
        ""PaymentType""     AS ""PaymentType"",
        ""Litres"",
        ""Price"",
        0.00                AS ""Discount"",
        ""Amount"",
        ""DispenserName"",
        ""NozzleName"",
        ""StorageLocation"",
        ""RunningBalance""
    FROM public.""vw_SalesData""
    WHERE ""SalesDate"" >= DATE_TRUNC('month', MAKE_DATE(@year, @month, 1))
      AND ""SalesDate"" <  DATE_TRUNC('month', MAKE_DATE(@year, @month, 1)) + INTERVAL '1 month'
    ORDER BY ""SaleId"";";

				var parameters = new[]
				{
			new NpgsqlParameter("@month", NpgsqlDbType.Integer) { Value = month },
			new NpgsqlParameter("@year",  NpgsqlDbType.Integer) { Value = year  },
		};

				// Keyless DTO — EF only maps the projected columns, no full-entity overhead.
				// AsNoTracking() is implicit on keyless types but stated explicitly for clarity.
				var salesData = await _context.Set<SalesReportRow>()
					.FromSqlRaw(sql, parameters)
					.AsNoTracking()
					.ToListAsync(ct);

				_logger.LogInformation("MonthlySalesReport: {Count} rows fetched for {Month}/{Year}.", salesData.Count, month, year);

				if (salesData.Count == 0)
					return ServiceResponse<byte[]>.Information("No Sales Data Found", null);

				if (salesData.Count > MaxRows)
				{
					_logger.LogWarning(
						"MonthlySalesReport: {Count} rows for {Month}/{Year} exceeds safety cap of {Max}.",
						salesData.Count, month, year, MaxRows);

					return ServiceResponse<byte[]>.Information(
						$"Report contains {salesData.Count:N0} rows which exceeds the export limit of {MaxRows:N0}. " +
						"Please contact your administrator.", null);
				}

				// ─────────────────────────────────────────────────────────────────────
				// Excel generation
				// ─────────────────────────────────────────────────────────────────────
				using var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add("Sales Report");

				var headers = new[]
				{
			"Sale ID", "Sales Date", "Transaction ID", "Station Name", "Attendant Name",
			"Customer Name", "Till Number", "Shift Number", "Vehicle", "Product Name",
			"Payment Type", "Litres", "Price", "Discount", "Sales Amount",
			"Dispenser Name", "Nozzle Name", "Storage Location", "Running Balance"
		};

				for (int i = 0; i < headers.Length; i++)
					worksheet.Cell(1, i + 1).Value = headers[i];

				// ─────────────────────────────────────────────────────────────────────
				// Row population — styles applied per-column after loop to avoid
				// per-cell style object allocation overhead on large row counts
				// ─────────────────────────────────────────────────────────────────────
				for (int i = 0; i < salesData.Count; i++)
				{
					var row = i + 2;
					var sale = salesData[i];

					worksheet.Cell(row, 1).Value = sale.SaleId;
					worksheet.Cell(row, 2).Value = sale.SalesDate;
					worksheet.Cell(row, 3).Value = sale.TransId ?? string.Empty;
					worksheet.Cell(row, 4).Value = sale.StationName ?? string.Empty;
					worksheet.Cell(row, 5).Value = sale.AttendantName ?? string.Empty;
					worksheet.Cell(row, 6).Value = sale.CustomerName ?? string.Empty;
					worksheet.Cell(row, 7).Value = sale.TillNumber ?? string.Empty;
					worksheet.Cell(row, 8).Value = sale.ShiftNumber ?? string.Empty;
					worksheet.Cell(row, 9).Value = sale.Vehicle ?? string.Empty;
					worksheet.Cell(row, 10).Value = sale.ProductName ?? string.Empty;
					worksheet.Cell(row, 11).Value = sale.PaymentType ?? string.Empty;
					worksheet.Cell(row, 12).Value = sale.Litres;
					worksheet.Cell(row, 13).Value = sale.Price;
					worksheet.Cell(row, 14).Value = sale.Discount;
					worksheet.Cell(row, 15).Value = sale.Amount;
					worksheet.Cell(row, 16).Value = sale.DispenserName ?? string.Empty;
					worksheet.Cell(row, 17).Value = sale.NozzleName ?? string.Empty;
					worksheet.Cell(row, 18).Value = sale.StorageLocation ?? string.Empty;
					worksheet.Cell(row, 19).Value = sale.RunningBalance;

				}

				// ─────────────────────────────────────────────────────────────────────
				// Post-loop column formatting — one style object per column, not per cell
				// ─────────────────────────────────────────────────────────────────────
				var dataRowCount = salesData.Count;

				// Date column — explicit format so Excel renders as datetime, not a serial number
				worksheet.Range(2, 2, dataRowCount + 1, 2)
						 .Style.NumberFormat.Format = "yyyy-MM-dd HH:mm:ss";

				// Numeric columns — 2 decimal places
				var numericCols = new[] { 12, 13, 14, 15, 19 };
				foreach (var col in numericCols)
					worksheet.Range(2, col, dataRowCount + 1, col)
							 .Style.NumberFormat.Format = "#,##0.00";

				// ─────────────────────────────────────────────────────────────────────
				// Table styling
				// ─────────────────────────────────────────────────────────────────────
				var range = worksheet.Range(1, 1, dataRowCount + 1, headers.Length);
				var table = range.CreateTable();
				table.Theme = XLTableTheme.TableStyleLight16;
				table.SetAutoFilter();

				// Fixed widths — AdjustToContents() is O(rows × cols), too slow on large sets
				var columnWidths = new double[]
				{
					18, 22, 18, 22, 22, 22, 14, 16, 18, 20,16, 12, 12, 12, 16, 20, 18, 20, 18
				};

				for (int i = 0; i < columnWidths.Length; i++)
					worksheet.Column(i + 1).Width = columnWidths[i];

				// ─────────────────────────────────────────────────────────────────────
				// Serialize to bytes
				// ─────────────────────────────────────────────────────────────────────
				byte[] reportBytes;
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					reportBytes = stream.ToArray();
				}

				// ─────────────────────────────────────────────────────────────────────
				// Audit trail
				// ─────────────────────────────────────────────────────────────────────
				var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);
				var message = $"Sales Report for {monthName} {year} exported successfully " +
								$"by {_authentication.Name()} on {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC";

				await _authentication.AddUserTrail(
					message, MethodBase.GetCurrentMethod()?.Name ?? "MonthlySalesReport");

				return ServiceResponse<byte[]>.Success("Sales Report Exported Successfully", reportBytes);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("MonthlySalesReport cancelled for {Month}/{Year}.", month, year);
				return ServiceResponse<byte[]>.Information("Report generation was cancelled.", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "MonthlySalesReport failed for {Month}/{Year}.", month, year);
				return ServiceResponse<byte[]>.Error(
					$"An error occurred while generating the sales report. Please try again or contact support. {ex.Message}", null);
			}
			finally
			{
				_context.Database.SetCommandTimeout(originalTimeout);
			}
		}



	}

}

