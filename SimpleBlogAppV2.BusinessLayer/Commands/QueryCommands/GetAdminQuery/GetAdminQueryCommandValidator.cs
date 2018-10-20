using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetAdminQuery
{
	public class GetAdminQueryCommandValidator : AbstractValidator<GetAdminQueryCommand>
	{
		public GetAdminQueryCommandValidator()
		{
			RuleFor(p => p.Page).NotEmpty().GreaterThan(0);
			RuleFor(p => p.PageSize).NotEmpty().GreaterThan(0);
		}
	}
}
