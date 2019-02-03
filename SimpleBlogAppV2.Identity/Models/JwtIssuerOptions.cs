using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Identity.Models
{
	public class JwtIssuerOptions
	{
		public string Issuer { get; set; }
		public string Subject { get; set; }
		public string Audience { get; set; }
		public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
		public DateTime Expiration => IssuedAt.Add(ValidFor);
		public DateTime NotBefore { get; set; } = DateTime.UtcNow;
		public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);
		public SigningCredentials SigningCredentials { get; set; }

		public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
	}
}