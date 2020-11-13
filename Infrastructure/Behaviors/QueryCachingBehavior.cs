using CarManager.Infrastructure.Cache;
using CarManager.Infrastructure.Requests;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Infrastructure.Behaviors
{
	public class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : CachedQuery<TResponse>
		where TResponse : class
	{
		private readonly ICache cache;

		public QueryCachingBehavior(ICache cache)
		{
			this.cache = cache;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var cacheKey = typeof(TRequest).Name + JsonSerializer.Serialize(request);
			var cacheResult = await cache.GetAsync<TResponse>(cacheKey, cancellationToken);
			if (cacheResult != default(TResponse))
			{
				return cacheResult;
			}

			var response = await next();
			await cache.SetAsync(cacheKey, response, request.CacheExpiration, cancellationToken);
			return response;
		}
	}
}
