using System;

namespace SimpleBlogAppV2.Core.VirtualEntities
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateLastUpdated { get; set; }
	}
}