using FluentValidation;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Delete
{
	public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
	{
		public DeletePostCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty().GreaterThan(0);
		}
	}
}