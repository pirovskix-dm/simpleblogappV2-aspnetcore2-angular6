using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Create
{
	public class CreatePostCommand : PostDTO, IRequest<int>
	{

	}
}
