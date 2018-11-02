using FluentValidation;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Get
{
	public class GetCategoryCommandValidator : AbstractValidator<GetCategoryCommand>
	{
		public GetCategoryCommandValidator()
		{
			RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
		}
	}
}
