using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MusteriManager : IMusteriService
    {
        private IMusteriDal _musteriDal;
        private IKanalService _kanalService;

        public MusteriManager(IMusteriDal musteriDal)
        {
            //Yarın öbür gün başka bir ORM aracı implement ediyorsa onu kullanabilirim
            _musteriDal = musteriDal;
        }


        public IDataResult<Musteri> GetById(int musteriId)
        {
            //Burada sürekli bağımlılık var. Yani proje sürekli EF olarak yazılması gerekir diyor. Bunu ortadan kaldırmak için Dependency İnjection işlemi yaptım.
            //EfProductDal productDal = new EfProductDal();
            //return productDal.Get(p => p.ProductId == productId);

            //Eğer bu şekilde kullanırsam "Dependency İnjection" sayesinde bağımlılığı ortadan kaldırıyor.Bakacak olursak ortada EFcore kodu gözümüyor.EFcore'a bağımlılık ortadan kalktı. Artık ona bağılı değilim.
            //Yani diğer ORM araçlarıda işlem yapabilmelidir.


            //Bu işlem döndürülüyorsa başarılı olmuştur onun için bana sadece data'sı yeter
            return new SuccessDataResult<Musteri>(_musteriDal.Get(p => p.Id == musteriId));
        }

        [PerformanceAspect(5)]//Bu metodun çalışma süresi 5 saniyeyi geçerse output'a yazılacak. Perfonmans hesaplama yapmak için
        public IDataResult<List<Musteri>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Musteri>>(_musteriDal.GetList().ToList());
        }



        [SecuredOperation("Musteri.List,Admin")]//Yetkileri mevcutmu
        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [LogAspect(typeof(DatabaseLogger))]//Loglama yapıldı ...... FileLogger da kullanabilirim
        public IDataResult<List<Musteri>> GetListByKanal(int kanalId)
        {
            return new SuccessDataResult<List<Musteri>>(_musteriDal.GetList(p => p.KanalId == kanalId).ToList());
        }


        //Cros Cutting Concerns - Validation, Cache, Log, Performance, Auth, Transaction
        //AOP (Aspect Oriented Programing) Yazılım Geliştirme Yaklaşımı


        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(MusteriValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IMusteriService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani başı Get ile başlayanları temizler
        public IResult Add(Musteri musteri)
        {
            //kuralları Kontrol etmek için 
            IResult result = BusinessRules.Run(CheckIfMusteriNoExists(musteri.MusteriNo), CheckIfKanalIsEnabled());

            if (result != null)
            {
                return result;
            }


            _musteriDal.Add(musteri);
            //"Ürün başarıyla eklendi."  parantez içinde bunu kullanmak Magic Stringlere giriyor yani bu mesajı bir çok yerde kullandığımı varsayarsak
            //Bunu değiştirmek istediğimizde çok zorlanacağız. O yüzden Magic Stringlerden kurtulmak için bir sınıf oluşturdum mesajları oradan çekiyorum
            return new SuccessResult(Messages.MusteriAdded);
        }


        private IResult CheckIfMusteriNoExists(string musteriNo)
        {//Metodlar çoğunlukla IResult kullanılıyor dikkat et

            var result = _musteriDal.GetList(p => p.MusteriNo == musteriNo).Any();
            if (result)
            {//Eğer girilen ürün adı sistemde varsa
                return new ErrorResult(Messages.MusteriNameAlreadyExists);
            }

            return new SuccessResult();//Boş bir successResult dönerse sorun yok Diğer metodda okumak için 

        }

        private IResult CheckIfKanalIsEnabled()
        {//Kanallar ile iş yapmayı göstermek için uyduruyorum Farklı servisleri kullanmayı göstermek için Mesajlar ve kurulan sistem değişebilir
            var result = _kanalService.GetList();
            if (result.Data.Count <= 10)
            {
                return new ErrorResult(Messages.MusteriNameAlreadyExists);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Musteri musteri)
        {
            // Transaction metodunu göstermek için Uydurma işlemler yapıyorum 
            _musteriDal.Update(musteri);//başarılı olsun 
            _musteriDal.Add(musteri);   // başarısız olsun sonrasında Update de geri alınsın
            return new SuccessResult(Messages.MusteriUpdated);
        }


        public IResult Delete(Musteri musteri)
        {
            _musteriDal.Delete(musteri);
            return new SuccessResult(Messages.MusteriDeleted);
        }

        public IResult Update(Musteri musteri)
        {
            _musteriDal.Update(musteri);
            return new SuccessResult(Messages.MusteriUpdated);
        }
    }
}
