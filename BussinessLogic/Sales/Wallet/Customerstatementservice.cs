/*
 * ⚠️ ASSUMPTIONS TO VERIFY
 * - `_context` is your existing DbContext (EF Core / Npgsql), injected the
 *   same way your other BussinessLogic.Sales services already do it —
 *   rename `ApplicationDbContext` below to your actual type.
 * - There's a `Customers` DbSet with CustomerCode, CustomerName,
 *   CustomerPhone, and a `Vehicles` DbSet with VehicleCode +
 *   RegistrationNumber — same shapes SearchVehicle already assumes.
 * - Opening balance = net of every Credit-Debit transaction *before*
 *   FromDate. If you'd rather always show 0, delete the openingBalance
 *   query and set `openingBalance = 0m`.
 * - PDF generation uses QuestPDF (dotnet add package QuestPDF) — free
 *   Community license for small teams. Swap BuildStatementPdf's internals
 *   if you already have a PDF library elsewhere in FuelFlow.
 * - Excel generation mirrors the ClosedXML pattern already used for the
 *   vw_SalesData report.
 */
using ClosedXML.Excel;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Personal_Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.Wallet
{
	public class CustomerStatementService : ICustomerStatementService
	{
		private readonly OTOContext _context; // rename to your DbContext type

		public CustomerStatementService(OTOContext context)
		{
			_context = context;
		}

		public async Task<CustomerStatementDto?> GetCustomerStatementAsync(
			string customerCode, DateTime fromDate, DateTime toDate)
		{
			var customer = await _context.Customers
				.Where(c => c.CustomerCode == customerCode)
				.Select(c => new { c.CustomerCode, c.CustomerName, c.CustomerPhone })
				.FirstOrDefaultAsync();

			if (customer == null)
				return null;

			var openingBalance = await _context.CustomerTransactions
				.Where(t => t.CustomerCode == customerCode && t.DateCreated < fromDate)
				.SumAsync(t => (decimal?)(t.Credit - t.Debit)) ?? 0m;

			// Left join to Vehicle so a transaction with no vehicle match
			// (or a vehicle later deleted) still shows up on the statement.
			var lines = await (
				from t in _context.CustomerTransactions
				join v in _context.Vehicles on t.VehicleCode equals v.VehicleCode into vehicleJoin
				from v in vehicleJoin.DefaultIfEmpty()
				where t.CustomerCode == customerCode
					  && t.DateCreated >= fromDate
					  && t.DateCreated <= toDate
				orderby t.DateCreated

				select new CustomerStatementLineDto
				{
					Date = t.DateCreated,
					TransactionReference = t.TransactionReference,
					VehicleCode = t.VehicleCode,
					RegistrationNumber = v != null ? v.VehicleRegistrationNumber : null,
					Narration = t.Narration,
					UserReference = t.UserReference,
					TopUpType = t.TopUpType,
					Credit = t.Credit,
					Debit = t.Debit,
				}
			).ToListAsync();

			var running = openingBalance;
			foreach (var line in lines)
			{
				running += line.Credit - line.Debit;
				line.RunningBalance = running;
			}

			return new CustomerStatementDto
			{
				CustomerCode = customer.CustomerCode,
				CustomerName = customer.CustomerName,
				CustomerPhone = customer.CustomerPhone,
				FromDate = fromDate,
				ToDate = toDate,
				OpeningBalance = openingBalance,
				TotalCredits = lines.Sum(l => l.Credit),
				TotalDebits = lines.Sum(l => l.Debit),
				ClosingBalance = running,
				Lines = lines,
			};
		}

		/* ---------------------------------------------------------- */
		/* Excel export — ClosedXML, same pattern as vw_SalesData      */
		/* ---------------------------------------------------------- */
		public byte[] BuildStatementExcel(CustomerStatementDto statement)
		{
			using var workbook = new XLWorkbook();
			var ws = workbook.Worksheets.Add("Statement");

			ws.Cell(1, 1).Value = "Customer Statement";
			ws.Cell(1, 1).Style.Font.SetBold().Font.FontSize = 14;
			ws.Cell(2, 1).Value = $"{statement.CustomerName} ({statement.CustomerCode})";
			ws.Cell(3, 1).Value = statement.CustomerPhone ?? "";
			ws.Cell(4, 1).Value = $"Period: {statement.FromDate:dd MMM yyyy} – {statement.ToDate:dd MMM yyyy}";

			var headerRow = 6;
			string[] headers = { "Date", "Reference", "Vehicle Reg.", "Narration", "Credit", "Debit", "Balance" };
			for (int i = 0; i < headers.Length; i++)
			{
				var cell = ws.Cell(headerRow, i + 1);
				cell.Value = headers[i];
				cell.Style.Font.SetBold();
				cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1c2f7a");
				cell.Style.Font.FontColor = XLColor.White;
			}

			var row = headerRow + 1;
			foreach (var line in statement.Lines)
			{
				ws.Cell(row, 1).Value = line.Date;
				ws.Cell(row, 1).Style.DateFormat.Format = "dd-MMM-yyyy HH:mm";
				ws.Cell(row, 2).Value = line.TransactionReference;
				ws.Cell(row, 3).Value = line.RegistrationNumber ?? "—";
				ws.Cell(row, 4).Value = line.Narration;
				ws.Cell(row, 5).Value = line.Credit;
				ws.Cell(row, 6).Value = line.Debit;
				ws.Cell(row, 7).Value = line.RunningBalance;
				row++;
			}

			ws.Range(headerRow + 1, 5, row - 1, 7).Style.NumberFormat.Format = "#,##0.00";
			ws.Cell(row, 4).Value = "Closing balance";
			ws.Cell(row, 4).Style.Font.SetBold();
			ws.Cell(row, 7).Value = statement.ClosingBalance;
			ws.Cell(row, 7).Style.Font.SetBold();
			ws.Cell(row, 7).Style.NumberFormat.Format = "#,##0.00";

			ws.Columns().AdjustToContents();

			using var stream = new System.IO.MemoryStream();
			workbook.SaveAs(stream);
			return stream.ToArray();
		}

		/* ---------------------------------------------------------- */
		/* PDF export — QuestPDF                                       */
		/* ---------------------------------------------------------- */
		public byte[] BuildStatementPdf(CustomerStatementDto statement)
		{
			QuestPDF.Settings.License = LicenseType.Community;

			var document = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(30);
					page.DefaultTextStyle(x => x.FontSize(9).FontFamily("Arial"));

					page.Header().Column(col =>
					{
						col.Item().Text("Customer Statement").FontSize(16).Bold().FontColor("#1c2f7a");
						col.Item().Text($"{statement.CustomerName} ({statement.CustomerCode})").FontSize(11).SemiBold();
						if (!string.IsNullOrWhiteSpace(statement.CustomerPhone))
							col.Item().Text(statement.CustomerPhone).FontColor(Colors.Grey.Darken1);
						col.Item().PaddingTop(4).Text(
							$"Period: {statement.FromDate:dd MMM yyyy} – {statement.ToDate:dd MMM yyyy}"
						).FontColor(Colors.Grey.Darken1);
					});

					page.Content().PaddingTop(15).Table(table =>
					{
						table.ColumnsDefinition(columns =>
						{
							columns.RelativeColumn(2); // Date
							columns.RelativeColumn(2); // Reference
							columns.RelativeColumn(2); // Vehicle
							columns.RelativeColumn(3); // Narration
							columns.RelativeColumn(1.5f); // Credit
							columns.RelativeColumn(1.5f); // Debit
							columns.RelativeColumn(1.5f); // Balance
						});

						table.Header(header =>
						{
							string[] headers = { "Date", "Reference", "Vehicle Reg.", "Narration", "Credit", "Debit", "Balance" };
							foreach (var h in headers)
							{
								header.Cell().Background("#1c2f7a").Padding(4)
									.Text(h).FontColor(Colors.White).SemiBold();
							}
						});

						foreach (var line in statement.Lines)
						{
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.Date.ToString("dd-MMM-yy HH:mm"));
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.TransactionReference);
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.RegistrationNumber ?? "—");
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.Narration);
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.Credit > 0 ? line.Credit.ToString("#,##0.00") : "");
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.Debit > 0 ? line.Debit.ToString("#,##0.00") : "");
							table.Cell().Padding(4).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2)
								.Text(line.RunningBalance.ToString("#,##0.00")).SemiBold();
						}
					});

					page.Footer().PaddingTop(10).Row(row =>
					{
						row.RelativeItem().Text(text =>
						{
							text.Span("Closing balance: ").SemiBold();
							text.Span($"KSh {statement.ClosingBalance:#,##0.00}").Bold().FontColor("#1c2f7a");
						});
						row.RelativeItem().AlignRight().Text(x =>
						{
							x.CurrentPageNumber();
							x.Span(" / ");
							x.TotalPages();
						});
					});
				});
			});

			return document.GeneratePdf();
		}
	}
}