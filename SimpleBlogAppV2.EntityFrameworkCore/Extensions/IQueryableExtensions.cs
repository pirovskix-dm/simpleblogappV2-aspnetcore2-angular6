using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleBlogAppV2.EntityFrameworkCore.Extensions
{
	public static class IQueryableExtensions
	{
		public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
			where TEntity : class
		{
			if (page < 1)
				throw new ArgumentException("page cannot be less than 1.");

			if (pageSize < 0)
				throw new ArgumentException("page size cannot be less than 0.");


			query = page == 1 ? query : query.Skip((page - 1) * pageSize);
			return query.Take(pageSize);
		}

		public static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query, string propertyString, bool IsSortAscending)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (string.IsNullOrWhiteSpace(propertyString))
				throw new ArgumentException("propertyString cannot be null or empty.");

			string[] propSequence = propertyString.Split('.', StringSplitOptions.RemoveEmptyEntries);
			if (!HasProperty<TEntity>(propSequence))
				throw new ArgumentException($"Type {typeof(TEntity).Name} does not contain {propertyString} property.");

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

		public static IQueryable<TEntity> ApplyStringSearching<TEntity>(this IQueryable<TEntity> query, string[] properties, string searchString)
			where TEntity : class
		{
			if (query == null)
				throw new ArgumentNullException("query");

			if (string.IsNullOrWhiteSpace(searchString))
				throw new ArgumentException("searchString cannot be null or empty.");

			if (properties == null || properties.Length == 0)
				throw new ArgumentException("search properties array cannot be null or empty.");

			CheckSearchProperties<TEntity>(properties);

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

		private static void CheckSearchProperties<T>(string[] properties)
			where T : class
		{
			foreach(var p in properties)
			{
				if (string.IsNullOrWhiteSpace(p))
					throw new ArgumentException("search property cannot be null or empty.");

				if (!HasProperty<T>(new string[] { p }, typeof(String)))
					throw new ArgumentException($"{typeof(T).Name} does not contain {p} property of type 'String'.");
			}
		}
	}
}
