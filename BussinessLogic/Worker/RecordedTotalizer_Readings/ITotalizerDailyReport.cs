
namespace BussinessLogic.Worker.RecordedTotalizer_Readings
{
	public interface ITotalizerDailyReport
	{
		Task<MemoryStream> GenerateStyledTotalizerExcelWithAllDaysAsync();
	}
}