using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Update
{
	public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
	{
		public UpdatePostCommandValidator()
		{
			RuleFor(p => p.Title).MaximumLength(100).NotEmpty();
			RuleFor(p => p.Content).NotEmpty();
			RuleFor(p => p.ShortContent).MaximumLength(500);
		}
	}
}
