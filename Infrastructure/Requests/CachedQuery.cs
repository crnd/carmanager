using System;
using System.Text.Json.Serialization;

namespace CarManager.Infrastructure.Requests
{
	public abstract class CachedQuery<T> : IQuery<T>
		where T : class
	{
		[JsonIgnore]
		public TimeSpan CacheExpiration { get; set; } = TimeSpan.FromMinutes(1);
	}
}
