
using DataAccessLayer.Common;

namespace BusinessLogic.DashBoard
{
    public interface IDashBoard
    {
        Task<ServiceResponse<object>> GetDashBoardData();
    }
}