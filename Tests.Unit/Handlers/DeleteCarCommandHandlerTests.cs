using CarManager.Application.Cars.Commands.DeleteCar;
using CarManager.Tests.Shared;
using CarManager.Tests.Shared.Generators;
using System.Threading.Tasks;
using Xunit;

namespace CarManager.Tests.Unit.Handlers
{
	public class DeleteCarCommandHandlerTests : IClassFixture<CarGenerator>
	{
		private readonly CarGenerator generator;

		public DeleteCarCommandHandlerTests(CarGenerator generator)
		{
			this.generator = generator;
		}

		[Fact]
		public async Task ReturnsUnitWhenCarNotFound()
		{
			var dbContext = DbContextFactory.CreateInMemory();
			var handler = new DeleteCarCommandHandler(dbContext);
			var result = await handler.Handle(generator.DeleteCommand, default);
			
			Assert.StrictEqual(MediatR.Unit.Value, result);
		}

		[Fact]
		public async Task ReturnsUnitWhenCarFound()
		{
			var dbContext = DbContextFactory.CreateInMemory();
			var entity = generator.Entity;
			dbContext.Cars.Add(entity);
			await dbContext.SaveChangesAsync();

			var handler = new DeleteCarCommandHandler(dbContext);
			var result = await handler.Handle(new DeleteCarCommand { Id = entity.PublicId }, default);
			
			Assert.StrictEqual(MediatR.Unit.Value, result);
		}
	}
}
