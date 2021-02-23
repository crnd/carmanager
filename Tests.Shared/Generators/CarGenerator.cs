using Bogus;
using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Application.Cars.Commands.DeleteCar;
using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Persistence.Entities;

namespace CarManager.Tests.Shared.Generators
{
	public class CarGenerator : GenericGenerator<Car, CreateCarCommand, UpdateCarCommand, DeleteCarCommand>
	{
		public CarGenerator()
		{
			entity = new Faker<Car>()
				.RuleFor(c => c.Id, f => f.Random.Int(1))
				.RuleFor(c => c.PublicId, f => f.Random.Guid())
				.RuleFor(c => c.Make, f => f.Random.Word())
				.RuleFor(c => c.Model, f => f.Random.Word())
				.RuleFor(c => c.Power, f => f.Random.Int(50, 700))
				.RuleFor(c => c.Created, f => f.Date.Past(2))
				.RuleFor(c => c.Updated, f => f.Date.Past(1));

			createCommand = new Faker<CreateCarCommand>()
				.RuleFor(c => c.Make, f => f.Random.Word())
				.RuleFor(c => c.Model, f => f.Random.Word())
				.RuleFor(c => c.Power, f => f.Random.Int(50, 700));

			updateCommand = new Faker<UpdateCarCommand>()
				.RuleFor(c => c.Make, f => f.Random.Word())
				.RuleFor(c => c.Model, f => f.Random.Word())
				.RuleFor(c => c.Power, f => f.Random.Int(50, 700));

			deleteCommand = new Faker<DeleteCarCommand>()
				.RuleFor(c => c.Id, f => f.Random.Guid());
		}
	}
}
