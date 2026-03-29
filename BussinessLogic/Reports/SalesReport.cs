using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Worker.SalesReport;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Messaging;
using DataAccessLayer.EntityModels.SetUps;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; // EPPlus namespace
using OfficeOpenXml.Table;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using static BussinessLogic.Messaging.BulkSms;
using TableStyles = OfficeOpenXml.Table.TableStyles;

public class SalesReportService
{
	private readonly OTOContext _context;
	private readonly IAuthCommonTasks _Authentication;
	private readonly IEmailService _EmailService;
	public SalesReportService(OTOContext context,IAuthCommonTasks authCommon, IEmailService emailService)
	{
		_context = context;
		_Authentication = authCommon;
		_EmailService = emailService;
	}
	public async Task GenerateDailySalesReportAsync(DateTime date,Mails mails)
	{
		// Ensure database connection string is properly set
		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
		{																										
			throw new InvalidOperationException("Database connection string is not configured.");
		}
		_context.Database.SetCommandTimeout(300);

		var sql = @"
        SELECT SaleId, SalesDate, TransId, StationName, Attendant_Name AS AttendantName,
               CustomerName, TillNumber, StationName AS Terminal, ShiftNumber, Vehicle, 
               ProductName, PaymentType, Litres, Price, 0.00 AS Discount, Amount, 
               DispenserName, NozzleName, StorageLocation 
        FROM vw_SalesData 
        WHERE CAST(SalesDate AS Date) = @ReportDate";
		
		DataTable dataTable = new();
		using (var connection = _context.Database.GetDbConnection())
		{
			await connection.OpenAsync(); // Open the connection explicitly
			using var command = connection.CreateCommand();
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			var parameter = command.CreateParameter();
			parameter.ParameterName = "@ReportDate";
			parameter.Value = date.Date;
			parameter.DbType = DbType.Date;
			command.Parameters.Add(parameter);

			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);
		}
	    var day = DateTime.UtcNow.Day.ToString() + GetOrdinalSuffix(DateTime.UtcNow.Day);
		var DateName = DateTime.UtcNow.ToString("MMMM yyyy");
		var Header = $"{day} {DateName}";

		// Generate Excel and send email
		using var excelStream = GenerateExcelFromDataTable(dataTable);
		await SendEmailWithAttachmentAsync(excelStream, $"SalesDailyReport_{date:yyyy_MM_dd}.xlsx",mails,$"{Header} Sales");
	}
	public async Task<ServiceResponse<object>> GenerateMonthlySalesReportAsync(int month, int year, Mails mails)
	{
		var recipients = mails ?? throw new ArgumentException("Recipients not found");

		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
		{
			return ServiceResponse<object>.Information("Database connection string is not configured.",null);
		}

		_context.Database.SetCommandTimeout(300);

		var sql = @"
		SELECT SaleId, SalesDate, TransId, StationName, Attendant_Name AS AttendantName,
			   CustomerName, TillNumber, StationName AS Terminal, ShiftNumber, Vehicle, 
			   ProductName, PaymentType, Litres, Price, 0.00 AS Discount, Amount, 
			   DispenserName, NozzleName, StorageLocation,IIF(Month(SalesDate) <= 7,0,RunningBalance) as RunningBalance 
		FROM vw_SalesData 
		WHERE MONTH(SalesDate) = @month AND YEAR(SalesDate) = @year
		ORDER BY Id";

		var dataTable = new DataTable();
		var connection = _context.Database.GetDbConnection();

		try
		{
			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandTimeout = 5000;
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			var monthParameter = command.CreateParameter();
			monthParameter.ParameterName = "@month";
			monthParameter.Value = month;
			monthParameter.DbType = DbType.Int16;
			command.Parameters.Add(monthParameter);

			var yearParameter = command.CreateParameter();
			yearParameter.ParameterName = "@year";
			yearParameter.Value = year;
			yearParameter.DbType = DbType.Int16;
			command.Parameters.Add(yearParameter);

			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);

			using var excelStream = GenerateExcelFromDataTable(dataTable);
			var info = excelStream.Length == 0 ? "No data found for the selected month." : string.Empty;
			if (!string.IsNullOrEmpty(info))
			{
				return ServiceResponse<object>.Information(info, null);
			}
			var fileName = $"SalesMonthlyReport_{year}_{month:D2}.xlsx";
			var reportPeriod = DateTime.UtcNow.ToString("MMMM yyyy");
			var body = SalesEmailBody($"{reportPeriod} Sales Report");
			await SendEmail(excelStream, fileName, recipients, $"{reportPeriod} Sales",body);

			var successMessage = $"Sales Report for {reportPeriod} has been successfully sent via email by {_Authentication.Name()}";
			return ServiceResponse<object>.Success(successMessage);
		}
		catch (Exception ex)
		{
			return ServiceResponse<object>.Error("An error occurred while generating the sales report.",ex.Message);
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
			{
				await connection.CloseAsync();
			}
		}
	}
	public async Task DailyTotalizerRecordings(Mails mails)
	{
		if (mails == null)
			throw new ArgumentNullException(nameof(mails), "Recipients not found");

		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Database connection string is not configured.");
		}

		_context.Database.SetCommandTimeout(300);

		var sql = @"  WITH TodayReadings AS (
      SELECT 
          st.StationName,
          st.StationCode,
          dp.DispenserName,
          nz.NozzleName,
          tr.Reading AS TodayReading,
          tr.NozzlesCode,
          tr.DateCreated
      FROM TotalizerReadings tr
      INNER JOIN Nozzles nz ON tr.NozzlesCode = nz.NozzleCode
      INNER JOIN Dispensers dp ON dp.DispenserCode = nz.DispenserCode
      INNER JOIN Stations st ON st.StationCode = dp.StationCode
      WHERE CAST(tr.DateCreated AS DATE) = CAST(GETDATE() AS DATE)
  ),
  RankedPreviousReadings AS (
    SELECT 
        tr.NozzlesCode,
        tr.Reading AS PreviousReading,
        ROW_NUMBER() OVER (PARTITION BY tr.NozzlesCode ORDER BY tr.DateCreated DESC) AS rn
    FROM TotalizerReadings tr
    WHERE CAST(tr.DateCreated AS DATE) < CAST(GETDATE() AS DATE)
    AND tr.Reading IS NOT NULL
)
SELECT 
    t.StationCode,
    t.StationName,
    t.DispenserName,
    t.NozzleName,
    t.TodayReading,
    p.PreviousReading AS YesterdayReading,
    CASE 
        WHEN t.TodayReading IS NOT NULL AND p.PreviousReading IS NOT NULL 
        THEN t.TodayReading - p.PreviousReading
        ELSE NULL
    END AS ReadingDifference
