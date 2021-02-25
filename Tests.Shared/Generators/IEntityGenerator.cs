using CarManager.Persistence.Entities;

namespace CarManager.Tests.Shared.Generators
{
	public interface IEntityGenerator<TEntity>
		where TEntity : class, IEntity
	{
		public TEntity Entity { get; }
	}
}
