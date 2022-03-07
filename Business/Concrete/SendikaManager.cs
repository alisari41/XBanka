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
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class SendikaManager : ISendikaService
    {
        private ISendikaDal _sendikaDal;

        public SendikaManager(ISendikaDal sendikaDal)
        {
            _sendikaDal = sendikaDal;
        }

        public IDataResult<Sendika> GetById(int sendikaId)
        {
            return new SuccessDataResult<Sendika>(_sendikaDal.Get(p => p.Id == sendikaId));

        }


        [PerformanceAspect(5)]//Bu metodun çalışma süresi 5 saniyeyi geçerse output'a yazılacak. Perfonmans hesaplama yapmak için
        [CacheAspect(100)]
        public IDataResult<List<Sendika>> GetList()
        {
            return new SuccessDataResult<List<Sendika>>(_sendikaDal.GetList().ToList());
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(SendikaValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("ISendikaService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani 
        public IResult Add(Sendika sendika)
        {
            //kuralları Kontrol etmek için 
            IResult result = BusinessRules.Run(CheckIfMusteriNoExists(sendika.Id));

            if (result != null)
            {
                return result;
            }

            _sendikaDal.Add(sendika);
            return new SuccessResult(Messages.SendikaAdded);

        }

        private IResult CheckIfMusteriNoExists(int sendikaId)
        {
            var result = _sendikaDal.GetList(p => p.Id == sendikaId).Any();
            if (result)
            {
                return new ErrorResult(Messages.SendikaIdAlreadyExists);
            }

            return new SuccessResult();//Boş bir successResult dönerse sorun yok Diğer metodda okumak için 
        }



        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ISendikaService.Get")]
        public IResult Delete(Sendika sendika)
        {
            _sendikaDal.Delete(sendika);
            return new SuccessResult(Messages.SendikaDeleted);
        }
        

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(SendikaValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("ISendikaService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani 
        public IResult Update(Sendika sendika)
        {
            _sendikaDal.Update(sendika);
            return new SuccessResult(Messages.SendikaUpdated);
        }
    }
}
