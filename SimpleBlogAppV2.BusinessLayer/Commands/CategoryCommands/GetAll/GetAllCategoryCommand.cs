using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using System.Collections.Generic;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.GetAll
{
	public class GetAllCategoryCommand : IRequest<IEnumerable<CategoryDTO>>
	{
	}
}