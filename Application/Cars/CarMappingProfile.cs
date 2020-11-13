using AutoMapper;
using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Persistence.Entities;

namespace CarManager.Application.Cars
{
	public class CarMappingProfile : Profile
	{
		public CarMappingProfile()
		{
			CreateMap<Car, CarDto>()
				.ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.PublicId));

			CreateMap<CreateCarCommand, Car>();

			CreateMap<UpdateCarCommand, Car>()
				.ForMember(entity => entity.Id, options => options.Ignore());
		}
	}
}
