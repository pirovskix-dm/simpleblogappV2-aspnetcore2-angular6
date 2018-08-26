using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace SimpleBlogAppV2.Logger
{
	public class FileLogger : ILogger
	{
		private readonly string path;
		private readonly string sqlPath;
		private readonly string otherPath;
		private object _lock = new object();

		private string[] filter = { "SELECT", "SET", "INSERT", "GET", "POST", "PUT", "DELETE" };

		public FileLogger(string path)
		{
			this.sqlPath = Path.Combine(path, "sqlLogger.txt");
			this.otherPath = Path.Combine(path, "otherLogger.txt");

			File.WriteAllText(sqlPath, "");
			File.WriteAllText(otherPath, "");
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (formatter != null)
			{
				lock (_lock)
				{
					string result = formatter(state, exception);
					if (filter.Any(f => result.Contains(f)))
						File.AppendAllText(sqlPath, result + Environment.NewLine + Environment.NewLine);
					else
						File.AppendAllText(otherPath, result + Environment.NewLine + Environment.NewLine);
				}
			}
		}
	}
}
