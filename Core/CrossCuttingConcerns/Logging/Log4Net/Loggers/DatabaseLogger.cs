using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger:LoggerServiceBase
    {//Veri tabanına yapacağımız log işlemini anlatıyor

        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
