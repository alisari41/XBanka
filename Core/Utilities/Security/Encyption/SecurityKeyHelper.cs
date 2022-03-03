using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encyption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {//bunu çoğu yerde kulllanabileceğimiz için yeni bir dosya sınıf içirisinde kodlayıp diğer taraflardan çağırıyorum.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
