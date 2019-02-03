using FluentValidation;

namespace SimpleBlogAppV2.Identity.Commands.IdentityCommands.Create
{
	public class CreateIdentityCommandValidator : AbstractValidator<CreateIdentityCommand>
	{
		public CreateIdentityCommandValidator()
		{
			RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email cannot be empty");
			RuleFor(vm => vm.Email).EmailAddress().WithMessage("Enter the valid email address");
			RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
			RuleFor(vm => vm.Password).MinimumLength(3).WithMessage("Password should be at leas 3 chars long");
			//RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
			//RuleFor(vm => vm.LastName).NotEmpty().WithMessage("LastName cannot be empty");
			//RuleFor(vm => vm.Location).NotEmpty().WithMessage("Location cannot be empty");
		}
	}
}
