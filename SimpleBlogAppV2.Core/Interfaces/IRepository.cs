using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Core.Interfaces
{
	public interface IRepository<TEntity> where TEntity : IEntity
	{
		Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken ct, Expression<Func<TEntity, T>> exp);

		Task<T> GetAsync<T>(int id, CancellationToken ct, Expression<Func<TEntity, T>> exp);

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Remove(TEntity entity);
	}
}