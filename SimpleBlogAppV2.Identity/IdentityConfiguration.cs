using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlogAppV2.Core.Entities;
using System;

namespace SimpleBlogAppV2.Identity
{
	public static class IdentityConfiguration
	{
		public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
		{
			IdentityBuilder identityBuilder = services.AddDefaultIdentity<AppUser>();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 3;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
				options.Lockout.MaxFailedAccessAttempts = 999;
				options.Lockout.AllowedForNewUsers = false;

				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(20);

				//options.LoginPath = "/Identity/Account/Login";
				//options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			return identityBuilder;
		}

		public static void UseAppIdentity(this IApplicationBuilder app)
		{
			app.UseCookiePolicy();
			app.UseAuthentication();
		}
	}
}