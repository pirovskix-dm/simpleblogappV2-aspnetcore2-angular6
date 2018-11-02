using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

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
