using CarManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManager.Persistence.Configurations
{
	public abstract class PublicEntityConfiguration<T> : EntityConfiguration<T>
		where T : class, IPublicEntity
	{
		public override void Configure(EntityTypeBuilder<T> builder)
		{
			base.Configure(builder);

			builder
				.HasIndex(e => e.PublicId)
				.IsUnique();
		}
	}
}
