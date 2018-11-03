namespace SimpleBlogAppV2.BusinessLayer.DTO
{
	public class QueryObjectDTO
	{
		public string Search { get; set; }
		public string SearchBy { get; set; }
		public string Filters { get; set; }
		public string SortBy { get; set; }
		public bool IsSortAscending { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
