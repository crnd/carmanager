using AutoMapper;
using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Application.Cars.Exceptions;
using CarManager.Tests.Shared;
using CarManager.Tests.Shared.Fixtures;
using CarManager.Tests.Shared.Generators;
using System.Threading.Tasks;
using Xunit;

namespace CarManager.Tests.Unit.Handlers
{
	public class UpdateCarCommandHandlerTests : IClassFixture<CarGenerator>, IClassFixture<MappingFixture>
	{
		private readonly CarGenerator generator;
		private readonly IMapper mapper;

		public UpdateCarCommandHandlerTests(CarGenerator generator, MappingFixture mappingFixture)
		{
			this.generator = generator;
			mapper = mappingFixture.Mapper;
		}

		[Fact]
		public async Task ThrowsNotFoundWhenCarNotFound()
		{
			var context = DbContextFactory.CreateInMemory();
			var handler = new UpdateCarCommandHandler(context, mapper);
			var command = generator.UpdateCommand;

			await Assert.ThrowsAsync<CarNotFoundException>(() => handler.Handle(command, default));
		}
	}
}
