using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Get
{
	public class GetPostCommandValidator : AbstractValidator<GetPostCommand>
	{
		public GetPostCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty().GreaterThan(0);
		}
	}
}
