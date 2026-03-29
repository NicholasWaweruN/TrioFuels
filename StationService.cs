using DataAccessLayer.Common;
using DataAccessLayer.Context;
using OTO_Gas.AuthenticationService;
using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EntityModels.Stations;
using DataAccessLayer.DTOs.Station;
using DataAccessLayer.EntityModels.Transactions;
using BussinessLogic.SetupService;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Station.Station
{
    public class StationService
    {
        private readonly OTOContext _context;
        private readonly IAuthentication _authentication;
        private readonly ICommonSetups _setups;

        public StationService(IAuthentication authentication, OTOContext context, ICommonSetups setups)
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
                return CreateResponse(Response.Information, "Station details cannot be empty", null);
            }

            try
            {
                if (await _context.Stations.AnyAsync(x => x.StationName == addStation.StationName))
                {
                    return CreateResponse(Response.Information, "Station already exists", null);
                }

                var code = await _setups.GetCodeGenerator("StationCode");
                var station = new GasStation
                {
                    StationName = addStation.StationName,
                    StationAddress = addStation.StationAddress,
                    StationCode = code,
                    DateCreated = DateTime.Now,
                    UserCode = _authentication.Usercode(),
                    IsActive = true,
                };

                await _context.Stations.AddAsync(station);

                var prices = _context.Products.Select(product => new Price
                {
                    DateCreated = DateTime.Now,
                    UserCode = _authentication.Usercode(),
                    StationCode = station.StationCode,
                    ProductCode = product.ProductCode,
                    Amount = 0,
                }).ToList();

                await _context.Prices.AddRangeAsync(prices);
                await _context.SaveChangesAsync();

                var message = $"Station {station.StationName} was created by {_authentication.Name()} on {DateTime.Now}";
                await _authentication.AddUserTrail(message);

                return CreateResponse(Response.Success, "Station created. Add Gas Prices", station);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return CreateResponse(Response.Error, "An error occurred", null);
            }
        }

        // Get all stations
        public async Task<ServiceResponse<object>> GetStations()
        {
            try
            {
                var stations = await _context.Stations
                    .AsNoTracking()
                    .Select(s => new
                    {
                        s.StationCode,
                        s.StationName,
                        s.StationAddress,
                        s.DateCreated,
                        Status = s.IsActive ? "Active" : "Inactive",
                    })
                    .ToListAsync();

                if (!stations.Any())
                {
                    return CreateResponse(Response.Information, "No stations found", null);
                }

                var message = $"User {_authentication.Name()} retrieved all stations on {DateTime.Now}";
                await _authentication.AddUserTrail(message);

                return CreateResponse(Response.Success, "Stations retrieved successfully", stations);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetStations));
                return CreateResponse(Response.Error, "An error occurred");
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
                    return CreateResponse(Response.Information, "Station not found", null);
                }

                station.IsActive = status;
                await _context.SaveChangesAsync();

                var stationStatus = status ? "Activated" : "Deactivated";
                var message = $"User {_authentication.Name()} {stationStatus} {station.StationName} Station on {DateTime.Now}";
                await _authentication.AddUserTrail(message);

                return CreateResponse(Response.Success, $"{station.StationName} Station {stationStatus} successfully", station);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return CreateResponse(Response.Error, "An error occurred", null);
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
                    
                    return CreateResponse(Response.Information, "Station not found", null);
                }

                station.StationName = updateStation.StationName;
                station.StationAddress = updateStation.StationAddress;
                await _context.SaveChangesAsync();

                var message = $"User {_authentication.Name()} updated {station.StationName} Station on {DateTime.Now}";
                await _authentication.AddUserTrail(message);

                return CreateResponse(Response.Success, "Station updated successfully", station);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return CreateResponse(Response.Error, "An error occurred", null);
            }
        }

        // Helper method to create responses
        private ServiceResponse<object> CreateResponse(int responseCode, string responseMessage, object? responseObject)
        {
            return new ServiceResponse<object>
            {
                ResponseCode = responseCode,
                ResponseMessage = responseMessage,
                ResponseObject = responseObject
            };
        }

        // Helper method to log errors
        private void LogError(Exception ex)
        {
            // Log the exception (details can be added as per your logging framework)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
