using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using System;
using System.Net;

namespace SimpleBlogAppV2.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			context.HttpContext.Response.ContentType = "application/json";

			if (context.Exception is BlogValidationException)
			{
				HandleBlogValidationException(context);
				return;
			}

			if (context.Exception is BlogNotFoundException)
			{
				HandleBlogNotFoundException(context);
				return;
			}

			HandleDefaultException(context);
		}

		private void HandleBlogValidationException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Result = new JsonResult(((BlogValidationException)context.Exception).Failures);
		}

		private void HandleBlogNotFoundException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
			context.Result = new JsonResult(new
			{
				error = new[] { context.Exception.Message },
				stackTrace = context.Exception.StackTrace
			});
		}

		private void HandleDefaultException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Result = new JsonResult(new
			{
				error = new[] { context.Exception.Message },
				stackTrace = context.Exception.StackTrace
			});
		}
	}
}
