using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Infrastructure.Cache
{
	public interface ICache
	{
		public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
			where T : class;

		public Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken)
			where T : class;
	}
}
