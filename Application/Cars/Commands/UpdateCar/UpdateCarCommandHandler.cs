using AutoMapper;
using CarManager.Application.Cars.Exceptions;
using CarManager.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Application.Cars.Commands.UpdateCar
{
	public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, CarDto>
	{
		private readonly CarManagerContext context;
		private readonly IMapper mapper;

		public UpdateCarCommandHandler(CarManagerContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
		{
			var car = await context.Cars.FirstOrDefaultAsync(c => c.PublicId == request.Id, cancellationToken);
			if (car == null)
			{
				throw new CarNotFoundException(request.Id);
			}

			mapper.Map(request, car);
			context.Cars.Update(car);

			try
			{
				await context.SaveChangesAsync(cancellationToken);
			}
			catch (DbUpdateException)
			{
				var makeModelExists = await context.Cars
					.AnyAsync(c => c.PublicId != request.Id && c.Make == request.Make && c.Model == request.Model && c.Power == request.Power, cancellationToken);
				if (makeModelExists)
				{
					throw new CarExistsException(request.Make, request.Model, request.Power);
				}

				throw;
			}

			return mapper.Map<CarDto>(car);
		}
	}
}
