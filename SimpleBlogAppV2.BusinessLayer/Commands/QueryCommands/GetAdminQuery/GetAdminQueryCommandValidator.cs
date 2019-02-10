using FluentValidation;
using SimpleBlogAppV2.Core.Entities;
using System;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetAdminQuery
{
	public class GetAdminQueryCommandValidator : AbstractValidator<GetAdminQueryCommand>
	{
		public GetAdminQueryCommandValidator()
		{
			RuleFor(p => p.SortBy).Must(SortPropertyBeInPostIfProvided).WithMessage(p => $"Post does not contain {p.SortBy} property.");
			RuleFor(p => p.Page).GreaterThan(0);
			RuleFor(p => p.PageSize).GreaterThan(0);
			RuleFor(p => p.Filters).Must(FilterPropertiesBeInPostIfProvided).WithMessage(p => $"Invalid filter {p.Filters}");
			RuleFor(p => p.SearchBy).Must(SearchPropertiesBeInPostIfSearchProvided).WithMessage(p => $"Invalid search properties {p.SearchBy}");

		}

		private bool SortPropertyBeInPostIfProvided(string SortBy)
		{
			if (string.IsNullOrWhiteSpace(SortBy))
				return true;

			string[] propSequence = SortBy.Split('.', StringSplitOptions.RemoveEmptyEntries);
			return HasProperty<Post>(propSequence);
		}

		private bool FilterPropertiesBeInPostIfProvided(string filters)
		{
			if (string.IsNullOrWhiteSpace(filters))
				return true;

			var dFilters = Utils.QueryParser.ParseFilter(filters);
			foreach (var f in dFilters)
			{
				if (!HasProperty<Post>(new string[] { f.Key }))
				{
					return false;
				}
			}
			return true;
		}

		private bool SearchPropertiesBeInPostIfSearchProvided(GetAdminQueryCommand command, string searchBy)
		{
			if (string.IsNullOrWhiteSpace(command.Search))
				return true;

			foreach (var p in Utils.QueryParser.ParseSearch(searchBy))
			{
				if (string.IsNullOrWhiteSpace(p))
				{
					return false;
				}

				if (!HasProperty<Post>(new string[] { p }, typeof(String)))
				{
					return false;
				}
			}

			return true;
		}

		private bool HasProperty<T>(string[] propSequence, Type propType = null) where T : class
		{
			if (propSequence == null || propSequence.Length == 0)
			{
				return false;
			}

			Type type = typeof(T);
			foreach (string p in propSequence)
			{
				type = type.GetProperty(p)?.PropertyType;
				if (type == null)
				{
					return false;
				}
			}

			if (propType != null && type != propType)
			{
				return false;
			}

			return true;
		}
	}
}