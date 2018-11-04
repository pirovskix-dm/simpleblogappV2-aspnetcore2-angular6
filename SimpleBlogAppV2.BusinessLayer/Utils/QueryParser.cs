using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleBlogAppV2.BusinessLayer.Utils
{
	public static class QueryParser
	{
		[DebuggerStepThrough]
		public static Dictionary<string, int> ParseFilter(string filters)
		{
			if (string.IsNullOrWhiteSpace(filters))
				return null;

			var result = new Dictionary<string, int>();

			string[] pairs = filters.Split(',', StringSplitOptions.RemoveEmptyEntries);
			if (pairs.Length < 1)
				return null;

			foreach (var pair in pairs)
			{
				string[] valueKey = pair.Split(':', StringSplitOptions.RemoveEmptyEntries);
				if (valueKey.Length != 2)
					throw new ArgumentException("Invalid filter key `" + pair + "`");

				TryAddToDictionary(result, valueKey[0], valueKey[1]);
			}

			return result;
		}

		[DebuggerStepThrough]
		public static string[] ParseSearch(string search)
		{
			if (string.IsNullOrWhiteSpace(search))
				return null;

			return search.Split(',', StringSplitOptions.RemoveEmptyEntries);
		}

		[DebuggerStepThrough]
		private static void TryAddToDictionary(Dictionary<string, int> d, string key, string value)
		{
			if (d.ContainsKey(key) || string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Invalid filter key");

			int.TryParse(value, out int intValue);
			if (intValue < 1)
				throw new ArgumentException("Invalid filter value");

			d.Add(key, intValue);
		}
	}
}
