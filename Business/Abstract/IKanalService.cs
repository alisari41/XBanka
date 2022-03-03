using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IKanalService
    {
        IDataResult<Kanal> GetById(int kanalId);
        IDataResult<List<Kanal>> GetList();
        List<Kanal> GetMusteriKanalList(Kanal kanal);// Join işlemi gerçekleştirilecek
        IResult Add(Kanal kanal);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(Kanal kanal);
        IResult Update(Kanal kanal);
    }
}
