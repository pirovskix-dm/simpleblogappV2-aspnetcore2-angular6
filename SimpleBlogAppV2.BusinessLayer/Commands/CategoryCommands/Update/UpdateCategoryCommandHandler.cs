using MediatR;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Validation.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Update
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await categoryRepository.GetAsync(request.Id, cancellationToken, c => c);
			if (category == null)
				throw new BlogNotFoundException("Category", request.Id);

			category.Name = request.Name;
			category.DateLastUpdated = DateTime.UtcNow;

			categoryRepository.Update(category);
			await unitOfWork.SaveAsync(cancellationToken);

			return category.Id;
		}
	}
}
