using CarManager.Infrastructure.Requests;
using MediatR;
using System;

namespace CarManager.Application.Cars.Commands.DeleteCar
{
	public class DeleteCarCommand : ICommand<Unit>
	{
		public Guid Id { get; set; }
	}
}
