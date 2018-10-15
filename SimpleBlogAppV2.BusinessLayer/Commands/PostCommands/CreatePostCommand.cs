using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands
{
	public class CreatePostCommand : PostDTO, IRequest<int>
	{

	}
}
