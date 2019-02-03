using Microsoft.EntityFrameworkCore;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.EntityFrameworkCore.Repositories
{
	public class EfCategoryRepository : BaseRepository, ICategoryRepository
	{
		public EfCategoryRepository(SimpleBlogAppV2DbContext context)
			: base(context)
		{
		}

		public async Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken ct, Expression<Func<Category, T>> exp)
		{
			return await context.Categories
				.Select(exp)
				.ToListAsync(ct)
				.ConfigureAwait(false);
		}

		public async Task<T> GetAsync<T>(int id, CancellationToken ct, Expression<Func<Category, T>> exp)
		{
			return await context.Categories
				.Where(p => p.Id == id)
				.Select(exp)
				.FirstOrDefaultAsync(ct)
				.ConfigureAwait(false);
		}

		public void Add(Category entity)
		{
			context.Categories.Add(entity);
		}

		public void Remove(Category entity)
		{
			context.Categories.Remove(entity);
		}

		public void Update(Category entity)
		{
			var entry = context.Categories.Update(entity);
			entry.Property(e => e.DateCreated).IsModified = false;
		}
	}
}