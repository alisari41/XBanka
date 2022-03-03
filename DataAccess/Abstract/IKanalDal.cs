using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IKanalDal : IEntityRepository<Kanal>
    {//Temel Veriye erişim operasyonları olacak
        List<Kanal> GetMusteriKanalList(Kanal kanal);
    }
}
