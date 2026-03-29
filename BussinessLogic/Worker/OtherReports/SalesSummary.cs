namespace BussinessLogic.Worker.OtherReports
{
	using BusinessLogic.Worker.SalesReport;
	using ClosedXML.Excel;
	using DataAccessLayer.Context;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Data;
	using System.IO;
	using System.Net;
	using System.Net.Mail;
	using System.Text;

	/// <summary>
	/// Defines the <see cref="SalesReport_Summary" />
	/// </summary>
	public class SalesReport_Summary
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly OTOContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="SalesReport_Summary"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="OTOContext"/></param>
		public SalesReport_Summary(OTOContext context)
		{
			_context = context;
		}

		/// <summary>
		/// The GenerateMonthlyStationReportsToStream
		/// </summary>
		/// <param name="year">The year<see cref="int"/></param>
		/// <param name="month">The month<see cref="int"/></param>
		/// <returns>The <see cref="MemoryStream"/></returns>
		public MemoryStream GenerateMonthlyStationReportsToStream(int year, int month)
		{
			var previousYear = year = month - 1 == 0 ? year - 1 : year;
			var previousMonth = month - 1 == 0 ? 12 : month - 1;
			// Execute stored procedures and collect their results
			var eventData = ExecuteStoredProcedure("GetMonthlyStationEvents", year, month);
			var quantityData = ExecuteStoredProcedure("GetMonthlyStationQuantities", year, month);
			var QuantityDataKgs = ExecuteStoredProcedure("GetMonthlyStationQuantitiesKgs", year, month);
			var eventWalkInData = ExecuteStoredProcedure("GetMonthlyStationEventsWalkIn", year, month);
			var quantityWalkInData = ExecuteStoredProcedure("GetMonthlyStationQuantitiesWalkIn", year, month);

			var eventData0 = ExecuteStoredProcedure("GetMonthlyStationEvents", previousYear, previousMonth);
			var quantityData0 = ExecuteStoredProcedure("GetMonthlyStationQuantities", previousYear, previousMonth);
			var QuantityDataKgs0 = ExecuteStoredProcedure("GetMonthlyStationQuantitiesKgs", previousYear, previousMonth);
			var eventWalkInData0 = ExecuteStoredProcedure("GetMonthlyStationEventsWalkIn", previousYear, previousMonth);
			var quantityWalkInData0 = ExecuteStoredProcedure("GetMonthlyStationQuantitiesWalkIn", previousYear, previousMonth);

			// Export to a MemoryStream
			return ExportToMemoryStream(year, month,
				("Station Quantities in Litres", quantityData,quantityData0),
				("Station Quantities in Kgs", QuantityDataKgs, QuantityDataKgs0),
				("Station Events count", eventData, eventData0),
				("Walk-In Quantities in Litres", quantityWalkInData, quantityWalkInData0),
				("Walk-In Events count", eventWalkInData, eventWalkInData0));
		
		}
		private DataTable ExecuteStoredProcedure(string storedProcedureName, int year, int month)
		{
			DataTable dataTable = new();

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = storedProcedureName;
				command.CommandType = CommandType.StoredProcedure;

				var yearParam = command.CreateParameter();
				yearParam.ParameterName = "@Year";
				yearParam.Value = year;
				command.Parameters.Add(yearParam);

				var monthParam = command.CreateParameter();
				monthParam.ParameterName = "@Month";
				monthParam.Value = month;
				command.Parameters.Add(monthParam);

				if (command?.Connection?.State != ConnectionState.Open)
				{
					command?.Connection?.Open();
				}

				using var reader = command?.ExecuteReader();
				if (reader != null)
				{
					dataTable.Load(reader);
				}
			}

			return dataTable;
		}
		private MemoryStream ExportToMemoryStream(int year, int month, params (string SectionName, DataTable Data,DataTable Data2)[] tableData)
		{
			using var workbook = new XLWorkbook();
			var nameOfMonth = DateTime.UtcNow.ToString("MMMM");
			
			var previousMonthName = DateTime.UtcNow.AddMonths(-1).ToString("MMMM");
			var previousYear = year = month - 1 == 0 ? year - 1 : year;
			var previousMonth = month - 1 == 0 ? 12 : month - 1;

			var worksheet = workbook.Worksheets.Add($"{nameOfMonth} sales summary");
			var worksheet1 = workbook.Worksheets.Add($"{previousMonthName} sales summary");

			int daysInMonth = DateTime.DaysInMonth(year, month);
			int daysPreviousMonth = DateTime.DaysInMonth(previousYear,previousMonth);

			int today = DateTime.UtcNow.Day;

			var defaultStyle = worksheet.Style;
			defaultStyle.Font.FontColor = XLColor.Black;
			defaultStyle.Font.FontSize = 10;

			var monthName = new DateTime(year, month, 1).ToString("MMMM");
			var prevmonthName = new DateTime(previousYear, previousMonth, 1).ToString("MMMM");
			// Add custom headers for the sheet
			AddCustomHeaders(worksheet, year, month, daysInMonth,monthName);
			AddCustomHeaders(worksheet1, previousYear, previousMonth, daysPreviousMonth, prevmonthName);
			int currentRow = 7;

			foreach (var (sectionName, dataTable,dataTable0) in tableData)
			{
				// Add section header
				var headerRow = currentRow;
				worksheet.Cell(currentRow, 1).Value = sectionName;
				worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
				worksheet.Cell(currentRow, 1).Style.Font.FontSize = 12;

				currentRow++;

				// Insert table rows
				foreach (DataRow row in dataTable.Rows)
				{
					decimal totalValue = 0;
					decimal tots = 0;
					for (int col = 0; col < dataTable.Columns.Count; col++)
					{
						// Retrieve the cell value
						var cellValue = row[col];

						if (col == 0)
						{
							// First column: Handle as a string
							worksheet.Cell(currentRow, col + 1).Value = cellValue is DBNull ? string.Empty : cellValue.ToString();
						}
						else
						{
							// Other columns: Attempt to handle as double
							if (cellValue is DBNull || !decimal.TryParse(cellValue.ToString(), out var numericValue))
							{
								worksheet.Cell(currentRow, col + 1).Value = 0; // Default to 0 for invalid or non-numeric values
							}
							else
							{
								worksheet.Cell(currentRow, col + 1).Value = numericValue;

								// Update header row with a sum
								var currentHeaderValue = worksheet.Cell(headerRow, col + 1).GetValue<decimal?>() ?? 0; // Default to 0 if header cell is empty
								worksheet.Cell(headerRow, col + 1).Value = currentHeaderValue + numericValue;
								totalValue += numericValue;

								worksheet.Cell(currentRow, dataTable.Columns.Count + 1).Value = totalValue;
								worksheet.Cell(currentRow, dataTable.Columns.Count + 1).CreateComment().AddText($"Total for {sectionName} on {DateTime.UtcNow.Date.Day} is {totalValue}");
								tots = worksheet.Cell(headerRow, dataTable.Columns.Count + 1).GetValue<decimal?>() ?? 0;
								worksheet.Cell(headerRow, dataTable.Columns.Count + 1).Value = tots + numericValue;
								worksheet.Cell(headerRow, dataTable.Columns.Count + 2).Value = tots / (today)-1;
								worksheet.Cell(headerRow, dataTable.Columns.Count + 2).CreateComment().AddText($"Average for {sectionName} on {DateTime.UtcNow.Date.Day} is {tots / today}");
								worksheet.Cell(headerRow, dataTable.Columns.Count + 2).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);

								if (sectionName.Contains("Events") || sectionName.Contains("Walk-In"))
								{
									worksheet.Row(currentRow).Style.NumberFormat.NumberFormatId = 4;
									worksheet.Row(currentRow).Style.NumberFormat.Format = "#,##0.00";
								}
								else
								{
									worksheet.Row(currentRow).Style.NumberFormat.NumberFormatId = 2;
								}

								worksheet.Row(currentRow).Style.NumberFormat.NumberFormatId = sectionName.Contains("Events") ? 1 : 2;
							}
						}
						var totals2 = worksheet.Cell(currentRow, dataTable.Columns.Count + 1).GetValue<decimal?>() ?? 0;
						var average2 = totals2 / today;
						worksheet.Cell(currentRow, dataTable.Columns.Count + 2).Value = average2;
						worksheet.Cell(currentRow, dataTable.Columns.Count + 2).CreateComment().AddText($"Average for {sectionName} on {DateTime.UtcNow.Date.Day} is {average2}");

					}
					currentRow++;
				}
				currentRow += 1; // Move down for the next section
				worksheet.Range(1, dataTable.Columns.Count + 2, 30, dataTable.Columns.Count + 1).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
				worksheet.Range(1, dataTable.Columns.Count + 2, 30, dataTable.Columns.Count + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
			}

			currentRow = 7;
			foreach (var (sectionName, dataTable, dataTable0) in tableData)
			{
				// Add section header
				var headerRow = currentRow;
				worksheet1.Cell(currentRow, 1).Value = sectionName;
				worksheet1.Cell(currentRow, 1).Style.Font.Bold = true;
				worksheet1.Cell(currentRow, 1).Style.Font.FontSize = 12;

				currentRow++;
				today = DateTime.DaysInMonth(previousYear, previousMonth);
				// Insert table rows
				foreach (DataRow row in dataTable0.Rows)
				{
					decimal totalValue = 0;
					decimal tots = 0;
					for (int col = 0; col < dataTable0.Columns.Count; col++)
					{
						// Retrieve the cell value
						var cellValue = row[col];

						if (col == 0)
						{
							// First column: Handle as a string
							worksheet1.Cell(currentRow, col + 1).Value = cellValue is DBNull ? string.Empty : cellValue.ToString();
						}
						else
						{
							// Other columns: Attempt to handle as double
							if (cellValue is DBNull || !decimal.TryParse(cellValue.ToString(), out var numericValue))
							{
								worksheet1.Cell(currentRow, col + 1).Value = 0; // Default to 0 for invalid or non-numeric values
							}
							else
							{
								worksheet1.Cell(currentRow, col + 1).Value = numericValue;

								// Update header row with a sum
								var currentHeaderValue = worksheet1.Cell(headerRow, col + 1).GetValue<decimal?>() ?? 0; // Default to 0 if header cell is empty
								worksheet1.Cell(headerRow, col + 1).Value = currentHeaderValue + numericValue;
								totalValue += numericValue;

								worksheet1.Cell(currentRow, dataTable0.Columns.Count + 1).Value = totalValue;
								worksheet1.Cell(currentRow, dataTable0.Columns.Count + 1).CreateComment().AddText($"Total for {sectionName} on {DateTime.UtcNow.Date.Day} is {totalValue}");
						 tots = worksheet1.Cell(headerRow,  dataTable0.Columns.Count + 1).GetValue<decimal?>() ?? 0;
								worksheet1.Cell(headerRow,  dataTable0.Columns.Count + 1).Value = tots + numericValue;
								worksheet1.Cell(headerRow,  dataTable0.Columns.Count + 2).Value = tots / today;
								worksheet1.Cell(headerRow,  dataTable0.Columns.Count + 2).CreateComment().AddText($"Average for {sectionName} on {DateTime.UtcNow.Date.Day} is {tots / today}");
								worksheet1.Cell(headerRow,  dataTable0.Columns.Count + 2).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);

								if (sectionName.Contains("Events") || sectionName.Contains("Walk-In"))
								{
									worksheet1.Row(currentRow).Style.NumberFormat.NumberFormatId = 4;
									worksheet1.Row(currentRow).Style.NumberFormat.Format = "#,##0.00";
								}
								else
								{
									worksheet1.Row(currentRow).Style.NumberFormat.NumberFormatId = 2;
								}

								worksheet1.Row(currentRow).Style.NumberFormat.NumberFormatId = sectionName.Contains("Events") ? 1 : 2;
							}
						}
						var totals2 = worksheet1.Cell(currentRow, dataTable0.Columns.Count + 1).GetValue<decimal?>() ?? 0;
						var average2 = totals2 / today;
						worksheet1.Cell(currentRow, dataTable0.Columns.Count + 2).Value = average2;
						worksheet1.Cell(currentRow, dataTable0.Columns.Count + 2).CreateComment().AddText($"Average for {sectionName} on {DateTime.UtcNow.Date.Day} is {average2}");

					}
					currentRow++;
				}
				currentRow += 1; // Move down for the next section
				worksheet1.Range(1, dataTable0.Columns.Count + 2, 30, dataTable0.Columns.Count + 1).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
				worksheet1.Range(1, dataTable0.Columns.Count + 2, 30, dataTable0.Columns.Count + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
			}

			MoreStyles(worksheet, daysInMonth);
			MoreStyles(worksheet1, daysPreviousMonth);
			var memoryStream = new MemoryStream();
			workbook.SaveAs(memoryStream);
			memoryStream.Position = 0;

			var emails = GetRecipients("006");

			SendEmailWithAttachmentAsync(memoryStream, $"SalesSummary_{year}_{month}.xlsx", emails.Result ?? new Mails(), "Sales Summary Report", "Sales Summary Report for the month of " + DateTime.UtcNow.ToString("MMMM")).Wait();

			return memoryStream;
		}
		private static void MoreStyles(IXLWorksheet worksheet,int daysInMonth)
		{
			worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Column(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			worksheet.Row(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Row(3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			worksheet.Row(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Row(4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			worksheet.Row(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Row(5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			worksheet.Row(12).Style.NumberFormat.NumberFormatId = 2;

			worksheet.Range(7, 1, 7, daysInMonth + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
			worksheet.Range(17, 1, 17, daysInMonth + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
			worksheet.Range(27, 1, 27, daysInMonth + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);

			worksheet.Range(12, 1, 12, daysInMonth + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Range(22, 1, 22, daysInMonth + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 102, 163);

			worksheet.Row(7).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Range(12, 1, 12, daysInMonth + 3).Style.Font.FontColor = XLColor.FromArgb(0, 51, 102);
			worksheet.Range(12, daysInMonth + 2, 12, daysInMonth + 4).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Row(17).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Range(17, daysInMonth + 2, 17, daysInMonth + 4).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Row(22).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Row(27).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);

			worksheet.Range(22, 1, 22, daysInMonth + 3).Style.Font.FontColor = XLColor.FromArgb(0, 51, 102);
			worksheet.Range(22, daysInMonth + 2, 22, daysInMonth + 4).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);

			worksheet.Columns().AdjustToContents();
		}
		private static void AddCustomHeaders(IXLWorksheet worksheet, int year, int month,int daysInMonth,string monthName) 
		{
			
			worksheet.Row(1).Style.NumberFormat.NumberFormatId = 1;

			
			// Merge the first two rows for the title
			worksheet.Range(1, 1, 2, daysInMonth + 1).Merge();
			worksheet.Columns().AdjustToContents();
			worksheet.Cell(1, 1).Value = $"{monthName.ToUpper()} OTOGAS SALES SUMMARY";

			worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.FromArgb(255, 102, 163);
			worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
			worksheet.Cell(1, 1).Style.Font.Bold = true;
			worksheet.Cell(1, 1).Style.Font.FontSize = 24;
			worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

			// Merge cells for summation and average header
			//worksheet.Range(3, daysInMonth + 2, 4, daysInMonth + 3).Merge();
			worksheet.Cell(3, daysInMonth + 2).Value = "SUMMATION";
			worksheet.Cell(3, daysInMonth + 3).Value = "AVERAGE";
			worksheet.Cell(3, daysInMonth + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			worksheet.Cell(3, daysInMonth + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
			worksheet.Cell(3, daysInMonth + 2).Style.Font.FontColor = XLColor.FromArgb(0, 51, 102);

			worksheet.Cell(3, daysInMonth + 2).Style.Font.Bold = true;

			worksheet.Cell(3, 1).Value = "DAY";
			worksheet.Cell(4, 1).Value = "DAY OF WEEK";
			worksheet.Cell(5, 1).Value = "DATE";

			// Populate headers with days, day names, and dates
			for (int day = 1; day <= daysInMonth; day++)
			{
				var date = new DateTime(year, month, day);

				// First row: Numbers 1, 2, 3, ...
				worksheet.Cell(3, day + 1).Value = day;
				worksheet.Row(3).Style.Font.Bold = true;

				// Second row: Day names
				worksheet.Cell(4, day + 1).Value = date.DayOfWeek.ToString();
				worksheet.Row(4).Style.Font.Bold = true;

				// Third row: Dates in format 'yyyy-MM-dd'
				worksheet.Cell(5, day + 1).Value = date.ToString("yyyy-MM-dd");
				worksheet.Row(5).Style.Font.Bold = true;

			}
		}
		private static async Task SendEmailWithAttachmentAsync(Stream excelStream, string filename, Mails recipient, string subject, string customMessage)
		{
			if (recipient is not null)
			{
				var mail = new MailMessage
				{
					From = new MailAddress("Reports@protoenergy.com"),
					Subject = subject,
					IsBodyHtml = true,
					Body = $@"<html>
						<body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #444;"">
							<h2 style=""color: #0056b3; margin-bottom: 20px;"">{subject}</h2>
							<p>Dear Anthony Muraya,</p>
							<p>
								Please find attached the <strong>Sales Daily Summary</strong> for 
								<strong>{DateTime.UtcNow:MMMM}</strong>.
							</p>
							<p>
								This report provides a detailed overview of sales performance and trends up to the specified date, 
								helping you track progress and make informed decisions.
							</p>
							<p style=""margin-top: 20px;"">
								<strong>Important Note:</strong> This email is automatically generated by our system. 
								If you have any questions or require further assistance, please contact support.
							</p>
							<p style=""margin-top: 20px;"">
								Best regards,<br>
								<em>System Service Team</em>
							</p>
						</body>
					</html>"
				};

				mail.To.Add(recipient.ToEmails);
				mail.CC.Add(recipient.CcEmails);
				mail.BodyEncoding = Encoding.UTF8;
				mail.SubjectEncoding = Encoding.UTF8;
				// Add attachment

				var attachment = new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
				mail.Attachments.Add(attachment);
				//mail.Body = "Test";
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
		public async Task<Mails?> GetRecipients(string reportCode)
		{
			if (string.IsNullOrEmpty(reportCode))
				throw new ArgumentException("Report code cannot be null or empty.", nameof(reportCode));

			try
			{
				// Query data using LINQ
				var email = await _context.Emails
					.Where(e => e.ReportCode == reportCode)
					.Select(e => new
					{
						ToEmails = e.To ?? "wawerun@protoenergy.com",
						CcEmails = e.ToCC ?? "wawerun@protoenergy.com"
					})
					.FirstOrDefaultAsync();

				if (email == null)
				{
					// Return default email addresses if no record is found
					return new Mails
					{
						ToEmails = "wawerun@protoenergy.com",
						CcEmails = "wawerun@protoenergy.com"

					};
				}

				// Map the result to a Mails object
				return new Mails
				{
					ToEmails = email.ToEmails,
					CcEmails = email.CcEmails
				};
			}
			catch (Exception)
			{
				return new Mails();
			}
		}
	}
}
