using FluentValidation;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Login
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Username cannot be empty");
			RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
			RuleFor(vm => vm.Password).Length(3, 12).WithMessage("Password must be between 3 and 12 characters");
		}
	}
}