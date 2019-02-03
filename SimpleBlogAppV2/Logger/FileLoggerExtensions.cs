using Microsoft.Extensions.Logging;

namespace SimpleBlogAppV2.Logger
{
	public static class FileLoggerExtensions
	{
		public static ILoggerFactory SetPath(this ILoggerFactory factory, string filePath)
		{
			factory.AddProvider(new FileLoggerProvider(filePath));
			return factory;
		}
	}
}