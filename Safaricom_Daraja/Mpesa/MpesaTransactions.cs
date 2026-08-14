using ClosedXML.Excel;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Safaricom_Daraja.Mpesa
{
	public class MpesaStatements : IMpesaStatements
	{
		private readonly OTOContext _context;
		public MpesaStatements(OTOContext context)
		{
			_context = context;
		}
		public class PagedResult<T>
		{
			public List<T> Items { get; set; } = new();
			public int TotalCount { get; set; }
			public int PageNumber { get; set; }
			public int PageSize { get; set; }
			public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
			public bool HasNextPage => PageNumber < TotalPages;
			public bool HasPreviousPage => PageNumber > 1;
		}

		public async Task<PagedResult<MpesaStatementLineDto>> GetMpesaStatementAsync(string? tillNumber = null,DateOnly? from = null,DateOnly? to = null,int pageNumber = 1,int pageSize = 50,CancellationToken ct = default)
		{
			// Clamp paging inputs — never trust caller-supplied page size on a public/reporting endpoint
			pageNumber = pageNumber < 1 ? 1 : pageNumber;
			pageSize = pageSize < 1 ? 50 : Math.Min(pageSize, 500);

			// Invalid range — from after to. Fail fast instead of silently returning garbage.
			if (from.HasValue && to.HasValue && from.Value > to.Value)
				throw new ArgumentException("'from' date cannot be later than 'to' date.");

			var query = _context.MpesaTransactions.AsNoTracking().AsQueryable();

			if (!string.IsNullOrWhiteSpace(tillNumber))
				query = query.Where(t => t.TillNumber == tillNumber.Trim());

			if (from.HasValue)
			{
				var fromDt = from.Value.ToDateTime(TimeOnly.MinValue);
				query = query.Where(t => t.TransTime >= fromDt);
			}

			if (to.HasValue)
			{
				// Exclusive upper bound at start of the NEXT day, not <= start of `to` day.
				// Original bug: t.TransTime.Date <= to.Value.ToDateTime(MinValue) silently
				// dropped every transaction on the "to" day after 00:00 — e.g. a statement
				// for "today" would show zero of today's transactions.
				var toExclusive = to.Value.AddDays(1).ToDateTime(TimeOnly.MinValue);
				query = query.Where(t => t.TransTime < toExclusive);
			}

			var totalCount = await query.CountAsync(ct);

			var items = await query
				.OrderByDescending(t => t.Id)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.Select(t => new MpesaStatementLineDto
				{
					TransId = t.TransID,
					TransTime = t.TransTime,
					TransAmount = t.TransAmount,
					StoreNumber = t.BusinessShortCode,
					TillNumber = t.TillNumber,
					Name = t.FirstName
				})
				.ToListAsync(ct);

			return new PagedResult<MpesaStatementLineDto>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			};
		}
		// Shared filter logic extracted so paging and export never drift apart
		private IQueryable<MpesaTransaction> BuildMpesaStatementQuery(
			string? tillNumber, DateOnly? from, DateOnly? to)
		{
			if (from.HasValue && to.HasValue && from.Value > to.Value)
				throw new ArgumentException("'from' date cannot be later than 'to' date.");

			var query = _context.MpesaTransactions.AsNoTracking().AsQueryable();

			if (!string.IsNullOrWhiteSpace(tillNumber))
				query = query.Where(t => t.TillNumber == tillNumber.Trim());

			if (from.HasValue)
			{
				var fromDt = from.Value.ToDateTime(TimeOnly.MinValue);
				query = query.Where(t => t.TransTime >= fromDt);
			}

			if (to.HasValue)
			{
				// Exclusive upper bound — same fix as the paged query, do not regress this
				var toExclusive = to.Value.AddDays(1).ToDateTime(TimeOnly.MinValue);
				query = query.Where(t => t.TransTime < toExclusive);
			}

			return query;
		}

		// Export needs every matching row, not a page — deliberately no Skip/Take.
		// Capped at a hard ceiling so a wide/unbounded date range can't blow up memory
		// or Excel row limits on a live production dataset.
		private const int MaxExportRows = 100_000;

		public async Task<List<MpesaStatementLineDto>> GetMpesaStatementForExportAsync(
			string? tillNumber = null,
			DateOnly? from = null,
			DateOnly? to = null,
			CancellationToken ct = default)
		{
			return await BuildMpesaStatementQuery(tillNumber, from, to)
				.OrderByDescending(t => t.Id)
				.Take(MaxExportRows)
				.Select(t => new MpesaStatementLineDto
				{
					TransId = t.TransID,
					TransTime = t.TransTime,
					TransAmount = t.TransAmount,
					StoreNumber = t.BusinessShortCode,
					TillNumber = t.TillNumber,
					Name = t.FirstName
				})
				.ToListAsync(ct);
		}

		public async Task<byte[]> ExportMpesaStatementAsync(
			string? tillNumber,
			DateOnly? from,
			DateOnly? to,
			CancellationToken ct = default)
		{
			var lines = await GetMpesaStatementForExportAsync(tillNumber, from, to, ct);

			// M-Pesa / Safaricom brand palette
			var mpesaGreen = XLColor.FromHtml("#00A651");
			var mpesaDarkGreen = XLColor.FromHtml("#007A3D");
			var mpesaLightGreen = XLColor.FromHtml("#E8F5E9");
			var mpesaRed = XLColor.FromHtml("#EF3340"); // used sparingly, e.g. zero/empty states

			using var workbook = new XLWorkbook();
			var sheet = workbook.Worksheets.Add("M-Pesa Statement");

			// Header block
			sheet.Cell(1, 1).Value = "M-PESA STATEMENT";
			sheet.Cell(1, 1).Style.Font.Bold = true;
			sheet.Cell(1, 1).Style.Font.FontSize = 16;
			sheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
			sheet.Cell(1, 1).Style.Fill.BackgroundColor = mpesaGreen;
			sheet.Range(1, 1, 1, 7).Merge();
			sheet.Row(1).Height = 24;

			var periodLabel = (from.HasValue || to.HasValue)
				? $"Period: {from?.ToString("dd-MMM-yyyy") ?? "…"} to {to?.ToString("dd-MMM-yyyy") ?? "…"}"
				: "Period: All";
			sheet.Cell(2, 1).Value = periodLabel;
			sheet.Cell(2, 1).Style.Font.FontColor = mpesaDarkGreen;
			sheet.Cell(2, 1).Style.Font.Italic = true;
			sheet.Range(2, 1, 2, 7).Merge();

			if (!string.IsNullOrWhiteSpace(tillNumber))
			{
				sheet.Cell(3, 1).Value = $"Till: {tillNumber}";
				sheet.Cell(3, 1).Style.Font.FontColor = mpesaDarkGreen;
				sheet.Range(3, 1, 3, 7).Merge();
			}

			if (lines.Count == MaxExportRows)
			{
				sheet.Cell(4, 1).Value =
					$"⚠ Export capped at {MaxExportRows:N0} rows — narrow the date range for a complete statement.";
				sheet.Cell(4, 1).Style.Font.FontColor = mpesaRed;
				sheet.Cell(4, 1).Style.Font.Italic = true;
				sheet.Range(4, 1, 4, 7).Merge();
			}

			// Table headers
			const int headerRow = 6;
			string[] headers = { "Trans ID", "Date & Time", "Amount (KES)", "Store No.", "Till No.", "Name", "" };
			for (var i = 0; i < headers.Length - 1; i++) // last col left blank/spacer, matches original 7-wide layout minus split date/time
			{
				var cell = sheet.Cell(headerRow, i + 1);
				cell.Value = headers[i];
				cell.Style.Font.Bold = true;
				cell.Style.Font.FontColor = XLColor.White;
				cell.Style.Fill.BackgroundColor = mpesaDarkGreen;
				cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
				cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
			}

			// Data rows
			var row = headerRow + 1;
			foreach (var line in lines)
			{
				sheet.Cell(row, 1).Value = line.TransId;

				var dateTimeCell = sheet.Cell(row, 2);
				dateTimeCell.Value = line.TransTime;                 // single DateTime, no split
				dateTimeCell.Style.DateFormat.Format = "dd-MMM-yyyy HH:mm:ss";

				var amountCell = sheet.Cell(row, 3);
				amountCell.Value = line.TransAmount;
				amountCell.Style.NumberFormat.Format = "#,##0.00";

				sheet.Cell(row, 4).Value = line.StoreNumber;
				sheet.Cell(row, 5).Value = line.TillNumber;
				sheet.Cell(row, 6).Value = line.Name;

				//
				// Zebra striping in a soft M-Pesa green tint for readability on long statements
				if ((row - headerRow) % 2 == 0)
				{
					sheet.Range(row, 1, row, 6).Style.Fill.BackgroundColor = mpesaLightGreen;
				}

				row++;
			}

			// Totals row
			sheet.Cell(row, 2).Value = "Total";
			sheet.Cell(row, 2).Style.Font.Bold = true;
			sheet.Cell(row, 2).Style.Font.FontColor = mpesaDarkGreen;
			sheet.Cell(row, 3).FormulaA1 = $"=SUM(C{headerRow + 1}:C{row - 1})";
			sheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0.00";
			sheet.Cell(row, 3).Style.Font.Bold = true;
			sheet.Range(row, 1, row, 6).Style.Border.TopBorder = XLBorderStyleValues.Medium;
			sheet.Range(row, 1, row, 6).Style.Border.TopBorderColor = mpesaGreen;

			sheet.Columns().AdjustToContents();
			sheet.SheetView.FreezeRows(headerRow);

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			return stream.ToArray();
		}
		public sealed class MpesaStatementLineDto
		{
			public string TransId { get; init; } = default!;
			public DateTime TransTime { get; init; }
			public decimal TransAmount { get; init; }
			public string StoreNumber { get; init; } = default!;
			public string TillNumber { get; init; } = default!;
			public string Name { get; init; } = default!;
		}
	}
}
