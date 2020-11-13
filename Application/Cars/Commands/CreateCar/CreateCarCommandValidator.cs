using FluentValidation;

namespace CarManager.Application.Cars.Commands.CreateCar
{
	public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
	{
		public CreateCarCommandValidator()
		{
			RuleFor(c => c.Make)
				.NotEmpty();

			RuleFor(c => c.Model)
				.NotEmpty();

			RuleFor(c => c.Power)
				.GreaterThan(0);
		}
	}
}
