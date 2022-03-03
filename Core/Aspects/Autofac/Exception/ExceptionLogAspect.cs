using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerServiceBase))//Bunun base si bu değilse
            {//DatabaseLogger veya FileLogger kullandığım tipler dışında girdiyse hata versin
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase =(LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnException(IInvocation invocation,System.Exception e)
        {//Bunun detaylarını veritabanına veya dosyaya kaydedicem. Kullanıcıya bir hata mesajı vermek yerine onu loglardan izlemek için
            //Benim gelen parametredeki hatayı ortaya çıkardığı hatayı yakalamam lazım
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);//Sadce metod bilgileri gelir
            logDetailWithException.ExceptionMessage = e.Message;//Oluşan hata mesajı
            _loggerServiceBase.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameteres = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameteres.Add(new LogParameter
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameteres
            };
            return logDetailWithException;

        }
    }
}
