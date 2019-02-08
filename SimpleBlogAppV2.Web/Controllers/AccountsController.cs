using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.Identity.Commands.IdentityCommands.Create;
using SimpleBlogAppV2.Identity.Commands.IdentityCommands.Login;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/accounts")]
	public class AccountsController : Controller
	{
		private readonly IMediator mediator;

		public AccountsController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost("register")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Register([FromBody] CreateIdentityCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Post([FromBody] LoginCommand command)
		{
			return Ok(await mediator.Send(command));
		}
	}
}