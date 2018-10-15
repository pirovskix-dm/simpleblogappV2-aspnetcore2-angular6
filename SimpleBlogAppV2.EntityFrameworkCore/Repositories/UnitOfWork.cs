using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.EntityFrameworkCore.Repositories
{
	public class EfUnitOfWork : BaseRepository, IUnitOfWork
	{
		public EfUnitOfWork(SimpleBlogAppV2DbContext context)
			: base(context)
		{

		}

		public async Task SaveAsync(CancellationToken cancellationToken = default)
		{
			await context.SaveChangesAsync(cancellationToken);
		}
	}
}
