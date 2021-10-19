using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Jwt
{
    public static class JwtExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            if(claim == null)
            {
                return 0;
            }

            return Convert.ToInt64(claim.Value);
        }
    }
}
