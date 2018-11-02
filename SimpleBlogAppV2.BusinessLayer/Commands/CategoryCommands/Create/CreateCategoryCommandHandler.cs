using MediatR;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Create
{
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = new Category()
			{
				Name = request.Name
			};

			categoryRepository.Add(category);
			await unitOfWork.SaveAsync(cancellationToken);

			return category.Id;
		}
	}
}
