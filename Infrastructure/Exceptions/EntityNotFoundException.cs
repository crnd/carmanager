using System;

namespace CarManager.Infrastructure.Exceptions
{
	public abstract class EntityNotFoundException : CarManagerException
	{
		protected EntityNotFoundException(Type entityType, Guid id)
			: base($"{entityType.Name} was not found with ID {id}.")
		{
		}
	}
}
