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

        public static Guid? GetId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.SingleOrDefault(c => c.Type == "userId")?.Value;

            if (userId != null)
            {
                return new Guid(userId);
            }

            return null;
        }
    }
}
