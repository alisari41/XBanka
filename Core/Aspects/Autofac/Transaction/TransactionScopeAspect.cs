using System;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {//Bütün metodlar çalışıyorsa işlem başarılı bir tanesi çalışmazsa işlem başarısız olarak kabul edilir geri alınarak sonlandırılır.
        public override void Intercept(IInvocation invocation)
        {//bunu ezicem transaction başında sonunda değilde bir döngü olarak düşün bütün hepsinin çalışması lazım 
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();//Metod başarılı mı başarısız mı olup olmadığını burdan bakıyorum
                    transactionScope.Complete();//Metodu çalıştır eğer başarılı oluysan işlemi kabul et
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();//Metodu çalıştır eğer başarılı olmadıysa yapılan işlemleri geri al
                    throw;
                }
            }
        }
    }
}
