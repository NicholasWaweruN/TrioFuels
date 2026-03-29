using BussinessLogic.Authentication.CommonTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Stations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BusinessLogic.Station.DispenserAssignments;
using BussinessLogic.Setup;

namespace BussinessLogic.Station.DispenserAssignments
{
	public class DispenserAssigments : IDispenserAssigments
    {
        private readonly OTOContext _context;
        private readonly IAuthCommonTasks _authentication;
        private readonly ICommonSetups _setups;
        public DispenserAssigments(OTOContext context, IAuthCommonTasks authentication, ICommonSetups setups)
        {
            _context = context;
            _authentication = authentication;
            _setups = setups;
        }

        public async Task<ServiceResponse<object>> AssignDispenserAsync(DispenserAssignmentDto assignment)
        {
            var response = new ServiceResponse<object>();
			try
			{

				var user = await _authentication.GetUserDetailsAsync(assignment.AttedantUserCode);
				if (user.ResponseCode.Equals(Response.Success))
				{
					if (user.ResponseObject is not null)
					{
						if (!user.ResponseObject.IsActive)
							return ServiceResponse<object>.Information($"User {user.ResponseObject.FirstName} {user.ResponseObject.LastName} Account is not active", null);

						var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.DispenserCode == assignment.DispenserCode);
						if (dispenser == null)
							return ServiceResponse<object>.Information("Dispenser does not exist", null);
						var stationName = await (from s in _context.Stations
												 join d in _context.Dispensers on s.StationCode equals d.StationCode
												 where d.DispenserCode.Equals(assignment.DispenserCode)
												 select s.StationName).FirstAsync();


						if (await (from s in _context.Shifts where s.DispenserCode.Equals(assignment.DispenserCode) && s.ShiftStatus.Equals(1) select s).AnyAsync())
							return ServiceResponse<object>.Information("There is an active shift in this dispenser", null);

						if (await (from d in _context.Dispensers where d.DispenserCode.Equals(assignment.DispenserCode) && d.IsActive == false select d).AnyAsync())
							return ServiceResponse<object>.Information("Dispenser is not active", null);

						if (await (from s in _context.Shifts where s.ShiftStatus.Equals(ShiftStatus.Variance) && s.UserCode.Equals(assignment.AttedantUserCode) select s).AnyAsync())
						{
							return ServiceResponse<object>.Information("the attendant has a variance!", null);
						}

						var assign = await _context.DispenserAssignments.Where(x => x.AttedantUserCode == assignment.AttedantUserCode).FirstOrDefaultAsync();
						if (assign is null)
						{
							if (await CheckAppAssignmentAsync(assignment.AttedantUserCode))
							{
								var ass = new DispenserAssignment
								{
									AttedantUserCode = assignment.AttedantUserCode,
									DispenserCode = assignment.DispenserCode,
									DateAssigned = DateTime.UtcNow,
									AssignedBy = _authentication.Name(),
									StationCode = assignment.StationCode,
								};


								//get Attedant Name
								var AttendantName = await (from a in _context.Users.Where(x => x.UserCode.Equals(assignment.AttedantUserCode))
														   select new
														   {
															   Name = string.Join(' ', a.FirstName, a.MiddName, a.LastName),
														   }).FirstOrDefaultAsync();

								await _context.DispenserAssignments.AddAsync(ass);
								await _context.SaveChangesAsync();

								var message = $"{_authentication.Name()} assigned {AttendantName} to dispenser {dispenser.DispenserName} at {stationName} station on {DateTime.UtcNow}";

								await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
								return ServiceResponse<object>.Success("Dispenser Assigned Successfully", null);
							}
							else
								return ServiceResponse<object>.Information("User is not assigned OtogasApp", null);
						}
						else
						{
							var AttendantName = await (from a in _context.Users.Where(x => x.UserCode.Equals(assignment.AttedantUserCode))
													   select new
													   {
														   Name = string.Join(' ', a.FirstName, a.MiddName, a.LastName),
													   }).FirstOrDefaultAsync();

							assign.DispenserCode = assignment.DispenserCode;
							assign.StationCode = assignment.StationCode;
							assign.AssignedBy = _authentication.Name();
							assign.DateAssigned = DateTime.UtcNow;

							_context.DispenserAssignments.Update(assign);
							await _context.SaveChangesAsync();
							var message = $"{_authentication.Name()} assigned {AttendantName} to dispenser {dispenser.DispenserName} at {stationName} station on {DateTime.UtcNow}";
							await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");
							return ServiceResponse<object>.Success("Dispenser Updated Successfully", new { assignment.DispenserCode, assignment.AttedantUserCode });

						}
					}
					return ServiceResponse<object>.Information("User Information Not Found", null);
				}
				return ServiceResponse<object>.Information("User not found", null);


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
        //Unassign a dispenser from a user
        public async Task<ServiceResponse<object>> UnAssignDispenserAsync(string userCode)
        {
            var response = new ServiceResponse<object>();
			try
			{
				//check if the dispenser is assigned to the user
				var check = await CheckDispenserAssignmentAsync(userCode);
				if (check.ResponseMessage != null)
				{
					if (check.ResponseCode.Equals(Response.Information))
					{
						return ServiceResponse<object>.Information(check.ResponseMessage, null);
					}
					if (check.ResponseCode.Equals(Response.Error))
					{
						return ServiceResponse<object>.Error(check.ResponseMessage, null);
					}
				}

				var user = await _authentication.GetUserDetailsAsync(userCode);
				if (user.ResponseCode.Equals(Response.Success))
				{
					if (user.ResponseObject is not null)
					{
						var stationName = string.Empty;
						var assign = await _context.DispenserAssignments.Where(x => x.AttedantUserCode == userCode).FirstOrDefaultAsync();
						if (assign != null)
						{
							var station = await _context.Stations.Where(x => x.StationCode == assign.StationCode).FirstOrDefaultAsync();
							if (station != null)
							{
								stationName = station.StationName;
							}
						}
						if (assign is not null)
						{
							_context.DispenserAssignments.Remove(assign);
							await _context.SaveChangesAsync();

							var message = $" {user.ResponseObject.FirstName} {user.ResponseObject.MiddName} {user.ResponseObject.LastName} has been Unassigned from  Dispenser {assign.DispenserCode} in {stationName} by {_authentication.Name()} on {DateTime.UtcNow}";
							await _authentication.AddUserTrail(message,MethodBase.GetCurrentMethod()?.Name ?? "");

							return ServiceResponse<object>.Success(message, null);
						}
						return ServiceResponse<object>.Information("Dispenser not assigned to user", null);
					}
					return ServiceResponse<object>.Information("User Information Not Found", null);
				}
				else
				{
					return ServiceResponse<object>.Information("User not found", null);
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
        //check if the user has a dispenser assigned
        private async Task<ServiceResponse<bool>> CheckDispenserAssignmentAsync(string userCode)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _authentication.GetUserDetailsAsync(userCode);
                if (user.ResponseCode.Equals(Response.Success))
                {
                    var assign = await (from dispenser in _context.Dispensers
                                        join assignment in _context.DispenserAssignments on dispenser.DispenserCode equals assignment.DispenserCode
                                        where assignment.AttedantUserCode.Equals(userCode)
                                        select assignment).AnyAsync();
                    if (!assign)
                    {
                        return ServiceResponse<bool>.Information("No Dispenser Assigned", false);

                    }
                    return ServiceResponse<bool>.Success("Dispenser Assigned", true);

                }
                else
                {
                    return ServiceResponse<bool>.Information("User not found", false);

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

				return ServiceResponse<bool>.Error("Something went wrong",false);

			}
		}

        //check if the user has been assigned an app "OTOGAS APP"
        private async Task<bool> CheckAppAssignmentAsync(string userCode)
        {
			try
			{
				var user = await _authentication.GetUserDetailsAsync(userCode);
				if (user.ResponseCode.Equals(Response.Success))
				{
					var assign = await (from userapp in _context.UserApps
										join apps in _context.ProtoApps on userapp.AppsCode equals apps.AppsCode
										where userapp.UserCode == userCode && apps.AppsCode == Apps.OtogasApp
										select userapp).AnyAsync();
					if (!assign)
						return false;
					return true;
				}
				else
					return false;
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


				return false;
			}
        }

        //list all dispensers assigned to users
        public async Task<ServiceResponse<object>> GetAllDispenserAssignmentsAsync()
        {
            var response = new ServiceResponse<object>();
            try
            {
                var assignments = await (from assignment in _context.DispenserAssignments
                                         join user in _context.Users on assignment.AttedantUserCode equals user.UserCode
                                         join dispenser in _context.Dispensers on assignment.DispenserCode equals dispenser.DispenserCode
                                         join station in _context.Stations on assignment.StationCode equals station.StationCode
                                         select new
                                         {
                                             AttendantName = string.Join(" ", user.FirstName, user.MiddName, user.LastName),
                                             dispenser.DispenserName,
                                             station.StationName,
                                             assignment.DateAssigned,
                                             assignment.AssignedBy
                                         }).ToListAsync();
                if (assignments.Count > 0)

                    return ServiceResponse<object>.Success("Dispenser Assignments", assignments);
                return ServiceResponse<object>.Information("No Dispenser Assignments", null);
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
        //List all Attendants who have OtogasApp in ProtoApps
        public async Task<ServiceResponse<object>> GetAllAttendantsWithOtogasApp()
        {
            var response = new ServiceResponse<object>();
            try
            {
                var attendants = await (from user in _context.Users
                                        join userapp in _context.UserApps on user.UserCode equals userapp.UserCode
                                        join app in _context.ProtoApps on userapp.AppsCode equals app.AppsCode
                                        where app.AppsCode == Apps.OtogasApp
                                        select new
                                        {
                                            Name = string.Join(" ", user.FirstName, user.MiddName, user.LastName),
                                            user.UserCode,
                                            user.PhoneNumber,
                                            user.Email,
                                            user.IsActive,
                                            user.PayrollNumber
                                        }).ToListAsync();
                if (attendants.Count > 0)
                    return ServiceResponse<object>.Information(string.Format("Attendants with {0}", Apps.OtogasApp), attendants);
                return ServiceResponse<object>.Information("No Attendants with OtogasApp", null);
            }
            catch (Exception ex)
            {
                return ServiceResponse<object>.Error("Something went wrong", ex.Message);
            }
        }
		//Get all Dispenser Assignments by Station
		public async Task<ServiceResponse<object>> GetAllDispenserAssignmentsAsync(string stationCode)
        {
            var response = new ServiceResponse<object>();
            try
            {
                var assignments = await (from assignment in _context.DispenserAssignments
                                         join user in _context.Users on assignment.AttedantUserCode equals user.UserCode
                                         join dispenser in _context.Dispensers on assignment.DispenserCode equals dispenser.DispenserCode
                                         join station in _context.Stations on assignment.StationCode equals station.StationCode
                                         where assignment.StationCode == stationCode
                                         select new
                                         {
                                             AttendantName = string.Join(" ", user.FirstName, user.MiddName, user.LastName),
											 dispenser.DispenserName,
											 station.StationName,
                                             assignment.DateAssigned,
                                             assignment.AssignedBy
                                         }).ToListAsync();
                if (assignments.Count > 0)
                    return ServiceResponse<object>.Success("Dispenser Assignments", assignments);
                return ServiceResponse<object>.Information("No Dispenser Assignments", null);
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
