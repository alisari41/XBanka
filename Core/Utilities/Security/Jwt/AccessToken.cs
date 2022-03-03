using System;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken
    {//Erişim Token 
        public string Token { get; set; }
        public DateTime Expiration { get; set; }//Tokenın ne kadar süre kullanacağını belirtmek için
    }
}
