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
    public class TicariFaliyetManager : ITicariFaliyetService
    {
        private ITicariFaleyetDal _ticariFaleyetDal;

        public TicariFaliyetManager(ITicariFaleyetDal ticariFaleyetDal)
        {
            _ticariFaleyetDal = ticariFaleyetDal;
        }

        public IDataResult<TicariFaaliyetler> GetById(int ticaeriFaliyetId)
        {
            return new SuccessDataResult<TicariFaaliyetler>(_ticariFaleyetDal.Get(ticaeriFaliyetId));
        }
        

        [CacheAspect(100)]
        [PerformanceAspect(5)]
        public IDataResult<List<TicariFaaliyetler>> GetList()
        {
            return new SuccessDataResult<List<TicariFaaliyetler>>(_ticariFaleyetDal.GetList().ToList());
        }


        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITicariFaliyetService.Get")]
        [ValidationAspect(typeof(TicariFaliyetValidator), Priority = 1)]
        public IResult Add(TicariFaaliyetler ticariFaliyet)
        {
            IResult result = BusinessRules.Run(CheckIfTicariFaaliyetNoExists(ticariFaliyet.GercekMusteriId));

            if (result != null)
            {
                return result;
            }

            _ticariFaleyetDal.Add(ticariFaliyet);
            return new SuccessResult(Messages.TicariFaaliyetAdded);
        }

        private IResult CheckIfTicariFaaliyetNoExists( int gercekMusteriId)
        {

            var result = _ticariFaleyetDal.GetList(p =>  p.GercekMusteriId == gercekMusteriId).Any();
            if (result)
            {//Eğer girilen kimlik numarası ve id sistemde varsa
                return new ErrorResult(Messages.TicariFaaliyetNameAlreadyExists);
            }

            return new SuccessResult();

        }


        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITicariFaliyetService.Get")]
        public IResult Delete(TicariFaaliyetler ticariFaaliyet)
        {
            _ticariFaleyetDal.Delete(ticariFaaliyet);
            return new SuccessResult(Messages.TicariFaaliyetDeleted);
        }


        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITicariFaliyetService.Get")]
        [ValidationAspect(typeof(TicariFaliyetValidator), Priority = 1)]
        public IResult Update(TicariFaaliyetler ticariFaaliyet)
        {
            _ticariFaleyetDal.Update(ticariFaaliyet);
            return new SuccessResult(Messages.TicariFaaliyetUpdated);
        }
    }
}
