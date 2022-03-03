using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {//Başka bir teknik kullandığımda Jwt ye bağlı kalmamak için bu arayüzü oluşturdum
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);//Kullanıcının rollerinide verip onların Token'a eklenmesini istiyorum
    }
}
