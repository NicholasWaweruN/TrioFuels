using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Worker.RecordedTotalizer_Readings
{
	public class TotalizerDailyReport : ITotalizerDailyReport
	{

		private readonly OTOContext _context;
		public TotalizerDailyReport(OTOContext context)
		{
			_context = context;
		}
		public async Task<MemoryStream> GenerateStyledTotalizerExcelWithAllDaysAsync()
		{
			var today = DateTime.Today;
			var year = today.Year;
			var month = today.Month;
			var lastDay = today.Day;

			using var package = new ExcelPackage();

			for (int day = 1; day <= lastDay; day++)
			{
				var currentDate = new DateTime(year, month, day);
				string sheetName = currentDate.ToString("dd");

				var data = await (
					from t in _context.TotalizerReadings
					join z in _context.Nozzles on t.NozzlesCode equals z.NozzleCode
					join d in _context.Dispensers on z.DispenserCode equals d.DispenserCode
					join s in _context.Stations on d.StationCode equals s.StationCode
					where t.DateCreated.Date == currentDate
						  && t.NozzlesCode != "N07"
						  && t.NozzlesCode != "N08"
					select new
					{
						Station = s.StationName,
						Dispenser = d.DispenserName,
						Nozzle = z.NozzleName,
						Reading = t.Reading,
						Date = t.DateCreated
					}
				).ToListAsync();

				if (data.Count == 0)
					continue;

				var sheet = package.Workbook.Worksheets.Add(sheetName);

				// === Title Row ===
				sheet.Cells["A1:E1"].Merge = true;
				var titleCell = sheet.Cells["A1"];
				titleCell.Value = $"Totalizer Readings for {currentDate:dd MMMM yyyy}";
				titleCell.Style.Font.Bold = true;
				titleCell.Style.Font.Size = 14;
				titleCell.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFCCE5")); // Pink font
				titleCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
				titleCell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#003366")); // Dark blue bg
				titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				titleCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				sheet.Row(1).Height = 25;

				// === Header Row ===
				var headers = new[] { "Station Name", "Dispenser Name", "Nozzle Name", "Reading", "Date Created" };
				for (int i = 0; i < headers.Length; i++)
				{
					var cell = sheet.Cells[2, i + 1];
					cell.Value = headers[i];
					cell.Style.Font.Bold = true;
					cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#003366")); // Dark blue font
					cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
					cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFCCE5")); // Pink background
					cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				}

				// === Data Rows ===
				for (int i = 0; i < data.Count; i++)
				{
					var row = i + 3; // Start from row 3
					sheet.Cells[row, 1].Value = data[i].Station;
					sheet.Cells[row, 2].Value = data[i].Dispenser;
					sheet.Cells[row, 3].Value = data[i].Nozzle;
					sheet.Cells[row, 4].Value = data[i].Reading;
					sheet.Cells[row, 5].Value = data[i].Date.ToString("yyyy-MM-dd HH:mm:ss");

					for (int col = 1; col <= 5; col++)
					{
						var cell = sheet.Cells[row, col];
						cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
						cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
						cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					}
				}

				sheet.Cells.AutoFitColumns();
			}

			var stream = new MemoryStream();
			await package.SaveAsAsync(stream);
			stream.Position = 0;
			return stream;
		}

	}
}
