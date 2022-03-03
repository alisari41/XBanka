using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        //metodun önünde çalışcak bir operasyon yazıcaz

        
        private int _duration;//süreye ihtiyac. Bu datayı ne kadar cache de tutmak istiyorum
        private ICacheManager _cacheManager;//Hangi Cache Manageri kullanıcaz

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //Önce Cache bak cache yoksa ekle cache de varsa cacheden getir.
            var methodName=string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //{clasın adı}.{metod adı}
            var arguments = invocation.Arguments.ToList();//Parametreler
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?. ToString()??"<Null>"))})";//her bir parametre için eğer parametre varsa onu string'e çevir. Aksi taktirde parametre yokmuş"<Null>" gibi yaz
            if (_cacheManager.IsAdd(key))
            {//bu key daha önce eklenmişse .Cache'de varsa
                invocation.ReturnValue = _cacheManager.Get(key); //metodu hiç çalıştırma bunun ReturnValue değeri key değeridir
                return;
            }
            invocation.Proceed(); //Key daha önce eklenmemişse metodu çalıştır. Cache'de yoksa
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }
    }
}
