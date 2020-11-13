using AutoMapper;
using CarManager.Application.Cars;
using CarManager.Persistence.Entities;
using CarManager.Tests.Shared.Fixtures;
using CarManager.Tests.Shared.Generators;
using Xunit;

namespace CarManager.Tests.Unit.Mapping
{
	public class CarMappingTests : IClassFixture<CarGenerator>, IClassFixture<MappingFixture>
	{
		private readonly CarGenerator generator;
		private readonly IMapper mapper;

		public CarMappingTests(CarGenerator generator, MappingFixture mappingFixture)
		{
			this.generator = generator;
			mapper = mappingFixture.Mapper;
		}

		[Fact]
		public void EntityToDto()
		{
			var entity = generator.Entity;
			var dto = mapper.Map<CarDto>(entity);

			Assert.StrictEqual(entity.PublicId, dto.Id);
			Assert.Equal(entity.Make, dto.Make);
			Assert.Equal(entity.Model, dto.Model);
			Assert.StrictEqual(entity.Power, dto.Power);
		}

		[Fact]
		public void CreateCommandToEntity()
		{
			var command = generator.CreateCommand;
			var entity = mapper.Map<Car>(command);

			Assert.Equal(command.Make, entity.Make);
			Assert.Equal(command.Model, entity.Model);
			Assert.StrictEqual(command.Power, entity.Power);
		}

		[Fact]
		public void UpdateCommandToEntity()
		{
			var command = generator.UpdateCommand;
			var entity = mapper.Map<Car>(command);

			Assert.Equal(command.Make, entity.Make);
			Assert.Equal(command.Model, entity.Model);
			Assert.StrictEqual(command.Power, entity.Power);
		}
	}
}
