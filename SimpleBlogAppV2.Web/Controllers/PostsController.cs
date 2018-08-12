using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/posts")]
	public class PostsController : Controller
	{
		private readonly IUnitOfWorkService unitOfWork;
		private readonly IPostService postService;

		public PostsController(IUnitOfWorkService unitOfWork, IPostService postService)
		{
			this.unitOfWork = unitOfWork;
			this.postService = postService;
		}

		[HttpGet]
		public async Task<IEnumerable<PostDTO>> GetPosts()
		{
			return await postService.GetAllPosts();
		}

		[HttpGet("blog")]
		public async Task<IEnumerable<PostDTO>> GetBlogPosts()
		{
			return await postService.GetBlogPosts();
		}

		[HttpGet("admin")]
		public async Task<IEnumerable<PostDTO>> GetAdminPosts()
		{
			return await postService.GetAdminPosts();
		}

		[HttpGet("default")]
		public IActionResult GetDefaultPost()
		{
			return Ok(new PostDTO());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPost([FromRoute] int id)
		{
			var post = await postService.GetPost(id);
			if (post == null)
				return NotFound();
			return Ok(post);
		}

		[HttpPost]
		public async Task<IActionResult> CreatePost([FromBody] PostDTO savePost)
		{
			return await CreateOrUpdatePost(null, savePost);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] PostDTO savePost)
		{
			return await CreateOrUpdatePost(id, savePost);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePost([FromRoute] int id)
		{
			postService.RemovePost(id);

			if (!await unitOfWork.TrySaveChangesAsync())
				return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok(id);
		}

		private async Task<IActionResult> CreateOrUpdatePost(int? id, PostDTO savePost)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			bool success = await postService.AddOrUpdatePost(id, savePost);
			if (!success)
				return NotFound();

			if (!await unitOfWork.TrySaveChangesAsync())
				return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok(postService.GetProcessedPostId());
		}
	}
}
