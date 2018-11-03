using System.Collections.Generic;

namespace SimpleBlogAppV2.Core.Query
{
	public class QueryObject
	{
		public string Search { get; set; }
		public string[] SearchBy { get; set; }
		public Dictionary<string, int> Filters { get; set; }
		public string SortBy { get; set; }
		public bool IsSortAscending { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
