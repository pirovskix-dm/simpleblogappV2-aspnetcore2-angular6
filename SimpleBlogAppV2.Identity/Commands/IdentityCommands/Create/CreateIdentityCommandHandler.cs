using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Validation.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Create
{
	public class CreateIdentityCommandHandler : IRequestHandler<CreateIdentityCommand, string>
	{
		private readonly UserManager<AppUser> userManager;

		public CreateIdentityCommandHandler(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
		}

		public async Task<string> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
		{
			var appUser = new AppUser()
			{
				UserName = request.Email,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName
			};
			IdentityResult result = await userManager.CreateAsync(appUser, request.Password).ConfigureAwait(false);
			if (!result.Succeeded)
			{
				throw new IdentityRegistrationException(result);
			}

			return appUser.Id;
		}
	}
}