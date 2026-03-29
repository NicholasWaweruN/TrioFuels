using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Worker.SalesReport;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static BusinessLogic.Services.Services;

namespace BussinessLogic.Sales.Wallet
{
	public class TransactionsSummaries
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		public TransactionsSummaries(OTOContext context,IAuthCommonTasks authentication)
		{
			_context = context;
			_authentication = authentication;
		}

		public async Task ComprehensiveCustomerBalances(Mails mails)
		{
			// SQL query to fetch the data for the first page
			var sql = "SELECT * FROM vw_VehicleTransactionSummary";

			// Execute SQL query and fetch data using FromSqlRaw
			var balance = await _context.Set<CustomerTransactionSummary>()
										 .FromSqlRaw(sql)
										 .ToListAsync();

			// SQL query to fetch data for transactions created yesterday
			var sqlYesterday = "SELECT * FROM vw_CustomerTransactionSummary";

			// Execute SQL query for yesterday's data
			var yesterdayBalance = await _context.Set<AllCredits>()
												  .FromSqlRaw(sqlYesterday)
												  .ToListAsync();

			// SQL query to fetch sales transactions from PaymentsSalesData view
			var sqlSales = "SELECT * FROM PaymentsSalesData ";

			// Execute SQL query for sales transactions data
			var salesTransactions = await _context.Set<SalesTransaction>()
												   .FromSqlRaw(sqlSales)
												   .ToListAsync();

			// Create the Excel file in memory
			using var package = new ExcelPackage();
			// Create a worksheet for the first page (Wallet Transactions)
			var worksheet1 = package.Workbook.Worksheets.Add("Wallet_Transactions");

			// Add headers to the Excel sheet
			worksheet1.Cells[1, 1].Value = "Vehicle Number";
			worksheet1.Cells[1, 2].Value = "Customer Name";
			worksheet1.Cells[1, 3].Value = "Opening Balance";
			worksheet1.Cells[1, 4].Value = "Total Credit";
			worksheet1.Cells[1, 5].Value = "Total Debit";
			worksheet1.Cells[1, 6].Value = "Closing Balance";

			// Populate the Excel sheet with the first set of data
			int row = 2;
			foreach (var item in balance)
			{
				worksheet1.Cells[row, 1].Value = item.VehicleRegistrationNumber;
				worksheet1.Cells[row, 2].Value = item.CustomerName;
				worksheet1.Cells[row, 3].Value = item.OpeningBalance;
				worksheet1.Cells[row, 4].Value = item.TotalCredit;
				worksheet1.Cells[row, 5].Value = item.TotalDebit; // Assuming TotalDebit is the same as Credit in the example
				worksheet1.Cells[row, 6].Value = item.ClosingBalance; // Assuming ClosingBalance = Balance + Credit
				row++;
			}

			// Define the range for the table (includes headers and data rows)
			var tableRange = worksheet1.Cells[1, 1, row - 1, 6];

			// Create a table from the range
			var table = worksheet1.Tables.Add(tableRange, "Customer_Wallet_Balance");

			// Apply the TableStyleLight9 theme
			table.TableStyle = TableStyles.Light9;

			// Auto-fit the columns
			worksheet1.Cells[worksheet1.Dimension.Address].AutoFitColumns();

			// Create a second worksheet for the transactions created yesterday (Credit Transactions)
			var worksheet2 = package.Workbook.Worksheets.Add("Credit_Transactions");

			// Add headers to the second worksheet
			worksheet2.Cells[1, 1].Value = "Customer Name";
			worksheet2.Cells[1, 2].Value = "Vehicle Registration Number";
			worksheet2.Cells[1, 3].Value = "Credit";
			worksheet2.Cells[1, 4].Value = "Product Name";
			worksheet2.Cells[1, 5].Value = "Date Created";
			worksheet2.Cells[1, 6].Value = "Transaction Reference";

			// Populate the second sheet with the yesterday's data
			int row2 = 2;
			foreach (var item in yesterdayBalance)
			{
				worksheet2.Cells[row2, 1].Value = item.CustomerName;
				worksheet2.Cells[row2, 2].Value = item.VehicleRegistrationNumber;
				worksheet2.Cells[row2, 3].Value = item.Credit;
				worksheet2.Cells[row2, 4].Value = item.ProductName;
				worksheet2.Cells[row2, 5].Value = item.DateCreated;
				worksheet2.Cells[row2, 6].Value = item.TransactionReference;
				row2++;
			}

			// Define the range for the table (includes headers and data rows)
			var tableRange2 = worksheet2.Cells[1, 1, row2 - 1, 6];

			// Create a table from the range
			var table2 = worksheet2.Tables.Add(tableRange2, "Credit_Transactions_Table");

			// Apply the TableStyleLight9 theme
			table2.TableStyle = TableStyles.Light9;

			// Auto-fit the columns
			worksheet2.Cells[worksheet2.Dimension.Address].AutoFitColumns();

			// Create a third worksheet for sales transactions
			var worksheet3 = package.Workbook.Worksheets.Add("Payments_Transactions");

			// Add headers to the third worksheet
			worksheet3.Cells[1, 1].Value = "Shift Number";
			worksheet3.Cells[1, 2].Value = "Sale ID";
			worksheet3.Cells[1, 3].Value = "Station Name";
			worksheet3.Cells[1, 4].Value = "Attendant Name";
			worksheet3.Cells[1, 5].Value = "Litres";
			worksheet3.Cells[1, 6].Value = "Amount";
			worksheet3.Cells[1, 7].Value = "Sales Date";
			worksheet3.Cells[1, 8].Value = "Payment Type";
			worksheet3.Cells[1, 9].Value = "Price";
			worksheet3.Cells[1, 10].Value = "Till Number";
			worksheet3.Cells[1, 11].Value = "Vehicle";
			worksheet3.Cells[1, 12].Value = "Product Name";
			worksheet3.Cells[1, 13].Value = "Customer Name";
			worksheet3.Cells[1, 14].Value = "Transaction ID";


			// Populate the third sheet with sales transactions data
			int row3 = 2;
			foreach (var item in salesTransactions)
			{
				worksheet3.Cells[row3, 1].Value = item.ShiftNumber;
				worksheet3.Cells[row3, 2].Value = item.SaleId;
				worksheet3.Cells[row3, 3].Value = item.StationName;
				worksheet3.Cells[row3, 4].Value = item.Attendant_Name;
				worksheet3.Cells[row3, 5].Value = item.Litres;
				worksheet3.Cells[row3, 6].Value = item.Amount;
				worksheet3.Cells[row3, 7].Value = item.SalesDate;
				worksheet3.Cells[row3, 8].Value = item.PaymentType;
				worksheet3.Cells[row3, 9].Value = item.Price;
				worksheet3.Cells[row3, 10].Value = item.TillNumber;
				worksheet3.Cells[row3, 11].Value = item.Vehicle;
				worksheet3.Cells[row3, 12].Value = item.ProductName;
				worksheet3.Cells[row3, 13].Value = item.CustomerName;
				worksheet3.Cells[row3, 14].Value = item.Transid;
				row3++;
			}
			worksheet1.Cells[worksheet1.Dimension.Address].AutoFitColumns();

			// Set the font size to 10 for all cells in worksheet1
			worksheet1.Cells[worksheet1.Dimension.Address].Style.Font.Size = 10;

			// Auto-fit the columns
			worksheet2.Cells[worksheet2.Dimension.Address].AutoFitColumns();

			// Set the font size to 10 for all cells in worksheet2
			worksheet2.Cells[worksheet2.Dimension.Address].Style.Font.Size = 10;

			// Auto-fit the columns
			worksheet3.Cells[worksheet3.Dimension.Address].AutoFitColumns();

			// Set the font size to 10 for all cells in worksheet3
			worksheet3.Cells[worksheet3.Dimension.Address].Style.Font.Size = 10;
			// Define the range for the table (includes headers and data rows)
			var tableRange3 = worksheet3.Cells[1, 1, row3 - 1, 14];

			// Create a table from the range
			var table3 = worksheet3.Tables.Add(tableRange3, "Sales_Transactions_Table");
			// For Credit Transactions sheet (worksheet2), format column 5 (Date Created) as DateTime
			worksheet2.Cells[2, 5, row2 - 1, 5].Style.Numberformat.Format = "yyyy-mm-dd hh:mm";

			// For Payments Transactions sheet (worksheet3), format column 7 (Sales Date) as DateTime
			worksheet3.Cells[2, 7, row3 - 1, 7].Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
			worksheet1.Cells[worksheet1.Dimension.Address].AutoFitColumns();
			worksheet2.Cells[worksheet2.Dimension.Address].AutoFitColumns();
			worksheet3.Cells[worksheet3.Dimension.Address].AutoFitColumns();
			// Apply the TableStyleLight9 theme
			table3.TableStyle = TableStyles.Light9;

			// Auto-fit the columns
			worksheet3.Cells[worksheet3.Dimension.Address].AutoFitColumns();

			// Convert the package to a MemoryStream
			using var stream = new MemoryStream();
			package.SaveAs(stream);
			stream.Position = 0; // Reset the stream position to the start

			// Send the email with the Excel file as an attachment
			await SendEmailWithAttachment(stream);
		}



