using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostSharp.Patterns.Contracts;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Identity.Factory;
using SimpleBlogAppV2.Identity.Models;
using SimpleBlogAppV2.Validation.Exceptions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Login
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IJwtFactory jwtFactory;
		private readonly JwtIssuerOptions jwtOptions;

		public LoginCommandHandler(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
		{
			this.userManager = userManager;
			this.jwtFactory = jwtFactory;
			this.jwtOptions = jwtOptions.Value;
		}

		public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var identity = await GetClaimsIdentity(request.UserName, request.Password).ConfigureAwait(false);
			if (identity == null)
			{
				throw new LoginFailureException();
			}

			//var response = new
			//{
			//	id = identity.Claims.Single(c => c.Type == "id").Value,
			//	auth_token = await jwtFactory.GenerateEncodedToken(request.UserName, identity),
			//	expires_in = (int)jwtOptions.ValidFor.TotalSeconds
			//};

			//return JsonConvert.SerializeObject(response, Formatting.Indented);\
			return await jwtFactory.GenerateEncodedToken(request.UserName, identity).ConfigureAwait(false);
		}

		private async Task<ClaimsIdentity> GetClaimsIdentity([Required] string userName, [Required] string password)
		{
			var userToVerify = await userManager.FindByNameAsync(userName).ConfigureAwait(false);
			if (userToVerify != null && await userManager.CheckPasswordAsync(userToVerify, password).ConfigureAwait(false))
			{
				return jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id);
			}

			return null;
		}
	}
}