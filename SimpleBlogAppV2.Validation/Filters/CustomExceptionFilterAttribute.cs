using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleBlogAppV2.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;

namespace SimpleBlogAppV2.Validation.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public readonly Dictionary<Type, Action<ExceptionContext>> exceptionHandlers;

		public CustomExceptionFilterAttribute()
		{
			exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>();
			exceptionHandlers.Add(typeof(BlogValidationException), HandleBlogValidationException);
			exceptionHandlers.Add(typeof(BlogNotFoundException), HandleBlogNotFoundException);
			exceptionHandlers.Add(typeof(IdentityRegistrationException), HandleIdentityRegistrationException);
			exceptionHandlers.Add(typeof(LoginFailureException), HandleLoginFailureException);
		}

		public override void OnException(ExceptionContext context)
		{
			context.HttpContext.Response.ContentType = "application/json";

			Type exType = context.Exception.GetType();
			if (exceptionHandlers.ContainsKey(exType))
			{
				exceptionHandlers[exType].Invoke(context);
			}
			else
			{
				HandleDefaultException(context);
			}
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

		private void HandleIdentityRegistrationException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Result = new JsonResult(((IdentityRegistrationException)context.Exception).Failures);
		}

		private void HandleLoginFailureException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Result = new JsonResult(new
			{
				error = new[] { context.Exception.Message },
				stackTrace = context.Exception.StackTrace
			});
		}
	}
}