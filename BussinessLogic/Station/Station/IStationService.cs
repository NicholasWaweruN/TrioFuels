using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Shifts.Station;

namespace BusinessLogic.Station.Station
{
    public interface IStationService
    {
        Task<ServiceResponse<object>> ActivateStation(string stationCode, bool status);
        Task<ServiceResponse<object>> AddStation(AddStationDto addStation);
        Task<ServiceResponse<object>> GetStations();
        Task<ServiceResponse<object>> UpdateStation(UpdateStationDto updateStation);
    }
}