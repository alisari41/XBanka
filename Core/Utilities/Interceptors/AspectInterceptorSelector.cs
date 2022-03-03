using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {//Bu sınıf benim merkezi noktam 
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //hangi metot ona bakıyorum
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));//Hata alırsa mesajı log dosyasına yaz.İstersem DatabaseLoggerda yazabilirim


            return classAttributes.OrderBy(x => x.Priority).ToArray();//Add metodunda birden fazla aspect kullanırsam sıralama yapabilmek için
        }
    }
}
