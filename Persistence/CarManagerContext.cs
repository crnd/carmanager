using CarManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Persistence
{
	public class CarManagerContext : DbContext
	{
		public CarManagerContext(DbContextOptions<CarManagerContext> options) : base(options)
		{
		}

		public DbSet<Car> Cars { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var now = DateTime.UtcNow;

			var entries = ChangeTracker.Entries<IEntity>();
			foreach (var entry in entries)
			{
				switch (entry.State)
				{
					case EntityState.Added:
						if (entry.Entity is IPublicEntity entity)
						{
							entity.PublicId = Guid.NewGuid();
						}

						entry.Entity.Created = now;
						entry.Entity.Updated = now;
						break;

					case EntityState.Modified:
						entry.Entity.Updated = now;
						break;

					case EntityState.Deleted:
						entry.Entity.Deleted = now;
						entry.State = EntityState.Modified;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarManagerContext).Assembly);
		}
	}
}
