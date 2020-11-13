using AutoMapper;
using CarManager.Application.Cars.Exceptions;
using CarManager.Persistence;
using CarManager.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Application.Cars.Commands.CreateCar
{
	public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarDto>
	{
		private readonly CarManagerContext context;
		private readonly IMapper mapper;

		public CreateCarCommandHandler(CarManagerContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<CarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
		{
			var car = mapper.Map<Car>(request);
			context.Cars.Add(car);

			try
			{
				await context.SaveChangesAsync(cancellationToken);
			}
			catch (DbUpdateException)
			{
				var carExists = await context.Cars
					.AnyAsync(c => c.Make == request.Make && c.Model == request.Model && c.Power == request.Power, cancellationToken);
				if (carExists)
				{
					throw new CarExistsException(request.Make, request.Model, request.Power);
				}

				throw;
			}

			return mapper.Map<CarDto>(car);
		}
	}
}
