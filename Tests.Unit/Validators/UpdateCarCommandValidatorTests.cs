using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Tests.Shared.Generators;
using Xunit;

namespace CarManager.Tests.Unit.Validators
{
	public class UpdateCarCommandValidatorTests : IClassFixture<CarGenerator>
	{
		private readonly CarGenerator generator;
		private readonly UpdateCarCommandValidator validator;

		public UpdateCarCommandValidatorTests(CarGenerator generator)
		{
			this.generator = generator;
			validator = new UpdateCarCommandValidator();
		}

		[Theory]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("CarMaker", true)]
		public void Make(string make, bool isValid)
		{
			var command = generator.UpdateCommand;
			command.Make = make;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}

		[Theory]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("CarModel", true)]
		public void Model(string model, bool isValid)
		{
			var command = generator.UpdateCommand;
			command.Model = model;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}

		[Theory]
		[InlineData(150, true)]
		[InlineData(0, false)]
		[InlineData(-100, false)]
		public void Power(int power, bool isValid)
		{
			var command = generator.UpdateCommand;
			command.Power = power;

			Assert.StrictEqual(isValid, validator.Validate(command).IsValid);
		}
	}
}
