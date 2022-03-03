using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {//Log'lananacak operasyondaki parametrelerin bilgileri
        public string Name { get; set; }//Tablo adı gibi düşün
        public object Value { get; set; }//Tablonun bir değeri mesela id=1 name=elma ... gibi gibi
        public string Type { get; set; } // Type ise tablonun tipi Product gibi
    }
}
