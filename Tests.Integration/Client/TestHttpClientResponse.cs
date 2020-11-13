using System.Net;

namespace CarManager.Tests.Integration.Client
{
	public class TestHttpClientResponse<TResponse>
	{
		public HttpStatusCode StatusCode { get; set; }

		public TResponse Content { get; set; }
	}
}
