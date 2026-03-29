using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Common;
using DataAccessLayer.DTOs.Sales;

namespace BussinessLogic.Stock.Adjust_Stock
{
	public interface IAdjustStock
	{
		Task<ServiceResponse<object>> AdjustStockTake([Required] int takeType, AdjustStockTakeDto adjust);
	}
}