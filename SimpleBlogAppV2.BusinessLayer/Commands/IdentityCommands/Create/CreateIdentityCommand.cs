using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.IdentityCommands.Create
{
	public class CreateIdentityCommand : RegistrationDTO, IRequest<string>
	{
		public CreateIdentityCommand()
		{

		}
	}
}
