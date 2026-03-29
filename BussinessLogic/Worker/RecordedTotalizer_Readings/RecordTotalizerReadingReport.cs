using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Worker.SalesReport;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Worker.RecordedTotalizer_Readings
{
	public class RecordTotalizerReadingReport
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _Authentication;
		private readonly IEmailService _EmailService;

		public RecordTotalizerReadingReport(OTOContext context, IAuthCommonTasks authentication, IEmailService emailService)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_Authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
			_EmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
		}

		public async Task DailyTotalizerRecordings(Mails mails)
		{
			if (mails == null)
				throw new ArgumentNullException(nameof(mails), "Recipients not found");

			var connectionString = _context.Database.GetConnectionString();
			if (string.IsNullOrEmpty(connectionString))
				throw new InvalidOperationException("Database connection string is not configured.");

			_context.Database.SetCommandTimeout(300);

			var connection = _context.Database.GetDbConnection();
			if (connection.State == ConnectionState.Open)
				await connection.CloseAsync();

			try
			{
				await connection.OpenAsync();
				var dataTable = await FetchTotalizerDataAsync(connection);
				var stationSubtotals = CalculateStationSubtotals(dataTable);
				var htmlBody = BuildHtmlReport(dataTable, stationSubtotals);
				var emailSubject = $"{DateTime.UtcNow:dd-MMM-yyyy} Totalizer Recordings Report";

				using var excelStream = GenerateExcelFromDataTable(dataTable);
				await _EmailService.SendEmailWithExcelAttachmentAsync(
					mails.ToEmails?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? [],
					mails.CcEmails?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? [],
					DateTime.UtcNow,
					emailSubject,
					htmlBody,
					dataTable
				);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to generate daily totalizer recordings report", ex);
			}
			finally
			{
				if (connection.State == ConnectionState.Open)
					await connection.CloseAsync();
			}
		}

		private static MemoryStream GenerateExcelFromDataTable(DataTable dataTable)
		{
			using var workbook = new ClosedXML.Excel.XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Totalizer Readings");

			// Load the DataTable into the worksheet starting from cell A1
			worksheet.Cell(1, 1).InsertTable(dataTable, "TotalizerData", true);

			var stream = new MemoryStream();
			workbook.SaveAs(stream);
			stream.Position = 0; // Reset the stream position before returning
			return stream;
		}

		private static async Task<DataTable> FetchTotalizerDataAsync(DbConnection connection)
		{
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

			using var command = connection.CreateCommand();
			command.CommandTimeout = 5000;
			command.CommandText = sql;

			using var reader = await command.ExecuteReaderAsync();
			var dataTable = new DataTable();
			dataTable.Load(reader);
			return dataTable;
		}
		private static Dictionary<string, (string Name, decimal TodayTotal, decimal YesterdayTotal, decimal DifferenceTotal)>CalculateStationSubtotals(DataTable dataTable)
		{
			var subtotals = new Dictionary<string, (string Name, decimal TodayTotal, decimal YesterdayTotal, decimal DifferenceTotal)>();

			foreach (DataRow row in dataTable.Rows)
			{
				var stationCode = row["StationCode"]?.ToString() ?? "UNKNOWN";
				var stationName = row["StationName"]?.ToString() ?? "Unknown Station";
				var today = row["TodayReading"] != DBNull.Value ? Convert.ToDecimal(row["TodayReading"]) : 0;
				var yesterday = row["YesterdayReading"] != DBNull.Value ? Convert.ToDecimal(row["YesterdayReading"]) : 0;
				var diff = row["ReadingDifference"] != DBNull.Value ? Convert.ToDecimal(row["ReadingDifference"]) : 0;

				if (!subtotals.ContainsKey(stationCode))
					subtotals[stationCode] = (stationName, 0, 0, 0);

				var current = subtotals[stationCode];
				subtotals[stationCode] = (
					current.Name,
					current.TodayTotal + today,
					current.YesterdayTotal + yesterday,
					current.DifferenceTotal + diff
				);
			}

			return subtotals;
		}
		private static string BuildHtmlReport(DataTable dataTable, Dictionary<string, (string Name, decimal TodayTotal, decimal YesterdayTotal, decimal DifferenceTotal)> subtotals)
		{
			var html = new StringBuilder();
			html.AppendLine("<!DOCTYPE html>");
			html.AppendLine("<html>");
			html.AppendLine("<head>");
			html.AppendLine("<meta charset='UTF-8'>");
			html.AppendLine("<style>");
			html.AppendLine("body { font-family: 'Segoe UI', Roboto, sans-serif; background-color: #ffffff; color: #333; padding: 20px; font-size: 16px; }"); // Increased font size
			html.AppendLine("h2 { text-align: center; color: #2c3e50; margin-bottom: 30px; }");
			html.AppendLine("table { width: 100%; border-collapse: collapse; font-size: 16px; }"); // Increased font size
			html.AppendLine("th, td { padding: 12px 10px; border: 1px solid #ddd; text-align: left; }");
			html.AppendLine("th { background-color: #1e88e5; color: white; }");
			html.AppendLine("tr:nth-child(even) { background-color: #f9f9f9; }");
			html.AppendLine("tr:hover { background-color: #f1f1f1; }");
			html.AppendLine(".station-header { background-color: #f4f6f8; font-weight: bold; border-top: 2px solid #999; }");
			html.AppendLine(".subtotal { background-color: #e8f5e9; font-weight: bold; }");
			html.AppendLine(".grand-total { background-color: #dcedc8; font-weight: bold; font-size: 18px; }"); // Grand total style
			html.AppendLine("</style>");
			html.AppendLine("</head>");
			html.AppendLine("<body>");

			html.AppendLine($"<h2>Totalizer Recordings Report – {DateTime.UtcNow:MMMM dd, yyyy}</h2>");
			html.AppendLine("<table>");
			html.AppendLine("<thead><tr>");
			html.AppendLine("<th style='width: 20%'>Station</th>");
			html.AppendLine("<th style='width: 20%'>Dispenser</th>");
			html.AppendLine("<th style='width: 20%'>Nozzle</th>");
			html.AppendLine("<th style='width: 13%'>Today Reading</th>");
			html.AppendLine("<th style='width: 13%'>Yesterday Reading</th>");
			html.AppendLine("<th style='width: 14%'>Difference</th>");
			html.AppendLine("</tr></thead><tbody>");

			string? currentStation = null;
			decimal grandTodayTotal = 0;
			decimal grandYesterdayTotal = 0;
			decimal grandDifferenceTotal = 0;

			foreach (DataRow row in dataTable.Rows)
			{
				var stationCode = row["StationCode"]?.ToString() ?? "UNKNOWN";
				var stationName = row["StationName"]?.ToString() ?? "Unknown Station";

				if (stationCode != currentStation)
				{
					if (currentStation != null && subtotals.ContainsKey(currentStation))
					{
						var subtotal = subtotals[currentStation];
						html.AppendLine($"<tr class='subtotal'><td colspan='3'>Subtotal for {subtotal.Name}</td>");
						html.AppendLine("<td></td><td></td>");
						html.AppendLine($"<td>{subtotal.DifferenceTotal:N2}</td></tr>");

						grandTodayTotal += subtotal.TodayTotal;
						grandYesterdayTotal += subtotal.YesterdayTotal;
						grandDifferenceTotal += subtotal.DifferenceTotal;
					}

					html.AppendLine($"<tr class='station-header'><td colspan='6'>Station: {stationName}</td></tr>");
					currentStation = stationCode;
				}

				html.AppendLine("<tr>");
				html.AppendLine($"<td>{stationName}</td>");
				html.AppendLine($"<td>{row["DispenserName"]?.ToString() ?? "N/A"}</td>");
				html.AppendLine($"<td>{row["NozzleName"]?.ToString() ?? "N/A"}</td>");
				html.AppendLine($"<td>{(row["TodayReading"] != DBNull.Value ? Convert.ToDecimal(row["TodayReading"]).ToString("N2") : "N/A")}</td>");
				html.AppendLine($"<td>{(row["YesterdayReading"] != DBNull.Value ? Convert.ToDecimal(row["YesterdayReading"]).ToString("N2") : "N/A")}</td>");
				html.AppendLine($"<td>{(row["ReadingDifference"] != DBNull.Value ? Convert.ToDecimal(row["ReadingDifference"]).ToString("N2") : "N/A")}</td>");
				html.AppendLine("</tr>");
			}

			// Final subtotal
			if (currentStation != null && subtotals.ContainsKey(currentStation))
			{
				var subtotal = subtotals[currentStation];
				html.AppendLine($"<tr class='subtotal'><td colspan='3'>Subtotal for {subtotal.Name}</td>");
				html.AppendLine("<td></td><td></td>");
				html.AppendLine($"<td>{subtotal.DifferenceTotal:N2}</td></tr>");

				grandTodayTotal += subtotal.TodayTotal;
				grandYesterdayTotal += subtotal.YesterdayTotal;
				grandDifferenceTotal += subtotal.DifferenceTotal;
			}

			// Grand total row
			html.AppendLine("<tr class='grand-total'>");
			html.AppendLine("<td colspan='3'>Grand Total</td>");
			html.AppendLine($"<td></td>");
			html.AppendLine($"<td></td>");
			html.AppendLine($"<td>{grandDifferenceTotal:N2}</td>");
			html.AppendLine("</tr>");

			html.AppendLine("</tbody></table>");
			html.AppendLine("</body></html>");

			return html.ToString();
		}

	}
}
