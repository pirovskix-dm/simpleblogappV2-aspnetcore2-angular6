using MediatR;
using SimpleBlogAppV2.Identity.DTO;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Create
{
	public class CreateIdentityCommand : RegistrationDTO, IRequest<string>
	{
		public CreateIdentityCommand()
		{
		}
	}
}