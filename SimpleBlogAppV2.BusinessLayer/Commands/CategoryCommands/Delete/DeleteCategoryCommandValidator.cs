using FluentValidation;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Delete
{
	public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
	{
		public DeleteCategoryCommandValidator()
		{
			RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
		}
	}
}