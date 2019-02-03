using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Create;
using SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Delete;
using SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Get;
using SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.GetAll;
using SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Update;
using SimpleBlogAppV2.BusinessLayer.DTO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/categories")]
	public class CategoriesController : Controller
	{
		private readonly IMediator mediator;

		public CategoriesController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("all")]
		[ProducesResponseType(typeof(IEnumerable<CategoryDTO>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetCategories(GetAllCategoryCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetCategory([FromRoute] int id)
		{
			var command = new GetCategoryCommand() { Id = id };
			return Ok(await mediator.Send(command));
		}

		[HttpPost]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
		{
			return Ok(await mediator.Send(command));
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryCommand command)
		{
			command.Id = id;
			return Ok(await mediator.Send(command));
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeleteCategory([FromRoute] int id)
		{
			var command = new DeleteCategoryCommand() { Id = id };
			return Ok(await mediator.Send(command));
		}
	}
}