using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Messaging;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using BussinessLogic.Setup;

namespace BusinessLogic.Sales.Wallet
{
	public class Statements
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly OTOContext _context;
		private readonly IAfricaIsTalking _africaIsTalking;
		private readonly IEmailService _emailService;
		private readonly IMemoryCache _cache;

		public Statements(IAuthCommonTasks authentication, ICommonSetups setups, OTOContext context, IAfricaIsTalking africaIsTalking, IEmailService emailService, IMemoryCache cache)
		{
			_authentication = authentication;
			_setups = setups;
			_context = context;
			_africaIsTalking = africaIsTalking;
			_emailService = emailService;
			_cache = cache;
		}

		public async Task<ServiceResponse<byte[]>> CustomerStatementAsPdf(string customerCode, DateTime from)
		{
			try
			{
				var customer = await GetCustomerAsync(customerCode);
				if (customer == null)
					return ServiceResponse<byte[]>.Information("Customer not found", null);

				var transactions = await GetCustomerTransactionsAsync(customerCode, from);
				if (transactions.Count == 0)
					return ServiceResponse<byte[]>.Information("No transactions found for the specified customer", null);

				using var pdfDocument = new PdfDocument();
				var page = pdfDocument.Pages.Add();
				var graphics = page.Graphics;

				// Load and draw logo
				DrawLogo(graphics);

				// Draw customer details
				DrawCustomerDetails(graphics, customer, customerCode);

				// Draw transactions
				DrawTransactionTable(graphics, transactions, pdfDocument);

				// Save PDF to memory stream
				using var pdfStream = new MemoryStream();
				pdfDocument.Save(pdfStream);
				pdfStream.Position = 0;

				await LogUserTrail(customer);

				return ServiceResponse<byte[]>.Success("Customer statement exported successfully as PDF", pdfStream.ToArray());
			}
			catch (Exception)
			{

				return ServiceResponse<byte[]>.Error("An error occurred while exporting the customer statement as PDF", null);
			}
		}

