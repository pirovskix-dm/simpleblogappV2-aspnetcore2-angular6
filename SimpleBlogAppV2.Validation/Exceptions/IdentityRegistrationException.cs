using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SimpleBlogAppV2.Validation.Exceptions
{
	public class IdentityRegistrationException : Exception
	{
		public IDictionary<string, string> Failures { get; }

		public IdentityRegistrationException()
			: base("User registration failure.")
		{
			Failures = new Dictionary<string, string>();
		}

		public IdentityRegistrationException(IdentityResult identityResult)
			: this()
		{
			foreach (IdentityError e in identityResult.Errors)
			{
				Failures.Add(e.Code, e.Description);
			}
		}
	}
}