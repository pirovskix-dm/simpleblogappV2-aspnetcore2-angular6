using MediatR;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Delete
{
	public class DeletePostCommand : IRequest<int>
	{
		public int Id { get; set; }
	}
}