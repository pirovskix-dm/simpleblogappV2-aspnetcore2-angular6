using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
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
		public async Task<IActionResult> GetBlogPosts(QueryObjectDTO query)
		{
			try
			{
				var result = await postService.GetBlogQueryResult(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpGet("admin")]
		public async Task<IActionResult> GetAdminPosts(QueryObjectDTO query)
		{
			try
			{
				var result = await postService.GetAdminQueryResult(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpGet("default")]
		public IActionResult GetDefaultPost()
		{
			return Ok(new PostDTO());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPost([FromRoute] int id)
		{
			try
			{
				var result = await postService.GetPost(id);
				if (result == null)
					return NotFound();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
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
			try
			{
				postService.RemovePost(id);
				await unitOfWork.SaveChangesAsync();
				return Ok(id);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		private async Task<IActionResult> CreateOrUpdatePost(int? id, PostDTO savePost)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				bool success = await postService.AddOrUpdatePost(id, savePost);
				if (!success)
					return NotFound();

				await unitOfWork.SaveChangesAsync();
				return Ok(postService.GetProcessedPostId());
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}
	}
}
