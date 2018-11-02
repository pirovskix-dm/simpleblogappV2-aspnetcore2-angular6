using Microsoft.EntityFrameworkCore;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Core.Query;
using SimpleBlogAppV2.EntityFrameworkCore.Extensions;
using SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.EntityFrameworkCore.Repositories
{
	public class EfPostRepository : BaseRepository, IPostRepository
	{
		public EfPostRepository(SimpleBlogAppV2DbContext context) 
			: base(context)
		{

		}

		public async Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken ct, Expression<Func<Post, T>> exp)
		{
			return await context.Posts
				.Include(p => p.Category)
				.Select(exp)
				.ToListAsync(ct);
		}

		public async Task<T> GetAsync<T>(int id, CancellationToken ct, Expression<Func<Post, T>> exp)
		{
			return await context.Posts
				.Include(p => p.Category)
				.Where(p => p.Id == id)
				.Select(exp)
				.FirstOrDefaultAsync(ct);
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

		public async Task<QueryResult<T>> GetQueryResultAsync<T>(QueryObject queryObj, CancellationToken ct, Expression<Func<Post, T>> exp)
		{
			var query = context.Posts
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryObj.Search))
				query = query.ApplyStringSearching(queryObj.SearchBy, queryObj.Search);
				
			var countTask = query.CountAsync(ct);

			if (!string.IsNullOrWhiteSpace(queryObj.SortBy))
				query = query.ApplyOrdering(queryObj.SortBy, queryObj.IsSortAscending);

			var itemsTask = query
				.ApplyPaging(queryObj.Page, queryObj.PageSize)
				.Select(exp)
				.ToListAsync(ct);

			await Task.WhenAll(countTask, itemsTask);

			var result = new QueryResult<T>();
			result.TotalItems = countTask.Result;
			result.Items = itemsTask.Result;

			return result;
		}
	}
}
