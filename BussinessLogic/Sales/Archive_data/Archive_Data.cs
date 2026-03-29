using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.Archirve;
using ClosedXML.Excel;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sales.Archive_data
{
	public class Archive_Data
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		public Archive_Data(OTOContext context,IAuthCommonTasks tasks)
		{
			_context = context;
			_authentication = tasks;
		}

		//Get data from SalesTransactions filter by Month
		public async Task<ServiceResponse<byte[]>> GetSalesTransactionsByMonth(ArchiveDataDto archive)
		{
			try
			{
				var sales = await _context.SalesTransactions.Where(x => x.Date.Month == archive.Month && x.Date.Year == archive.Year).ToListAsync();

				if (sales.Count == 0)
					return ServiceResponse<byte[]>.Information("No sales transactions found for the selected month", null);

				var workbook = new XLWorkbook();
				var worksheet = workbook.AddWorksheet("Sales Data");
				worksheet.Cell(1, 1).Value = "Order Id";
				worksheet.Cell(1, 2).Value = "Date";
				worksheet.Cell(1, 3).Value = "Transaction Id";
				worksheet.Cell(1, 4).Value = "Outlet";
				worksheet.Cell(1, 5).Value = "Attendant";
				worksheet.Cell(1, 6).Value = "Customer Name";
				worksheet.Cell(1, 7).Value = "Till Number";
				worksheet.Cell(1, 8).Value = "Terminal Name";
				worksheet.Cell(1, 9).Value = "Shift Number";
				worksheet.Cell(1, 10).Value = "Registration Number";
				worksheet.Cell(1, 11).Value = "Product";
				worksheet.Cell(1, 12).Value = "Payment Type";
				worksheet.Cell(1, 13).Value = "Quantity";
				worksheet.Cell(1, 14).Value = "Unit Price";
				worksheet.Cell(1, 15).Value = "Running Reading";
				worksheet.Cell(1, 16).Value = "Amount Paid";
				worksheet.Cell(1, 17).Value = "Dispenser Name";
				worksheet.Cell(1, 18).Value = "Nozzle Name";
				worksheet.Cell(1, 19).Value = "Storage Location";
				worksheet.Cell(1, 20).Value = "Original Date";

				worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightBlue;
				worksheet.Row(1).Style.Font.Bold = true;
				worksheet.Row(1).Style.Font.FontColor = XLColor.White;
				worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				worksheet.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
				worksheet.Row(1).Style.Font.FontSize = 12;

				int currentRow = 2;
				foreach (var sale in sales)
				{
					worksheet.Cell(currentRow, 1).Value = sale.OrderId;
					worksheet.Cell(currentRow, 2).Value = sale.Date;
					worksheet.Cell(currentRow, 3).Value = sale.TransactionId;
					worksheet.Cell(currentRow, 4).Value = sale.OutLet;
					worksheet.Cell(currentRow, 5).Value = sale.Attendant;
					worksheet.Cell(currentRow, 6).Value = sale.CustomerName;
					worksheet.Cell(currentRow, 7).Value = sale.TillNumber;
					worksheet.Cell(currentRow, 8).Value = sale.TerminalName;
					worksheet.Cell(currentRow, 9).Value = sale.ShiftNumber;
					worksheet.Cell(currentRow, 10).Value = sale.RegistrationNumber;
					worksheet.Cell(currentRow, 11).Value = sale.Product;
					worksheet.Cell(currentRow, 12).Value = sale.PaymentType;
					worksheet.Cell(currentRow, 13).Value = sale.Quantity;
					worksheet.Cell(currentRow, 14).Value = sale.UnitPrice;
					worksheet.Cell(currentRow, 15).Value = sale.RunningReading;
					worksheet.Cell(currentRow, 16).Value = sale.AmountPaid;
					worksheet.Cell(currentRow, 17).Value = sale.DispenserName;
					worksheet.Cell(currentRow, 18).Value = sale.NozzleName;
					worksheet.Cell(currentRow, 19).Value = sale.StorageLocation;
					worksheet.Cell(currentRow, 20).Value = sale.OriginalDate;
					currentRow++;
				}


				//auto-fit columns
				worksheet.Columns().AdjustToContents();
				//apply styling
				worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
				worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
				worksheet.Style.Font.FontSize = 12;
				//convert the workbook to a byte array
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);

				return ServiceResponse<byte[]>.Success("Sales Report Exported Successfully", stream.ToArray());


			}
			catch (Exception )
			{
				return ServiceResponse<byte[]>.Error("An error occured while exporting sales report", null);
			}

		}
		public async Task<ServiceResponse<byte[]>> GetSalesTransactionsDate(DateTime date)
		{
			try
			{
				var sales = await _context.SalesTransactions.Where(x => x.Date == date.Date).ToListAsync();

				if (sales.Count == 0)
					return ServiceResponse<byte[]>.Information("No sales transactions found for the selected month", null);

				var workbook = new XLWorkbook();
				var worksheet = workbook.AddWorksheet("Sales Data");
				worksheet.Cell(1, 1).Value = "Order Id";
				worksheet.Cell(1, 2).Value = "Date";
				worksheet.Cell(1, 3).Value = "Transaction Id";
				worksheet.Cell(1, 4).Value = "Outlet";
				worksheet.Cell(1, 5).Value = "Attendant";
				worksheet.Cell(1, 6).Value = "Customer Name";
				worksheet.Cell(1, 7).Value = "Till Number";
				worksheet.Cell(1, 8).Value = "Terminal Name";
				worksheet.Cell(1, 9).Value = "Shift Number";
				worksheet.Cell(1, 10).Value = "Registration Number";
				worksheet.Cell(1, 11).Value = "Product";
				worksheet.Cell(1, 12).Value = "Payment Type";
				worksheet.Cell(1, 13).Value = "Quantity";
				worksheet.Cell(1, 14).Value = "Unit Price";
				worksheet.Cell(1, 15).Value = "Running Reading";
				worksheet.Cell(1, 16).Value = "Amount Paid";
				worksheet.Cell(1, 17).Value = "Dispenser Name";
				worksheet.Cell(1, 18).Value = "Nozzle Name";
				worksheet.Cell(1, 19).Value = "Storage Location";
				worksheet.Cell(1, 20).Value = "Original Date";

				worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightBlue;
				worksheet.Row(1).Style.Font.Bold = true;
				worksheet.Row(1).Style.Font.FontColor = XLColor.White;
				worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				worksheet.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
				worksheet.Row(1).Style.Font.FontSize = 12;

				int currentRow = 2;
				foreach (var sale in sales)
				{
					worksheet.Cell(currentRow, 1).Value = sale.OrderId;
					worksheet.Cell(currentRow, 2).Value = sale.Date;
					worksheet.Cell(currentRow, 3).Value = sale.TransactionId;
					worksheet.Cell(currentRow, 4).Value = sale.OutLet;
					worksheet.Cell(currentRow, 5).Value = sale.Attendant;
					worksheet.Cell(currentRow, 6).Value = sale.CustomerName;
					worksheet.Cell(currentRow, 7).Value = sale.TillNumber;
					worksheet.Cell(currentRow, 8).Value = sale.TerminalName;
					worksheet.Cell(currentRow, 9).Value = sale.ShiftNumber;
					worksheet.Cell(currentRow, 10).Value = sale.RegistrationNumber;
					worksheet.Cell(currentRow, 11).Value = sale.Product;
					worksheet.Cell(currentRow, 12).Value = sale.PaymentType;
					worksheet.Cell(currentRow, 13).Value = sale.Quantity;
					worksheet.Cell(currentRow, 14).Value = sale.UnitPrice;
					worksheet.Cell(currentRow, 15).Value = sale.RunningReading;
					worksheet.Cell(currentRow, 16).Value = sale.AmountPaid;
					worksheet.Cell(currentRow, 17).Value = sale.DispenserName;
					worksheet.Cell(currentRow, 18).Value = sale.NozzleName;
					worksheet.Cell(currentRow, 19).Value = sale.StorageLocation;
					worksheet.Cell(currentRow, 20).Value = sale.OriginalDate;
					currentRow++;
				}

				//auto-fit columns
				worksheet.Columns().AdjustToContents();
				//apply styling
				worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
				worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
				worksheet.Style.Font.FontSize = 12;
				//convert the workbook to a byte array
				using var stream = new MemoryStream();
				workbook.SaveAs(stream);

				return ServiceResponse<byte[]>.Success("Sales Report Exported Successfully", stream.ToArray());


			}
			catch (Exception ex)
			{
				var method = ex.TargetSite;
				await _authentication.ErrorTrail(
								new ErrorTrail
								{
									DateCreated = DateTime.UtcNow,
									ErrorCode = "004",
									ErrorMessage = ex.Message,
									Method = method is null ? "" : method.Name

								});
				return ServiceResponse<byte[]>.Error("An error occured while exporting sales report", null);
			}

		}
		public class ArchiveDataDto
		{
			public int Month { get; set; }
			public int Year { get; set; }
		}
	}
}
