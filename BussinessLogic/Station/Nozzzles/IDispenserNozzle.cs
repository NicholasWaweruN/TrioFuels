using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Shifts.Station;

namespace BusinessLogic.Station.Nozzzles
{
    public interface IDispenserNozzle
    {
        Task<ServiceResponse<object>> ActivateNozzle(string nozzleCode, bool status);
        Task<ServiceResponse<object>> AddNozzle(AddNozzleDto addNozzle);
        Task<ServiceResponse<object>> GetNozzles();
        Task<ServiceResponse<object>> UpdateNozzle(UpdateNozzleDto updateNozzle);
    }
}