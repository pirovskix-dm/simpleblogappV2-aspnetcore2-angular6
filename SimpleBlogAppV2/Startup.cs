using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlogAppV2.BusinessLayer;
using SimpleBlogAppV2.EntityFrameworkCore;
using System;

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
			services.UseEntityFramework(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SimpleBlogAppV2DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

			services.AddServices();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = $"{ClientAppPath}/dist";
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
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
