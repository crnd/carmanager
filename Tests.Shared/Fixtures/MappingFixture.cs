using AutoMapper;
using CarManager.Application.Cars;

namespace CarManager.Tests.Shared.Fixtures
{
	public class MappingFixture
	{
		public IMapper Mapper { get; }

		public MappingFixture()
		{
			var applicationAssembly = typeof(CarDto).Assembly;
			var configuration = new MapperConfiguration(c => c.AddMaps(applicationAssembly));
			Mapper = new Mapper(configuration);
		}
	}
}
