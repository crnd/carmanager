using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Infrastructure.Cache
{
	public class InMemoryCache : ICache
	{
		private readonly IMemoryCache cache;

		public InMemoryCache(IMemoryCache cache)
		{
			this.cache = cache;
		}

		public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
			where T : class
		{
			var result = (T)cache.Get(key);
			return Task.FromResult(result);
		}

		public Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken)
			where T : class
		{
			cache.Set(key, value, DateTimeOffset.Now.Add(expiration));
			return Task.CompletedTask;
		}
	}
}
