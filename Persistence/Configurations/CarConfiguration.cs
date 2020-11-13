using CarManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarManager.Persistence.Configurations
{
	public class CarConfiguration : PublicEntityConfiguration<Car>
	{
		public override void Configure(EntityTypeBuilder<Car> builder)
		{
			base.Configure(builder);

			builder
				.Property(e => e.Make)
				.IsRequired();

			builder
				.Property(e => e.Model)
				.IsRequired();

			builder
				.HasIndex(e => new { e.Make, e.Model, e.Power })
				.IsUnique();
		}
	}
}
