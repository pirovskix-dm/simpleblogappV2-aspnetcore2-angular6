using Microsoft.Extensions.DependencyInjection;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using SimpleBlogAppV2.BusinessLayer.Services;

namespace SimpleBlogAppV2.BusinessLayer
{
	public static class ServicesConfiguration
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<IPostService, PostService>();
			services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
		}
	}
}
