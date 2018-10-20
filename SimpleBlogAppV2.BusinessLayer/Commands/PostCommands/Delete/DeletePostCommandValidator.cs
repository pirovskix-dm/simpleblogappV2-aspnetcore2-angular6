using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

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
