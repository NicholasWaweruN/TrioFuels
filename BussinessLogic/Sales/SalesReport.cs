using BusinessLogic.SetupService;
using BussinessLogic.Setup;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sales
{
	public class SalesReport
	{
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		public SalesReport(OTOContext context, ICommonSetups setups)
		{
			_context = context;
			_setups = setups;
		}

		//public async Task<ServiceResponse<object>> ExportSalesToExcel()
		//{
		//	try
		//	{
		//		var response = new ServiceResponse<object>();

		//		int batchSize = 5000; // Adjust the batch size as needed
		//		int totalRecords = await _context.QuantityTransactions.CountAsync();
		//		int totalBatches = (int)Math.Ceiling((double)totalRecords / batchSize);

		//		using (var workbook = new XLWorkbook())
		//		{
		//			var worksheet = workbook.Worksheets.Add("Sales Data");

		//			// Define Excel column headers
		//			worksheet.Cell(1, 1).Value = "Sale ID";
		//			worksheet.Cell(1, 2).Value = "Sale Date";
		//			worksheet.Cell(1, 3).Value = "Payment References";
		//			worksheet.Cell(1, 4).Value = "Station Name";
		//			worksheet.Cell(1, 5).Value = "User Name";
		//			worksheet.Cell(1, 6).Value = "Customer Name";
		//			worksheet.Cell(1, 7).Value = "Till Number";
		//			worksheet.Cell(1, 8).Value = "Terminal";
		//			worksheet.Cell(1, 9).Value = "Shift Number";
		//			worksheet.Cell(1, 10).Value = "Vehicle Registration";
		//			worksheet.Cell(1, 11).Value = "Payment Type";
		//			worksheet.Cell(1, 12).Value = "Quantity";
		//			worksheet.Cell(1, 13).Value = "Price";
		//			worksheet.Cell(1, 14).Value = "Amount";
		//			worksheet.Cell(1, 15).Value = "Dispenser Name";
		//			worksheet.Cell(1, 16).Value = "Nozzle Name";
		//			worksheet.Cell(1, 17).Value = "Storage Location";

		//			int currentRow = 2;

		//			var paymentReferences = await (from pt in _context.PaymentTransactions
		//										   group pt by pt.SaleId into g
		//										   select new
		//										   {
		//											   SaleId = g.Key,
		//											   PaymentRefrences = string.Join(",", g.Select(x => x.PaymentRefrence))
		//										   }).ToListAsync();

		//			var Qt = await (from q in _context.QuantityTransactions
		//							group q by q.SaleId into g
		//							select new
		//							{
		//								SalesId = g.Key,
		//								Quantity = g.Sum(x => x.QuantityCredit- x.QuantityCredit),
		//								Amount = g.Sum(x => x.AmountCredit - x.AmountDebit),
		//								DispenserCode = g.Select(x => x.DispenserCode).FirstOrDefault(),
		//								NozzleCode = g.Select(x => x.NozzleCode).FirstOrDefault(),
		//								VehicleCode = g.Select(x => x.VehicleCode).FirstOrDefault(),
		//								PaymentTypeCode = g.Select(x => x.PaymentTypeCode).FirstOrDefault(),
		//								UserCode = g.Select(x => x.UserCode).FirstOrDefault(),
		//								Date = g.Select(x => x.DateCreated).FirstOrDefault()
		//							}
		//							 ).ToListAsync();

		//			var salesdata = (from q in Qt
		//							join p in paymentReferences on q.SalesId equals p.SaleId
		//							select new
		//							{
		//								q.SalesId,
		//								q.Quantity,
		//								q.Amount,
		//								p.PaymentRefrences,
		//								q.DispenserCode,
		//								q.NozzleCode,
		//								q.VehicleCode,
		//								q.PaymentTypeCode,
		//								q.UserCode,
		//								q.Date
		//							}).ToList();


		//			for (int i = 0; i < totalBatches; i++)
		//			{
		//				// Fetch payments records group by saleid and put payment references in a single column separated by comma
					



		//				var salesBatch = await (from q in salesdata
		//										join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
		//										join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
		//										join s in _context.Stations on d.StationCode equals s.StationCode
		//										join v in _context.Vehicles on q.VehicleCode equals v.VehicleCode
		//										join c in _context.Customers on v.CustomerCode equals c.CustomerCode
		//										join p in _context.PaymentTypes on q.PaymentTypeCode equals p.PaymentTypeId
		//										join pts in _context.PaymentTypes on q.PaymentTypeCode equals pts.PaymentTypeId
		//										join u in _context.Users on q.UserCode equals u.UserCode
		//										where q.Date.Date == DateTime.UtcNow.Date.AddDays(-1)
		//										select new
		//										{
		//											q.SalesId,
		//											SaleDate = p.DateCreated,
		//											pt.PaymentRefrences,
		//											s.StationName,
		//											Name = string.Join(' ', u.FirstName, u.MiddName, u.LastName),
		//											c.CustomerName,
		//											d.TillNumber,
		//											Terminal = s.StationName,
		//											q.ShiftNumber,
		//											v.VehicleRegistrationNumber,
		//											pts.PaymentTypeName,
		//											Quantity = q.QuantityCredit == 0 ? -q.QuantityDebit : q.QuantityCredit,
		//											q.Price,
		//											Amount = q.AmountCredit == 0 ? -q.AmountDebit : q.AmountCredit,
		//											d.DispenserName,
		//											n.NozzleName,
		//											d.StorageLocation
		//										})
		//										.Skip(i * batchSize)
		//										.Take(batchSize)
		//										.ToListAsync();

		//				// Write each batch to the Excel file
		//				foreach (var sale in salesBatch)
		//				{
		//					worksheet.Cell(currentRow, 1).Value = sale.SaleId;
		//					worksheet.Cell(currentRow, 2).Value = sale.SaleDate;
		//					worksheet.Cell(currentRow, 3).Value = sale.PaymentRefrences;
		//					worksheet.Cell(currentRow, 4).Value = sale.StationName;
		//					worksheet.Cell(currentRow, 5).Value = sale.Name;
		//					worksheet.Cell(currentRow, 6).Value = sale.CustomerName;
		//					worksheet.Cell(currentRow, 7).Value = sale.TillNumber;
		//					worksheet.Cell(currentRow, 8).Value = sale.Terminal;
		//					worksheet.Cell(currentRow, 9).Value = sale.ShiftNumber;
		//					worksheet.Cell(currentRow, 10).Value = sale.VehicleRegistrationNumber;
		//					worksheet.Cell(currentRow, 11).Value = sale.PaymentTypeName;
		//					worksheet.Cell(currentRow, 12).Value = sale.Quantity;
		//					worksheet.Cell(currentRow, 13).Value = sale.Price;
		//					worksheet.Cell(currentRow, 14).Value = sale.Amount;
		//					worksheet.Cell(currentRow, 15).Value = sale.DispenserName;
		//					worksheet.Cell(currentRow, 16).Value = sale.NozzleName;
		//					worksheet.Cell(currentRow, 17).Value = sale.StorageLocation;

		//					currentRow++;
		//				}
		//			}

		//			// Auto-fit columns for better readability
		//			worksheet.Columns().AdjustToContents();
		//			//worksheet.Rows().AdjustToContents();
		//			worksheet.Rows().Style.Alignment.WrapText = true;

		//			// Save the Excel file to a location
		//			var filePath = "/path/to/your/sales_data.xlsx";
		//			workbook.SaveAs(filePath);

		//			return ServiceResponse<object>.Success("Sales exported successfully", filePath);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return ServiceResponse<object>.Error($"An error occurred while exporting sales: {ex.Message}", null);
		//	}
		//}
	}
}
