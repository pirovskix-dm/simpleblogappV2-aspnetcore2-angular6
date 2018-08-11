using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using SimpleBlogAppV2.Web.ViewModels;
using System;
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPost([FromRoute] int id)
		{
			string hello = "32";
			String.IsNullOrWhiteSpace(hello);

			var postDTO = await postService.GetPost(id);
			if (postDTO == null)
				return NotFound();
			return Ok(postDTO);
		}

		[HttpPost]
		public async Task<IActionResult> CreatePost([FromBody] PostViewModel savePost)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			postService.AddPost(new PostDTO()
			{
				Title = savePost.Title,
				Content = savePost.Content,
				ShortContent = savePost.ShortContent
			});

			if (!await unitOfWork.TrySaveChangesAsync())
				return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok();
		}
	}
}
