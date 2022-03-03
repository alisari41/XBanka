using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserForLoginDto : IDto //Çıplak class kalmaması için bir tane Core'da IDto oluşturdum
    {//İşimi görecek nesneler üretmek için kullanıyorum.
        //Mvc içersinde kullanırsam Projeyi değiştirdiğimde veya API ye geçtiğimde sıkıntı çıkarır o yüzden Entities içerisinde kullandım
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
