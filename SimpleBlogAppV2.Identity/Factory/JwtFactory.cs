using Microsoft.Extensions.Options;
using SimpleBlogAppV2.Identity.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.Identity.Factory
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtIssuerOptions jwtOptions;
		private const string Rol = "rol";
		private const string Id = "id";
		private const string ApiAccess = "api_access";

		public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
		{
			this.jwtOptions = jwtOptions.Value;
			ThrowIfInvalidOptions(this.jwtOptions);
		}

		public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
		{
			var claims = new[]
			{
				 new Claim(JwtRegisteredClaimNames.Sub, userName),
				 new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
				 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
				 identity.FindFirst(Rol),
				 identity.FindFirst(Id)
			};

			// Create the JWT security token and encode it.
			var jwt = new JwtSecurityToken(
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				claims: claims,
				notBefore: jwtOptions.NotBefore,
				expires: jwtOptions.Expiration,
				signingCredentials: jwtOptions.SigningCredentials);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
		{
			return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
			{
				new Claim(Id, id),
				new Claim(Rol, ApiAccess)
			});
		}

		private static long ToUnixEpochDate(DateTime date)
		{
			return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
		}

		private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			if (options.ValidFor <= TimeSpan.Zero)
			{
				throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
			}

			if (options.SigningCredentials == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
			}

			if (options.JtiGenerator == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
			}
		}
	}
}