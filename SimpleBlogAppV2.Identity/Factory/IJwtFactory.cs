using SimpleBlogAppV2.Core.Entities;
using System.Security.Claims;

namespace SimpleBlogAppV2.Identity.Factory
{
	public interface IJwtFactory
	{
		string GenerateEncodedToken(string userName, ClaimsIdentity identity);

		ClaimsIdentity GenerateClaimsIdentity(AppUser user);
	}
}