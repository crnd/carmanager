using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarManager.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.Application.Cars.Queries.GetCars
{
	public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarDto>>
	{
		private readonly CarManagerContext context;
		private readonly IConfigurationProvider provider;

		public GetCarsQueryHandler(CarManagerContext context, IConfigurationProvider provider)
		{
			this.context = context;
			this.provider = provider;
		}

		public async Task<List<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
		{
			return await context.Cars
				.ProjectTo<CarDto>(provider)
				.ToListAsync(cancellationToken);
		}
	}
}
