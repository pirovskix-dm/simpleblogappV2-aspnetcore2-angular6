using System;

namespace SimpleBlogAppV2.BusinessLayer.DTO
{
	public class PostDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string ShortContent { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateLastUpdated { get; set; }
	}
}
