using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }//kullanıcıya gönderilecek mesaj database hatası içini
        public int StatusCode { get; set; }//kullanıcıya gönderilcek hata kodu

        public override string ToString()
        {//direk yazdırdığımızda bunu yazsın
            return JsonConvert.SerializeObject(this);
        }
    }
}
