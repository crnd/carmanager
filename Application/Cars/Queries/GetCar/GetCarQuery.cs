using CarManager.Infrastructure.Requests;
using System;

namespace CarManager.Application.Cars.Queries.GetCar
{
	public class GetCarQuery : IQuery<CarDto>
	{
		public Guid Id { get; set; }
	}
}
