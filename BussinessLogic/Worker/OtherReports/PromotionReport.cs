using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using DataAccessLayer.EntityModels.SetUps;
using BusinessLogic.Worker.SalesReport;

namespace BussinessLogic.Worker.OtherReports
{
	public class PromotionReport
	{
		private readonly OTOContext _context;

		public PromotionReport(OTOContext context)
		{
			_context = context;
		}

		public MemoryStream GeneratePromotionReport(DateTime startDate, DateTime endDate, DateTime beforeStart, DateTime beforeEnd)
		{
			// Execute stored procedure and collect results
			var promoData = ExecuteStoredProcedure("Above100Litres", startDate, endDate,beforeStart,beforeEnd);

			// Export to MemoryStream
			return ExportToMemoryStream(startDate, endDate, ("Promotion Data (Above 100 Litres)", promoData));
		}

		private DataTable ExecuteStoredProcedure(string storedProcedureName, DateTime startDate, DateTime endDate,DateTime beforeStart,DateTime beforeEnd)
		{
			DataTable dataTable = new DataTable();

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = storedProcedureName;
				command.CommandType = CommandType.StoredProcedure;

				var startParam = command.CreateParameter();
				startParam.ParameterName = "@StartDate";
				startParam.Value = startDate;
				command.Parameters.Add(startParam);

				var endParam = command.CreateParameter();
				endParam.ParameterName = "@EndDate";
				endParam.Value = endDate;
				command.Parameters.Add(endParam);

				var beforeStartParam = command.CreateParameter();
				beforeStartParam.ParameterName = "@BeforeStart";
				beforeStartParam.Value = beforeStart;
				command.Parameters.Add(beforeStartParam);

				var beforeEndParam = command.CreateParameter();
				beforeEndParam.ParameterName = "@BeforeEnd";
				beforeEndParam.Value = beforeEnd;
				command.Parameters.Add(beforeEndParam);


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

		private static MemoryStream ExportToMemoryStream(DateTime startDate, DateTime endDate, params (string SectionName, DataTable Data)[] tableData)
		{
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Promotion Report");

			AddCustomHeaders(worksheet, startDate, endDate);

			int currentRow = 7; // Start inserting data after headers

			foreach (var (sectionName, dataTable) in tableData)
			{
				// Add section header
				worksheet.Cell(currentRow, 1).Value = sectionName;
				worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
				currentRow++;

				// Insert table data
				foreach (DataRow row in dataTable.Rows)
				{
					for (int col = 0; col < dataTable.Columns.Count; col++)
					{
						var cellValue = row[col];
						if (cellValue is DateTime dateValue)
						{
							worksheet.Cell(currentRow, col + 1).Value = dateValue;
						}
						else if (cellValue is int intValue)
						{
							worksheet.Cell(currentRow, col + 1).Value = intValue;
						}
						else
						{
							worksheet.Cell(currentRow, col + 1).Value = cellValue?.ToString() ?? string.Empty;
						}
					}
					currentRow++;
				}

				currentRow += 2; // Leave space between sections
			}

			worksheet.Columns().AdjustToContents();

			var memoryStream = new MemoryStream();
			workbook.SaveAs(memoryStream);
			memoryStream.Position = 0;

			return memoryStream;
		}

		private static void AddCustomHeaders(IXLWorksheet worksheet, DateTime startDate, DateTime endDate)
		{
			worksheet.Cell(1, 1).Value = "Promotion Report - Vehicles with Consumption Above 100 Litres";
			worksheet.Cell(1, 1).Style.Font.Bold = true;
			worksheet.Cell(1, 1).Style.Font.FontSize = 16;

			worksheet.Cell(3, 1).Value = $"Report Period: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}";
			worksheet.Cell(3, 1).Style.Font.Italic = true;
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
					Body = customMessage
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
				var email = await _context.Emails
					.Where(e => e.ReportCode == reportCode)
					.Select(e => new
					{
						ToEmails = e.To ?? "default@protoenergy.com",
						CcEmails = e.ToCC ?? "default@protoenergy.com"
					})
					.FirstOrDefaultAsync();

				if (email == null)
				{
					return new Mails
					{
						ToEmails = "default@protoenergy.com",
						CcEmails = "default@protoenergy.com"
					};
				}

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
