using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public class GercekMusteriManager : IGercekMusteriService
    {
        private IGercekMusteriDal _gercekMusteriDal;

        public GercekMusteriManager(IGercekMusteriDal gercekMusteriDal)
        {
            _gercekMusteriDal = gercekMusteriDal;
        }

        public IDataResult<GercekMusteriler> GetById(int gercekMusteriId)
        {
            return new SuccessDataResult<GercekMusteriler>(_gercekMusteriDal.Get(gercekMusteriId));
        }


        [CacheAspect(100)]
        [PerformanceAspect(5)]
        public IDataResult<List<GercekMusteriler>> GetList()
        {
            return new SuccessDataResult<List<GercekMusteriler>>(_gercekMusteriDal.GetList().ToList());
        }


        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IGercekMusteriService.Get")]
        [ValidationAspect(typeof(GercekMusteriValidator), Priority = 1)]
        public IResult AddRange(GercekMusteriler gercekMusteriler)
        {
            var result = BusinessRules.Run(CheckIfTuzelMusteriNoExists(gercekMusteriler.Id, gercekMusteriler.TcKimlikNo));

            if (result != null)
            {
                return result;
            }

            _gercekMusteriDal.AddRange(gercekMusteriler);//İd ben giricem
            return new SuccessResult(Messages.GercekMusteriAdded);
        }

        private IResult CheckIfTuzelMusteriNoExists(int id, string TcKimlikNo)
        {
            var result = _gercekMusteriDal.GetList(p => p.Id == id && p.TcKimlikNo == TcKimlikNo).Any();
            if (result)
            {//Eğer girilen kimlik numarası ve id sistemde varsa
                return new ErrorResult(Messages.GercekMusteriNameAlreadyExists);
            }

            return new SuccessResult();

        }


        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IGercekMusteriService.Get")]
        public IResult Delete(GercekMusteriler gercekMusteriler)
        {
            _gercekMusteriDal.Delete(gercekMusteriler);
            return new SuccessResult(Messages.GercekMusteriDeleted);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IGercekMusteriService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani başı Get ile başlayanları temizler
        [ValidationAspect(typeof(GercekMusteriValidator), Priority = 1)]
        public IResult Update(GercekMusteriler gercekMusteriler)
        {
            _gercekMusteriDal.Update(gercekMusteriler);
            return new SuccessResult(Messages.GercekMusteriUpdated);
        }
    }
}
