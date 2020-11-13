namespace CarManager.Tests.Integration.Client
{
	public class TestHttpClientPostResponse<TResponse> : TestHttpClientResponse<TResponse>
	{
		public string Location { get; set; }
	}
}
