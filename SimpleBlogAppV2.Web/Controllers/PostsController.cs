using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Create;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Delete;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Get;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Update;
using SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetAdminQuery;
using SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetBlogQuery;
using SimpleBlogAppV2.BusinessLayer.DTO;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/posts")]
	public class PostsController : Controller
	{
		private readonly IMediator mediator;

		public PostsController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("blog")]
		[ProducesResponseType(typeof(QueryResultDTO<PostDTO>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBlogPosts(GetBlogQueryCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpGet("admin")]
		[ProducesResponseType(typeof(QueryResultDTO<PostDTO>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetAdminPosts(GetAdminQueryCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(PostDTO), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetPost([FromRoute] int id)
		{
			var command = new GetPostCommand() { Id = id };
			return Ok(await mediator.Send(command));
		}

		[HttpPost]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostCommand command)
		{
			command.Id = id; // TODO
			return Ok(await mediator.Send(command));
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeletePost([FromRoute] int id)
		{
			var command = new DeletePostCommand() { Id = id };
			return Ok(await mediator.Send(command));
		}
	}
}
