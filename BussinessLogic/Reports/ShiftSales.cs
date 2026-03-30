using BusinessLogic.Worker.SalesReport;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Reports
{
	public class ShiftSales(OTOContext context)
	{
		private readonly OTOContext _context = context;

		public async Task GenerateDailySalesReportAsync(DateTime date, int shiftNumber, Mails mails)
		{
			var connectionString = _context.Database.GetConnectionString();
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Database connection string is not configured.");
			}
			_context.Database.SetCommandTimeout(300);

			var sql = @"
        SELECT SaleId,SalesDate, StationName, Attendant_Name AS AttendantName,
               StationName AS Terminal, ShiftNumber, Vehicle, 
               ProductName, PaymentType, Litres, Price,DispenserName, NozzleName, StorageLocation 
        FROM vw_SalesData 
        WHERE CAST(SalesDate AS Date) = @ReportDate AND ShiftNumber = @ShiftNumber";

			DataTable dataTable = new();
			using (var connection = _context.Database.GetDbConnection())
			{
				await connection.OpenAsync();
				using var command = connection.CreateCommand();
				command.CommandText = sql;
				command.CommandType = CommandType.Text;

				var dateParameter = command.CreateParameter();
				dateParameter.ParameterName = "@ReportDate";
				dateParameter.Value = date.Date;
				dateParameter.DbType = DbType.Date;
				command.Parameters.Add(dateParameter);

				var shiftParameter = command.CreateParameter();
				shiftParameter.ParameterName = "@ShiftNumber";
				shiftParameter.Value = shiftNumber;
				shiftParameter.DbType = DbType.Int32;
				command.Parameters.Add(shiftParameter);

				using var reader = await command.ExecuteReaderAsync();
				dataTable.Load(reader);
			}

			var day = DateTime.UtcNow.Day.ToString() + GetOrdinalSuffix(DateTime.UtcNow.Day);
			var dateName = DateTime.UtcNow.ToString("MMMM yyyy");
			var header = $"{day} {dateName} Shift {shiftNumber} Sales";

			using var excelStream = GenerateExcelFromDataTable(dataTable);
			await SendEmailWithAttachmentAsync(excelStream, $"SalesDailyReport_Shift{shiftNumber}_{date:yyyy_MM_dd}.xlsx", mails, header);
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
			var worksheet = package.Workbook.Worksheets.Add("Shift Report");

			// Load the data from the DataTable into the worksheet
			worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

			// Format the data as a table
			var tableRange = worksheet.Cells[1, 1, dataTable.Rows.Count + 1, dataTable.Columns.Count];
			var table = worksheet.Tables.Add(tableRange, "shiftsales");
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

		private async Task SendEmailWithAttachmentAsync(Stream excelStream, string filename, Mails recipient, string subject)
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
                            <strong>{subject.Replace("Sales", "")}</strong>.
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

		private async Task SendEmailAsync(string subject, Mails recipient, decimal rental, decimal outRight)
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

		private static string Body(string subject, decimal rental, decimal OutRight)
		{
			var Body = $@"
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
	}
}
