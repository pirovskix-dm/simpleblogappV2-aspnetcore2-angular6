using System.Threading.Tasks;

namespace SimpleBlogAppV2.Core.Interfaces.Repositories
{
	public interface IUnitOfWork
	{
		Task SaveAsync();
	}
}
