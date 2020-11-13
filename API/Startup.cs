using AutoMapper;
using CarManager.Application.Cars;
using CarManager.Infrastructure.Behaviors;
using CarManager.Infrastructure.Cache;
using CarManager.Infrastructure.Requests;
using CarManager.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace CarManager.API
{
	public class Startup
	{
		private const string ApiTitle = "CarManager";
		private const string ApiVersion = "v1";

		public void ConfigureServices(IServiceCollection services)
		{
			var applicationAssembly = typeof(CarDto).Assembly;
			var infrastructureAssembly = typeof(IQuery<>).Assembly;

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(QueryCachingBehavior<,>));
			services.AddScoped<ICache, InMemoryCache>();

			services
				.AddDbContext<CarManagerContext>(o => o.UseInMemoryDatabase(nameof(CarManagerContext)))
				.AddMemoryCache()
				.AddMediatR(new [] { applicationAssembly, infrastructureAssembly })
				.AddAutoMapper(applicationAssembly)
				.AddSwaggerGen(o => {
					o.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiTitle, Version = ApiVersion });
					o.AddFluentValidationRules();

					var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
					o.IncludeXmlComments(apiXmlPath, true);

					var applicationXmlFile = $"{applicationAssembly.GetName().Name}.xml";
					var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
					o.IncludeXmlComments(applicationXmlPath);
				})
				.AddControllers(o => o.Filters.Add(typeof(CustomExceptionHandler)))
				.AddFluentValidation(c => c.RegisterValidatorsFromAssembly(applicationAssembly))
				.AddMvcOptions(o => o.ModelValidatorProviders.Clear());
		}

		public void Configure(IApplicationBuilder app)
		{
			app
				.UseSwagger()
				.UseSwaggerUI(o => o.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"{ApiTitle} {ApiVersion}"))
				.UseRouting()
				.UseEndpoints(e => e.MapControllers());
		}
	}
}
