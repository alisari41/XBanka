using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfKanalDal : EfEntityRepositoryBase<Kanal, BankaContext>, IKanalDal
    {//Artık EfKanalDal'ın içerisinde ürün ekleme silme güncelleme hazır
        public List<Kanal> GetMusteriKanalList(Kanal kanal)
        {
            using (var context = new BankaContext())
            {
                var result = from kanallar in context.Kanallar
                    join musteriler in context.Musteriler
                        on kanallar.Id equals musteriler.KanalId
                    where kanallar.Id == kanal.Id
                    select new Kanal
                    {
                        Id = kanallar.Id,
                        Ad = kanallar.Ad
                    };
                return result.ToList();
            }
        }
    }
}
