using CarManager.API;
using CarManager.Application.Cars;
using CarManager.Application.Cars.Commands.CreateCar;
using CarManager.Application.Cars.Commands.UpdateCar;
using CarManager.Tests.Integration.Client;
using CarManager.Tests.Shared;
using CarManager.Tests.Shared.Generators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CarManager.Tests.Integration
{
	public class CarTests : IClassFixture<ApiApplicationFactory<Startup>>, IClassFixture<CarGenerator>
	{
		private readonly TestHttpClient client;
		private readonly CarGenerator carGenerator;

		public CarTests(ApiApplicationFactory<Startup> factory, CarGenerator carGenerator)
		{
			client = factory.CreateTestClient("cars/");
			this.carGenerator = carGenerator;
		}

		[Fact]
		public async Task GetCarsReturnsOk()
		{
			var statusCode = await client.GetAsync();

			Assert.StrictEqual(HttpStatusCode.OK, statusCode);
		}

		[Fact]
		public async Task GetCarsReturnsSeededCars()
		{
			var result = await client.GetAsync<List<CarDto>>();

			Assert.NotEmpty(result.Content);
		}

		[Fact]
		public async Task GetCarReturns404NotFoundWhenNotFound()
		{
			var statusCode = await client.GetAsync(Guid.NewGuid().ToString());

			Assert.StrictEqual(HttpStatusCode.NotFound, statusCode);
		}

		[Fact]
		public async Task GetCarReturnsOkWhenFound()
		{
			var statusCode = await client.GetAsync(Seeding.Cars[7].PublicId.ToString());

			Assert.StrictEqual(HttpStatusCode.OK, statusCode);
		}

		[Fact]
		public async Task GetCarReturnsDtoWhenFound()
		{
			var seededCar = Seeding.Cars[1];
			var result = await client.GetAsync<CarDto>(seededCar.PublicId.ToString());

			Assert.StrictEqual(seededCar.PublicId, result.Content.Id);
			Assert.Equal(seededCar.Make, result.Content.Make);
			Assert.Equal(seededCar.Model, result.Content.Model);
			Assert.StrictEqual(seededCar.Power, result.Content.Power);
		}

		[Fact]
		public async Task CreateReturns201Created()
		{
			var statusCode = await client.PostAsync(carGenerator.CreateCommand);

			Assert.StrictEqual(HttpStatusCode.Created, statusCode);
		}

		[Fact]
		public async Task CreateReturnsCorrectDto()
		{
			var command = carGenerator.CreateCommand;
			var result = await client.PostAsync<CreateCarCommand, CarDto>(command);

			Assert.NotEqual(Guid.Empty, result.Content.Id);
			Assert.Equal(command.Make, result.Content.Make);
			Assert.Equal(command.Model, result.Content.Model);
			Assert.StrictEqual(command.Power, result.Content.Power);
		}

		[Fact]
		public async Task CreateReturnsCorrectLocationHeader()
		{
			var command = carGenerator.CreateCommand;
			var createResult = await client.PostAsync<CreateCarCommand, CarDto>(command);
			var getResult = await client.GetAsync<CarDto>(createResult.Location);

			Assert.Equal(command.Make, getResult.Content.Make);
			Assert.Equal(command.Model, getResult.Content.Model);
			Assert.StrictEqual(command.Power, getResult.Content.Power);
		}

		[Fact]
		public async Task UpdateReturns404NotFoundWhenNotFound()
		{
			var statusCode = await client.PutAsync(Guid.NewGuid().ToString(), carGenerator.UpdateCommand);

			Assert.StrictEqual(HttpStatusCode.NotFound, statusCode);
		}

		[Fact]
		public async Task UpdateReturnsOkWhenFound()
		{
			var statusCode = await client.PutAsync(Seeding.Cars[3].PublicId.ToString(), carGenerator.UpdateCommand);

			Assert.StrictEqual(HttpStatusCode.OK, statusCode);
		}

		[Fact]
		public async Task UpdateReturnsCorrectDto()
		{
			var id = Seeding.Cars[4].PublicId;
			var command = carGenerator.UpdateCommand;
			var result = await client.PutAsync<UpdateCarCommand, CarDto>(id.ToString(), command);

			Assert.StrictEqual(id, result.Content.Id);
			Assert.Equal(command.Make, result.Content.Make);
			Assert.Equal(command.Model, result.Content.Model);
			Assert.StrictEqual(command.Power, result.Content.Power);
		}

		[Fact]
		public async Task DeleteReturns204NoContentWhenNotFound()
		{
			var statusCode = await client.DeleteAsync(Guid.NewGuid().ToString());

			Assert.StrictEqual(HttpStatusCode.NoContent, statusCode);
		}

		[Fact]
		public async Task DeleteReturns204NoContentWhenFound()
		{
			var statusCode = await client.DeleteAsync(Seeding.Cars[15].PublicId.ToString());

			Assert.StrictEqual(HttpStatusCode.NoContent, statusCode);
		}
	}
}
