using ClosedXML.Excel;
using DataAccessLayer.Context;
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
		public async Task<List<MpesaStatementLineDto>> GetMpesaStatementAsync(
			string? tillNumber = null,
			DateOnly? from = null,
			DateOnly? to = null,
			CancellationToken ct = default)
		{
			var query = _context.MpesaTransactions
				.AsNoTracking()
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(tillNumber))
				query = query.Where(t => t.TillNumber == tillNumber);

			if (from.HasValue)
				query = query.Where(t => t.TransTime.Date >= from.Value.ToDateTime(TimeOnly.MinValue));

			if (to.HasValue)
				query = query.Where(t => t.TransTime.Date <= to.Value.ToDateTime(TimeOnly.MinValue));

			return await query
				.OrderByDescending(t => t.Id)
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
			var lines = await GetMpesaStatementAsync(tillNumber, from, to, ct);

			using var workbook = new XLWorkbook();
			var sheet = workbook.Worksheets.Add("M-Pesa Statement");

			// Header block
			sheet.Cell(1, 1).Value = "M-Pesa Statement";
			sheet.Cell(1, 1).Style.Font.Bold = true;
			sheet.Cell(1, 1).Style.Font.FontSize = 14;

			var periodLabel = (from.HasValue || to.HasValue)
				? $"Period: {from?.ToString("dd-MMM-yyyy") ?? "…"} to {to?.ToString("dd-MMM-yyyy") ?? "…"}"
				: "Period: All";
			sheet.Cell(2, 1).Value = periodLabel;

			if (!string.IsNullOrWhiteSpace(tillNumber))
				sheet.Cell(3, 1).Value = $"Till: {tillNumber}";

			// Table headers
			var headerRow = 5;
			string[] headers = { "Trans ID", "Date", "Time", "Amount (KES)", "Store No.", "Till No.", "Name" };
			for (var i = 0; i < headers.Length; i++)
			{
				var cell = sheet.Cell(headerRow, i + 1);
				cell.Value = headers[i];
				cell.Style.Font.Bold = true;
				cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");
				cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
			}

			// Data rows
			var row = headerRow + 1;
			foreach (var line in lines)
			{
				sheet.Cell(row, 1).Value = line.TransId;

				var dateCell = sheet.Cell(row, 2);
				dateCell.Value = line.TransTime.Date;              // DateTime, not DateOnly
				dateCell.Style.DateFormat.Format = "dd-MMM-yyyy";

				var timeCell = sheet.Cell(row, 3);
				timeCell.Value = line.TransTime.TimeOfDay;         // TimeSpan, not TimeOnly
				timeCell.Style.DateFormat.Format = "HH:mm:ss";

				var amountCell = sheet.Cell(row, 4);
				amountCell.Value = line.TransAmount;
				amountCell.Style.NumberFormat.Format = "#,##0.00";

				sheet.Cell(row, 5).Value = line.StoreNumber;
				sheet.Cell(row, 6).Value = line.TillNumber;
				sheet.Cell(row, 7).Value = line.Name;

				row++;
			}

			// Totals row
			sheet.Cell(row, 3).Value = "Total";
			sheet.Cell(row, 3).Style.Font.Bold = true;
			sheet.Cell(row, 4).FormulaA1 = $"=SUM(D{headerRow + 1}:D{row - 1})";
			sheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0.00";
			sheet.Cell(row, 4).Style.Font.Bold = true;

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
