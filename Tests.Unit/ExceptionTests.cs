using CarManager.Application.Cars;
using CarManager.Infrastructure.Cache;
using CarManager.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarManager.Tests.Unit
{
	public class ExceptionTests
	{
		[Fact]
		public void ExceptionsInheritedFromCarManagerException()
		{
			var exceptions = new List<Type>();

			var applicationAssemblyExceptions = typeof(CarDto)
				.Assembly
				.GetTypes()
				.Where(t => typeof(Exception).IsAssignableFrom(t) && !t.IsAbstract);
			exceptions.AddRange(applicationAssemblyExceptions);

			var infrastructureAssemblyExceptions = typeof(ICache)
				.Assembly
				.GetTypes()
				.Where(t => typeof(Exception).IsAssignableFrom(t) && !t.IsAbstract);
			exceptions.AddRange(applicationAssemblyExceptions);

			foreach (var exception in exceptions)
			{
				Assert.True(
					typeof(CarManagerException).IsAssignableFrom(exception),
					$"{exception.Name} is not inherited from {nameof(CarManagerException)}.");
			}
		}
	}
}
