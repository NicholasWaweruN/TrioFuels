using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Messaging
{
	public class GraphHelper : IGraphHelper
	{
		private readonly GraphServiceClient _graphClient;

		public GraphHelper(string tenantId, string clientId, string clientSecret)
		{
			var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
			_graphClient = new GraphServiceClient(clientSecretCredential, new[] { "https://graph.microsoft.com/.default" });
		}

		public GraphServiceClient Client => _graphClient;
	}
}
