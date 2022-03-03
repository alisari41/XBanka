using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {//User sisteme giriş yapacak veya sisteme kayıtlı değilse kayıt olucak
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);//Kullanıcı var mı. Daha önce var mı diye kontrol etcem
        IDataResult<AccessToken> CreateAccessToken(User user);

    }
}
