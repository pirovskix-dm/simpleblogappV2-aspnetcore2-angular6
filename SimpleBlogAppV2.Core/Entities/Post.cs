using SimpleBlogAppV2.Core.Interfaces;
using SimpleBlogAppV2.Core.VirtualEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlogAppV2.Core.Entities
{
	[Table("Post")]
	public class Post : BaseEntity, IEntity
	{
		[StringLength(100)]
		public string Title { get; set; }

		public string Content { get; set; }

		[StringLength(500)]
		public string ShortContent { get; set; }
	}
}
