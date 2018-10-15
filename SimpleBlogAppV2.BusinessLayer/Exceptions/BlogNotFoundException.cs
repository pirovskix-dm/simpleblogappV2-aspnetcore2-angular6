using System;

namespace SimpleBlogAppV2.BusinessLayer.Exceptions
{
	public class BlogNotFoundException : Exception
	{
		public BlogNotFoundException(string name, object key)
			: base($"Entity \"{name}\" ({key}) was not found.")
		{

		}
	}
}
