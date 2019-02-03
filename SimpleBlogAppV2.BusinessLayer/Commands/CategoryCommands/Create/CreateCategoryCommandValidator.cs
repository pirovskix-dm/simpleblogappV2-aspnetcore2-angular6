using FluentValidation;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Create
{
	public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(c => c.Name).NotEmpty().MaximumLength(500);
		}
	}
}