FROM TodayReadings t
LEFT JOIN RankedPreviousReadings p ON t.NozzlesCode = p.NozzlesCode AND p.rn = 1
WHERE t.TodayReading IS NOT NULL OR p.PreviousReading IS NOT NULL
ORDER BY t.StationName, t.DispenserName, t.NozzleName";

		var connection = _context.Database.GetDbConnection();
		if (connection.State == ConnectionState.Open)
		{
			await connection.CloseAsync();
		}

		try
		{
			await connection.OpenAsync();
			var dataTable = new DataTable();
			var htmlTable = new StringBuilder();

			using (var command = connection.CreateCommand())
			{
				command.CommandTimeout = 5000;
				command.CommandText = sql;

				using var reader = await command.ExecuteReaderAsync();
				dataTable.Load(reader);

				// Calculate subtotals per station with proper null handling
				var stationSubtotals = new Dictionary<string, (string Name, decimal TodayTotal, decimal YesterdayTotal, decimal DifferenceTotal)>();

				foreach (DataRow row in dataTable.Rows)
				{
					var stationCode = row["StationCode"]?.ToString() ?? "UNKNOWN";
					var stationName = row["StationName"]?.ToString() ?? "Unknown Station";
					var todayReading = row["TodayReading"] != DBNull.Value ? Convert.ToDecimal(row["TodayReading"]) : 0;
					var yesterdayReading = row["YesterdayReading"] != DBNull.Value ? Convert.ToDecimal(row["YesterdayReading"]) : 0;
					var difference = row["ReadingDifference"] != DBNull.Value ? Convert.ToDecimal(row["ReadingDifference"]) : 0;

					if (!stationSubtotals.ContainsKey(stationCode))
					{
						stationSubtotals[stationCode] = (stationName, 0, 0, 0);
					}

					stationSubtotals[stationCode] = (
						stationName,0,
						0,
						stationSubtotals[stationCode].DifferenceTotal + difference
					);
				}

				// Build HTML table for email body
				htmlTable.Append("<html><body>");
				htmlTable.Append($"<h2>Totalizer Recordings Report - {DateTime.UtcNow:MMMM dd, yyyy}</h2>");
				htmlTable.Append("<table border='1' cellpadding='5' cellspacing='0' style='width:100%;'>");

				// Table headers
				htmlTable.Append("<thead><tr style='background-color:#4CAF50;color:white;'>");
				htmlTable.Append("<th>Station</th>");
				htmlTable.Append("<th>Dispenser</th>");
				htmlTable.Append("<th>Nozzle</th>");
				htmlTable.Append("<th>Today Reading</th>");
				htmlTable.Append("<th>Yesterday Reading</th>");
				htmlTable.Append("<th>Difference</th>");
				htmlTable.Append("</tr></thead>");

				// Table body
				htmlTable.Append("<tbody>");
				string? currentStationCode = null;

				foreach (DataRow row in dataTable.Rows)
				{
					var stationCode = row["StationCode"]?.ToString() ?? "UNKNOWN";
					var stationName = row["StationName"]?.ToString() ?? "Unknown Station";

					// Add station header and subtotal when station changes
					if (stationCode != currentStationCode)
					{
						if (currentStationCode != null && stationSubtotals.ContainsKey(currentStationCode))
						{
							// Add subtotal row for previous station
							var subtotal = stationSubtotals[currentStationCode];
							htmlTable.Append("<tr style='font-weight:bold; background-color:#f2f2f2;'>");
							htmlTable.Append($"<td colspan='3'>Subtotal for {subtotal.Name}</td>");
							htmlTable.Append($"<td></td>");
							htmlTable.Append($"<td></td>");
							htmlTable.Append($"<td>{subtotal.DifferenceTotal:N2}</td>");
							htmlTable.Append("</tr>");
						}

						// Add new station header
						htmlTable.Append($"<tr><td colspan='6' style='font-weight:bold; background-color:#e6e6e6;'>Station: {stationName}</td></tr>");
						currentStationCode = stationCode;
					}

					// Add data row
					htmlTable.Append("<tr>");
					htmlTable.Append($"<td>{stationName}</td>");
					htmlTable.Append($"<td>{row["DispenserName"]?.ToString() ?? "N/A"}</td>");
					htmlTable.Append($"<td>{row["NozzleName"]?.ToString() ?? "N/A"}</td>");
					htmlTable.Append($"<td>{(row["TodayReading"] != DBNull.Value ? Convert.ToDecimal(row["TodayReading"]).ToString("N2") : "N/A")}</td>");
					htmlTable.Append($"<td>{(row["YesterdayReading"] != DBNull.Value ? Convert.ToDecimal(row["YesterdayReading"]).ToString("N2") : "N/A")}</td>");
					htmlTable.Append($"<td>{(row["ReadingDifference"] != DBNull.Value ? Convert.ToDecimal(row["ReadingDifference"]).ToString("N2") : "N/A")}</td>");
					htmlTable.Append("</tr>");
				}

				// Add final subtotal
				if (currentStationCode != null && stationSubtotals.ContainsKey(currentStationCode))
				{
					var subtotal = stationSubtotals[currentStationCode];
					htmlTable.Append("<tr style='font-weight:bold; background-color:#f2f2f2;'>");
					htmlTable.Append($"<td colspan='3'>Subtotal for {subtotal.Name}</td>");
					htmlTable.Append($"<td>{subtotal.TodayTotal:N2}</td>");
					htmlTable.Append($"<td>{subtotal.YesterdayTotal:N2}</td>");
					htmlTable.Append($"<td>{subtotal.DifferenceTotal:N2}</td>");
					htmlTable.Append("</tr>");
				}

				// Add grand total if there are multiple stations
				if (stationSubtotals.Count > 1)
				{
					var grandTotal = (
						"",
						"",
						stationSubtotals.Values.Sum(x => x.DifferenceTotal)
					);

					htmlTable.Append("<tr style='font-weight:bold; background-color:#d9d9d9;'>");
					htmlTable.Append($"<td colspan='3'>GRAND TOTAL</td>");
					htmlTable.Append($"<td>{grandTotal.Item1:N2}</td>");
					htmlTable.Append($"<td>{grandTotal.Item2:N2}</td>");
					htmlTable.Append($"<td>{grandTotal.Item3:N2}</td>");
					htmlTable.Append("</tr>");
				}

				htmlTable.Append("</tbody></table>");
				htmlTable.Append("</body></html>");
			}

			using (var excelStream = GenerateExcelFromDataTable(dataTable))
			{
				var fileName = $"Totalizer_Recording_Report_{DateTime.UtcNow:yyyyMMdd}.xlsx";
				var emailSubject = $"{DateTime.UtcNow:dd-MMM-yyyy} Totalizer Recordings Report";

				await _EmailService.SendEmailWithExcelAttachmentAsync(
					mails.ToEmails?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>(),
					mails.CcEmails?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>(),
					DateTime.UtcNow,
					emailSubject,
					htmlTable.ToString(),
					dataTable
				);
			}
		}
		catch (Exception ex)
		{
			//_logger.LogError(ex, "Error generating daily totalizer recordings report");
			throw new ApplicationException("Failed to generate daily totalizer recordings report", ex);
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
			{
				await connection.CloseAsync();
			}
		}
	}
	public async Task SendInstallationCostReportAsync(Mails mails)
	{
		var emails = mails ?? throw new ArgumentException("Recipients not found");

		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
			throw new InvalidOperationException("Database connection string is not configured.");

		_context.Database.SetCommandTimeout(300);

		const string sql = @"
			SELECT 
				av.VehicleRegistrationNumber AS [Number Plate],
				CAST(av.ConversionDate AS DATE) AS [Conversion Date],
				COALESCE(SUM(sd.Litres), 0) AS Litres,
				sd.Price AS Price,
				COALESCE(SUM(sd.Amount), 0) AS [Amount],
				62.0 AS [Execss Per Litre],
				COALESCE(SUM(sd.Litres), 0) * 62 AS [Excess Collected],
				26400 AS [Install Cost],
				26400 - COALESCE(SUM(sd.Litres), 0) * 62 AS [Remaing Balance],
				CASE 
					WHEN 26400 > 0 THEN 
						(COALESCE(SUM(sd.Litres), 0) * 62.0) / 26400 * 100 
					ELSE 
						0 
				END AS [% Recovered]
			FROM Vehicles av
			LEFT JOIN vw_SalesData sd
				ON sd.Vehicle = av.VehicleRegistrationNumber 
				AND sd.ProductCode = '04' 
				AND sd.HasValue = 1
			WHERE av.ProductCode = '04'
			GROUP BY av.VehicleRegistrationNumber, sd.Price, CAST(av.ConversionDate AS DATE)
			ORDER BY CAST(av.ConversionDate AS DATE);";

		var dataTable = new DataTable();
		var connection = _context.Database.GetDbConnection();

		if (connection.State == ConnectionState.Open)
			await connection.CloseAsync();

		try
		{
			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandTimeout = 5000;
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);

			using var excelStream = GenerateExcelFromDataInstallationCost(dataTable);
			var fileName = $"InstallationReport_{DateTime.UtcNow:yyyyMMdd}.xlsx";
			var subject = $"Installation Cost Report - {DateTime.UtcNow:MMMM yyyy}";

			await SendCostRecoveryEmail(excelStream, fileName, emails, subject);
		}
		catch (Exception ex)
		{
			// Log exception or notify
			_ = ex.Message;
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
				await connection.CloseAsync();
		}
	}
	private static MemoryStream GenerateExcelFromDataInstallationCost(DataTable dataTable)
	{
		var stream = new MemoryStream();

		try
		{
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Installation Cost");

			// === Styling Colors ===
			var darkBlue = XLColor.FromArgb(0, 51, 102);
			var pink = XLColor.FromHtml("#FF66A3");
			var darkPink = XLColor.FromHtml("#D94A90");
			worksheet.TabColor = darkBlue;

			// === Insert RowNo Column at Position 1 ===
			dataTable.Columns.Add("RowNo", typeof(int)).SetOrdinal(0);
			for (int i = 0; i < dataTable.Rows.Count; i++)
				dataTable.Rows[i]["RowNo"] = i + 1;

			// === Title Row ===
			var titleRange = worksheet.Range(1, 1, 1, dataTable.Columns.Count);
			titleRange.Merge();
			titleRange.Value = "INSTALLATION COST RECOVERY";
			titleRange.Style.Font.SetBold();
			titleRange.Style.Font.SetFontSize(16);
			titleRange.Style.Font.SetFontColor(pink);
			titleRange.Style.Fill.SetBackgroundColor(darkBlue);
			titleRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
			titleRange.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
			worksheet.Row(1).Height = 30;

			// === Header Row ===
			int headerRowIndex = 2;
			for (int col = 0; col < dataTable.Columns.Count; col++)
			{
				var cell = worksheet.Cell(headerRowIndex, col + 1);
				cell.Value = dataTable.Columns[col].ColumnName;
				cell.Style.Font.SetBold();
				cell.Style.Font.SetFontColor(darkBlue);
				cell.Style.Fill.SetBackgroundColor(pink);
				cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
			}
			worksheet.SheetView.FreezeRows(2); // Freeze title + header

			// === Data Rows ===
			int dataStartRow = 3;
			for (int row = 0; row < dataTable.Rows.Count; row++)
			{
				for (int col = 0; col < dataTable.Columns.Count; col++)
				{
					var cell = worksheet.Cell(dataStartRow + row, col + 1);
					var value = dataTable.Rows[row][col];
					string colName = dataTable.Columns[col].ColumnName;

					if (value is DBNull)
					{
						cell.Value = "-";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
					else if (colName == "RowNo")
					{
						cell.Value = (int)value;
						cell.Style.NumberFormat.NumberFormatId = 1;
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
					else if (value is decimal or double or float or int)
					{
						cell.Value = Convert.ToDecimal(value);
						cell.Style.NumberFormat.Format = "_(#,##0.00_);(#,##0.00);-";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					}
					else if (value is DateTime dt)
					{
						cell.Value = dt;
						cell.Style.DateFormat.Format = "yyyy-MM-dd";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
					else
					{
						cell.Value = value.ToString();
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
				}
			}

			int dataEndRow = dataStartRow + dataTable.Rows.Count - 1;

			// === Zebra Striping ===
			for (int row = dataStartRow; row <= dataEndRow; row++)
			{
				if ((row - dataStartRow) % 2 == 1)
				{
					worksheet.Range(row, 1, row, dataTable.Columns.Count)
						.Style.Fill.SetBackgroundColor(XLColor.WhiteSmoke);
				}
			}

			// === Totals ===
			decimal totalLitres = 0, totalAmount = 0, totalExcess = 0, totalInstall = 0, totalRemain = 0, totalRecovered = 0;
			decimal? totalPrice = null, totalExcessPerLitre = null;

			foreach (DataRow row in dataTable.Rows)
			{
				totalLitres += row["Litres"] as decimal? ?? 0;
				totalAmount += row["Amount"] as decimal? ?? 0;
				totalExcess += row["Excess Collected"] as decimal? ?? 0;
			   totalInstall += row["Install Cost"] as decimal? ?? 0;
				totalRemain += row["Remaing Balance"] as decimal? ?? 0;
			 totalRecovered += row["% Recovered"] as decimal? ?? 0;

				if (totalPrice == null && row["Price"] != DBNull.Value)
					totalPrice = Convert.ToDecimal(row["Price"]);

				if (totalExcessPerLitre == null && row["Execss Per Litre"] != DBNull.Value)
					totalExcessPerLitre = Convert.ToDecimal(row["Execss Per Litre"]);
			}

			decimal avgRecovered = dataTable.Rows.Count > 0 ? totalRecovered / dataTable.Rows.Count : 0;

			// === Total Row ===
			int totalRow = dataEndRow + 1;
			worksheet.Row(totalRow).Height = 22;
			worksheet.Cell(totalRow, 1).Value = "TOTAL";
			worksheet.Cell(totalRow, 1).Style.Font.SetBold();

			for (int col = 1; col <= dataTable.Columns.Count; col++)
			{
				string colName = dataTable.Columns[col - 1].ColumnName;
				var cell = worksheet.Cell(totalRow, col);
				cell.Style.Font.SetBold();
				cell.Style.Font.SetFontColor(darkBlue);
				cell.Style.Fill.SetBackgroundColor(pink);

				if (colName == "RowNo")
				{
					cell.Value = "TOTAL";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				}
				else if (colName == "Litres")
				{
					cell.Value = totalLitres;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "Amount")
				{
					cell.Value = totalAmount;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "Excess Collected")
				{
					cell.Value = totalExcess;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "Install Cost")
				{
					cell.Value = totalInstall;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				
				else if (colName == "Remaining Balance")
				{
					cell.Value = totalRemain;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "Price")
				{
					cell.Value = totalPrice is null ? "-" : totalPrice;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "Execss Per Litre")
				{
					cell.Value = totalExcessPerLitre is null ? "-" : totalExcessPerLitre;
					cell.Style.NumberFormat.Format = "#,##0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else if (colName == "% Recovered")
				{
					cell.Value = avgRecovered;
					cell.Style.NumberFormat.Format = "0.00";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
				}
				else
				{
					cell.Value = "-";
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
				}
			}

			worksheet.Columns().AdjustToContents();
			workbook.SaveAs(stream);
			stream.Position = 0;
		}
		catch (Exception ex)
		{
			// Log or handle error
			Console.WriteLine("Excel generation error: " + ex.Message);
			throw; // Optionally rethrow or return null
		}

		return stream;
	}
	public async Task Above100(Mails mails)
	{
		var emails = mails ?? throw new ArgumentException("Recipients not found");
		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Database connection string is not configured.");
		}

		_context.Database.SetCommandTimeout(300);

					var sql = @"WITH MonthlyLitres AS (
	SELECT 
		Vehicle,
		CustomerName,
		SUM(CASE WHEN MONTH(SalesDate) = 1 THEN Litres ELSE 0 END) AS Jan_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 2 THEN Litres ELSE 0 END) AS Feb_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 3 THEN Litres ELSE 0 END) AS Mar_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 4 THEN Litres ELSE 0 END) AS Apr_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 5 THEN Litres ELSE 0 END) AS May_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 6 THEN Litres ELSE 0 END) AS Jun_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 7 THEN Litres ELSE 0 END) AS Jul_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 8 THEN Litres ELSE 0 END) AS Aug_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 9 THEN Litres ELSE 0 END) AS Sep_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 10 THEN Litres ELSE 0 END) AS Oct_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 11 THEN Litres ELSE 0 END) AS Nov_Litres,
		SUM(CASE WHEN MONTH(SalesDate) = 12 THEN Litres ELSE 0 END) AS Dec_Litres
	FROM vw_SalesData vs
	WHERE 
		YEAR(SalesDate) = 2025
		AND MONTH(SalesDate) BETWEEN 1 AND 12
		AND HasValue = 1
		AND Vehicle NOT LIKE '%WALK_IN%'
	GROUP BY 
		Vehicle, CustomerName
),
WithCurrentMonthLitres AS (
	SELECT *, 
		CASE MONTH(GETDATE())
			WHEN 1 THEN Jan_Litres
			WHEN 2 THEN Feb_Litres
			WHEN 3 THEN Mar_Litres
			WHEN 4 THEN Apr_Litres
			WHEN 5 THEN May_Litres
			WHEN 6 THEN Jun_Litres
			WHEN 7 THEN Jul_Litres
			WHEN 8 THEN Aug_Litres
			WHEN 9 THEN Sep_Litres
			WHEN 10 THEN Oct_Litres
			WHEN 11 THEN Nov_Litres
			WHEN 12 THEN Dec_Litres
		END AS CurrentMonthLitres
	FROM MonthlyLitres
),
FinalOrdered AS (
	SELECT *,
		ROW_NUMBER() OVER (ORDER BY CurrentMonthLitres DESC) AS RowNo
	FROM WithCurrentMonthLitres
	WHERE CurrentMonthLitres >= 100
)
SELECT RowNo,Vehicle,CustomerName as [Customer Name],Jan_Litres,Feb_Litres,Mar_Litres,Apr_Litres,
May_Litres,Jun_Litres,Jul_Litres,Aug_Litres,Sep_Litres,Oct_Litres,Nov_Litres,Dec_Litres  FROM FinalOrdered
ORDER BY RowNo;";

		DataTable dataTable = new();

		var connection = _context.Database.GetDbConnection();
		if (connection.State == ConnectionState.Open)
		{
			await connection.CloseAsync();
		}
		try
		{
			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandTimeout = 5000;
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);

			using var excelStream = GenerateExcelFromMonthlyLitres(dataTable);
			var fileName = $"Above100Report_{DateTime.UtcNow:MMMM}.xlsx";
			var DateName = DateTime.UtcNow.ToString("MMMM yyyy");
			var emailBody =  Above100EmailBody($"{DateName} Above 100");
			await SendEmail(excelStream, fileName, emails, $"{DateName} Above 100",emailBody);
		}
		catch (Exception ex)
		{
			// Log or handle the error
			_ = ex.Message;
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
			{
				await connection.CloseAsync();
			}
		}
	}
	private static string GetOrdinalSuffix(int day)
	{
		return day switch
		{
			1 or 21 or 31 => "st",
			2 or 22 => "nd",
			3 or 23 => "rd",
			_ => "th"
		};
	}
	public static MemoryStream GenerateExcelFromDataTable(DataTable dt,string sheetName = "SalesReport",string tableName = "ReportTable",TableStyles tableStyle = TableStyles.Light9)
	{
		if (dt == null || dt.Rows.Count == 0)
		{
			throw new ArgumentException("The DataTable is null or contains no data.");
		}

		var stream = new MemoryStream();

		try
		{
			using (var package = new ExcelPackage(stream))
			{
				// Add a new worksheet
				var worksheet = package.Workbook.Worksheets.Add(sheetName);

				// Load DataTable into worksheet
				worksheet.Cells["A1"].LoadFromDataTable(dt, true);

				// Define table range
				var tableRange = worksheet.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count];

				// Add table formatting
				var table = worksheet.Tables.Add(tableRange, tableName);
				table.ShowHeader = true;
				table.TableStyle = tableStyle;

				// Apply styling
				worksheet.Cells.Style.Font.Name = "Arial";
				worksheet.Cells.Style.Font.Size = 10;

				// Auto-fit columns
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

				// Save to stream
				package.Save();
			}

			// Reset stream position
			stream.Position = 0;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("An error occurred while generating the Excel file.", ex);
		}

		return stream;
	}
	private static MemoryStream GenerateExcelFromDataTable(DataTable dataTable)
	{
		using var package = new ExcelPackage();
		var worksheet = package.Workbook.Worksheets.Add("Above 100");
		// Load the data from the DataTable into the worksheet
		worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

		// Format the data as a table
		var tableRange = worksheet.Cells[1, 1, dataTable.Rows.Count + 1, dataTable.Columns.Count];
		var table = worksheet.Tables.Add(tableRange, "SalesReportTable");
		table.ShowHeader = true;
		table.TableStyle = TableStyles.Light9; // Choose a predefined Excel table style

		worksheet.Cells[2, 2, dataTable.Rows.Count + 1, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
 
		// Auto-fit columns for better visibility
		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
	
		// Save the Excel package to a memory stream
		var stream = new MemoryStream();
		package.SaveAs(stream);
		stream.Position = 0;
		return stream;
	}
	private static MemoryStream GenerateExcelFromMonthlyLitres(DataTable dataTable)
	{
		var stream = new MemoryStream();
		try
		{
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Monthly Litres");

			var darkBlue = XLColor.FromArgb(0, 51, 102);
			var pink = XLColor.FromHtml("#FF66A3");
			worksheet.TabColor = darkBlue;

			// Title Row
			var titleRange = worksheet.Range(1, 1, 1, dataTable.Columns.Count);
			titleRange.Merge();
			titleRange.Value = "MONTHLY LITRES (Above 100)";
			titleRange.Style.Font.SetBold();
			titleRange.Style.Font.SetFontSize(16);
			titleRange.Style.Font.SetFontColor(pink);
			titleRange.Style.Fill.SetBackgroundColor(darkBlue);
			titleRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
			titleRange.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
			worksheet.Row(1).Height = 30;

			// Header Row
			int headerRowIndex = 2;
			for (int col = 0; col < dataTable.Columns.Count; col++)
			{
				var cell = worksheet.Cell(headerRowIndex, col + 1);
				cell.Value = dataTable.Columns[col].ColumnName;
				cell.Style.Font.SetBold();
				cell.Style.Font.SetFontColor(darkBlue);
				cell.Style.Fill.SetBackgroundColor(pink);
				cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
			}
			worksheet.SheetView.FreezeRows(2);

			// Data Rows
			int dataStartRow = 3;
			for (int row = 0; row < dataTable.Rows.Count; row++)
			{
				for (int col = 0; col < dataTable.Columns.Count; col++)
				{
					var cell = worksheet.Cell(dataStartRow + row, col + 1);
					var value = dataTable.Rows[row][col];
					var colName = dataTable.Columns[col].ColumnName;

					if (value is DBNull)
					{
						cell.Value = "-";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
					else if (colName == "RowNo")
					{
						cell.Value = Convert.ToInt32(value);
						cell.Style.NumberFormat.NumberFormatId = 1;
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
					else if (value is decimal or double or float or int)
					{
						cell.Value = Convert.ToDecimal(value);
						cell.Style.NumberFormat.Format = "#,##0.00";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					}
					else
					{
						cell.Value = value.ToString();
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
				}
			}

			// Zebra Striping
			int dataEndRow = dataStartRow + dataTable.Rows.Count - 1;
			for (int row = dataStartRow; row <= dataEndRow; row++)
			{
				if ((row - dataStartRow) % 2 == 1)
				{
					worksheet.Range(row, 1, row, dataTable.Columns.Count)
						.Style.Fill.SetBackgroundColor(XLColor.WhiteSmoke);
				}
			}

			worksheet.Columns().AdjustToContents();
			workbook.SaveAs(stream);
			stream.Position = 0;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error generating Excel: {ex.Message}");
			throw;
		}

		return stream;
	}
	private async Task<DataTable> ExecuteQueryAsync(string sql, Dictionary<string, object> parameters)
	{
		DataTable dataTable = new();

		using (var connection = _context.Database.GetDbConnection())
		{
			using var command = connection.CreateCommand();
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			// Add parameters to the command
			foreach (var param in parameters)
			{
				var dbParameter = command.CreateParameter();
				dbParameter.ParameterName = param.Key;
				dbParameter.Value = param.Value;
				command.Parameters.Add(dbParameter);
			}

			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);
		}

		return dataTable;
	}
	private static async Task SendEmailWithAttachmentAsync(Stream excelStream, string filename, Mails recipient,string subject)
	{
		if (recipient is not null)
		{
			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <h2 style='color: #333;'>{subject}</h2>
                        <p>Dear Team,</p>
                        <p>
                            Please find attached the Sales Report for
                            <strong>{subject.Replace("Sales","")}</strong>.
                        </p>
                        <p>
                            The report provides a comprehensive overview of the sales trends and 
                            performance up to the specified date.
                        </p>
                        <p style='margin-top: 20px;'>
                            <strong>Note:</strong> This email is automatically generated by the system service.
                        </p>
                        <p>Best regards,<br>System Service</p>
                    </body>
                </html>"
			};

			mail.To.Add(recipient.ToEmails);
			mail.CC.Add(recipient.CcEmails);
		

			// Add attachment
			var attachment = new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			mail.Attachments.Add(attachment);

			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274"
			};

			using var smtp = new SmtpClient("smtp.office365.com")
			{ 
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred,
			};
			await smtp.SendMailAsync(mail);
		}
	}
	private string SalesEmailBody(string subject)
	{
		return $@"
		<html>
			<body style='font-family: Arial, sans-serif; line-height: 1.6;'>
				<h2 style='color: #2c3e50;'>{subject}</h2>
				<p>Dear Team,</p>
				<p>
					Please find attached the <strong>Sales Report</strong> for the period of 
					<strong>{subject.Replace("Sales","")}</strong>.
				</p>
				<p>
					The report provides a comprehensive overview of sales trends and performance up to the specified period.
				</p>
		
				<p style='margin-top: 20px; color: #7f8c8d;'>
					<strong>Note:</strong> This is an automated email sent by the reporting system.
				</p>
				<p>Best regards,<br>System Service</p>
			</body>
		</html>";
	}
	private static async Task SendEmailAsync(string subject, Mails recipient, decimal rental, decimal outRight)
	{
		if (recipient is not null)
		{
			
			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = Body(subject, rental, outRight)
			};

			mail.To.Add(recipient.ToEmails);
			mail.CC.Add(recipient.CcEmails);

			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274"
			};

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred
			};

			await smtp.SendMailAsync(mail);
		}
	}
	private static async Task SendCostRecoveryEmail(Stream excelStream, string filename, Mails recipient, string subject)
	{
		if (recipient is not null)
		{
			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"
			<html>
				<body style='font-family: Arial, sans-serif; line-height: 1.6;'>
					<h2 style='color: #2c3e50;'>{subject}</h2>
					<p>Dear Team,</p>
					<p>
						Please find attached the <strong>Cost Recovery Report</strong> for the period of 
						<strong>{subject.Replace("Installation Cost Recovery Report", "").Trim()}</strong>.
					</p>
					<p>
						This report provides a detailed overview of cost recovery performance, including 
						key insights into financial reconciliation across operational activities.
					</p>
				
					<p style='margin-top: 20px; color: #7f8c8d;'>
						<strong>Note:</strong> This is an automated email sent by the reporting system.
					</p>
					<p>Best regards,<br>System Service</p>
				</body>
			</html>"
			};

			// Add recipients
			if (!string.IsNullOrWhiteSpace(recipient.ToEmails))
			{
				foreach (var email in recipient.ToEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.To.Add(email.Trim());
				}
			}

			if (!string.IsNullOrWhiteSpace(recipient.CcEmails))
			{
				foreach (var email in recipient.CcEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.CC.Add(email.Trim());
				}
			}

			// Reset stream position and add attachment
			if (excelStream.CanSeek)
				excelStream.Position = 0;

			var attachment = new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			mail.Attachments.Add(attachment);

			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274" // Use secret manager or environment variables for security
			};

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred,
			};

			await smtp.SendMailAsync(mail);
		}
	}
	private static async Task TelematicSalesReport(Stream excelStream, string filename, Mails recipient, string subject)
	{
		if (recipient is not null)
		{
			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"
<html>
	<body style='font-family: Arial, sans-serif; line-height: 1.6;'>
		<h2 style='color: #2c3e50;'>{subject}</h2>
		<p>Dear Team,</p>
		<p>
			Please find attached the <strong>Telematic Fueling Report</strong> for the period of 
			<strong>{subject.Replace("Telematic Fueling Report", "").Trim()}</strong>.
		</p>
		<p>
			This report provides a detailed summary of fueling activity for vehicles equipped with the 
			<strong>Telematic System</strong>. These systems enable real-time tracking of where and when vehicles fuel.
		</p>
		<p>
			The attached report includes extracted fueling data from the telematic platform, helping monitor fuel usage, 
			and support operational efficiency and accountability.
		</p>
		<p style='margin-top: 20px; color: #7f8c8d;'>
			<strong>Note:</strong> This is an automated email sent by the reporting system.
		</p>
		<p>Best regards,<br>System Service</p>
	</body>
</html>"
			};


			// Add recipients
			if (!string.IsNullOrWhiteSpace(recipient.ToEmails))
			{
				foreach (var email in recipient.ToEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.To.Add(email.Trim());
				}
			}

			if (!string.IsNullOrWhiteSpace(recipient.CcEmails))
			{
				foreach (var email in recipient.CcEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.CC.Add(email.Trim());
				}
			}

			// Reset stream position and add attachment
			if (excelStream.CanSeek)
				excelStream.Position = 0;

			var attachment = new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			mail.Attachments.Add(attachment);

			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274" // Use secret manager or environment variables for security
			};

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred,
			};

			await smtp.SendMailAsync(mail);
		}
	}
	public static string Above100EmailBody(string subject)
	{

		var Body = $@"
			<html>
				<body style='font-family: Arial, sans-serif; line-height: 1.6;'>
					<h2 style='color: #2c3e50;'>{subject}</h2>
					<p>Dear Team,</p>
					<p>
						Please find attached the <strong>Above 100 Litres Sales Report</strong> for the period of 
						<strong>{subject.Replace("Sales", "").Replace("Above 100", "").Trim()}</strong>.
					</p>
					<p>
						This report highlights customers or vehicles whose sales exceeded 100 litres during the month.
						It is intended to provide insight into high-volume transactions for your review and analysis.
					</p>
					<p>
						If you have any questions or require additional details, please feel free to reach out to the reporting team.
					</p>
					<p style='margin-top: 20px; color: #7f8c8d;'>
						<strong>Note:</strong> This is an automated email sent by the system.
					</p>
					<p>Best regards,<br>System Service</p>
				</body>
			</html>";
		return Body;
	}
	private static async Task SendEmail(Stream excelStream, string filename, Mails recipient, string subject,string Body)
	{
		if (recipient is not null)
		{
			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"{Body}"
			};

			// Add recipients
			if (!string.IsNullOrWhiteSpace(recipient.ToEmails))
			{
				foreach (var email in recipient.ToEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.To.Add(email.Trim());
				}
			}

			if (!string.IsNullOrWhiteSpace(recipient.CcEmails))
			{
				foreach (var email in recipient.CcEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.CC.Add(email.Trim());
				}
			}

			// Reset stream position and add attachment
			if (excelStream.CanSeek)
				excelStream.Position = 0;

			var attachment = new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			mail.Attachments.Add(attachment);

			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274" // Use secret manager or environment variables for security
			};

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred,
			};

			await smtp.SendMailAsync(mail);
		}
	}
	private static string Body(string subject,decimal rental,decimal OutRight)
	{
	var	Body = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <h2 style='color: #333;'>{subject}</h2>
                        <p>Dear Team,</p>
                        <p>
                            The scheduled price change has been triggered and the new price is {rental:N2} Kshs for rental and {OutRight:N2} Kshs for OutRight </strong>.
                        </p>
                        <p>
                            Please review the relevant information and take necessary actions if required.
                        </p>
                        <p style='margin-top: 20px;'>
                            <strong>Note:</strong> This email is automatically generated by the system.
                        </p>
                        <p>Best regards,<br>System Service</p>
                    </body>
                </html>";
		return Body;
	}
	public async Task TelematicVehiclesSalesReport(Mails mails)
	{
		var emails = mails ?? throw new ArgumentException("Recipients not found");

		var connectionString = _context.Database.GetConnectionString();
		if (string.IsNullOrEmpty(connectionString))
			throw new InvalidOperationException("Database connection string is not configured.");

		_context.Database.SetCommandTimeout(300);

		const string sql = @"
WITH SalesData AS
(
    SELECT
        v.VehicleRegistrationNumber,
        CAST(COALESCE(v.ConversionDate, '1900-01-01') AS date) AS ConversionDate,
        MONTH(s.SalesDate) AS SaleMonth,
        CAST(s.Litres AS decimal(18,2)) AS Litres,
        c.CustomerName
    FROM Vehicles v
    LEFT JOIN vw_salesData s
        ON s.Vehicle = v.VehicleRegistrationNumber
       AND s.IsTelematicInstalled = 1
       AND s.SalesDate >= DATEFROMPARTS(YEAR(GETDATE()), 1, 1)
       AND s.SalesDate <  DATEFROMPARTS(YEAR(GETDATE()) + 1, 1, 1)
    INNER JOIN Customers c
        ON c.CustomerCode = v.CustomerCode
    WHERE v.IsTelematicInstalled = 1
),
PivotedData AS
(
    SELECT
        CustomerName,
        CAST(COALESCE(ConversionDate, '1900-01-01') AS date) AS ConversionDate,
        VehicleRegistrationNumber,
        ISNULL([1], 0)  AS Jan,
        ISNULL([2], 0)  AS Feb,
        ISNULL([3], 0)  AS Mar,
        ISNULL([4], 0)  AS Apr,
        ISNULL([5], 0)  AS May,
        ISNULL([6], 0)  AS Jun,
        ISNULL([7], 0)  AS Jul,
        ISNULL([8], 0)  AS Aug,
        ISNULL([9], 0)  AS Sep,
        ISNULL([10], 0) AS Oct,
        ISNULL([11], 0) AS Nov,
        ISNULL([12], 0) AS [Dec]
    FROM
    (
        SELECT CustomerName, VehicleRegistrationNumber, ConversionDate, SaleMonth, Litres
        FROM SalesData
    ) AS S
    PIVOT
    (
        SUM(Litres) FOR SaleMonth IN
        ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
    ) AS P
)
SELECT
    ROW_NUMBER() OVER (ORDER BY ConversionDate, VehicleRegistrationNumber) AS [No],
    CustomerName,
    ConversionDate,
    VehicleRegistrationNumber,
    Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, [Dec]
FROM PivotedData
ORDER BY ConversionDate, VehicleRegistrationNumber;";

		var dataTable = new DataTable();
		var connection = _context.Database.GetDbConnection();

		try
		{
			if (connection.State == ConnectionState.Open)
				await connection.CloseAsync();

			await connection.OpenAsync();

			using var command = connection.CreateCommand();
			command.CommandTimeout = 5000;
			command.CommandText = sql;
			command.CommandType = CommandType.Text;

			using var reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);

			// Ensure a Total column exists
			if (!dataTable.Columns.Contains("Total"))
				dataTable.Columns.Add("Total", typeof(decimal));

			// Boundaries by name (safer than magic numbers)
			int janIndex = dataTable.Columns.IndexOf("Jan");
			int decIndex = dataTable.Columns.IndexOf("Dec");
			int totalIndex = dataTable.Columns.IndexOf("Total");

			// Per-vehicle totals
			foreach (DataRow row in dataTable.Rows)
			{
				decimal rowTotal = 0m;
				for (int i = janIndex; i <= decIndex; i++)
				{
					if (row[i] != DBNull.Value)
						rowTotal += Convert.ToDecimal(row[i]);
				}
				row[totalIndex] = rowTotal;
			}

			// Grand total row
			var totalRow = dataTable.NewRow();
			totalRow["No"] = DBNull.Value;
			totalRow["CustomerName"] = "ALL CUSTOMERS";
			totalRow["VehicleRegistrationNumber"] = "TOTAL";
			totalRow["ConversionDate"] = DBNull.Value;

			// Sum each monthly column
			for (int i = janIndex; i <= totalIndex; i++)
			{
				decimal colSum = 0m;
				foreach (DataRow row in dataTable.Rows)
				{
					if (row[i] != DBNull.Value)
						colSum += Convert.ToDecimal(row[i]);
				}
				totalRow[i] = colSum;
			}
			dataTable.Rows.Add(totalRow);

			// Excel
			using var excelStream = GenerateExcelFromDataTelematicReport(dataTable);
			var fileName = $"TelematicFuelUsage_{DateTime.UtcNow:yyyyMMdd}.xlsx";
			var subject = $"Telematic Vehicles Fuel Consumption Report - {DateTime.UtcNow:MMMM yyyy}";

			await TelematicSalesReport(excelStream, fileName, emails, subject);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine($"Error generating/sending report: {ex.Message}");
			Console.Error.WriteLine(ex.StackTrace);
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
				await connection.CloseAsync();
		}
	}
	private static MemoryStream GenerateExcelFromDataTelematicReport(DataTable dataTable)
	{
		var stream = new MemoryStream();
		using var workbook = new XLWorkbook();
		var ws = workbook.Worksheets.Add("Telematic Fuel Usage");
		ws.TabColor = XLColor.DarkBlue;
		ws.TabSelected = true;

		int totalColumns = dataTable.Columns.Count;

		// Colors
		var titleBg = XLColor.FromArgb(0, 51, 102);
		var pinkBg = XLColor.FromHtml("#FF66A3");
		var darkBlueFont = XLColor.FromArgb(0, 51, 102);
		var whiteSmokeBg = XLColor.WhiteSmoke;
		var lightGrayBg = XLColor.LightGray;

		// Title row
		ws.Cell(1, 1).SetValue("TELEMATIC SALES REPORT");
		ws.Range(1, 1, 1, totalColumns).Merge();
		var title = ws.Range(1, 1, 1, totalColumns);
		title.Style.Font.FontSize = 20;
		title.Style.Font.Bold = true;
		title.Style.Font.FontColor = pinkBg;
		title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
		title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
		title.Style.Fill.BackgroundColor = titleBg;
		ws.Row(1).Height = 30;

		// Header row
		for (int c = 0; c < totalColumns; c++)
		{
			var cell = ws.Cell(2, c + 1);
			cell.SetValue(dataTable.Columns[c].ColumnName);
			cell.Style.Font.Bold = true;
			cell.Style.Font.FontColor = darkBlueFont;
			cell.Style.Fill.BackgroundColor = pinkBg;
			cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
			cell.Style.Font.FontName = "Calibri";
			cell.Style.Font.FontSize = 11;
		}

		int currentRow = 3;

		// Column indices by name (safer than magic numbers)
		int noIdx = dataTable.Columns.IndexOf("No");
		int custIdx = dataTable.Columns.IndexOf("CustomerName");
		int convIdx = dataTable.Columns.IndexOf("ConversionDate");
		int regIdx = dataTable.Columns.IndexOf("VehicleRegistrationNumber");
		int janIdx = dataTable.Columns.IndexOf("Jan");
		int decIdx = dataTable.Columns.IndexOf("Dec");
		int totIdx = dataTable.Columns.IndexOf("Total");

		// Write all data rows (including the appended grand total row)
		for (int r = 0; r < dataTable.Rows.Count; r++)
		{
			var row = dataTable.Rows[r];

			string vehicleReg = row[regIdx] == DBNull.Value ? "" : row[regIdx].ToString()!.Trim();
			string customer = row[custIdx] == DBNull.Value ? "" : row[custIdx].ToString()!.Trim();
			bool isTotalRow = vehicleReg.Equals("TOTAL", StringComparison.OrdinalIgnoreCase)
								|| customer.Equals("ALL CUSTOMERS", StringComparison.OrdinalIgnoreCase);

			for (int c = 0; c < totalColumns; c++)
			{
				var cell = ws.Cell(currentRow, c + 1);
				var raw = row[c];

				// Base style
				cell.Style.Font.FontName = "Calibri";
				cell.Style.Font.FontSize = 11;
				cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

				if (c == convIdx && raw != DBNull.Value && DateTime.TryParse(raw.ToString(), out var dt))
				{
					cell.SetValue((DateTime)dt);
					cell.Style.DateFormat.Format = "yyyy-MMM-dd";
				}
				else if ((c >= janIdx && c <= decIdx) || c == totIdx)
				{
					var val = raw == DBNull.Value ? 0m : Convert.ToDecimal(raw);
					cell.SetValue((decimal)val);
					cell.Style.NumberFormat.Format = "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)";
				}
				else if (c == noIdx && raw != DBNull.Value && int.TryParse(raw.ToString(), out var n))
				{
					cell.SetValue((int)n);
					cell.Style.NumberFormat.NumberFormatId = 1;
				}
				else
				{
					cell.SetValue(raw == DBNull.Value ? string.Empty : raw.ToString()!);
				}

				// Zebra striping on data rows (not the total)
				if (!isTotalRow && currentRow % 2 == 1)
					cell.Style.Fill.BackgroundColor = whiteSmokeBg;

				// Highlight the Total column
				if (c == totIdx)
				{
					cell.Style.Fill.BackgroundColor = titleBg;
					cell.Style.Font.FontColor = pinkBg;
					cell.Style.Font.Bold = true;
				}

				// Strong style for the grand total row
				if (isTotalRow)
				{
					cell.Style.Font.Bold = true;
					cell.Style.Font.FontColor = darkBlueFont;
					cell.Style.Fill.BackgroundColor = pinkBg;
				}
			}

			currentRow++;
		}

		// ---- Add CHANGE % (MoM) row based on the GRAND TOTAL row ----
		if (dataTable.Rows.Count > 0 && janIdx >= 0 && decIdx >= 0 && totIdx >= 0)
		{
			var grandTotal = dataTable.Rows[dataTable.Rows.Count - 1];

			var changeRow = currentRow;
			for (int c = 0; c < totalColumns; c++)
			{
				var cell = ws.Cell(changeRow, c + 1);

				// Base style for change row
				cell.Style.Font.FontName = "Calibri";
				cell.Style.Font.FontSize = 11;
				cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				cell.Style.Font.Bold = true;
				cell.Style.Font.FontColor = darkBlueFont;
				cell.Style.Fill.BackgroundColor = lightGrayBg;
				cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

				if (c == 0)
				{
					cell.SetValue(string.Empty);
					continue;
				}

				if (c == custIdx)
				{
					cell.SetValue("CHANGE % (MoM)");
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					continue;
				}

				if (c == convIdx || c == regIdx || c == noIdx)
				{
					cell.SetValue(string.Empty);
					continue;
				}

				if (c >= janIdx && c <= decIdx)
				{
					int monthOffset = c - janIdx; // Jan=0
					if (monthOffset == 0)
					{
						cell.SetValue("-");
					}
					else
					{
						decimal prev = grandTotal[janIdx + monthOffset - 1] == DBNull.Value
							? 0m : Convert.ToDecimal(grandTotal[janIdx + monthOffset - 1]);
						decimal curr = grandTotal[janIdx + monthOffset] == DBNull.Value
							? 0m : Convert.ToDecimal(grandTotal[janIdx + monthOffset]);

						if (prev != 0m)
						{
							var pct = (curr - prev) / prev; // decimal (e.g., 0.12 for 12%)
							cell.SetValue((decimal)pct);
							cell.Style.NumberFormat.Format = "0.00%";
						}
						else
						{
							cell.SetValue("-");
						}
					}
					continue;
				}

				if (c == totIdx)
				{
					cell.SetValue("-");
					continue;
				}

				cell.SetValue(string.Empty);
			}

			currentRow++;
		}

		ws.Columns().AdjustToContents();
		ws.SheetView.FreezeRows(2); // Freeze title + headers
		workbook.SaveAs(stream);
		stream.Position = 0;
		return stream;
	}

}
