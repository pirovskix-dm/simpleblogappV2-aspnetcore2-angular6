using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Identity.Factory;
using SimpleBlogAppV2.Identity.Models;
using System;
using System.Text;

namespace SimpleBlogAppV2.Identity
{
	public static class IdentityConfiguration
	{
		private static readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("iNivDmHLpUA223sqsfhqGbMRdRj1PVkH")); // todo: get this from somewhere secure

		public static IdentityBuilder ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IJwtFactory, JwtFactory>();

			IdentityBuilder identityBuilder = services.AddDefaultIdentity<AppUser>();

			services.ConfigureIdentityOptions();
			services.ConfigureCookie();
			services.ConfigureJWT(configuration);

			return identityBuilder;
		}

		private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");
			services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions["Issuer"];
				options.Audience = jwtAppSettingOptions["Audience"];
				options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			});

			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();
			});

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions["Issuer"],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions["Audience"],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,

				RequireExpirationTime = false,
				ValidateLifetime = false,
				ClockSkew = TimeSpan.Zero,
			};

			services.
				AddAuthentication(o =>
				{
					o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(o => o.TokenValidationParameters = tokenValidationParameters);
		}

		private static void ConfigureCookie(this IServiceCollection services)
		{
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(20);

				//options.LoginPath = "/Identity/Account/Login";
				//options.AccessDeniedPath = "/Identity/Account/AccessDenied";
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

				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});
		}

		public static void UseAppIdentity(this IApplicationBuilder app)
		{
			app.UseCookiePolicy();
			app.UseAuthentication();
		}
	}
}