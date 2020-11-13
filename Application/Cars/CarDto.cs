using System;

namespace CarManager.Application.Cars
{
	public class CarDto
	{
		/// <summary>
		/// ID of the car.
		/// </summary>
		/// <example>ffe46427-a83a-405f-b54f-b9dabfddb5d0</example>
		public Guid Id { get; set; }

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
