using System;
using System.Linq;
using System.Security.Claims;

namespace API.Core.Platform.Security
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == "email")?.Value;
        }

        public static string GetId(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == "userId")?.Value;
        }
    }
}
