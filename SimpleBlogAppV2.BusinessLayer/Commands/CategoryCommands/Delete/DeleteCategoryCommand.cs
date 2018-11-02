using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Delete
{
	public class DeleteCategoryCommand : IRequest<int>
	{
		public int Id { get; set; }
	}
}
