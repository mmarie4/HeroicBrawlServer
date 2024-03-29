﻿using System;
using System.Linq;
using System.Security.Claims;

namespace HeroicBrawlServer.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid ExtractUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claims = claimsPrincipal.Claims;
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                throw new Exception("The user has no id in the token");
            }

            return Guid.Parse(userIdClaim);
        }
    }
}
