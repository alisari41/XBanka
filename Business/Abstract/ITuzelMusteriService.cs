using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITuzelMusteriService
    {
        IDataResult<TuzelMusteriler> GetById(int tuzelMusteriId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<TuzelMusteriler>> GetList();
        IResult AddRange(TuzelMusteriler tuzelMusteri);
        IResult Delete(TuzelMusteriler tuzelMusteri);
        IResult Update(TuzelMusteriler tuzelMusteri);

        IResult TransactionalOperation(TuzelMusteriler tuzelMusteri);//Bütün metodlar çalışıyorsa başarılı biri çalışmıyorsa başarısız sayılır diğer işlemler geri çekilir

    }
}
