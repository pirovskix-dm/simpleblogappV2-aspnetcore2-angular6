using System;

namespace SimpleBlogAppV2.BusinessLayer.DTO
{
	public class PostDTO
	{
		public int Id { get; set; }

		//[Required]
		//[StringLength(100)]
		public string Title { get; set; }

		//[Required]
		public string Content { get; set; }

		//[StringLength(500)]
		public string ShortContent { get; set; }

		public CategoryDTO Category { get; set; }

		public DateTime? DateCreated { get; set; }

		public DateTime? DateLastUpdated { get; set; }
	}
}
