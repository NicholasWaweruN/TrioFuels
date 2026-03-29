using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Messaging;
using BusinessLogic.SetupService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Setup;

namespace BussinessLogic.Sales.Wallet.Voucher
{
	public class VoucherService : IVoucherService
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;
		private readonly IAfricaIsTalking _isTalking;

		public VoucherService(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups, IAfricaIsTalking isTalking)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
			_isTalking = isTalking;
		}
		public async Task<ServiceResponse<object>> UploadVouchersFromExcel(IFormFile file, string userCode)
		{
			if (file == null || file.Length == 0)
				return ServiceResponse<object>.Error("Excel file is missing.", null);

			try
			{
				using var stream = new MemoryStream();
				await file.CopyToAsync(stream);
				using var package = new ExcelPackage(stream);
				var worksheet = package.Workbook.Worksheets[0];
				var rowCount = worksheet.Dimension.Rows;

				var vouchers = new List<Vouchers>();


				var firstRow = worksheet.Cells[1, 1].Text?.Trim();
				if (string.IsNullOrEmpty(firstRow) || !decimal.TryParse(worksheet.Cells[1, 2].Text?.Trim(), out _))
					return ServiceResponse<object>.Error("Invalid Excel format. Ensure the first row contains headers.", null);
				
				for (int row = 2; row <= rowCount; row++)
				{
					var vehicleNumber = worksheet.Cells[row, 1].Text?.Trim();
					var amountText = worksheet.Cells[row, 2].Text?.Trim();

					if (string.IsNullOrEmpty(vehicleNumber) || string.IsNullOrEmpty(amountText) ||!decimal.TryParse(amountText, out var totalAmount) || totalAmount <= 0)
						continue;

					var vehicle = await _context.Vehicles.AsNoTracking()
						.FirstOrDefaultAsync(v => v.VehicleRegistrationNumber == vehicleNumber);

					if (vehicle == null)
						continue;

					switch (totalAmount)
					{
						case <= 1000:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, totalAmount, userCode));
							break;

						case 1500:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1000, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 500, userCode));
							break;

						case 2000:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1000, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1000, userCode));
							break;

						case 2500:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1500, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1000, userCode));
							break;

						case 3000:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 2000, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1000, userCode));
							break;

						case 3500:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 2000, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 1500, userCode));
							break;

						case 4000:
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 2000, userCode));
							vouchers.Add(CreateVoucher(vehicle.VehicleCode, 2000, userCode));
							break;

						default:
							// General logic: split into 2000 chunks, then remainder as-is
							decimal remaining = totalAmount;

							while (remaining >= 2000)
							{
								vouchers.Add(CreateVoucher(vehicle.VehicleCode, 2000, userCode));
								remaining -= 2000;
							}

							if (remaining > 0)
								vouchers.Add(CreateVoucher(vehicle.VehicleCode, remaining, userCode));

							break;
					}
				}

				if (vouchers.Count == 0)
					return ServiceResponse<object>.Information("No valid vouchers generated.", null);

				await _context.Vouchers.AddRangeAsync(vouchers);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success($"{vouchers.Count} vouchers uploaded successfully.", null);
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while processing the file.", null);
			}
		}


		private Vouchers CreateVoucher(string vehicleCode, decimal amount, string userCode)
		{
			return new Vouchers
			{
				VoucherNo = _setups.GenerateSaleId(),
				VehicleCode = vehicleCode,
				Amount = amount,
				IsUsed = false,
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(), // or pass in userCode
				ExpiryDate = DateTime.UtcNow.AddDays(30)
			};
		}

		public async Task<PaginatedResult<ActiveVoucherDto>> GetAllVouchersWithVehiclesAsync(int page = 1, int pageSize = 50)
		{
			var now = DateTime.UtcNow;

			var query = from v in _context.Vouchers
						join vehicle in _context.Vehicles on v.VehicleCode equals vehicle.VehicleCode
						select new ActiveVoucherDto
						{
							VoucherNo = v.VoucherNo,
							Amount = v.Amount,
							DateCreated = v.DateCreated,
							ExpiryDate = v.ExpiryDate,
							UserCode = v.UserCode,
							VehicleCode = vehicle.VehicleCode,
							VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
							Status = v.IsUsed ? "Used" :  v.ExpiryDate < now ? "Expired" : "Active"
						};

			var totalCount = await query.CountAsync();
			var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

			var data = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PaginatedResult<ActiveVoucherDto>
			{
				Page = page,
				PageSize = pageSize,
				TotalCount = totalCount,
				TotalPages = totalPages,
				Data = data
			};
		}



		public class PaginatedResult<T>
		{
			public int Page { get; set; }
			public int PageSize { get; set; }
			public int TotalCount { get; set; }
			public int TotalPages { get; set; }
			public List<T> Data { get; set; } = new();
		}


		public class ActiveVoucherDto
		{
			public string VoucherNo { get; set; } = string.Empty;
			public decimal Amount { get; set; }
			public DateTime DateCreated { get; set; }
			public DateTime? ExpiryDate { get; set; }
			public string UserCode { get; set; } = string.Empty;
			public string VehicleCode { get; set; } = string.Empty;
			public string VehicleRegistrationNumber { get; set; } = string.Empty;
			public string Status { get; set; } = string.Empty;// "Used", "Expired", or "Active"
		}


		public async Task<byte[]> GenerateActiveVouchersExcelAsync()
		{
			var activeVouchers = await (
				from v in _context.Vouchers
				join vehicle in _context.Vehicles on v.VehicleCode equals vehicle.VehicleCode
				where v.IsUsed == false
				select new
				{
					v.VoucherNo,
					v.Amount,
					v.DateCreated,
					v.ExpiryDate,
					v.UserCode,
					vehicle.VehicleRegistrationNumber
				}).ToListAsync();

			using var package = new ExcelPackage();
			var worksheet = package.Workbook.Worksheets.Add("Active Vouchers");

			// Title Row (Light Pink Background)
			worksheet.Cells["A1:F1"].Merge = true;
			worksheet.Cells["A1"].Value = "Active Vouchers Report";
			worksheet.Cells["A1"].Style.Font.Size = 14;
			worksheet.Cells["A1"].Style.Font.Bold = true;
			worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			worksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFCCE5")); // Light Pink
			worksheet.Cells["A1"].Style.Font.Color.SetColor(System.Drawing.Color.Black); // Title font now black for contrast

			// Header Row (Dark Blue)
			var headers = new[] { "Voucher No", "Amount", "Date Created", "Expiry Date", "User Code", "Vehicle Registration" };
			for (int col = 1; col <= headers.Length; col++)
			{
				var cell = worksheet.Cells[2, col];
				cell.Value = headers[col - 1];
				cell.Style.Font.Bold = true;
				cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
				cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#002060")); // Dark Blue
				cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
				cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
			}

			// Data Rows
			for (int i = 0; i < activeVouchers.Count; i++)
			{
				var row = i + 3;
				var item = activeVouchers[i];

				worksheet.Cells[row, 1].Value = item.VoucherNo;
				worksheet.Cells[row, 2].Value = item.Amount;
				worksheet.Cells[row, 3].Value = item.DateCreated.ToString("yyyy-MM-dd");
				worksheet.Cells[row, 4].Value = item.ExpiryDate.ToString("yyyy-MM-dd");
				worksheet.Cells[row, 5].Value = item.UserCode;
				worksheet.Cells[row, 6].Value = item.VehicleRegistrationNumber;

				for (int col = 1; col <= 6; col++)
				{
					var cell = worksheet.Cells[row, col];
					cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				}
			}

			worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
			return package.GetAsByteArray();
		}
	}
}
