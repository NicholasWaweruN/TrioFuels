using DataAccessLayer.Common;
using DataAccessLayer.EntityModels.Stations;

namespace BusinessLogic.Station.DispenserAssignments
{
    public interface IDispenserAssigments
    {
        Task<ServiceResponse<object>> AssignDispenserAsync(DispenserAssignmentDto assignment);
        Task<ServiceResponse<object>> GetAllAttendantsWithOtogasApp();
        Task<ServiceResponse<object>> GetAllDispenserAssignmentsAsync();
        Task<ServiceResponse<object>> GetAllDispenserAssignmentsAsync(string stationCode);
        Task<ServiceResponse<object>> UnAssignDispenserAsync(string userCode);
    }
}