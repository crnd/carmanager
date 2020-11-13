using CarManager.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Application.Cars.Commands.DeleteCar
{
	public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
	{
		private readonly CarManagerContext context;

		public DeleteCarCommandHandler(CarManagerContext context)
		{
			this.context = context;
		}

		public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
		{
			var car = await context.Cars.FirstOrDefaultAsync(c => c.PublicId == request.Id, cancellationToken);
			if (car != null)
			{
				context.Cars.Remove(car);
				await context.SaveChangesAsync(cancellationToken);
			}

			return Unit.Value;
		}
	}
}
