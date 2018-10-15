using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.Commands.PostCommands;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/posts")]
	public class PostsController : Controller
	{
		private readonly IUnitOfWorkService unitOfWork;
		private readonly IPostService postService;
		private readonly IMediator mediator;

		public PostsController(IUnitOfWorkService unitOfWork, IPostService postService, IMediator mediator)
		{
			this.unitOfWork = unitOfWork;
			this.postService = postService;
			this.mediator = mediator;
		}

		[HttpGet]
		public async Task<IEnumerable<PostDTO>> GetPosts()
		{
			return await postService.GetAllPosts();
		}

		[HttpGet("blog")]
		[ProducesResponseType(typeof(QueryResultDTO<PostDTO>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetBlogPosts(QueryObjectDTO query)
		{
			if (query is null)
				return BadRequest();

			var result = await postService.GetBlogQueryResult(query);
			return Ok(result);
		}

		[HttpGet("admin")]
		[ProducesResponseType(typeof(QueryResultDTO<PostDTO>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetAdminPosts(QueryObjectDTO query)
		{
			if (query is null)
				return BadRequest();

			var result = await postService.GetAdminQueryResult(query);
			return Ok(result);
		}

		[HttpGet("default")]
		public IActionResult GetDefaultPost()
		{
			return Ok(new PostDTO());
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(PostDTO), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetPost([FromRoute] int id)
		{
			var result = await postService.GetPost(id);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand createCommand)
		{
			return Ok(await mediator.Send(createCommand));
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] PostDTO savePost)
		{
			return await CreateOrUpdatePost(id, savePost);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeletePost([FromRoute] int id)
		{
			postService.RemovePost(id);
			await unitOfWork.SaveChangesAsync();
			return Ok(id);
		}

		private async Task<IActionResult> CreateOrUpdatePost(int? id, PostDTO savePost)
		{
			if (savePost is null || !ModelState.IsValid)
				return BadRequest(ModelState);
			
			await postService.AddOrUpdatePost(id, savePost);
			await unitOfWork.SaveChangesAsync();

			return Ok(postService.GetProcessedPostId());
		}
	}
}
