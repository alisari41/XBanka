using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MusteriValidator : AbstractValidator<Musteri>
    {//Musteri Entity'sini doğrulayacak ... AbstractValidator için FluentValidation kütüphanesi indirmek gerekir.
        public MusteriValidator()
        {
            // Aslında tek bir satırda kullanılabilir. Fakat  1 satırda 2 kural olmuş olur. ayırmak  sade ve daha güzel (yazım tekniği)..SOLİD e uyuyorum
            //Mesela tek satırda 2 kural yan yana olsaydı Mesajjı nasıl vericektim gibi ayırarak kullanmaya alışmalıyım
            RuleFor(p => p.MusteriNo).NotEmpty(); //Kural. ProductName boş olamaz ve  P yi yukardan gelen <Product> nesnelerinden alır
            RuleFor(p => p.MusteriNo).Length(2, 10);// min 2(dahil) karakter max 10(dahil) karakter olabilir.

            RuleFor(p => p.KanalId).NotEmpty();
            RuleFor(p => p.MusteriNo).Must(StartWithWith);//Belli bir alan ile başlamalı kuralı. Mesela faturanın başına 00 koyun
        }

        private bool StartWithWith(string arg)
        {//Musterilerin Musteri numaraları bunlara göre başlamalı. GerçekMüşteriler "G" TüzelMüşteriler "T" Sertifakalar "S"
            return arg.StartsWith("G") || arg.StartsWith("T")||arg.StartsWith("S");
        }
    }
}
