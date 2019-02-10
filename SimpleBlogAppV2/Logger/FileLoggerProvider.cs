using Microsoft.Extensions.Logging;

namespace SimpleBlogAppV2.Logger
{
	public class FileLoggerProvider : ILoggerProvider
	{
		private readonly string path;

		public FileLoggerProvider(string path)
		{
			this.path = path;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return FileLogger.Create(path);
		}

		public void Dispose()
		{
		}
	}
}