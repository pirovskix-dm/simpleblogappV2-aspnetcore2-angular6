using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleBlogAppV2.EntityFrameworkCore.Extensions
{
	public static class IQueryableExtensions
	{
		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query, in int page, in int pageSize)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (page < 1)
				throw new ArgumentException("page cannot be less than 1.");

			if (pageSize < 0)
				throw new ArgumentException("page size cannot be less than 0.");

			return _ApplyPaging(query, page, pageSize);
		}
		private static IQueryable<TEntity> _ApplyPaging<TEntity>(IQueryable<TEntity> query, in int page, in int pageSize)
		{
			query = page == 1 ? query : query.Skip((page - 1) * pageSize);
			return query.Take(pageSize);
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query, string propertyString, in bool IsSortAscending)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (string.IsNullOrWhiteSpace(propertyString))
				throw new ArgumentException("propertyString cannot be null or empty.");

			string[] propSequence = propertyString.Split('.', StringSplitOptions.RemoveEmptyEntries);
			if (!HasProperty<TEntity>(propSequence))
				throw new ArgumentException($"Type {typeof(TEntity).Name} does not contain {propertyString} property.");

			return _ApplyOrdering(query, propSequence, IsSortAscending);
		}
		private static IQueryable<TEntity> _ApplyOrdering<TEntity>(IQueryable<TEntity> query, string[] propSequence, in bool IsSortAscending)
		{
			var entity = Expression.Parameter(typeof(TEntity), "x");
			var property = Expression.Property(entity, propSequence[0]);

			for (int i = 1; i < propSequence.Length; i++)
				property = Expression.Property(property, propSequence[i]);

			var lambda = Expression.Lambda(property, entity);

			MethodInfo method = null;
			if (IsSortAscending)
				method = GetMethodInfo<IQueryable<object>>(q => q.OrderBy(o => o));
			else
				method = GetMethodInfo<IQueryable<object>>(q => q.OrderByDescending(o => o));

			var result = method
				.MakeGenericMethod(typeof(TEntity), property.Type)
				.Invoke(null, new object[] { query, lambda });

			return result == null ? query : (IOrderedQueryable<TEntity>)result;
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyStringSearching<TEntity>(this IQueryable<TEntity> query, string[] properties, string searchString)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (string.IsNullOrWhiteSpace(searchString))
				throw new ArgumentException("searchString cannot be null or empty.");

			if (properties == null || properties.Length == 0)
				throw new ArgumentException("search properties array cannot be null or empty.");

			foreach (var p in properties)
			{
				if (string.IsNullOrWhiteSpace(p))
					throw new ArgumentException("search property cannot be null or empty.");

				if (!HasProperty<TEntity>(new string[] { p }, typeof(String)))
					throw new ArgumentException($"{typeof(TEntity).Name} does not contain {p} property of type 'String'.");
			}

			return _ApplyStringSearching(query, properties, searchString);
		}
		private static IQueryable<TEntity> _ApplyStringSearching<TEntity>(IQueryable<TEntity> query, string[] properties, string searchString)
		{
			var search = Expression.Constant(searchString, typeof(string));
			var entity = Expression.Parameter(typeof(TEntity), "e");

			var containsMethod = GetMethodInfo<String>(s => s.Contains(""));

			var exp = (Expression)Expression.Call(Expression.Property(entity, properties[0]), containsMethod, search);
			for (int i = 1; i < properties.Length; i++)
			{
				var mExp = Expression.Call(Expression.Property(entity, properties[i]), containsMethod, search);
				exp = (Expression)Expression.Or(exp, mExp);
			}

			var lambda = Expression.Lambda(exp, entity);
			var result = GetMethodInfo<IQueryable<object>>(q => q.Where(s => true))
				.MakeGenericMethod(typeof(TEntity))
				.Invoke(null, new object[] { query, lambda });

			return result == null ? query : (IQueryable<TEntity>)result;
		}

		[DebuggerStepThrough]
		public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> query, Dictionary<string, int> filters)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (filters == null)
				throw new ArgumentNullException("filters"); ;

			if (filters.Count == 0)
				throw new ArgumentException("filters count can not be null.");

			foreach (var f in filters)
			{
				if (!HasProperty<TEntity>(new string[] { f.Key }))
					throw new ArgumentException($"{typeof(TEntity).Name} does not contain {f.Key} property.");
			}

			return _ApplyFiltering(query, filters);
		}
		private static IQueryable<TEntity> _ApplyFiltering<TEntity>(IQueryable<TEntity> query, Dictionary<string, int> filters)
		{
			var entity = Expression.Parameter(typeof(TEntity), "e");
			Expression exp = null;
			foreach (var f in filters)
			{
				var propType = typeof(TEntity).GetProperty(f.Key + "Id").PropertyType;
				var filterId = Expression.Constant(f.Value, propType);
				var prop = Expression.Property(entity, f.Key + "Id");
				if (exp == null)
				{
					exp = Expression.Equal(prop, filterId);
				}
				else
				{
					var mExp = Expression.Equal(prop, filterId);
					exp = (Expression)Expression.And(exp, mExp);
				}
			}

			var lambda = Expression.Lambda(exp, entity);
			var result = GetMethodInfo<IQueryable<object>>(q => q.Where(s => true))
				.MakeGenericMethod(typeof(TEntity))
				.Invoke(null, new object[] { query, lambda });

			return result == null ? query : (IQueryable<TEntity>)result;
		}


		private static MethodInfo GetMethodInfo<T>(Expression<Func<T, object>> expression)
		{
			return GetMethodInfoFromExpression(expression.Body);
		}

		private static MethodInfo GetMethodInfo<T>(Expression<Func<T, bool>> expression)
		{
			return GetMethodInfoFromExpression(expression.Body);
		}

		private static MethodInfo GetMethodInfoFromExpression(Expression expression)
		{
			if (!(expression is MethodCallExpression))
				return null;

			var mi = (expression as MethodCallExpression).Method;

			if (mi.IsGenericMethod && !mi.IsGenericMethodDefinition)
				return mi.GetGenericMethodDefinition();

			return mi;
		}

		private static bool HasProperty<T>(string[] propSequence, Type propType = null) where T : class
		{
			if (propSequence == null || propSequence.Length == 0)
				return false;

			Type type = typeof(T);
			foreach (string p in propSequence)
			{
				type = type.GetProperty(p)?.PropertyType;
				if (type == null)
					return false;
			}

			if (propType != null && type != propType)
				return false;

			return true;
		}
	}
}
