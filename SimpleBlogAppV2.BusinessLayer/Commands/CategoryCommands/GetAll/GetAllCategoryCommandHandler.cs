using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.GetAll
{
	public class GetAllCategoryCommandHandler : IRequestHandler<GetAllCategoryCommand, IEnumerable<CategoryDTO>>
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public GetAllCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoryCommand request, CancellationToken ct)
		{
			return await categoryRepository.GetAllAsync(ct, c => new CategoryDTO()
			{
				Id = c.Id,
				Name = c.Name
			});
		}
	}
}
