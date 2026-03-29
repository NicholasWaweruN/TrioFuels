namespace BusinessLogic.Worker.SalesReport
{
	public interface IWorkerRecipients
	{
		Task<Mails?> GetRecipients(string reportCode);
	}
}