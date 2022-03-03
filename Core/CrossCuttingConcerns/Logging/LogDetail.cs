using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {//loga ait detayları tutar
        public string MethodName { get; set; }//Loga konu olan metodun ismi
        public List<LogParameter> LogParameters { get; set; }//birden fazla parametleri olabilir

    }
}
