using CarManager.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Infrastructure.Behaviors
{
	public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : notnull
	{
		private readonly IEnumerable<IValidator<TRequest>> validators;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			this.validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var context = new ValidationContext<TRequest>(request);
			var validationFailures = validators
				.Select(validator => validator.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(failures => failures != null)
				.ToArray();

			if (validationFailures.Any())
			{
				throw new RequestValidationException(validationFailures);
			}

			return next();
		}
	}
}
