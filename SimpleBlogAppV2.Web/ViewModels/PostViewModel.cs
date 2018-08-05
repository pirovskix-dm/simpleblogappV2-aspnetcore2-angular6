using System.ComponentModel.DataAnnotations;

namespace SimpleBlogAppV2.Web.ViewModels
{
	public class PostViewModel
	{
		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		[StringLength(500)]
		public string ShortContent { get; set; }
	}
}
