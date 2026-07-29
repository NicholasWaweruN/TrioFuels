
using ClosedXML.Excel;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Credit;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.Credit_Management
{

	public interface ICreditStatementService
	{
		Task<CreditStatementDto?> GetCreditStatementAsync(string customerCode, DateTime fromDate, DateTime toDate);
		byte[] BuildStatementExcel(CreditStatementDto statement);
		byte[] BuildStatementPdf(CreditStatementDto statement);
	}
	public class CreditStatementService : ICreditStatementService
	{
		private readonly OTOContext _context;

		public CreditStatementService(OTOContext context)
		{
			_context = context;
		}

		public async Task<CreditStatementDto?> GetCreditStatementAsync(
			string customerCode, DateTime fromDate, DateTime toDate)
		{
			var customer = await _context.Customers
				.Where(c => c.CustomerCode == customerCode)
				.Select(c => new { c.CustomerCode, c.CustomerName, c.CustomerPhone })
				.FirstOrDefaultAsync();

			if (customer == null)
				return null;

			// Opening balance mirrors GetOutstandingCreditAsync's sign convention:
			// Debit increases exposure, Credit (repayments) reduces it.
			var openingBalance = await _context.CreditTransactions
				.Where(t => t.CustomerCode == customerCode && t.DateCreated < fromDate)
				.SumAsync(t => (decimal?)(t.Debit - t.Credit)) ?? 0m;

			// Left join to Vehicle so a transaction with no vehicle match
			// (or a vehicle later deleted) still shows up on the statement.
			var lines = await (
				from t in _context.CreditTransactions
				join v in _context.Vehicles on t.VehicleCode equals v.VehicleCode into vehicleJoin
				from v in vehicleJoin.DefaultIfEmpty()
				where t.CustomerCode == customerCode
					  && t.DateCreated >= fromDate
					  && t.DateCreated <= toDate
				orderby t.DateCreated

				select new CreditStatementLineDto
				{
					Date = t.DateCreated,
					TransactionReference = t.TransactionReference,
					SaleId = t.SaleId,
					VehicleCode = t.VehicleCode,
					RegistrationNumber = v != null ? v.VehicleRegistrationNumber : null,
					StationCode = t.StationCode,
					UserCode = t.UserCode,
					Debit = t.Debit,
					Credit = t.Credit,
				}
			).ToListAsync();

			var running = openingBalance;
			foreach (var line in lines)
			{
				running += line.Debit - line.Credit;
				line.RunningBalance = running;
			}

			return new CreditStatementDto
			{
				CustomerCode = customer.CustomerCode,
				CustomerName = customer.CustomerName,
				CustomerPhone = customer.CustomerPhone,
				FromDate = fromDate,
				ToDate = toDate,
				OpeningBalance = openingBalance,
				TotalCharges = lines.Sum(l => l.Debit),
				TotalRepayments = lines.Sum(l => l.Credit),
				ClosingBalance = running,
				Lines = lines,
			};
		}

		/* ---------------------------------------------------------- */
		/* Excel export — ClosedXML, same pattern as the wallet report */
		/* ---------------------------------------------------------- */
		public byte[] BuildStatementExcel(CreditStatementDto statement)
		{
			using var workbook = new XLWorkbook();
			var ws = workbook.Worksheets.Add("Credit Statement");

			ws.Cell(1, 1).Value = "Customer Credit Statement";
			ws.Cell(1, 1).Style.Font.SetBold().Font.FontSize = 14;
			ws.Cell(2, 1).Value = $"{statement.CustomerName} ({statement.CustomerCode})";
			ws.Cell(3, 1).Value = statement.CustomerPhone ?? "";
			ws.Cell(4, 1).Value = $"Period: {statement.FromDate:dd MMM yyyy} – {statement.ToDate:dd MMM yyyy}";

			var headerRow = 6;
			string[] headers = { "Date", "Reference", "Sale/Repayment Ref", "Vehicle Reg.", "Station", "Charged", "Repaid", "Balance Owed" };
			for (int i = 0; i < headers.Length; i++)
			{
				var cell = ws.Cell(headerRow, i + 1);
				cell.Value = headers[i];
				cell.Style.Font.SetBold();
				cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#7a1c1c");
				cell.Style.Font.FontColor = XLColor.White;
			}

			var row = headerRow + 1;
			foreach (var line in statement.Lines)
			{
				ws.Cell(row, 1).Value = line.Date;
				ws.Cell(row, 1).Style.DateFormat.Format = "dd-MMM-yyyy HH:mm";
				ws.Cell(row, 2).Value = line.TransactionReference;
				ws.Cell(row, 3).Value = line.SaleId;
				ws.Cell(row, 4).Value = line.RegistrationNumber ?? "—";
				ws.Cell(row, 5).Value = line.StationCode ?? "—";
				ws.Cell(row, 6).Value = line.Debit;
				ws.Cell(row, 7).Value = line.Credit;
				ws.Cell(row, 8).Value = line.RunningBalance;
				row++;
			}

			ws.Range(headerRow + 1, 6, row - 1, 8).Style.NumberFormat.Format = "#,##0.00";
			ws.Cell(row, 5).Value = "Closing balance owed";
			ws.Cell(row, 5).Style.Font.SetBold();
			ws.Cell(row, 8).Value = statement.ClosingBalance;
			ws.Cell(row, 8).Style.Font.SetBold();
			ws.Cell(row, 8).Style.NumberFormat.Format = "#,##0.00";

			ws.Columns().AdjustToContents();

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			return stream.ToArray();
		}

		/* ---------------------------------------------------------- */
		/* PDF export — QuestPDF                                       */
		/* ---------------------------------------------------------- */
		public byte[] BuildStatementPdf(CreditStatementDto statement)
		{
			QuestPDF.Settings.License = LicenseType.Community;

			var logoPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "assets", "trio-fuels-logo.png");
			byte[]? logoBytes = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : null;

			var ink = Color.FromHex("#14224F");
			var primary = Color.FromHex("#7A1C1C");
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

						col.Item().PaddingTop(8).Text("Customer Credit Statement")
							.Bold().FontSize(12).FontColor(ink);

						col.Item().PaddingTop(6).Row(row =>
						{
							row.RelativeItem().Column(c =>
							{
								c.Item().Text("CUSTOMER DETAILS").FontSize(6.5f).FontColor(muted).Bold();
								c.Item().Text(statement.CustomerName ?? "—").FontSize(9).Bold();
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

							SummaryCard("OPENING BALANCE OWED", statement.OpeningBalance, ink);
							SummaryCard("TOTAL CHARGED", statement.TotalCharges, danger);
							SummaryCard("TOTAL REPAID", statement.TotalRepayments, success);
							SummaryCard("CLOSING BALANCE OWED", statement.ClosingBalance, primary, last: true);
						});

						col.Item().PaddingTop(12);

						col.Item().Table(table =>
						{
							table.ColumnsDefinition(c =>
							{
								c.RelativeColumn(2.0f);  // Date
								c.RelativeColumn(2.0f);  // Reference
								c.RelativeColumn(1.8f);  // Sale/Repayment Ref
								c.RelativeColumn(1.4f);  // Vehicle
								c.RelativeColumn(1.4f);  // Station
								c.RelativeColumn(1.3f);  // Charged
								c.RelativeColumn(1.3f);  // Repaid
								c.RelativeColumn(1.6f);  // Balance
							});

							table.Header(header =>
							{
								void HeaderCell(string text) =>
									header.Cell().Background(primary).Padding(4)
										.Text(text).FontSize(7).Bold().FontColor(Colors.White);

								HeaderCell("Date");
								HeaderCell("Reference");
								HeaderCell("Sale/Repay Ref");
								HeaderCell("Vehicle");
								HeaderCell("Station");
								HeaderCell("Charged");
								HeaderCell("Repaid");
								HeaderCell("Balance Owed");
							});

							if (statement.Lines.Count == 0)
							{
								table.Cell().ColumnSpan(8).Padding(12).AlignCenter()
									.Text("No credit activity in this period.").FontColor(muted).Italic();
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
										.Text(line.SaleId ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4)
										.Text(line.RegistrationNumber ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4)
										.Text(line.StationCode ?? "—").FontSize(7.5f);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text(line.Debit > 0 ? $"{line.Debit:N2}" : "\u2014")
										.FontSize(7.5f).FontColor(line.Debit > 0 ? danger : muted);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text(line.Credit > 0 ? $"{line.Credit:N2}" : "\u2014")
										.FontSize(7.5f).FontColor(line.Credit > 0 ? success : muted);
									table.Cell().Background(bg).Padding(4).AlignRight()
										.Text($"{line.RunningBalance:N2}").FontSize(7.5f).Bold();
								}

								table.Footer(footer =>
								{
									footer.Cell().ColumnSpan(7).Background(rowAlt).Padding(5).AlignRight()
										.Text("Closing Balance Owed").FontSize(8).Bold();
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

	public class CreditStatementLineDto
	{
		public DateTime Date { get; set; }
		public string? TransactionReference { get; set; }

		/// <summary>
		/// SaleId on the CreditTransactions row. For an actual credit sale this is
		/// the real sale id; for a repayment it's the repayment reference generated
		/// by RepayCreditAsync (there's no underlying sale to point to).
		/// </summary>
		public string? SaleId { get; set; }

		public string? VehicleCode { get; set; }
		public string? RegistrationNumber { get; set; }
		public string? StationCode { get; set; }
		public string? UserCode { get; set; }

		/// <summary>Charge that increased the outstanding balance (credit sale).</summary>
		public decimal Debit { get; set; }

		/// <summary>Repayment that reduced the outstanding balance.</summary>
		public decimal Credit { get; set; }

		public decimal RunningBalance { get; set; }
	}

	public class CreditStatementDto
	{
		public string CustomerCode { get; set; } = string.Empty;
		public string? CustomerName { get; set; }
		public string? CustomerPhone { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		/// <summary>Amount owed as of the moment before FromDate (Debit - Credit, net of all prior activity).</summary>
		public decimal OpeningBalance { get; set; }

		/// <summary>Sum of charges (Debit) within the period — new credit extended.</summary>
		public decimal TotalCharges { get; set; }

		/// <summary>Sum of repayments (Credit) within the period.</summary>
		public decimal TotalRepayments { get; set; }

		/// <summary>Amount still owed at ToDate. Negative = customer is in credit surplus.</summary>
		public decimal ClosingBalance { get; set; }

		public List<CreditStatementLineDto> Lines { get; set; } = new();
	}
}




