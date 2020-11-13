using CarManager.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace CarManager.API
{
	public class CustomExceptionHandler : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			HttpStatusCode code;
			var problemDetails = new ProblemDetails();
			switch (context.Exception)
			{
				case EntityNotFoundException ex:
					code = HttpStatusCode.NotFound;
					problemDetails.Title = ex.Message;
					break;
				case RequestValidationException ex:
					code = HttpStatusCode.BadRequest;
					problemDetails = new ValidationProblemDetails(ex.Failures)
					{
						Title = ex.Message
					};
					break;
				case EntityExistsException ex:
					code = HttpStatusCode.Conflict;
					problemDetails.Title = ex.Message;
					break;
				case OperationCanceledException _:
					code = HttpStatusCode.BadRequest;
					break;
				default:
					code = HttpStatusCode.InternalServerError;
					break;
			}

			if (code != HttpStatusCode.InternalServerError)
			{
				context.ExceptionHandled = true;
			}

			problemDetails.Status = (int)code;
			context.HttpContext.Response.StatusCode = (int)code;
			context.Result = new ObjectResult(problemDetails);
		}
	}
}
