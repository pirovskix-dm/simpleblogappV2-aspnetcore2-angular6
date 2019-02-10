using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SimpleBlogAppV2.EntityFrameworkCore.Extensions
{
	public static class IQueryableExtensions
	{
		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query, in int page, in int pageSize)
			where TEntity : class
		{
			query = page == 1 ? query : query.Skip((page - 1) * pageSize);
			return query.Take(pageSize);
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query, string propertyString, in bool IsSortAscending)
			where TEntity : class
		{
			return IsSortAscending ? query.OrderBy(propertyString) : query.OrderBy($"{propertyString} DESC");
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyStringSearching<TEntity>(this IQueryable<TEntity> query, string[] properties, string searchString)
			where TEntity : class
		{
			string sql = properties.Select(p => $"{p}.Contains(@0)").Join(" OR ");
			return query.Where(sql, searchString);
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> query, Dictionary<string, int> filters)
			where TEntity : class
		{
			foreach (var f in filters)
			{
				query = query.Where($"{f.Key}Id = @0", f.Value);
			}
			return query;
		}
	}
}