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
    public class TuzelMusteriManager : ITuzelMusteriService
    {
        private ITuzelMusteriDal _tuzelMusteriDal;

        public TuzelMusteriManager(ITuzelMusteriDal tuzelMusteriDal)
        {
            _tuzelMusteriDal = tuzelMusteriDal;
        }


        [CacheAspect(100)]
        public IDataResult<TuzelMusteriler> GetById(int tuzelMusteriId)
        {
            return new SuccessDataResult<TuzelMusteriler>(_tuzelMusteriDal.Get(tuzelMusteriId));
        }


        [CacheAspect(100)]
        [PerformanceAspect(5)]
        public IDataResult<List<TuzelMusteriler>> GetList()
        {
            return new SuccessDataResult<List<TuzelMusteriler>>(_tuzelMusteriDal.GetList().ToList());
        }


        //[SecuredOperation("Admin")]
        [CacheRemoveAspect("ITuzelMusteriService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani başı Get ile başlayanları temizler
        [ValidationAspect(typeof(TuzelMusteriValidator), Priority = 1)]
        public IResult AddRange(TuzelMusteriler tuzelMusteri)
        {
            IResult result = BusinessRules.Run(CheckIfTuzelMusteriNoExists(tuzelMusteri.Id, tuzelMusteri.VergiNo));

            if (result != null)
            {
                return result;
            }

            _tuzelMusteriDal.AddRange(tuzelMusteri);//İd otomatik değil ben eklicem
            return new SuccessResult(Messages.TuzelMusteriAdded);
        }

        private IResult CheckIfTuzelMusteriNoExists(int id, string vergiNo)
        {
            var result = _tuzelMusteriDal.GetList(p => p.Id == id && p.VergiNo == vergiNo).Any();
            if (result)
            {//Eğer girilen vergi numarası ve id sistemde varsa
                return new ErrorResult(Messages.TuzelMusteriNameAlreadyExists);
            }

            return new SuccessResult();
        }


      //  [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITuzelMusteriService.Get")]
        public IResult Delete(TuzelMusteriler tuzelMusteri)
        {
            _tuzelMusteriDal.Delete(tuzelMusteri);
            return new SuccessResult(Messages.TuzelMusteriDeleted);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITuzelMusteriService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani başı Get ile başlayanları temizler
        [ValidationAspect(typeof(TuzelMusteriValidator), Priority = 1)]
        public IResult Update(TuzelMusteriler tuzelMusteri)
        {
            _tuzelMusteriDal.Update(tuzelMusteri);
            return new SuccessResult(Messages.TuzelMusteriUpdated);
        }


        [TransactionScopeAspect]
        public IResult TransactionalOperation(TuzelMusteriler tuzelMusteri)
        {
            _tuzelMusteriDal.Delete(tuzelMusteri);
            _tuzelMusteriDal.Update(tuzelMusteri);
            return new SuccessResult(Messages.TuzelMusteriUpdated);
        }



    }
}
