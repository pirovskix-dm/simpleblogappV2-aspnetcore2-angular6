using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Core.Interfaces.Repositories
{
	public interface IUnitOfWork
	{
		Task SaveAsync(CancellationToken cancellationToken = default);
	}
}