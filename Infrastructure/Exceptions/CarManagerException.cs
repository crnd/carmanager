using System;

namespace CarManager.Infrastructure.Exceptions
{
	public abstract class CarManagerException : Exception
	{
		protected CarManagerException(string message)
			: base(message)
		{
		}
	}
}
