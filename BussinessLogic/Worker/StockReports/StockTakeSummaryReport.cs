using Azure;
using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Worker.SalesReport;
using BussinessLogic.Sales.NewSales;
using ClosedXML.Excel;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts;
using DataAccessLayer.DTOs.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static BusinessLogic.SetupService.UserSetups;

namespace BussinessLogic.Worker.StockReports
{
	public class StockTakeSummaryReport
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;

		public StockTakeSummaryReport(IAuthCommonTasks authentication, OTOContext context)
		{
			_authentication = authentication;
			_context = context;
		}

		public async Task StockTakeSummariesReportAsync(Mails mails, int? year = null, int? month = null)
		{
			var emails = mails ?? throw new ArgumentException("Recipients not found");

			var connectionString = _context.Database.GetConnectionString();
			if (string.IsNullOrEmpty(connectionString))
				throw new InvalidOperationException("Database connection string is not configured.");

			_context.Database.SetCommandTimeout(300);

								// Main summary
								string sql = @"
											DECLARE @Year INT = 2025;
								DECLARE @Month INT = 8;

								IF @Year IS NULL SET @Year = YEAR(GETDATE());
								IF @Month IS NULL SET @Month = MONTH(GETDATE());

								DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1);
								DECLARE @EndDate   DATE = DATEADD(MONTH, 1, @StartDate);

								-- Subquery for Otopay Sales per Shift
								;WITH Otopay AS
								(
									SELECT 
										ShiftNumber,NozzleCode,
										SUM(Litres) AS [Otopay Sales],
										COUNT(*) AS [Events]
									FROM vw_SalesData
									GROUP BY ShiftNumber,NozzleCode
								)

								SELECT 
									sft.Id,
									st.ShiftNumber,
									LTRIM(RTRIM(ISNULL(asp.FirstName, '') + ' ' + ISNULL(asp.MiddName, '') + ' ' + ISNULL(asp.LastName, ''))) AS AttendantName,
									s.StationName AS [Station Name],
									d.DispenserName AS [Dispenser Name],
									n.NozzleName AS [Nozzle Name],
									st.OpeningReading AS [Opening Reading],
									st.ExpectedOpeningReading AS [Expected Opening Reading],
									st.OpeningVariance AS [Opening Variance],
									st.ClosingReading AS [Closing Reading],
									st.ExpectedClosingReading AS [Expected Closing Reading],
									st.ClosingVariance AS [Closing Variance],
									st.QuantitySold AS [Totalizer Sales],
									ISNULL(o.[Otopay Sales], 0) AS [Otopay Sales],
									ISNULL(o.[Events], 0) AS [Otopay Events],
									sft.ShiftStartTime AS [Shift Start Time],
									sft.ShiftEndTime AS [Shift End Time]
								FROM StockTakeSummaries st
								INNER JOIN Nozzles n ON n.NozzleCode = st.NozzleCode
								INNER JOIN Dispensers d ON d.DispenserCode = n.DispenserCode
								INNER JOIN Stations s ON s.StationCode = d.StationCode
								INNER JOIN AspNetUsers asp ON asp.UserCode = st.UserCode
								INNER JOIN Shifts sft ON sft.ShiftNumber = st.ShiftNumber
								LEFT JOIN Otopay o ON o.ShiftNumber = st.ShiftNumber and o.NozzleCode = st.NozzleCode
								WHERE st.DateCreated >= @StartDate
								  AND st.DateCreated < @EndDate
								  AND st.VarianceStatus IN (0, 2)
								ORDER BY sft.Id;
";

								// Variance sheet
								string varianceSql = @"
					DECLARE @Year INT = @YearParam;
					DECLARE @Month INT = @MonthParam;

					IF @Year IS NULL SET @Year = YEAR(GETDATE());
					IF @Month IS NULL SET @Month = MONTH(GETDATE());

					DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1);
					DECLARE @EndDate   DATE = DATEADD(MONTH, 1, @StartDate);

					SELECT 
						sft.Id AS ShiftId,
						st.ShiftNumber,
						n.NozzleName,
						d.DispenserName,
						s.StationName,
						LTRIM(RTRIM(ISNULL(au.FirstName, '') + ' ' + ISNULL(au.MiddName, '') + ' ' + ISNULL(au.LastName, ''))) AS AttendantName,
						COUNT(*) AS RecordCount,
						SUM(st.OpeningVariance) AS TotalOpeningVariance,
						SUM(st.ClosingVariance) AS TotalClosingVariance,
						SUM(st.QuantitySold) AS TotalQuantitySold
					FROM StockTakeSummaries st
					INNER JOIN Nozzles n ON n.NozzleCode = st.NozzleCode
					INNER JOIN Dispensers d ON d.DispenserCode = n.DispenserCode
					INNER JOIN Stations s ON s.StationCode = d.StationCode
					INNER JOIN AspNetUsers au ON au.UserCode = st.UserCode
					INNER JOIN Shifts sft ON sft.ShiftNumber = st.ShiftNumber
					WHERE st.DateCreated >= @StartDate
					  AND st.DateCreated < @EndDate
					  AND st.VarianceStatus = 2
					GROUP BY 
						n.NozzleName, 
						d.DispenserName, 
						s.StationName,
						st.ShiftNumber,
						sft.Id,
						LTRIM(RTRIM(ISNULL(au.FirstName, '') + ' ' + ISNULL(au.MiddName, '') + ' ' + ISNULL(au.LastName, '')))
					ORDER BY sft.Id;";


			var summariesTable = new DataTable();
			var varianceTable = new DataTable();
			var connection = _context.Database.GetDbConnection();

			try
			{
				if (connection.State == System.Data.ConnectionState.Open)
					await connection.CloseAsync();

				await connection.OpenAsync();

				// --- Run main summary query ---
				using (var command = connection.CreateCommand())
				{
					command.CommandText = sql;
					command.CommandType = System.Data.CommandType.Text;

					var pYear = command.CreateParameter();
					pYear.ParameterName = "@YearParam";
					pYear.Value = (object?)year ?? DBNull.Value;
					command.Parameters.Add(pYear);

					var pMonth = command.CreateParameter();
					pMonth.ParameterName = "@MonthParam";
					pMonth.Value = (object?)month ?? DBNull.Value;
					command.Parameters.Add(pMonth);

					using var reader = await command.ExecuteReaderAsync();
					summariesTable.Load(reader);
				}

				// --- Run variance query ---
				using (var command = connection.CreateCommand())
				{
					command.CommandText = varianceSql;
					command.CommandType = System.Data.CommandType.Text;

					var pYear = command.CreateParameter();
					pYear.ParameterName = "@YearParam";
					pYear.Value = (object?)year ?? DBNull.Value;
					command.Parameters.Add(pYear);

					var pMonth = command.CreateParameter();
					pMonth.ParameterName = "@MonthParam";
					pMonth.Value = (object?)month ?? DBNull.Value;
					command.Parameters.Add(pMonth);

					using var reader = await command.ExecuteReaderAsync();
					varianceTable.Load(reader);
				}

				// Generate Excel report
				using var excelStream = GenerateExcelFromDataStockTakeReport(summariesTable, varianceTable);
				var fileName = $"StockTakeSummaries_{DateTime.UtcNow:yyyyMMdd}.xlsx";
				var subject = $"OTOPay Stock Take Report - {DateTime.UtcNow:MMMM yyyy}";

				await StockTakeSummariesEmail(excelStream, fileName, emails, subject);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Error generating/sending report: {ex.Message}");
				Console.Error.WriteLine(ex.StackTrace);
				throw; // bubble up for observability
			}
			finally
			{
				if (connection.State == System.Data.ConnectionState.Open)
					await connection.CloseAsync();
			}
		}
		private static async Task StockTakeSummariesEmail(Stream excelStream, string filename, Mails recipient, string subject)
		{
			if (recipient is null) return;

			// NOTE: move these to secure config/Key Vault
			var networkCred = new NetworkCredential
			{
				UserName = "Reports@protoenergy.com",
				Password = "Tag50274"
			};

			using var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"
				<html>
				  <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
					<h2 style='color: #2c3e50;'>{subject}</h2>
					<p>Dear Team,</p>
					<p>Please find attached the <strong>Stock Take Summaries Report</strong> for <strong>{subject}</strong>.</p>
					<p>This report provides a detailed summary of stock take readings, variances, and sales quantities to help monitor inventory accuracy and support operational control.</p>
					<p style='margin-top: 20px; color: #7f8c8d;'><strong>Note:</strong> This is an automated email sent by the reporting system.</p>
					<p>Best regards,<br/>System Service</p>
				  </body>
				</html>"
			};

			// Recipients
			if (!string.IsNullOrWhiteSpace(recipient.ToEmails))
				foreach (var email in recipient.ToEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
					mail.To.Add(email.Trim());

			if (!string.IsNullOrWhiteSpace(recipient.CcEmails))
				foreach (var email in recipient.CcEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
					mail.CC.Add(email.Trim());

			// Reset stream position (attachment will own/close it)
			if (excelStream.CanSeek) excelStream.Position = 0;

			var attachment = new Attachment(excelStream, filename,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			mail.Attachments.Add(attachment);

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = networkCred,
			};

			await smtp.SendMailAsync(mail);
		}
		private static MemoryStream GenerateExcelFromDataStockTakeReport(DataTable dataTable, DataTable? varianceTable = null)
		{
			var stream = new MemoryStream();
			using var workbook = new XLWorkbook();

			// --- Sheet 1: Stock Take Summaries ---
			var worksheet = workbook.Worksheets.Add("Stock Take Summary");
			worksheet.TabColor = XLColor.DarkBlue;
			worksheet.TabSelected = true;

			var totalColumns = dataTable.Columns.Count;

			// Colors
			var titleBg = XLColor.FromArgb(0, 51, 102);      // Dark Blue
			var pinkBg = XLColor.FromHtml("#FF66A3");        // Pink
			var darkBlueFont = XLColor.FromArgb(0, 51, 102); // Dark Blue

			// Title
			var titleCell = worksheet.Cell(1, 1);
			titleCell.Value = "STOCK TAKE REPORT";
			worksheet.Range(1, 1, 1, totalColumns).Merge();
			titleCell.Style.Font.FontSize = 20;
			titleCell.Style.Font.FontColor = pinkBg;
			titleCell.Style.Font.Bold = true;
			titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
			titleCell.Style.Fill.BackgroundColor = titleBg;
			worksheet.Row(1).Height = 30;

			// Headers
			for (int col = 0; col < totalColumns; col++)
			{
				var cell = worksheet.Cell(2, col + 1);
				cell.Value = dataTable.Columns[col].ColumnName;
				cell.Style.Font.FontColor = darkBlueFont;
				cell.Style.Fill.BackgroundColor = pinkBg;
				cell.Style.Font.Bold = true;
				cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				cell.Style.Font.FontName = "Calibri";
				cell.Style.Font.FontSize = 11;
			}

			// Data
			for (int row = 0; row < dataTable.Rows.Count; row++)
			{
				int currentRow = worksheet.LastRowUsed().RowNumber() + 1;

				for (int col = 0; col < totalColumns; col++)
				{
					var cell = worksheet.Cell(currentRow, col + 1);
					var rawValue = dataTable.Rows[row][col];
					string? safeValue = rawValue == DBNull.Value ? string.Empty : rawValue.ToString();

					cell.Style.Font.FontName = "Calibri";
					cell.Style.Font.FontSize = 11;
					cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

					if (int.TryParse(safeValue, out var intValue))
					{
						cell.Value = intValue;
						cell.Style.NumberFormat.Format = "###0";
					}
					else if (decimal.TryParse(safeValue, out var numericValue))
					{
						cell.Value = numericValue;
						cell.Style.NumberFormat.Format = "#,##0.00";
					}
					else if (DateTime.TryParse(safeValue, out var date))
					{
						cell.Value = date;
						cell.Style.DateFormat.Format = "yyyy-MMM-dd HH:mm";
					}
					else
					{
						cell.Value = safeValue;
					}

					if (currentRow % 2 == 1)
						cell.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
				}
			}

			worksheet.Columns().AdjustToContents();
			worksheet.SheetView.FreezeRows(2);

			// --- Sheet 2: Variance ---
			if (varianceTable != null && varianceTable.Rows.Count > 0)
			{
				var wsVar = workbook.Worksheets.Add("Variance");
				wsVar.TabColor = XLColor.DarkBlue;

				var varCols = varianceTable.Columns.Count;

				// Title
				var vTitle = wsVar.Cell(1, 1);
				vTitle.Value = "VARIANCE SUMMARY";
				wsVar.Range(1, 1, 1, varCols).Merge();
				vTitle.Style.Font.FontSize = 18;
				vTitle.Style.Font.FontColor = pinkBg;
				vTitle.Style.Font.Bold = true;
				vTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				vTitle.Style.Fill.BackgroundColor = titleBg;
				wsVar.Row(1).Height = 26;

				// Headers
				for (int col = 0; col < varCols; col++)
				{
					var cell = wsVar.Cell(2, col + 1);
					cell.Value = varianceTable.Columns[col].ColumnName;
					cell.Style.Font.FontColor = darkBlueFont;
					cell.Style.Fill.BackgroundColor = pinkBg;
					cell.Style.Font.Bold = true;
					cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
					cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
					cell.Style.Font.FontName = "Calibri";
					cell.Style.Font.FontSize = 11;
				}

				// Data
				for (int row = 0; row < varianceTable.Rows.Count; row++)
				{
                    // Replace the following lines in GenerateExcelFromDataStockTakeReport:

                    // Before:
                    // int currentRow = wsVar.LastRowUsed().RowNumber() + 1;

                    // After:
                    int currentRow = wsVar.LastRowUsed() != null ? wsVar.LastRowUsed().RowNumber() + 1 : 3;

					for (int col = 0; col < varCols; col++)
					{
						var cell = wsVar.Cell(currentRow, col + 1);
						var rawValue = varianceTable.Rows[row][col];
						string? safeValue = rawValue == DBNull.Value ? string.Empty : rawValue.ToString();

						cell.Style.Font.FontName = "Calibri";
						cell.Style.Font.FontSize = 11;
						cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

						if (int.TryParse(safeValue, out var intValue))
						{
							cell.Value = intValue;
							cell.Style.NumberFormat.Format = "###0";
						}
						else if (decimal.TryParse(safeValue, out var numericValue))
						{
							cell.Value = numericValue;
							cell.Style.NumberFormat.Format = "#,##0.00";
						}
						else if (DateTime.TryParse(safeValue, out var date))
						{
							cell.Value = date;
							cell.Style.DateFormat.Format = "yyyy-MMM-dd HH:mm";
						}
						else
						{
							cell.Value = safeValue;
						}

						if (currentRow % 2 == 1)
							cell.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
					}
				}

				wsVar.Columns().AdjustToContents();
				wsVar.SheetView.FreezeRows(2);
			}

			workbook.SaveAs(stream);
			stream.Position = 0;
			return stream;
		}
	}
}
