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

			var logoPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "assets", "trio-fuels-logo.png");
			byte[]? logoBytes = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : null;

			// Declared as Color (not string) so every ternary like
			// "condition ? success : muted" resolves to Color unambiguously.
			var ink = Color.FromHex("#14224F");
			var primary = Color.FromHex("#1C2F7A");
			var muted = Color.FromHex("#64748B");
			var border = Color.FromHex("#E2E8F0");
			var success = Color.FromHex("#1F9D55");
			var danger = Color.FromHex("#E24B4A");
			var rowAlt = Color.FromHex("#F8FAFC");

			var document = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(22);
					page.DefaultTextStyle(x => x.FontSize(8.5f).FontColor(ink).FontFamily("Arial"));

					// ── Header ──
					page.Header().Column(col =>
					{
						col.Item().Row(row =>
						{
							row.ConstantItem(110).Height(42).AlignMiddle().Element(e =>
							{
								if (logoBytes != null)
									e.Image(logoBytes).FitArea();
								else
									e.Text("TRIO FUELS").Bold().FontSize(15).FontColor(primary);
							});

							row.RelativeItem().AlignRight().Column(c =>
							{
								c.Item().Text("Trio Fuels").Bold().FontSize(12).FontColor(primary);
								c.Item().Text("Nairobi, Kenya").FontSize(8).FontColor(muted);
							});
						});

						col.Item().PaddingTop(8).LineHorizontal(1).LineColor(border);

						col.Item().PaddingTop(8).Text("Customer Wallet Statement")
							.Bold().FontSize(12).FontColor(ink);

						col.Item().PaddingTop(6).Row(row =>
						{
							row.RelativeItem().Column(c =>
							{
								c.Item().Text("CUSTOMER DETAILS").FontSize(6.5f).FontColor(muted).Bold();
								c.Item().Text(statement.CustomerName).FontSize(9).Bold();
								c.Item().Text(statement.CustomerPhone ?? "—").FontSize(8).FontColor(muted);
								c.Item().Text(statement.CustomerCode).FontSize(7.5f).FontColor(muted);
							});

							row.RelativeItem().AlignRight().Column(c =>
							{
								c.Item().Text("STATEMENT PERIOD").FontSize(6.5f).FontColor(muted).Bold();
								c.Item().Text($"{statement.FromDate:dd MMM yyyy} \u2013 {statement.ToDate:dd MMM yyyy}")
									.FontSize(9).Bold();
								c.Item().Text($"Generated {DateTime.Now:dd MMM yyyy, HH:mm}")
									.FontSize(7.5f).FontColor(muted);
							});
						});

						col.Item().PaddingTop(8).LineHorizontal(1).LineColor(border);
					});

					// ── Body ──
					page.Content().PaddingTop(10).Column(col =>
					{
						col.Item().Row(row =>
						{
							void SummaryCard(string label, decimal value, Color color, bool last = false)
							{
								row.RelativeItem().Background(rowAlt).Padding(6).PaddingRight(last ? 6 : 3).Column(c =>
								{
									c.Item().Text(label).FontSize(6.5f).FontColor(muted).Bold();
									c.Item().PaddingTop(2).Text($"KSh {value:N2}").FontSize(9.5f).Bold().FontColor(color);
								});
								if (!last) row.ConstantItem(3);
							}

							SummaryCard("OPENING BALANCE", statement.OpeningBalance, ink);
							SummaryCard("TOTAL CREDITS", statement.TotalCredits, success);
							SummaryCard("TOTAL DEBITS", statement.TotalDebits, danger);
							SummaryCard("CLOSING BALANCE", statement.ClosingBalance, primary, last: true);
						});

						col.Item().PaddingTop(12);

						col.Item().Table(table =>
						{
							table.ColumnsDefinition(c =>
							{
								c.RelativeColumn(2.1f);  // Date
								c.RelativeColumn(2.2f);  // Reference
								c.RelativeColumn(1.5f);  // Vehicle
								c.RelativeColumn(3.0f);  // Narration
								c.RelativeColumn(1.3f);  // Credit
								c.RelativeColumn(1.3f);  // Debit
								c.RelativeColumn(1.5f);  // Balance
							});

							table.Header(header =>
							{
								void HeaderCell(string text) =>
									header.Cell().Background(primary).Padding(4)
										.Text(text).FontSize(7).Bold().FontColor(Colors.White);

								HeaderCell("Date");
								HeaderCell("Reference");
								HeaderCell("Vehicle");
								HeaderCell("Narration");
								HeaderCell("Credit");
								HeaderCell("Debit");
								HeaderCell("Balance");
							});

							if (statement.Lines.Count == 0)
							{
								table.Cell().ColumnSpan(7).Padding(12).AlignCenter()
									.Text("No transactions in this period.").FontColor(muted).Italic();
							}
							else
							{
								for (int i = 0; i < statement.Lines.Count; i++)
								{
									var line = statement.Lines[i];
									var bg = i % 2 == 0 ? Colors.White : rowAlt;

									table.Cell().Background(bg).Padding(4)
										.Text(line.Date.ToString("dd MMM yy, HH:mm")).FontSize(7.5f);
									table.Cell().Background(bg).Padding(4)
										.Text(line.TransactionReference ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4)
										.Text(line.RegistrationNumber ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4)
										.Text(line.Narration ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text(line.Credit > 0 ? $"{line.Credit:N2}" : "\u2014")
										.FontSize(7.5f).FontColor(line.Credit > 0 ? success : muted);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text(line.Debit > 0 ? $"{line.Debit:N2}" : "\u2014")
										.FontSize(7.5f).FontColor(line.Debit > 0 ? danger : muted);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text($"{line.RunningBalance:N2}").FontSize(7.5f).Bold();
								}

								table.Footer(footer =>
								{
									footer.Cell().ColumnSpan(6).Background(rowAlt).Padding(5).AlignRight()
										.Text("Closing Balance").FontSize(8).Bold();
									footer.Cell().Background(rowAlt).Padding(5).AlignRight()
										.Text($"KSh {statement.ClosingBalance:N2}").FontSize(8).Bold().FontColor(primary);
								});
							}
						});
					});

					// ── Footer ──
					page.Footer().PaddingTop(6).Column(col =>
					{
						col.Item().LineHorizontal(0.5f).LineColor(border);
						col.Item().PaddingTop(3).Row(row =>
						{
							row.RelativeItem().Text("Trio Fuels \u00b7 Nairobi, Kenya").FontSize(6.5f).FontColor(muted);
							row.RelativeItem().AlignRight().Text(t =>
							{
								t.Span("Page ").FontSize(6.5f).FontColor(muted);
								t.CurrentPageNumber().FontSize(6.5f).FontColor(muted);
								t.Span(" of ").FontSize(6.5f).FontColor(muted);
								t.TotalPages().FontSize(6.5f).FontColor(muted);
							});
						});
					});
				});
			});

			return document.GeneratePdf();
		}
	}
}