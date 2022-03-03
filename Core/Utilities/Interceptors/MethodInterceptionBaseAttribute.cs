using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Method,AllowMultiple = true,Inherited = true)]//Bu işlemler sadece Classların en tepesinde kullanılabilir en tepede kullanılırsa bütün metotlar etkilenir veya metodların üzerinde kullanılabilir.1den fazla istenirse kullanılabilir.Onu Inherited eden alt classlarda da kullanılabilsin.
    public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor
    {
        public int Priority { get; set; }//ProductManager da zaman geçtikçe birden fazla Aspect vermek istediğimde bunları sıralandırmak için

        public virtual void Intercept(IInvocation invocation)
        {//Metodun arasına girmek için kullandım
            
        }
    }
}
