using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Dapper;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.Dapper
{
    public class DapperTicariFaliyetDal : DapperEntityRepositoryBase<TicariFaaliyetler>, ITicariFaleyetDal
    {
    }
}
