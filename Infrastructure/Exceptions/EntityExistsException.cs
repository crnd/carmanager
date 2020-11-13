namespace CarManager.Infrastructure.Exceptions
{
	public abstract class EntityExistsException : CarManagerException
	{
		protected EntityExistsException(string message)
			: base(message)
		{
		}
	}
}
