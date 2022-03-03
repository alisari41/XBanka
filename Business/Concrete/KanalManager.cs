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
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace Business.Concrete
{
    public class KanalManager : IKanalService
    {
        private IKanalDal _kanalDal;

        public KanalManager(IKanalDal kanalDal)
        {
            _kanalDal = kanalDal;
        }

        public IDataResult<Kanal> GetById(int kanalId)
        {
            return new SuccessDataResult<Kanal>(_kanalDal.Get(p => p.Id == kanalId));
        }


        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        public IDataResult<List<Kanal>> GetList()
        {
            return new SuccessDataResult<List<Kanal>>(_kanalDal.GetList().ToList());
        }

        [SecuredOperation("Admin")]//Yetkileri mevcutmu
        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [LogAspect(typeof(DatabaseLogger))]//Loglama yapıldı ...... FileLogger da kullanabilirim
        [PerformanceAspect(5,Priority = 1)]//Bu metodun çalışma süresi 5 saniyeyi geçerse output'a yazılacak. Perfonmans hesaplama yapmak için
        public List<Kanal> GetMusteriKanalList(Kanal kanal)
        {//Bu Kanalı hangi Müşteriler aldıysa onlarla beraber sıralayacağım. 2 tabloyu birleştirme işlemi gerçekleştiriliyor.
            Thread.Sleep(5000);
            return _kanalDal.GetMusteriKanalList(kanal);
        }

        [ValidationAspect(typeof(KanalValidator),Priority = 1)]
        [CacheRemoveAspect("IKanalService.Get")]
        public IResult Add(Kanal kanal)
        {
            var result = BusinessRules.Run(CheckIfKanalNameExists(kanal.Ad));//Kural çalıştır

            if (result!=null)
            {
                return result;
            }
            _kanalDal.Add(kanal);

            return new SuccessResult(Messages.KanalAdded);
        }

        private IResult CheckIfKanalNameExists(string kanalAdi)
        {// Kanal adı daha önce kullanılmış mı
            //Any metodu, bir koleksiyonda belirtilen koşula uygun kayıt varsa geriye true, yoksa false değerini döndürmektetedir.
            var result = _kanalDal.GetList(p => p.Ad == kanalAdi).Any();
            if (result)
            {// Kanal adı varsa
                return new ErrorResult(Messages.KanalNameAlreadyExists);
            }

            return new SuccessResult();
        }

        public IResult Delete(Kanal kanal)
        {
            _kanalDal.Delete(kanal);
            return new SuccessResult(Messages.KanalDeleted);
        }

        public IResult Update(Kanal kanal)
        {
            _kanalDal.Update(kanal);
            return new SuccessResult(Messages.KanalUpdated);
        }
    }
}