		private async Task SendEmailWithAttachment(MemoryStream stream)
		{
			try
			{
				var networkCred = new NetworkCredential
				{
					UserName = "Reports@protoenergy.com",
					Password = "Tag50274",
				};
				// Set up email settings
				var smtpClient = new SmtpClient("smtp.office365.com")
				{
					Port = 587, // Use appropriate SMTP port (e.g., 587 for TLS)
					Credentials = new NetworkCredential(networkCred.UserName, networkCred.Password),
					EnableSsl = true,
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(networkCred.UserName),
					Subject = "Customer Wallet Balances",
					IsBodyHtml = true,
				};

				var recipients = await GetEmailRecipients("003");
				if (recipients.ResponseObject != null)
				{
					var To = recipients.ResponseObject.To;
					var ToCC = recipients.ResponseObject.ToCC;

					// Add recipient(s)
					mailMessage.To.Add(To);
					mailMessage.CC.Add(ToCC);
				}

				// HTML email body content
				string emailBody = @"
					<html>
					<head>
						<style>
							body { font-family: Arial, sans-serif; color: #333333; margin: 20px; }
							h2 { color: #007BFF; }
							.content { font-size: 14px; line-height: 1.6; }
							.footer { margin-top: 20px; font-size: 12px; color: #888888; }
						</style>
					</head>
					<body>
						<h2>Comprehensive Wallet Balances Report</h2>
						<p class='content'>
							Dear All,<br><br>
							Please find attached the Wallet Balances report in Excel format.<br><br>
							If you have any questions or need further assistance, feel free to contact us.<br><br>
							Best regards,<br>
							IT Team
						</p>
						<div class='footer'>
							<p>If you have any inquiries, please contact our support team at support@protoenergy.com.</p>
						</div>
					</body>
					</html>";

				// Assign the email body
				mailMessage.Body = emailBody;

				// Attach the Excel file from the MemoryStream
				var attachment = new Attachment(stream, "CustomerTransactionSummary.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
				mailMessage.Attachments.Add(attachment);

				// Send the email asynchronously
				await smtpClient.SendMailAsync(mailMessage);

				Console.WriteLine("Email sent successfully!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sending email: " + ex.Message);
			}
		}

		private async Task<ServiceResponse<EmailsDto>> GetEmailRecipients(string reportCode)
		{
			try
			{
				var emails = await (from e in _context.Emails
									select e).Where(x => x.ReportCode.Equals(reportCode)).FirstOrDefaultAsync();

				if (emails != null)
				{
					var newEmail = new EmailsDto
					{
						To = emails.To,
						ToCC = emails.ToCC,
					};
					if (_authentication.Usercode() == "00008")
					{
						newEmail = new EmailsDto
						{
							To = "wawerun@protoenergy.com",
							ToCC = "wawerun@protoenergy.com"
						};
					}

					return ServiceResponse<EmailsDto>.Success("", newEmail);

				}
				return ServiceResponse<EmailsDto>.Information("No email recipients found", null);

			}
			catch (Exception)

			{
			};

				return ServiceResponse<EmailsDto>.Error("Something went wrong", null);
			}
		}
	}


