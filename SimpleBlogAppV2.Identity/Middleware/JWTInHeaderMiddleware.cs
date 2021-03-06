﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Identity.Middleware
{
	public class JWTInHeaderMiddleware
	{
		private readonly RequestDelegate next;

		public JWTInHeaderMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			string token = context.Request.Cookies["simpleBlogApp_auth_token"];
			if (!string.IsNullOrWhiteSpace(token))
			{
				context.Request.Headers.Append("Authorization", "Bearer " + token);
			}

			await next.Invoke(context).ConfigureAwait(false);
		}
	}
}
