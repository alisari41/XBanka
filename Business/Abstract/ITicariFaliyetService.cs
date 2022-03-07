using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITicariFaliyetService
    {
        IDataResult<TicariFaaliyetler> GetById(int ticaeriFaliyetId);
        IDataResult<List<TicariFaaliyetler>> GetList();
        IResult Add(TicariFaaliyetler ticaeriFaliyet);
        IResult Delete(TicariFaaliyetler ticaeriFaliyet);
        IResult Update(TicariFaaliyetler ticaeriFaliyet);
    }
}
