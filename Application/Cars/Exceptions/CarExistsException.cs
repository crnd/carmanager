using CarManager.Infrastructure.Exceptions;
using CarManager.Persistence.Entities;

namespace CarManager.Application.Cars.Exceptions
{
	public class CarExistsException : EntityExistsException
	{
		public CarExistsException(string make, string model, int power)
			: base($"{nameof(Car)} already exists with make {make}, model {model} and power {power}.")
		{
		}
	}
}
