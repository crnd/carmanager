using CarManager.Infrastructure.Exceptions;
using CarManager.Persistence.Entities;
using System;

namespace CarManager.Application.Cars.Exceptions
{
	public class CarNotFoundException : EntityNotFoundException
	{
		public CarNotFoundException(Guid id)
			: base(typeof(Car), id)
		{
		}
	}
}
