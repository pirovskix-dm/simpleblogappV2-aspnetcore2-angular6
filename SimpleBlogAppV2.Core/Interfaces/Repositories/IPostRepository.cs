using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Query;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Core.Interfaces.Repositories
{
	public interface IPostRepository : IRepository<Post>
	{
		Task<QueryResult<T>> GetQueryResultAsync<T>(QueryObject queryObj, Expression<Func<Post, T>> exp);
	}
}
