using FluentValidation;

namespace CarManager.Application.Cars.Commands.UpdateCar
{
	public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
	{
		public UpdateCarCommandValidator()
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
