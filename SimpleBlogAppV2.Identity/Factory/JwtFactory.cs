using Microsoft.Extensions.Options;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Identity.Constants;
using SimpleBlogAppV2.Identity.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace SimpleBlogAppV2.Identity.Factory
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtIssuerOptions jwtOptions;

		public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
		{
			this.jwtOptions = jwtOptions.Value;
		}

		public string GenerateEncodedToken(string userName, ClaimsIdentity identity)
		{
			//var claims = new[]
			//{
			//	 new Claim(JwtRegisteredClaimNames.Sub, userName),
			//	 new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
			//	 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
			//	 identity.FindFirst(JwtClaimIdentifiers.Rol),
			//	 identity.FindFirst(JwtClaimIdentifiers.Id)
			//};

			var jwt = new JwtSecurityToken(
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				claims: identity.Claims,
				notBefore: jwtOptions.NotBefore,
				expires: jwtOptions.Expiration,
				signingCredentials: jwtOptions.SigningCredentials);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public ClaimsIdentity GenerateClaimsIdentity(AppUser user)
		{
			return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new[]
			{
				new Claim(JwtClaimIdentifiers.Id, user.Id),
				new Claim(JwtClaimIdentifiers.Rol, "Admin") // user.Role
			});
		}

		private static long ToUnixEpochDate(DateTime date)
		{
			return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
		}
	}
}