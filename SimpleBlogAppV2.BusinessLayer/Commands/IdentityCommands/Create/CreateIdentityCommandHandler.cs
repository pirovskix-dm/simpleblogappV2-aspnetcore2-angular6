using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleBlogAppV2.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.IdentityCommands.Create
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
			IdentityResult result = await userManager.CreateAsync(appUser, request.Password);
			return appUser.Id;
		}
	}
}
