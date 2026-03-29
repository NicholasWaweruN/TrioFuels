using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.SetupService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Shifts.Station;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BussinessLogic.Setup;

namespace BusinessLogic.Station.Nozzzles
{
	public class DispenserNozzle : IDispenserNozzle
    {
        private readonly OTOContext _context;
        private readonly IAuthCommonTasks _authentication;
        private readonly ICommonSetups _setups;

        public DispenserNozzle(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups)
        {
            _context = context;
            _authentication = authentication;
            _setups = setups;
        }
        //Add a new nozzle to a dispenser
        public async Task<ServiceResponse<object>> AddNozzle(AddNozzleDto addNozzle)
        {
            try
            {
                if (addNozzle is null)
                    return ServiceResponse<object>.Information("Nozzle details cannot be empty", null);
                else
                {
                    var nozzleexists = _context.Nozzles.Where(x => x.NozzleName == addNozzle.NozzleName && x.DispenserCode.Equals(addNozzle.DispenserCode)).FirstOrDefault();
                    var code = await _setups.GetCodeGenerator("NozzleCode");
                    if (nozzleexists is not null)
                        return ServiceResponse<object>.Information("Nozzle already exists", null);
                    var nozzle = new Nozzle
                    {
                        NozzleName = addNozzle.NozzleName,
                        NozzleCode = code,
                        DateCreated = DateTime.UtcNow,
                        UserCode = _authentication.Usercode(),
                        IsActive = true,
                        DispenserCode = addNozzle.DispenserCode,
                    };
                    await _context.Nozzles.AddAsync(nozzle);
                    await _context.SaveChangesAsync();
                    var message = $"Nozzle {addNozzle.NozzleName} Added SuccessFully by {_authentication.Name()} on {DateTime.UtcNow}";
                    await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

                    return ServiceResponse<object>.Success("Nozzle added successfully", nozzle);
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
        //Get all nozzles in a dispenser with station name
        public async Task<ServiceResponse<object>> GetNozzles()
        {
            try
            {
                var nozzles = await (from nozzle in _context.Nozzles
                                     join dispenser in _context.Dispensers on nozzle.DispenserCode equals dispenser.DispenserCode
                                     join station in _context.Stations on dispenser.StationCode equals station.StationCode
                                     select new
                                     {
                                         nozzle.NozzleCode,
                                         nozzle.NozzleName,
                                         Status = nozzle.IsActive ? "Active" : "Inactive",
                                         station.StationName,
                                         station.StationCode,
                                         dispenser.DispenserName,
                                         dispenser.DispenserCode,
                                     }).AsNoTracking().ToListAsync();
                if (nozzles.Count == 0)
                    return ServiceResponse<object>.Information("Nozzles not found", null);
                return ServiceResponse<object>.Success("Nozzles retrieved successfully", nozzles);
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
        //Activate or deactivate a nozzle
        public async Task<ServiceResponse<object>> ActivateNozzle(string nozzleCode, bool status)
        {
            try
            {
                var nozzle = await _context.Nozzles.FirstOrDefaultAsync(x => x.NozzleCode == nozzleCode);
                if (nozzle is null)
                {
                    return ServiceResponse<object>.Information("Nozzle not found", null);
                }
                nozzle.IsActive = status;
                await _context.SaveChangesAsync();
                var nozzleStatus = status ? "Activated" : "Deactivated";
                var message = $"User {_authentication.Name()} {nozzleStatus} {nozzle.NozzleName} Nozzle on {DateTime.UtcNow}";
                await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
                return ServiceResponse<object>.Success($"Nozzle {nozzleStatus} successfully", nozzle);
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
        //Update a nozzle
        public async Task<ServiceResponse<object>> UpdateNozzle(UpdateNozzleDto updateNozzle)
        {
            try
            {
                if (updateNozzle is null)
                    return ServiceResponse<object>.Information("Nozzle details cannot be empty", null);
                else
                {
                    var nozzle = _context.Nozzles.Where(x => x.NozzleCode == updateNozzle.NozzleCode).FirstOrDefault();
                    if (nozzle is null)
                    {
                      return ServiceResponse<object>.Information("Nozzle not found", null);
                    }
                    nozzle.NozzleName = updateNozzle.NozzleName;
                    _context.Nozzles.Update(nozzle);
                    await _context.SaveChangesAsync();
                    var message = $"Nozzle {updateNozzle.NozzleName} Updated successFully by {_authentication.Name()} on {DateTime.UtcNow}";
                    await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
                    return ServiceResponse<object>.Success("Nozzle updated successfully", nozzle);
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
       //List Dispenser Nozzles by DispenserId'
        public async Task<ServiceResponse<object>> GetNozzlesByDispenser(string dispenserCode)
        {
            try
            {
                var nozzles = await _context.Nozzles.Where(x => x.DispenserCode == dispenserCode).Select(x => new
                {
                    x.NozzleCode,
                    x.NozzleName,
                    x.IsActive
                }).ToListAsync();
                if (nozzles.Count == 0)
                   return ServiceResponse<object>.Information("Nozzles not found", null);;
                return ServiceResponse<object>.Success("Nozzles retrieved successfully", nozzles);
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
    }
}
