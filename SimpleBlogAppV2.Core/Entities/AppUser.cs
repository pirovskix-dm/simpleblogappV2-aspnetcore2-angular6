using Microsoft.AspNetCore.Identity;
using SimpleBlogAppV2.Core.Interfaces;

namespace SimpleBlogAppV2.Core.Entities
{
	public class AppUser : IdentityUser, IEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
