using CarManager.Infrastructure.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarManager.Tests.Integration.Client
{
	public class TestHttpClient
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;

		public TestHttpClient(in HttpClient client)
		{
			this.client = client;
			options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<HttpStatusCode> GetAsync()
		{
			return await GetAsync(null);
		}

		public async Task<HttpStatusCode> GetAsync(string requestUri)
		{
			var response = await client.GetAsync(requestUri);
			return response.StatusCode;
		}

		public async Task<TestHttpClientResponse<TResponse>> GetAsync<TResponse>()
		{
			return await GetAsync<TResponse>(null);
		}

		public async Task<TestHttpClientResponse<TResponse>> GetAsync<TResponse>(string requestUri)
		{
			var response = await client.GetAsync(requestUri);
			var content = await response.Content.ReadAsStringAsync();
			return new TestHttpClientResponse<TResponse>
			{
				StatusCode = response.StatusCode,
				Content = JsonSerializer.Deserialize<TResponse>(content, options)
			};
		}

		public async Task<HttpStatusCode> PostAsync<TCommand>(TCommand command)
		{
			return await PostAsync(null, command);
		}


		public async Task<HttpStatusCode> PostAsync<TCommand>(string requestUri, TCommand command)
		{
			var httpContent = new StringContent(JsonSerializer.Serialize(command));
			httpContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
			var response = await client.PostAsync(requestUri, httpContent);
			return response.StatusCode;
		}

		public async Task<TestHttpClientPostResponse<TResponse>> PostAsync<TCommand, TResponse>(TCommand command)
			where TCommand : ICommand<TResponse>
		{
			return await PostAsync<TCommand, TResponse>(null, command);
		}


		public async Task<TestHttpClientPostResponse<TResponse>> PostAsync<TCommand, TResponse>(string requestUri, TCommand command)
			where TCommand : ICommand<TResponse>
		{
			var httpContent = new StringContent(JsonSerializer.Serialize(command));
			httpContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
			var response = await client.PostAsync(requestUri, httpContent);
			var content = await response.Content.ReadAsStringAsync();
			return new TestHttpClientPostResponse<TResponse>
			{
				StatusCode = response.StatusCode,
				Content = JsonSerializer.Deserialize<TResponse>(content, options),
				Location = response.Headers.GetValues("Location").Single()
			};
		}

		public async Task<TestHttpClientResponse<TResponse>> PutAsync<TCommand, TResponse>(string requestUri, TCommand command)
			where TCommand : ICommand<TResponse>
		{
			var httpContent = new StringContent(JsonSerializer.Serialize(command));
			httpContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
			var response = await client.PutAsync(requestUri, httpContent);
			var content = await response.Content.ReadAsStringAsync();
			return new TestHttpClientResponse<TResponse>
			{
				StatusCode = response.StatusCode,
				Content = JsonSerializer.Deserialize<TResponse>(content, options)
			};
		}

		public async Task<HttpStatusCode> PutAsync<TCommand>(string requestUri, TCommand command)
		{
			var httpContent = new StringContent(JsonSerializer.Serialize(command));
			httpContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
			var response = await client.PutAsync(requestUri, httpContent);
			return response.StatusCode;
		}

		public async Task<HttpStatusCode> DeleteAsync(string requestUri)
		{
			var response = await client.DeleteAsync(requestUri);
			return response.StatusCode;
		}
	}
}
