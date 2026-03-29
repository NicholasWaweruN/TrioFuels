using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Shifts.Station;

namespace BusinessLogic.Station.StationTank
{
    public interface IStationTanks
    {
      
        Task<ServiceResponse<object>> AddTank(AddTankDto addTank);
        Task<ServiceResponse<object>> GetTanks();
        Task<ServiceResponse<object>> StationTank(string stationCode);
        Task<ServiceResponse<object>> UpdateTank(UpdateTankDto updateTank);
    }
}