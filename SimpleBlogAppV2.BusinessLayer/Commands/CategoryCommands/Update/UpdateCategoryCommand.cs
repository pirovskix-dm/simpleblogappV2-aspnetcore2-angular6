using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Update
{
	public class UpdateCategoryCommand : CategoryDTO, IRequest<int>
	{

	}
}
