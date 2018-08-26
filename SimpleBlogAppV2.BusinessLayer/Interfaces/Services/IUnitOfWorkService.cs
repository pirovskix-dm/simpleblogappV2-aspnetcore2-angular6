using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Interfaces.Services
{
	public interface IUnitOfWorkService
	{
		Task SaveChangesAsync();
	}
}
