using MediatR;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Delete
{
	public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = new Category() { Id = request.Id };
			categoryRepository.Remove(category);
			await unitOfWork.SaveAsync(cancellationToken);
			return request.Id;
		}
	}
}
