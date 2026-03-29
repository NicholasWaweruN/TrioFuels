using Microsoft.Graph;

namespace BussinessLogic.Messaging
{
	public interface IGraphHelper
	{
		GraphServiceClient Client { get; }
	}
}