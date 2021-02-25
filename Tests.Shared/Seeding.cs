using CarManager.Persistence;
using CarManager.Persistence.Entities;
using CarManager.Tests.Shared.Generators;

namespace CarManager.Tests.Shared
{
	public static class Seeding
	{
		public static Car[] Cars { get; private set; }

		public static void Initialize(in CarManagerContext context)
		{
			Cars = GenerateEntities<Car, CarGenerator>(50);

			context.Cars.AddRange(Cars);

			context.SaveChanges();
		}

		private static TEntity[] GenerateEntities<TEntity, TGenerator>(in int amount)
			where TEntity : class, IEntity
			where TGenerator : IEntityGenerator<TEntity>, new()
		{
			var generator = new TGenerator();
			var entities = new TEntity[amount];

			for (var i = 0; i < amount; i++)
			{
				entities[i] = generator.Entity;
			}

			return entities;
		}
	}
}
