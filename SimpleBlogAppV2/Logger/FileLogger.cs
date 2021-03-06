﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace SimpleBlogAppV2.Logger
{
	public class FileLogger : ILogger
	{
		private readonly string sqlPath;
		private readonly string otherPath;
		private readonly object _lock = new object();

		private readonly string[] filter = { "SELECT", "SET", "INSERT", "GET", "POST", "PUT", "DELETE", "LINQ", "expression", "Request finished" };

		public FileLogger(string path)
		{
			this.sqlPath = Path.Combine(path, "sqlLogger.txt");
			this.otherPath = Path.Combine(path, "otherLogger.txt");

			File.WriteAllText(sqlPath, "");
			File.WriteAllText(otherPath, "");
		}

		private static ILogger instance = null;

		public static ILogger Create(string path)
		{
			return instance ?? (instance = new FileLogger(path));
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
			if (formatter == null)
			{
				return;
			}

			lock (_lock)
			{
				try
				{
					string result = formatter(state, exception);
					if (filter.Any(f => result.Contains(f)))
					{
						File.AppendAllText(sqlPath, result + Environment.NewLine + Environment.NewLine);
					}
					else
					{
						File.AppendAllText(otherPath, result + Environment.NewLine + Environment.NewLine);
					}
				}
				catch
				{
				}
			}
		}
	}
}