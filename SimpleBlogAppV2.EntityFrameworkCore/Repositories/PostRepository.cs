using Microsoft.EntityFrameworkCore;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.EntityFrameworkCore.Repositories
{
	public class EfPostRepository : BaseRepository, IPostRepository
	{
		public EfPostRepository(SimpleBlogAppV2DbContext context) 
			: base(context)
		{

		}

		public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<Post, T>> exp)
		{
			return await context.Posts
				.Select(exp)
				.ToListAsync();
		}

		public async Task<T> GetAsync<T>(int id, Expression<Func<Post, T>> exp)
		{
			return await context.Posts
				.Where(p => p.Id == id)
				.Select(exp)
				.FirstOrDefaultAsync();
		}

		public void Add(Post entity)
		{
			context.Posts.Add(entity);
		}

		public void Remove(Post entity)
		{
			context.Posts.Remove(entity);
		}

		public void Update(Post entity)
		{
			var entry = context.Posts.Update(entity);
			entry.Property(e => e.DateCreated).IsModified = false;
		}
	}
}
