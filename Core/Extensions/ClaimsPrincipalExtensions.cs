using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {//Mevcut kullanıcıya karşılık gelir. ClaimsPrincipal ile sadece role değil başka claimlerede erişebilmeliyim.
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {//Hangi claimstype için filtreleme yapıyorum demek 
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();  //claimsPrincipal varmı diye bakıyorum hiç sisteme login olmamış olabilir
            return result; 
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {//Burada roleri çekiyorum
            return claimsPrincipal ?.Claims(ClaimTypes.Role);
        }
    }
}
