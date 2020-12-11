using MediatR;

namespace CarManager.Infrastructure.Requests
{
	public interface ICommand<T> : IRequest<T>
		where T : notnull
	{
	}
}