		private async Task<Customer> GetCustomerAsync(string customerCode)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerCode == customerCode) ?? new Customer();
		}

		private async Task<List<TransactionDto>> GetCustomerTransactionsAsync(string customerCode, DateTime from)
		{
			var balanceBefore = await _context.CustomerTransactions
				.Where(x => x.VehicleCode == customerCode && x.DateCreated.Date <= from.Date)
				.SumAsync(x => x.Credit - x.Debit);

			var transactions = await (from c in _context.CustomerTransactions
									  join v in _context.Vehicles on c.VehicleCode equals v.VehicleCode
									  where v.CustomerCode == customerCode
									  select new TransactionDto
									  {
										  VehicleRegistrationNumber = v.VehicleRegistrationNumber,
										  TransactionReference = c.TransactionReference,
										  DateCreated = c.DateCreated,
										  Credit = c.Credit,
										  Debit = c.Debit
									  }).OrderBy(t => t.DateCreated).ToListAsync();

			if (balanceBefore != 0)
			{
				transactions.Insert(0, new TransactionDto
				{
					VehicleRegistrationNumber = $"Balance Before {from:yyyy-MMMM-dd}",
					TransactionReference = _setups.GenerateSaleId(),
					DateCreated = from,
					Credit = balanceBefore,
					Debit = 0,
				});
			}

			decimal runningBalance = balanceBefore;
			foreach (var transaction in transactions)
			{
				runningBalance += transaction.Credit - transaction.Debit;
				transaction.RunningBalance = runningBalance;
			}

			return transactions;
		}

		private static void DrawLogo(PdfGraphics graphics)
		{
			using var logoStream = new FileStream("wwwroot/Logo/logo.png", FileMode.Open, FileAccess.Read);
			var logoImage = PdfImage.FromStream(logoStream);
			graphics.DrawImage(logoImage, new RectangleF(20, 70, 80, 80));
		}

		private static void DrawCustomerDetails(PdfGraphics graphics, Customer customer, string customerCode)
		{
			var bodyFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
			var detailsXPosition = 110; // Adjusted to leave space for the logo
			graphics.DrawString($"Customer: {customer.CustomerName}", bodyFont, PdfBrushes.Black, new PointF(detailsXPosition, 70));
			graphics.DrawString($"Customer Code: {customerCode}", bodyFont, PdfBrushes.Black, new PointF(detailsXPosition, 90));
			graphics.DrawString($"Statement Date: {DateTime.UtcNow:yyyy-MM-dd}", bodyFont, PdfBrushes.Black, new PointF(detailsXPosition, 110));
			graphics.DrawLine(new PdfPen(new PdfColor(0x15, 0x15, 0x27), 1.5f), new PointF(30, 130), new PointF(graphics.ClientSize.Width - 30, 130));
		}

		private static void DrawTransactionTable(PdfGraphics graphics, List<TransactionDto> transactions, PdfDocument pdfDocument)
		{
			var headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
			var bodyFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
			var headerBackgroundBrush = new PdfSolidBrush(new PdfColor(0xF0, 0xF0, 0xF0));
			var primaryBrush = new PdfSolidBrush(new PdfColor(0x15, 0x15, 0x27));
			float yPosition = 140;

			graphics.DrawRectangle(headerBackgroundBrush, new RectangleF(30, yPosition, graphics.ClientSize.Width - 60, 20));
			graphics.DrawString("Transaction ID", headerFont, primaryBrush, new PointF(40, yPosition + 5));
			graphics.DrawString("Reg_No", headerFont, primaryBrush, new PointF(140, yPosition + 5));
			graphics.DrawString("Date", headerFont, primaryBrush, new PointF(220, yPosition + 5));
			graphics.DrawString("Credit", headerFont, primaryBrush, new PointF(310, yPosition + 5));
			graphics.DrawString("Debit", headerFont, primaryBrush, new PointF(380, yPosition + 5));
			graphics.DrawString("Balance", headerFont, primaryBrush, new PointF(450, yPosition + 5));

			yPosition += 30;
			foreach (var transaction in transactions)
			{
				var rowBackgroundColor = transactions.IndexOf(transaction) % 2 == 0 ? PdfBrushes.White : headerBackgroundBrush;
				graphics.DrawRectangle(rowBackgroundColor, new RectangleF(30, yPosition - 5, graphics.ClientSize.Width - 60, 20));
				graphics.DrawString(transaction.TransactionReference, bodyFont, PdfBrushes.Black, new PointF(40, yPosition));
				graphics.DrawString(transaction.VehicleRegistrationNumber, bodyFont, PdfBrushes.Black, new PointF(140, yPosition));
				graphics.DrawString(transaction.DateCreated.ToString("yyyy-MM-dd HH:mm"), bodyFont, PdfBrushes.Black, new PointF(220, yPosition));
				graphics.DrawString(transaction.Credit.ToString("N2"), bodyFont, PdfBrushes.Black, new PointF(310, yPosition));
				graphics.DrawString(transaction.Debit.ToString("N2"), bodyFont, PdfBrushes.Black, new PointF(380, yPosition));
				graphics.DrawString(transaction.RunningBalance.ToString("N2"), bodyFont, PdfBrushes.Black, new PointF(450, yPosition));

				yPosition += 20;
				if (yPosition > graphics.ClientSize.Height - 50)
				{
					var page = pdfDocument.Pages.Add();
					graphics = page.Graphics;
					yPosition = 30;
				}
			}
		}
		private static void DrawCustomerStatement(PdfGraphics graphics, Customer customer, List<TransactionDto> transactions)
		{
			var headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
			var bodyFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
			var primaryBrush = new PdfSolidBrush(new PdfColor(0x15, 0x15, 0x27)); // Dark text color

			// Draw Logo
			using var logoStream = new FileStream("wwwroot/Logo/logo.png", FileMode.Open, FileAccess.Read);
			var logoImage = PdfImage.FromStream(logoStream);
			graphics.DrawImage(logoImage, new RectangleF(20, 20, 80, 80));  // Adjust placement and size

			// Draw company and customer details
			var detailsXPosition = 110; // Adjusted for logo space
			graphics.DrawString($"Steve Beyer Productions Pty. Ltd.", headerFont, primaryBrush, new PointF(detailsXPosition, 20));
			graphics.DrawString($"Customer: {customer.CustomerName}", bodyFont, primaryBrush, new PointF(detailsXPosition, 40));
			graphics.DrawString($"Customer Code: {customer.CustomerCode}", bodyFont, primaryBrush, new PointF(detailsXPosition, 60));
			graphics.DrawString($"Statement Date: {DateTime.UtcNow:yyyy-MM-dd}", bodyFont, primaryBrush, new PointF(detailsXPosition, 80));
			graphics.DrawString($"Amount Due: {transactions.Sum(t => t.Credit - t.Debit):C2}", bodyFont, primaryBrush, new PointF(detailsXPosition, 100));

			// Draw a line after customer details
			graphics.DrawLine(new PdfPen(new PdfColor(0x15, 0x15, 0x27), 1.5f), new PointF(30, 120), new PointF(graphics.ClientSize.Width - 30, 120));

			// Draw Transaction Table Header
			float yPosition = 130;
			graphics.DrawString("Date", headerFont, primaryBrush, new PointF(30, yPosition));
			graphics.DrawString("Description", headerFont, primaryBrush, new PointF(120, yPosition));
			graphics.DrawString("PO Number", headerFont, primaryBrush, new PointF(250, yPosition));
			graphics.DrawString("Debit", headerFont, primaryBrush, new PointF(350, yPosition));
			graphics.DrawString("Credit", headerFont, primaryBrush, new PointF(430, yPosition));
			graphics.DrawString("Balance", headerFont, primaryBrush, new PointF(510, yPosition));

			yPosition += 20;

			// Draw transactions
			foreach (var transaction in transactions)
			{
				graphics.DrawString(transaction.DateCreated.ToString("dd/MM/yyyy"), bodyFont, primaryBrush, new PointF(30, yPosition));
				graphics.DrawString(transaction.TransactionReference, bodyFont, primaryBrush, new PointF(120, yPosition));
				graphics.DrawString(transaction.Credit.ToString(), bodyFont, primaryBrush, new PointF(250, yPosition));
				graphics.DrawString(transaction.Debit.ToString("N2"), bodyFont, primaryBrush, new PointF(350, yPosition));
				graphics.DrawString(transaction.Credit.ToString("N2"), bodyFont, primaryBrush, new PointF(430, yPosition));
				graphics.DrawString(transaction.RunningBalance.ToString("N2"), bodyFont, primaryBrush, new PointF(510, yPosition));
				yPosition += 20;
			}
		}
		private async Task LogUserTrail(Customer customer)
		{
			var message = $"{_authentication.Name()} exported customer statement of {customer.CustomerName} on {DateTime.UtcNow}";
			await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
		}


	}
}