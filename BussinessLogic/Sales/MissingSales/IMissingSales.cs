using DataAccessLayer.Common;

namespace BusinessLogic.Sales.MissingSales
{
	public interface IMissingSales
	{
		Task<ServiceResponse> DeferVariance(string shiftNumber);
		Task<ServiceResponse> OffWriteVariance(string shiftNumber);
	}
}