using MediatR;
using SimpleBlogAppV2.Identity.DTO;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Login
{
	public class LoginCommand : CredentialsDTO, IRequest<string>
	{
	}
}