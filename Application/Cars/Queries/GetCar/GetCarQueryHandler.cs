using AutoMapper;
using CarManager.Application.Cars.Exceptions;
using CarManager.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Application.Cars.Queries.GetCar
{
	public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarDto>
	{
		private readonly CarManagerContext context;
		private readonly IMapper mapper;

		public GetCarQueryHandler(CarManagerContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<CarDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
		{
			var car = await context.Cars.FirstOrDefaultAsync(c => c.PublicId == request.Id, cancellationToken);
			if (car == null)
			{
				throw new CarNotFoundException(request.Id);
			}

			return mapper.Map<CarDto>(car);
		}
	}
}
