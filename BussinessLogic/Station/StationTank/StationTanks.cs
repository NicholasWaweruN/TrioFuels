using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.SetupService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts.Station;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using OTO_Gas.AuthenticationService;
using BussinessLogic.Setup;

namespace BusinessLogic.Station.StationTank
{
    public class StationTanks : IStationTanks
    {
        private readonly OTOContext _context;
        private readonly IAuthCommonTasks _authentication;
        private readonly ICommonSetups _setups;

        public StationTanks(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups)
        {
            _context = context;
            _authentication = authentication;
            _setups = setups;
        }
        //Add a new tank
        public async Task<ServiceResponse<object>> AddTank(AddTankDto addTank)
        {
            try
            {
                if (addTank is null)
                {
                   return ServiceResponse<object>.Information("Tank details cannot be empty", null);

                }
                else
                {
                    var tankexists = _context.Tank.Where(x => x.TankName == addTank.TankName).FirstOrDefault();
                    var code = await _setups.GetCodeGenerator("TankCode");
                    if (tankexists is not null)
                    {
                        return ServiceResponse<object>.Information("Tank already exists", null);
                    }
                    var tank = new Tank
                    {
                        TankName = addTank.TankName,
                        TankCode = code,
                        DateCreated = DateTime.UtcNow,
                        StationCode = addTank.StationCode,
						IsActive = true,
						UserCode = _authentication.Usercode()
                    };
					var message = $"Tank {addTank.TankName} added to station {addTank.StationCode} by {_authentication.Name()} on {DateTime.UtcNow}";
					await _context.Tank.AddAsync(tank);
                    await _context.SaveChangesAsync();
                    return ServiceResponse<object>.Success("Tank added successfully", tank);
                }
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
        //list all tanks with station name
        public async Task<ServiceResponse<object>> GetTanks()
        {
            try
            {
                var tanks = await _context.Tank
                    .AsNoTracking()
                    .Select(t => new
                    {
                        t.TankCode,
                        t.TankName,
                        t.StationCode,
                        t.DateCreated,
                        StationName = _context.Stations.Where(x => x.StationCode == t.StationCode).Select(x => x.StationName).FirstOrDefault()
                    }).ToListAsync();

                if (tanks.Count != 0)
                   return ServiceResponse<object>.Success("Tanks retrieved ",null);
                  return ServiceResponse<object>.Information("Tanks not found", null);
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
        //Update a tank
        public async Task<ServiceResponse<object>> UpdateTank(UpdateTankDto updateTank)
        {
            try
            {
                if (updateTank is null)
                    return ServiceResponse<object>.Information("Tank details cannot be empty", null);
                else
                {
                    var tank = _context.Tank.Where(x => x.TankCode == updateTank.TankCode).FirstOrDefault();
                    if (tank is null)
                    {
                        return ServiceResponse<object>.Information("Tank does not exist", null);
                    }
                    tank.TankName = updateTank.TankName;
                    _context.Tank.Update(tank);
                    await _context.SaveChangesAsync();
                   
                    return ServiceResponse<object>.Success("Tank updated successfully", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponse<object>.Error(ex.Message, null);
            }
        }
        //accept delivery of fuel to a tank in a station by updating table table OtogasDelivery on orderid
        
			
        //list tanks by stationcode
        public async Task<ServiceResponse<object>> StationTank(string stationCode)
        {
            var tanks = await (from t in _context.Tank
                               join s in _context.Stations on t.StationCode equals s.StationCode
                               where t.StationCode == stationCode
                               select t).ToListAsync();
            if (tanks.Count == 0)
                return ServiceResponse<object>.Information("Tanks not found", null);
            return ServiceResponse<object>.Success("Tanks Found", tanks);

        }

    }
}
