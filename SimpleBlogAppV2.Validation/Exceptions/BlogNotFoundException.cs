using System;

namespace SimpleBlogAppV2.Validation.Exceptions
{
	public class BlogNotFoundException : Exception
	{
		public BlogNotFoundException(string name, object key)
			: base($"Entity \"{name}\" ({key}) was not found.")
		{

		}

		public BlogNotFoundException(string name)
			: base($"Entity \"{name}\" was not found.")
		{

		}
	}
}
