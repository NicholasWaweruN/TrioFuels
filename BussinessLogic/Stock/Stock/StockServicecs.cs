using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using BusinessLogic.Stock.Stock;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.DTOs.Transactions;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using static BussinessLogic.Sales.MissingSales.MisingSale;
using BusinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;

namespace BussinessLogic.Stock.Stock
{
	public class StockServicecs : IStockServicecs
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emails;
		private readonly IMainData MainData;
		private readonly ICommonSalesTasks _salesTasks;

		public StockServicecs(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups, IEmailService emails, IMainData data, ICommonSalesTasks salesTasks)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
			_emails = emails;
			MainData = data;
			_salesTasks = salesTasks;
		}

		// Entry method for stock tak??e
		public async Task<ServiceResponse<object>> StockTakeAsync(StockTakeDto stockTake)
		{
			foreach (var take in stockTake.Readings)
			{
				var nozzleexists = await (from n in _context.Nozzles
										  where n.NozzleCode == take.NozzleCode
										  select n).FirstOrDefaultAsync();
				if (nozzleexists is null)
					return ServiceResponse<object>.Information($" {take.NozzleCode} Nozzle does not exist.", null);

				if (take.Reading < 0)
					return ServiceResponse<object>.Information("Reading cannot be negative", null);

				if (!await IsInitialStockTakeDoneAsync(take.NozzleCode))
					return ServiceResponse<object>.Information("Initial stock take has not been done", null);

			}

			var userShift = await GetUserOpenShiftAsync();

			if (userShift != null)
				if (userShift.ShiftStatus == ShiftStatus.Variance)
					return ServiceResponse<object>.Success($"User has a variance on shift {userShift.ShiftNumber}", null);

			var dispenser = await GetDispenserAssignedToUserAsync();

			if (string.IsNullOrEmpty(dispenser))
				return ServiceResponse<object>.Information("Dispenser not found for user", null);

			if (userShift is null)
				return await CreateNewShiftAndProcessStockTakeAsync(stockTake, dispenser);
			else if (userShift.ShiftStatus == ShiftStatus.Open)
				return await ProcessExistingShiftStockTakeAsync(stockTake, userShift);
			return ServiceResponse<object>.Information("Shift already closed or in variance", null);
		}

		// Handles initial stock take
		public async Task<ServiceResponse<object>> InitialStockTake(StockTakeDto initialStockTakeDto)
		{
			try
			{
				//get dispenser by paasing nozzlecode
				var dispenserName = await (from d in _context.Dispensers
										   join n in _context.Nozzles on d.DispenserCode equals n.DispenserCode
										   join s in _context.Stations on d.StationCode equals s.StationCode
										   where n.NozzleCode.Equals(initialStockTakeDto.Readings.First().NozzleCode)
										   select new { d.DispenserName, s.StationName }).FirstOrDefaultAsync();

				if (dispenserName is null)
					return ServiceResponse<object>.Information($"Dispenser does not exist", null);

				var shift = GenerateShiftNumber();

				foreach (var item in initialStockTakeDto.Readings)
				{
					//validate nozzle 
					var nozzleexists = await (from n in _context.Nozzles
											  where n.NozzleCode == item.NozzleCode
											  select n).FirstOrDefaultAsync();

					if (nozzleexists == null)
						return ServiceResponse<object>.Information($" {item.NozzleCode} Nozzle does not exist.", null);

					if (await IsInitialStockTakeDoneAsync(item.NozzleCode))
						return ServiceResponse<object>.Information("Initial Stock Take Already Done", null);

					await AddInitialStockTakeAsync(item, shift);
				}

				var message = $@"Initial stock take done by {_authentication.Name()} for Dispenser {dispenserName.DispenserName} at {dispenserName.StationName} Station on {DateTime.UtcNow}";
				await _authentication.AddUserTrail(message, MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Initial Stock Taken Successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}


		// Gets stock takes for a user on a specific date
		public async Task<ServiceResponse<object>> GetStockTakes(string date)
		{

			try
			{
				var stockTakes = await (from stockTake in _context.StockTakes
										join nozzle in _context.Nozzles on stockTake.NozzleCode equals nozzle.NozzleCode
										join dispenser in _context.Dispensers on nozzle.DispenserCode equals dispenser.DispenserCode
										join station in _context.Stations on dispenser.StationCode equals station.StationCode
										join user in _context.Users on stockTake.UserCode equals user.UserCode
										where stockTake.DateCreated.Date == Convert.ToDateTime(date).Date
										select new
										{
											Name = user.FirstName + " " + user.LastName,
											stockTake.NozzleCode,
											stockTake.OpeningReading,
											stockTake.ClosingReading,
											stockTake.DateCreated.Date,
											nozzle.NozzleName,
											station.StationName,
											dispenser.DispenserName
										}).OrderBy(x => x.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleCode).ThenBy(x => x.Date).AsNoTracking().ToListAsync();

				if (stockTakes.Count == 0)
					return ServiceResponse<object>.Information("No stock takes found", null);
				return ServiceResponse<object>.Success("Success", stockTakes);
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

				return ServiceResponse<object>.Error("Something went wrong", ex.Message);

			}
		}

		// Gets variances for all users
		public async Task<ServiceResponse<object>> ShiftVariances()
		{
			try
			{
				//Get Current Shift
				var shift = await (from s in _context.Shifts
								   where s.ShiftStatus == ShiftStatus.Open && s.UserCode == _authentication.Usercode()
								   select s.ShiftNumber).FirstOrDefaultAsync();

				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   where ss.VarianceStatus == ShiftStatus.Variance
									   && ss.ShiftNumber == shift
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
										   payrollNumber = u.PayrollNumber,
										   Name = string.Join(' ', u.FirstName, u.MiddName, u.LastName)
									   }).AsNoTracking().ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<object>.Information("No variances found", null);
				return ServiceResponse<object>.Success("", variances);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);

			}
		}

		// Adjusts stock take based on given adjustments
		public async Task<ServiceResponse<object>> AdjustStockTake([Required] int takeType, AdjustStockTakeDto adjust)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				foreach (var item in adjust.Readings)
				{
					var stocktake = await GetStockTakeByNozzleAndShiftAsync(item.NozzleCode, adjust.ShiftNumber);
					var stocktakeSummary = await GetStockTakeSummaryByNozzleAndShiftAsync(item.NozzleCode, adjust.ShiftNumber);

					if (stocktake == null || stocktakeSummary == null)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("Stock take or summary not found", null);
					}

					AdjustStockTakeValues(stocktake, stocktakeSummary, takeType, item.Reading);
				}

				var messages = $@"Stock adjusted by {_authentication.Name()} on {DateTime.UtcNow} of shiftNumber {adjust.ShiftNumber}";
				await _authentication.AddUserTrail(messages, MethodBase.GetCurrentMethod()?.Name ?? "");

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				return ServiceResponse<object>.Success("Stock take adjusted successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}
		// Gets deliveries for a specific date


		//save base64 image to file TotalizerImages folder StockTakeDto
		public class ReceiveDeliveryDto
		{
			public string OrderId { get; set; } = string.Empty; // Order ID for the delivery
			public double DeliveryQuantityKgs { get; set; } // Quantity of the delivery in kilograms
			public string RotoGaugeImageBeforeDelivery { get; set; } = string.Empty; // Image URL or path before delivery
			public double RotoGaugePercAfterDelivery { get; set; } // RotoGauge percentage after delivery
			public string RotoGaugeImageAfterDelivery { get; set; } = string.Empty; // Image URL or path after delivery
			public double RotoGaugePercBeforeDelivery { get; set; } // RotoGauge percentage before delivery
			public double DeliveryQuantityLitres { get; set; } // Quantity of the delivery in liters
		}
		// Receives a delivery and updates the database


		// Private methods for refactoring common tasks
		private async Task<bool> IsInitialStockTakeDoneAsync(string nozzleCode)
		{
			return await _context.StockTakes.AnyAsync(x => x.TakeType == 99 && x.NozzleCode == nozzleCode);
		}
		private async Task<Shift?> GetUserOpenShiftAsync()
		{
			var openshift = await _context.Shifts.FirstOrDefaultAsync(x => x.UserCode == _authentication.Usercode() && x.ShiftStatus == ShiftStatus.Open);
			if (openshift is not null)
				return openshift;
			else
				return null;
		}
		private async Task<string> GetDispenserAssignedToUserAsync()
		{
			var dispenser = await _context.DispenserAssignments
								 .Where(a => a.AttedantUserCode == _authentication.Usercode())
								 .Select(a => a.DispenserCode)
								 .FirstOrDefaultAsync();
			if (dispenser is not null)
				return dispenser;
			return string.Empty;
		}
		private async Task<ServiceResponse<object>> CreateNewShiftAndProcessStockTakeAsync(StockTakeDto stockTake, string dispenser)
		{
			var newShiftNumber = GenerateShiftNumber();
			MainData.ShiftNumber = newShiftNumber;
			var newShift = new Shift
			{
				IsEmailSent = false,
				ShiftNumber = newShiftNumber,
				UserCode = _authentication.Usercode(),
				ShiftStatus = ShiftStatus.Open,
				ShiftStartTime = DateTime.UtcNow,
				DateCreated = DateTime.UtcNow,
				DispenserCode = dispenser,
			};
			await _context.AddAsync(newShift);
			await _context.SaveChangesAsync();

			await ProcessStockTakeReadingsAsync(stockTake, newShiftNumber, isOpeningReading: true, newShift);
			return ServiceResponse<object>.Success("Stock take completed successfully", null);
		}
		private async Task<ServiceResponse<object>> ProcessExistingShiftStockTakeAsync(StockTakeDto stockTake, Shift shift)
		{
			await ProcessStockTakeReadingsAsync(stockTake, shift.ShiftNumber, isOpeningReading: false, shift);
			return ServiceResponse<object>.Success("Stock take completed successfully", null);
		}

		private async Task ProcessStockTakeReadingsAsync(StockTakeDto stockTake, string shiftNumber, bool isOpeningReading, Shift shift)
		{
			decimal totalVariance = 0;
			var userCode = _authentication.Usercode();
			var nozzleCodes = stockTake.Readings.Select(n => n.NozzleCode).ToList();

			// Fetch all necessary StockTakes and StockTakeSummaries in one go
			var stockTakes = await _context.StockTakes
				.Where(s => s.ShiftNumber == shiftNumber && nozzleCodes.Contains(s.NozzleCode))
				.ToListAsync();

			var stockTakeSummaries = await _context.StockTakeSummaries
				.Where(s => s.ShiftNumber == shiftNumber && nozzleCodes.Contains(s.NozzleCode))
				.ToListAsync();


			foreach (var nozzle in stockTake.Readings)
			{
				var stockTakeEntity = stockTakes.FirstOrDefault(s => s.NozzleCode == nozzle.NozzleCode);
				if (isOpeningReading)
					if (stockTakeEntity == null)
					{
						stockTakeEntity = new StockTake { DateCreated = DateTime.UtcNow, ShiftNumber = shiftNumber, UserCode = userCode, NozzleCode = nozzle.NozzleCode, OpeningReading = nozzle.Reading, ClosingReading = 0 };
						_context.StockTakes.Add(stockTakeEntity);
					}
					else if (stockTakeEntity != null)
					{
						stockTakeEntity.ClosingReading = nozzle.Reading;
						_context.StockTakes.Update(stockTakeEntity);
					}

				// Process StockTakeSummary
				var expectedReading = await GetExpectedReadingAsync(nozzle.NozzleCode);
				var variance = nozzle.Reading - expectedReading;
				var stockTakeSummary = stockTakeSummaries.FirstOrDefault(s => s.NozzleCode == nozzle.NozzleCode);

				if (stockTakeSummary == null)
				{
					if (isOpeningReading)
					{
						var openingVariance = nozzle.Reading - expectedReading;

						var newStockTakeSummary = new StockTakeSummary
						{
							DateCreated = DateTime.UtcNow,
							ShiftNumber = shiftNumber,
							UserCode = userCode,
							NozzleCode = nozzle.NozzleCode,
							OpeningReading = nozzle.Reading,
							ExpectedOpeningReading = expectedReading,
							ClosingReading = 0,
							ExpectedClosingReading = 0,
							QuantitySold = 0,
							ClosingVariance = 0,
							OpeningVariance = openingVariance,
							VarianceStatus = openingVariance != 0 ? ShiftStatus.Variance : ShiftStatus.Open
						};
						_context.StockTakeSummaries.Add(newStockTakeSummary);
					}
				}
				else
				{
					var QuantitySold = await (from q in _context.QuantityTransactions
											  where q.ShiftNumber == shiftNumber && q.NozzleCode == nozzle.NozzleCode
											  select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

					stockTakeSummary.QuantitySold = QuantitySold;
					stockTakeSummary.ExpectedClosingReading = expectedReading;
					stockTakeSummary.ClosingReading = nozzle.Reading;
					stockTakeSummary.ClosingVariance = variance;
					stockTakeSummary.VarianceStatus = variance != 0 ? ShiftStatus.Variance : ShiftStatus.Closed;

					_context.StockTakeSummaries.Update(stockTakeSummary);
				}

				totalVariance += variance;
			}

			await _context.SaveChangesAsync();

			if (!isOpeningReading)
			{
				await _salesTasks.ReconcileStockSummariesAsync(shift.ShiftNumber);

				//get the highest variance for this shif in stocktakesummaries
				var highestv = await (from q in _context.StockTakeSummaries
									  where q.ShiftNumber.Equals(shiftNumber)
									  select q).MaxAsync(x => Math.Abs(x.ClosingVariance));

				totalVariance = await (from q in _context.StockTakeSummaries
									   where q.ShiftNumber == shiftNumber
									   select q).SumAsync(x => x.ClosingVariance);




				totalVariance = await (from q in _context.StockTakeSummaries
									   where q.ShiftNumber == shiftNumber
									   select q).SumAsync(x => x.ClosingVariance);

			}

			await UpdateShiftStatusAsync(shiftNumber, totalVariance, isOpeningReading, shift);
		}

		private async Task UpdateShiftStatusAsync(string shiftNumber, decimal totalVariance, bool isOpeningReading, Shift shift)
		{

			if (shift != null)
			{
				shift.ShiftStatus = totalVariance == 0
					? isOpeningReading ? ShiftStatus.Open
					: ShiftStatus.Closed
					: ShiftStatus.Variance;

				if (isOpeningReading)
					shift.ShiftStartTime = DateTime.UtcNow;
				else
					shift.ShiftEndTime = DateTime.UtcNow;

				_context.Shifts.Update(shift);
				await _context.SaveChangesAsync();
			}
		}

		private async Task<decimal> GetExpectedReadingAsync(string nozzleCode)
		{
			var totalizerReading = await (from q in _context.QuantityTransactions
										  where q.NozzleCode == nozzleCode
										  select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			var currentVariance = await (from ss in _context.StockTakeSummaries
										 where ss.NozzleCode == nozzleCode
										 select ss).SumAsync(v => v.ClosingVariance);

			return totalizerReading + currentVariance;
		}
		private async Task<decimal> GetExpectedReadingAsync(string nozzleCode, string shiftNumber)
		{
			var totalizerReading = await (from q in _context.QuantityTransactions
										  where q.NozzleCode == nozzleCode
										  select q).SumAsync(x => x.QuantityCredit - x.QuantityDebit);

			var currentVariance = await (from ss in _context.StockTakeSummaries
										 where ss.NozzleCode == nozzleCode && ss.ShiftNumber != shiftNumber
										 select ss).SumAsync(v => v.ClosingVariance);

			return totalizerReading + currentVariance;
		}

		private async Task AddInitialStockTakeAsync(Readings item, string shift)
		{
			var stocktake = new StockTake
			{
				DateCreated = DateTime.UtcNow,
				NozzleCode = item.NozzleCode,
				ShiftNumber = shift,
				OpeningReading = item.Reading,
				ClosingReading = 0,
				UserCode = _authentication.Usercode(),
				TakeType = 99
			};
			await _context.StockTakes.AddAsync(stocktake);

			var quantity = new QuantityTransactions
			{
				DateCreated = DateTime.UtcNow,
				UserCode = _authentication.Usercode(),
				NozzleCode = item.NozzleCode,
				QuantityCredit = item.Reading,
				QuantityDebit = 0,
				ShiftNumber = shift,
				SaleId = _setups.GenerateSaleId(),
				PaymentTypeCode = 99,
			};
			await _context.QuantityTransactions.AddAsync(quantity);
			await _context.SaveChangesAsync();
		}

		private void AdjustStockTakeValues(StockTake stocktake, StockTakeSummary stocktakeSummary, int takeType, decimal reading)
		{
			if (takeType == 2)
			{
				stocktake.ClosingReading = reading;
				stocktakeSummary.ClosingReading = reading;
				stocktakeSummary.ClosingVariance = reading - stocktakeSummary.ExpectedClosingReading;
				stocktakeSummary.VarianceStatus = stocktakeSummary.ClosingVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			}
			else
			{
				stocktake.OpeningReading = reading;
				stocktakeSummary.OpeningReading = reading;
				stocktakeSummary.OpeningVariance = reading - stocktakeSummary.ExpectedOpeningReading;
				stocktakeSummary.VarianceStatus = stocktakeSummary.OpeningVariance == 0 ? ShiftStatus.Closed : ShiftStatus.Variance;
			}

			_context.Update(stocktake);
			_context.StockTakeSummaries.Update(stocktakeSummary);
		}
		private async Task<StockTake?> GetStockTakeByNozzleAndShiftAsync(string nozzleCode, string shiftNumber)
		{
			var stocktake = await _context.StockTakes.FirstOrDefaultAsync(x => x.NozzleCode == nozzleCode && x.ShiftNumber == shiftNumber);
			if (stocktake is not null)
				return stocktake;
			else
				return null;

		}
		private async Task<StockTakeSummary?> GetStockTakeSummaryByNozzleAndShiftAsync(string nozzleCode, string shiftNumber)
		{
			var stockSummary = await _context.StockTakeSummaries.FirstOrDefaultAsync(x => x.NozzleCode == nozzleCode && x.ShiftNumber == shiftNumber);
			if (stockSummary is not null)
				return stockSummary;
			return null;
		}

		private static readonly Dictionary<int, string> MonthAlphabetMapping = new Dictionary<int, string>
		{
			{ 1, "LA" }, { 2, "JB" }, { 3, "VC" }, { 4, "KD" }, { 5, "WE" },
			{ 6, "XF" }, { 7, "VG" }, { 8, "QH" }, { 9, "SI" }, { 10, "BJ" }, { 11, "CK" }, { 12, "FL" }
		};

		private static readonly Dictionary<int, string> YearAlphabetMapping = new()
		{
			{ 2023,  "MN" },{ 2024, "NO" },{ 2025, "OP" },{ 2026, "PQ" },{ 2027, "QR" },{ 2028, "RS" },{ 2029, "ST" },{ 2030, "TU" }
		};

		private static readonly Dictionary<int, char> DayAlphabetMapping = new()
		{
			{ 1, 'X' }, { 2, 'Y' }, { 3, 'Z' }, { 4, 'A' }, { 5, 'B' },
			{ 6, 'C' }, { 7, 'D' }, { 8, 'E' }, { 9, 'F' }, { 10, 'G' },
			{ 11, 'H' }, { 12, 'I' }, { 13, 'J' }, { 14, 'K' }, { 15, 'L' },
			{ 16, 'M' }, { 17, 'N' }, { 18, 'O' }, { 19, 'P' }, { 20, 'Q' },
			{ 21, 'R' }, { 22, 'S' }, { 23, 'T' }, { 24, 'U' }, { 25, 'V' },
			{ 26, 'W' }, { 27, 'X' }, { 28, 'Y' }, { 29, 'Z' }, { 30, 'A' },
			{ 31, 'B' }
		};

		private static string GenerateShiftNumber()
		{
			var date = DateTime.UtcNow;
			var monthLetter = MonthAlphabetMapping[date.Month];
			var yearLetter = YearAlphabetMapping[date.Year];
			var dayLetter = DayAlphabetMapping[date.Day];
			var timePortion = date.ToString("HHmmssfff");
			var uniqueCode = $"{yearLetter}{monthLetter}{dayLetter}{timePortion}";
			return uniqueCode.ToUpper();
		}
		//List Variance From StockTakeSummary Table 
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
								ss.Id,
								d.DispenserCode,
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
								s.StationCode,
								payrollNumber = u.PayrollNumber,
								Name = string.Join(' ', u.FirstName, u.MiddName, u.LastName),
							};

				// Apply filters
				if (date.HasValue)
					query = query.Where(x => x.DateCreated.Date == date.Value.Date);

				if (!string.IsNullOrEmpty(shiftNumber))
					query = query.Where(x => x.ShiftNumber == shiftNumber);

				if (!string.IsNullOrEmpty(stationName))
					query = query.Where(x => x.StationName.Contains(stationName));

				var variances = await query
					.OrderBy(x => x.StationName)
					.ThenBy(x => x.DispenserName)
					.ThenBy(x => x.Id)
					.AsNoTracking()
					.ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<object>.Information("No variances found", null);
				return ServiceResponse<object>.Success("Variance List", variances);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error(ex.Message, null);
			}
		}
		//auto clear variance if the sum of closing variance for the shift is less than 5 plus or minus  that dispenser insert the record in QuantityTransaction Table and PaymentTransaction and update the StockTakeSummary Table

		private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
		{
			return await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => new Vehicle
				{
					ProductCode = v.ProductCode,
					VehicleRegistration = v.VehicleRegistrationNumber,
					CreditLimit = v.CreditLimit,
				}).FirstOrDefaultAsync() ?? new Vehicle();
		}

		public async Task<ServiceResponse<object>> GetTotalizerReadings()
		{
			try
			{
				var totalizerReadings = await (from q in _context.QuantityTransactions
											   join s in _context.Stations on q.StationCode equals s.StationCode
											   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
											   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
											   group q by new
											   {
												   q.NozzleCode,
												   s.StationName,
												   d.DispenserName,
												   n.NozzleName
											   } into g
											   select new
											   {
												   NozzleCode = g.Key,
												   g.Key.NozzleName,
												   g.Key.DispenserName,
												   g.Key.StationName,
												   TotalizerReading = g.Sum(x => x.QuantityCredit - x.QuantityDebit)
											   }).AsNoTracking().OrderBy(x => x.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleName).ToListAsync();

				if (totalizerReadings.Count == 0)
					return ServiceResponse<object>.Information("No totalizer readings found", null);
				return ServiceResponse<object>.Success("", totalizerReadings);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
			//Adjust stock in table StocktakeSummary for each nozzle in the list foar a particular shift

		}
		//totalizer reading for a particular day
		public async Task<ServiceResponse<object>> GetTotalizerReadings(DateTime date)
		{
			try
			{
				var totalizerReadings = await (from q in _context.QuantityTransactions
											   join s in _context.Stations on q.StationCode equals s.StationCode
											   join d in _context.Dispensers on q.DispenserCode equals d.DispenserCode
											   join n in _context.Nozzles on q.NozzleCode equals n.NozzleCode
											   where q.DateCreated.Date <= date.Date
											   group q by new
											   {
												   q.NozzleCode,
												   s.StationName,
												   d.DispenserName,
												   n.NozzleName
											   } into g
											   select new
											   {
												   NozzleCode = g.Key,
												   g.Key.NozzleName,
												   g.Key.DispenserName,
												   g.Key.StationName,
												   TotalizerReading = g.Sum(x => x.QuantityCredit - x.QuantityDebit)
											   }).OrderBy(X => X.StationName).ThenBy(x => x.DispenserName).ThenBy(x => x.NozzleName).AsNoTracking().ToListAsync();

				if (totalizerReadings.Count == 0)
					return ServiceResponse<object>.Information("No totalizer readings found", null);
				return ServiceResponse<object>.Success("", totalizerReadings);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		public async Task<ServiceResponse<object>> AdjustStockTakes(AdjustStockTakeSummaryDto adjust)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				var stockTakesToUpdate = new List<StockTakeSummary>();

				foreach (var item in adjust.Readings)
				{
					var stockTake = await _context.StockTakeSummaries
									   .AsNoTracking() // Ensure no tracking to avoid conflicts
									   .FirstOrDefaultAsync(ss => ss.NozzleCode == item.NozzleCode && ss.ShiftNumber == adjust.ShiftNumber);

					if (stockTake == null)
					{
						await transaction.RollbackAsync();
						return ServiceResponse<object>.Information("Stock take summary not found", null);
					}

					// Modify the entity properties
					stockTake.ClosingReading = item.ClosingReading;
					stockTake.OpeningReading = item.OpeningReading;
					stockTake.OpeningVariance = 0;

					// Attach and add to the list for batch update later
					_context.StockTakeSummaries.Attach(stockTake);
					stockTakesToUpdate.Add(stockTake);
				}

				if (stockTakesToUpdate.Count != 0)
				{
					_context.StockTakeSummaries.UpdateRange(stockTakesToUpdate);
				}

				// Log user trail after processing all stock takes
				var messages = $@"Stock adjusted by {_authentication.Name()} on {DateTime.UtcNow} for shift number {adjust.ShiftNumber}";

				await _context.SaveChangesAsync();
				await ReconcileStockSummaries(adjust.ShiftNumber);
				await _authentication.AddUserTrail(messages, MethodBase.GetCurrentMethod()?.Name ?? "");

				await transaction.CommitAsync();

				return ServiceResponse<object>.Success("Stock take summary adjusted successfully", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		//export all variances
		public async Task<ServiceResponse<byte[]>> ExportAllVariances()
		{
			try
			{
				var variances = await (from ss in _context.StockTakeSummaries
									   join n in _context.Nozzles on ss.NozzleCode equals n.NozzleCode
									   join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
									   join s in _context.Stations on d.StationCode equals s.StationCode
									   join u in _context.Users on ss.UserCode equals u.UserCode
									   where ss.VarianceStatus == ShiftStatus.Variance && ss.VarianceStatus == ShiftStatus.Pending
									   select new
									   {
										   d.DispenserCode,
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
										   Name = string.Join(' ', u.FirstName, u.MiddName, u.LastName)
									   }).AsNoTracking().ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<byte[]>.Information("No variances found", null);

				// Create and populate DataTable only if there are variances
				var dataTable = new DataTable("VarianceReport");
				dataTable.Columns.AddRange(
				[
					new("ShiftNumber", typeof(string)),
					new("StationName", typeof(string)),
					new("DispenserName", typeof(string)),
					new("NozzleName", typeof(string)),
					new("OpeningReading", typeof(decimal)),
					new("ExpectedOpeningReading", typeof(decimal)),
					new("ClosingReading", typeof(decimal)),
					new("ExpectedClosingReading", typeof(decimal)),
					new("Variance", typeof(decimal)),
					new("QuantitySold", typeof(decimal)),
					new("Status", typeof(string)),
					new("DateCreated", typeof(DateTime)),
					new("PayrollNumber", typeof(string)),
					new("Name", typeof(string))
				]);

				// Add variances to DataTable
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
						variance.Name ?? string.Empty // Handle nulls gracefully
					);
				}
				//add datatable to excel
				var excel = new ExcelPackage();
				var ws = excel.Workbook.Worksheets.Add("VarianceReport");
				ws.Cells["A1"].LoadFromDataTable(dataTable, true);
				ws.Cells.AutoFitColumns();

				var stream = new MemoryStream(excel.GetAsByteArray());

				return ServiceResponse<byte[]>.Success("Variance report generated successfully", stream.ToArray());
			}
			catch (Exception)
			{

				return ServiceResponse<byte[]>.Error("Failed to generate variance report", null);

			}
		}
		public async Task<ServiceResponse> ReconcileStockSummaries(string shiftNumber)
		{
			return await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
		}
		public class VarianceDto
		{
			public long ShiftId { get; set; }
			public string DispenserCode { get; set; } = string.Empty;
			public string ShiftNumber { get; set; } = string.Empty;
			public string UserCode { get; set; } = string.Empty;
			public string NozzleCode { get; set; } = string.Empty;
			[Precision(18, 2)] public decimal OpeningReading { get; set; }
			[Precision(18, 2)] public decimal ExpectedOpeningReading { get; set; }
			[Precision(18, 2)] public decimal ClosingReading { get; set; }
			[Precision(18, 2)] public decimal ExpectedClosingReading { get; set; }
			[Precision(18, 2)] public decimal Variance { get; set; }
			[Precision(18, 2)] public decimal QuantitySold { get; set; }
			public string Status { get; set; } = string.Empty;
			public DateTime DateCreated { get; set; }
			public string NozzleName { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string PayrollNumber { get; set; } = string.Empty;
			public string Name { get; set; } = string.Empty;
		}
		public async Task<ServiceResponse> NozzleQuantityTransfer(string shiftNumber)
		{
			const decimal varianceThreshold = 0m; // Acceptable variance threshold

			// Step 1: Get variances for the shift
			var variances = await GetVariance(shiftNumber);
			var positiveVarianceRecord = variances.FirstOrDefault(v => v.ClosingVariance > 0);
			var negativeVarianceRecord = variances.FirstOrDefault(v => v.ClosingVariance < 0);

			if (positiveVarianceRecord == null || negativeVarianceRecord == null)
				return ServiceResponse<object>.Information("Nozzle variance data missing.", null);


			decimal negativeVariance = negativeVarianceRecord.ClosingVariance;
			string positiveNozzle = positiveVarianceRecord.NozzleCode;
			string negativeNozzle = negativeVarianceRecord.NozzleCode;

			var dispensercode = await (from s in _context.Shifts
									   where s.ShiftNumber == shiftNumber
									   select s.DispenserCode).FirstOrDefaultAsync() ?? string.Empty;

			var stationCode = await (from s in _context.Dispensers
									 where s.DispenserCode == dispensercode
									 select s.StationCode).FirstOrDefaultAsync() ?? string.Empty;

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				while (Math.Abs(negativeVariance) > varianceThreshold)
				{
					// Find the closest transaction to move
					var transactionToMove = await GetClosestTransaction(shiftNumber, Math.Abs(negativeVariance), positiveNozzle);

					if (transactionToMove == null)
					{
						break; // Exit if no matching transactions
					}

					// Update the nozzle,dispensercode,stationCode code
					transactionToMove.NozzleCode = negativeNozzle;
					transactionToMove.DispenserCode = dispensercode;
					transactionToMove.StationCode = stationCode;

					//Save to moved
					MovedTransactions(new MovedTransactions
					{
						AmountCredit = transactionToMove.AmountCredit,
						NozzleCode = transactionToMove.NozzleCode,
						AmountDebit = transactionToMove.AmountDebit,
						DateCreated = transactionToMove.DateCreated,
						DispenserCode = transactionToMove.DispenserCode,
						IsReversed = transactionToMove.IsReversed,
						PaymentTypeCode = transactionToMove.PaymentTypeCode,
						ShiftNumber = transactionToMove.ShiftNumber,
						Price = transactionToMove.Price,
						QuantityCredit = transactionToMove.QuantityCredit,
						UserCode = transactionToMove.UserCode,
						SaleId = transactionToMove.SaleId,
						QuantityDebit = transactionToMove.QuantityDebit,
						StationCode = transactionToMove.StationCode,
						VehicleCode = transactionToMove.VehicleCode,
					});

					_context.QuantityTransactions.Update(transactionToMove);
					await _context.SaveChangesAsync();

					// Update remaining variance
					negativeVariance += transactionToMove.QuantityCredit;
				}

				await transaction.CommitAsync();
				await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
				return ServiceResponse<object>.Success("Nozzle quantity transfer successful.", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		private async Task<List<StockTakeSummary>> GetVariance(string shiftNumber)
		{
			return await _context.StockTakeSummaries
								 .Where(ss => ss.ShiftNumber == shiftNumber)
								 .AsNoTracking()
								 .ToListAsync();
		}

		public async void MovedTransactions(MovedTransactions transactions)
		{

			var moved = new MovedTransactions()
			{
				AmountCredit = transactions.AmountCredit,
				AmountDebit = transactions.AmountDebit,
				NozzleCode = transactions.NozzleCode,
				DispenserCode = transactions.DispenserCode,
				StationCode = transactions.StationCode,
				DateCreated = transactions.DateCreated,
				QuantityDebit = transactions.QuantityDebit,
				IsReversed = transactions.IsReversed,
				PaymentTypeCode = transactions.PaymentTypeCode,
				Price = transactions.Price,
				QuantityCredit = transactions.QuantityCredit,
				SaleId = transactions.SaleId,
				ShiftNumber = transactions.ShiftNumber,
				UserCode = transactions.UserCode,
				VehicleCode = transactions.VehicleCode,
			};

			_ = await _context.AddAsync(moved);
			await _context.SaveChangesAsync();
		}

		private async Task<QuantityTransactions?> GetClosestTransaction(string shiftNumber, decimal quantityFrom, string nozzleCodeFrom)
		{
			return await _context.QuantityTransactions
								 .Where(qt => qt.ShiftNumber == shiftNumber &&
											  qt.NozzleCode == nozzleCodeFrom &&
											  qt.QuantityCredit <= quantityFrom)
								 .OrderByDescending(qt => qt.QuantityCredit) // Descending to prioritize larger values closer to the target
								 .FirstOrDefaultAsync();
		}
		private async Task<List<ThePrices>> GetProductPrice(string stationCode)
		{
			var newPrice = new List<ThePrices>();

			var prices = await (from p in _context.Prices
								where p.StationCode == stationCode
								select new ThePrices
								{
									ProductCode = p.ProductCode,
									Price = p.Amount
								}).ToListAsync();

			newPrice.AddRange(prices);
			return newPrice;
		}

		//Reset stocktake in stocktakeSummaries
		public async Task<ServiceResponse> ResetShift(string shiftNumber)
		{
			try
			{
				var shift = await (from p in _context.Shifts
								   where p.ShiftNumber == shiftNumber
								   select p).FirstOrDefaultAsync();

				var readings = await (from r in _context.StockTakeSummaries
									  where r.ShiftNumber == shiftNumber
									  select r).ToListAsync();

				if (shift != null)
				{
					shift.ShiftStatus = 1;
					shift.ShiftEndTime = null;
					_context.Shifts.Update(shift);
				}
				else
					return ServiceResponse<object>.Information($"{shiftNumber} does not exist");

				foreach (var reading in readings)
				{
					reading.ClosingReading = 0;
					reading.ClosingVariance = 0;
					reading.VarianceStatus = 1;
					reading.ExpectedClosingReading = 0;
					_context.StockTakeSummaries.Update(reading);
				}
				await _context.SaveChangesAsync();
				await _authentication.AddUserTrail($"Shift {shiftNumber} was reset by {_authentication.Usercode()} on {DateTime.UtcNow}", MethodBase.GetCurrentMethod()?.Name ?? "");
				return ServiceResponse<object>.Success($"Reset for shift Number {shiftNumber} was successFul", null);

			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}


		}


	}
}