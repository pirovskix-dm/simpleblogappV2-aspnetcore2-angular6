using System.Collections.Generic;

namespace SimpleBlogAppV2.BusinessLayer.DTO
{
	public class QueryResultDTO<T>
	{
		public int TotalItems { get; set; }
		public IEnumerable<T> Items { get; set; }
	}
}