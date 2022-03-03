using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)//Metod çalışmadan önce sen çalış demek 
        {//invocation çalıştırılmaya çalışan metod

        }
        protected virtual void OnAfter(IInvocation invocation)//Metod çalıştıktan sonra sen çalış demek 
        {//invocation çalıştırılmaya çalışan metod

        }
        protected virtual void OnException(IInvocation invocation,System.Exception e)//Metod hata verdiğinde sen çalış demek 
        {//invocation çalıştırılmaya çalışan metod

        }
        protected virtual void OnSuccess(IInvocation invocation)//Metod başarılı olduğunda sen çalış demek 
        {//invocation çalıştırılmaya çalışan metod

        }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed(); //Operasyonu (metotu) çalıştır Demek
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation,e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
