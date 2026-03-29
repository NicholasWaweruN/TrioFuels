using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;
using DataAccessLayer.EntityModels.Transactions;

namespace BusinessLogic.Sales.MissingSales
{
    public interface IMissingSales
    {
        Task<ServiceResponse<object>> AddMissingSales(MisingSaleDto sales);
        Task<ServiceResponse> DeferVariance(string shiftNumber);
        Task<UserAndPrice> GetEmployeeAsync(string userCode);
        Task<ServiceResponse> OffWriteVariance(string shiftNumber);
    }
}