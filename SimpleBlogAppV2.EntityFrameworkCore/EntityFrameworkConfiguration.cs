﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.EntityFrameworkCore.Repositories;

namespace SimpleBlogAppV2.EntityFrameworkCore
{
	public static class EntityFrameworkConfiguration
	{
		public static void UseEntityFramework(this IServiceCollection services, string connectionString, IdentityBuilder identityBuilder = null)
		{
			services.AddScoped<IPostRepository, EfPostRepository>();
			services.AddScoped<ICategoryRepository, EfCategoryRepository>();
			services.AddScoped<IUnitOfWork, EfUnitOfWork>();

			services.AddDbContext<SimpleBlogAppV2DbContext>(options =>
			{
				options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(SimpleBlogAppV2DbContext).Namespace));
			});

			identityBuilder.AddEntityFrameworkStores<SimpleBlogAppV2DbContext>();
		}
	}
}