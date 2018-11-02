using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Create
{
	public class CreateCategoryCommand : CategoryDTO, IRequest<int>
	{

	}
}
