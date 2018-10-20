﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetBlogQuery
{
	public class GetBlogQueryCommandValidator : AbstractValidator<GetBlogQueryCommand>
	{
		public GetBlogQueryCommandValidator()
		{
			RuleFor(p => p.Page).NotEmpty().GreaterThan(0);
			RuleFor(p => p.PageSize).NotEmpty().GreaterThan(0);
		}
	}
}
