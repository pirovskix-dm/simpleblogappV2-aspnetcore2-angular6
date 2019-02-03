using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.Identity.Commands.IdentityCommands.Create;
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

		[HttpPost]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> CreateUser([FromBody] CreateIdentityCommand command)
		{
			return Ok(await mediator.Send(command));
		}
	}
}