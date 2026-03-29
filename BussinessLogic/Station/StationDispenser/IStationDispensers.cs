using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Shifts.Station;

namespace BusinessLogic.Station.StationDispenser
{
	public interface IStationDispensers
	{
		Task<ServiceResponse<object>> ActivateDispenser(string dispenserCode, bool status);
		Task<ServiceResponse<object>> AddDispenser(AddDispenserDto addDispenser);
		Task<ServiceResponse<object>> ListDispenserNozzles(string dispenserCode);
		Task<ServiceResponse<object>> ListDispenserNozzlesByDispenserCode(string dispenserCode);
		Task<ServiceResponse<object>> ListDispensers();
		Task<ServiceResponse<object>> ListStationDispensers(string stationCode);
		Task<ServiceResponse<object>> ListStationDispensersByStationCode(string stationCode);
		Task<ServiceResponse<object>> UpdateDispenser(UpdateDispenserDto updateDispenser);
	}
}