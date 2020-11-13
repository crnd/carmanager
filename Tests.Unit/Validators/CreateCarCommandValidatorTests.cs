using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Tests.Shared.Generators;
using Xunit;

namespace CarManager.Tests.Unit.Validators
{
	public class CreateCarCommandValidatorTests : IClassFixture<CarGenerator>
	{
		private readonly CarGenerator generator;
		private readonly CreateCarCommandValidator validator;

		public CreateCarCommandValidatorTests(CarGenerator generator)
		{
			this.generator = generator;
			validator = new CreateCarCommandValidator();
		}

		[Theory]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("CarMaker", true)]
		public void Make(string make, bool isValid)
		{
			var command = generator.CreateCommand;
			command.Make = make;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}

		[Theory]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("CarModel", true)]
		public void Model(string model, bool isValid)
		{
			var command = generator.CreateCommand;
			command.Model = model;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}

		[Theory]
		[InlineData(150, true)]
		[InlineData(0, false)]
		[InlineData(-100, false)]
		public void Power(int power, bool isValid)
		{
			var command = generator.CreateCommand;
			command.Power = power;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}
	}
}
