using SimpleBlogAppV2.Core.Interfaces;
using SimpleBlogAppV2.Core.VirtualEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlogAppV2.Core.Entities
{
	[Table("Category")]
	public class Category : BaseLookup, IEntity
	{
		public IList<Post> Posts { get; set; }
		public Category()
		{
			Posts = new List<Post>();
		}
	}
}
