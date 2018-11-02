using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Get
{
	public class GetCategoryCommand : IRequest<CategoryDTO>
	{
		public int Id { get; set; }
	}
}
