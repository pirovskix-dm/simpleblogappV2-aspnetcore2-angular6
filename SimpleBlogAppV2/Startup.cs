using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBlogAppV2.BusinessLayer;
using SimpleBlogAppV2.BusinessLayer.Commands;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Create;
using SimpleBlogAppV2.EntityFrameworkCore;
using SimpleBlogAppV2.Filters;
using SimpleBlogAppV2.Logger;
using System;
using System.IO;

namespace SimpleBlogAppV2.App
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		private const string ClientAppPath = "AngularHost";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.UseEntityFramework(@"Data Source=DEV-PC4;Initial Catalog=SimpleBlogAppV2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
			services.AddMediatR();

			services
				.AddMvc(o => o.Filters.Add(typeof(CustomExceptionFilterAttribute)))
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>());

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = $"{ClientAppPath}/dist";
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				loggerFactory.SetPath(Directory.GetCurrentDirectory());
				loggerFactory.CreateLogger<FileLogger>();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = ClientAppPath;

				if (env.IsDevelopment())
				{
					spa.Options.StartupTimeout = new TimeSpan(days: 0, hours: 0, minutes: 1, seconds: 30);
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
