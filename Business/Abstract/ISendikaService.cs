using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISendikaService
    {
        IDataResult<Sendika> GetById(int sendikaId);
        IDataResult<List<Sendika>> GetList();
        IResult Add(Sendika sendika);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(Sendika sendika);
        IResult Update(Sendika sendika);
    }
}
