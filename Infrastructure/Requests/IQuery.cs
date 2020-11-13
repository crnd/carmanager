using MediatR;

namespace CarManager.Infrastructure.Requests
{
	public interface IQuery<T> : IRequest<T>
		where T : class
	{
	}
}
