using System;

namespace SimpleBlogAppV2.Validation.Exceptions
{
	public class LoginFailureException : Exception
	{
		public LoginFailureException()
			: base("Invalid username or password.")
		{
		}
	}
}