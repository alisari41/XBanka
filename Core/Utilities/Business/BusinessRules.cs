using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {//İş kurallarını her yere yazmak yerine bir sınıf oluşturdum
        public static IResult Run(params IResult[] logics)//iş kuralları çalıştırmak için bir array istedim
        {//Metodlar çoğunlukla IResult kullanılıyor dikkat et
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;//null dönerse sorun yok
        }
    }
}
