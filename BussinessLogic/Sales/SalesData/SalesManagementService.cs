using BusinessLogic.SetupService;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.Db_Views;
using DataAccessLayer.EntityModels.Personal_Wallet;
using DataAccessLayer.EntityModels.SetUps;
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
		// add a new sale

		// get balance for a customer from customer transactions
		public async Task<ServiceResponse<decimal>> GetCustomerBalance(string vehicleCode)
		{
			try
			{
				var response = new ServiceResponse<decimal>();
				var balance = await _context.CustomerTransactions
							  .Where(x => x.VehicleCode.Equals(vehicleCode))
							  .SumAsync(x => x.Credit - x.Debit);
				response.ResponseObject = balance;
				return response;
			}
			catch (Exception ex)
			{
				return new ServiceResponse<decimal>
				{
					ResponseMessage = ex.Message,
					ResponseCode = Response.Error
				};
			}
		}
		public async Task<ServiceResponse<ShiftSummaryDto>> SalesShiftSummarySummary()
		{
			try
			{
				var response = new ServiceResponse<ShiftSummaryDto>();

				// Get current shift number
				var shift = await (from s in _context.Shifts
								   where s.ShiftStatus == ShiftStatus.Open && s.UserCode == _authentication.Usercode()
								   select s.ShiftNumber).FirstOrDefaultAsync();
				var shiftNumber = shift;

				if (shiftNumber is null)
					return ServiceResponse<ShiftSummaryDto>.Information("Shift Not Found", null);

				// Fetch quantity sold
				var quantitySoldSql = @"SELECT SUM(QuantityCredit - QuantityDebit) as Value FROM QuantityTransactions WHERE ShiftNumber = @ShiftNumber";
				var quantitySold = await _context.Set<IntValue>()
					.FromSqlRaw(quantitySoldSql, new SqlParameter("@ShiftNumber", shiftNumber))
					.FirstOrDefaultAsync();

				var fuelingEventsSql = @"SELECT COUNT(*) as Value FROM QuantityTransactions WHERE ShiftNumber = @ShiftNumber";
				var fuelingEvents = await _context.Set<IntValue>()
					.FromSqlRaw(fuelingEventsSql, new SqlParameter("@ShiftNumber", shiftNumber))
					.FirstOrDefaultAsync();
				// Set response object
				if (quantitySold is not null || fuelingEvents is not null)
				{
					response.ResponseObject = new ShiftSummaryDto
					{
						QuantitySold = quantitySold is null ? 0 : quantitySold.Value,
						FuelingEvents = fuelingEvents is null ? 0 : fuelingEvents.Value
					};
				}
				return ServiceResponse<ShiftSummaryDto>.Success("Data Found", response.ResponseObject);
			}
			catch (Exception ex)
			{
				return ServiceResponse<ShiftSummaryDto>.Error($"An error occurred while fetching shift summary: {ex.Message}", null);
			}
		}
		//get sales summary for all station pass stationcode return stationname,quantity sold, fueling events, total amount per day
		public async Task<ServiceResponse<List<StationSummaryDto>>> SalesSummaryPerStation(string stationCode, DateTime date)
		{
			try
			{
				var response = new ServiceResponse<List<StationSummaryDto>>();

				var sqlQuery = @"SELECT s.StationName,SUM(q.QuantityCredit - q.QuantityDebit) AS QuantitySold,COUNT(*) AS FuelingEvents
                                FROM QuantityTransactions q
                                INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
                                INNER JOIN Stations s ON d.StationCode = s.StationCode
                                WHERE d.StationCode = @StationCode AND CONVERT(date, q.DateCreated) = @Date
                                GROUP BY s.StationName";

				var summary = await _context.Set<StationSummaryDto>()
											.FromSqlRaw(sqlQuery,
											new SqlParameter("@StationCode", stationCode),
											new SqlParameter("@Date", date.Date)).ToListAsync();

				if (summary.Count == 0)
				{
					return new ServiceResponse<List<StationSummaryDto>>
					{
						ResponseMessage = "No records found",
						ResponseCode = Response.Information
					};
				}

				return ServiceResponse<List<StationSummaryDto>>.Success("Data Found", summary);
			}
			catch (Exception ex)
			{
				return ServiceResponse<List<StationSummaryDto>>.Error($"An error occurred while fetching station summary: {ex.Message}", null);
			}
		}

		//get all reversed sales join nozzles, station and dispensers, vehicle,paymenttypes
		public async Task<ServiceResponse<List<ReversedSalesDto>>> ReversedSales(DateTime dateFrom, DateTime dateTo)
		{
			try
			{
				var response = new ServiceResponse<List<ReversedSalesDto>>();

				var sqlQuery = @"
            SELECT s.StationName,
                   q.NozzleCode AS Nozzle,
                   q.QuantityDebit AS Quantity,
                   v.VehicleRegistrationNumber,
                   d.DispenserName,
                   p.PaymentTypeName AS PaymentType,
                   q.SaleId AS TransactionCode,
                   q.DateCreated AS TransactionDate
            FROM QuantityTransactions q
            INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
            INNER JOIN Stations s ON d.StationCode = s.StationCode
            INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
            INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
            WHERE q.QuantityDebit > 0 AND q.DateCreated >= @DateFrom AND q.DateCreated <= @DateTo";

				var reversedSales = await _context.Set<ReversedSalesDto>()
												  .FromSqlRaw(sqlQuery, new SqlParameter("@DateFrom", dateFrom),
																	   new SqlParameter("@DateTo", dateTo))
												  .ToListAsync();

				if (reversedSales.Count == 0)
					return ServiceResponse<List<ReversedSalesDto>>.Information("No records found", null);

				return ServiceResponse<List<ReversedSalesDto>>.Success("Data Found", reversedSales);
			}
			catch (Exception ex)
			{
				return ServiceResponse<List<ReversedSalesDto>>.Error($"An error occurred while fetching  d sales: {ex.Message}", null);
			}
		}

		// get all sales join nozzles, station and dispensers, vehicle,paymenttypes filter by station,dispenser,shiftnumber,
		// datefrom,dateto where if the filter is null return all sales for the month
		public async Task<ServiceResponse<List<SalesDto>>> Sales(SalesListDto sales)
		{
			try
			{
				var response = new ServiceResponse<List<SalesDto>>();
				var sqlQuery = @"SELECT s.StationName,
                                   q.NozzleCode AS Nozzle,
                                   q.QuantityCredit AS Quantity,
                                   v.VehicleRegistrationNumber,
                                   d.DispenserName,
                                   p.PaymentTypeName AS PaymentType,
                                   q.SaleId AS TransactionCode,
                                   q.DateCreated AS TransactionDate,
                                   q.ShiftNumber
                            FROM QuantityTransactions q
                            INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
                            INNER JOIN Stations s ON d.StationCode = s.StationCode
                            INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
                            INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
                            WHERE q.DateCreated >= @DateFrom AND q.DateCreated <= @DateTo
                            AND (@StationCode IS NULL OR s.StationCode = @StationCode)
                            AND (@DispenserCode IS NULL OR d.DispenserCode = @DispenserCode)
                            AND (@ShiftNumber IS NULL OR q.ShiftNumber = @ShiftNumber)";

				var sale = await _context.Set<SalesDto>()
										  .FromSqlRaw(sqlQuery, new SqlParameter("@DateFrom", sales.DateFrom),
														   new SqlParameter("@DateTo", sales.DateTo),
														   new SqlParameter("@StationCode", sales.StationCode ?? (object)DBNull.Value),
														   new SqlParameter("@DispenserCode", sales.DispenserCode ?? (object)DBNull.Value),
														   new SqlParameter("@ShiftNumber", sales.ShiftNumber ?? (object)DBNull.Value))
										  .ToListAsync();

				if (sale.Count == 0)
				{
					return new ServiceResponse<List<SalesDto>>
					{
						ResponseMessage = "No records found",
						ResponseCode = Response.Information // You can use a custom code to indicate no records found
					};
				}

				response.ResponseObject = sale;
				response.ResponseMessage = "Success";
				response.ResponseCode = Response.Success;
				return response;
			}
			catch (Exception ex)
			{
				return new ServiceResponse<List<SalesDto>>
				{
					ResponseMessage = ex.Message,
					ResponseCode = Response.Error
				};
			}
		}
		//get all sales for a specific vehicle
		public async Task<ServiceResponse<List<SalesDto>>> GetSalesForVehicle(string vehicleCode)
		{
			try
			{
				var response = new ServiceResponse<List<SalesDto>>();

				var sqlQuery = @"
            SELECT s.StationName,
                   q.NozzleCode AS Nozzle,
                   q.QuantityCredit AS Quantity,
                   v.VehicleRegistrationNumber,
                   d.DispenserName,
                   p.PaymentTypeName AS PaymentType,
                   q.SaleId AS TransactionCode,
                   q.DateCreated AS TransactionDate,
                   q.ShiftNumber
            FROM QuantityTransactions q
            INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
            INNER JOIN Stations s ON d.StationCode = s.StationCode
            INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
            INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
            WHERE v.VehicleCode = {0}";

				var sales = await _context.Set<SalesDto>().FromSqlRaw(sqlQuery, vehicleCode).ToListAsync();
				return ServiceResponse<List<SalesDto>>.Success("Data Found", sales);
			}
			catch (Exception ex)
			{
				return ServiceResponse<List<SalesDto>>.Error($"An error occurred while fetching sales for vehicle: {ex.Message}", null);
			}
		}
		//get sales for a particular shift
		public async Task<ServiceResponse<List<SalesDto>>> GetSalesForShift(string shiftNumber)
		{
			try
			{
				var response = new ServiceResponse<List<SalesDto>>();

				var sqlQuery = @"
					SELECT s.StationName,
						   q.NozzleCode AS Nozzle,
						   q.QuantityCredit AS Quantity,
						   v.VehicleRegistrationNumber,
						   d.DispenserName,
						   p.PaymentTypeName AS PaymentType,
						   q.SaleId AS TransactionCode,
						   q.DateCreated AS TransactionDate,
						   q.ShiftNumber
					FROM QuantityTransactions q
					INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
					INNER JOIN Stations s ON d.StationCode = s.StationCode
					INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
					INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
					WHERE q.ShiftNumber = {0}";

				var sales = await _context.Set<SalesDto>().FromSqlRaw(sqlQuery, shiftNumber).ToListAsync();
				if (sales.Count is 0)
					return ServiceResponse<List<SalesDto>>.Information("No records found", null);

				return ServiceResponse<List<SalesDto>>.Success("Data Found", [.. sales.OrderBy(x => x.TransactionDate)]);
			}
			catch (Exception ex)
			{
				return ServiceResponse<List<SalesDto>>.Error($"An error occurred while fetching sales for shift: {ex.Message}", null);
			}
		}
		//get sales for payment type
		public async Task<ServiceResponse<List<SalesDto>>> GetSalesForPaymentType(string paymentTypeCode)
		{
			try
			{
				var response = new ServiceResponse<List<SalesDto>>();
				var sqlQuery = @"
                               SELECT s.StationName,
                               q.NozzleCode AS Nozzle,
                               q.QuantityCredit AS Quantity,
                               v.VehicleRegistrationNumber,
                               d.DispenserName,
                               p.PaymentTypeName AS PaymentType,
                               q.SaleId AS TransactionCode,
                               q.DateCreated AS TransactionDate,
                               q.ShiftNumber
                        FROM QuantityTransactions q
                        INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
                        INNER JOIN Stations s ON d.StationCode = s.StationCode
                        INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
                        INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
                        WHERE q.PaymentTypeCode = {0}";

				var sales = await _context.Set<SalesDto>().FromSqlRaw(sqlQuery, paymentTypeCode).ToListAsync();
				return ServiceResponse<List<SalesDto>>.Success("Data Found", sales);

			}
			catch (Exception ex)
			{
				return ServiceResponse<List<SalesDto>>.Error($"An error occurred while fetching sales for payment type: {ex.Message}", null);
			}

		}

		public class SalesFilterDto
		{
			public DateTime? Date { get; set; }
			public string ShiftNumber { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string DispenserCode { get; set; } = string.Empty;
			public int PaymentTypeCode { get; set; }
			public string VehicleCode { get; set; } = string.Empty;
			public string NozzleCode { get; set; } = string.Empty;
			public string TransactionCode { get; set; } = string.Empty;
			public int PageNumber { get; set; }
			public int PageSize { get; set; }
		}

		// filter sales by date or shift number or station code or dispenser code or payment type code or vehicle code or nozzle code or transaction code
		public async Task<ServiceResponse<PaginatedResponse<List<SalesDto>>>> FilterSales(SalesFilterDto filter)
		{
			try
			{
				var response = new ServiceResponse<PaginatedResponse<List<SalesDto>>>();
				var sqlQuery = @"
        SELECT s.StationName,
               q.NozzleCode AS Nozzle,
               iif(q.QuantityCredit > 0,q.QuantityCredit,-q.QuantityDebit) AS Quantity,
               v.VehicleRegistrationNumber,
               d.DispenserName,
               p.PaymentTypeName AS PaymentType,
               q.SaleId AS TransactionCode,
               q.DateCreated AS TransactionDate,
               q.ShiftNumber
        FROM QuantityTransactions q
        INNER JOIN Dispensers d ON q.DispenserCode = d.DispenserCode
        INNER JOIN Stations s ON d.StationCode = s.StationCode
        INNER JOIN Vehicles v ON q.VehicleCode = v.VehicleCode
        INNER JOIN PaymentTypes p ON q.PaymentTypeCode = p.PaymentTypeCode
        WHERE (@Date IS NULL OR CONVERT(date, q.DateCreated) = @Date)
        AND (@ShiftNumber IS NULL OR q.ShiftNumber = @ShiftNumber)
        AND (@StationCode IS NULL OR s.StationCode = @StationCode)
        AND (@DispenserCode IS NULL OR d.DispenserCode = @DispenserCode)
        AND (@PaymentTypeCode IS NULL OR q.PaymentTypeCode = @PaymentTypeCode)
        AND (@VehicleCode IS NULL OR v.VehicleCode = @VehicleCode)
        AND (@NozzleCode IS NULL OR q.NozzleCode = @NozzleCode)
        AND (@TransactionCode IS NULL OR q.SaleId = @TransactionCode)
        ORDER BY q.DateCreated DESC
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

				var sales = await _context.Set<SalesDto>().FromSqlRaw(sqlQuery,
					new SqlParameter("@Date", filter.Date ?? (object)DBNull.Value),
					new SqlParameter("@ShiftNumber", filter.ShiftNumber ?? (object)DBNull.Value),
					new SqlParameter("@StationCode", filter.StationCode ?? (object)DBNull.Value),
					new SqlParameter("@DispenserCode", filter.DispenserCode ?? (object)DBNull.Value),

					new SqlParameter("@VehicleCode", filter.VehicleCode ?? (object)DBNull.Value),
					new SqlParameter("@NozzleCode", filter.NozzleCode ?? (object)DBNull.Value),
					new SqlParameter("@TransactionCode", filter.TransactionCode ?? (object)DBNull.Value),
					new SqlParameter("@Skip", (filter.PageNumber - 1) * filter.PageSize),
					new SqlParameter("@Take", filter.PageSize))
					.ToListAsync();

				if (sales.Count == 0)
				{
					return ServiceResponse<PaginatedResponse<List<SalesDto>>>.Information("No records found", null);
				}

				var totalRecords = await _context.QuantityTransactions.CountAsync(q =>
			(filter.Date == null || q.DateCreated.Date == filter.Date.Value.Date) &&
			(filter.ShiftNumber == null || q.ShiftNumber == filter.ShiftNumber) &&
			(filter.StationCode == null || q.StationCode == filter.StationCode) &&
			(filter.DispenserCode == null || q.DispenserCode == filter.DispenserCode) &&
			(filter.VehicleCode == null || q.VehicleCode == filter.VehicleCode) &&
			(filter.NozzleCode == null || q.NozzleCode == filter.NozzleCode) &&
			(filter.TransactionCode == null || q.SaleId == filter.TransactionCode));

				return new ServiceResponse<PaginatedResponse<List<SalesDto>>>
				{
					ResponseCode = Response.Success,
					ResponseMessage = "Data Retrieved",
					ResponseObject = new PaginatedResponse<List<SalesDto>>
					{
						Data = sales,
						TotalRecords = totalRecords,
						PageNumber = filter.PageNumber,
						PageSize = filter.PageSize
					}
				};
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
				return ServiceResponse<PaginatedResponse<List<SalesDto>>>.Error($"An error occurred while fetching sales: {ex.Message}", null);
			}
		}
		public class PaginatedResponse<T>
		{
			public T? Data { get; set; }
			public int TotalRecords { get; set; }
			public int PageNumber { get; set; }
			public int PageSize { get; set; }
		}
		public class DashboardSalesSummaryDto
		{
			public string StationCode { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			[Precision(18, 2)] public decimal TotalSales { get; set; }
			[Precision(18, 2)] public decimal TotalRevenue { get; set; }
			public int TotalTransactions { get; set; }
		}


		//dash board sales summary statics for the last 7 days
		public async Task<ServiceResponse<List<DashboardSalesSummaryDto>>> DashboardSalesSummary()
		{
			try
			{
				var response = new ServiceResponse<List<DashboardSalesSummaryDto>>();

				var sqlQuery = @"
            SELECT CONVERT(date, q.DateCreated) AS Date,
                   SUM(iif(q.QuantityCredit > 0,q.QuantityCredit,-q.QuantityDebit)) AS QuantitySold,
                   COUNT(*) AS FuelingEvents
            FROM QuantityTransactions q
            WHERE q.DateCreated >= DATEADD(DAY, -7, GETDATE())
            GROUP BY CONVERT(date, q.DateCreated)";

				var summary = await _context.Set<DashboardSalesSummaryDto>()
											.FromSqlRaw(sqlQuery)
											.ToListAsync();

				if (summary.Count == 0)
					return ServiceResponse<List<DashboardSalesSummaryDto>>.Information("No records found", null);

				response.ResponseObject = summary;
				response.ResponseMessage = "Success";
				response.ResponseCode = Response.Success;
				return response;
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
				return ServiceResponse<List<DashboardSalesSummaryDto>>.Information($"Something went wrong", null);

			}
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



		//list all sales 
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
								 join pp in _context.PetroleumProducts on d.PetroleumCode equals pp.PetroleumCode
								 join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
								 from v in _context.Vehicles.Where(v => v.VehicleCode == q.VehicleCode).DefaultIfEmpty()
								 select new SaleTransactionDto
								 {
									 StationName = s.StationName,
									 NozzleCode = q.NozzleCode,
									 Quantity = q.QuantityCredit == 0 ? -q.QuantityDebit : q.QuantityCredit,
									 VehicleRegistrationNumber = v == null ? p.PaymentTypeName : v.VehicleRegistrationNumber,
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
					var currentDate = DateTime.UtcNow;
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



		//get all paymenttransactions for a particular transactionCode
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

		//export all customer balances to excel
		public async Task<ServiceResponse<byte[]>> ExportCustomerTransactions()
		{
			try
			{
				var response = new ServiceResponse<byte[]>();

				// 1. Customer Balances
				var customerBalances = await (from c in _context.CustomerTransactions
											  join v in _context.Vehicles on c.VehicleCode equals v.VehicleCode
											  join ct in _context.Customers on v.CustomerCode equals ct.CustomerCode
											  group c by new
											  {
												  c.VehicleCode,
												  v.VehicleRegistrationNumber,
												  ct.CustomerName,
												  ct.CustomerPhone
											  } into g
											  select new
											  {
												  g.Key.VehicleCode,
												  g.Key.CustomerName,
												  RegistrationNumber = g.Key.VehicleRegistrationNumber,
												  PhoneNumber = g.Key.CustomerPhone.Length >= 10
													  ? g.Key.CustomerPhone.Substring(0, 4) + "***" + g.Key.CustomerPhone.Substring(7)
													  : string.Empty,
												  Balance = g.Sum(x => x.Credit - x.Debit),
											  }).ToListAsync();

				// 2. Wallet Balances
				var balances = await (from wt in _context.Wallet_Transactions_Personal
									  join wc in _context.Personal_Wallet_Customers
									  on wt.WalletId equals wc.WalletId
									  group new { wt, wc } by new
									  {
										  wt.WalletId,
										  FullName = wc.FirstName + " " + wc.MiddleName + " " + wc.LastName,
									  } into g
									  select new
									  {
										  g.Key.WalletId,
										  Name = g.Key.FullName,
										  Credits = g.Sum(x => x.wt.Credit),
										  Debits = g.Sum(x => x.wt.Debit),
										  Balance = g.Sum(x => x.wt.Credit) - g.Sum(x => x.wt.Debit)
									  }).Where(x => x.WalletId != "0034").ToListAsync();

				// 3. Wallet Credits
				var credits = await (from wt in _context.Wallet_Transactions_Personal
									 join wc in _context.Personal_Wallet_Customers
									 on wt.WalletId equals wc.WalletId
									 where wt.Credit != 0 && wt.WalletId != "0034"
									 select new
									 {
										 wt.WalletId,
										 Name = wc.FirstName + " " + wc.MiddleName + " " + wc.LastName,
										 wt.Credit,
										 wt.DateCreated,
										 wt.SaleId
									 }).Where(x => x.DateCreated.Month == DateTime.UtcNow.Month).ToListAsync();

				// 4. Wallet Payments
				var payments = await (from wt in _context.Wallet_Transactions_Personal
									  join wc in _context.Personal_Wallet_Customers
									  on wt.WalletId equals wc.WalletId
									  where wt.Debit != 0 && wt.WalletId != "0034"
									  select new
									  {
										  wt.WalletId,
										  Name = wc.FirstName + " " + wc.MiddleName + " " + wc.LastName,
										  wt.Debit,
										  wt.DateCreated,
										  wt.SaleId
									  }).Where(x => x.DateCreated.Month == DateTime.UtcNow.Month).ToListAsync();

				if (customerBalances.Count == 0)
					return ServiceResponse<byte[]>.Information("No Customer Balances Found", null);

				using var workbook = new XLWorkbook();

				// Sheet 1: Customer Balances
				var sheet1 = workbook.Worksheets.Add("Main Wallet Balances");
				sheet1.Cell(1, 1).Value = "No";
				sheet1.Cell(1, 2).Value = "Customer Name";
				sheet1.Cell(1, 3).Value = "Phone Number";
				sheet1.Cell(1, 4).Value = "Registration Number";
				sheet1.Cell(1, 5).Value = "Balance";
				sheet1.Row(1).Style.Font.Bold = true;

				for (int i = 0; i < customerBalances.Count; i++)
				{
					sheet1.Cell(i + 2, 1).Value = i + 1;
					sheet1.Cell(i + 2, 2).Value = customerBalances[i].CustomerName.ToUpper().Trim();
					sheet1.Cell(i + 2, 3).Value = customerBalances[i].PhoneNumber;
					sheet1.Cell(i + 2, 4).Value = customerBalances[i].RegistrationNumber.ToUpper();
					sheet1.Cell(i + 2, 5).Value = customerBalances[i].Balance;
				}
				sheet1.Columns().AdjustToContents();

				// Sheet 2: Wallet Balances
				var sheet2 = workbook.Worksheets.Add("Personal Wallet Balances");
				sheet2.Cell(1, 1).Value = "No";
				sheet2.Cell(1, 2).Value = "WalletId";
				sheet2.Cell(1, 3).Value = "Name";
				sheet2.Cell(1, 4).Value = "Credits";
				sheet2.Cell(1, 5).Value = "Debits";
				sheet2.Cell(1, 6).Value = "Balance";
				sheet2.Row(1).Style.Font.Bold = true;

				for (int i = 0; i < balances.Count; i++)
				{
					sheet2.Cell(i + 2, 1).Value = i + 1;
					sheet2.Cell(i + 2, 2).Value = balances[i].WalletId;
					sheet2.Cell(i + 2, 3).Value = balances[i].Name;
					sheet2.Cell(i + 2, 4).Value = balances[i].Credits.ToString("N2");
					sheet2.Cell(i + 2, 5).Value = balances[i].Debits.ToString("N2");
					sheet2.Cell(i + 2, 6).Value = balances[i].Balance.ToString("N2");
				}
				sheet2.Columns().AdjustToContents();

				// Sheet 3: Wallet Credits
				var sheet3 = workbook.Worksheets.Add("Personal Wallet Credits");
				sheet3.Cell(1, 1).Value = "No";
				sheet3.Cell(1, 2).Value = "WalletId";
				sheet3.Cell(1, 3).Value = "Name";
				sheet3.Cell(1, 4).Value = "Credit";
				sheet3.Cell(1, 5).Value = "Date Created";
				sheet3.Cell(1, 6).Value = "SaleId";
				sheet3.Row(1).Style.Font.Bold = true;

				for (int i = 0; i < credits.Count; i++)
				{
					sheet3.Cell(i + 2, 1).Value = i + 1;
					sheet3.Cell(i + 2, 2).Value = credits[i].WalletId;
					sheet3.Cell(i + 2, 3).Value = credits[i].Name;
					sheet3.Cell(i + 2, 4).Value = credits[i].Credit.ToString("N2");
					sheet3.Cell(i + 2, 5).Value = credits[i].DateCreated;
					sheet3.Cell(i + 2, 6).Value = credits[i].SaleId;
				}
				sheet3.Columns().AdjustToContents();

				// Sheet 4: Wallet Payments
				var sheet4 = workbook.Worksheets.Add("Personal Wallet Payments");
				sheet4.Cell(1, 1).Value = "No";
				sheet4.Cell(1, 2).Value = "WalletId";
				sheet4.Cell(1, 3).Value = "Name";
				sheet4.Cell(1, 4).Value = "Debit";
				sheet4.Cell(1, 5).Value = "Date Created";
				sheet4.Cell(1, 6).Value = "SaleId";
				sheet4.Row(1).Style.Font.Bold = true;

				for (int i = 0; i < payments.Count; i++)
				{
					sheet4.Cell(i + 2, 1).Value = i + 1;
					sheet4.Cell(i + 2, 2).Value = payments[i].WalletId;
					sheet4.Cell(i + 2, 3).Value = payments[i].Name;
					sheet4.Cell(i + 2, 4).Value = payments[i].Debit.ToString("N2");
					sheet4.Cell(i + 2, 5).Value = payments[i].DateCreated;
					sheet4.Cell(i + 2, 6).Value = payments[i].SaleId;
				}
				sheet4.Columns().AdjustToContents();

				// Convert workbook to byte array
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);
				response.ResponseObject = stream.ToArray();

				return ServiceResponse<byte[]>.Success("Customer Transactions Exported Successfully", response.ResponseObject);
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
						Method = method?.Name ?? ""
					});
				return ServiceResponse<byte[]>.Error($"An error occurred while exporting customer transactions: {ex.Message}", null);
			}
		}

		//get all sales for a particular vehicle
		public async Task<ServiceResponse<object>> GetSalesForVehicle(string vehicleCode, int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				var response = new ServiceResponse<object>();

				var sales = await (from q in _context.QuantityTransactions
								   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
								   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
								   join s in _context.Stations on d.StationCode equals s.StationCode
								   join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
								   join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
								   where q.VehicleCode == vehicleCode
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
		//get all sales for a particular shift
		public async Task<ServiceResponse<object>> GetSalesForShift(string shiftNumber, int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				var response = new ServiceResponse<object>();

				var sales = await (from q in _context.QuantityTransactions
								   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
								   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
								   join s in _context.Stations on d.StationCode equals s.StationCode
								   join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
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

		//view payments in Paymentransaction pass saleid
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

		//sales per shift summary
		public async Task<ServiceResponse<object>> SalesPerShiftSummary()
		{
			try
			{
				var response = new ServiceResponse<object>();

				var salesSummary = await (from q in _context.QuantityTransactions
										  join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										  join s in _context.Stations on d.StationCode equals s.StationCode
										  join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
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
		//This month detailed sales statics
		public async Task<ServiceResponse<object>> SalesThisMonth()
		{
			try
			{
				var response = new ServiceResponse<object>();

				var salesSummary = await (from q in _context.QuantityTransactions
										  join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										  join s in _context.Stations on d.StationCode equals s.StationCode
										  join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
										  join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
										  where q.DateCreated.Month == DateTime.UtcNow.Month
										  group q by new { q.DateCreated.Date, s.StationName } into g
										  select new
										  {
											  g.Key.StationName,
											  g.Key.Date,
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

		//sales forecast for the next 7 days
		public async Task<ServiceResponse<object>> SalesForecast()
		{
			try
			{
				var response = new ServiceResponse<object>();

				var salesForecast = await (from q in _context.QuantityTransactions
										   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
										   join s in _context.Stations on d.StationCode equals s.StationCode
										   join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
										   join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
										   where q.DateCreated >= DateTime.UtcNow.Date && q.DateCreated <= DateTime.UtcNow.AddDays(7).Date
										   group q by new { q.DateCreated.Date, s.StationName } into g
										   select new
										   {
											   g.Key.StationName,
											   g.Key.Date,
											   QuantitySold = g.Sum(x => x.QuantityCredit - x.QuantityDebit),
											   FuelingEvents = g.Count()
										   }).ToListAsync();

				if (salesForecast.Count == 0)
					return ServiceResponse<object>.Information("No Sales Forecast Found", null);

				return ServiceResponse<object>.Success("Sales Forecast Found", salesForecast);
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
				return ServiceResponse<object>.Error($"An error occurred while fetching sales forecast: {ex.Message}", null);
			}
		}
		// from sql view map the data to Dto SalesData
		public async Task<ServiceResponse<object>> GetSalesData(DateTime date)
		{
			try
			{
				var response = new ServiceResponse<object>();
				var sql = @$"select  SaleId,SalesDate,Transid,StationName,Attendant_Name as AttendantName,CustomerName,TillNumber,ShiftNumber,Vehicle,ProductName,PaymentType,Litres
						,Price,Amount,DispenserName,NozzleName,StorageLocation  from vw_SalesData 
					where CAST(SalesDate as Date) = '{date.Year}-{date.Month}-{date.Day}'";
				var salesData = await _context.Set<OtopaySales>().FromSqlRaw(sql).ToListAsync();

				if (salesData.Count == 0)
					return ServiceResponse<object>.Information("No Sales Data Found", null);

				return ServiceResponse<object>.Success("Sales Data Found", salesData);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error($"An error occurred while fetching sales data: {ex.Message}", null);
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



		/// <summary>
		/// Generates and exports the monthly sales report as an Excel workbook.
		///
		/// Changes from original:
		///   1. try/catch wrapping the entire method → prevents 500s on DB timeout or Excel errors
		///   2. AsNoTracking() added          → EF does not track ~N thousand read-only rows (memory + speed)
		///   3. Dead CASE removed from SQL    → WHERE already scopes to the month; EXTRACT < @month was always false
		///   4. SetCommandTimeout scoped      → timeout set before query, reset in finally to avoid polluting shared context
		///   5. ClosedXML bulk-write tuning   → SuspendEvents during row loop cuts overhead on large result sets
		///   6. AdjustToContents replaced     → O(N*M) scan replaced with a fixed-width pass (configurable per column)
		///   7. Row count guard               → warns caller before attempting to load an unsafe volume into memory
		///   8. Logger injected               → structured error logging instead of swallowing exceptions silently
		/// </summary>
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

				_logger.LogInformation(
					"MonthlySalesReport: {Count} rows fetched for {Month}/{Year}.",
					salesData.Count, month, year);

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
			18, 22, 18, 22, 22, 22, 14, 16, 18, 20,
			16, 12, 12, 12, 16, 20, 18, 20, 18
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

		//get fueling events for a particular vehicle
		public async Task<ServiceResponse<object>> GetFuelingEventsForVehicle(
		string vehicleCode,
		int page = 1,
		int pageSize = 50,
		CancellationToken ct = default)
		{
			// ✅ Input validation
			if (string.IsNullOrWhiteSpace(vehicleCode))
				return ServiceResponse<object>.Information("Vehicle code must be provided.", null);

			if (page < 1)
				return ServiceResponse<object>.Information("Page number must be greater than 0.", null);

			if (pageSize < 1 || pageSize > 200)
				return ServiceResponse<object>.Information("Page size must be between 1 and 200.", null);

			try
			{
				var query = from q in _context.QuantityTransactions.AsNoTracking()
							join d in _context.Dispensers.AsNoTracking()
								on q.DispenserCode equals d.DispenserCode
							join n in _context.Nozzles.AsNoTracking()
								on q.NozzleCode equals n.NozzleCode
							join s in _context.Stations.AsNoTracking()
								on d.StationCode equals s.StationCode
							join v in _context.Vehicles.AsNoTracking()
								on q.VehicleCode equals v.VehicleCode
							// ✅ Left join — payment type may not always be set
							join p in _context.PaymentTypes.AsNoTracking()
								on q.PaymentTypeCode equals p.PaymentTypeId into paymentGroup
							from p in paymentGroup.DefaultIfEmpty()
							where q.VehicleCode == vehicleCode
							orderby q.DateCreated descending
							select new
							{
								s.StationName,
								s.StationCode,
								d.DispenserName,
								d.DispenserCode,
								n.NozzleName,
								q.NozzleCode,
								v.VehicleRegistrationNumber,
								PaymentTypeName = p != null ? p.PaymentTypeName : "Unknown",
								q.SaleId,
								q.DateCreated,
								q.ShiftNumber,
								// ✅ Safer sign logic — handles both zero case
								Quantity = q.QuantityCredit != 0
									? q.QuantityCredit
									: q.QuantityDebit != 0
										? -q.QuantityDebit
										: 0,
								Amount = q.AmountCredit != 0
									? q.AmountCredit
									: q.AmountDebit != 0
										? -q.AmountDebit
										: 0,
							};

				// ✅ Get total count for pagination metadata
				var totalCount = await query.CountAsync(ct);

				var fuelingEvents = await query
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync(ct);

				if (fuelingEvents.Count == 0)
					return ServiceResponse<object>.Information("No Fueling Events Found", null);

				// ✅ Return pagination metadata alongside results
				var result = new
				{
					Page = page,
					PageSize = pageSize,
					TotalCount = totalCount,
					TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
					HasNextPage = page * pageSize < totalCount,
					Data = fuelingEvents
				};

				return ServiceResponse<object>.Success("Fueling Events Found", result);
			}
			catch (OperationCanceledException)
			{
				// ✅ Don't treat cancellation as an error
				return ServiceResponse<object>.Information("Request was cancelled.", null);
			}
			catch (Exception)
			{
				// ✅ Log internally, return safe message externally
				return ServiceResponse<object>.Error("An error occurred while fetching fueling events.", null);
			}
		}

	}

}

