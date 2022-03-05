using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AdresManager :IAdresService
    {
        private IAdresDal _adresDal;

        public AdresManager(IAdresDal adresDal)
        {
            _adresDal = adresDal;
        }

        [SecuredOperation("Admin")]
        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [PerformanceAspect(5)]//Bu metodun çalışma süresi 5 saniyeyi geçerse output'a yazılacak. Perfonmans hesaplama yapmak için
        public IDataResult<List<Adres>> GetList()
        {
            return new SuccessDataResult<List<Adres>>(_adresDal.GetList().ToList());
        }


        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(AdresValidator), Priority = 1)]
        [CacheRemoveAspect("IAdresService.Get")]//Yeni adres eklediği zaman Ön belleği temizleme işlemi. İçersinde IAdresService.Get olanları Yani 
        public IResult Add(Adres adres)
        {
            _adresDal.Add(adres);
            return new SuccessResult(Messages.AdresAdded);
        }


        [CacheRemoveAspect("IAdresService.Get")]
        public IResult Delete(Adres adres)
        {
            _adresDal.Delete(adres);
            return new SuccessResult(Messages.AdresDeleted);
        }
        

        [ValidationAspect(typeof(AdresValidator), Priority = 1)]
        [CacheRemoveAspect("IAdresService.Get")]
        public IResult Update(Adres adres)
        {
            _adresDal.Update(adres);
            return new SuccessResult(Messages.AdresUpdated);
        }
    }
}
