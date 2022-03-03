using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//Gönderilen validatorType bir IValidator türünde değilse
            {
                throw new System.Exception(AspectMessages.WrongValidationType);//Mesaj veriyorum
            }

            //hata yoksa
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //ihtiyacım olan diğer nesne sınıfa oluşmak için objecte ulaşmak için
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//metodun argumanlarını(parametrelerini productmanagerdaki Add'nin parametresindeki sınıf ) buluyor. Birden fazlada olabilir.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
