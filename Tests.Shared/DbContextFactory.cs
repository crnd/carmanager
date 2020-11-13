using CarManager.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Tests.Shared
{
	public static class DbContextFactory
	{
		public static CarManagerContext CreateInMemory(in string dbName = nameof(CarManagerContext))
		{
			var options = new DbContextOptionsBuilder<CarManagerContext>()
				.UseInMemoryDatabase(dbName)
				.Options;

			return new CarManagerContext(options);
		}
	}
}
