using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IGercekMusteriService
    {
        IDataResult<GercekMusteriler> GetById(int gercekMusteriId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<GercekMusteriler>> GetList();
        IResult AddRange(GercekMusteriler gercekMusteriler);
        IResult Delete(GercekMusteriler gercekMusteriler);
        IResult Update(GercekMusteriler gercekMusteriler);
    }
}
