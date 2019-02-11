using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Identity.Constants;
using SimpleBlogAppV2.Identity.Factory;
using SimpleBlogAppV2.Identity.Middleware;
using SimpleBlogAppV2.Identity.Models;
using System;

namespace SimpleBlogAppV2.Identity
{
	public static class IdentityConfiguration
	{
		public static IdentityBuilder ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors();
			services.AddSingleton<IJwtFactory, JwtFactory>();

			IdentityBuilder identityBuilder = services.AddDefaultIdentity<AppUser>();

			services.ConfigureIdentityOptions();
			//services.ConfigureCookie();
			services.ConfigureJWT(configuration);

			return identityBuilder;
		}

		private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");
			var jwtIssuerOptions = new JwtIssuerOptions()
			{
				Issuer = jwtAppSettingOptions["Issuer"],
				Audience = jwtAppSettingOptions["Audience"],
				ValidFor = TimeSpan.FromMinutes(5),
				KEY = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH" // todo: get this from somewhere secure
			};
			services.Configure<JwtIssuerOptions>(option =>
			{
				option.Issuer = jwtIssuerOptions.Issuer;
				option.Audience = jwtIssuerOptions.Audience;
				option.ValidFor = jwtIssuerOptions.ValidFor;
				option.KEY = jwtIssuerOptions.KEY;
			});

			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();

				options.AddPolicy("GetAdminPosts", policy => policy.RequireClaim(JwtClaimIdentifiers.Rol, "Admin"));
			});

			services.
				AddAuthentication(o =>
				{
					o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(o =>
				{
					o.RequireHttpsMetadata = false;
					o.SaveToken = true;
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = jwtIssuerOptions.Issuer,

						ValidateAudience = true,
						ValidAudience = jwtIssuerOptions.Audience,

						ValidateIssuerSigningKey = true,
						IssuerSigningKey = jwtIssuerOptions.SymmetricSecurityKey,

						RequireExpirationTime = false,
						ValidateLifetime = false,
						ClockSkew = TimeSpan.Zero,
					};
				});
		}

		private static void ConfigureCookie(this IServiceCollection services)
		{
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(20);
				options.SlidingExpiration = true;
			});
		}

		private static void ConfigureIdentityOptions(this IServiceCollection services)
		{
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

				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});
		}

		public static void UseAppIdentity(this IApplicationBuilder app)
		{
			app.UseMiddleware<JWTInHeaderMiddleware>();
			app.UseCors(o =>
			{
				o.AllowAnyOrigin();
				o.AllowAnyMethod();
				o.AllowAnyHeader();
			});
			//app.UseCookiePolicy();
			app.UseAuthentication();
		}
	}
}