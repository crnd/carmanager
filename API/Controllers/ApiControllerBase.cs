using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace CarManager.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Consumes(MediaTypeNames.Application.Json)]
	[Produces(MediaTypeNames.Application.Json)]
	public abstract class ApiControllerBase : ControllerBase
	{
		private ISender sender;

		protected ISender Sender => sender ??= HttpContext.RequestServices.GetService<ISender>();
	}
}
