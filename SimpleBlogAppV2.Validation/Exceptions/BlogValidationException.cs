using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlogAppV2.Validation.Exceptions
{
	public class BlogValidationException : Exception
	{
		public IDictionary<string, string[]> Failures { get; }

		public BlogValidationException()
			: base("One or more validation failures have occurred.")
		{
			Failures = new Dictionary<string, string[]>();
		}

		public BlogValidationException(List<ValidationFailure> failures)
			: this()
		{
			var propertyNames = failures
				.Select(e => e.PropertyName)
				.Distinct();

			foreach (var propertyName in propertyNames)
			{
				var propertyFailures = failures
					.Where(e => e.PropertyName == propertyName)
					.Select(e => e.ErrorMessage)
					.ToArray();

				Failures.Add(propertyName, propertyFailures);
			}
		}
	}
}
