using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace CarManager.Infrastructure.Exceptions
{
	public class RequestValidationException : CarManagerException
	{
		public IDictionary<string, string[]> Failures { get; }

		public RequestValidationException(ValidationFailure[] validationFailures)
			: this()
		{
			var propertyNames = validationFailures
				.Select(failure => failure.PropertyName)
				.Distinct();

			foreach (var propertyName in propertyNames)
			{
				var propertyFailures = validationFailures
					.Where(failure => failure.PropertyName == propertyName)
					.Select(failure => failure.ErrorMessage)
					.ToArray();

				Failures.Add(propertyName, propertyFailures);
			}
		}

		public RequestValidationException()
			: this("Request validation failed.")
		{
			Failures = new Dictionary<string, string[]>();
		}

		public RequestValidationException(string message)
			: base(message)
		{
		}
	}
}
