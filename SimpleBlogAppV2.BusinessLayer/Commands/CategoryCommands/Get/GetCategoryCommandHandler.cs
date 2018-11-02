using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Get
{
	public class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, CategoryDTO>
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public GetCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<CategoryDTO> Handle(GetCategoryCommand request, CancellationToken ct)
		{
			var result = await categoryRepository.GetAsync(request.Id, ct, (Category c) => new CategoryDTO()
			{
				Id = c.Id,
				Name = c.Name
			});

			if (result == null)
				throw new BlogNotFoundException("Category", request.Id);

			return result;
		}
	}
}
