using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Get
{
	public class GetPostCommand : IRequest<PostDTO>
	{
		public int Id { get; set; }
	}
}
