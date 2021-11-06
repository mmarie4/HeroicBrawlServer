using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HeroicBrawlServer.Shared.Extensions
{
    public static class StringExtensions
    {
        public static Guid ExtractUserId(this string bearerToken)
        {
            var token = new JwtSecurityToken(bearerToken);
            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("The user has no id in the token");
            }

            return new Guid(userId);
        }
    }
}
