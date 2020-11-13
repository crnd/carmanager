using CarManager.Infrastructure.Requests;

namespace CarManager.Application.Cars.Commands.CreateCar
{
	public class CreateCarCommand : ICommand<CarDto>
	{
		/// <summary>
		/// Make of the car.
		/// </summary>
		/// <example>Redcar</example>
		public string Make { get; set; }

		/// <summary>
		/// Model of the car.
		/// </summary>
		/// <example>Tundra</example>
		public string Model { get; set; }

		/// <summary>
		/// Power of the car in kilowatts.
		/// </summary>
		/// <example>500</example>
		public int Power { get; set; }
	}
}
