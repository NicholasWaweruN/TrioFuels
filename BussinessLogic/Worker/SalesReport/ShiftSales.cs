using BusinessLogic.Worker.SalesReport;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Db_Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BussinessLogic.Worker.SalesReport
{
	public class ShiftsSales
	{
		private readonly OTOContext _context;
		private readonly IEmailService _email;
		public ShiftsSales(OTOContext context,IEmailService email) 
		{
			_context = context;
			_email = email;
		}
		public async Task<ServiceResponse<byte[]>> ShiftSalesReport(List<string> shiftNumbers)
		{
			_context.Database.SetCommandTimeout(300);

			// Create a parameterized SQL query with IN clause
			var shiftNumbersParameter = string.Join(",", shiftNumbers.Select((s, i) => $"@p{i}"));
			var sql = @$"SELECT SaleId, SalesDate, TransId, StationName, Attendant_Name AS AttendantName,
                CustomerName, TillNumber, StationName as Terminal, ShiftNumber, Vehicle, ProductName, PaymentType,
                Litres, Price, 0.00 as Discount, Amount, DispenserName, NozzleName, StorageLocation 
                FROM vw_SalesData WHERE ShiftNumber IN ({shiftNumbersParameter})";

			// Prepare the parameters
			var sqlParameters = shiftNumbers.Select((shift, index) => new SqlParameter($"@p{index}", shift)).ToArray();

			// Execute the query
			var salesData = await _context.Set<OtopaySales>().FromSqlRaw(sql, sqlParameters).ToListAsync();

			if (salesData.Count == 0)
				return ServiceResponse<byte[]>.Information("No Sales Data Found", null);

			// Group data by ShiftNumber
			var groupedData = salesData.GroupBy(s => s.ShiftNumber);

			// Create a new workbook
			var workbook = new XLWorkbook();
			workbook.Properties.Title = "Shift Sales Report";
			workbook.Properties.Author = "Otopay Sales Team";
			workbook.Properties.Company = "Proto Energy";
			workbook.Properties.Created = DateTime.UtcNow;
			workbook.Properties.Status = "Final";


			foreach (var shiftGroup in groupedData)
			{
				var shift = shiftGroup.Key;
				//Get Dispenser and stationName
				var stationName = shiftGroup.FirstOrDefault()?.StationName;
				var dispenserName = shiftGroup.FirstOrDefault()?.DispenserName;
				var sheetName = $"{stationName}_{dispenserName}";

				// Create a new worksheet for each shift
				var worksheet = workbook.Worksheets.Add($"{sheetName}");

				// Define headers
				var headers = new string[]
				{
					"SaleId", "SalesDate", "TransId", "StationName", "AttendantName", "CustomerName",
					"TillNumber", "Terminal", "ShiftNumber", "Vehicle", "ProductName", "PaymentType", "Litres",
					"Price", "Discount", "Amount", "DispenserName", "NozzleName", "StorageLocation"
				};

				// Insert headers into the first row
				for (int i = 0; i < headers.Length; i++)
				{
					worksheet.Cell(1, i + 1).Value = headers[i];
				}

				// Populate data rows for the current shift
				var shiftData = shiftGroup.ToList();
				for (int i = 0; i < shiftData.Count; i++)
				{
					worksheet.Cell(i + 2, 1).Value = shiftData[i].SaleId;
					worksheet.Cell(i + 2, 2).Value = shiftData[i].SalesDate;
					worksheet.Cell(i + 2, 3).Value = shiftData[i].TransId;
					worksheet.Cell(i + 2, 4).Value = shiftData[i].StationName;
					worksheet.Cell(i + 2, 5).Value = shiftData[i].AttendantName;
					worksheet.Cell(i + 2, 6).Value = shiftData[i].CustomerName;
					worksheet.Cell(i + 2, 7).Value = shiftData[i].TillNumber;
					worksheet.Cell(i + 2, 8).Value = shiftData[i].Terminal;
					worksheet.Cell(i + 2, 9).Value = shiftData[i].ShiftNumber;
					worksheet.Cell(i + 2, 10).Value = shiftData[i].Vehicle;
					worksheet.Cell(i + 2, 11).Value = shiftData[i].ProductName;
					worksheet.Cell(i + 2, 12).Value = shiftData[i].PaymentType;
					worksheet.Cell(i + 2, 13).Value = shiftData[i].Litres;
					worksheet.Cell(i + 2, 14).Value = shiftData[i].Price;
					worksheet.Cell(i + 2, 15).Value = shiftData[i].Discount;
					worksheet.Cell(i + 2, 16).Value = shiftData[i].Amount;
					worksheet.Cell(i + 2, 17).Value = shiftData[i].DispenserName;
					worksheet.Cell(i + 2, 18).Value = shiftData[i].NozzleName;
					worksheet.Cell(i + 2, 19).Value = shiftData[i].StorageLocation;
				}

				// Create an Excel table for the current shift
				var range = worksheet.Range(1, 1, shiftData.Count + 1, headers.Length);
				var table = range.CreateTable();
				table.Theme = XLTableTheme.TableStyleLight13;
				table.ShowAutoFilter = false;
				worksheet.Columns().AdjustToContents();
			}

			// Convert workbook to byte array

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			stream.Position = 0; // Reset the stream's position after saving

			var reportName = $"Report_{DateTime.UtcNow:yyyy_MM_dd}.xlsx";

			await SendEmailWithAttachmentAsync(stream, $"Shift_Sales_Report.xlsx", await GetRecipients("008") ?? new Mails(), "Shift Sales Report", "Shift Sales Report");
			return ServiceResponse<byte[]>.Success("Sales Report Exported Successfully", stream.ToArray());
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
							     <p>Dear All,</p>
                    <p>
                        Please find attached the <strong>Shift Sales Report</strong> for 
                        <strong>{ DateTime.UtcNow:MMMM yyyy}</strong>.
                    </p>
                    <p>
                        This report provides a comprehensive summary of sales activities during the specified shift.
                    </p>
                    <p style=\""margin-top: 20px;\"">
                        <strong>Important Note:</strong> This email is automatically generated by our system. 
                        If you have any questions or require further assistance, please contact support.
                    </p>
                    <p style=\""margin-top: 20px;\"">
                        Best regards,<br>
                        <em>System Service Team</em>
                    </p>
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
