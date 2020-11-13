using CarManager.Persistence;
using CarManager.Tests.Integration.Client;
using CarManager.Tests.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CarManager.Tests.Integration
{
	public class ApiApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
		where TStartup : class
	{
		protected override IHostBuilder CreateHostBuilder()
		{
			return base.CreateHostBuilder();
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var dbContextDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<CarManagerContext>));
				services.Remove(dbContextDescriptor);

				services.AddDbContext<CarManagerContext>(options => options.UseInMemoryDatabase(nameof(CarManagerContext)));

				using var scope = services.BuildServiceProvider().CreateScope();
				var scopedProvider = scope.ServiceProvider;
				var context = scopedProvider.GetRequiredService<CarManagerContext>();

				if (context.Database.EnsureCreated())
				{
					Seeding.Initialize(context);
				}
			});
		}

		public TestHttpClient CreateTestClient() => new TestHttpClient(CreateClient());

		public TestHttpClient CreateTestClient(string defaultRoute)
		{
			var client = CreateClient();
			client.BaseAddress = new Uri(client.BaseAddress, defaultRoute);
			return new TestHttpClient(client);
		}
	}
}
