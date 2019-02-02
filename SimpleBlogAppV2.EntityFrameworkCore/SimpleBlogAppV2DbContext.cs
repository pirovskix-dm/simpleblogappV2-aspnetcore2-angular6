using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.VirtualEntities;
using System.Linq;

namespace SimpleBlogAppV2.EntityFrameworkCore
{
	public class SimpleBlogAppV2DbContext : IdentityDbContext<AppUser>
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }

		public SimpleBlogAppV2DbContext(DbContextOptions<SimpleBlogAppV2DbContext> options)
			: base(options)
		{
			Database.Migrate();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>()
				.HasMany(c => c.Posts)
				.WithOne(p => p.Category)
				.OnDelete(DeleteBehavior.SetNull);

			var entityTypes = modelBuilder
				.Model
				.GetEntityTypes()
				.Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType));

			foreach (var entityType in entityTypes)
			{
				modelBuilder
					.Entity(entityType.ClrType)
					.Property(nameof(BaseEntity.DateCreated))
					.HasDefaultValueSql("GETDATE()");
				modelBuilder
					.Entity(entityType.ClrType)
					.Property(nameof(BaseEntity.DateLastUpdated))
					.HasDefaultValueSql("GETDATE()");
			}
		}
	}
}
