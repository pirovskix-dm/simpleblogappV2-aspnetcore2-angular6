using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.EntityFrameworkCore.Repositories
{
	public class EfUnitOfWork : BaseRepository, IUnitOfWork
	{
		public EfUnitOfWork(SimpleBlogAppV2DbContext context)
			: base(context)
		{

		}

		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}
	}
}
