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
using Swashbuckle.AspNetCore.SwaggerGen;
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
			var apiAssembly = typeof(Startup).Assembly;
			var applicationAssembly = typeof(CarDto).Assembly;
			var infrastructureAssembly = typeof(IQuery<>).Assembly;

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(QueryCachingBehavior<,>));
			services.AddScoped<ICache, InMemoryCache>();

			services
				.AddDbContext<CarManagerContext>(o => o.UseInMemoryDatabase(nameof(CarManager)))
				.AddMemoryCache()
				.AddMediatR(new[] { applicationAssembly, infrastructureAssembly })
				.AddAutoMapper(applicationAssembly)
				.AddSwaggerGen(o =>
				{
					o.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = ApiTitle, Version = ApiVersion });
					o.AddFluentValidationRules();
					IncludeAssemblyComments(o, apiAssembly, true);
					IncludeAssemblyComments(o, applicationAssembly, false);
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

		private static void IncludeAssemblyComments(SwaggerGenOptions options, Assembly assembly, bool includeControllerComments)
		{
			var xmlFile = $"{assembly.GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath, includeControllerComments);
		}
	}
}
