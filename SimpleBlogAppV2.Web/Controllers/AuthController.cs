using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.Identity.Commands.IdentityCommands.Login;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/auth")]
	public class AuthController : Controller
	{
		private readonly IMediator mediator;

		public AuthController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Post([FromBody] LoginCommand command)
		{
			return Ok(await mediator.Send(command));
		}
	}
}