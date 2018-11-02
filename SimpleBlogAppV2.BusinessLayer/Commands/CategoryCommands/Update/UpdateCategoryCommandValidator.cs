﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlogAppV2.BusinessLayer.Commands.CategoryCommands.Update
{
	public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(c => c.Name).MaximumLength(500).NotEmpty();
		}
	}
}
