using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Infrastructure.Cache
{
	/// <summary>
	/// Interface for using an asynchronous key-value cache with duration-based expiration.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Get an item from the cache with the specified <paramref name="key"/>.
		/// </summary>
		/// <typeparam name="T">Cache item type to fetch from the cache.</typeparam>
		/// <param name="key">Unique identifier for the requested cache entry.</param>
		/// <param name="cancellationToken">Propagates notifications that the operation should be canceled.</param>
		/// <returns><see cref="Task"/> containing the found cache item or null.</returns>
		public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
			where T : class;

		/// <summary>
		/// Store an item to the cache with the specified <paramref name="key"/>.
		/// </summary>
		/// <typeparam name="T">Cache item type to store to the cache.</typeparam>
		/// <param name="key">Unique identifier for the cache entry to store.</param>
		/// <param name="value">The value to set in the cache.</param>
		/// <param name="expiration">Duration until the cache entry should be evicted.</param>
		/// <param name="cancellationToken">Propagates notifications that the operation should be canceled.</param>
		/// <returns><see cref="Task"/></returns>
		public Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken)
			where T : class;
	}
}
