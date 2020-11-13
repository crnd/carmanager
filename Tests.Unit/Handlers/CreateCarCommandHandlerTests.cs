using AutoMapper;
using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Tests.Shared;
using CarManager.Tests.Shared.Fixtures;
using CarManager.Tests.Shared.Generators;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarManager.Tests.Unit.Handlers
{
	public class CreateCarCommandHandlerTests : IClassFixture<CarGenerator>, IClassFixture<MappingFixture>
	{
		private readonly CarGenerator generator;
		private readonly IMapper mapper;

		public CreateCarCommandHandlerTests(CarGenerator generator, MappingFixture mappingFixture)
		{
			this.generator = generator;
			mapper = mappingFixture.Mapper;
		}

		[Fact]
		public async Task GeneratesId()
		{
			var context = DbContextFactory.CreateInMemory();
			var handler = new CreateCarCommandHandler(context, mapper);

			var result = await handler.Handle(generator.CreateCommand, default);

			Assert.NotEqual(Guid.Empty, result.Id);
		}
	}
}
