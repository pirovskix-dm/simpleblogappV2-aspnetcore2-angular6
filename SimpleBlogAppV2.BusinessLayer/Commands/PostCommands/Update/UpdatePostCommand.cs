using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Update
{
	public class UpdatePostCommand : PostDTO, IRequest<int>
	{
	}
}