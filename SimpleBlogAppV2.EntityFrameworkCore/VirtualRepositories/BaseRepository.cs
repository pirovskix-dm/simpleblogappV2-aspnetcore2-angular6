namespace SimpleBlogAppV2.EntityFrameworkCore.VirtualRepositories
{
	public abstract class BaseRepository
	{
		protected internal readonly SimpleBlogAppV2DbContext context;

		public BaseRepository(SimpleBlogAppV2DbContext context)
		{
			this.context = context;
		}
	}
}
