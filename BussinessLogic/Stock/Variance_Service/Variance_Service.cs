using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using System.Data;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace BussinessLogic.Stock.Variance_Service
{
	public class VarianceService
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;

		public VarianceService(IAuthCommonTasks authentication, OTOContext context)
		{
			_authentication = authentication;
			_context = context;
		}

		// Retrieves shift variances
		public async Task<ServiceResponse<object>> ShiftVariances()
		{
			try
			{
				var shiftNumber = await _context.Shifts
					.Where(s => s.ShiftStatus == ShiftStatus.Open && s.UserCode == _authentication.Usercode())
					.Select(s => s.ShiftNumber)
					.FirstOrDefaultAsync();

				if (shiftNumber is null)
					return ServiceResponse<object>.Information("No open shift found", null);

				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   where ss.VarianceStatus == ShiftStatus.Variance && ss.ShiftNumber == shiftNumber
									   select new
									   {
										   ss.ShiftNumber,
										   ss.UserCode,
										   ss.NozzleCode,
										   ss.OpeningReading,
										   ss.ClosingReading,
										   ss.ExpectedClosingReading,
										   Variance = ss.ClosingVariance + ss.OpeningVariance,
										   Status = ss.VarianceStatus,
										   ss.DateCreated,
										   n.NozzleName,
										   d.DispenserName,
										   s.StationName,
										   u.PayrollNumber,
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName })
									   }).AsNoTracking().ToListAsync();

				if (!variances.Any())
					return ServiceResponse<object>.Information("No variances found", null);

				return ServiceResponse<object>.Success("Variance data retrieved successfully", variances);
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Lists variances with optional filters
		public async Task<ServiceResponse<object>> ListVariance(DateTime? date, string? shiftNumber, string? stationName)
		{
			try
			{
				var query = from ss in _context.StockTakeSummaries
							join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
							join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
							join s in _context.Stations on d.StationCode equals s.StationCode
							join u in _context.Users on ss.UserCode equals u.UserCode
							where ss.VarianceStatus == ShiftStatus.Variance || ss.VarianceStatus == ShiftStatus.Pending
							select new
							{
								ss.ShiftNumber,
								ss.UserCode,
								ss.NozzleCode,
								ss.OpeningReading,
								ss.ClosingReading,
								ss.ExpectedClosingReading,
								Variance = ss.ClosingVariance + ss.OpeningVariance,
								ss.QuantitySold,
								Status = ss.VarianceStatus,
								ss.DateCreated,
								n.NozzleName,
								d.DispenserName,
								s.StationName,
								u.PayrollNumber,
								Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName })
							};

				if (date.HasValue)
					query = query.Where(x => x.DateCreated.Date == date.Value.Date);

				if (!string.IsNullOrEmpty(shiftNumber))
					query = query.Where(x => x.ShiftNumber == shiftNumber);

				if (!string.IsNullOrEmpty(stationName))
					query = query.Where(x => x.StationName.Contains(stationName));

				var variances = await query
					.OrderBy(x => x.StationName)
					.ThenBy(x => x.DispenserName)
					.ThenBy(x => x.NozzleName)
					.AsNoTracking()
					.ToListAsync();

				if (!variances.Any())
					return ServiceResponse<object>.Information("No variances found", null);

				return ServiceResponse<object>.Success("Variance list retrieved successfully", variances);
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Exports all variances to an Excel file
		public async Task<ServiceResponse<byte[]>> ExportAllVariances()
		{
			try
			{
				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   where ss.VarianceStatus == ShiftStatus.Variance || ss.VarianceStatus == ShiftStatus.Pending
									   select new
									   {
										   ss.ShiftNumber,
										   ss.UserCode,
										   ss.NozzleCode,
										   ss.OpeningReading,
										   ss.ClosingReading,
										   ss.ExpectedClosingReading,
										   ss.ExpectedOpeningReading,
										   Variance = ss.ClosingVariance + ss.OpeningVariance,
										   ss.QuantitySold,
										   Status = ss.VarianceStatus,
										   ss.DateCreated,
										   n.NozzleName,
										   d.DispenserName,
										   s.StationName,
										   s.StationCode,
										   u.PayrollNumber,
										   Name = string.Join(' ', new object[] { u.FirstName, u.MiddName, u.LastName })
									   }).AsNoTracking().ToListAsync();

				if (!variances.Any())
					return ServiceResponse<byte[]>.Information("No variances found", null);

				var dataTable = new DataTable("VarianceReport");
				dataTable.Columns.AddRange(new[]
				{
					new DataColumn("ShiftNumber", typeof(string)),
					new DataColumn("StationName", typeof(string)),
					new DataColumn("DispenserName", typeof(string)),
					new DataColumn("NozzleName", typeof(string)),
					new DataColumn("OpeningReading", typeof(decimal)),
					new DataColumn("ExpectedOpeningReading", typeof(decimal)),
					new DataColumn("ClosingReading", typeof(decimal)),
					new DataColumn("ExpectedClosingReading", typeof(decimal)),
					new DataColumn("Variance", typeof(decimal)),
					new DataColumn("QuantitySold", typeof(decimal)),
					new DataColumn("Status", typeof(string)),
					new DataColumn("DateCreated", typeof(DateTime)),
					new DataColumn("PayrollNumber", typeof(string)),
					new DataColumn("Name", typeof(string))
				});

				foreach (var variance in variances)
				{
					dataTable.Rows.Add(
						variance.ShiftNumber,
						variance.StationName,
						variance.DispenserName,
						variance.NozzleName,
						variance.OpeningReading,
						variance.ExpectedOpeningReading,
						variance.ClosingReading,
						variance.ExpectedClosingReading,
						variance.Variance,
						variance.QuantitySold,
						variance.Status,
						variance.DateCreated,
						variance.PayrollNumber,
						variance.Name ?? string.Empty
					);
				}

				using var excel = new ExcelPackage();
				var worksheet = excel.Workbook.Worksheets.Add("VarianceReport");
				worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
				worksheet.Cells.AutoFitColumns();

				return ServiceResponse<byte[]>.Success("Variance report generated successfully", excel.GetAsByteArray());
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<byte[]>.Error("Failed to generate variance report", null);
			}
		}

		// Reconciles variances for a shift
		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			try
			{
				// Reconciliation logic goes here
				return ServiceResponse<object>.Success("Reconciliation completed successfully");
			}
			catch (Exception ex)
			{
				await LogErrorTrailAsync(ex);
				return ServiceResponse<object>.Error("Failed to reconcile stock summaries");
			}
		}

		// Logs errors for tracking
		private async Task LogErrorTrailAsync(Exception ex)
		{
			await _authentication.ErrorTrail(new ErrorTrail
			{
				DateCreated = DateTime.UtcNow,
				ErrorCode = "004",
				ErrorMessage = ex.Message,
				Method = ex.TargetSite?.Name ?? string.Empty
			});
		}
	}
}
