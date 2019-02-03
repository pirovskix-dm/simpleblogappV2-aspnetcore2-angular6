using FluentValidation;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Create
{
	public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
	{
		public CreatePostCommandValidator()
		{
			RuleFor(p => p.Title).MaximumLength(100).NotEmpty();
			RuleFor(p => p.Content).NotEmpty();
			RuleFor(p => p.ShortContent).MaximumLength(500);
		}
	}
}