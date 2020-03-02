using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Utils
{
    public static class UserExt
    {
        public static long Id(this ClaimsPrincipal principal)
        {
            return long.Parse(principal.Identity.Name);
        }
    }
}
