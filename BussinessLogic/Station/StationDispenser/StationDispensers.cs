using BusinessLogic.SetupService;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using OTO_Gas.AuthenticationService;
using DataAccessLayer.EntityModels.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.DTOs.Shifts.Station;
using BussinessLogic.Setup;

namespace BusinessLogic.Station.StationDispenser
{
	public class StationDispensers : IStationDispensers
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentication;
		private readonly ICommonSetups _setups;

		public StationDispensers(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups)
		{
			_context = context;
			_authentication = authentication;
			_setups = setups;
		}
		//Add a new dispenser
		public async Task<ServiceResponse<object>> AddDispenser(AddDispenserDto addDispenser)
		{
			try
			{
				if (addDispenser is null)
					return ServiceResponse<object>.Information("Dispenser details cannot be empty", null);
				else
				{
					var dispenserexists = _context.Dispensers.Where(x => x.DispenserName == addDispenser.DispenserName && x.StationCode == addDispenser.StationCode).FirstOrDefault();

					var code = await _setups.GetCodeGenerator("DispenserCode");
					if (dispenserexists is not null)
						return ServiceResponse<object>.Information("Dispenser already exists", null);
					var dispenser = new Dispenser
					{
						DispenserName = addDispenser.DispenserName,
						DispenserCode = code,
						DateCreated = DateTime.UtcNow,
						StationCode = addDispenser.StationCode,
						StorageLocation = addDispenser.StorageLocation,
						IsActive = true,
						UserCode = _authentication.Usercode(),
						TillNumber = addDispenser.TillNumber ?? string.Empty,
					};
					await _context.Dispensers.AddAsync(dispenser);
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Dispenser added successfully", dispenser);
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
		//Update a dispenser
		public async Task<ServiceResponse<object>> UpdateDispenser(UpdateDispenserDto updateDispenser)
		{
			try
			{
				if (updateDispenser is null)
					return ServiceResponse<object>.Information("Dispenser details cannot be empty", null);
				else
				{
					var dispenser = _context.Dispensers.Where(x => x.DispenserCode == updateDispenser.DispenserCode).FirstOrDefault();
					if (dispenser is null)
						return ServiceResponse<object>.Information("Dispenser not found", null);
					dispenser.DispenserName = updateDispenser.DispenserName;
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Success("Dispenser updated successfully", null);
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
		//List all active dispensers with their station names
		public async Task<ServiceResponse<object>> ListDispensers()
		{
			try
			{
				var dispensers = await (from s in _context.Stations
										join d in _context.Dispensers on s.StationCode equals d.StationCode
										select new
										{
											s.StationName,
											s.StationCode,
											d.DispenserName,
											d.DispenserCode,
										}
										).ToListAsync();
				if (dispensers.Count == 0)
					return ServiceResponse<object>.Information("Dispensers not found", null);
				return ServiceResponse<object>.Success("Dispensers retrieved successfully", dispensers);
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
		//Activate or deactivate a dispenser on same Method
		public async Task<ServiceResponse<object>> ActivateDispenser(string dispenserCode, bool status)
		{
			try
			{
				var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.DispenserCode == dispenserCode);
				if (dispenser == null)
					return ServiceResponse<object>.Information("Dispenser not found", null);
				dispenser.IsActive = status;
				await _context.SaveChangesAsync();
				return ServiceResponse<object>.Success("Dispenser updated successfully", null);
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
		//List Station Dispenser by StationId
		public async Task<ServiceResponse<object>> ListStationDispensers(string stationCode)
		{
			try
			{
				var dispensers = await _context.Dispensers.Where(x => x.StationCode == stationCode).ToListAsync();
				if (dispensers.Count == 0)
				{
					return ServiceResponse<object>.Information("Dispensers not found", null);
				}
				var dispenserList = new List<object>();
				foreach (var dispenser in dispensers)
				{
					var dispenserDetails = new
					{
						dispenser.DispenserCode,
						dispenser.DispenserName,
						dispenser.IsActive
					};

					dispenserList.Add(dispenserDetails);
				}
				return ServiceResponse<object>.Success("Dispensers listed successfully", dispenserList);
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
		//List Dispenser Nozzles by DispenserId
		public async Task<ServiceResponse<object>> ListDispenserNozzles(string dispenserCode)
		{
			try
			{
				var nozzles = await _context.Nozzles.Where(x => x.DispenserCode == dispenserCode).ToListAsync();
				if (nozzles.Count == 0)
				{
					return ServiceResponse<object>.Information("Nozzles not found", null);
				}
				var nozzleList = new List<object>();
				foreach (var nozzle in nozzles)
				{
					var nozzleDetails = new
					{
						nozzle.NozzleCode,
						nozzle.NozzleName,
						nozzle.IsActive
					};

					nozzleList.Add(nozzleDetails);
				}
				return ServiceResponse<object>.Success("Nozzles listed successfully", nozzleList);
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
		// List Station Dispenser by StationCode
		public async Task<ServiceResponse<object>> ListStationDispensersByStationCode(string stationCode)
		{
			try
			{
				var dispensers = await _context.Dispensers.Where(x => x.StationCode == stationCode).ToListAsync();
				if (dispensers.Count == 0)
				{
					return ServiceResponse<object>.Information("Dispensers not found", null);
				}
				var dispenserList = new List<object>();
				foreach (var dispenser in dispensers)
				{
					var dispenserDetails = new
					{
						dispenser.DispenserCode,
						dispenser.DispenserName,
						dispenser.IsActive
					};

					dispenserList.Add(dispenserDetails);
				}
				return ServiceResponse<object>.Success("Dispensers listed successfully", dispenserList);
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
		//List Dispenser Nozzles by DispenserCode
		public async Task<ServiceResponse<object>> ListDispenserNozzlesByDispenserCode(string dispenserCode)
		{
			try
			{
				var nozzles = await _context.Nozzles.Where(x => x.DispenserCode == dispenserCode).ToListAsync();
				if (nozzles.Count == 0)
				{
					return ServiceResponse<object>.Information("Nozzles not found", null);
				}
				var nozzleList = new List<object>();
				foreach (var nozzle in nozzles)
				{
					var nozzleDetails = new
					{
						nozzle.NozzleCode,
						nozzle.NozzleName,
						nozzle.IsActive
					};

					nozzleList.Add(nozzleDetails);
				}
				return ServiceResponse<object>.Success("Nozzles listed successfully", nozzleList);
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


