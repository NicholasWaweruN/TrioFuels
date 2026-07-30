using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Db_Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogic.Sales.SalesData
{
	public class VehicleSales : IVehicleSales
	{
		private readonly OTOContext _context;
		public VehicleSales(OTOContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<object>> GetFuelSalesAsync(FuelSaleFilterDto filter)
		{
			if (string.IsNullOrWhiteSpace(filter.Vehicle) && string.IsNullOrWhiteSpace(filter.PhoneNumber))
				return ServiceResponse<object>.Information("Provide either a vehicle or a phone number to search by", null);

			var query = _context.FuelSales.AsNoTracking().AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Vehicle))
				query = query.Where(f => f.Vehicle == filter.Vehicle);
			else
				query = query.Where(f => f.CustomerPhone == filter.PhoneNumber);

			if (filter.FromDate.HasValue)
				query = query.Where(f => f.SalesDate >= filter.FromDate.Value);

			if (filter.ToDate.HasValue)
				query = query.Where(f => f.SalesDate <= filter.ToDate.Value);

			var results = await query
				.OrderByDescending(f => f.SalesDate)
				.ToListAsync();

			if (!results.Any())
				return ServiceResponse<object>.Information("No fuel sales found for the given filter", null);

			return ServiceResponse<object>.Success("Fuel sales retrieved successfully", results);
		}

		public async Task<ServiceResponse<byte[]>> ExportFuelSalesToExcelAsync(FuelSaleFilterDto filter)
		{
			var result = await GetFuelSalesAsync(filter);

			if (result.ResponseCode != Response.Success)
				return ServiceResponse<byte[]>.Information(result.ResponseMessage!, null);

			var sales = (List<FuelSale>)result.ResponseObject!;

			using var workbook = new XLWorkbook();
			var sheet = workbook.Worksheets.Add("Fuel Sales");

			var headers = new[]
			{
		"Vehicle", "Shift Number", "Sale Id", "Station", "Dispenser",
		"Storage Location", "Nozzle", "Attendant", "Customer", "Phone",
		"Petroleum", "Payment Type", "Litres", "Price", "Amount",
		"Sales Date", "Till Number", "Trans Id", "Running Balance", "Reversed"
	};

			for (int i = 0; i < headers.Length; i++)
				sheet.Cell(1, i + 1).Value = headers[i];

			sheet.Row(1).Style.Font.Bold = true;

			int row = 2;
			foreach (var s in sales)
			{
				sheet.Cell(row, 1).Value = s.Vehicle;
				sheet.Cell(row, 2).Value = s.ShiftNumber;
				sheet.Cell(row, 3).Value = s.SaleId;
				sheet.Cell(row, 4).Value = s.StationName;
				sheet.Cell(row, 5).Value = s.DispenserName;
				sheet.Cell(row, 6).Value = s.StorageLocation;
				sheet.Cell(row, 7).Value = s.NozzleName;
				sheet.Cell(row, 8).Value = s.AttendantName;
				sheet.Cell(row, 9).Value = s.CustomerName;
				sheet.Cell(row, 10).Value = s.CustomerPhone;
				sheet.Cell(row, 11).Value = s.PetroleumName;
				sheet.Cell(row, 12).Value = s.PaymentType;
				sheet.Cell(row, 13).Value = s.Litres;
				sheet.Cell(row, 14).Value = s.Price;
				sheet.Cell(row, 15).Value = s.Amount;
				sheet.Cell(row, 16).Value = s.SalesDate;
				sheet.Cell(row, 16).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
				sheet.Cell(row, 17).Value = s.TillNumber;
				sheet.Cell(row, 18).Value = s.TransId;
				sheet.Cell(row, 19).Value = s.RunningBalance;
				sheet.Cell(row, 20).Value = s.IsReversed ? "Yes" : "No";
				row++;
			}

			sheet.Columns().AdjustToContents();

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);

			return ServiceResponse<byte[]>.Success("Excel file generated", stream.ToArray());
		}
	}

	public class FuelSaleFilterDto
	{
		public string? Vehicle { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
