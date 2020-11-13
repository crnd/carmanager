using CarManager.Application.Cars;
using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Application.Cars.Commands.DeleteCar;
using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Application.Cars.Queries.GetCar;
using CarManager.Application.Cars.Queries.GetCars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarManager.API.Controllers
{
	public class CarsController : ApiControllerBase
	{
		/// <summary>
		/// Get all cars.
		/// </summary>
		/// <param name="cancellationToken">Propagates notification that request should be cancelled.</param>
		/// <returns>List of <see cref="CarDto"/> objects.</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<CarDto>>> GetCars(CancellationToken cancellationToken)
		{
			return await Sender.Send(new GetCarsQuery(), cancellationToken);
		}

		/// <summary>
		/// Get a single car.
		/// </summary>
		/// <param name="id">Id of the car to get.</param>
		/// <param name="cancellationToken">Propagates notification that request should be cancelled.</param>
		/// <returns>Single <see cref="CarDto"/> object.</returns>
		/// <response code="404">A car was not found with the given <paramref name="id"/>.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<CarDto>> GetCar(Guid id, CancellationToken cancellationToken)
		{
			return await Sender.Send(new GetCarQuery { Id = id }, cancellationToken);
		}

		/// <summary>
		/// Create a new car.
		/// </summary>
		/// <param name="command">Values to use for creating the car.</param>
		/// <param name="cancellationToken">Propagates notification that request should be cancelled.</param>
		/// <returns>Created <see cref="CarDto"/> object.</returns>
		/// <response code="400">Request body was not valid.</response>
		/// <response code="409">A car already exists with the given make, model and power.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
		public async Task<ActionResult<CarDto>> CreateCar(CreateCarCommand command, CancellationToken cancellationToken)
		{
			var car = await Sender.Send(command, cancellationToken);
			return CreatedAtAction(nameof(GetCar), new { car.Id }, car);
		}

		/// <summary>
		/// Update an existing car.
		/// </summary>
		/// <param name="id">Id of the car to update.</param>
		/// <param name="command">Values to use for updating the car.</param>
		/// <param name="cancellationToken">Propagates notification that request should be cancelled.</param>
		/// <returns>Updated <see cref="CarDto"/> object.</returns>
		/// <response code="400">Request body was not valid.</response>
		/// <response code="404">A car was not found with the given <paramref name="id"/>.</response>
		/// <response code="409">A car already exists with the given make, model and power.</response>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
		public async Task<ActionResult<CarDto>> UpdateCar(Guid id, UpdateCarCommand command, CancellationToken cancellationToken)
		{
			command.Id = id;
			return await Sender.Send(command, cancellationToken);
		}

		/// <summary>
		/// Delete an existing car.
		/// </summary>
		/// <param name="id">ID of the car to delete.</param>
		/// <param name="cancellationToken">Propagates notification that request should be cancelled.</param>
		/// <returns>No content.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> DeleteCar(Guid id, CancellationToken cancellationToken)
		{
			await Sender.Send(new DeleteCarCommand { Id = id }, cancellationToken);
			return NoContent();
		}
	}
}
