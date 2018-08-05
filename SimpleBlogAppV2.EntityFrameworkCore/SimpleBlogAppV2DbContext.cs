using Microsoft.EntityFrameworkCore;
using SimpleBlogAppV2.Core.Entities;

namespace SimpleBlogAppV2.EntityFrameworkCore
{
	public class SimpleBlogAppV2DbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }

		public SimpleBlogAppV2DbContext(DbContextOptions<SimpleBlogAppV2DbContext> options)
			: base(options)
		{

		}
	}
}
