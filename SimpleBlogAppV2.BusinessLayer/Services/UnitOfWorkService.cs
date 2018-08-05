using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Services
{
	public class UnitOfWorkService : IUnitOfWorkService
	{
		private readonly IUnitOfWork unitOfWork;

		public UnitOfWorkService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<bool> TrySaveChangesAsync()
		{
			try
			{
				await unitOfWork.SaveAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
