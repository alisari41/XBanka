using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;//Kullanıcı var mı diye kontrol edicem.
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {//Sisteme Kayıt olma
            byte[] passwordhash, passwordsalt;//aslında bunları boş gönderiyorum bir CreatePasswordHash metodunda bilgileri oluşmuş halde geliyor
            HashingHelper.CreatePasswordHash(password, out passwordhash, out passwordsalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordhash,
                PasswordSalt = passwordsalt,
                Status = true//kullanıcı ilk kez kayıt olacağı için "true" verdim.False vererek bazı onaylama işlemlerinden sonra kayıt yapılabilir.
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegisterd);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)//Böyle bir kullanıcı varmı kotrol etmek lazım
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            //buraya geliyorsa kullanıcıyı bulduk demek 
            //bize gelen şifreyi passwordsalt ve passwordhash bilgilerini kotrol etcez
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))//Şifreler eşleşmiyorsa
            {//Verify = Doğrulama
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {//Girilen kullanıcı mevcut mu
            if (_userService.GetByMail(email) != null)
            {//Böyle bir kullanıcı varsa
                return new ErrorResult(Messages.UserAlreadyExits);
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {//Kayıt olan kişi login olduktan sonra kullanıcımıza bir token vericez bu vasıta ile işlemlerini gerçekleştirecek.
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
