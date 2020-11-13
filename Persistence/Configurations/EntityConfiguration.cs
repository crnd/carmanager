using CarManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManager.Persistence.Configurations
{
	public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
		where T : class, IEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder
				.HasQueryFilter(e => !e.Deleted.HasValue);
		}
	}
}
