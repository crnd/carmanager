using System;
using System.Linq;

namespace CarManager.Infrastructure.Exceptions
{
	public abstract class EntityNotFoundException : CarManagerException
	{
		protected EntityNotFoundException(Type entityType, Guid id)
			: base($"{entityType.Name} was not found with ID {id}.")
		{
		}

		protected EntityNotFoundException(Type entityType, params (string, Guid)[] ids)
			: base($"{entityType.Name} was not found with IDs {ids.Select(id => id.Item1 + ": " + id.Item2)}.")
		{
		}
	}
}
