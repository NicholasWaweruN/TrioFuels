using DataAccessLayer.Common;
using DataAccessLayer.Context;
using OTO_Gas.AuthenticationService;
using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.EntityModels.Transactions;
using BusinessLogic.SetupService;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DTOs.Sales;
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.EntityModels.SetUps;
using System.Reflection;
using DataAccessLayer.DTOs.Shifts.Station;
using BussinessLogic.Setup;

namespace BusinessLogic.Station.Station
{
    public class StationService : IStationService
    {
        private readonly OTOContext _context;
        private readonly IAuthCommonTasks _authentication;
        private readonly ICommonSetups _setups;

        public StationService(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups)
        {
            _context = context;
            _authentication = authentication;
            _setups = setups;
        }

		// Add a new station
		public async Task<ServiceResponse<object>> AddStation(AddStationDto addStation)
		{
			if (addStation == null)
			{
				return ServiceResponse<object>.Information("Station details cannot be empty", null);
			}

			try
			{
				var stationExists = await _context.Stations.FirstOrDefaultAsync(x => x.StationName == addStation.StationName);
				if (stationExists != null)
				{
					return ServiceResponse<object>.Information("Station already exists", null);
				}
				//generate unique code for the station using datetime
				var codeunique = GenerateUniqueCode();
				var code = await _setups.GetCodeGenerator("StationCode");

				var station = new GasStation
				{
					StationName = addStation.StationName,
					StationAddress = addStation.StationAddress,
					StationCode = code,
					DateCreated = DateTime.UtcNow,
					UserCode = _authentication.Usercode(),
					IsActive = true,
					LocationId = codeunique
				};
				await _context.Stations.AddAsync(station);

				//Add a new location on table  CustomerLocations
		
				//add a record in code generator table typename StationCode
				await _setups.AddCodeGenerator(code);

				foreach (var product in _context.Products)
				{
					var price = new Price
					{
						DateCreated = DateTime.UtcNow,
						UserCode = _authentication.Usercode(),
						StationCode = station.StationCode,
						ProductCode = product.ProductCode,
						Amount = 0,
					};
					await _context.Prices.AddAsync(price);
				}



				await _context.SaveChangesAsync();

				var message = $"Station {station.StationName} was created by {_authentication.Name()} on {DateTime.UtcNow} Adding price template";
				await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

				return ServiceResponse<object>.Success("Station created. Add Gas Prices", station);
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
        string GenerateUniqueCode()
        {
            // Get current date and time
            DateTime now = DateTime.UtcNow;

            // Format the DateTime into a unique string
            string dateTimeString = now.ToString("yyyyMMddHHmmssfff"); // YearMonthDayHourMinuteSecondMillisecond

            // Optionally add a random number for extra uniqueness
            Random random = new();
            int randomNumber = random.Next(1000, 9999);

            // Combine the dateTime string and the random number
            string uniqueCode = $"{dateTimeString}{randomNumber}";

            return uniqueCode;
        }
		// Get all stations


		public async Task<ServiceResponse<object>> GetStations()
		{
			try
			{
				var stations = await _context.Stations
					.Select(s => new
					{
						s.StationCode,
						s.StationName,
						s.StationAddress,
						s.DateCreated,
						Status = s.IsActive ? "Active" : "Inactive"
					})
					.ToListAsync();

				if (!stations.Any())
					return ServiceResponse<object>.Information("No stations found", null);

				var dispensers = await _context.Dispensers
					.Select(d => new
					{
						d.DispenserCode,
						d.DispenserName,
						d.StationCode
					})
					.ToListAsync();

				var prices = await _context.Prices
					.Select(p => new
					{
						p.StationCode,
						p.DispenserCode,
						p.ProductCode,
						p.Amount
					})
					.ToListAsync();

				var products = await _context.Products
					.Select(p => new
					{
						p.ProductCode,
						p.ProductName
					})
					.ToListAsync();

				var result = stations.Select(st => new
				{
					st.StationCode,
					st.StationName,
					st.StationAddress,
					st.DateCreated,
					st.Status,

					Dispensers = dispensers
						.Where(d => d.StationCode == st.StationCode)
						.Select(d => new
						{
							d.DispenserCode,
							d.DispenserName,

							Prices = prices
								.Where(p => p.StationCode == st.StationCode && p.DispenserCode == d.DispenserCode)
								.Select(p => new
								{
									p.ProductCode,
									ProductName = products.FirstOrDefault(x => x.ProductCode == p.ProductCode)?.ProductName ?? "",
									p.Amount
								}).ToList()

						}).ToList()
				}).ToList();

				return ServiceResponse<object>.Success("Stations retrieved successfully", result);
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
						Method = method?.Name ?? ""
					});

				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// Activate or deactivate a station
		public async Task<ServiceResponse<object>> ActivateStation(string stationCode, bool status)
        {
            try
            {
                var station = await _context.Stations.FirstOrDefaultAsync(x => x.StationCode == stationCode);
                if (station == null)
                {
                    return ServiceResponse<object>.Information("Station not found", null);
                }

                station.IsActive = status;
                await _context.SaveChangesAsync();

                var stationStatus = status ? "Activated" : "Deactivated";
                var message = $"User {_authentication.Name()} {stationStatus} {station.StationName} Station on {DateTime.UtcNow}";
                await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

                return ServiceResponse<object>.Success($"Station {stationStatus} successfully", station);
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

        // Update station details
        public async Task<ServiceResponse<object>> UpdateStation(UpdateStationDto updateStation)
        {
            try
            {
                var station = await _context.Stations.FirstOrDefaultAsync(x => x.StationCode == updateStation.StationCode);
                if (station == null)
                {
                    return ServiceResponse<object>.Information("Station not found", null);
                }

                station.StationName = updateStation.StationName;
                station.StationAddress = updateStation.StationAddress;
                await _context.SaveChangesAsync();

                var message = $"{_authentication.Name()} updated {station.StationName} Station on {DateTime.UtcNow}";
                await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

                return ServiceResponse<object>.Success("Station updated successfully", station);
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

        // Helper method to create responses
  

        // Helper method to log errors
        private void LogError(Exception ex)
        {
            // Log the exception (details can be added as per your logging framework)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
