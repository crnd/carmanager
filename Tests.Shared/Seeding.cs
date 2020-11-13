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
			Cars = GenerateCars(50);

			context.Cars.AddRange(Cars);

			context.SaveChanges();
		}

		private static Car[] GenerateCars(in int amount)
		{
			var carGenerator = new CarGenerator();
			var cars = new Car[amount];

			for (var i = 0; i < amount; i++)
			{
				cars[i] = carGenerator.Entity;
			}

			return cars;
		}
	}
}
