using SimpleBlogAppV2.Core.Interfaces;
using SimpleBlogAppV2.Core.VirtualEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlogAppV2.Core.Entities
{
	[Table("Post")]
	public class Post : BaseEntity, IEntity
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public string ShortContent { get; set; }

		public int? CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
