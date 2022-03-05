using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, BankaContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {//Bir kullanıcının rollerini çekmek istiyorum.
            //Beni bunu yapmam için join işlemi yapmam lazım Yani 2 tabloyu birleştirmem lazım.

            using (var context = new BankaContext())
            {//Gelen User bilgilerinin join işlemleri ile rollerini listeledim
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id //sınırlandırma yapıldı
                             select new OperationClaim
                             {
                                 //operationClaim rol listesini döndürmcem
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }

        }
    }
}
