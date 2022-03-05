using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAdresService
    {
        IDataResult<List<Adres>> GetList();
        IResult Add(Adres adres);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(Adres adres);
        IResult Update(Adres adres);
    }
}
